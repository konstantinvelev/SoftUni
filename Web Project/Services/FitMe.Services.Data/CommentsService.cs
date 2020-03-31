namespace FitMe.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using FitMe.Data.Common.Repositories;
    using FitMe.Data.Models;

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
    }
}
