using Carglass.TechnicalAssessment.Backend.Dtos;
using FluentValidation;

public class CreateProductoDtoValidator : AbstractValidator<CreateProductoDto>
{
    public CreateProductoDtoValidator()
    {
        RuleFor(x => x.ProductName)
            .NotEmpty()
            .WithMessage("El nombre del producto no puede estar vacío.");
    }
}
