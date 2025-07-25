
using Monitux_POS.Clases;
using Monitux_POS.Controles;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Monitux_POS.Ventanas
{
    public partial class V_Dashboard : Form
    {
        public DateTime fechaSeleccionada;

        public V_Dashboard()
        {
            InitializeComponent();

        }


        private string FormatearTextoCambio(decimal cambio, DateTime fecha)
        {
            if (cambio == 0)
                return "Sin cambio";

            return cambio > 0
                ? $"↑ {cambio:F1}% vs {fecha.AddDays(-1):d}"
                : $"↓ {Math.Abs(cambio):F1}% vs {fecha.AddDays(-1):d}";
        }


        private void V_Dashboard_Load(object sender, EventArgs e)
        {

            DashboardService servicio = new DashboardService(V_Menu_Principal.Secuencial_Empresa);

            // Variaciones corregidas
            decimal vVentas = servicio.CalcularCambioPorcentualVentas();
            string textoVentas = vVentas != 0
                ? (vVentas > 0 ? $"↑ {vVentas:F1}% vs ayer" : $"↓ {Math.Abs(vVentas):F1}% vs ayer")
                : "Sin cambio";

            decimal vCompras = servicio.CalcularCambioPorcentualCompras(fechaSeleccionada);
            string textoCompras = vCompras != 0
                ? (vCompras > 0 ? $"↑ {vCompras:F1}% vs ayer" : $"↓ {Math.Abs(vCompras):F1}% vs ayer")
                : "Sin cambio";

            var tarjetas = new List<TarjetaDashboard>
{
    new TarjetaDashboard("Ventas Hoy", servicio.ObtenerTotalVentasHoy().ToString("C"), textoVentas, Properties.Resources.cart, Color.DarkGreen, new Point(20, 20)),
    new TarjetaDashboard("Ventas Mes", servicio.ObtenerTotalVentasDelMes().ToString("C"), "", Properties.Resources.cart, Color.ForestGreen, new Point(240, 20)),
    new TarjetaDashboard("Compras Hoy", servicio.ObtenerTotalComprasHoy().ToString("C"), textoCompras, Properties.Resources.cart, Color.Teal, new Point(460, 20)),
    new TarjetaDashboard("Compras Mes", servicio.ObtenerTotalComprasDelMes().ToString("C"), "", Properties.Resources.cart, Color.MediumTurquoise, new Point(680, 20)),
    new TarjetaDashboard("Ingresos Hoy", servicio.ObtenerTotalIngresosHoy().ToString("C"), "", Properties.Resources.cart, Color.DarkOrange, new Point(20, 140)),
    new TarjetaDashboard("Egresos Hoy", servicio.ObtenerTotalEgresosHoy().ToString("C"), "", Properties.Resources.cart, Color.IndianRed, new Point(240, 140)),
    new TarjetaDashboard("Otros Ingresos", servicio.ObtenerOtrosIngresosHoy().ToString("C"), "", Properties.Resources.cart, Color.Goldenrod, new Point(460, 140)),
    new TarjetaDashboard("Otros Egresos", servicio.ObtenerOtrosEgresosHoy().ToString("C"), "", Properties.Resources.cart, Color.SaddleBrown, new Point(680, 140)),
    new TarjetaDashboard("Stock Crítico", servicio.ObtenerStockCritico().ToString(), "", Properties.Resources.cart, Color.Orange, new Point(20, 260)),
    new TarjetaDashboard("Productos Vencidos", servicio.ObtenerProductosVencidos().ToString(), "", Properties.Resources.cart, Color.DarkRed, new Point(240, 260)),
    new TarjetaDashboard("Agotados", servicio.ObtenerAgotados().ToString(), "", Properties.Resources.cart, Color.Red, new Point(460, 260)),
    new TarjetaDashboard("Por Cobrar Vencidas", servicio.ObtenerCuentasPorCobrarVencidas().Count.ToString(), "", Properties.Resources.cart, Color.Firebrick, new Point(680, 260)),
    new TarjetaDashboard("Por Pagar Vencidas", servicio.ObtenerCuentasPorPagarVencidas().Count.ToString(), "", Properties.Resources.cart, Color.Crimson, new Point(20, 380))
};

            foreach (var tarjeta in tarjetas)
            {
                flowLayoutPanel1.Controls.Add(tarjeta.Panel);
            }




        }

        /*
        private void CargarTarjetasParaFecha(DateTime fecha)
        {
            flowLayoutPanel1.Controls.Clear();

            var servicio = new DashboardService(V_Menu_Principal.Secuencial_Empresa);

            // Ventas
            var cambioVentas = servicio.CalcularCambioPorcentualVentas(fecha);
            var tarjetaVentas = new TarjetaDashboard(
                "Ventas",
                servicio.ObtenerTotalVentasPorFecha(fecha).ToString("C"),
                FormatearTextoCambio(cambioVentas, fecha),
                Properties.Resources.cart,
                Color.DarkGreen,
                new Point(20, 20)
            );
            flowLayoutPanel1.Controls.Add(tarjetaVentas.Panel);

            // Compras
            var cambioCompras = servicio.CalcularCambioPorcentualCompras(fecha);
            var tarjetaCompras = new TarjetaDashboard(
                "Compras",
                servicio.ObtenerTotalComprasPorFecha(fecha).ToString("C"),
                FormatearTextoCambio(cambioCompras, fecha),
                Properties.Resources.cart,
                Color.Teal,
                new Point(240, 20)
            );
            flowLayoutPanel1.Controls.Add(tarjetaCompras.Panel);

            // Ingresos
            var cambioIngresos = servicio.CalcularCambioPorcentualIngresos(fecha);
            var tarjetaIngresos = new TarjetaDashboard(
                "Ingresos",
                servicio.ObtenerTotalIngresosPorFecha(fecha).ToString("C"),
                FormatearTextoCambio(cambioIngresos, fecha),
                Properties.Resources.cart,
                Color.DarkOrange,
                new Point(460, 20)
            );
            flowLayoutPanel1.Controls.Add(tarjetaIngresos.Panel);

            // Egresos
            var cambioEgresos = servicio.CalcularCambioPorcentualEgresos(fecha);
            var tarjetaEgresos = new TarjetaDashboard(
                "Egresos",
                servicio.ObtenerTotalEgresosPorFecha(fecha).ToString("C"),
                FormatearTextoCambio(cambioEgresos, fecha),
                Properties.Resources.cart,
                Color.IndianRed,
                new Point(680, 20)
            );
            flowLayoutPanel1.Controls.Add(tarjetaEgresos.Panel);
        }
*/


        private void CargarTarjetasParaFecha(DateTime fecha)
        {
            flowLayoutPanel1.Controls.Clear();
            var servicio = new DashboardService(V_Menu_Principal.Secuencial_Empresa);

            // Comparativas
            decimal cambioVentas = servicio.CalcularCambioPorcentualVentas(fecha);
            decimal cambioCompras = servicio.CalcularCambioPorcentualCompras(fecha);
            decimal cambioIngresos = servicio.CalcularCambioPorcentualIngresos(fecha);
            decimal cambioEgresos = servicio.CalcularCambioPorcentualEgresos(fecha);

            var tarjetas = new List<TarjetaDashboard>
    {
        new TarjetaDashboard("Ventas", servicio.ObtenerTotalVentasPorFecha(fecha).ToString("C"), FormatearTextoCambio(cambioVentas, fecha), Properties.Resources.cart, Color.DarkGreen, new Point(20, 20)),

         new TarjetaDashboard("Ventas Mes", servicio.ObtenerTotalVentasDelMes().ToString("C"), "", Properties.Resources.cart, Color.ForestGreen, new Point(240, 20)),

         new TarjetaDashboard("Compras", servicio.ObtenerTotalComprasPorFecha(fecha).ToString("C"), FormatearTextoCambio(cambioCompras, fecha), Properties.Resources.cart, Color.Teal, new Point(240, 20)),

          new TarjetaDashboard("Compras Mes", servicio.ObtenerTotalComprasDelMes().ToString("C"), "", Properties.Resources.cart, Color.MediumTurquoise, new Point(680, 20)),

                new TarjetaDashboard("Ingresos", servicio.ObtenerTotalIngresosPorFecha(fecha).ToString("C"), FormatearTextoCambio(cambioIngresos, fecha), Properties.Resources.cart, Color.DarkOrange, new Point(460, 20)),
        new TarjetaDashboard("Egresos", servicio.ObtenerTotalEgresosPorFecha(fecha).ToString("C"), FormatearTextoCambio(cambioEgresos, fecha), Properties.Resources.cart, Color.IndianRed, new Point(680, 20)),

        // Métricas fijas
       
        new TarjetaDashboard("Otros Ingresos", servicio.ObtenerOtrosIngresosPorFecha(fecha).ToString("C"), "", Properties.Resources.cart, Color.Goldenrod, new Point(20, 140)),
        new TarjetaDashboard("Otros Egresos", servicio.ObtenerOtrosEgresosPorFecha(fecha).ToString("C"), "", Properties.Resources.cart, Color.SaddleBrown, new Point(240, 140)),
        new TarjetaDashboard("Stock Crítico", servicio.ObtenerStockCritico().ToString(), "", Properties.Resources.cart, Color.Orange, new Point(460, 140)),
        new TarjetaDashboard("Productos Vencidos", servicio.ObtenerProductosVencidos().ToString(), "", Properties.Resources.cart, Color.DarkRed, new Point(680, 140)),
        new TarjetaDashboard("Agotados", servicio.ObtenerAgotados().ToString(), "", Properties.Resources.cart, Color.Red, new Point(20, 260)),
        new TarjetaDashboard("Por Cobrar Vencidas", servicio.ObtenerCuentasPorCobrarVencidas().Count.ToString(), "", Properties.Resources.cart, Color.Firebrick, new Point(240, 260)),
        new TarjetaDashboard("Por Pagar Vencidas", servicio.ObtenerCuentasPorPagarVencidas().Count.ToString(), "", Properties.Resources.cart, Color.Crimson, new Point(460, 260))
    };

            foreach (var tarjeta in tarjetas)
            {
                flowLayoutPanel1.Controls.Add(tarjeta.Panel);
            }
        }


        private void btnRefrescar_Click(object sender, EventArgs e)
        {



        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            fechaSeleccionada = dateTimePicker1.Value;
            CargarTarjetasParaFecha(fechaSeleccionada);
        }
    }
}
