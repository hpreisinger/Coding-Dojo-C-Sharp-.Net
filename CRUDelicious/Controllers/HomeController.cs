using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CRUDelicious.Controllers
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
            List<Dish> AllDishes = dbContext.Dishes.ToList();
            ViewBag.AllDishes = AllDishes;
            return View();
        }
        
        [HttpGet("/new")]
        public IActionResult AddForm()
        {
            return View();
        }
        
        [HttpGet("/{id}")]
        public IActionResult Details(int id)
        {
            Dish FocusDish = dbContext.Dishes.FirstOrDefault(dish => dish.DishId == id);
            return View("Details", FocusDish);
        }

        [HttpGet("/edit/{id}")]
        public IActionResult EditForm(int id)
        {
            Dish FocusDish = dbContext.Dishes.FirstOrDefault(dish => dish.DishId == id);
            return View("EditForm", FocusDish);
        }

        [HttpPost("submit/add")]
        public IActionResult SubmitAdd(Dish newDish)
        {
            if (ModelState.IsValid)
            {
                dbContext.Add(newDish);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("AddForm");
            }
        }

        [HttpPost("submit/edit/{EditedID}")]
        public IActionResult SubmitEdit(int EditedID, Dish Edits)
        {
            Dish ToEdit = dbContext.Dishes.FirstOrDefault(dish => dish.DishId == EditedID);
            if (ModelState.IsValid)
            {
                ToEdit.Name = Edits.Name;
                ToEdit.Chef = Edits.Chef;
                ToEdit.Tastiness = Edits.Tastiness;
                ToEdit.Calories = Edits.Calories;
                ToEdit.Description = Edits.Description;
                ToEdit.UpdatedAt = DateTime.Now;
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("/edit/{id}");
            }
        }

        [HttpGet("submit/remove/{id}")]
        public IActionResult SubmitRemove(int id)
        {
            Dish FocusDish = dbContext.Dishes.FirstOrDefault(dish => dish.DishId == id);
            dbContext.Dishes.Remove(FocusDish);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
