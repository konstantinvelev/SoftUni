namespace FitMe.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitMe.Data.Models;
    using FitMe.Web.ViewModels.Exercise;

    public interface IExercisesService
    {
        public IEnumerable<Exercise> GetAll();

        public Task CreateWomansDietAsync(CreateExercisesInputModel create, string userId);

        public Task CreateMansDietAsync(CreateExercisesInputModel create, string userId);
    }
}
