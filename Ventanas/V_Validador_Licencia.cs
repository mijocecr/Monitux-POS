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
    public partial class V_Validador_Licencia : Form
    {
        public V_Validador_Licencia()
        {
            InitializeComponent();
        }

        private async void btnValidar_Click(object sender, EventArgs e)
        {



            ////////////////////////////////////////////////////////


        
            var gestor = new Gestor_Licencia();
            bool ok = await gestor.ValidarYActivarLicenciaAsync(txtLicencia.Text.Trim());

            if (ok)
            {
                V_Menu_Principal.MSG.ShowMSG("✅ Licencia activada correctamente","Monitux-POS");
                lblResultado.Text = "✅ Licencia válida";
                lblCliente.Text = Properties.Settings.Default.NombreCliente;
                lblExpira.Text = Properties.Settings.Default.FechaExpiracion.ToString("dd/MM/yyyy");

                // Continuar con la app
            }
            else
            {
                V_Menu_Principal.MSG.ShowMSG("❌ Licencia inválida, vencida o ya usada\nContacte con: hn.one.click.solutions@gmail.com", "Monitux-POS");
            }
        




            //////////////////////////////////////////////////////




/*

            string codigo = txtLicencia.Text.Trim();

            if (string.IsNullOrEmpty(codigo))
            {
                lblResultado.Text = "⚠️ Ingresa un código.";
                return;
            }

            var validador = new ValidadorLicencia(); // Clase aparte que contiene la lógica
            var resultado = await validador.ValidarAsync(codigo);

            if (resultado.EsValido)
            {
               
                Properties.Settings.Default.LicenciaValida = true;
                Properties.Settings.Default.Save();
            }
            else
            {
                lblResultado.Text = "❌ Licencia no válida";
                lblCliente.Text = "";
                lblExpira.Text = "";
            }

            */



        }

        private void V_Validador_Licencia_Load(object sender, EventArgs e)
        {

        }
    }


   

}
