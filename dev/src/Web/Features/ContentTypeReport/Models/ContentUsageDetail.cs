using Perficient.Web.Middleware.Datalayer;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Features.ContentTypeReport.Models
{
    public class ContentUsageDetail : IEntityData
    {
        [Key]
        public int Id { get; set; }
        [Required]        
        public int ContentID { get; set; }
        public string UseWhen { get; set; }
        public string DonotUseWhen { get; set; }       
    }
}
