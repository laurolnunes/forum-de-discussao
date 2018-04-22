using System;
using Chatter.Domain.Core.Models;
using Chatter.Domain.Users;
using FluentValidation;

namespace Chatter.Domain.Topics
{
    public class Post : Entity<Post>
    {
        public Post(int idTopico, int idUsuario, string text)
        {
            TopicId = idTopico;
            UserId = idUsuario;
            Text = text;  
        }

        protected Post() { }
        
        public int UserId { get; private set; }
        public int TopicId { get; private set; }
        public string Text { get; private set; }
        public DateTime Created { get; private set; }
        public bool Removed { get; private set; }

        // Propriedades de navegação do EF
        public virtual User User { get; private set; }
        public virtual Topic Topic { get; private set; }

        public void SetUser(User user)
        {
            User = user;
        }

        public void SetTopic(Topic topic)
        {
            Topic = topic;
        }

        public void SetRemoved()
        {
            Removed = true;
        }

        public override bool IsValid()
        {
            RuleForText();
            RuleForTopic();
            RuleForUser();

            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }

        private void RuleForText()
        {
            RuleFor(t => t.Text)
                .NotEmpty().WithMessage("Text is required.")
                .Length(2, 1000).WithMessage("Text length must be between 2 and 1000 characters.");
        }

        private void RuleForTopic()
        {
            RuleFor(t => t.TopicId)
                .NotEmpty().WithMessage("Topic is required.");
        }

        private void RuleForUser()
        {
            RuleFor(t => t.UserId)
                .NotEmpty().WithMessage("User is required.");
        }
    }
}