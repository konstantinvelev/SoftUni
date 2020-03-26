namespace FitMe.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitMe.Data.Models;
    using FitMe.Web.ViewModels.Exercise;

    public interface IExercisesService
    {
        public IEnumerable<Exercise> GetAll();

        public Task CreateWomansExercisesAsync(CreateExercisesInputModel create, string userId);

        public Task CreateMansExercisesAsync(CreateExercisesInputModel create, string userId);

        public Task<Exercise> GetDietByIdAsync(string id);

        public Task DeleteDietAsync(string id);

        public IEnumerable<Exercise> GetExersisesByUser(string userId);
    }
}
