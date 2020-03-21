using FitMe.Data.Models;

namespace FitMe.Web.ViewModels.Diets
{
    public class CreateDietInputModel
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public Gender TypeOfGender { get; set; }
    }
}
