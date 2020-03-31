namespace FitMe.Services.Data
{
    using System.Collections.Generic;

    using FitMe.Data.Models;

    public interface ICommentsService
    {
        IEnumerable<Comment> All();
    }
}
