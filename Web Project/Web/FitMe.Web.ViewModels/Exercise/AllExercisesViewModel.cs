namespace FitMe.Web.ViewModels.Exercise
{
    using System.Collections.Generic;


    public class AllExercisesViewModel
    {
        public IEnumerable<ExerciseViewModel> Exercises { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }
    }
}
