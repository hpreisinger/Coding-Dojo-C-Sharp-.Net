using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
namespace TimeDisplay.Controllers
{
    public class TimeController : Controller
    {
        [HttpGet("")]
        public ViewResult Index()
        {
			DateTime CurrentTime = DateTime.Now;
            ViewBag.Time = CurrentTime;
            return View("Index");
        }

    }
}