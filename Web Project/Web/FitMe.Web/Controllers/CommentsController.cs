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
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost ("/Comments/Create")]
        [Authorize]
        public async Task<IActionResult> Create(string postId, CreateCommentInputModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            input.UserUserName = user.UserName;
            var diet = await this.dietsService.GetDietByIdAsync(postId);
            var exercises = await this.exercisesService.GetExercisesByIdAsync(postId);
            if (diet == null && exercises == null)
            {
                return this.View(input);
            }
            else if (exercises == null)
            {
                return this.RedirectToAction("/Diets/Details?dietId=" + diet.Id);
            }
            else
            {
                return this.RedirectToAction("/Exercises/Details?exerciseId=" + exercises.Id);
            }
        }
    }
}
