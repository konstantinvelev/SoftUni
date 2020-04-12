namespace FitMe.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FitMe.Data.Models;
    using FitMe.Services.Data;
    using FitMe.Services.Mapping;
    using FitMe.Web.ViewModels.Comments;
    using FitMe.Web.ViewModels.Diets;
    using FitMe.Web.ViewModels.Exercise;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class DietsController : BaseController
    {
        private const int ItemsPerPage = 5;

        private readonly IDietsService dietsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUsersService usersService;
        private readonly ICommentsService commentsService;

        public DietsController(
            IDietsService dietsService,
            UserManager<ApplicationUser> userManager,
            IUsersService usersService,
            ICommentsService commentsService)
        {
            this.dietsService = dietsService;
            this.userManager = userManager;
            this.usersService = usersService;
            this.commentsService = commentsService;
        }

        public async Task<IActionResult> All(int? page = 1)
        {
            var all = this.dietsService.GetAll(ItemsPerPage, (int)((page - 1) * ItemsPerPage));
            var list = new List<PostDietViewModel>();
            foreach (var item in all)
            {
                var user = this.usersService.GetUserById(item.UserId);
                var dietsViewModel = new PostDietViewModel
                {
                    Id = item.Id,
                    Title = item.Title,
                    Description = item.Description,
                    UserUserName = user.UserName,
                    CommentsCount = item.Comments.Count,
                    CreatedOn = item.CreatedOn,
                };
                list.Add(dietsViewModel);
            }

            var viewModel = new AllDietsViewModel
            {
                Diets = list,
                CurrentPage = (int)page,
            };

            var count = this.dietsService.GetCount();
            viewModel.PagesCount = (int)Math.Ceiling((double)count / ItemsPerPage);
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
        public async Task<IActionResult> Create(CreateDietInputModel input)
        {
            var diet = AutoMapperConfig.MapperInstance.Map<Diet>(input);
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            await this.dietsService.CreateDietAsync(input, user.Id);
            return this.Redirect("/Diets/All");

        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Details(string dietId)
        {

            var user = await this.userManager.GetUserAsync(this.User);
            var diet = await this.dietsService.GetDietByIdAsync(dietId);
            var comment = this.commentsService.All<CommentViewModel>().Where(s => s.PostId == diet.Id);


            var viewModel = new DietsDetailViewModel()
            {
                Id = diet.Id,
                Title = diet.Title,
                Description = diet.Description,
                CreatedOn = diet.CreatedOn.ToString(),
                UserUserName = user.UserName,
                VotesCount = diet.Votes.Count,
                Comments = comment,
                UserUserId = user.Id,
            };

            if (viewModel == null)
            {
                return this.NotFound();
            }

            return this.View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(string dietId)
        {
            var diet = await this.dietsService.GetDietByIdAsync(dietId);
            var user = await this.userManager.GetUserAsync(this.User);
            if (diet.UserId == user.Id)
            {
                await this.commentsService.DeleteComments(diet.Comments);
                await this.dietsService.DeleteDietAsync(dietId);
                return this.Redirect("/");
            }

            return this.View("/Diets/Details");
        }

        [Authorize]
        public async Task<IActionResult> YourDiets()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var diets = this.dietsService.GetDietsByUser(user.Id);

            if (diets.Count() <= 0)
            {
                return this.Redirect("/Home/NullObjects");
            }

            var viewModel = new YourDietsViewModel
            {
                Diets = diets,
            };

            return this.View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(string dietId)
        {
            var diet = await this.dietsService.GetDietByIdAsync(dietId);
            var viewModel = new EditDietViewModel
            {
                Title = diet.Title,
                Description = diet.Description,
            };
            viewModel.DietId = dietId;
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(string dietId, EditDietInputModel input)
        {
            input.DietId = dietId;
            await this.dietsService.Update(input);
            return this.Redirect("/Diets/YourDiets");
        }
    }
}
