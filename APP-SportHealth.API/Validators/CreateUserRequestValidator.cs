namespace APP_SportHealth.API.Validators
{
    using APP_SportHealth.API.DTO;
    using APP_SportHealth.Application.Interfaces;
    using APP_SportHealth.Infrastructure;
    using FluentValidation;

    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        private readonly IUserRepository _userRepository;
        public CreateUserRequestValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            // 🔥 Name
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .MinimumLength(3).WithMessage("El nombre debe tener al menos 3 caracteres");

            // 🔥 Email
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop) // 🔥 optimización
                .NotEmpty().WithMessage("El email es obligatorio")
                .EmailAddress().WithMessage("El email no es válido")
                .MustAsync(BeUniqueEmail).WithMessage("El email ya está registrado");

            // 🔥 Password
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria")
                .MinimumLength(6).WithMessage("La contraseña debe tener mínimo 6 caracteres");
        }

        private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            var exists = await _userRepository.ExistsByEmail(email);
            return !exists;
        }


    }
}
