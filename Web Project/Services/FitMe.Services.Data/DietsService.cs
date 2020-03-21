namespace FitMe.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FitMe.Data.Common.Repositories;
    using FitMe.Data.Models;
    using Microsoft.AspNetCore.Identity;

    public class DietsService : IDietsService
    {
        private readonly IDeletableEntityRepository<Diet> dietsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public DietsService(IDeletableEntityRepository<Diet> dietsRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.dietsRepository = dietsRepository;
            this.usersRepository = usersRepository;
        }

        public IEnumerable<Diet> GetAll()
        {
            var diets = this.dietsRepository.All().ToList();

            return diets;
        }
    }
}
