using AutoMapper;
using Chatter.Application.Interfaces;
using Chatter.Application.ViewModels;
using Chatter.Domain.Core.Bus;
using Chatter.Domain.Topics.Commands;
using Chatter.Domain.Topics.Repository;
using System.Collections.Generic;

namespace Chatter.Application.Services
{
    public class TopicApplicationService : ITopicApplicationService
    {
        private readonly IBus _bus;
        private readonly IMapper _mapper;
        private readonly ITopicRepository _topicRepository;

        public TopicApplicationService(IBus bus, IMapper mapper, ITopicRepository topicRepository)
        {
            _bus = bus;
            _mapper = mapper;
            _topicRepository = topicRepository;
        }

        public void Add(TopicViewModel topic)
        {
            var addCommand = _mapper.Map<AddTopicCommand>(topic);
            _bus.SendCommand(addCommand);
        }

        public TopicViewModel Get(int id)
        {
            return _mapper.Map<TopicViewModel>(_topicRepository.Get(id));
        }

        public List<TopicViewModel> GetAll(bool activeOnly = false)
        {
            return _mapper.Map<List<TopicViewModel>>(_topicRepository.GetAll());
        }

        public List<TopicViewModel> GetByCategory(int categoryId)
        {
            return _mapper.Map<List<TopicViewModel>>(_topicRepository.Find(t => t.CategoryId == categoryId));
        }

        public List<TopicViewModel> GetByUser(int userId)
        {
            return _mapper.Map<List<TopicViewModel>>(_topicRepository.Find(t => t.UserId == userId));
        }

        public void Update(TopicViewModel topic)
        {
            var updateCommand = _mapper.Map<UpdateTopicCommand>(topic);
            _bus.SendCommand(updateCommand);
        }

        public DiscussionViewModel GetDiscussion(int topicId)
        {
            var topic = _topicRepository.Get(topicId);
            var posts = _topicRepository.GetPostsFromTopic(topicId);
            var discussion = new DiscussionViewModel
            {
                TopicViewModel = _mapper.Map<TopicViewModel>(topic),
                PostsViewModel = _mapper.Map<List<PostViewModel>>(posts),
                ReplyViewModel = new PostViewModel { TopicId = topicId }
            };
            return discussion;
        }

        public void AddPost(PostViewModel post)
        {
            var addPostCommand = _mapper.Map<AddPostCommand>(post);
            _bus.SendCommand(addPostCommand);
        }

        public void Remove(int id)
        {
            _bus.SendCommand(new RemoveTopicCommand(id));
        }

        public void Dispose()
        {
            _topicRepository.Dispose();
        }
    }
}