using AutoMapper;
using Chatter.Application.Interfaces;
using Chatter.Application.Services;
using Chatter.Domain.Categories.Commands;
using Chatter.Domain.Categories.CommandsHandler;
using Chatter.Domain.Categories.Events;
using Chatter.Domain.Categories.EventsHandler;
using Chatter.Domain.Categories.Repository;
using Chatter.Domain.Core.Bus;
using Chatter.Domain.Core.Events;
using Chatter.Domain.Core.Notifications;
using Chatter.Domain.Interfaces;
using Chatter.Domain.Log;
using Chatter.Domain.Topics.Commands;
using Chatter.Domain.Topics.CommandsHandlers;
using Chatter.Domain.Topics.Events;
using Chatter.Domain.Topics.EventsHandlers;
using Chatter.Domain.Topics.Repository;
using Chatter.Domain.Users.Commands;
using Chatter.Domain.Users.CommandsHandler;
using Chatter.Domain.Users.Events;
using Chatter.Domain.Users.EventsHandler;
using Chatter.Domain.Users.Repository;
using Chatter.Infra.CrossCutting.AspNetFilters;
using Chatter.Infra.CrossCutting.Bus;
using Chatter.Infra.CrossCutting.Identity.Models;
using Chatter.Infra.CrossCutting.Identity.Services;
using Chatter.Infra.Data.Context;
using Chatter.Infra.Data.Repository;
using Chatter.Infra.Data.UoW;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Chatter.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootstrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASPNET
            services.AddScoped<IUser, AspNetUser>();
            services.AddTransient<IEmailSender, EmailSender>();

            // Application
            services.AddSingleton(Mapper.Configuration);
            services.AddScoped<IMapper>(map => new Mapper(map.GetRequiredService<IConfigurationProvider>(), map.GetService));

            services.AddScoped<ITopicApplicationService, TopicApplicationService>();
            services.AddScoped<ICategoryApplicationService, CategoryApplicationService>();
            services.AddScoped<IUserApplicationService, UserApplicationService>();
            services.AddScoped<ILogApplicationService, LogApplicationService>();

            
            // Domain - Commands
            services.AddScoped<IHandler<AddPostCommand>, TopicCommandHandler>();
            services.AddScoped<IHandler<AddTopicCommand>, TopicCommandHandler>();
            services.AddScoped<IHandler<RemovePostCommand>, TopicCommandHandler>();
            services.AddScoped<IHandler<RemoveTopicCommand>, TopicCommandHandler>();
            services.AddScoped<IHandler<UpdateTopicCommand>, TopicCommandHandler>();

            services.AddScoped<IHandler<AddCategoryCommand>, CategoryCommandHandler>();
            services.AddScoped<IHandler<RemoveCategoryCommand>, CategoryCommandHandler>();
            services.AddScoped<IHandler<UpdateCategoryCommand>, CategoryCommandHandler>();

            services.AddScoped<IHandler<AddUserCommand>, UserCommandHandler>();
            
            // Domain - Events
            services.AddScoped<IDomainNotificationHandler<DomainNotification>, DomainNotificationHandler>();

            services.AddScoped<IHandler<AddedPostEvent>, TopicEventHandler>();
            services.AddScoped<IHandler<AddedTopicEvent>, TopicEventHandler>();
            services.AddScoped<IHandler<RemovedPostEvent>, TopicEventHandler>();
            services.AddScoped<IHandler<RemovedTopicEvent>, TopicEventHandler>();
            services.AddScoped<IHandler<UpdatedTopicEvent>, TopicEventHandler>();

            services.AddScoped<IHandler<AddedCategoryEvent>, CategoryEventHandler>();
            services.AddScoped<IHandler<RemovedCategoryEvent>, CategoryEventHandler>();
            services.AddScoped<IHandler<UpdatedCategoryEvent>, CategoryEventHandler>();

            services.AddScoped<IHandler<AddedUserEvent>, UserEventHandler>();
            
            // Infra - Data
            services.AddScoped<ITopicRepository, TopicRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ChatterContext>();

            // Infra - Bus
            services.AddScoped<IBus, InMemoryBus>();

            // Infra - Filtros
            services.AddScoped<ILogger<GlobalExceptionHandlingFilter>, Logger<GlobalExceptionHandlingFilter>>();
            services.AddScoped<GlobalExceptionHandlingFilter>();
        }
    }
}