﻿namespace FitMe.Web.ViewModels.Diets
{
    using System.ComponentModel.DataAnnotations;

    using FitMe.Data.Models;

    public class CreateDietInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(35)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Gender { get; set; }
    }
}
