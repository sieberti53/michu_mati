using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace michu_mati.Models
{
    public class WeeklyMealPlan
    {
        public Dictionary<DayOfWeek, Recipe?> Meals { get; set; } = new()
    {
        { DayOfWeek.Monday, null },
        { DayOfWeek.Tuesday, null },
        { DayOfWeek.Wednesday, null },
        { DayOfWeek.Thursday, null },
        { DayOfWeek.Friday, null },
        { DayOfWeek.Saturday, null },
        { DayOfWeek.Sunday, null }
    };
    }
}
