using Monitux_POS.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monitux_POS.Ventanas
{
    public partial class V_Ingresos_Egresos : Form
    {
        public bool IsEgreso { get; set; } = false; // Indica si es un egreso o ingreso
        public int Secuencial_Usuario { get; set; }
        public int Secuencial_Empresa { get; set; }
        public V_Ingresos_Egresos()
        {
            InitializeComponent();
        }

        private void V_Ingresos_Egresos_Load(object sender, EventArgs e)
        {
            if (IsEgreso == true)
            {
                button1.Text = "Registrar Egreso";
                button1.ForeColor = Color.FromArgb(128,255, 255);
            }
            else
            {
                button1.Text = "Registrar Ingreso";
                button1.ForeColor = Color.Lime;
            }
            this.Text = "Monitux-POS v." + V_Menu_Principal.VER;
           
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {


            // Permitir solo dígitos, retroceso y punto
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true; // Bloquea el carácter
            }

            // Solo un punto decimal permitido
            if (e.KeyChar == '.' && (sender as System.Windows.Forms.TextBox).Text.Contains("."))
            {
                e.Handled = true;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {






            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();


            if (!decimal.TryParse(txtTotal.Text, out var total) || total <= 0)
            {
                V_Menu_Principal.MSG.ShowMSG("Debe ingresar un monto válido.","Error");
                return;
            }


            if (this.IsEgreso == false)
            {
                var ingresoManual = new Ingreso
                {
                    Fecha = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"),
                    Tipo_Ingreso = "Ingreso Manual",
                    Total = (double)total,
                    Descripcion = txtDescripcion.Text.Trim(),
                    Secuencial_Usuario = this.Secuencial_Usuario,
                    Secuencial_Empresa = this.Secuencial_Empresa,
                    Secuencial_Factura = 0 // porque no está vinculado a ninguna factura
                };

                context.Ingresos.Add(ingresoManual);
                context.SaveChanges();

                V_Menu_Principal.MSG.ShowMSG("Ingreso registrado correctamente.", "Exito");

                Util.Registrar_Actividad(ingresoManual.Secuencial_Usuario, $"Ha registrado un ingreso manual de {ingresoManual.Total} {V_Menu_Principal.moneda} por concepto de: {ingresoManual.Descripcion}.", ingresoManual.Secuencial_Empresa);

            }


            else {
                var egresoManual = new Egreso
                {
                    Fecha = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"),
                    Tipo_Egreso = "Egreso Manual",
                    Total = (double)total,
                    Descripcion = txtDescripcion.Text.Trim(),
                    Secuencial_Usuario = this.Secuencial_Usuario,
                    Secuencial_Empresa = this.Secuencial_Empresa,
                    Secuencial_Factura = 0 // porque no está vinculado a ninguna factura
                };
                context.Egresos.Add(egresoManual);
                context.SaveChanges();
                V_Menu_Principal.MSG.ShowMSG("Egreso registrado correctamente.", "Exito");
                Util.Registrar_Actividad(egresoManual.Secuencial_Usuario, $"Ha registrado un egreso manual de {egresoManual.Total} {V_Menu_Principal.moneda} por concepto de: {egresoManual.Descripcion}.", egresoManual.Secuencial_Empresa);
            }

            this.Close();


            

        }
            
    }
}
