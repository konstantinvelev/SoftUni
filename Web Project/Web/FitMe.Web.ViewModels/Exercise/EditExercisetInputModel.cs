using FitMe.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace FitMe.Web.ViewModels.Exercise
{
    public class EditExercisetInputModel
    {
        public string ExerciseId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(35)]
        public string Title { get; set; }

        [Required]
        [MaxLength(35)]
        public string Content { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public string Video { get; set; }
    }
}
