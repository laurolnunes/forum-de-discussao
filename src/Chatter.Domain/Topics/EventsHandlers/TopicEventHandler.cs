using Chatter.Domain.Categories.Events;
using Chatter.Domain.Core.Events;
using Chatter.Domain.Topics.Events;

namespace Chatter.Domain.Topics.EventsHandlers
{
    public class TopicEventHandler : 
        IHandler<AddedTopicEvent>,
        IHandler<RemovedTopicEvent>,
        IHandler<AddedPostEvent>,
        IHandler<RemovedPostEvent>,
        IHandler<UpdatedTopicEvent>
    {
        public void Handle(AddedTopicEvent message)
        {
            // Ação realizada após a inclusão do registro (e-mail ou log, etc)
        }

        public void Handle(RemovedTopicEvent message)
        {
            // Ação realizada após a inclusão do registro (e-mail ou log, etc)
        }

        public void Handle(AddedCategoryEvent message)
        {
            // Ação realizada após a inclusão do registro (e-mail ou log, etc)
        }

        public void Handle(UpdatedCategoryEvent message)
        {
            // Ação realizada após a inclusão do registro (e-mail ou log, etc)
        }

        public void Handle(RemovedCategoryEvent message)
        {
            // Ação realizada após a inclusão do registro (e-mail ou log, etc)
        }

        public void Handle(AddedPostEvent message)
        {
            // Ação realizada após a inclusão do registro (e-mail ou log, etc)
        }

        public void Handle(RemovedPostEvent message)
        {
            // Ação realizada após a inclusão do registro (e-mail ou log, etc)
        }

        public void Handle(UpdatedTopicEvent message)
        {
            // Ação realizada após a inclusão do registro (e-mail ou log, etc)
        }
    }
}