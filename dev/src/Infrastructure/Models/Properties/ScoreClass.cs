using System;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Infrastructure.Models.Properties
{
    public class ScoreClass: IEquatable<ScoreClass>
    {
        public string Name { get; set; }

        [Display(Name = "CSS Class")]
        public string ScoreClassName { get; set; }

        public override bool Equals(object obj) => Equals(obj as ScoreClass);

        public bool Equals(ScoreClass other)
        {
            return other?.Name == Name;
        }

        public override int GetHashCode() => Name.GetHashCode();
    }
}
