using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Crud.Models
{
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "Obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Obrigatório")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Obrigatório")]
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "Obrigatório")]
        public int Valor { get; set; }

        [NotMapped]
        public string Categoria { get; set; }
    }
}
