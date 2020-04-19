namespace FitMe.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FitMe.Data.Common.Repositories;
    using FitMe.Data.Models;
    using FitMe.Web.ViewModels.Diets;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Moq;
    using Xunit;

    public class DietsServiceTests
    {
        private Mock<IDietsService> mockService;

        public DietsServiceTests()
        {
            this.mockService = new Mock<IDietsService>();
        }

        public void GetCountCorrectlyTest()
        {
            this.mockService.Setup(s => s.GetCount()).Returns(2);
            Assert.Equal(2, this.mockService.Object.GetCount());
        }

        [Fact]
        public void GetAllDietCorrectlyTest()
        {
            this.mockService.Setup(s => s.GetAll(null, 0)).Returns(new List<Diet>
            {
                new Diet{ Id = "6b44d6d8-9bb2-4469-a227-a039c5751700", Title = "koceto", Description = "koceto" },
                new Diet {Id = "23f7169a-2e9a-42d3-88f3-a8a5947e3a08", Title = "mrKing", Description = "mrKing" },
            });

            Assert.Equal(2, this.mockService.Object.GetAll().Count());
        }

        [Fact]
        public void CreateDietCorrectlyTest()
        {
            var input = new CreateDietInputModel
            {
                Title = "das",
                Description = "ads",
            };

            var diet = this.mockService.Setup(s => s.CreateDietAsync(input, Guid.NewGuid().ToString()));
            Assert.NotNull(diet);
        }

        [Fact]
        public void DeleteDietCorrectlyTest()
        {
            this.mockService.Setup(s => s.GetAll(null, 0)).Returns(new List<Diet>
            {
                new Diet {Id = "6b44d6d8-9bb2-4469-a227-a039c5751700", Title = "mrKing", Description = "mrKing" },
            });

            this.mockService.Object.DeleteDietAsync("6b44d6d8-9bb2-4469-a227-a039c5751700");
            Assert.Equal(0, this.mockService.Object.GetCount());
        }
    }
}
