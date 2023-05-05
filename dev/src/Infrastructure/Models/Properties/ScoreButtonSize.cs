using System;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Infrastructure.Models.Properties
{
    public class ScoreButtonSize:IEquatable<ScoreButtonSize>
    {
        public string Name { get; set; }

        [Display(Name = "Button Size")]
        public string ButtonSizeClass { get; set; }

        public override bool Equals(object obj) => Equals(obj as ScoreButtonSize);

        public bool Equals(ScoreButtonSize other)
        {
            return other?.Name == Name;
        }

        public override int GetHashCode() => Name.GetHashCode();
    }
}
