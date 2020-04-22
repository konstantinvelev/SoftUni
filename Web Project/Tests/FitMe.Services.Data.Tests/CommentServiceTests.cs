namespace FitMe.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FitMe.Data;
    using FitMe.Data.Models;
    using FitMe.Data.Repositories;
    using FitMe.Web.ViewModels.Comments;
    using Moq;
    using Xunit;

    public class CommentServiceTests
    {
        private ApplicationDbContext db;
        private Mock<ICommentsService> mockService;
        private CommentsService service;

        public CommentServiceTests()
        {
            this.db = BaseServiceTests.CreateContext();
            this.mockService = new Mock<ICommentsService>();

            var commentsRepository = new EfDeletableEntityRepository<Comment>(this.db);
            this.service = new CommentsService(commentsRepository);
        }

        [Fact]
        public async Task CreateCommentCorrectlyTest()
        {
            var input = new CreateCommentInputModel
            {
                Content = "ads",
                PostId = "6b44d6d8-9bb2-4469-a227-a039c5751700",
                UserId = "6b44d6d8-9bb2-4469-a227-a039c5751722",
                UserUserName = "Koceto",
            };

            var input2 = new CreateCommentInputModel
            {
                Content = "ddd",
                PostId = "6b44d6d8-9bb2-4469-a227-a038c5751700",
                UserId = "6b44d6d8-9bb2-4469-a227-a035c5751722",
                UserUserName = "Koceto",
            };

            await this.service.CreateComment(input);
            await this.service.CreateComment(input2);

            Assert.Equal(2, this.db.Comments.Count());
        }

        [Fact]
        public void GetAllCorrectlyTest()
        {
            var exercise = new Comment
            {
                Id = "6b44d6d8-9bb2-4469-a227-a039c5751700",
                Content = "adsa",
                UserId = "6b44d699-9bb2-4469-a227-a039c5751700",
                PostId = "6b44d6d8-9bb2-4469-a227-a039c5751707",
            };
            var exercise2 = new Comment
            {
                Id = "6b44d6d6-9bb2-4469-a227-a039c5751700",
                Content = "hgfd",
                UserId = "6b44d669-9bb2-4469-a227-a039c5751700",
                PostId = "6b44d678-9bb2-4469-a227-a039c5751707",
            };
            this.db.Comments.Add(exercise);
            this.db.Comments.Add(exercise2);
            this.db.SaveChanges();
            var exercises = this.service.All();
            Assert.Equal(2, exercises.Count());
        }

        [Fact]
        public async Task DeleteCommentCorrectlyTest()
        {
            var exercise = new Comment
            {
                Id = "6b44d6d6-9bb2-4469-a227-a039c5751700",
                Content = "hgfd",
                UserId = "6b44d669-9bb2-4469-a227-a039c5751700",
                PostId = "6b44d678-9bb2-4469-a227-a039c5751707",
            };
            this.db.Comments.Add(exercise);
            this.db.SaveChanges();

            await this.service.Delete(exercise.Id);
            Assert.Empty(this.service.All());
        }

        [Fact]
        public void GetCommentByIdAsyncCorrectlyTest()
        {
            var data = new List<Comment>()
            {
                new Comment { Id = "6b44d6d8-9bb2-4469-a227-a039c5751700", Content = "badabum", UserId = "6b44d699-9bb2-4469-a227-a039c5751700", PostId = "6b44d6d8-9bb2-4469-a227-a039c5751707" },
                new Comment { Id = "6b44d6d8-9bb2-4469-a527-a039c5751700", Content = "budabam", UserId = "6b44d669-9bb2-4469-a227-a039c5751700", PostId = "6b44d678-9bb2-4469-a227-a039c5751707" },
            };
            this.db.Comments.AddRange(data);
            this.db.SaveChanges();

            var comment = this.service.GetById("6b44d6d8-9bb2-4469-a227-a039c5751700");
            Assert.Equal("badabum", comment.Content);
        }

        [Fact]
        public void DeleteCommentsForPostCorrectlyTest()
        {
            var data = new List<Comment>()
            {
                new Comment { Id = "6b44d6d8-9bb2-4469-a227-a039c5751700", Content = "badabum", UserId = "6b44d699-9bb2-4469-a227-a039c5751230", PostId = "6ba4d6d8-9bb2-4469-a227-a039c5751707" },
                new Comment { Id = "6b44d6d8-9bb2-4469-a527-a039c5751700", Content = "budabam", UserId = "6b44d669-9bb2-4469-a227-a039c5753210", PostId = "6bf4d678-9bb2-4469-a227-a039c5751707" },
                new Comment { Id = "6b44d6d8-9bb2-4469-a527-a039c5751180", Content = "budbam", UserId = "6b44d669-9bb2-4469-a227-a039c5752310", PostId = "6b4rd678-9bb2-4469-a227-a039c5751707" },
                new Comment { Id = "6b44d6d8-9bb2-4469-a527-a079c5751120", Content = "budaam", UserId = "6b44d669-9bb2-4469-a227-a039c5751700", PostId = "6b4dd678-9bb2-4469-a227-a039c5751707" },
            };

            this.db.Comments.AddRange(data);
            this.db.SaveChangesAsync();

            var comments = this.service.All();
            var deletedComments = this.service.DeleteComments((ICollection<Comment>)comments);
            Assert.Empty(this.db.Comments);
        }
    }
}
