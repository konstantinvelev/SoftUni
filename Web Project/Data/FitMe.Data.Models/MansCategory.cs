namespace FitMe.Data.Models
{
    using System;
    using System.Collections.Generic;

    using FitMe.Data.Common.Models;

    public class MansCategory : BaseDeletableModel<string>
    {
        public MansCategory()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Exercises = new HashSet<Exercise>();
            this.Gender = Constants.MansGender;
        }

        public ICollection<Exercise> Exercises { get; set; }

        public string Gender { get; set; } = Constants.MansGender;
    }
}
