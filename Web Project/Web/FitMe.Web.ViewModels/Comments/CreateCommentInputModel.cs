namespace FitMe.Web.ViewModels.Comments
{
    using System.ComponentModel.DataAnnotations;

    public class CreateCommentInputModel
    {
        public string PostId { get; set; }

        public string UserId { get; set; }

        public string UserUserName { get; set; }

        [Required(ErrorMessage = "Content must be not null")]
        [StringLength(int.MaxValue, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string Content { get; set; }
    }
}
