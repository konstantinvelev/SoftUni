namespace FitMe.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using FitMe.Data.Common.Repositories;
    using FitMe.Data.Models;

    public class VotesService : IVotesService
    {
        private readonly IRepository<Vote> votesRepository;

        public VotesService(IRepository<Vote> votesRepository)
        {
            this.votesRepository = votesRepository;
        }

        public int GetVotes(string postId)
        {
            var votes = this.votesRepository.All()
                .Where(x => x.PostId == postId).Sum(x => (int)x.VoteType);
            return votes;
        }

        public async Task VoteAsync(string postId, string userId, bool isUpVote)
        {
            var vote = this.votesRepository.All()
                .FirstOrDefault(x => x.PostId == postId && x.UserId == userId);
            if (vote != null)
            {
                vote.VoteType = isUpVote ? VoteType.UpVote : VoteType.DownVote;
            }
            else
            {
                vote = new Vote
                {
                    PostId = postId,
                    UserId = userId,
                    VoteType = isUpVote ? VoteType.UpVote : VoteType.DownVote,
                };

                await this.votesRepository.AddAsync(vote);
            }

            await this.votesRepository.SaveChangesAsync();
        }
    }
}

