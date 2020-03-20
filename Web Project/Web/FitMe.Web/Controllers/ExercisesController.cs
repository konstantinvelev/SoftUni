using Microsoft.AspNetCore.Mvc;

namespace FitMe.Web.Controllers
{
    public class ExercisesController : BaseController
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

        [HttpGet]
        public IActionResult Diets()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult AddExercises()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult AddDiets()
        {
            return this.View();
        }
    }
}
