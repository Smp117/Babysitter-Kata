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
            HoursViewModel viewModel = new HoursViewModel(0, 0, 0);
            return View(viewModel);
        }

        [HttpPost]
        public string UpdateDropDowns(int start, int bed, int end)
        {
            return JsonConvert.SerializeObject(new HoursViewModel(start, bed, end));
        }
    }
}