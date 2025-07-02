using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitux_POS.Clases
{
  public  class Usuario
    {
        [Key]
        public int Secuencial { get; set; }
        public string Codigo { get; set; }
        public string? Nombre { get; set; }
        public string Password { get; set; }
        public string  ? Imagen { get; set; }
        public string Acceso { get; set; }
        public bool Activo { get; set; }

        public int Secuencial_Empresa { get; set; }

    }
}
