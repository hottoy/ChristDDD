﻿using Christ3D.Application.Interfaces;
using Christ3D.Application.Services;
using Christ3D.Domain.CommandHandlers;
using Christ3D.Domain.Commands;
using Christ3D.Domain.Core.Bus;
using Christ3D.Domain.Interfaces;
using Christ3D.Infra.Bus;
using Christ3D.Infra.Data.Context;
using Christ3D.Infra.Data.Repository;
using Christ3D.Infra.Data.UoW;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Christ3D.Infra.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // 注入 Application
            services.AddScoped<IStudentAppService, StudentAppService>();

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();


            // Domain - Commands
            services.AddScoped<IRequestHandler<RegisterStudentCommand, Unit>, StudentCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateStudentCommand, Unit>, StudentCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveStudentCommand, Unit>, StudentCommandHandler>();

            // Domain - Memory
            services.AddSingleton<IMemoryCache>(factory =>
            {
                var cache = new MemoryCache(new MemoryCacheOptions());
                return cache;
            });



            // 注入 Infra - Data
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<StudyContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

        }
    }
}