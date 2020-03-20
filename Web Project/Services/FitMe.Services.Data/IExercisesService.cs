namespace FitMe.Services.Data
{
    using System.Collections.Generic;

    using FitMe.Data.Models;

    public interface IExercisesService
    {
        IEnumerable<Exercise> GetAll();
    }
}
