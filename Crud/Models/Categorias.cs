using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Crud.Models
{
    public class Categorias
    {
        [Key]
        public int ID { get; set; }
        public string Categoria { get; set; }
    }
}

