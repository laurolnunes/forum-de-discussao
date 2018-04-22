using System;
using System.Collections.Generic;
using Chatter.Domain.Core.Models;
using Chatter.Domain.Topics;
using FluentValidation;

namespace Chatter.Domain.Users
{
    public class User : Entity<User>
    {
        public User(string name, string email, Guid identityId)
        {
            Name = name;
            Email = email;
            IdentityId = identityId;
        }

        public User()
        {
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateTime Created { get; private set; }
        public bool Active { get; private set; }
        public bool Removed { get; private set; }
        public Guid IdentityId { get; set; }

        // Propriedade de navegação do EF
        public virtual ICollection<Topic> Topics { get; set; }
        public virtual ICollection<Post> Posts { get; set; }

        public void SetRemoved()
        {
            Removed = true;
        }

        public override bool IsValid()
        {
            RuleForName();
            RuleForEmail();

            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }

        private void RuleForName()
        {
            RuleFor(t => t.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 200).WithMessage("Name length must be between 2 and 200 characters.");
        }

        private void RuleForEmail()
        {
            RuleFor(t => t.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid e-mail")
                .Length(2, 150).WithMessage("E-mail length must be between 2 and 150 characters.");
        }
    }
}