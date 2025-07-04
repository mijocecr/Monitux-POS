using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitux_POS.Clases
{
    public class Item_Factura
    {
        int Secuencial {  get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public double Precio { get; set; }
        public double Total => Math.Round(Cantidad * Precio,2);


    }
}
