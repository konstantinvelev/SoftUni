namespace FitMe.Data.Models
{
    using FitMe.Data.Common.Models;

    public class Vote : BaseModel<int>
    {
        public VoteType VoteType { get; set; }

        public string DietsId { get; set; }

        public Diet Diets { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string ExerciseId { get; set; }

        public Exercise Exercise { get; set; }
    }
}