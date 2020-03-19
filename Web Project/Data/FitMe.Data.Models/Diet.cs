using FitMe.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FitMe.Data.Models
{
    public class Diet : BaseDeletableModel<string>
    {
        public Diet()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Votes = new HashSet<Vote>();
            this.Comments = new HashSet<Comment>();
        }

        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        [MaxLength(int.MaxValue)]
        public string Description { get; set; }

        public ICollection<Comment> Comments { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<Vote> Votes { get; set; }
    }
}
