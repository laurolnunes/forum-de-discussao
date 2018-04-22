using Chatter.Domain.CommandsHandler;
using Chatter.Domain.Core.Bus;
using Chatter.Domain.Core.Events;
using Chatter.Domain.Core.Notifications;
using Chatter.Domain.Interfaces;
using Chatter.Domain.Topics.Commands;
using Chatter.Domain.Topics.Events;
using Chatter.Domain.Topics.Repository;
using FluentValidation.Results;

namespace Chatter.Domain.Topics.CommandsHandlers
{
    public class TopicCommandHandler : CommandHandler,
        IHandler<AddTopicCommand>,
        IHandler<UpdateTopicCommand>,
        IHandler<RemoveTopicCommand>,
        IHandler<AddPostCommand>,
        IHandler<RemovePostCommand>
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IBus _bus;
        private readonly IUser _user;

        public TopicCommandHandler(ITopicRepository topicRepository, IUnitOfWork uow, IBus bus, IDomainNotificationHandler<DomainNotification> notification, IUser user) : base (uow, bus, notification)
        {
            _topicRepository = topicRepository;
            _bus = bus;
            _user = user;
        }

        // Topic
        public void Handle(AddTopicCommand message)
        {
            var topic = new Topic(message.UserId, message.CategoryId, message.Title, message.Description);

            if (!topic.IsValid())
            {
                NotifyErrors(topic.ValidationResult);
                return;
            }
            // Validar se o usuario pode criar tópico

            _topicRepository.Add(topic);
            if (Commit())
            {
                // Notificar successo
                _bus.RaiseEvent(new AddedTopicEvent(topic.Id, topic.UserId, topic.CategoryId, topic.Title, topic.Description, topic.Created, topic.Active, topic.Removed));
            }
        }
        
        public void Handle(UpdateTopicCommand message)
        {
            var currentTopic = _topicRepository.Get(message.Id);
            if (currentTopic.UserId != _user.GetUserId())
            {
                currentTopic.ValidationResult.Errors.Add(new ValidationFailure("UserId", "Acesso negado."));
                NotifyErrors(currentTopic.ValidationResult);
                return;
            }

            var topic = Topic.TopicFactory.FullTopic(message.Id, message.UserId, message.CategoryId, message.Title, message.Description, message.Created);
            if (!topic.IsValid())
            {
                NotifyErrors(topic.ValidationResult);
                return;
            }
            _topicRepository.Update(topic);
            if (Commit())
            {
                _bus.RaiseEvent(new UpdatedTopicEvent(topic.Id, topic.UserId, topic.CategoryId, topic.Title, topic.Description, topic.Created));
            }
        }

        public void Handle(RemoveTopicCommand message)
        {
            var currentTopic = _topicRepository.Get(message.Id);
            if (currentTopic == null) return;
            if (currentTopic.UserId != _user.GetUserId())
            {
                currentTopic.ValidationResult.Errors.Add(new ValidationFailure("UserId", "Acesso negado."));
                NotifyErrors(currentTopic.ValidationResult);
                return;
            }

            currentTopic.SetRemoved();
            _topicRepository.Update(currentTopic);

            var posts = _topicRepository.GetPostsFromTopic(message.Id);
            foreach (var post in posts)
            {
                var currentPost = _topicRepository.GetPost(post.Id);
                currentPost.SetRemoved();
                
                _topicRepository.UpdatePost(currentPost);
            }

            if (Commit())
            {
                _bus.RaiseEvent(new RemovedTopicEvent(currentTopic.Id, currentTopic.UserId, currentTopic.CategoryId, currentTopic.Title, currentTopic.Description, currentTopic.Created, currentTopic.Active, currentTopic.Removed));
            }
        }
        
        // Post
        public void Handle(AddPostCommand message)
        {
            var post = new Post(message.TopicId, message.UserId, message.Text);
            if (!post.IsValid())
            {
                NotifyErrors(post.ValidationResult);
                return;
            }

            _topicRepository.AddPost(post);
            if (Commit())
            {
                _bus.RaiseEvent(new AddedPostEvent(post.Id, post.UserId, post.TopicId, post.Text, post.Created));
            }
        }

        public void Handle(RemovePostCommand message)
        {
            var currentPost = _topicRepository.GetPost(message.Id);
            if (currentPost == null) return;
            if (currentPost.UserId != _user.GetUserId())
            {
                currentPost.ValidationResult.Errors.Add(new ValidationFailure("UserId", "Acesso negado."));
                NotifyErrors(currentPost.ValidationResult);
                return;
            }
            
            currentPost.SetRemoved();

            _topicRepository.UpdatePost(currentPost);
            if (Commit())
            {
                _bus.RaiseEvent(new RemovedPostEvent(message.Id));
            }
        }
    }
}