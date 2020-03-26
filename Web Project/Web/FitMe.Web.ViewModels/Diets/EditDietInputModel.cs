namespace FitMe.Web.ViewModels.Diets
{
    using System.ComponentModel.DataAnnotations;

   public class EditDietInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(35)]
        public string Title { get; set; }

        [Required]
        [MaxLength(35)]
        public string Description { get; set; }

        [Required]
        public string Gender { get; set; }
    }
}
