using System.Collections.Generic;

namespace Perficient.Web.Features.Navigation.Models
{
    public class SectionMenuSpiderData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public List<SectionMenuSpiderData> Children { get; set; }
    }
}
