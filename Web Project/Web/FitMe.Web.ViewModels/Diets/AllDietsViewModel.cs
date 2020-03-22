namespace FitMe.Web.ViewModels.Exercise
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using FitMe.Data.Models;

    public class AllDietsViewModel
    {
        public IEnumerable<Diet> Diets { get; set; }
    }
}
