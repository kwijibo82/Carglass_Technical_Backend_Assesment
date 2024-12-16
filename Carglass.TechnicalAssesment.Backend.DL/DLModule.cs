using Autofac;
using Carglass.TechnicalAssessment.Backend.DL.Repositories;
using Carglass.TechnicalAssessment.Backend.DL.Interfaces;

namespace Carglass.TechnicalAssessment.Backend.DL
{
    public class DLModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ApplicationDbContext>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.RegisterType<ClientRepository>()
                .As<IClientRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<ProductoRepository>()
                .As<IProductoRepository>()
                .InstancePerLifetimeScope();
        }
    }
}
