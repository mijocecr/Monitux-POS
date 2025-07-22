using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitux_POS.Clases
{
    public class Cuentas_Pagar
    {

        [Key] public int Secuencial { get; set; }
        public int Secuencial_Factura { get; set; } = 0;
        public int Secuencial_Proveedor { get; set; } = 0;
        public int Secuencial_Usuario { get; set; } = 0;


        public string? Fecha { get; set; } = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        public string? Fecha_Vencimiento { get; set; }
        public double? Total { get; set; } = 0.0;
        public double? Saldo { get; set; } = 0.0;
        public double? Pagado { get; set; } = 0.0;
        public double? Otros_Cargos { get; set; } = 0.0;
        public double? Descuento { get; set; } = 0.0;
        public double? Impuesto { get; set; } = 0.0;
        public double? Gran_Total { get; set; } = 0.0;

        public int Secuencial_Empresa { get; set; }


    }
}
