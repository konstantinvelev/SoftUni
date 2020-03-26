namespace FitMe.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using FitMe.Data.Common.Repositories;
    using FitMe.Data.Models;
    using FitMe.Web.ViewModels.Diets;
    using Microsoft.AspNetCore.Identity;

    public class DietsService : IDietsService
    {
        private readonly IDeletableEntityRepository<Diet> dietsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public DietsService(
            IDeletableEntityRepository<Diet> dietsRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.dietsRepository = dietsRepository;
            this.usersRepository = usersRepository;
        }

        public async Task CreateWomansDietAsync(CreateDietInputModel create, string userId)
        {
            var diet = new Diet
            {
                Title = create.Title,
                Description = create.Description,
                TypeOfGender = Enum.Parse<Gender>("Woman"),
                UserId = userId,
            };
            await this.dietsRepository.AddAsync(diet);
            await this.dietsRepository.SaveChangesAsync();
        }

        public async Task CreateMansDietAsync(CreateDietInputModel create, string userId)
        {
            var diet = new Diet
            {
                Title = create.Title,
                Description = create.Description,
                TypeOfGender = Enum.Parse<Gender>("Man"),
                UserId = userId,
            };
            await this.dietsRepository.AddAsync(diet);
            await this.dietsRepository.SaveChangesAsync();
        }

        public IEnumerable<Diet> GetAll()
        {
            var diets = this.dietsRepository.All().ToList();

            return diets;
        }

        public async Task<Diet> GetDietByIdAsync(string id)
        {
            var diet = await this.dietsRepository.GetByIdWithDeletedAsync(id);
            return diet;
        }

        public async Task DeleteDietAsync(string id)
        {
            var diet = await this.dietsRepository.GetByIdWithDeletedAsync(id);
            this.dietsRepository.Delete(diet);
            await this.dietsRepository.SaveChangesAsync();
        }

        public IEnumerable<Diet> GetDietsByUser(string userId)
        {
            var diets = this.dietsRepository.All().Where(s => s.UserId == userId).ToList();

            return diets;
        }

        public async Task Update(string dietId, EditDietInputModel input)
        {
            var diet = await this.dietsRepository.GetByIdWithDeletedAsync(dietId);
            diet = new Diet
            {
                Title = input.Title,
                Description = input.Description,
                TypeOfGender = Enum.Parse<Gender>(input.Gender),
            };
            this.dietsRepository.Update(diet);
            await this.dietsRepository.SaveChangesAsync();
        }
    }
}
