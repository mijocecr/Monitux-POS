using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Monitux_POS.Clases
{
    public class Proveedor
    {
        [Key]
        public int Secuencial { get; set; }
        public string? Nombre { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public string? Email { get; set; }
        public string? Contacto { get; set; }
        public string? Tipo { get; set; }

        public byte[]? Imagen { get; set; } // ← Cambio aquí

        public bool? Activo { get; set; } = true;
        public int Secuencial_Empresa { get; set; }
    }
}
