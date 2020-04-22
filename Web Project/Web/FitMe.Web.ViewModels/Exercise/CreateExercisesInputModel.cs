namespace FitMe.Web.ViewModels.Exercise
{
    using System.ComponentModel.DataAnnotations;

    using FitMe.Data.Models;

    public class CreateExercisesInputModel
    {
        [Required(ErrorMessage = "Title must be not null")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content must be not null")]
        [StringLength(int.MaxValue, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string Content { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required(ErrorMessage = "URL must be not null")]
        public string Video { get; set; }

        public string UserID { get; set; }
    }
}
