using APP_SportHealth.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace APP_SportHealth.Application.Interfaces
{
    public interface IActivityRepository
    {
        Task Create(Activity activity);
        Task<Activity?> GetById(Guid id);
        Task<List<Activity>> ListByUser(Guid userId);
    }
}
