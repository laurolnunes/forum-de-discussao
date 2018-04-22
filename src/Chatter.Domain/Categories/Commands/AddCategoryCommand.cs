using Chatter.Domain.Core.Commands;

namespace Chatter.Domain.Categories.Commands
{
    public class AddCategoryCommand : Command
    {
        public AddCategoryCommand(string name, string description, string image, bool isFeatured, bool active)
        {
            Name = name;
            Description = description;
            Image = image;
            IsFeatured = isFeatured;
            Active = active;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public bool IsFeatured { get; set; }
        public bool Active { get; set; }
    }
}