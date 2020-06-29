using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Modelos.Basicos
{
   public class Libro
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Isbn { get; set; }

        public int AutorId { get; set; }
        public int CategoriaId { get; set; }
    }
}
