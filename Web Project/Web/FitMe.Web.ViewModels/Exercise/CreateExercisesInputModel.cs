using FitMe.Data.Models;

namespace FitMe.Web.ViewModels.Exercise
{
    public class CreateExercisesInputModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public Gender TypeOfGender { get; set; }

        public byte[] Video { get; set; }

        public string UserID { get; set; }
    }
}
