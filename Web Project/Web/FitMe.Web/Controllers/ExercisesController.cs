namespace FitMe.Web.Controllers
{
    using System.Linq;

    using FitMe.Services.Data;
    using FitMe.Web.ViewModels.Categories;
    using Microsoft.AspNetCore.Mvc;

    public class ExercisesController : BaseController
    {
        private readonly IExercisesService exercisesService;

        public ExercisesController(IExercisesService exercisesService)
        {
            this.exercisesService = exercisesService;
        }


        public IActionResult AllForMans()
        {
            var all = this.exercisesService.GetAll().Where(s => s.TypeOfGender.ToString() == "Man");
            var viewModel = new AllExercisesViewMode
            {
                Exercises = all,
            };

            return this.View(viewModel);
        }

        public IActionResult AllForWomans()
        {
            var all = this.exercisesService.GetAll().Where(s => s.TypeOfGender.ToString() == "Woman");
            var viewModel = new AllExercisesViewMode
            {
                Exercises = all,
            };

            return this.View(viewModel);
        }

        public IActionResult Create()
        {
            return this.View();
        }
    }
}
