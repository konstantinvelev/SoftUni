namespace FitMe.Data.Models
{
    using System;
    using System.Collections.Generic;

    using FitMe.Data.Common.Models;

    public class WomansCategory : BaseDeletableModel<string>
    {
        public WomansCategory()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Exercises = new HashSet<Exercise>();
            this.Gender = Constants.WomenGender;
        }

        public ICollection<Exercise> Exercises { get; set; }

        public string Gender { get; set; } = Constants.WomenGender;
    }
}
