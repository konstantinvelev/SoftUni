namespace FitMe.Web.Controllers
{
    using System.Linq;

    using FitMe.Data.Models;
    using FitMe.Services.Data;
    using FitMe.Web.ViewModels.Exercise;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class DietsController : BaseController
    {
        private readonly IDietsService dietsService;

        public DietsController(IDietsService dietsService)
        {
            this.dietsService = dietsService;
        }

        [HttpGet]
        public IActionResult AddDiets()
        {
            return this.View();
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

        public IActionResult Create()
        {
            return this.View();
        }
    }
}
