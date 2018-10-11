using BabysitterKata.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BabysitterKata.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UpdateDropDowns(int start, int bed, int end)
        {
            var times = Time.AvailableTimes();
            HoursViewModel viewModel = new HoursViewModel();
            // Start time can be any available time
            viewModel.StartHour = times.First(x => x.Value == start);
            // Bed can't be any earlier than the start time
            viewModel.BedTimeHour = bed > start ? times.First(x => x.Value == bed) : times.First(x => x.Value == start);
            // End time can't be before bed time and has to be after start time
            // If the kid won't be in bed before the job is finished, bedtime and end time will be the same
            viewModel.EndHour = end > bed ? times.First(x => x.Value == end) : bed > start ? times.First(x => x.Value == bed) : times.First(x => x.Value == bed + 1);

            // Filter dropdowns to only display appropriate hours
            viewModel.StartOptions = times;
            viewModel.BedTimeOptions = times.Where(x => x.Value >= viewModel.BedTimeHour.Value).ToList();
            viewModel.BedTimeOptions = times.Where(x => x.Value >= viewModel.EndHour.Value).ToList();
            
            return PartialView(viewModel);
        }
    }
}