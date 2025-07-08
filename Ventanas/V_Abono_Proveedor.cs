using Monitux_POS.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monitux_POS.Ventanas
{
    public partial class V_Abono_Proveedor : Form
    {



        public int Secuencial_CTAP { get; set; }
        public string Proveedor_Nombre { get; set; }
        public string Proveedor_Telefono { get; set; }
        public int Secuencial_Factura { get; set; }

        public int Secuencial_Proveedor { get; set; }

        public int Secuencial_Usuario { get; set; }
        public double Gran_Total { get; set; }
        public int Secuencial_Empresa { get; set; }



        public V_Abono_Proveedor(int secuencial_ctap, int secuencial_proveedor, string nombre_proveedor, double gran_total)
        {
            InitializeComponent();

            Gran_Total = gran_total;
            this.Text = "CTA. a Pagar: " + secuencial_ctap.ToString();
            Secuencial_Empresa = V_Menu_Principal.Secuencial_Empresa;
            Secuencial_Usuario = V_Menu_Principal.Secuencial_Usuario;
            Secuencial_Proveedor = secuencial_proveedor;
            Secuencial_CTAP = secuencial_ctap;
            label6.Text = nombre_proveedor;

        }



        private void Cargar_Datos()
        {
            textBox1.KeyPress += textBox1_KeyPress;


            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe


            var cta_pagar = context.Cuentas_Pagar
    .Where(c => c.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa && c.Secuencial_Proveedor == Secuencial_Proveedor && c.Secuencial == Secuencial_CTAP)
    .ToList();




            foreach (var item in cta_pagar)
            {

                label8.Text = item.Gran_Total.ToString();
                label9.Text = item.Pagado.ToString();
                label10.Text = item.Saldo.ToString();




            }



            double valorRef = Math.Round(Convert.ToDouble(this.Gran_Total), 2);
            double tolerancia = 0.05;

            var compra = context.Compras
                .Where(c =>
                    c.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa &&
                    c.Secuencial_Proveedor == Secuencial_Proveedor && c.Tipo == "Credito")
                .AsEnumerable() // Evaluación en memoria para evitar errores de traducción en EF
                .Where(c =>
                    Math.Abs(Convert.ToDouble(c.Gran_Total) - valorRef) < tolerancia)
                .ToList();




            foreach (var item in compra)
            {

                label11.Text = item.Secuencial.ToString();



            }



        }




        private void V_Abono_Proveedor_Load(object sender, EventArgs e)
        {
            Cargar_Datos();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

            // Permitir solo dígitos, retroceso y punto
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true; // Bloquea el carácter
            }

            // Solo un punto decimal permitido
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains("."))
            {
                e.Handled = true;
            }



        }

        private void button2_Click(object sender, EventArgs e)
        {

            ///////////////////
            //Codigo Optimizado

            if (!string.IsNullOrEmpty(textBox1.Text))
            {

                double abono = double.Parse(textBox1.Text.Replace(",", "."), CultureInfo.InvariantCulture);
                double pagado = double.Parse(label9.Text.Replace(",", "."), CultureInfo.InvariantCulture);
                double saldo = double.Parse(label10.Text.Replace(",", "."), CultureInfo.InvariantCulture);

                double nuevo_pagado = pagado + abono;
                double nuevo_saldo = saldo - abono;

                SQLitePCL.Batteries.Init();

                // 👇 Usamos un solo contexto si no es necesario separarlos
                using var context = new Monitux_DB_Context();
                context.Database.EnsureCreated();

                // 🔄 Actualizar cuenta por cobrar
                var cta_pagar = context.Cuentas_Pagar
                    .FirstOrDefault(p => p.Secuencial == this.Secuencial_CTAP &&
                                         p.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa);

                if (cta_pagar != null)
                {
                    cta_pagar.Saldo = nuevo_saldo;
                    cta_pagar.Pagado = nuevo_pagado;
                    cta_pagar.Secuencial_Factura = int.Parse(label11.Text);
                    cta_pagar.Secuencial_Empresa = Secuencial_Empresa;
                    cta_pagar.Secuencial_Proveedor = Secuencial_Proveedor;
                    cta_pagar.Secuencial_Usuario = V_Menu_Principal.Secuencial_Usuario;

                    // 🧾 Registrar abono como egreso
                    var egreso = new Egreso
                    {
                        Secuencial_Empresa = this.Secuencial_Empresa,
                        Secuencial_Factura = this.Secuencial_CTAP,
                        Secuencial_Usuario = this.Secuencial_Usuario,
                        Fecha = DateTime.Now.ToString("dd/MM/yyyy"),
                        Total = abono,
                        Tipo_Egreso = "Efectivo / Tarjeta ",
                        Descripcion = $"Abono a CTA: {Secuencial_CTAP} de la Factura: {label11.Text}"
                    };




                    context.Egresos.Add(egreso);

                    // 💰 También registrar el abono
                    var abono_compra = new Abono_Compra
                    {
                        Secuencial_Empresa = this.Secuencial_Empresa,
                        Secuencial_Proveedor = this.Secuencial_Proveedor,
                        Secuencial_Usuario = this.Secuencial_Usuario,
                        Secuencial_CTAP = this.Secuencial_CTAP,
                        Fecha = DateTime.Now.ToString("dd/MM/yyyy"),
                        Monto = abono
                    };

                    context.Abonos_Compras.Add(abono_compra);

                    // 💾 Guardar todo junto
                    context.SaveChanges();

                    // 🔔 Feedback al usuario
                    Util.Registrar_Actividad(Secuencial_Usuario,

                        $"Ha registrado un pago a CTA: {Secuencial_CTAP} de  {label6.Text} por un monto de: {textBox1.Text} {V_Menu_Principal.moneda} a Factura : {label11.Text}",
                        V_Menu_Principal.Secuencial_Empresa);

                    V_Menu_Principal.MSG.ShowMSG("Pago registrado correctamente", "Éxito");
                }

                Cargar_Datos();
                this.Dispose();

            }
            else
            {

                V_Menu_Principal.MSG.ShowMSG("El monto a pagar es invalido.", "Error");
                return;
            }

            //Codigo Optimizado
            ///////////////////




        }

        private void button1_Click(object sender, EventArgs e)
        {
            V_Abonos v_Abonos = new V_Abonos(Secuencial_CTAP, false, label6.Text, int.Parse(label11.Text));


            v_Abonos.ShowDialog();
        }
    }
}
