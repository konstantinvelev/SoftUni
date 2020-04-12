using FitMe.Data.Models;
using FitMe.Web.ViewModels.Comments;
using System.Collections.Generic;

namespace FitMe.Web.ViewModels.Diets
{
    public class DietsDetailViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string UserUserName { get; set; }

        public string CreatedOn { get; set; }

        public int VotesCount { get; set; }

        public string UserUserId { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}
