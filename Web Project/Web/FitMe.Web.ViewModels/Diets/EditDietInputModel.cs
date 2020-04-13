namespace FitMe.Web.ViewModels.Diets
{
    using System.ComponentModel.DataAnnotations;

    public class EditDietInputModel
    {
        public string DietId { get; set; }

        [Required(ErrorMessage = "Title must be not null")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description must be not null")]
        [StringLength(int.MaxValue, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string Description { get; set; }

        [Required]
        public string Gender { get; set; }
    }
}
