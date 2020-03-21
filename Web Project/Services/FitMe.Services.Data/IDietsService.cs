namespace FitMe.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using FitMe.Data.Models;

    public interface IDietsService
    {
        public IEnumerable<Diet> GetAll();

    }
}
