using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAccess;
using EPiServer.Logging;
using EPiServer.Security;
using EPiServer.Web;
using Microsoft.Extensions.Configuration;
using Perficient.Infrastructure.Templates.Extensions;
using Perficient.Infrastructure.Templates.Interfaces;
using Perficient.Infrastructure.Templates.Models;
using System;
using System.Collections.Concurrent;
using System.Linq;
using ILogger = EPiServer.Logging.ILogger;

namespace Perficient.Infrastructure.Templates.Services
{
    public class TemplatesService : ITemplatesService
    {
        private readonly IContentRepository _contentRepository;
        private readonly ContentRootService _contentRootService;
        private readonly ILogger _log = LogManager.GetLogger();
        private readonly ISiteDefinitionEvents _siteDefinitionEvents;
        private readonly ISiteDefinitionRepository _siteDefinitionRepository;
        private readonly IContentTypeRepository _contentTypeRepository;
        private readonly IConfiguration _configuration;
        private readonly ContentAssetHelper _contentAssetHelper;

        public TemplatesService(
            IContentRepository contentRepository,
            ContentRootService contentRootService,
            ISiteDefinitionEvents siteDefinitionEvents,
            ISiteDefinitionRepository siteDefinitionRepository,
            IContentTypeRepository contentTypeRepository,
            IConfiguration configuration,
            ContentAssetHelper contentAssetHelper)
        {
            _contentRepository = contentRepository;
            _contentRootService = contentRootService;
            _siteDefinitionEvents = siteDefinitionEvents;
            _siteDefinitionRepository = siteDefinitionRepository;
            _contentTypeRepository = contentTypeRepository;
            _configuration = configuration;
            _maximumDepth = int.TryParse(_configuration["TemplateSettings:MaximumDepth"], out int maximumDepth)
                ? maximumDepth
                : 10;
            _contentAssetHelper = contentAssetHelper;
        }

        public ContentReference TemplatesRoot { get; set; }

        private readonly int _maximumDepth;

        public void InitializeTemplates()
        {
            try
            {
                RegisterContentRoots();
            }
            catch (NotSupportedException notSupportedException)
            {
                _log.Error($"[Templates] {notSupportedException.Message}", exception: notSupportedException);
                throw;
            }

            _siteDefinitionEvents.SiteCreated += SiteCreated;
            _siteDefinitionEvents.SiteUpdated += SiteUpdated;
            _siteDefinitionEvents.SiteDeleted += SiteDeleted;
        }

        public void UninitializeTemplates()
        {
            _siteDefinitionEvents.SiteCreated -= SiteCreated;
            _siteDefinitionEvents.SiteUpdated -= SiteUpdated;
            _siteDefinitionEvents.SiteDeleted -= SiteDeleted;
        }

        public void UpdateTemplates()
        {
            var root = GetTemplatesRoot();

            if (root == null)
            {
                return;
            }

            TemplatesRoot = root;

            var children = _contentRepository.GetChildren<TemplatesRootFolder>(TemplatesRoot);

            UpdateGlobalFolder(children.FirstOrDefault(x => x.Name.Equals(TemplatesRootFolder.TemplatesGlobalName)));

            foreach (var siteDefinition in _siteDefinitionRepository.List())
            {
                var siteFolder =
                    children.FirstOrDefault(x => x.Name.Equals(siteDefinition.Name, StringComparison.InvariantCultureIgnoreCase));

                UpdateSiteFolder(siteDefinition, siteFolder);
            }
        }

        private void UpdateGlobalFolder(TemplatesRootFolder globalFolder)
        {
            ContentReference globalFolderRef;

            // create For All Sites folder if it is not existed
            if (globalFolder == null)
            {
                globalFolder = _contentRepository.GetDefault<TemplatesRootFolder>(TemplatesRoot);
                globalFolder.Name = TemplatesRootFolder.TemplatesGlobalName;

                globalFolderRef = _contentRepository.Save(globalFolder, SaveAction.Publish, AccessLevel.NoAccess);
            }
            else
            {
                globalFolderRef = globalFolder.ContentLink;
            }

            UpdateTemplateSubfolders(globalFolderRef);
        }

        private void RegisterContentRoots()
        {
            var registeredRoots = _contentRepository.GetItems(_contentRootService.List(), new LoaderOptions());
            var templatesRootRegistered = registeredRoots.Any(x => x.ContentGuid == TemplatesRootFolder.TemplatesRootGuid && x.Name.Equals(TemplatesRootFolder.TemplatesRootName));

            if (!templatesRootRegistered)
            {
                _contentRootService.Register<TemplatesRootFolder>(TemplatesRootFolder.TemplatesRootName, TemplatesRootFolder.TemplatesRootGuid, ContentReference.RootPage);
            }
            UpdateTemplates();

        }

        private void UpdateSiteFolder(SiteDefinition siteDefinition, TemplatesRootFolder siteFolder)
        {
            ContentReference siteFolderRef;
            if (siteFolder == null)
            {
                var folder = _contentRepository.GetDefault<TemplatesRootFolder>(TemplatesRoot);
                folder.Name = siteDefinition.Name;

                siteFolderRef = _contentRepository.Save(folder, SaveAction.Publish, AccessLevel.NoAccess);
            }
            else
            {
                siteFolderRef = siteFolder.ContentLink;
            }
            UpdateTemplateSubfolders(siteFolderRef);
        }

