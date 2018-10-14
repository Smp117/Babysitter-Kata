using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BabysitterKata.Models
{
    public class HoursViewModel
    {
        public HoursViewModel(int start, int bed, int end)
        {
            var times = Time.AvailableTimes();
            // Start time can be any available time
            StartHour = times.First(x => x.Value == start);
            // Bed can't be any earlier than the start time
            BedTimeHour = bed > start ? times.First(x => x.Value == bed) : times.First(x => x.Value == start);
            // End time can't be before bed time and has to be after start time
            // If the kid won't be in bed before the job is finished, bedtime and end time will be the same
            if (end > BedTimeHour.Value)
                EndHour = times.First(x => x.Value == end);
            else if (BedTimeHour.Value > StartHour.Value)
                EndHour = times.First(x => x.Value == BedTimeHour.Value);
            else
                EndHour = times.First(x => x.Value == BedTimeHour.Value + 1);

            //EndHour = end > bed ? times.First(x => x.Value == end) : bed > start ? times.First(x => x.Value == bed) : times.First(x => x.Value == bed + 1);

            // Filter dropdowns to only display appropriate hours
            StartOptions = Time.AvailableTimes();
            StartOptions.RemoveAt(StartOptions.Count() - 1);
            BedTimeOptions = times.Where(x => x.Value >= StartHour.Value).ToList();
            if (start >= end || start >= bed)
                EndOptions = times.Where(x => x.Value >= BedTimeHour.Value + 1).ToList();
            else
                EndOptions = times.Where(x => x.Value >= BedTimeHour.Value).ToList();
        }

        private static int PreBedRate { get { return 12; } }
        private static int PostBedRate { get { return 8; } }
        private static int PostMidnightRate { get { return 16; } }

        public Time StartHour { get; set; }
        public List<Time> StartOptions { get; set; }
        public Time BedTimeHour { get; set; }
        public List<Time> BedTimeOptions { get; set; }
        public Time EndHour { get; set; }
        public List<Time> EndOptions { get; set; }
        [Display(Name = "Nightly Charge")]
        public int NightlyCharge { get { return CalculateNightlyCharge(); }}

        private int CalculateNightlyCharge()
        {
            // If Start time is after midnight, there is only 1 rate to care about
            if (StartHour.Value >= Time.Midnight.Value)
                return (EndHour.Value - StartHour.Value) * PostMidnightRate;
            
            int start = 0;
            int postBed = 0;
            int postMidnight = 0;

            if (BedTimeHour.Value < Time.Midnight.Value)
                start = (BedTimeHour.Value - StartHour.Value) * PreBedRate;
            else
                start = (Time.Midnight.Value - StartHour.Value) * PreBedRate;

            if (EndHour.Value < Time.Midnight.Value)
                postBed = (EndHour.Value - BedTimeHour.Value) * PostBedRate;
            else
            {
                if(BedTimeHour.Value < Time.Midnight.Value)
                    postBed = (Time.Midnight.Value - BedTimeHour.Value) * PostBedRate;
                postMidnight = (EndHour.Value - Time.Midnight.Value) * PostMidnightRate;
            }

            return start + postBed + postMidnight;
        }
    }
}