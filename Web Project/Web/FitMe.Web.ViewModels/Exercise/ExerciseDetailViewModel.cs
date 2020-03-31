namespace FitMe.Web.ViewModels.Exercise
{
   public class ExerciseDetailViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Gender { get; set; }

        public string UserUserName { get; set; }

        public string CreatedOn { get; set; }

        public byte[] Video { get; set; }

        public int VotesCount { get; set; }
    }
}
