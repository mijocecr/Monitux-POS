using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitux_POS.Clases
{
    public class Abono_Venta
    {

        [Key] public int Secuencial { get; set; }
        public int Secuencial_CTAC { get; set; }
        public int Secuencial_Usuario { get; set; }
        public int Secuencial_Cliente { get; set; }
        public string Fecha { get; set; } = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        public double Monto { get; set; } = 0;
        public int Secuencial_Empresa { get; set; }

    }
}
