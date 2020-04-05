namespace FitMe.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using FitMe.Data.Models;
    using FitMe.Services.Data;
    using FitMe.Services.Mapping;
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

        public DietsController(
            IDietsService dietsService,
            UserManager<ApplicationUser> userManager)
        {
            this.dietsService = dietsService;
            this.userManager = userManager;
        }

        public IActionResult All(int? page)
        {
            if (page == 0 || !page.HasValue)
            {
                page = 1;
            }

            var all = this.dietsService.GetAll(ItemsPerPage, (int)((page - 1) * ItemsPerPage));
            var viewModel = new AllDietsViewModel
            {
                Diets = all,
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

            var viewModel = new DietsDetailViewModel()
            {
                Id = diet.Id,
                Title = diet.Title,
                Description = diet.Description,
                CreatedOn = diet.CreatedOn.ToString(),
                UserUserName = user.UserName,
                VotesCount = diet.Votes.Count,
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
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(EditDietInputModel input, string dietId)
        {
            await this.dietsService.Update(dietId, input);
            return this.Redirect("/Diets/YourDiets");
        }
    }
}
