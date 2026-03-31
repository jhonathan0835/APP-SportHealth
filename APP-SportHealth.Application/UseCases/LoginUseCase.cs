using System;
using System.Collections.Generic;
using System.Text;
using APP_SportHealth.Application.Interfaces;
using BCrypt.Net;

namespace APP_SportHealth.Application.UseCases
{


    public class LoginUseCase
    {
        private readonly IUserRepository _repo;
        private readonly IJwtService _jwt;

        public LoginUseCase(IUserRepository repo, IJwtService jwt)
        {
            _repo = repo;
            _jwt = jwt;
        }

        public async Task<string?> Execute(string email, string password)
        {
            // 1. Buscar usuario
            var user = await _repo.GetByEmailAsync(email);

            // siguientes dos lineas para crear hash de usuarios
            //var hash = BCrypt.Net.BCrypt.HashPassword("123456");
            //Console.WriteLine(hash);


            if (user == null)
                return null;
            
            // 2. Validar contraseña
            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return null;

            // 3. Generar token
            var token = _jwt.GenerateToken(user.Id.ToString(), user.Email);

            return token;
        }
    }
}

