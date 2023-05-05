using System;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Infrastructure.Models.Properties
{
    public class ScoreButtonStyle : IEquatable<ScoreButtonStyle>
    {
        public string Name { get; set; }

        [Display(Name = "Button Style")]
        public string ButtonStyleClass { get; set; }

        public override bool Equals(object obj) => Equals(obj as ScoreButtonStyle);

        public bool Equals(ScoreButtonStyle other)
        {
            return other?.Name == Name;
        }

        public override int GetHashCode() => Name.GetHashCode();
    }
}
