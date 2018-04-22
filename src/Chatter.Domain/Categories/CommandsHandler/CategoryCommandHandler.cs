using Chatter.Domain.Categories.Commands;
using Chatter.Domain.Categories.Events;
using Chatter.Domain.Categories.Repository;
using Chatter.Domain.CommandsHandler;
using Chatter.Domain.Core.Bus;
using Chatter.Domain.Core.Events;
using Chatter.Domain.Core.Notifications;
using Chatter.Domain.Interfaces;

namespace Chatter.Domain.Categories.CommandsHandler
{
    public class CategoryCommandHandler : CommandHandler,
        IHandler<AddCategoryCommand>,
        IHandler<UpdateCategoryCommand>,
        IHandler<RemoveCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBus _bus;

        public CategoryCommandHandler(IUnitOfWork uow, IBus bus, IDomainNotificationHandler<DomainNotification> notification, ICategoryRepository categoryRepository) : base(uow, bus, notification)
        {
            _categoryRepository = categoryRepository;
            _bus = bus;
        }

        // Category
        public void Handle(AddCategoryCommand message)
        {
            var category = new Category(message.Name, message.Description, message.Image, message.IsFeatured, message.Active);
            if (!category.IsValid())
            {
                NotifyErrors(category.ValidationResult);
                return;
            }

            _categoryRepository.Add(category);
            if (Commit())
            {
                _bus.RaiseEvent(new AddedCategoryEvent(category.Id, category.Name, category.Description, category.Image, category.IsFeatured, category.Active, category.Created));
            }
        }

        public void Handle(UpdateCategoryCommand message)
        {
            var category = Category.CategoryFactory.FullCategory(message.Id, message.Name, message.Description, message.Image, message.IsFeatured, message.Active, message.Created);
            if (!category.IsValid())
            {
                NotifyErrors(category.ValidationResult);
                return;
            }
            _categoryRepository.Update(category);
            if (Commit())
            {
                _bus.RaiseEvent(new UpdatedCategoryEvent(category.Id, category.Name, category.Description, category.Image, category.IsFeatured, category.Active, category.Created));
            }
        }

        public void Handle(RemoveCategoryCommand message)
        {
            _categoryRepository.Remove(message.Id);
            if (Commit())
            {
                _bus.RaiseEvent(new RemovedCategoryEvent(message.Id));
            }
        }
    }
}