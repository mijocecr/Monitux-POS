using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitux_POS.Clases
{
    public class Kardex
    {
        [Key]
        public int Secuencial { get; set; }
        public string Fecha { get; set; } = DateTime.Now.ToString();
        public int Secuencial_Producto { get; set;  }
        public string Descripcion { get; set; }
        public double Cantidad { get; set; } = 0;
        public double Costo { get; set; } = 0;

        public double Costo_Total { get; set; } = 0;
        public double Venta { get; set; } = 0;
        public double Venta_Total { get; set; } = 0;
        public double Saldo { get; set; } = 0;
        public string Movimiento { get; set; }


    }
}
