namespace FitMe.Web.ViewModels.Exercise
{
    using System.ComponentModel.DataAnnotations;

    using FitMe.Data.Models;

    public class CreateExercisesInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(35)]
        public string Title { get; set; }

        [Required]
        [MaxLength(35)]
        public string Content { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public byte[] Video { get; set; }

        public string UserID { get; set; }
    }
}
