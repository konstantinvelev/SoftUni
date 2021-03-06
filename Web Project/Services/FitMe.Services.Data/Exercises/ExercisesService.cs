﻿namespace FitMe.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FitMe.Data.Common.Repositories;
    using FitMe.Data.Models;
    using FitMe.Services.Mapping;
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

        public IEnumerable<T> GetAllForWomens<T>(int? take = null, int skip = 0)
        {
            var query = this.exerciseRepository
                .All()
                .Where(s => s.TypeOfGender == Gender.Woman)
                .OrderByDescending(s => s.CreatedOn)
                .Skip(skip);

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query.To<T>().ToList();
        }

        public IEnumerable<T> GetAllForMans<T>(int? take = null, int skip = 0)
        {
            var query = this.exerciseRepository.All()
                .Where(s => s.TypeOfGender == Gender.Man)
                .OrderByDescending(s => s.CreatedOn)
                .To<T>()
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
            var exercise = await this.exerciseRepository.GetByIdWithDeletedAsync(id);
            this.exerciseRepository.Delete(exercise);
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

        public async Task Update(EditExercisetInputModel input)
        {
            var exercise = await this.exerciseRepository.GetByIdWithDeletedAsync(input.ExerciseId);

            exercise.Title = input.Title;
            exercise.Content = input.Content;
            exercise.Video = input.Video;
            exercise.TypeOfGender = Enum.Parse<Gender>(input.Gender);
            await this.exerciseRepository.SaveChangesAsync();
        }

        public async Task AddCommenToExercise(Exercise exercise, Comment comment)
        {
            exercise.Commetns.Add(comment);

            await this.exerciseRepository.SaveChangesAsync();
        }
    }
}