        private void UpdateTemplateSubfolders(ContentReference parentLink)
        {
            if (!_contentRepository.GetChildren<BlockTemplatesFolder>(parentLink).Any())
            {
                var blockTemplateFolder = _contentRepository.GetDefault<BlockTemplatesFolder>(parentLink);
                blockTemplateFolder.Name = "Block Templates";

                _contentRepository.Save(blockTemplateFolder, SaveAction.Publish, AccessLevel.NoAccess);
            }

            if (!_contentRepository.GetChildren<PageTemplatesFolder>(parentLink).Any())
            {
                var pagesTemplateFolder = _contentRepository.GetDefault<PageTemplatesFolder>(parentLink);
                pagesTemplateFolder.Name = "Page Templates";
                _contentRepository.Save(pagesTemplateFolder, SaveAction.Publish, AccessLevel.NoAccess);
            }

        }

        private void SiteCreated(object sender, SiteDefinitionEventArgs e)
        {
            UpdateSiteFolder(e.Site,
                _contentRepository.GetChildren<TemplatesRootFolder>(TemplatesRoot).FirstOrDefault(x => x.Name.Equals(e.Site.Name, StringComparison.InvariantCultureIgnoreCase)));
        }

        private void SiteDeleted(object sender, SiteDefinitionEventArgs e)
        {
            var siteFolder = _contentRepository.GetChildren<TemplatesRootFolder>(TemplatesRoot)
                .FirstOrDefault(x => x.Name.Equals(e.Site.Name, StringComparison.InvariantCultureIgnoreCase));

            if (siteFolder == null)
            {
                return;
            }

            _contentRepository.Delete(siteFolder.ContentLink, true, AccessLevel.NoAccess);
        }

        private void SiteUpdated(object sender, SiteDefinitionEventArgs e)
        {
            var updatedArgs = e as SiteDefinitionUpdatedEventArgs;
            var prevSite = updatedArgs.PreviousSite;
            var updatedSite = updatedArgs.Site;
            var templatesRoot = TemplatesRoot;

            if (_contentRepository.GetChildren<IContent>(templatesRoot)
                ?.FirstOrDefault(x => x.Name.Equals(prevSite.Name, StringComparison.InvariantCultureIgnoreCase))
                is ContentFolder currentTemplateFolder)
            {
                var cloneFolder = currentTemplateFolder.CreateWritableClone();
                cloneFolder.Name = updatedSite.Name;
                _contentRepository.Save(cloneFolder);
                return;
            }

            UpdateSiteFolder(e.Site, null);
        }

        // Save a content as a new template and return the template instance id
        public int SaveAsTemplate(int sourceContentId, int sourceTypeId, int folderId, out string message)
        {
            message = string.Empty;
            int id = 0;

            var sourceContent = _contentRepository.Get<IContent>(new ContentReference(sourceContentId));
            if (sourceContent == null)
            {
                message = "ContentNotFound";
                return id;
            }

            if (sourceContent is not ITemplateContent templateSourceContent)
            {
                message = "ContentDoesNotImplementITemplateContent";
                return id;
            }

            // check if the content type from source exist
            var sourceContentType = _contentTypeRepository.Load(sourceTypeId);
            if (sourceContentType == null)
            {
                message = "ContentTypeNotExist";
                return id;
            }

            //Start copy data to a new template
            try
            {
                var saveFolder = folderId > 0
                    ? new ContentReference(folderId, true)
                    : GetTemplatesGlobalFolder();

                if (ContentReference.IsNullOrEmpty(saveFolder))
                {
                    message = "Save Folder Not Found";
                    return id;
                }

                // create a new template
                var newTemplate = _contentRepository.GetDefault<IContent>(saveFolder, sourceTypeId);
                newTemplate.Name = sourceContent.Name;
                var newTemplateRef = _contentRepository.Save(newTemplate, SaveAction.SkipValidation, AccessLevel.NoAccess);

                // populate the data from source content to the existing new template
                var newContentClone = _contentRepository.Get<ContentData>(newTemplateRef).CreateWritableClone();
                templateSourceContent.PopulateContentTo(newContentClone as IContent, 1, _maximumDepth, _contentRepository, _contentAssetHelper);

                _contentRepository.Save(newContentClone as IContent, SaveAction.Publish, AccessLevel.NoAccess);
                id = saveFolder.ID;
            }
            catch
            {
                message = "SaveNewTemplateFail";
                throw;
            }

            return id;
        }


        private ContentReference GetTemplatesRoot()
        {
            var root = _contentRepository
                ?.GetItems(_contentRootService.List(), new LoaderOptions())
                ?.FirstOrDefault(x => x.ContentGuid == TemplatesRootFolder.TemplatesRootGuid)
                ?.ContentLink;

            return root ?? ContentReference.EmptyReference;
        }

        private ContentReference GetTemplatesGlobalFolder()
        {
            var root = TemplatesRoot ?? GetTemplatesRoot();

            var globalFolder = _contentRepository
                .GetChildren<TemplatesRootFolder>(root)
                ?.FirstOrDefault(folder => folder.Name.Equals(TemplatesRootFolder.TemplatesGlobalName));

            return globalFolder?.ContentLink ?? ContentReference.EmptyReference;
        }
    }
}
