using AutoMapper;
using Chatter.Application.ViewModels;
using Chatter.Domain.Categories;
using Chatter.Domain.Categories.Commands;
using Chatter.Domain.Log;
using Chatter.Domain.Topics;
using Chatter.Domain.Topics.Commands;
using Chatter.Domain.Users;
using Chatter.Domain.Users.Commands;

namespace Chatter.Application.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(profile =>
            {
                profile.AddProfile(new MappingProfileForCommands());
                profile.AddProfile(new MappingProfileForRequests());
                profile.AddProfile(new MappingProfileForServices());
            });
        }
    }

    public class MappingProfileForRequests : Profile
    {
        public MappingProfileForRequests()
        {
            CreateMap<Category, CategoryViewModel>();
            CreateMap<User, UserViewModel>();
            CreateMap<Topic, TopicViewModel>();
            CreateMap<Post, PostViewModel>();
            CreateMap<Log, LogViewModel>();
        }
    }

    public class MappingProfileForServices : Profile
    {
        public MappingProfileForServices()
        {
            CreateMap<LogViewModel, Log>();
        }
    }

    public class MappingProfileForCommands : Profile
    {
        public MappingProfileForCommands()
        {
            // Category
            CreateMap<CategoryViewModel, AddCategoryCommand>()
                .ConstructUsing(c => new AddCategoryCommand(c.Name, c.Description, c.Image, c.IsFeatured, c.Active));

            CreateMap<CategoryViewModel, RemoveCategoryCommand>()
                .ConstructUsing(c => new RemoveCategoryCommand(c.Id));

            CreateMap<CategoryViewModel, UpdateCategoryCommand>()
                .ConstructUsing(c => new UpdateCategoryCommand(c.Id, c.Name, c.Description, c.Image, c.IsFeatured, c.Active, c.Created));
            
            // Topic
            CreateMap<TopicViewModel, AddTopicCommand>()
                .ConstructUsing(t => new AddTopicCommand(t.UserId, t.CategoryId, t.Title, t.Description));

            CreateMap<TopicViewModel, RemoveTopicCommand>()
                .ConstructUsing(t => new RemoveTopicCommand(t.Id));

            CreateMap<TopicViewModel, UpdateTopicCommand>()
                .ConstructUsing(t => new UpdateTopicCommand(t.Id, t.UserId, t.CategoryId, t.Title, t.Description, t.Created));
            
            // Post
            CreateMap<PostViewModel, AddPostCommand>()
                .ConstructUsing(p => new AddPostCommand(p.UserId, p.TopicId, p.Text));

            CreateMap<PostViewModel, RemovePostCommand>()
                .ConstructUsing(p => new RemovePostCommand(p.Id));

            // User
            CreateMap<UserViewModel, AddUserCommand>()
                .ConstructUsing(u => new AddUserCommand(u.Name, u.Email, u.IdentityId));
        }
    }
}