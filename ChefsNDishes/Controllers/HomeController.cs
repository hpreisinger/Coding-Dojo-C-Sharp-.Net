using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChefsNDishes.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ChefsNDishes.Controllers
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
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("/")]
        public IActionResult Index()
        {
            List<Chef> AllChefs = dbContext.Chefs
                .Include(chef => chef.CreatedDishes)
                .ToList();
            return View(AllChefs);
        }

        [HttpGet("/dishes")]
        public IActionResult Dishes()
        {
            List<Dish> AllDishes = dbContext.Dishes
                .Include(dish => dish.Creator)
                .ToList();
            return View(AllDishes);
        }

        [HttpGet("/new")]
        public IActionResult AddChef()
        {
            return View();
        }

        [HttpGet("/dishes/new")]
        public IActionResult AddDish()
        {
            List<Chef> AllChefs = dbContext.Chefs
                .Include(chef => chef.CreatedDishes)
                .ToList();
            ViewBag.AllChefs = AllChefs;
            return View();
        }

        [HttpPost("/submit/dish")]
        public IActionResult SubmitDish(Dish newDish)
        {
            if (ModelState.IsValid)
            {
                dbContext.Add(newDish);
                dbContext.SaveChanges();
                return RedirectToAction("Dishes");
            }
            else
            {
                return View("AddDish");
            }
        }

        [HttpPost("/submit/chef")]
        public IActionResult SubmitChef(Chef newChef)
        {
            if (ModelState.IsValid)
            {
                dbContext.Add(newChef);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("AddChef");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
