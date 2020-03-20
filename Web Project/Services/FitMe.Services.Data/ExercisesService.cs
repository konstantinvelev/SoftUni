namespace FitMe.Services.Data
{
    using System.Collections.Generic;

    using FitMe.Data.Common.Repositories;
    using FitMe.Data.Models;

    public class ExercisesService : IExercisesService
    {
        private readonly IDeletableEntityRepository<Exercise> repository;

        public ExercisesService(IDeletableEntityRepository<Exercise> repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Exercise> GetAll()
        {
            var allExercise = this.repository.All();

            return allExercise;
        }
    }
}
