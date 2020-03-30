﻿namespace FitMe.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using FitMe.Data.Models;
    using FitMe.Services.Data;
    using FitMe.Web.ViewModels.Diets;
    using FitMe.Web.ViewModels.Exercise;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class DietsController : BaseController
    {
        private readonly IDietsService dietsService;
        private readonly UserManager<ApplicationUser> userManager;

        public DietsController(
            IDietsService dietsService,
            UserManager<ApplicationUser> userManager)
        {
            this.dietsService = dietsService;
            this.userManager = userManager;
        }

        public IActionResult AllForMans()
        {
            var all = this.dietsService.GetAll().Where(s => s.TypeOfGender.ToString() == "Man");
            var viewModel = new AllDietsViewModel
            {
                Diets = all,
            };

            return this.View(viewModel);
        }

        public IActionResult AllForWomans()
        {
            var all = this.dietsService.GetAll().Where(s => s.TypeOfGender.ToString() == "Woman");
            var viewModel = new AllDietsViewModel
            {
                Diets = all,
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
        public async Task<IActionResult> Create(CreateDietInputModel input)
        {
            if (!ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            if (input.Gender.ToString() == "Male")
            {
                await this.dietsService.CreateMansDietAsync(input, user.Id);
                return this.Redirect("/Diets/AllForMans");
            }
            else
            {
                await this.dietsService.CreateWomansDietAsync(input, user.Id);
                return this.Redirect("/Diets/AllForMans");
            }
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
                Gender = diet.TypeOfGender.ToString(),
                CreatedOn = diet.CreatedOn.ToString(),
                UserUserName = user.UserName,
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
                Gender = diet.TypeOfGender.ToString(),
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
