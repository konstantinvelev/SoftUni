namespace FitMe.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FitMe.Data.Common.Repositories;
    using FitMe.Data.Models;
    using FitMe.Web.ViewModels.Exercise;

    public class ExercisesService : IExercisesService
    {
        private readonly IDeletableEntityRepository<Exercise> exerciseRepository;

        public ExercisesService(IDeletableEntityRepository<Exercise> exerciseRepository)
        {
            this.exerciseRepository = exerciseRepository;
        }

        public async Task CreateWomansExercisesAsync(CreateExercisesInputModel create, string userId)
        {
            var exercise = new Exercise
            {
                Title = create.Title,
                Content = create.Content,
                TypeOfGender = Enum.Parse<Gender>("Woman"),
                Video = create.Video,
                UserID = userId,
            };
            await this.exerciseRepository.AddAsync(exercise);
            await this.exerciseRepository.SaveChangesAsync();
        }

        public async Task CreateMansExercisesAsync(CreateExercisesInputModel create, string userId)
        {
            var exercise = new Exercise
            {
                Title = create.Title,
                Content = create.Content,
                TypeOfGender = Enum.Parse<Gender>("Man"),
                Video = create.Video,
                UserID = userId,
            };
            await this.exerciseRepository.AddAsync(exercise);
            await this.exerciseRepository.SaveChangesAsync();
        }

        public IEnumerable<Exercise> GetAll(int? take = null, int skip = 0)
        {
            var query = this.exerciseRepository.All()
                .OrderByDescending(s => s.CreatedOn)
                .Skip(skip);

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.ToList();

        }

        public async Task<Exercise> GetExercisesByIdAsync(string id)
        {
            var exercise = await this.exerciseRepository.GetByIdWithDeletedAsync(id);
            return exercise;
        }

        public async Task DeleteExercisesAsync(string id)
        {
            var diet = await this.exerciseRepository.GetByIdWithDeletedAsync(id);
            this.exerciseRepository.Delete(diet);
            await this.exerciseRepository.SaveChangesAsync();
        }

        public IEnumerable<Exercise> GetExersisesByUser(string userId)
        {
            var exercises = this.exerciseRepository.All().Where(s => s.UserID == userId).ToList();

            return exercises;
        }

        public int GetCount()
        {
            var count = this.exerciseRepository.All().Count();
            return count;
        }
    }
}
