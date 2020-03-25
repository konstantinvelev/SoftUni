namespace FitMe.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitMe.Data.Models;
    using FitMe.Web.ViewModels.Diets;

    public interface IDietsService
    {
        public IEnumerable<Diet> GetAll();

        public Task CreateWomansDietAsync(CreateDietInputModel create, string userId);

        public Task CreateMansDietAsync(CreateDietInputModel create, string userId);

        public Task<Diet> GetDietByIdAsync(string id);
    }
}
