namespace FitMe.Web.ViewModels.Exercise
{
    using System.Collections.Generic;

    using FitMe.Data.Models;

    public class YourExercisesViewModel
    {
        public IEnumerable<Exercise> Exercises { get; set; }
    }
}
