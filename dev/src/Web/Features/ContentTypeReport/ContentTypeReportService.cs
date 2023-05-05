using EPiServer;
using EPiServer.Cms.Shell;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.Editor;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Mvc.Rendering;
using Perficient.Web.Features.ContentTypeReport.Models;
using Perficient.Web.Middleware.Datalayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Perficient.Web.Features.ContentTypeReport
{
    public class ContentTypeReportService : IContentTypeReportService
    {
        private Injected<IContentTypeRepository> _contentTypeRepository;
        private Injected<IContentModelUsage> _contentModelUsage;       
        private Injected<IContentRepository> _contentRepo;
        private Injected<IContentVersionRepository> _contentVersionRepository;
        private Injected<IContentLoader> _contentLoader;
        private ContentTypeDBContext _context;

        public ContentTypeReportService(ContentTypeDBContext context)
        {            
            _context = context;
        }

        #region Services
        public List<string> GetContentTypeOptions()
        {
            List<SelectListItem> _contentTypeItems = new List<SelectListItem>();
            var _contentTypeRepository = ServiceLocator.Current.GetInstance<IContentTypeRepository>();
            var AllContentTypeBase = _contentTypeRepository.List().Where(z=>z.Base.ToString() != null).Select(z => z.Base.ToString()).Distinct().ToList();

            return AllContentTypeBase;
        }

        public List<ContentBasicInformationModel> GetContentTypes(string ContentType)
        {
            var contentTypes = _contentTypeRepository.Service.List().Where(o => o.Name != "SysRoot" && o.Name != "SysRecycleBin" && o.Base.ToString() == ContentType)
                .Select(o => new ContentBasicInformationModel
                {
                    Id = o.ID,
                    Name = o.Name,
                    DisplayName = string.IsNullOrEmpty(o.DisplayName) ? "" : o.DisplayName,
                    Description = ValidateString(o.Description),
                    Group = ValidateString(o.GroupName),
                    Count = GetContentCount(o.ID, "en")
                })
                .OrderBy(o => o.Name)
                .ToList();
            return contentTypes;
        }

        public bool SaveUsageDetails(int ContentId, string UseWhen, string DoNotUseWhen)
        {
            try
            {
                var EFUsageDetails = new EFDataRepository<ContentUsageDetail>(_context);

                var usageDetails = EFUsageDetails.GetAll().Where(o => o.ContentID == ContentId).FirstOrDefault();
                if (usageDetails == null)
                {
                    EFUsageDetails.Add(new ContentUsageDetail
                    {
                        ContentID = ContentId,
                        UseWhen = UseWhen,
                        DonotUseWhen = DoNotUseWhen
                    });
                }
                else
                {
                    usageDetails.UseWhen = UseWhen;
                    usageDetails.DonotUseWhen = DoNotUseWhen;
                    EFUsageDetails.Update(usageDetails);                           
                }

                EFUsageDetails.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
           
        }

        public bool DeleteUsageDetails(int ContentId)
        {
            try
            {
                var EFUsageDetails = new EFDataRepository<ContentUsageDetail>(_context);
                var usageDetails = EFUsageDetails.GetAll().Where(o => o.ContentID == ContentId).FirstOrDefault();

                if (usageDetails != null)
                {
                    EFUsageDetails.Delete(usageDetails);
                    EFUsageDetails.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool SaveUsageImages(int ContentId, string ImageName)
        {
            try
            {
                var EFUsageImages = new EFDataRepository<ContentUsageImage>(_context);

                var usageImages = EFUsageImages.GetAll().Where(o => o.ContentID == ContentId & o.UsageImage == ImageName).FirstOrDefault();
                if (usageImages == null)
                {
                    EFUsageImages.Add(new ContentUsageImage
                    {
                        ContentID = ContentId,
                        UsageImage = ImageName
                    });
                    EFUsageImages.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool DeleteUsageImages(int ContentId, string ImageName)
        {
            try
            {
                var EFUsageImages = new EFDataRepository<ContentUsageImage>(_context);
                var usageImages = EFUsageImages.GetAll().Where(o => o.ContentID == ContentId & o.UsageImage == ImageName).FirstOrDefault();

                if (usageImages != null)
                {
                    EFUsageImages.Delete(usageImages);
                    EFUsageImages.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public ContentDetailsModel GetProperties(int ContentId)
        {
            var contentType = _contentTypeRepository.Service.Load(ContentId);
            var contentDetailsModel = new ContentDetailsModel();
            if (contentType != null)
            {
                var customAttributeInfo = contentType.ModelType.GetCustomAttributesData();
                var EFUsageDetails = new EFDataRepository<ContentUsageDetail>(_context);
                var EFUsageImages = new EFDataRepository<ContentUsageImage>(_context);
                contentDetailsModel = new ContentDetailsModel
                {
                    ContentID = contentType.ID,
                    GUID = contentType.GUID,
                    Name = ValidateString(contentType.Name),
                    DisplayName = ValidateString(contentType.DisplayName),
                    Description = ValidateString(contentType.Description),
                    GroupName = ValidateString(contentType.GroupName),
                    UsageDetails = EFUsageDetails.GetAll().Where(o => o.ContentID == ContentId).FirstOrDefault(),
                    UsageImages = EFUsageImages.GetAll().Where(o => o.ContentID == ContentId).ToList()
                };
                if (contentType.ModelType != null)
                {
                    string iconPath = ContentIconPath(customAttributeInfo);
                    contentDetailsModel.AvailableInEditMode = IsAvailableInEditMode(customAttributeInfo);
                    contentDetailsModel.IconPath = string.IsNullOrEmpty(ContentIconPath(customAttributeInfo)) ? string.Empty : "<img src='" + iconPath + "' />";
                    contentDetailsModel.IncludeContentTypes = ValidateString(GetTypeList(customAttributeInfo, "IncludeOn"));
                    contentDetailsModel.ExcludeContentTypes = ValidateString(GetTypeList(customAttributeInfo, "ExcludeOn"));
                }
                var propertiesDetails = contentType.ModelType.GetMembers(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => contentType.PropertyDefinitions.Any(pd => pd.Name.Equals(p.Name)))
                    .Select(p => new ContentTypePropertyDefinitionsModel
                    {
                        MemberInfo = p,
                        PropertyDefinition = contentType.PropertyDefinitions.First(pd => pd.Name.Equals(p.Name))
                    });

                var properties = propertiesDetails
                        .Select(o => new ContentTypePropertiesDetailsModel
                        {
                            DisplayName = o.PropertyDefinition.EditCaption,
                            Name = o.PropertyDefinition.Name,
                            Description = o.PropertyDefinition.HelpText == null ? String.Empty : o.PropertyDefinition.HelpText?.Replace("'", "''"),
                            Group = o.PropertyDefinition.Tab?.Name,
                            Type = o.PropertyDefinition.Type?.Name,
                            Order = o.PropertyDefinition.FieldOrder,
                            Attributes = GetattributeInfo(o.MemberInfo.CustomAttributes)
                        }).ToList();
                contentDetailsModel.Properties = properties;
            }
            return contentDetailsModel;
        }

        public List<InstancesSummaryModel> GetInstancesOfContent(int ContentId)
        {
            int instancesCount;
            List<InstancesSummaryModel> instancesReport = new List<InstancesSummaryModel>();
            var repository = ServiceLocator.Current.GetInstance<IPublishedStateAssessor>();
            // Find usages of the content type in question
            IList<ContentUsage> contentUsages = _contentModelUsage.Service.ListContentOfContentType(_contentTypeRepository.Service.Load(ContentId));
            // Get contents excluding versions
            IList<IContent> myContent = contentUsages.Select(contentUsage =>
                contentUsage.ContentLink.ToReferenceWithoutVersion())
                .Distinct()
                .Select(contentReference => _contentRepo.Service.Get<IContent>(contentReference))
                .ToList();

            foreach (IContent content in myContent)
            {
                var contentRef = content.ContentLink;
                if (repository.IsPublished(content, PagePublishedStatus.Published))
                {
                    instancesCount = _contentRepo.Service.GetReferencesToContent(content.ContentLink, false).Count();
                    instancesReport.Add(new InstancesSummaryModel
                    {
                        Id = contentRef.ID,
                        Name = content.Name,
                        PageUrl = ServiceLocator.Current.GetInstance<UrlResolver>().GetUrl(contentRef),
                        EditUrl = PageEditing.GetEditUrl(contentRef),
                        CreatedDate = _contentVersionRepository.Service.List(contentRef).OrderBy(v => v.Saved).Select(x => x.Saved).FirstOrDefault().ToString(),
                        UpdatedDate = _contentVersionRepository.Service.List(contentRef).OrderByDescending(v => v.Saved).Select(x => x.Saved).FirstOrDefault().ToString(),
                        Author = _contentVersionRepository.Service.List(contentRef).OrderBy(v => v.Saved).Select(x => x.SavedBy).FirstOrDefault().ToString(),
                        Count = instancesCount,
                        InstantReferences = GetReferencesOfInstance(contentRef, instancesCount)
                    });
                }
            }
            return instancesReport;
        }
        #endregion

        #region Private Methods        

        /// <summary>
        /// Get references of instance
        /// </summary>
        /// <param name="contentRef"></param>
        /// <param name="instCount"></param>
        /// <returns></returns>
        private List<ReferencesSummaryModel> GetReferencesOfInstance(ContentReference contentRef, int instCount)
        {
            List<ReferencesSummaryModel> referencesReport = new List<ReferencesSummaryModel>();
            if (instCount > 0)
            {               
                // get all references where this item is used
                var references = _contentRepo.Service.GetReferencesToContent(contentRef, false);
                foreach (var reference in references)
                {
                    var parentItem = _contentLoader.Service.Get<IContent>(reference.OwnerID);
                    // log the owner information in our result set
                    referencesReport.Add(new ReferencesSummaryModel
                    {
                        Id = parentItem.ContentLink.ID,
                        Name = parentItem.Name,
                        PageUrl = ServiceLocator.Current.GetInstance<UrlResolver>().GetUrl(parentItem.ContentLink),
                        EditUrl = PageEditing.GetEditUrl(parentItem.ContentLink),
                        CreatedDate = _contentVersionRepository.Service.List(parentItem.ContentLink).OrderBy(v => v.Saved).Select(x => x.Saved).FirstOrDefault().ToString(),
                        UpdatedDate = _contentVersionRepository.Service.List(parentItem.ContentLink).OrderByDescending(v => v.Saved).Select(x => x.Saved).FirstOrDefault().ToString(),
                        Author = _contentVersionRepository.Service.List(parentItem.ContentLink).OrderBy(v => v.Saved).Select(x => x.SavedBy).FirstOrDefault().ToString()
                    });
                }                
            }
            return referencesReport;
        }

        /// <summary>
        /// get all available content types
        /// </summary>
        /// <param name="customAttributes"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        private string GetTypeList(IList<CustomAttributeData> customAttributes, string memberName)
        {
            string list = string.Empty;
            var arguments = customAttributes
              .Where(c => c.AttributeType.Name == "AvailableContentTypesAttribute")
              .Select(x => x.NamedArguments.Where(y => y.MemberName == memberName))
              .FirstOrDefault()?.Select(c => c.TypedValue.Value as IEnumerable<CustomAttributeTypedArgument>).FirstOrDefault();

            if (arguments != null)
            {
                foreach (CustomAttributeTypedArgument argValue in arguments)
                {
                    TypeInfo info = argValue.Value as TypeInfo;
                    list = string.IsNullOrEmpty(list) ? info.Name : list + "," + info.Name;
                }
                return list;
            }
            return string.Empty;
        }

        /// <summary>
        /// fall back value of the property values
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string ValidateString(string data)
        {
            return string.IsNullOrEmpty(data) ? "N/A" : data;
        }

        /// <summary>
        /// get count of the content reference
        /// </summary>
        /// <param name="contentTypeId"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        private int GetContentCount(int contentTypeId, string language)
        {
            IList<ContentUsage> contentUsages = _contentModelUsage.Service.ListContentOfContentType(_contentTypeRepository.Service.Load(contentTypeId));
            int contentCount = _contentModelUsage.Service.ListContentOfContentType(_contentTypeRepository.Service.Load(contentTypeId))
                .Select(contentUsage =>
                contentUsage.ContentLink.ToReferenceWithoutVersion())
                .Distinct().Count();
            return contentCount;
        }

        /// <summary>
        /// get specific attribute information
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        private string GetattributeInfo(IEnumerable<CustomAttributeData> o)
        {
            string result = string.Empty;
            o.ToList().ForEach(x =>
            {
                result = result + (string.IsNullOrEmpty(result) ? x.AttributeType.Name : ", " + x.AttributeType.Name);
            });
            //result = result + string.Join(",", x.AttributeType.Name);
            return result.Replace("Attribute", "");
        }

        /// <summary>
        /// get if this block / page available in edit mode
        /// </summary>
        /// <param name="customAttributes"></param>
        /// <returns></returns>
        private string IsAvailableInEditMode(IList<CustomAttributeData> customAttributes)
        {
            string isAvailableInEditMode = customAttributes
            .Where(c => c.AttributeType.Name == "ContentTypeAttribute")
            .Select(x => x.NamedArguments.Where(y => y.MemberName == "AvailableInEditMode"))
            .FirstOrDefault()?.Select(c => c.TypedValue.Value.ToString()).FirstOrDefault();

            return string.IsNullOrEmpty(isAvailableInEditMode) ? "True" : isAvailableInEditMode;
        }

        /// <summary>
        /// get content icon path
        /// </summary>
        /// <param name="customAttributes"></param>
        /// <returns></returns>
        private string ContentIconPath(IList<CustomAttributeData> customAttributes)
        {
            string removeString = "~";
            string IconPath = customAttributes
            .Where(c => c.AttributeType.Name == "SiteImageUrl" || c.AttributeType.Name == "ImageUrlAttribute")
            .Select(x => x.ConstructorArguments)
            .FirstOrDefault()?.Select(c => c.Value.ToString()).FirstOrDefault();

            IconPath = string.IsNullOrEmpty(IconPath) ? string.Empty : IconPath;
            int index = IconPath.IndexOf(removeString);
            string cleanPath = (index < 0) ? IconPath : IconPath.Remove(index, removeString.Length);

            return cleanPath;
        }
        #endregion

    }
}
