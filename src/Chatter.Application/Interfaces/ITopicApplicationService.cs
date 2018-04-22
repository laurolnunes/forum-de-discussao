using System;
using System.Collections.Generic;
using Chatter.Application.ViewModels;

namespace Chatter.Application.Interfaces
{
    public interface ITopicApplicationService : IDisposable
    {
        void Add(TopicViewModel topic);

        void Remove(int id);

        TopicViewModel Get(int id);

        List<TopicViewModel> GetAll(bool activeOnly = false);

        List<TopicViewModel> GetByCategory(int categoryId);

        List<TopicViewModel> GetByUser(int userId);

        void Update(TopicViewModel topic);

        DiscussionViewModel GetDiscussion(int topicId);

        void AddPost(PostViewModel post);
    }
}