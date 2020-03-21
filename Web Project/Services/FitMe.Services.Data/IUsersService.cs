namespace FitMe.Services.Data
{
    using FitMe.Data.Models;

    public interface IUsersService
    {
        ApplicationUser GetUserById(string id);
    }
}
