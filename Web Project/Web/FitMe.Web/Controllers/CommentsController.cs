namespace FitMe.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using FitMe.Data.Models;
    using FitMe.Services.Data;
    using FitMe.Web.ViewModels.Comments;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class CommentsController : Controller
    {
        private readonly ICommentsService commentsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDietsService dietsService;
        private readonly IExercisesService exercisesService;

        public CommentsController(
            ICommentsService commentsService,
            UserManager<ApplicationUser> userManager,
            IDietsService dietsService,
            IExercisesService exercisesService)
        {
            this.commentsService = commentsService;
            this.userManager = userManager;
            this.dietsService = dietsService;
            this.exercisesService = exercisesService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create(string postId)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var model = new CreateCommentInputModel
            {
                UserUserName = user.UserName,
                PostId = postId,
            };
            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(string postId, CreateCommentInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            input.UserUserName = user.UserName;

            var comment = await this.commentsService.CreateComment(input);
            var diet = await this.dietsService.GetDietByIdAsync(postId);
            var exercise = await this.exercisesService.GetExercisesByIdAsync(postId);

            if (diet == null && exercise == null)
            {
                return this.View(input);
            }
            else if (exercise == null)
            {
                await this.dietsService.AddCommentToDiet(diet, comment);
                return this.Redirect("/Diets/Details?dietId=" + diet.Id);
            }
            else
            {
                await this.exercisesService.AddCommenToExercise(exercise, comment);
                return this.Redirect("/Exercises/Details?exerciseId=" + exercise.Id);
            }
        }
    }
}
