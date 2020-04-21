namespace FitMe.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using FitMe.Data;
    using FitMe.Data.Models;
    using FitMe.Data.Repositories;
    using FitMe.Web.ViewModels.Exercise;
    using Moq;
    using Xunit;

    public class ExercisesServiceTest
    {
        private ApplicationDbContext db;
        private Mock<IDietsService> mockService;
        private ExercisesService service;

        public ExercisesServiceTest()
        {
            this.db = BaseServiceTests.CreateContext();
            this.mockService = new Mock<IDietsService>();

            var exercisesRepository = new EfDeletableEntityRepository<Exercise>(this.db);
            this.service = new ExercisesService(exercisesRepository);
        }

        [Fact]
        public async Task CreateWomansExercisesAsyncCorrectly()
        {
            var input = new CreateExercisesInputModel
            {
                Title = "das",
                Content = "ads",
                Gender = Gender.Man.ToString(),
                Video = "https://google.com",
                UserID = "6b44d6d8-9bb2-4469-a227-a039c5751700",
            };

            var input2 = new CreateExercisesInputModel
            {
                Title = "das",
                Content = "ads",
                Gender = Gender.Woman.ToString(),
                Video = "https://google.com",
                UserID = "6b44d6d8-9bb2-4469-a227-a039c5751700",
            };

            await this.service.CreateMansExercisesAsync(input, Guid.NewGuid().ToString());
            await this.service.CreateWomansExercisesAsync(input2, Guid.NewGuid().ToString());

            Assert.Equal(1, this.db.Exercises.Where(s=>s.TypeOfGender == Gender.Woman).Count());
        }

        [Fact]
        public async Task CreateMansExercisesAsyncCorrectly()
        {
            var input = new CreateExercisesInputModel
            {
                Title = "das",
                Content = "ads",
                Gender = Gender.Man.ToString(),
                Video = "https://google.com",
                UserID = "6b44d6d8-9bb2-4469-a227-a039c5751700",
            };

            var input2 = new CreateExercisesInputModel
            {
                Title = "das",
                Content = "ads",
                Gender = Gender.Woman.ToString(),
                Video = "https://google.com",
                UserID = "6b44d6d8-9bb2-4469-a227-a039c5751700",
            };

            await this.service.CreateMansExercisesAsync(input, Guid.NewGuid().ToString());
            await this.service.CreateWomansExercisesAsync(input2, Guid.NewGuid().ToString());

            Assert.Equal(1, this.db.Exercises.Where(s => s.TypeOfGender == Gender.Man).Count());
        }

        [Fact]
        public async Task GetCountCorrectlyTest()
        {
            var input = new CreateExercisesInputModel
            {
                Title = "das",
                Content = "ads",
                Gender = Gender.Man.ToString(),
                Video = "https://google.com",
                UserID = "6b44d6d8-9bb2-4469-a227-a039c5751700",
            };

            await this.service.CreateMansExercisesAsync(input, Guid.NewGuid().ToString());

            var count = this.service.GetCount();

            Assert.Equal(1, count);
        }

        [Fact]
        public async Task DeleteExerciseCorrectlyTest()
        {
            var exercise = new Exercise
            {
                Id = "6b44d6d8-9bb2-4469-a227-a039c5751700",
                Title = "ssss",
                Content = "sss",
                TypeOfGender = Gender.Man,
                Video = "sss",
                UserID = "6b44d6d8-9bb2-4469-a227-a039c5751700",
            };
            this.db.Exercises.Add(exercise);
            await this.db.SaveChangesAsync();

            await this.service.DeleteExercisesAsync(exercise.Id);
            Assert.Equal(0, this.service.GetCount());
        }

        [Fact]
        public async Task GetDietByIdAsyncCorrectlyTest()
        {
            var data = new List<Exercise>()
            {
                new Exercise{ Id = "6b44d6d8-9bb2-4469-a227-a039c5751700", Title = "sad", Content = "adsa", TypeOfGender = Gender.Man, Video = "asad", UserID = "6b44d6d8-9bb2-4469-a227-a039c5751987"},
                new Exercise{ Id = "6b44d6d8-9bb2-4469-a527-a039c5751700", Title = "sad", Content = "adsa", TypeOfGender = Gender.Man, Video = "asad", UserID = "6b44d6d8-9bb2-4b69-a227-a039c5751987"},
            };
            this.db.Exercises.AddRange(data);
            await this.db.SaveChangesAsync();

            var exercise = await this.service.GetExercisesByIdAsync("6b44d6d8-9bb2-4469-a227-a039c5751700");
            Assert.Equal("sad", exercise.Title);
        }

        [Fact]
        public void GetDietsByUserCorrectlyTest()
        {
            var data = new Exercise
            {
                Id = "6b44d6d8-9bb2-4469-a227-a039c5751700",
                Title = "sad",
                Content = "adsa",
                Video = "oooo",
                UserID = "6b44d699-9bb2-4469-a227-a039c5751700",
            };
            this.db.Exercises.Add(data);
            this.db.SaveChangesAsync();

            var exercises = this.service.GetExersisesByUser("6b44d699-9bb2-4469-a227-a039c5751700");
            Assert.NotEmpty(exercises);
        }

        [Fact]
        public async Task UpdateTitleCorrectlyTest()
        {
            var data = new Exercise
            {
                Id = "6b44d6d8-9bb2-4469-a227-a039c5751700",
                Title = "sad",
                Content = "adsa",
                Video = "oooo",
                TypeOfGender = Gender.Woman,
                UserID = "6b44d699-9bb2-4469-a227-a039c5751700",
            };

            this.db.Exercises.Add(data);
            await this.db.SaveChangesAsync();

            var editModel = new EditExercisetInputModel
            {
                Title = "Title",
                Content = "adsa",
                Video = "oooo",
                Gender = Gender.Woman.ToString(),
                ExerciseId = data.Id,
            };

            await this.service.Update(editModel);
            Assert.Equal("Title", data.Title);
        }

        [Fact]
        public async Task UpdateContentCorrectlyTest()
        {
            var data = new Exercise
            {
                Id = "6b44d6d8-9bb2-4469-a227-a039c5751700",
                Title = "Title",
                Content = "bby",
                Video = "oooo",
                TypeOfGender = Gender.Woman,
                UserID = "6b44d699-9bb2-4469-a227-a039c5751700",
            };

            this.db.Exercises.Add(data);
            await this.db.SaveChangesAsync();

            var editModel = new EditExercisetInputModel
            {
                Title = "Title",
                Content = "hei",
                Video = "oooo",
                Gender = Gender.Woman.ToString(),
                ExerciseId = data.Id,
            };

            await this.service.Update(editModel);
            Assert.Equal("hei", data.Content);
        }

        [Fact]
        public async Task UpdateVideoCorrectlyTest()
        {
            var data = new Exercise
            {
                Id = "6b44d6d8-9bb2-4469-a227-a039c5751700",
                Title = "Title",
                Content = "hei",
                Video = "oooo",
                TypeOfGender = Gender.Woman,
                UserID = "6b44d699-9bb2-4469-a227-a039c5751700",
            };

            this.db.Exercises.Add(data);
            await this.db.SaveChangesAsync();

            var editModel = new EditExercisetInputModel
            {
                Title = "Title",
                Content = "hei",
                Video = "kkkk",
                Gender = Gender.Woman.ToString(),
                ExerciseId = data.Id,
            };

            await this.service.Update(editModel);
            Assert.Equal("kkkk", data.Video);
        }

        [Fact]
        public async Task UpdateGenderCorrectlyTest()
        {
            var data = new Exercise
            {
                Id = "6b44d6d8-9bb2-4469-a227-a039c5751700",
                Title = "Title",
                Content = "hei",
                Video = "oooo",
                TypeOfGender = Gender.Woman,
                UserID = "6b44d699-9bb2-4469-a227-a039c5751700",
            };

            this.db.Exercises.Add(data);
            await this.db.SaveChangesAsync();

            var editModel = new EditExercisetInputModel
            {
                Title = "Title",
                Content = "hei",
                Video = "oooo",
                Gender = Gender.Man.ToString(),
                ExerciseId = data.Id,
            };

            await this.service.Update(editModel);
            Assert.Equal(Gender.Man, data.TypeOfGender);
        }

        [Fact]
        public async Task AddCommentToPostCorrectlyTest()
        {
            var data1 = new Exercise
            {
                Id = "6b44d6d8-9bb2-4469-a227-a039c5751700",
                Title = "Title",
                Content = "hei",
                Video = "oooo",
                TypeOfGender = Gender.Man,
                UserID = "6b44d699-9bb2-4469-a227-a039c5751700",
            };
            var comment = new Comment
            {
                Id = "sadaa",
                Content = "sss",
                PostId = "6b44d6d8-9bb2-4469-a227-a039c5751788",
            };

            this.db.Exercises.Add(data1);
            await this.db.SaveChangesAsync();

            await this.service.AddCommenToExercise(await this.service.GetExercisesByIdAsync("6b44d6d8-9bb2-4469-a227-a039c5751700"), comment);

            Assert.Equal(1, data1.Commetns.Count);
        }

        //[Fact]
        //public void GetAllForMansCorrectly()
        //{
        //    var data1 = new Exercise
        //    {
        //        Id = "6b44d6d8-9bb2-4469-a227-a039c6251700",
        //        Title = "Title",
        //        Content = "hei",
        //        Video = "https://www.youtube.com/watch?v=PnuT3TKS2nY",
        //        TypeOfGender = Gender.Man,
        //        UserID = "6b44d699-9bb2-4469-a227-a039c5751700",
        //    };
        //    var data2 = new Exercise
        //    {
        //        Id = "6b44d6d8-9bb2-4469-a227-a039c5851700",
        //        Title = "Title",
        //        Content = "hei",
        //        Video = "https://www.youtube.com/watch?v=PnuTsTKS2nY",
        //        TypeOfGender = Gender.Woman,
        //        UserID = "6b44d699-9bb2-4469-a227-a039c5751700",
        //    };
        //    var data3 = new Exercise
        //    {
        //        Id = "6b44d6d8-9bb2-4469-a227-a039c5351700",
        //        Title = "Title",
        //        Content = "hei",
        //        Video = "https://www.youtube.com/watch?v=NnuT3TKS2nY",
        //        TypeOfGender = Gender.Man,
        //        UserID = "6b44d699-9bb2-4469-a227-a039c5751700",
        //    };
        //    this.db.Exercises.Add(data1);
        //    this.db.Exercises.Add(data2);
        //    this.db.Exercises.Add(data3);
        //    this.db.SaveChanges();


        //    var exercises = this.service.GetAllForMans<ExerciseViewModel>(null, 0);
        //    Assert.Equal(2, exercises.Count());
        //}

        //[Fact]
        //public void GetAllForWommansCorrectly()
        //{
        //    var data1 = new Exercise
        //    {
        //        Id = "6b44d6d8-9bb2-4469-a227-a039c6251700",
        //        Title = "Title",
        //        Content = "hei",
        //        Video = "https://www.youtube.com/watch?v=PnuT3TKS2nY",
        //        TypeOfGender = Gender.Man,
        //        UserID = "6b44d699-9bb2-4469-a227-a039c5751700",
        //    };
        //    var data2 = new Exercise
        //    {
        //        Id = "6b44d6d8-9bb2-4469-a227-a039c5851700",
        //        Title = "Title",
        //        Content = "hei",
        //        Video = "https://www.youtube.com/watch?v=PnuTsTKS2nY",
        //        TypeOfGender = Gender.Woman,
        //        UserID = "6b44d699-9bb2-4469-a227-a039c5751700",
        //    };
        //    var data3 = new Exercise
        //    {
        //        Id = "6b44d6d8-9bb2-4469-a227-a039c5351700",
        //        Title = "Title",
        //        Content = "hevvi",
        //        Video = "https://www.youtube.com/watch?v=NnuT3TKS2nY",
        //        TypeOfGender = Gender.Man,
        //        UserID = "6b44d699-9bb2-4469-a227-a039c5751700",
        //    };
        //    this.db.Exercises.Add(data1);
        //    this.db.Exercises.Add(data2);
        //    this.db.Exercises.Add(data3);
        //    this.db.SaveChanges();

        //    var exercises = this.service.GetAllForWomens<ExerciseViewModel>(null, 0);
        //    Assert.Equal(2, exercises.Count());
        //}
    }
}
