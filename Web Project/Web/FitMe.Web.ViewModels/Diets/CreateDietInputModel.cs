namespace FitMe.Web.ViewModels.Diets
{
    using System.ComponentModel.DataAnnotations;

    using FitMe.Data.Models;
    using FitMe.Services.Mapping;

    public class CreateDietInputModel : IMapTo<Diet>
    {
        [Required(ErrorMessage = "Title must be not null")]
        [StringLength(int.MaxValue, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description must be not null")]
        [StringLength(int.MaxValue, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string Description { get; set; }
    }
}
