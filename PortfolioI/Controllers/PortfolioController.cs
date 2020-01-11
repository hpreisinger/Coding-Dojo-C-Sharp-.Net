using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
namespace PortfolioI.Controllers
{
    public class PortfolioController : Controller
    {
        [HttpGet("")]
        public ViewResult Index()
        {
            return View("Index");
        }

        [HttpGet("projects")]
        public ViewResult Projects()
        {
            return View("Projects");
        }

        [HttpGet("contact")]
        public ViewResult Contact()
        {
            return View("Contact");
        }

        [HttpGet("submit")]
        public RedirectToActionResult Submit()
        {
            return RedirectToAction("Index");
        }
    }
}