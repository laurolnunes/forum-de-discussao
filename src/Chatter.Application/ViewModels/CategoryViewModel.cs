using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Chatter.Application.ViewModels
{
    public class CategoryViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DisplayName("Nome")]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [DisplayName("Descrição")]
        [StringLength(500, MinimumLength = 2)]
        public string Description { get; set; }

        [DisplayName("Imagem")]
        [StringLength(500, MinimumLength = 2)]
        public string Image { get; set; }

        public DateTime Created { get; set; }

        [DisplayName("Destaque")]
        public bool IsFeatured { get; set; }

        [DisplayName("Ativo")]
        public bool Active { get; set; }

        public bool Removed { get; set; }
    }
}