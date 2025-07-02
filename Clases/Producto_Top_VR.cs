using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitux_POS.Clases
{
    public class Producto_Top_VR
    {
        public int Secuencial_Producto { get; set; }
        public string Descripcion { get; set; }
        public string Codigo { get; set; }
        public double TotalVendido { get; set; }
        public double Venta { get; set; } // Hay que ajustar esto para incluir el Secuencial de la Empresa
    }                                     //Por ahora se queda asi, pero tengo que cambiarlo
}
