using APP_SportHealth.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace APP_SportHealth.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task AddAsync(User user);
        Task<bool> ExistsByEmail(string email);
        Task Create(User user);
    }
}
