using Autofac;
using FluentValidation;
using System.Reflection;

namespace Carglass.TechnicalAssessment.Backend.Dtos;

public class DtosModule : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        RegisterValidators(builder);
    }

    private static void RegisterValidators(ContainerBuilder builder)
    {
        builder.RegisterType<ClientDtoValidator>().As<IValidator<ClientDto>>();
    }
}