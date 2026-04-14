namespace APP_SportHealth.API.Validators
{
    using APP_SportHealth.API.DTO;
    using FluentValidation;

    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {

            // 🔥 Name
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .MinimumLength(3).WithMessage("El nombre debe tener al menos 3 caracteres");

            // 🔥 Email
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop) // 🔥 optimización
                .NotEmpty().WithMessage("El email es obligatorio")
                .EmailAddress().WithMessage("El email no es válido");

            // 🔥 Password
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria")
                .MinimumLength(6).WithMessage("La contraseña debe tener mínimo 6 caracteres");
        }




    }
}
