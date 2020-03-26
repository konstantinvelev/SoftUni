namespace FitMe.Web.ViewModels.Diets
{
    using System.Collections.Generic;

    using FitMe.Data.Models;

    public class YourDietsViewModel
    {
        public IEnumerable<Diet> Diets { get; set; }
    }
}
