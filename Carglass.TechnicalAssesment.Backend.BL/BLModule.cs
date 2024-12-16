using Autofac;
using AutoMapper;
using Carglass.TechnicalAssessment.Backend.BL.Clients;
using Carglass.TechnicalAssessment.Backend.BL.Interfaces;
using Carglass.TechnicalAssessment.Backend.BL.Productos;

namespace Carglass.TechnicalAssessment.Backend.BL
{
    public class BLModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ClientService>()
                .As<IClientService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ProductoService>()
                .As<IProductoService>()
                .InstancePerLifetimeScope();

            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            })).AsSelf().SingleInstance();

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper())
                .As<IMapper>()
                .InstancePerLifetimeScope();
        }
    }
}
