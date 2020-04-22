namespace FitMe.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using FitMe.Data;
    using FitMe.Data.Models;
    using FitMe.Data.Repositories;
    using Xunit;

    public class VotesServiceTests
    {
        private ApplicationDbContext db;
        private VotesService service;

        public VotesServiceTests()
        {
            this.db = BaseServiceTests.CreateContext();

            var votesRepository = new EfRepository<Vote>(this.db);
            this.service = new VotesService(votesRepository);
        }

        [Fact]
        public void GetVotes()
        {
            var input = new Vote
            {
                UserId = "6b44d6d8-9bb2-4469-a227-a039c5751700",
                VoteType = VoteType.UpVote,
                PostId = "7744d6d8-9bb2-4469-a227-a049c5751700",
            };

            var input2 = new Vote
            {
                UserId = "6b44d6d8-9bb2-4469-a227-a038c5751700",
                VoteType = VoteType.UpVote,
                PostId = "7744d6d8-9bb2-4469-a227-a049c5751700",
            };
            this.db.Votes.Add(input);
            this.db.Votes.Add(input2);
            this.db.SaveChanges();

            var count = this.service.GetVotes("7744d6d8-9bb2-4469-a227-a049c5751700");

            Assert.Equal(2, count);
        }

        [Fact]
        public void GetVotesUncorrecly()
        {
            var input = new Vote
            {
                UserId = "6b44d6d8-9bb2-4469-a227-a039c5751700",
                VoteType = VoteType.UpVote,
                PostId = "7744d6d8-9bb2-4469-a227-a049c5751700",
            };

            var input2 = new Vote
            {
                UserId = "6b44d6d8-9bb2-4469-a227-a038c5751700",
                VoteType = VoteType.DownVote,
                PostId = "7744d6d8-9bb2-4469-a227-a049c5751700",
            };
            this.db.Votes.Add(input);
            this.db.Votes.Add(input2);
            this.db.SaveChanges();

            var count = this.service.GetVotes("7744d6d8-9bb2-4469-a227-a049c5751700");

            Assert.Equal(1, count);
        }
    }
}
