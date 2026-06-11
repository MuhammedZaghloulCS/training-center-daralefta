using Infrastructure.Abstractions.IRepositories.ISysRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Abstractions.IRepositories
{
    public interface IFeedbackRepository :IGenericRepository<Domain.Entities.Feedback,int>
    {
    }
}
