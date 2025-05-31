using System.ComponentModel.DataAnnotations;

namespace Monitux_POS.Clases
{
    public class Comentario
    {
        [Key]
        public int Secuencial { get; set; }
        public int Secuencial_Factura_C { get; set; }

        public int Secuencial_Factura_V { get; set; }

        public int Secuencial_Producto { get; set; }
        public int Secuencial_Cotizacion { get; set; }
        public int Secuencial_Orden { get; set; }

        public string Contenido { get; set; }




    }
}
