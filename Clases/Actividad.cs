using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitux_POS.Clases
{
   public class Actividad
    {
        [Key]
        public int Secuencial { get; set; }
        public int Secuencial_Usuario { get; set; }

        public string Fecha { get; set; } = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");

        public string ? Descripcion { get;set; }

        public int Secuencial_Empresa { get; set; }




    }
}
