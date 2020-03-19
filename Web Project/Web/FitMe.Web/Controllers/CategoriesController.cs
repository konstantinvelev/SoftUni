using Microsoft.AspNetCore.Mvc;

namespace FitMe.Web.Controllers
{
    public class CategoriesController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult Exercises()
        {
            return this.View();
        }
    }
}
