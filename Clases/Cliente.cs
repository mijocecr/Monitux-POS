using System;
using System.ComponentModel.DataAnnotations;

namespace Monitux_POS.Clases
{
    public class Cliente
    {
        [Key]
        public int Secuencial { get; set; }

        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public string? Email { get; set; }

        // Imagen como archivo binario
        public byte[]? Imagen { get; set; }

        public bool? Activo { get; set; }

        public int Secuencial_Empresa { get; set; }
    }
}
