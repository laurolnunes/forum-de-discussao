using System.Collections.Generic;
using Chatter.Domain.Interfaces;

namespace Chatter.Domain.Topics.Repository
{
    public interface ITopicRepository : IRepository<Topic>
    {
        // Posts
        Post AddPost(Post post);

        void UpdatePost(Post post);

        Post GetPost(int id);
        
        void RemovePost(int id);

        List<Post> GetPostsFromTopic(int topicId);
    }
}