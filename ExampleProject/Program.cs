using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using CityProject.DAL;
using Autofac.Core;
using Sieve.Services;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using CityProject.Repository.Common;
using CityProject.Repository;
using CityProject.Service.Common;
using CityProject.Service.Mappings;
using CityProject.Service;
using CityProject.WebAPI.Mappings;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterType<CityRepository>().As<ICityRepository>().InstancePerLifetimeScope();
        builder.RegisterType<CityParkRepository>().As<ICityParkRepository>().InstancePerLifetimeScope();
        builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
        builder.RegisterType<CityService>().As<ICityService>().InstancePerLifetimeScope();
        builder.RegisterType<CityParkService>().As<ICityParkService>().InstancePerLifetimeScope();
        builder.RegisterAutoMapper(typeof(AutoMapperServiceProfile).Assembly);
        builder.RegisterAutoMapper(typeof(AutoMapperWebAPIProfile).Assembly);
        builder.RegisterType<SieveProcessor>().As<ISieveProcessor>().InstancePerLifetimeScope();
    });
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();

