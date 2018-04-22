using Chatter.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Chatter.Application.ViewModels;
using Chatter.Domain.Core.Notifications;
using Chatter.Domain.Interfaces;
using Chatter.Site.Controllers.Base;

namespace Chatter.Site.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly ICategoryApplicationService _applicationService;

        public CategoryController(ICategoryApplicationService applicationService, 
            IDomainNotificationHandler<DomainNotification> notifications,
            IUser user) : base(notifications, user)
        {
            _applicationService = applicationService;
        }

         public IActionResult Index()
        {
            return View(_applicationService.GetAll());
        }
        
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var categoryViewModel = _applicationService.Get(id.Value);
            if (categoryViewModel == null)
            {
                return NotFound();
            }
            return View(categoryViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid)
                return View(categoryViewModel);

            _applicationService.Add(categoryViewModel);
            TempData["ServerResult"] = IsValidOperation() ? "success;Category created successfully!" : "error;Category was not created. See the notifications for details.";

            return View(categoryViewModel);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryViewModel = _applicationService.Get(id.Value);
            if (categoryViewModel == null)
            {
                return NotFound();
            }
            return View(categoryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryViewModel categoryViewModel)
        {
            if (categoryViewModel.Id == 0)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(categoryViewModel);
            }
            _applicationService.Update(categoryViewModel);
            TempData["ServerResult"] = IsValidOperation() ? "success;Category updated successfully!" : "error;Category was not updated. See the notifications for details.";
            return View(categoryViewModel);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var topicViewModel = _applicationService.Get(id.Value);
            if (topicViewModel == null)
            {
                return NotFound();
            }
            return View(topicViewModel);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _applicationService.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
