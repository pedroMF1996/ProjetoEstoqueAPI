using System.ComponentModel.DataAnnotations;

namespace EstoqueAPI.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Este campo é obrigatório")]
        [MaxLength(60,ErrorMessage ="Este campo precisa ter no máximo 60 caractéres")]
        [MinLength(3,ErrorMessage ="Este campo precisa ter no mínimo 3 carácteres")]
        public string Title { get; set; }

        public Category(){}
    }
}
