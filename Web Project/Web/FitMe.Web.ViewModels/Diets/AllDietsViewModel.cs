namespace FitMe.Web.ViewModels.Diets
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using FitMe.Data.Models;

    public class AllDietsViewModel
    {
        public IEnumerable<PostDietViewModel> Diets { get; set; }

        public int PagesCount { get; set; }

        public int CurrentPage { get; set; }
    }
}
