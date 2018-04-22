using System;
using Chatter.Domain.Core.Commands;

namespace Chatter.Domain.Categories.Commands
{
    public class UpdateCategoryCommand : Command
    {
        public UpdateCategoryCommand(int id, string name, string description, string image, bool isFeatured, bool active, DateTime created)
        {
            Id = id;
            Name = name;
            Description = description;
            Image = image;
            IsFeatured = isFeatured;
            Active = active;
            Created = created;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool IsFeatured { get; set; }
        public bool Active { get; set; }
        public DateTime Created { get; set; }
    }
}