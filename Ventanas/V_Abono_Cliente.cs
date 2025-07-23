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
using static QuestPDF.Helpers.Colors;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Monitux_POS.Ventanas
{
    public partial class V_Abono_Cliente : Form
    {

        public int Secuencial_CTAC { get; set; }
        public string Cliente_Nombre { get; set; }


        public int Secuencial_Cliente { get; set; }

        public int Secuencial_Usuario { get; set; }
        public double Gran_Total { get; set; }
        public int Secuencial_Empresa { get; set; }

        public V_Abono_Cliente(int secuencial_ctac, int secuencial_cliente, string nombre_cliente, double gran_total)
        {
            InitializeComponent();
            Gran_Total = gran_total;
            this.Text = "CTA. a Cobrar: " + secuencial_ctac.ToString();
            Secuencial_Empresa = V_Menu_Principal.Secuencial_Empresa;
            Secuencial_Usuario = V_Menu_Principal.Secuencial_Usuario;
            Secuencial_Cliente = secuencial_cliente;
            Secuencial_CTAC = secuencial_ctac;
            label6.Text = nombre_cliente;

        }







        private void Cargar_Datos()
        {
            textBox1.KeyPress += textBox1_KeyPress;


            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe


            var cta_cobrar = context.Cuentas_Cobrar
    .Where(c => c.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa && c.Secuencial_Cliente == Secuencial_Cliente && c.Secuencial == Secuencial_CTAC)
    .ToList();




            foreach (var item in cta_cobrar)
            {

                label8.Text = item.Gran_Total.ToString();
                label9.Text = item.Pagado.ToString();
                label10.Text = item.Saldo.ToString();




            }



            double valorRef = Math.Round(Convert.ToDouble(this.Gran_Total), 2);
            double tolerancia = 0.05;

            var venta = context.Ventas
                .Where(c =>
                    c.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa &&
                    c.Secuencial_Cliente == Secuencial_Cliente && c.Tipo == "Credito")
                .AsEnumerable() // Evaluación en memoria para evitar errores de traducción en EF
                .Where(c =>
                    Math.Abs(Convert.ToDouble(c.Gran_Total) - valorRef) < tolerancia)
                .ToList();




            foreach (var item in venta)
            {

                label11.Text = item.Secuencial.ToString();



            }



        }






        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void V_Abono_Cliente_Load(object sender, EventArgs e)
        {
            Cargar_Datos();
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
                var cta_cobrar = context.Cuentas_Cobrar
                    .FirstOrDefault(p => p.Secuencial == this.Secuencial_CTAC &&
                                         p.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa);

                if (cta_cobrar != null)
                {
                    cta_cobrar.Saldo = nuevo_saldo;
                    cta_cobrar.Pagado = nuevo_pagado;
                    cta_cobrar.Secuencial_Factura = int.Parse(label11.Text);
                    cta_cobrar.Secuencial_Empresa = Secuencial_Empresa;
                    cta_cobrar.Secuencial_Cliente = Secuencial_Cliente;
                    cta_cobrar.Secuencial_Usuario = V_Menu_Principal.Secuencial_Usuario;

                    // 🧾 Registrar abono como ingreso
                    var ingreso = new Ingreso
                    {
                        Secuencial_Empresa = this.Secuencial_Empresa,
                        Secuencial_Factura = this.Secuencial_CTAC,
                        Secuencial_Usuario = this.Secuencial_Usuario,
                        Fecha = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                        Total = abono,
                        Tipo_Ingreso = "Pago Recibido",
                        Descripcion = $"Abono a CTA: {Secuencial_CTAC} de la Factura: {label11.Text}"
                    };

                    context.Ingresos.Add(ingreso);

                    // 💰 También registrar el abono
                    var abono_venta = new Abono_Venta
                    {
                        Secuencial_Empresa = this.Secuencial_Empresa,
                        Secuencial_Cliente = this.Secuencial_Cliente,
                        Secuencial_Usuario = this.Secuencial_Usuario,
                        Secuencial_CTAC = this.Secuencial_CTAC,
                        Fecha = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                        Monto = abono
                    };

                    context.Abonos_Ventas.Add(abono_venta);

                    // 💾 Guardar todo junto
                    context.SaveChanges();

                    // 🔔 Feedback al usuario
                    Util.Registrar_Actividad(Secuencial_Usuario,
                        $"Ha registrado un abono a CTA:{Secuencial_CTAC} de {label6.Text} por un monto de: {textBox1.Text} {V_Menu_Principal.moneda} a Factura: {label11.Text}",
                        V_Menu_Principal.Secuencial_Empresa);

                    V_Menu_Principal.MSG.ShowMSG("Abono registrado correctamente", "Éxito");
                }

                Cargar_Datos();
                this.Dispose();

            }
            else
            {

                V_Menu_Principal.MSG.ShowMSG("El monto a recibir es invalido.", "Error");
                return;
            }

            //Codigo Optimizado
            ///////////////////






            /*
                        double pagado = Convert.ToDouble(label9.Text.Replace(",", "."), CultureInfo.InvariantCulture);

                        double saldo = Convert.ToDouble(label10.Text.Replace(",", "."), CultureInfo.InvariantCulture);
                        double abono = Convert.ToDouble(textBox1.Text.Replace(",", "."), CultureInfo.InvariantCulture);


                        double nuevo_saldo = saldo - abono;
                        double nuevo_pagado = pagado + abono;

                        SQLitePCL.Batteries.Init();

                        using var context = new Monitux_DB_Context();
                        context.Database.EnsureCreated(); // Asegura que la BD exista

                        // Buscar cuenta existente
                        var cta_cobrar = context.Cuentas_Cobrar
                            .FirstOrDefault(p => p.Secuencial == this.Secuencial_CTAC &&
                                                 p.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa);

                        if (cta_cobrar != null)
                        {
                            // Actualizar campos
                            cta_cobrar.Saldo = nuevo_saldo;
                            cta_cobrar.Pagado = nuevo_pagado;
                            cta_cobrar.Secuencial_Factura = int.Parse(label11.Text);
                            cta_cobrar.Secuencial_Empresa = Secuencial_Empresa;
                            cta_cobrar.Secuencial_Cliente = Secuencial_Cliente;
                            cta_cobrar.Secuencial_Usuario = V_Menu_Principal.Secuencial_Usuario;

                            context.SaveChanges();

                            Util.Registrar_Actividad(
                                Secuencial_Usuario,
                                "Ha registrado un abono por un monto de: " + textBox1.Text +
                                " a Factura: " + label11.Text,
                                V_Menu_Principal.Secuencial_Empresa);

                            V_Menu_Principal.MSG.ShowMSG("Abono registrado correctamente", "Éxito");
                        }

                        // Registrar ingreso en nuevo contexto
                        using var context1 = new Monitux_DB_Context();
                        context1.Database.EnsureCreated();

                        var ingreso = new Ingreso
                        {
                            Secuencial_Empresa = this.Secuencial_Empresa,
                            Secuencial_Factura = this.Secuencial_CTAC,
                            Secuencial_Usuario = this.Secuencial_Usuario,
                            Fecha = DateTime.Now.ToString("dd/MM/yyyy"),
                            Total = abono,
                            Tipo_Ingreso = "Efectivo / Tarjeta ",
                            Descripcion = "Abono a CTA " + Secuencial_CTAC + " a la Factura: " + label11.Text

                        };

                        context1.Ingresos.Add(ingreso);
                        context1.SaveChanges();



                        // Registrar ingreso en nuevo contexto



                        using var context2 = new Monitux_DB_Context();
                        context2.Database.EnsureCreated();

                        var abono_venta = new Abono_Venta
                        {
                            Secuencial_Empresa = this.Secuencial_Empresa,
                            Secuencial_Cliente = this.Secuencial_Cliente,
                            Secuencial_Usuario = this.Secuencial_Usuario,
                            Secuencial_CTAC = this.Secuencial_CTAC,
                            Fecha = DateTime.Now.ToString("dd/MM/yyyy"),
                            Monto = abono
                        };

                        context2.Abonos_Ventas.Add(abono_venta);
                        context2.SaveChanges();



                        Cargar_Datos();
                        this.Dispose();

                        ///////////////////*/

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

        private void button1_Click(object sender, EventArgs e)
        {
            V_Abonos v_Abonos = new V_Abonos(Secuencial_CTAC, true,label6.Text, int.Parse(label11.Text));
            

            v_Abonos.ShowDialog();


        }
    }
}
