using System;
using System.ComponentModel.DataAnnotations;

namespace Chatter.Application.ViewModels
{
    public class PostViewModel
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public int TopicId { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 2)]
        public string Text { get; set; }

        public DateTime Created { get; set; }

        public bool Removed { get; set; }

        public UserViewModel User { get; set; }
    }
}