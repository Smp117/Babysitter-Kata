using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BabysitterKata.Models
{
    public class HoursViewModel
    {
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