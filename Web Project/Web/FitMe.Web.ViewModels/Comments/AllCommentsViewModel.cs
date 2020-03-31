namespace FitMe.Web.ViewModels.Comments
{
    using System.Collections.Generic;

    using FitMe.Data.Models;

    public class AllCommentsViewModel
    {
        public IEnumerable<Comment> Comments { get; set; }
    }
}
