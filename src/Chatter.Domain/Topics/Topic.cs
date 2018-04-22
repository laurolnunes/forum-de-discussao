using System;
using System.Collections.Generic;
using Chatter.Domain.Categories;
using Chatter.Domain.Core.Models;
using Chatter.Domain.Users;
using FluentValidation;

namespace Chatter.Domain.Topics
{
    public class Topic : Entity<Topic>
    {
        public Topic(int userId, int categoryId, string title, string description)
        {
            UserId = userId;
            CategoryId = categoryId;
            Title = title;
            Description = description;
        }

        public Topic() { }
        
        public int UserId { get; private set; }
        public int CategoryId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public DateTime Created { get; private set; }
        public bool Active { get; private set; }
        public bool Removed { get; private set; }
        
        // Propriedade de navegação do EF
        public virtual User User { get; private set; }
        public virtual Category Category { get; private set; }
        public virtual ICollection<Post> Posts { get; private set; }

        public void SetCategory(Category category)
        {
            Category = category;
        }

        public void SetUSer(User user)
        {
            User = user;
        }

        public void SetRemoved()
        {
            Removed = true;
        }

        public void Remove()
        {
            Removed = true;
        }

        public override bool IsValid()
        {
            RuleForTitle();
            RuleForUser();
            RuleForCategory();
            RuleForDescription();

            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }

        private void RuleForTitle()
        {
            RuleFor(t => t.Title)
                .NotEmpty().WithMessage("Title is required.")
                .Length(2, 200).WithMessage("Title length must be between 2 and 200 characters.");
        }

        private void RuleForDescription()
        {
            RuleFor(t => t.Description)
                .Length(2, 1000).WithMessage("Description length must be between 2 and 1000 characters.");
        }

        private void RuleForUser()
        {
            RuleFor(t => t.UserId)
                .NotEqual(0).WithMessage("User is required.");
        }

        private void RuleForCategory()
        {
            RuleFor(t => t.CategoryId)
                .NotEqual(0).WithMessage("Category is required.");
        }

        public static class TopicFactory
        {
            public static Topic FullTopic(int id, int userId, int categoryId, string title, string description, DateTime created)
            {
                var topic = new Topic
                {
                    Id = id,
                    UserId = userId,
                    CategoryId = categoryId,
                    Title = title,
                    Description = description,
                    Created = created
                };
                return topic;
            }
        }
    }
}