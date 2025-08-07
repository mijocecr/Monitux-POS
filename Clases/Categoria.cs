using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitux_POS.Clases
{
    public class Categoria
    {

        [Key]
        public int Secuencial { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }

        // Aquí se guarda el contenido binario de la imagen
        public byte[]? Imagen { get; set; }

        public int Secuencial_Empresa { get; set; }
    }
}
