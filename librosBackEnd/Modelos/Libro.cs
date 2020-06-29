
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace librosBackEnd.modelos
{
    public class Libro
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
   
        [Required]
        public string Isbn { get; set; }

        public int    AutorId { get; set; }
        public int CategoriaId { get; set; }

      
    }
}
