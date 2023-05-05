using System.Collections.Generic;

namespace Perficient.Infrastructure.Models.Properties
{
    public class ScoreEditMenuModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string EditUrl { get; set; }
        public List<ScoreEditMenuModel> Children { get; set; }
    }
}
