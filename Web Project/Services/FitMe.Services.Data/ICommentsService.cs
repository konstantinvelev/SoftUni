namespace FitMe.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitMe.Data.Models;
    using FitMe.Web.ViewModels.Comments;

    public interface ICommentsService
    {
        IEnumerable<Comment> All();

        Task<Comment> CreateComment(CreateCommentInputModel input);
    }
}
