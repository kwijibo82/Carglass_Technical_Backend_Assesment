using Carglass.TechnicalAssessment.Backend.Dtos;
using FluentValidation;

public class CreateClientDtoValidator : AbstractValidator<ClientDto>
{
    public CreateClientDtoValidator()
    {
        RuleFor(c => c.DocType).NotEmpty().WithMessage("Document type is required.");
        RuleFor(c => c.DocNum).NotEmpty().Length(5, 20).WithMessage("Document number must be between 5 and 20 characters.");
        RuleFor(c => c.GivenName).NotEmpty().WithMessage("Given name is required.");
        RuleFor(c => c.FamilyName1).NotEmpty().WithMessage("Family name is required.");
        RuleFor(c => c.Email).EmailAddress().When(c => !string.IsNullOrEmpty(c.Email))
            .WithMessage("Email format is invalid.");
    }
}
