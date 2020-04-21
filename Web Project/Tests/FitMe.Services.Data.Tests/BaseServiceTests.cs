namespace FitMe.Services.Data.Tests
{
    using System;

    using FitMe.Data;
    using Microsoft.EntityFrameworkCore;

    public class BaseServiceTests
    {
        public static ApplicationDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var context = new ApplicationDbContext(options);
            return context;
        }
    }
}
