using APP_SportHealth.Application.Interfaces;
using APP_SportHealth.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace APP_SportHealth.Application.UseCases
{
    public class CreateUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public CreateUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Execute(string name, string email, string password)
        {
            // 🔐 Hashear password
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User(name, email, passwordHash);

            await _userRepository.Create(user);
        }
    }
}
