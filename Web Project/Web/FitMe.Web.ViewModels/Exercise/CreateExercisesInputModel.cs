namespace FitMe.Web.ViewModels.Exercise
{
    using System.ComponentModel.DataAnnotations;

    using FitMe.Data.Models;

    public class CreateExercisesInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MaxLength(int.MaxValue)]
        public string Content { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Video { get; set; }

        public string UserID { get; set; }
    }
}
