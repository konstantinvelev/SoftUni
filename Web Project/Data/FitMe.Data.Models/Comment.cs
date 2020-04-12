namespace FitMe.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using FitMe.Data.Common.Models;

    public class Comment : BaseDeletableModel<string>
    {
        public Comment()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string UserId { get; set; }

        [Required]
        [MaxLength(int.MaxValue)]
        public string Content { get; set; }

        public string PostId { get; set; }

        public Exercise Exercise { get; set; }

        public string ExerciseId { get; set; }

        public Diet Diet { get; set; }

        public string DietId { get; set; }
    }
}