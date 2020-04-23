namespace FitMe.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FitMe.Data.Models;
    using FitMe.Services.Data;
    using FitMe.Web.ViewModels.Comments;
    using FitMe.Web.ViewModels.Exercise;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ExercisesController : BaseController
    {
        private const int ItemsPerPage = 5;
        private const string NullContent = "Add some descriptions!";

        private readonly IExercisesService exercisesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ICommentsService commentsService;
        private readonly IUsersService usersService;

        public ExercisesController(
            IExercisesService exercisesService,
            UserManager<ApplicationUser> userManager,
            ICommentsService commentsService,
            IUsersService usersService)
        {
            this.exercisesService = exercisesService;
            this.userManager = userManager;
            this.commentsService = commentsService;
            this.usersService = usersService;
        }

        public async Task<IActionResult> AllForMans(int? page = 1)
        {
            var all = this.exercisesService.GetAllForMans<ExerciseViewModel>(ItemsPerPage, (int)((page - 1) * ItemsPerPage));

            var viewModel = new AllExercisesViewModel
            {
                Exercises = all,
                CurrentPage = (int)page,
            };

            var exercises = this.exercisesService.GetAll().Where(s => s.TypeOfGender == Gender.Man);
            viewModel.PagesCount = (int)Math.Ceiling((double)exercises.Count() / ItemsPerPage);
            return this.View(viewModel);
        }

        public async Task<IActionResult> AllForWomans(int? page = 1)
        {
            var all = this.exercisesService.GetAllForWomens<ExerciseViewModel>(ItemsPerPage, (int)((page - 1) * ItemsPerPage));

            var viewModel = new AllExercisesViewModel
            {
                Exercises = all,
                CurrentPage = (int)page,
            };

            var exercises = this.exercisesService.GetAll().Where(s => s.TypeOfGender == Gender.Woman);
            viewModel.PagesCount = (int)Math.Ceiling((double)exercises.Count() / ItemsPerPage);
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
            var url = input.Video.Split("watch?v=");
            input.Video = url[1];

            var user = await this.userManager.GetUserAsync(this.User);
            if (input.Gender.ToString() == "Man")
            {
                await this.exercisesService.CreateMansExercisesAsync(input, user.Id);
                return this.Redirect("/Exercises/AllForMans");
            }
            else
            {
                await this.exercisesService.CreateWomansExercisesAsync(input, user.Id);
                return this.Redirect("/Exercises/AllForWomans");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Details(string exerciseId)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var exercise = await this.exercisesService.GetExercisesByIdAsync(exerciseId);

            var comments = this.commentsService.All<CommentViewModel>().Where(s => s.PostId == exercise.Id);

            var viewModel = new ExerciseDetailViewModel
            {
                Id = exercise.Id,
                Title = exercise.Title,
                Content = exercise.Content,
                Gender = exercise.TypeOfGender.ToString(),
                CreatedOn = exercise.CreatedOn.ToString(),
                UserUserName = user.UserName,
                UserUserId = user.Id,
                Video = exercise.Video,
                VotesCount = exercise.Votes.Count(),
                Comments = comments.OrderBy(s => s.CreatedOn),
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
            var exercise = await this.exercisesService.GetExercisesByIdAsync(exerciseId);
            var user = await this.userManager.GetUserAsync(this.User);

            if (exercise.UserID == user.Id)
            {
                await this.commentsService.DeleteComments(exercise.Commetns);
                await this.exercisesService.DeleteExercisesAsync(exerciseId);

                return this.Redirect("/Exercises/YourExercises");
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
        public async Task<IActionResult> Edit(string exerciseId)
        {
            var exercise = await this.exercisesService.GetExercisesByIdAsync(exerciseId);
            if (exercise.Content == null)
            {
                exercise.Content = NullContent;
            }

            var viewModel = new EditExerciseViewModel
            {
                Title = exercise.Title,
                Content = exercise.Content,
                Gender = exercise.TypeOfGender.ToString(),
                Video = exercise.Video,
            };

            viewModel.ExerciseId = exerciseId;
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(string exerciseId, EditExercisetInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.View(input);
            }

            var url = input.Video.Split("watch?v=");
            if (url.Length > 1)
            {
                input.Video = url[1];
                input.ExerciseId = exerciseId;
            }
            else
            {
                input.Video = url[0];
            }

            await this.exercisesService.Update(input);
            return this.Redirect("/Exercises/YourExercises");
        }
    }
}
