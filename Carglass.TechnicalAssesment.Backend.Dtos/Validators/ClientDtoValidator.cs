using Carglass.TechnicalAssessment.Backend.Dtos;
using FluentValidation;

public class ClientDtoValidator : AbstractValidator<ClientDto>
{
    public ClientDtoValidator()
    {
        RuleFor(x => x.GivenName)
            .NotEmpty().WithMessage("El nombre no puede estar vacío.");

        RuleFor(x => x.FamilyName1)
            .NotEmpty().WithMessage("El apellido no puede estar vacío.");

        RuleFor(x => x.DocType)
            .NotEmpty().WithMessage("El tipo de documento no puede estar vacío.");

        RuleFor(x => x.DocNum)
            .NotEmpty().WithMessage("El número de documento no puede estar vacío.");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("El teléfono no puede estar vacío.")
            .Unless(IsPutRequest);

        RuleFor(x => x.Email)
            .NotEmpty().EmailAddress().WithMessage("El correo electrónico no es válido.")
            .Unless(IsPutRequest);

        When(IsPutRequest, () =>
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El ID debe ser mayor que 0 para actualizaciones.");
        });
    }

    private bool IsPutRequest(ClientDto dto)
    {
        return dto.Id > 0;
    }
}
