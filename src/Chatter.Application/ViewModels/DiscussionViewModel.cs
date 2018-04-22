using System.Collections.Generic;

namespace Chatter.Application.ViewModels
{
    public class DiscussionViewModel
    {
        public TopicViewModel TopicViewModel { get; set; }

        public List<PostViewModel> PostsViewModel { get; set; }

        public PostViewModel ReplyViewModel { get; set; }


    }
}