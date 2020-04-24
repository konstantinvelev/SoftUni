namespace FitMe.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitMe.Data.Models;
    using FitMe.Web.ViewModels.Comments;

    public interface ICommentsService
    {
        IEnumerable<Comment> All();

        Task DeleteComments(ICollection<Comment> all);

        IEnumerable<T> All<T>();

        Task<Comment> CreateComment(CreateCommentInputModel input);

        Task Delete(string commentId);

        Comment GetById(string id);
    }
}
