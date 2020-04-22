namespace FitMe.Services.Data.Tests
{
    using FitMe.Data;
    using FitMe.Data.Models;
    using FitMe.Data.Repositories;
    using Xunit;

    public class UsersServiceTests
    {
        private ApplicationDbContext db;
        private UsersService service;

        public UsersServiceTests()
        {
            this.db = BaseServiceTests.CreateContext();

            var commentsRepository = new EfDeletableEntityRepository<ApplicationUser>(this.db);
            this.service = new UsersService(commentsRepository);
        }

        [Fact]
        public void GetUserById()
        {
            var input = new ApplicationUser
            {
                Id = "6b44d6d8-9bb2-4469-a227-a039c5751700",
                Email = "admin@abv.bg",
                TypeOfGender = Gender.Man,
            };

            var input2 = new ApplicationUser
            {
                Id = "6b44d6d8-9bb2-4569-a227-a039c5751700",
                Email = "admin2@abv.bg",
                TypeOfGender = Gender.Woman,
            };
            this.db.Users.Add(input);
            this.db.Users.Add(input2);
            this.db.SaveChanges();

            var user = this.service.GetUserById(input.Id);

            Assert.Equal("admin@abv.bg", user.Email);
        }
    }
}
