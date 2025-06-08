using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitux_POS.Clases
{
    public class Cotizacion_Detalle
    {

        [Key] public int Secuencial { get; set; }
        public int Secuencial_Cotizacion { get; set; } = 0;
        
        public int Secuencial_Usuario { get; set; } = 0;

        public int Secuencial_Cliente { get; set; } = 0;
        public int Secuencial_Producto { get; set; } = 0;
        public string? Fecha { get; set; }
        public string? Codigo { get; set; } = ""; //Código del producto
        public string? Descripcion { get; set; }
        public double ? Cantidad { get; set; } = 0;

        public double? Precio { get; set; } = 0;

        public double? Total { get; set; } = 0;



    }
}
