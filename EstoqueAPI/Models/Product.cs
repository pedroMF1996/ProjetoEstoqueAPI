using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EstoqueAPI.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigadtório")]
        [MaxLength(60, ErrorMessage ="Este campo deve ter no máximo 60 caractéres")]
        [MinLength(3,ErrorMessage ="Este campo deve ter no mínimo 3 caracteres")]
        public string Title { get; set; }

        [MaxLength(1024,ErrorMessage ="Este campo deve ter no máximo 1024 caractéres")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "O preço deve ser maior do que zero")]
        public decimal Cost { get; set; }

        [Required(ErrorMessage ="Este campo é obrigatório")]
        [Range(1,int.MaxValue,ErrorMessage ="Categoria inválida")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public Product() {}
    }
}
