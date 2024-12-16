using FluentValidation;

public class UpdateProductoDtoValidator : AbstractValidator<UpdateProductoDto>
{
    public UpdateProductoDtoValidator()
    {
        RuleFor(x => x.ProductName)
            .NotEmpty()
            .WithMessage("El nombre del producto no puede estar vacío.");
    }
}
