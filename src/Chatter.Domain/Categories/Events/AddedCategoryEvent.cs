using System;
using Chatter.Domain.Core.Events;

namespace Chatter.Domain.Categories.Events
{
    public class AddedCategoryEvent : Event
    {
        public AddedCategoryEvent(int id, string name, string description, string image, bool isFeatured, bool active, DateTime created)
        {
            Id = id;
            Name = name;
            Description = description;
            Image = image;
            IsFeatured = isFeatured;
            Active = active;
            Created = created;
            AggregateId = id;
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