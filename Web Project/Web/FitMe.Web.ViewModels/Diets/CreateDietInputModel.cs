namespace FitMe.Web.ViewModels.Diets
{
    using System.ComponentModel.DataAnnotations;

    using FitMe.Data.Models;
    using FitMe.Services.Mapping;

    public class CreateDietInputModel : IMapTo<Diet>
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        [Display(Name = "Title")]

        public string Title { get; set; }

        [Required]
        [Display(Name = "Description")]

        public string Description { get; set; }
    }
}
