namespace FitMe.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using FitMe.Data.Common.Models;

    public class Comment : BaseDeletableModel<string>
    {
        public Comment()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Votes = new HashSet<Vote>();
        }

        public ApplicationUser User { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [MaxLength(int.MaxValue)]
        public string Content { get; set; }

        public Exercise Exercise { get; set; }

        public string ExerciseId { get; set; }

        public ICollection<Vote> Votes { get; set; }
    }
}