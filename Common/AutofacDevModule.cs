using Autofac;
using ExampleProject.DAL.Common;
using ExampleProject.DAL;
using ExampleProject.Repository.Common;
using ExampleProject.Repository;
using AutoMapper.Contrib.Autofac.DependencyInjection;

namespace ExampleProject.Common
{
    public class AutofacDevModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Binds interfaces to specific classes when doing dependeny injection
            builder.RegisterType<CityRepository>().As<ICityRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CityParkRepository>().As<ICityParkRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterAutoMapper(typeof(AutoMapperProfiles).Assembly);
        }
    }
}
