using System.ComponentModel.DataAnnotations;

namespace Horeca.MVC.Helpers.Attributes
{
    public class DateAttribute : RangeAttribute
    {
        public DateAttribute()
          : base(typeof(DateTime), DateTime.Today.ToShortDateString(), DateTime.MaxValue.ToShortDateString()) { }
    }
}