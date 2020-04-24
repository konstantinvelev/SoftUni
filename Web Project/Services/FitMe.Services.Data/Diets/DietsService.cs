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

        public async Task CreateDietAsync(CreateDietInputModel create, string userId)
        {
            var diet = new Diet
            {
                Title = create.Title,
                Description = create.Description,
                UserId = userId,
            };
            await this.dietsRepository.AddAsync(diet);
            await this.dietsRepository.SaveChangesAsync();
        }

        public IEnumerable<Diet> GetAll(int? take = null, int skip = 0)
        {
            var query = this.dietsRepository.All()
                .OrderByDescending(s => s.CreatedOn)
                .Skip(skip);

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.ToList();
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

        public async Task Update(EditDietInputModel input)
        {
            var diet = await this.dietsRepository.GetByIdWithDeletedAsync(input.DietId);

            diet.Title = input.Title;
            diet.Description = input.Description;
            await this.dietsRepository.SaveChangesAsync();
        }

        public int GetCount()
        {
            var count = this.dietsRepository.All().Count();

            return count;
        }

        public async Task AddCommentToDiet(Diet diet, Comment comment)
        {
            diet.Comments.Add(comment);

            await this.dietsRepository.SaveChangesAsync();
        }
    }
}
