﻿using System.Reflection;
using Autofac;
using AutoMapper;
using Balances.Configuration;
using Balances.GrpcServices;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swisschain.Sdk.Server.Common;

namespace Balances
{
    public sealed class Startup : SwisschainStartup<AppConfig>
    {
        public Startup(IConfiguration configuration)
            : base(configuration)
        {
        }

        protected override void RegisterEndpoints(IEndpointRouteBuilder endpoints)
        {
            base.RegisterEndpoints(endpoints);

            endpoints.MapGrpcService<MonitoringService>();
            endpoints.MapGrpcService<BalancesService>();
        }

        protected override void ConfigureServicesExt(IServiceCollection services)
        {
            services
                .AddAutoMapper(typeof(AutoMapperProfile), typeof(Services.AutoMapperProfile))
                .AddControllersWithViews()
                .AddFluentValidation(options =>
                {
                    ValidatorOptions.CascadeMode = CascadeMode.StopOnFirstFailure;
                    options.RegisterValidatorsFromAssembly(Assembly.GetEntryAssembly());
                });
        }

        protected override void ConfigureContainerExt(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModule(Config.BalancesService.BalancesServiceAddress));
            builder.RegisterModule(new Services.AutofacModule());
        }

        protected override void ConfigureExt(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ApplicationServices.GetRequiredService<AutoMapper.IConfigurationProvider>()
                .AssertConfigurationIsValid();
        }
    }
}
