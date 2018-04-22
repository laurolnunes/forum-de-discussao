using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Chatter.Application.ViewModels
{
    public class UserViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Nome")]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [DisplayName("E-mail")]
        [StringLength(150)]
        [EmailAddress]
        public string Email { get; set; }
        
        public DateTime Created { get; set; }

        public bool Active { get; set; }

        public bool Removed { get; set; }

        public Guid IdentityId { get; set; }
    }
}