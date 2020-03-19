namespace FitMe.Data.Models
{
    using System;
    using System.Collections.Generic;

    using FitMe.Data.Common.Models;

    public class Category : BaseDeletableModel<string>
    {
        public Category()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Exercises = new HashSet<Exercise>();
        }

        public ICollection<Exercise> Exercises { get; set; }

        public Gender Gender { get; set; }
    }
}
