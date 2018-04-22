using System;
using System.Collections.Generic;
using Chatter.Domain.Core.Models;
using Chatter.Domain.Topics;
using FluentValidation;

namespace Chatter.Domain.Categories
{
    public class Category : Entity<Category>
    {
        public Category(string name, string description, string image, bool isFeatured, bool active)
        {
            Name = name;
            Description = description;
            Image = image;
            IsFeatured = isFeatured;
            Active = active;
        }

        public Category() { }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }
        public DateTime Created { get; private set; }
        public bool IsFeatured { get; private set; }
        public bool Active { get; private set; }
        public bool Removed { get; private set; }

        // Propriedades de navegação do EF
        public virtual ICollection<Topic> Topics { get; set; }

        public void SetRemoved()
        {
            Removed = true;
        }

        public override bool IsValid()
        {
            RuleForName();
            RuleForDescription();
            RuleForImage();

            ValidationResult = Validate(this);
            return ValidationResult.IsValid;
        }

        private void RuleForName()
        {
            RuleFor(t => t.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(2, 100).WithMessage("Name length must be between 2 and 100 characters.");
        }

        private void RuleForDescription()
        {
            RuleFor(t => t.Description)
                .Length(2, 500).WithMessage("Description length must be between 2 and 500 characters.");
        }

        private void RuleForImage()
        {
            RuleFor(t => t.Image)
                .Length(2, 500).WithMessage("Image length must be between 2 and 500 characters.");
        }

        public static class CategoryFactory
        {
            public static Category FullCategory(int id, string name, string description, string image, bool isFeatured, bool active, DateTime created)
            {
                var category = new Category
                {
                    Id = id,
                    Name = name,
                    Description = description,
                    Image = image,
                    IsFeatured = isFeatured,
                    Active = active,
                    Created = created
                };
                return category;
            }
        }
    }
}