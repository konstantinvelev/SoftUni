namespace FitMe.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FitMe.Data.Models;
    using FitMe.Web.ViewModels.Diets;

    public interface IDietsService
    {
        IEnumerable<Diet> GetAll(int? take = null, int skip = 0);

        Task CreateDietAsync(CreateDietInputModel create, string userId);

        Task<Diet> GetDietByIdAsync(string id);

        Task DeleteDietAsync(string id);

        IEnumerable<Diet> GetDietsByUser(string userId);

        Task Update(EditDietInputModel input);

        int GetCount();

        Task AddCommentToDiet(Diet diet, Comment comment);
    }
}
