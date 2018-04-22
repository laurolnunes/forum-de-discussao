using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Chatter.Application.ViewModels
{
    public class TopicViewModel
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Usuário")]
        public int UserId { get; set; }

        [DisplayName("Categoria")]
        public int CategoryId { get; set; }

        [Required]
        [DisplayName("Título")]
        [StringLength(200, MinimumLength = 2)]
        public string Title { get; set; }

        [DisplayName("Descrição")]
        [StringLength(1000, MinimumLength = 2)]
        public string Description { get; set; }

        public DateTime Created { get; set; }

        [DisplayName("Ativo")]
        public bool Active { get; set; }

        public bool Removed { get; set; }

        public List<PostViewModel> Posts { get; set; }


        public UserViewModel User { get; set; }
    }
}