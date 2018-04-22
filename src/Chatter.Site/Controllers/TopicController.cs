using System;
using Chatter.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Chatter.Application.ViewModels;
using Chatter.Domain.Core.Notifications;
using Chatter.Domain.Interfaces;
using Chatter.Site.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace Chatter.Site.Controllers
{
    [Route("")]
    public class TopicController : BaseController
    {
        private readonly ITopicApplicationService _applicationService;

        public TopicController(ITopicApplicationService applicationService,
            IDomainNotificationHandler<DomainNotification> notification,
            IUser user) : base(notification, user)
        {
            _applicationService = applicationService;
        }

        [Route("")]
        [Route("topicos")]
        public IActionResult Index()
        {
            
            throw new Exception("Error Custom");
            return View(_applicationService.GetAll());
        }

        [Authorize(Policy = "LoggedAction")]
        [Route("meus-topicos")]
        public IActionResult MyTopics()
        {
            return View(_applicationService.GetByUser(UserId));
        }

        [Route("detalhes/{id:int}")]
        public IActionResult Details(int? id)
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

        [Route("novo-topico")]
        [Authorize(Policy = "LoggedAction")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("novo-topico")]
        [Authorize(Policy = "LoggedAction")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TopicViewModel topicViewModel)
        {
            topicViewModel.UserId = UserId;
            if (!ModelState.IsValid)
                return View(topicViewModel);

            _applicationService.Add(topicViewModel);
            TempData["ServerResult"] = IsValidOperation() ? "success;Topic created successfully!" : "error;Topic was not created. See the notifications for details.";

            return View(topicViewModel);
        }

        [Route("discussao/{id:int}")]
        public IActionResult Discussion(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topicViewModel = _applicationService.GetDiscussion(id.Value);
            if (topicViewModel == null)
            {
                return NotFound();
            }
            return View(topicViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("discussao/{id:int}")]
        [Authorize(Policy = "LoggedAction")]
        public IActionResult Discussion(DiscussionViewModel discussionViewModel)
        {
            var replyViewModel = discussionViewModel.ReplyViewModel;
            replyViewModel.UserId = UserId;

            discussionViewModel = _applicationService.GetDiscussion(discussionViewModel.ReplyViewModel.TopicId);
            discussionViewModel.ReplyViewModel = replyViewModel;

            if (replyViewModel.TopicId == 0)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(discussionViewModel);
            }
            _applicationService.AddPost(discussionViewModel.ReplyViewModel);
            TempData["ServerResult"] = IsValidOperation() ? "success;Post created successfully!" : "error;Post was not created. See the notifications for details.";

            return IsValidOperation() ? (IActionResult)RedirectToAction("Discussion", new { id = discussionViewModel.TopicViewModel.Id }) : View("Discussion", discussionViewModel);
        }

        [Route("editar-topico/{id:int}")]
        [Authorize(Policy = "LoggedAction")]
        public IActionResult Edit(int? id)
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
            if (topicViewModel.UserId != UserId)
            {
                return RedirectToAction("MyTopics");
            }
            return View(topicViewModel);
        }

        [HttpPost]
        [Route("editar-topico/{id:int}")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "LoggedAction")]
        public IActionResult Edit(TopicViewModel topicViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(topicViewModel);
            }

            topicViewModel.UserId = UserId;
            _applicationService.Update(topicViewModel);
            TempData["ServerResult"] = IsValidOperation() ? "success;Topic updated successfully!" : "error;Topic was not updated. See the notifications for details.";
            return View(topicViewModel);
        }

        [Route("excluir/{id:int}")]
        [Authorize(Policy = "LoggedAction")]
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
            if (topicViewModel.UserId != UserId)
            {
                return RedirectToAction("MyTopics");
            }
            return View(topicViewModel);
        }

        [Route("excluir/{id:int}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "LoggedAction")]
        public IActionResult DeleteConfirmed(int id)
        {
            var topicViewModel = _applicationService.Get(id);
            if (topicViewModel == null)
            {
                return NotFound();
            }
            if (topicViewModel.UserId != UserId)
            {
                return RedirectToAction("MyTopics");
            }
            _applicationService.Remove(id);
            return RedirectToAction("Index");
        }
    }
}
