﻿namespace FitMe.Services.Data
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

        public IEnumerable<Exercise> GetAll()
        {
            var allExercise = this.exerciseRepository.All().ToList();

            return allExercise;
        }

        public async Task<Exercise> GetDietByIdAsync(string id)
        {
            var exercise = await this.exerciseRepository.GetByIdWithDeletedAsync(id);
            return exercise;
        }

        public async Task DeleteDietAsync(string id)
        {
            var diet = await this.exerciseRepository.GetByIdWithDeletedAsync(id);
            this.exerciseRepository.Delete(diet);
            await this.exerciseRepository.SaveChangesAsync();
        }
    }
}
