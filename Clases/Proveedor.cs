using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitux_POS.Clases
{
    public class Proveedor
    {
        [Key]
        public int Secuencial { get; set; }
        public string? Nombre { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public string? Email { get; set; }
        public string? Contacto { get; set; }
        public int? Tipo { get; set; }

        public string? Imagen { get; set; }
        public bool? Activo { get; set; } = true;

    }
}
