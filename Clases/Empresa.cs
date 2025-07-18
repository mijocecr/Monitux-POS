using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitux_POS.Clases
{
    public class Empresa
    {
        [Key] public int Secuencial { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Moneda { get; set; } = string.Empty;
        public double ISV { get; set; } = 0.0;

        public bool Activa { get; set; } = true;
        public string Imagen { get; set; } = string.Empty;
        public int Secuencial_Usuario { get; set; } 

        public string RSS { get; set; } = "https://www.tunota.com/rss/honduras-hoy.xml";

    }
}
