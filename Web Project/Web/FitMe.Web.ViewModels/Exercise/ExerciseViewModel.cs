namespace FitMe.Web.ViewModels.Exercise
{
    using System;

    using Data.Models;
    using FitMe.Services.Mapping;

    public class ExerciseViewModel : IMapFrom<Exercise>
    {
        public string UserUserName { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CommentsCount { get; set; }

        public string Id { get; set; }

        public string Title { get; set; }

        public string Gender { get; set; }

        public string Video { get; set; }

        public string Content { get; set; }
    }
}