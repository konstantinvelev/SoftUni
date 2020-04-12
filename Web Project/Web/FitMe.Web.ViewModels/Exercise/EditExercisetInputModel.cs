using FitMe.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace FitMe.Web.ViewModels.Exercise
{
    public class EditExercisetInputModel
    {
        public string ExerciseId { get; set; }

        [Required]
        [MinLength(3)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Gender { get; set; }

        public string Video { get; set; }
    }
}
