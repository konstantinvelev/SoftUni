namespace FitMe.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using FitMe.Data.Models;
    using FitMe.Services.Data;
    using FitMe.Web.ViewModels.Categories;
    using FitMe.Web.ViewModels.Exercise;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ExercisesController : BaseController
    {
        private readonly IExercisesService exercisesService;
        private readonly UserManager<ApplicationUser> userManager;

        public ExercisesController(
            IExercisesService exercisesService,
            UserManager<ApplicationUser> userManager)
        {
            this.exercisesService = exercisesService;
            this.userManager = userManager;
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

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateExercisesInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            if (input.Gender.ToString() == "Male")
            {
                await this.exercisesService.CreateMansExercisesAsync(input, user.Id);
                return this.Redirect("/Exercises/AllForMans");
            }
            else
            {
                await this.exercisesService.CreateWomansExercisesAsync(input, user.Id);
                return this.Redirect("/Exercises/AllForMans");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Details(string exerciseId)
        {
            var exercise = await this.exercisesService.GetDietByIdAsync(exerciseId);
            var viewModel = new ExerciseDetailViewModel
            {
                Title = exercise.Title,
                Content = exercise.Content,
                Gender = exercise.TypeOfGender.ToString(),
            };

            return this.View(viewModel);
        }
    }
}
