using System;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Infrastructure.Models.Properties
{
    public class ScoreColor: IEquatable<ScoreColor>
    {
        public string Name { get; set; }

        [Display(Name = "Color Code")]
        public string ColorCode { get; set; }

        public override bool Equals(object obj) => Equals(obj as ScoreColor);

        public bool Equals(ScoreColor other)
        {
            return other?.Name == Name;
        }

        public override int GetHashCode() => (Name).GetHashCode();       
    }
}
