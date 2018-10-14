using BabysitterKata.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;

namespace BabysitterKata.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HoursViewModel viewModel = CreateViewModel(0, 0, 0);
            return View(viewModel);
        }

        [HttpPost]
        public string UpdateDropDowns(int start, int bed, int end)
        {
            return JsonConvert.SerializeObject(CreateViewModel(start, bed, end));
        }

        private HoursViewModel CreateViewModel(int start, int bed, int end)
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
            viewModel.StartOptions = Time.AvailableTimes();
            viewModel.StartOptions.RemoveAt(viewModel.StartOptions.Count() - 1);
            viewModel.BedTimeOptions = times.Where(x => x.Value >= viewModel.StartHour.Value).ToList();
            if (start >= end || start >= bed)
                viewModel.EndOptions = times.Where(x => x.Value >= viewModel.BedTimeHour.Value + 1).ToList();
            else
                viewModel.EndOptions = times.Where(x => x.Value >= viewModel.BedTimeHour.Value).ToList();
            return viewModel;
        }
    }
}