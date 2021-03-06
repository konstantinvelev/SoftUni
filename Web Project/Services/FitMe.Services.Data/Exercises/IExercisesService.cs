﻿namespace FitMe.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitMe.Data.Models;
    using FitMe.Web.ViewModels.Exercise;

    public interface IExercisesService
    {
        IEnumerable<Exercise> GetAll(int? take = null, int skip = 0);

        IEnumerable<T> GetAllForMans<T>(int? take = null, int skip = 0);

        IEnumerable<T> GetAllForWomens<T>(int? take = null, int skip = 0);

        Task CreateWomansExercisesAsync(CreateExercisesInputModel create, string userId);

        Task CreateMansExercisesAsync(CreateExercisesInputModel create, string userId);

        Task<Exercise> GetExercisesByIdAsync(string id);

        Task DeleteExercisesAsync(string id);

        IEnumerable<Exercise> GetExersisesByUser(string userId);

        Task Update(EditExercisetInputModel input);

        int GetCount();

        Task AddCommenToExercise(Exercise exercise, Comment comment);
    }
}
