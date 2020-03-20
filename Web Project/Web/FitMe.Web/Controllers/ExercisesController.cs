namespace FitMe.Web.Controllers
{
    using FitMe.Services.Data;
    using FitMe.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;

    public class ExercisesController : BaseController
    {
        private readonly IExercisesService exercisesService;

        public ExercisesController(IExercisesService exercisesService)
        {
            this.exercisesService = exercisesService;
        }

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

        public IActionResult All()
        {
            var all = this.exercisesService.GetAll().Select(x => x.Title);
            var viewModel = new AllViewMode
            {
                Title = all,
            };

            return this.View(viewModel);
        }
    }
}
