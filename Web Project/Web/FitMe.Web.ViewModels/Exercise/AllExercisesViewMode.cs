namespace FitMe.Web.ViewModels.Categories
{
    using System.Collections.Generic;

    using FitMe.Data.Models;

    public class AllExercisesViewMode
    {
        public IEnumerable<ExerciseViewModel> Exercises { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }
    }
}
