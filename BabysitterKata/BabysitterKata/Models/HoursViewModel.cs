﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BabysitterKata.Models
{
    public class HoursViewModel
    {
        public Time StartHour { get; set; }
        public List<Time> StartOptions { get; set; }
        public Time BedTimeHour { get; set; }
        public List<Time> BedTimeOptions { get; set; }
        public Time EndHour { get; set; }
        public List<Time> EndOptions { get; set; }

    }
}