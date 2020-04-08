namespace FitMe.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FitMe.Data.Common.Repositories;
    using FitMe.Data.Models;
    using FitMe.Web.ViewModels.Comments;

    public class CommentsService : ICommentsService
    {
        private readonly IDeletableEntityRepository<Comment> commentsRepository;

        public CommentsService(IDeletableEntityRepository<Comment> commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        public IEnumerable<Comment> All()
        {
            var comments = this.commentsRepository.All().ToList();
            return comments;
        }

        public async Task<Comment> CreateComment(CreateCommentInputModel input)
        {
            var comment = new Comment
            {
                Content = input.Content,
            };

            await this.commentsRepository.AddAsync(comment);
            await this.commentsRepository.SaveChangesAsync();
            return comment;
        }
    }
}
