using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RandomPasscode.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace RandomPasscode.Controllers
{
    public static class SessionExtensions
    {

        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            string value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }

    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            int? counter = HttpContext.Session.GetInt32("Counter");
            if (counter == null)
            {
                HttpContext.Session.SetInt32("Counter", 1);
                counter = HttpContext.Session.GetInt32("Counter");
            }

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var tempCode = new char[14];
            var random = new Random();

            for (int i=0; i<14; i++){
                tempCode[i] = chars[random.Next(chars.Length)];
            }

            var finalCode = new String(tempCode);
            counter ++;
            HttpContext.Session.SetInt32("Counter", (int)counter);

            ViewBag.Counter = HttpContext.Session.GetInt32("Counter");
            ViewBag.Passcode = finalCode;
            
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
