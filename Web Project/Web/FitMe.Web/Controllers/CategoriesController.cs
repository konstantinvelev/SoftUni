namespace FitMe.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class CategoriesController : BaseController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult Mans()
        {
            return this.View();
        }

        [HttpGet]
        public IActionResult Womans()
        {
            return this.View();
        }
    }
}
