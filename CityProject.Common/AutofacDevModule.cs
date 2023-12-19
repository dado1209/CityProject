using CityProject.DAL;
using CityProject.Repository;
using CityProject.Repository.Common;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using Autofac;

namespace CityProject.Common
{
    public class AutofacDevModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Binds interfaces to specific classes when doing dependeny injection
            builder.RegisterType<CityRepository>().As<ICityRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CityParkRepository>().As<ICityParkRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<DAL.Common.IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterAutoMapper(typeof(AutoMapperProfiles).Assembly);
        }
    }
}
