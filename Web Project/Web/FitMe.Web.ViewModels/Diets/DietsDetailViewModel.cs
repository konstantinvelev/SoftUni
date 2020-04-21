namespace FitMe.Web.ViewModels.Diets
{
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using FitMe.Data.Models;
    using FitMe.Services.Mapping;
    using FitMe.Web.ViewModels.Comments;

    public class DietsDetailViewModel : IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string UserUserName { get; set; }

        public string CreatedOn { get; set; }

        public int VotesCount { get; set; }

        public string UserUserId { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Diet, DietsDetailViewModel>()
                .ForMember(x => x.VotesCount, options =>
                {
                    options.MapFrom(p => p.Votes.Sum(v => (int)v.VoteType));
                });
        }
    }
}
