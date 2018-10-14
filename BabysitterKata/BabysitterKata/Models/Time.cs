using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabysitterKata.Models
{
    public class Time
    {
        public string Display { get; set; }
        public int Value { get; set; }

        public Time(string display, int value)
        {
            Display = display;
            Value = value;
        }

        public static Time Midnight { get { return new Time("12:00 AM", 7); } }

        public static List<Time> AvailableTimes()
        {
            return new List<Time>()
            {
                new Time("5:00 PM", 0),
                new Time("6:00 PM", 1),
                new Time("7:00 PM", 2),
                new Time("8:00 PM", 3),
                new Time("9:00 PM", 4),
                new Time("10:00 PM", 5),
                new Time("11:00 PM", 6),
                new Time("12:00 AM", 7),
                new Time("1:00 AM", 8),
                new Time("2:00 AM", 9),
                new Time("3:00 AM", 10),
                new Time("4:00 AM", 11)
            };
        }
    }
}