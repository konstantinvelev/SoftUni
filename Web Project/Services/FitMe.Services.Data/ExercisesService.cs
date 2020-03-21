namespace FitMe.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using FitMe.Data.Common.Repositories;
    using FitMe.Data.Models;

    public class ExercisesService : IExercisesService
    {
        private readonly IDeletableEntityRepository<Exercise> exerciseRepository;

        public ExercisesService(IDeletableEntityRepository<Exercise> exerciseRepository)
        {
            this.exerciseRepository = exerciseRepository;
        }

        public IEnumerable<Exercise> GetAll()
        {
            var allExercise = this.exerciseRepository.All().ToList();

            return allExercise;
        }
    }
}
