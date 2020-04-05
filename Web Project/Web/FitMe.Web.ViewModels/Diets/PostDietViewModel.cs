namespace FitMe.Web.ViewModels.Diets
{
    using System;

    public class PostDietViewModel
    {
        public string Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string UserUserName { get; set; }

        public int CommentsCount { get; set; }
    }
}
