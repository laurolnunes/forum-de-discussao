using Chatter.Domain.Categories.Events;
using Chatter.Domain.Core.Events;

namespace Chatter.Domain.Categories.EventsHandler
{
    public class CategoryEventHandler :
        IHandler<AddedCategoryEvent>,
        IHandler<UpdatedCategoryEvent>,
        IHandler<RemovedCategoryEvent>
    {
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
    }
}