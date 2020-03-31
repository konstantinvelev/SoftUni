﻿namespace FitMe.Web.Controllers
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
        [Authorize]
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
            var user = await this.userManager.GetUserAsync(this.User);
            var exercise = await this.exercisesService.GetDietByIdAsync(exerciseId);

            var viewModel = new ExerciseDetailViewModel
            {
                Id = exercise.Id,
                Title = exercise.Title,
                Content = exercise.Content,
                Gender = exercise.TypeOfGender.ToString(),
                CreatedOn = exercise.CreatedOn.ToString(),
                UserUserName = user.UserName,
                Video = exercise.Video,
                VotesCount = exercise.Votes.Count(),
            };

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(string exerciseId)
        {
            var exercise = await this.exercisesService.GetDietByIdAsync(exerciseId);
            var user = await this.userManager.GetUserAsync(this.User);
            if (exercise.UserID == user.Id)
            {
                await this.exercisesService.DeleteDietAsync(exerciseId);
                return this.Redirect("/");
            }

            return this.View("/Exercises/Details");
        }

        [Authorize]
        public async Task<IActionResult> YourExercises()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var exercises = this.exercisesService.GetExersisesByUser(user.Id);

            if (exercises.Count() <= 0)
            {
                return this.Redirect("/Home/NullObjects");
            }

            var viewModel = new YourExercisesViewModel
            {
                Exercises = exercises,
            };

            return this.View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(string dietId)
        {
            var exercises = await this.exercisesService.GetDietByIdAsync(dietId);
            //var viewModel = new EditDietViewModel
            //{
            //    Title = exercises.Title,
            //    Content = exercises.Content,
            //    Gender = exercises.TypeOfGender.ToString(),
            //};
            return this.View(/*viewModel*/);
        }

        //[HttpPost]
        //[Authorize]
        //public async Task<IActionResult> Update(EditDietInputModel input, string dietId)
        //{
        //    await this.exercisesService.Update(dietId, input);
        //    return this.Redirect("/Diets/YourDiets");
        //}
    }
}
