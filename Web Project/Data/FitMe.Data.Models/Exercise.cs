namespace FitMe.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using FitMe.Data.Common.Models;

    using FitMe.Data.Models;

    public class Exercise : BaseDeletableModel<string>
    {
        public Exercise()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Commetns = new HashSet<Comment>();
            this.Votes = new HashSet<Vote>();
        }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(int.MaxValue)]
        public string Content { get; set; }

        [Required]
        public Gender TypeOfGender { get; set; }

        public byte[] Video { get; set; }

        [Required]
        public string UserID { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<Comment> Commetns { get; set; }

        public ICollection<Vote> Votes { get; set; }

        public Category Category { get; set; }

        public string CategoryId { get; set; }
    }
}