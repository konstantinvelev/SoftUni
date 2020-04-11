namespace FitMe.Web.ViewModels.Comments
{
    using FitMe.Data.Models;
    using FitMe.Services.Mapping;

   public class CommentViewModel : IMapFrom<Comment>
    {
        public string Id { get; set; }

        public string PostId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string UserUserName { get; set; }

        public string CreatedOn { get; set; }

        public int VotesCount { get; set; }
    }
}
