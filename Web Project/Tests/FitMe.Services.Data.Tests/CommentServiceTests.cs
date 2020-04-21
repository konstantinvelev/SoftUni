using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FitMe.Data;
using FitMe.Data.Common.Repositories;
using FitMe.Data.Models;
using FitMe.Web.ViewModels.Comments;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace FitMe.Services.Data.Tests
{
    public class CommentServiceTests
    {
        private Mock<ICommentsService> mockService;

        public CommentServiceTests()
        {
            this.mockService = new Mock<ICommentsService>();
        }

        [Fact]
        public void CreateCorrectlyTest()
        {
            var input = new CreateCommentInputModel
            {
                Content = "ads",
            };

            var comment = this.mockService.Object.CreateComment(input);
            Assert.NotNull(comment);
        }

        [Fact]
        public async Task DeleteCorrectlyTest()
        {
            var input = new CreateCommentInputModel
            {
                UserId = "20",
                Content = "ads",
                PostId = "111",
            };
            var input2 = new CreateCommentInputModel
            {
                UserId = "20",
                Content = "ads",
                PostId = "222",
            };
            await this.mockService.Object.CreateComment(input);
            await this.mockService.Object.CreateComment(input2);

            var all = (ICollection<Comment>)this.mockService.Object.All();

            await this.mockService.Object.DeleteComments(all);
            Assert.Empty(this.mockService.Object.All());
        }
    }
}
