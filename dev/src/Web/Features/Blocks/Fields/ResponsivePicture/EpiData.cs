using System;

namespace Perficient.Web.Features.Blocks.Fields.ResponsivePicture
{
    public class EpiData
    {
        public bool isPreferredLanguageAvailable { get; set; }
        public bool hasTranslationAccess { get; set; }
        public bool isSubRoot { get; set; }
        public bool isStartPage { get; set; }
        public bool isWastebasket { get; set; }
        public bool hasChildren { get; set; }
        public string thumbnailUrl { get; set; }
        public string publicUrl { get; set; }
        public string downloadUrl { get; set; }
        public string previewUrl { get; set; }
        public string permanentLink { get; set; }
        public bool hasTemplate { get; set; }
        public int accessMask { get; set; }
        public int accessRights { get; set; }
        public string name { get; set; }
        public int contentLink { get; set; }
        public int parentLink { get; set; }
        public string uri { get; set; }
        public string contentGuid { get; set; }
        public int contentTypeID { get; set; }
        public string contentTypeName { get; set; }
        public DateTime created { get; set; }
        public string createdBy { get; set; }
        public DateTime changed { get; set; }
        public string changedBy { get; set; }
        public DateTime saved { get; set; }
        public bool isDeleted { get; set; }
        public int providerCapabilityMask { get; set; }
        public int status { get; set; }
        public string typeIdentifier { get; set; }
    }
}
