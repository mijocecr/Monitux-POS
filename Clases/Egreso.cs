﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitux_POS.Clases
{
    public class Egreso
    {


        [Key]

        public int Secuencial { get; set; }
        public int? Secuencial_Factura { get; set; } = 0;
        public int Secuencial_Usuario { get; set; } = 0;
        public string Fecha { get; set; } = DateTime.Now.ToString("dd/MM/yyyy");
        public string Tipo_Egreso { get; set; } = "";
        public string Descripcion { get; set; } = "";
        public double Total { get; set; } = 0.0;

        public int Secuencial_Empresa { get; set; }

    }
}
