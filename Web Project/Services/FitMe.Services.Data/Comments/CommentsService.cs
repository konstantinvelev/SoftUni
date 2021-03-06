﻿namespace FitMe.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FitMe.Data.Common.Repositories;
    using FitMe.Data.Models;
    using FitMe.Services.Mapping;
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

        public IEnumerable<T> All<T>()
        {
            var comments = this.commentsRepository.All().To<T>().ToList();
            return comments;
        }

        public async Task<Comment> CreateComment(CreateCommentInputModel input)
        {
            var comment = new Comment
            {
                Content = input.Content,
                PostId = input.PostId,
                UserId = input.UserId,
            };

            await this.commentsRepository.AddAsync(comment);
            await this.commentsRepository.SaveChangesAsync();
            return comment;
        }

        public async Task Delete(string commentId)
        {
            var comment = await this.commentsRepository.GetByIdWithDeletedAsync(commentId);
            this.commentsRepository.Delete(comment);
            await this.commentsRepository.SaveChangesAsync();
        }

        public async Task DeleteComments(ICollection<Comment> all)
        {
            foreach (var item in all)
            {
                this.commentsRepository.Delete(item);
            }

            await this.commentsRepository.SaveChangesAsync();
        }

        public Comment GetById(string id)
        {
            var comment = this.commentsRepository.All().FirstOrDefault(s => s.Id == id);
            return comment;
        }
    }
}
