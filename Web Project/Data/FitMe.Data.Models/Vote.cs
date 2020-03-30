namespace FitMe.Data.Models
{
    using FitMe.Data.Common.Models;

    public class Vote : BaseModel<int>
    {
        public VoteType VoteType { get; set; }

        public string PostId { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}