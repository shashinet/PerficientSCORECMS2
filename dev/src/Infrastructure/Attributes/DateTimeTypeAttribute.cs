using System;
using System.ComponentModel.DataAnnotations;

namespace Perficient.Infrastructure.Attributes
{
    /// <summary>
    /// Used with [UIHint("UTC")] to limit the DateTime property to a 
    /// Date only selector  or a Time only selector. 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DateTimeTypeAttribute : Attribute
    {
        public DateTimeSelector Selector { get; set; }

        public DateTimeTypeAttribute(DateTimeSelector selector)
        {
            Selector = selector;
        }
    }

    public enum DateTimeSelector
    {
        [Display(Name = "Date Only", Description = "date")]
        DateOnly,

        [Display(Name = "Time Only", Description = "time")]
        TimeOnly
    }
}
