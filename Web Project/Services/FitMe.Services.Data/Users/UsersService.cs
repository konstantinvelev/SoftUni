namespace FitMe.Services.Data
{
    using System.Linq;

    using FitMe.Data.Common.Repositories;
    using FitMe.Data.Models;

    public class UsersService : IUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> repository;

        public UsersService(IDeletableEntityRepository<ApplicationUser> repository)
        {
            this.repository = repository;
        }

        public ApplicationUser GetUserById(string id)
        {
            var user = this.repository.All().FirstOrDefault(x => x.Id == id);
            return user;
        }
    }
}
