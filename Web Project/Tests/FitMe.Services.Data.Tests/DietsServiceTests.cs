namespace FitMe.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using FitMe.Data;
    using FitMe.Data.Common.Repositories;
    using FitMe.Data.Models;
    using FitMe.Data.Repositories;
    using FitMe.Web.ViewModels.Diets;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Moq;
    using Xunit;

    public class DietsServiceTests
    {
        private ApplicationDbContext db;
        private Mock<IDietsService> mockService;
        private DietsService service;

        public DietsServiceTests()
        {
            this.db = BaseServiceTests.CreateContext();
            this.mockService = new Mock<IDietsService>();

            var dietsRepository = new EfDeletableEntityRepository<Diet>(this.db);
            var userRepository = new EfDeletableEntityRepository<ApplicationUser>(this.db);
            this.service = new DietsService(dietsRepository, userRepository);
        }

        [Fact]
        public async Task CreateDietCorrectlyTest()
        {
            var input = new CreateDietInputModel
            {
                Title = "das",
                Description = "ads",
            };

            var input2 = new CreateDietInputModel
            {
                Title = "das",
                Description = "ads",
            };

            await this.service.CreateDietAsync(input, Guid.NewGuid().ToString());
            await this.service.CreateDietAsync(input2, Guid.NewGuid().ToString());

            Assert.Equal(2, this.db.Diets.Count());
        }

        [Fact]
        public async Task GetCountCorrectlyTest()
        {
            var input = new CreateDietInputModel
            {
                Title = "das",
                Description = "ads",
            };

            await this.service.CreateDietAsync(input, Guid.NewGuid().ToString());

            var count = this.service.GetCount();

            Assert.Equal(1, count);
        }

        [Fact]
        public async Task DeleteDietCorrectlyTest()
        {
            var diet = new Diet
            {
                Id = "6b44d6d8-9bb2-4469-a227-a039c5751700",
                Title = "mrKing",
                Description = "mrKing",
            };
            this.db.Diets.Add(diet);
            this.db.SaveChanges();

            await this.service.DeleteDietAsync(diet.Id);
            Assert.Equal(0, this.service.GetCount());
        }

        [Fact]
        public async Task GetDietByIdAsyncCorrectlyTest()
        {
            var data = new List<Diet>()
            {
                new Diet{ Id = "6b44d6d8-9bb2-4469-a227-a039c5751700", Title = "sad", Description = "adsa"},
                new Diet{ Id = "6b44d6d8-9bb2-4469-a527-a039c5751700", Title = "sad", Description = "adsa"},
            };
            this.db.Diets.AddRange(data);
            await this.db.SaveChangesAsync();

            var diet = await this.service.GetDietByIdAsync("6b44d6d8-9bb2-4469-a227-a039c5751700");
            Assert.Equal("sad", diet.Title);
        }

        [Fact]
        public void GetDietsByUserCorrectlyTest()
        {
            var data = new Diet
            {
                Id = "6b44d6d8-9bb2-4469-a227-a039c5751700",
                Title = "sad",
                Description = "adsa",
                UserId = "6b44d699-9bb2-4469-a227-a039c5751700",
            };
            this.db.Diets.AddRange(data);
            this.db.SaveChangesAsync();

            var diet = this.service.GetDietsByUser("6b44d699-9bb2-4469-a227-a039c5751700");
            Assert.NotEmpty(diet);
        }

        [Fact]
        public async Task UpdateTitleCorrectlyTest()
        {
            var data = new Diet
            {
                Id = "6b44d6d8-9bb2-4469-a227-a039c5751700",
                Title = "sad",
                Description = "adsa",
            };
            this.db.Diets.AddRange(data);
            await this.db.SaveChangesAsync();

            var editModel = new EditDietInputModel
            {
                Title = "Title",
                Description = "adsa",
                DietId = data.Id,
            };

            await this.service.Update(editModel);
            Assert.Equal("Title", data.Title);
        }

        [Fact]
        public async Task UpdateDescriptionCorrectlyTest()
        {
            var data = new Diet
            {
                Id = "6b44d6d8-9bb2-4469-a227-a039c5751700",
                Title = "sad",
                Description = "adsa",
            };
            this.db.Diets.AddRange(data);
            await this.db.SaveChangesAsync();

            var editModel = new EditDietInputModel
            {
                Title = "sad",
                Description = "Best",
                DietId = data.Id,
            };

            await this.service.Update(editModel);
            Assert.Equal("Best", data.Description);
        }

        [Fact]
        public async Task AddCommentToPostCorrectlyTest()
        {
            var diet = new Diet
            {
                Id = "6b44d6d8-9bb2-4469-a227-a039c5751700",
                Title = "mrKing",
                Description = "mrKing",
            };

            var comment = new Comment
            {
                Id = "sadaa",
                Content = "sss",
                PostId = "6b44d6d8-9bb2-4469-a227-a039c5751788",
            };

            this.db.Diets.Add(diet);
            await this.db.SaveChangesAsync();

            await this.service.AddCommentToDiet(await this.service.GetDietByIdAsync("6b44d6d8-9bb2-4469-a227-a039c5751700"), comment);

            Assert.Equal(1, diet.Comments.Count);
        }
    }
}
