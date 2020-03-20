namespace FitMe.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult MansExercises()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult WomansExercises()
        {
            return this.View();
        }
    }
}
