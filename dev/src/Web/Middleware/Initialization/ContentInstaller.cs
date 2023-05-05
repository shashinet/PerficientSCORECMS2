using EPiServer;
using EPiServer.Authorization;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Enterprise;
using EPiServer.Find.Cms;
using EPiServer.Security;
using EPiServer.ServiceLocation;
using EPiServer.Shell.Security;
using EPiServer.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Perficient.Infrastructure.Settings.Interfaces;
using Perficient.Infrastructure.Settings.Models.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Perficient.Web.Middleware.Initialization
{
    public class ContentInstaller : IBlockingFirstRequestInitializer
    {
        private readonly UIUserProvider _uIUserProvider;
        private readonly UIRoleProvider _uIRoleProvider;
        private readonly ISiteDefinitionRepository _siteDefinitionRepository;
        private readonly ContentRootService _contentRootService;
        private readonly IContentRepository _contentRepository;
        private readonly IDataImporter _dataImporter;
        private readonly ISettingsService _settingsService;
        private readonly ILanguageBranchRepository _languageBranchRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly EventedIndexingSettings _eventedIndexingSettings;
        private readonly IPrincipalAccessor _principalAccessor;
        private readonly ContentRootRepository _contentRootRepository;

        public ContentInstaller(UIUserProvider uIUserProvider,
            UIRoleProvider uIRoleProvider,
            ISiteDefinitionRepository siteDefinitionRepository,
            ContentRootService contentRootService,
            IContentRepository contentRepository,
            IDataImporter dataImporter,
            ISettingsService settingsService,
            ILanguageBranchRepository languageBranchRepository,
            IWebHostEnvironment webHostEnvironment,
            EventedIndexingSettings eventedIndexingSettings,
            IPrincipalAccessor principalAccessor,
            ContentRootRepository contentRootRepository)
        {
            _uIUserProvider = uIUserProvider;
            _uIRoleProvider = uIRoleProvider;
            _siteDefinitionRepository = siteDefinitionRepository;
            _contentRootService = contentRootService;
            _contentRepository = contentRepository;
            _dataImporter = dataImporter;
            _settingsService = settingsService;
            _languageBranchRepository = languageBranchRepository;
            _webHostEnvironment = webHostEnvironment;
            _eventedIndexingSettings = eventedIndexingSettings;
            _principalAccessor = principalAccessor;
            _contentRootRepository = contentRootRepository;
        }

        public bool CanRunInParallel => false;

        public async Task InitializeAsync(HttpContext httpContext)
        {
            InstallDefaultContent(httpContext);
            _settingsService.InitializeSettings();

            await CreateUser("admin2", "admin2@perficient.com", new[] { Roles.Administrators, Roles.WebAdmins });
            if (await IsAnyUserRegistered())
            {
                return;
            }

            await CreateUser("admin", "admin@perficient.com", new[] { Roles.Administrators, Roles.WebAdmins });

        }

        private void InstallDefaultContent(HttpContext context)
        {
            if (_siteDefinitionRepository.List().Any() || Type.GetType("Foundation.Features.Setup.SetupController, Foundation") != null)
            {
                return;
            }

            var request = context.Request;

            var siteDefinition = new SiteDefinition
            {
                Name = "Perficient",
                SiteUrl = new Uri(request.GetDisplayUrl()),
            };

            siteDefinition.Hosts.Add(new HostDefinition()
            {
                Name = request.Host.Host,
                Type = HostDefinitionType.Primary
            });

            siteDefinition.Hosts.Add(new HostDefinition()
            {
                Name = HostDefinition.WildcardHostName,
                Type = HostDefinitionType.Undefined
            });

            var registeredRoots = _contentRootRepository.List();
            var settingsRootRegistered = registeredRoots.Any(x => x.Key.Equals(SettingsFolder.SettingsRootName));

            if (!settingsRootRegistered)
            {
                _contentRootService.Register<SettingsFolder>(SettingsFolder.SettingsRootName, SettingsFolder.SettingsRootGuid, ContentReference.RootPage);
            }

            var importPath = Path.Combine(_webHostEnvironment.ContentRootPath, "App_Data/cms.episerverdata");

            if (File.Exists(importPath))
            {

                CreateSite(new FileStream(importPath,
                        FileMode.Open,
                        FileAccess.Read,
                        FileShare.Read),
                    siteDefinition,
                    ContentReference.RootPage);
            }

            ServiceLocator.Current.GetInstance<ISettingsService>().UpdateSettings();

            _principalAccessor.Principal = new GenericPrincipal(new GenericIdentity("Importer"), null);
        }

        private void CreateSite(Stream stream, SiteDefinition siteDefinition, ContentReference startPage)
        {
            _eventedIndexingSettings.EventedIndexingEnabled = false;
            _eventedIndexingSettings.ScheduledPageQueueEnabled = false;
            ImportEpiserverContent(stream, startPage, siteDefinition);
            _eventedIndexingSettings.EventedIndexingEnabled = true;
            _eventedIndexingSettings.ScheduledPageQueueEnabled = true;
        }

        public bool ImportEpiserverContent(Stream stream,
            ContentReference destinationRoot,
            SiteDefinition siteDefinition = null)
        {
            var success = false;
            try
            {
                var status = _dataImporter.Status;

                if (status == null)
                {
                    return false;
                }

                UpdateLanguageBranches(status);
                if (siteDefinition != null && !ContentReference.IsNullOrEmpty(status.ImportedRoot))
                {
                    siteDefinition.StartPage = status.ImportedRoot;
                    _siteDefinitionRepository.Save(siteDefinition);
                    SiteDefinition.Current = siteDefinition;
                    success = true;
                }
            }
            catch
            {
                success = false;
            }

            return success;
        }

        private void UpdateLanguageBranches(IImportStatus status)
        {
            if (status.ContentLanguages == null)
            {
                return;
            }

            foreach (var languageId in status.ContentLanguages)
            {
                var languageBranch = _languageBranchRepository.Load(languageId);

                if (languageBranch == null)
                {
                    languageBranch = new LanguageBranch(languageId);
                    _languageBranchRepository.Save(languageBranch);
                }
                else if (!languageBranch.Enabled)
                {
                    languageBranch = languageBranch.CreateWritableClone();
                    languageBranch.Enabled = true;
                    _languageBranchRepository.Save(languageBranch);
                }
            }
        }

        private async Task<bool> IsAnyUserRegistered()
        {
            var res = await _uIUserProvider.GetAllUsersAsync(0, 1).CountAsync();

            return res > 0;
        }

        private async Task CreateUser(string username, string email, IEnumerable<string> roles)
        {
            var result = await _uIUserProvider.CreateUserAsync(username, "Pass@word1", email, null, null, true);

            if (result.Status == UIUserCreateStatus.Success)
            {
                foreach (var role in roles)
                {
                    var exists = await _uIRoleProvider.RoleExistsAsync(role);
                    if (!exists)
                    {
                        await _uIRoleProvider.CreateRoleAsync(role);
                    }
                }

                await _uIRoleProvider.AddUserToRolesAsync(result.User.Username, roles);
            }
        }
    }

    public static class ZipExtensions
    {
        [Flags]
        private enum Knowns : byte
        {
            None = 0,
            Size = 0x01,
            CompressedSize = 0x02,
            Crc = 0x04,
            Time = 0x08,
            ExternalAttributes = 0x10,
        }

        public static bool IsDirectory(this ZipArchiveEntry entry)
            => entry.FullName.Length > 0
            && (entry.FullName[entry.FullName.Length - 1] == '/' || entry.FullName[entry.FullName.Length - 1] == '\\');

        public static bool IsFile(this ZipArchiveEntry entry) => !entry.IsDirectory();
    }
}
