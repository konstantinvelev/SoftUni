using System;

namespace FitMe.Web.ViewModels.Categories
{
    public class ExerciseViewModel
    {
        public string UserUserName { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CommentsCount { get; set; }

        public string Id { get; set; }

        public string Title { get; set; }

        public string Gender { get; set; }

        public string Content { get; set; }
    }
}