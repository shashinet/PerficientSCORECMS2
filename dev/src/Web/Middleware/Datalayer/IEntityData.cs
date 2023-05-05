using System.ComponentModel.DataAnnotations;

namespace Perficient.Web.Middleware.Datalayer
{
    public interface IEntityData
    {
        [Key]
        int Id { get; set; }
    }
}
