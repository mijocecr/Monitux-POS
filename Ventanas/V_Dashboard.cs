
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
            Cargar_Datos();
        }


        private void Cargar_Datos()
        {


            flowLayoutPanel1.Controls.Clear();
            fechaSeleccionada = DateTime.Now.Date;
            DashboardService servicio = new DashboardService(V_Menu_Principal.Secuencial_Empresa);
            listBox1.DataSource = null;
            listBox1.Items.Clear();
            listBox1.DataSource= servicio.ObtenerActividadReciente();

            // --- Variaciones mensuales ---
            decimal vVentasMes = servicio.CalcularCambioPorcentualVentasMes();
            string textoVentasMes = vVentasMes != 0
                ? (vVentasMes > 0 ? $"↑ {vVentasMes:F1}% vs mes anterior" : $"↓ {Math.Abs(vVentasMes):F1}% vs mes anterior")
                : "Sin cambio";

            decimal vComprasMes = servicio.CalcularCambioPorcentualComprasMes();
            string textoComprasMes = vComprasMes != 0
                ? (vComprasMes > 0 ? $"↑ {vComprasMes:F1}% vs mes anterior" : $"↓ {Math.Abs(vComprasMes):F1}% vs mes anterior")
                : "Sin cambio";

            decimal vIngresosMes = servicio.CalcularCambioPorcentualIngresosMes();
            string textoIngresosMes = vIngresosMes != 0
                ? (vIngresosMes > 0 ? $"↑ {vIngresosMes:F1}% vs mes anterior" : $"↓ {Math.Abs(vIngresosMes):F1}% vs mes anterior")
                : "Sin cambio";

            decimal vEgresosMes = servicio.CalcularCambioPorcentualEgresosMes();
            string textoEgresosMes = vEgresosMes != 0
                ? (vEgresosMes > 0 ? $"↑ {vEgresosMes:F1}% vs mes anterior" : $"↓ {Math.Abs(vEgresosMes):F1}% vs mes anterior")
                : "Sin cambio";

            // --- Variaciones diarias ---
            decimal vVentas = servicio.CalcularCambioPorcentualVentas();
            string textoVentas = vVentas != 0
                ? (vVentas > 0 ? $"↑ {vVentas:F1}% vs ayer" : $"↓ {Math.Abs(vVentas):F1}% vs ayer")
                : "Sin cambio";

            decimal vCompras = servicio.CalcularCambioPorcentualCompras(fechaSeleccionada);
            string textoCompras = vCompras != 0
                ? (vCompras > 0 ? $"↑ {vCompras:F1}% vs ayer" : $"↓ {Math.Abs(vCompras):F1}% vs ayer")
                : "Sin cambio";

            // --- Tarjetas del Dashboard ---
            var tarjetas = new List<TarjetaDashboard>
{
    new TarjetaDashboard("Ventas Hoy", servicio.ObtenerTotalVentasHoy(DateTime.Today).ToString("C"), textoVentas, Properties.Resources.coins_add, Color.FromArgb(11, 8, 20), new Point(20, 20)),
    new TarjetaDashboard("Ventas Mes", servicio.ObtenerTotalVentasDelMes().ToString("C"), textoVentasMes, Properties.Resources.coins, Color.FromArgb(0, 168, 107), new Point(240, 20)),

    new TarjetaDashboard("Compras Hoy", servicio.ObtenerTotalComprasHoy(DateTime.Today).ToString("C"), textoCompras, Properties.Resources.coins_delete, Color.FromArgb(11, 8, 20), new Point(460, 20)),
    new TarjetaDashboard("Compras Mes", servicio.ObtenerTotalComprasDelMes().ToString("C"), textoComprasMes, Properties.Resources.coins, Color.FromArgb(0, 75, 160), new Point(680, 20)),

    new TarjetaDashboard("Ingresos Hoy", servicio.ObtenerTotalIngresosHoy().ToString("C"), "", Properties.Resources.chart_line_add, Color.FromArgb(11, 8, 20), new Point(20, 140)),
    new TarjetaDashboard("Ingresos Mes", servicio.ObtenerTotalIngresosDelMes().ToString("C"), textoIngresosMes, Properties.Resources.chart_line, Color.FromArgb(76, 154, 105), new Point(240, 140)),

    new TarjetaDashboard("Egresos Hoy", servicio.ObtenerTotalEgresosHoy().ToString("C"), "", Properties.Resources.chart_line_delete, Color.FromArgb(11, 8, 20), new Point(460, 140)),
    new TarjetaDashboard("Egresos Mes", servicio.ObtenerTotalEgresosDelMes().ToString("C"), textoEgresosMes, Properties.Resources.chart_line, Color.Firebrick, new Point(680, 140)),

    new TarjetaDashboard("Otros Ingresos", servicio.ObtenerOtrosIngresosHoy().ToString("C"), "", Properties.Resources.coins_add, Color.YellowGreen, new Point(20, 260)),
    new TarjetaDashboard("Otros Egresos", servicio.ObtenerOtrosEgresosHoy().ToString("C"), "", Properties.Resources.coins_delete, Color.DodgerBlue, new Point(240, 260)),
     new TarjetaDashboard("Agotados", servicio.ObtenerAgotados().ToString(), "", Properties.Resources.tag_red, Color.FromArgb(31, 31, 31), new Point(20, 380)),

    new TarjetaDashboard("Stock Crítico", servicio.ObtenerStockCritico().ToString(), "", Properties.Resources.tag_yellow, Color.FromArgb(31, 31, 31), new Point(460, 260)),
    new TarjetaDashboard("Productos Vencidos", servicio.ObtenerProductosVencidos().ToString(), "", Properties.Resources.bin, Color.FromArgb(31, 31, 31), new Point(680, 260)),

     new TarjetaDashboard("Por Cobrar Vencidas", servicio.ObtenerCuentasPorCobrarVencidas().Count.ToString(), "", Properties.Resources.add, Color.DarkSlateGray, new Point(240, 380)),
    new TarjetaDashboard("Por Pagar Vencidas", servicio.ObtenerCuentasPorPagarVencidas().Count.ToString(), "", Properties.Resources.delete1, Color.DarkSlateBlue, new Point(460, 380))
};

            // --- Renderizado en el panel ---
            foreach (var tarjeta in tarjetas)
            {
                var tarjetaActual = tarjeta;
                flowLayoutPanel1.Controls.Add(tarjetaActual.Panel);

                tarjetaActual.Panel.Click += (s, eArgs) => EjecutarAccionTarjeta(tarjetaActual.Titulo);

                foreach (Control control in tarjetaActual.Panel.Controls)
                {
                    control.Click += (s, eArgs) => EjecutarAccionTarjeta(tarjetaActual.Titulo);
                }
            }

            Cargar_Destacados();





        }

        private void Cargar_Destacados()
        {


            using (var db = new Monitux_DB_Context())
            {
                var destacados = db.Clientes
                    .AsEnumerable() // fuerza evaluación en memoria para poder usar DateTime.Parse
                    .Select(c =>
                    {
                        var ventasCliente = db.Ventas
                            .AsEnumerable()
                            .Where(v => v.Secuencial_Cliente == c.Secuencial);

                        var ventasUltimos30Dias = ventasCliente
                            .Where(v =>
                            {
                                DateTime fecha;
                                return DateTime.TryParse(v.Fecha, out fecha) &&
                                       fecha >= DateTime.Today.AddDays(-30);
                            });

                        var comprasTotales = ventasUltimos30Dias.Sum(v => v.Total);
                        var numeroTransacciones = ventasUltimos30Dias.Count();

                        DateTime ultimaCompra = ventasCliente
                            .Select(v =>
                            {
                                DateTime fecha;
                                return DateTime.TryParse(v.Fecha, out fecha) ? fecha : DateTime.MinValue;
                            })
                            .OrderByDescending(f => f)
                            .FirstOrDefault();

                        return new
                        {
                            Nombre = c.Nombre,
                            Telefono = c.Telefono,
                            ComprasTotales = comprasTotales,
                            NumeroTransacciones = numeroTransacciones,
                            UltimaCompra = ultimaCompra != DateTime.MinValue ? (DateTime?)ultimaCompra : null
                        };
                    })
                    .OrderByDescending(c => c.ComprasTotales)
                    .Take(5)
                    .ToList();

                dataGridViewClientesDestacados.DataSource = destacados;
                /*
                                // 🥇 Datos del cliente top (Kelian Daniela F. por ejemplo)
                                var clienteTop = destacados.FirstOrDefault();


                                // 🥇 Cliente destacado
                                string titulo = $"🥇 Cliente #1 del Mes: \n{clienteTop.Nombre}";
                                string valorPrincipal = $"{V_Menu_Principal.moneda}{clienteTop.ComprasTotales:N2}";
                                string variacion = $"Compras: {clienteTop.NumeroTransacciones} | Última: {clienteTop.UltimaCompra?.ToString("dd/MM/yyyy") ?? "N/D"}";

                                Image icono = Properties.Resources.coins_add; // Opcional, reemplaza por algo más icónico
                                Color fondo = Color.FromArgb(26, 32, 44); // Fondo más neutral y elegante
                                Point ubicacion = new Point(15, 15);

                                var tarjeta = new TarjetaDashboard(titulo, valorPrincipal, variacion, icono, fondo, ubicacion);

                                // 🌈 Fuentes y colores mejorados
                                tarjeta.LabelTitulo.Font = new Font("Segoe UI", 14, FontStyle.Bold);
                                tarjeta.LabelTitulo.ForeColor = Color.White;

                                tarjeta.LabelValor.Font = new Font("Segoe UI", 18, FontStyle.Bold);
                                tarjeta.LabelValor.ForeColor = Color.Gold;

                                tarjeta.LabelVariacion.Font = new Font("Segoe UI", 11, FontStyle.Regular);
                                tarjeta.LabelVariacion.ForeColor = Color.Silver;

                                // 🔲 Tamaño y espacio interior
                                tarjeta.Panel.Size = new Size(380, 130);
                                tarjeta.Panel.Padding = new Padding(15); // Espacio interior
                                tarjeta.Panel.BorderStyle = BorderStyle.FixedSingle; // Opcional: resalta los bordes

                                // 🎯 Acción interactiva
                                tarjeta.AsignarClickComun((s, e) =>
                                {
                                    // AbrirHistorialCliente(clienteTop.Secuencial);
                                });

                                // 📌 Agregar al panel
                                panelClientes.Controls.Add(tarjeta.Panel);



                                // 🥈 Datos del cliente #2
                                var clienteSegundo = destacados.Skip(1).FirstOrDefault();
                                if (clienteSegundo != null)
                                {
                                    string titulo2 = $"🥈 Cliente #2 del Mes: \n{clienteSegundo.Nombre}";
                                    string valorPrincipal2 = $"{V_Menu_Principal.moneda}{clienteSegundo.ComprasTotales:N2}";
                                    string variacion2 = $"Compras: {clienteSegundo.NumeroTransacciones} | Última: {clienteSegundo.UltimaCompra?.ToString("dd/MM/yyyy") ?? "N/D"}";

                                    Image icono2 = Properties.Resources.coins_add; // Cambia por otro ícono simbólico
                                    Color fondo2 = Color.FromArgb(36, 42, 60); // Fondo similar pero diferente tono
                                    Point ubicacion2 = new Point(15, 155); // Posición más abajo

                                    var tarjeta2 = new TarjetaDashboard(titulo2, valorPrincipal2, variacion2, icono2, fondo2, ubicacion2);

                                    // Estilo visual para cliente #2
                                    tarjeta2.LabelTitulo.Font = new Font("Segoe UI", 13, FontStyle.Bold);
                                    tarjeta2.LabelTitulo.ForeColor = Color.WhiteSmoke;

                                    tarjeta2.LabelValor.Font = new Font("Segoe UI", 17, FontStyle.Bold);
                                    tarjeta2.LabelValor.ForeColor = Color.Silver;

                                    tarjeta2.LabelVariacion.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                                    tarjeta2.LabelVariacion.ForeColor = Color.LightGray;

                                    tarjeta2.Panel.Size = new Size(380, 130);
                                    tarjeta2.Panel.Padding = new Padding(15);
                                    tarjeta2.Panel.BorderStyle = BorderStyle.FixedSingle;

                                    tarjeta2.AsignarClickComun((s, e) =>
                                    {
                                        //AbrirHistorialCliente(clienteSegundo.Secuencial);
                                    });

                                    panelClientes.Controls.Add(tarjeta2.Panel);
                                }




                                // 🥉 Obtener el cliente en tercera posición
                                var clienteTercero = destacados.Skip(2).FirstOrDefault();
                                if (clienteTercero != null)
                                {
                                    string titulo3 = $"🥉 Cliente #3 del Mes: \n{clienteTercero.Nombre}";
                                    string valorPrincipal3 = $"{V_Menu_Principal.moneda}{clienteTercero.ComprasTotales:N2}";
                                    string variacion3 = $"Compras: {clienteTercero.NumeroTransacciones} | Última: {clienteTercero.UltimaCompra?.ToString("dd/MM/yyyy") ?? "N/D"}";

                                    Image icono3 = Properties.Resources.box_down; // Ícono alternativo tipo gráfico 📊
                                    Color fondo3 = Color.FromArgb(46, 50, 66); // Otro tono para distinguir
                                    Point ubicacion3 = new Point(15, 295); // Espaciado más abajo

                                    var tarjeta3 = new TarjetaDashboard(titulo3, valorPrincipal3, variacion3, icono3, fondo3, ubicacion3);

                                    // ✨ Estilo personalizado
                                    tarjeta3.LabelTitulo.Font = new Font("Segoe UI", 13, FontStyle.Bold);
                                    tarjeta3.LabelTitulo.ForeColor = Color.WhiteSmoke;

                                    tarjeta3.LabelValor.Font = new Font("Segoe UI", 17, FontStyle.Bold);
                                    tarjeta3.LabelValor.ForeColor = Color.DarkOrange;

                                    tarjeta3.LabelVariacion.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                                    tarjeta3.LabelVariacion.ForeColor = Color.LightGray;

                                    tarjeta3.Panel.Size = new Size(380, 130);
                                    tarjeta3.Panel.Padding = new Padding(15);
                                    tarjeta3.Panel.BorderStyle = BorderStyle.FixedSingle;

                                    tarjeta3.AsignarClickComun((s, e) =>
                                    {
                                       // AbrirHistorialCliente(clienteTercero.Secuencial);
                                    });

                                    panelClientes.Controls.Add(tarjeta3.Panel);
                                }




                                */

                /*

                string[] titulos = { "🥇 Cliente #1", "🥈 Cliente #2", "🥉 Cliente #3" };
                Color[] fondos = {
    Color.FromArgb(26, 32, 44), // Azul oscuro para el #1
    Color.FromArgb(36, 42, 60), // Tono plata para el #2
    Color.FromArgb(46, 50, 66)  // Tono bronce para el #3
};
                Color[] coloresValor = { Color.Gold, Color.Silver, Color.DarkOrange };
                Image[] iconos = {
    Properties.Resources.coins_add,
    Properties.Resources.coins_delete,
    Properties.Resources.bin
};

                int alturaBase = 15;
                int separacionVertical = 140;

                for (int i = 0; i < Math.Min(3, destacados.Count); i++)
                {
                    var cliente = destacados[i];

                    string titulo = $"{titulos[i]} del Mes: {cliente.Nombre}";
                    string valorPrincipal = $"{V_Menu_Principal.moneda}{cliente.ComprasTotales:N2}";
                    string variacion = $"Compras: {cliente.NumeroTransacciones} | Última: {cliente.UltimaCompra?.ToString("dd/MM/yyyy") ?? "N/D"}";
                    Point ubicacion = new Point(15, alturaBase + i * separacionVertical);

                    var tarjeta = new TarjetaDashboard(titulo, valorPrincipal, variacion, iconos[i], fondos[i], ubicacion);

                    tarjeta.LabelTitulo.Font = new Font("Segoe UI", 13, FontStyle.Bold);
                    tarjeta.LabelTitulo.ForeColor = Color.White;

                    tarjeta.LabelValor.Font = new Font("Segoe UI", 17, FontStyle.Bold);
                    tarjeta.LabelValor.ForeColor = coloresValor[i];

                    tarjeta.LabelVariacion.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                    tarjeta.LabelVariacion.ForeColor = Color.LightGray;

                    tarjeta.Panel.Size = new Size(380, 130);
                    tarjeta.Panel.Padding = new Padding(15);
                    tarjeta.Panel.BorderStyle = BorderStyle.FixedSingle;

                    tarjeta.AsignarClickComun((s, e) =>
                    {
                       //AbrirHistorialCliente(cliente.Secuencial);
                    });

                    panelClientes.Controls.Add(tarjeta.Panel);
                }
                */



                string[] titulos = { "🥇 Cliente #1", "🥈 Cliente #2", "🥉 Cliente #3" };
                Color[] fondos = {
    Color.FromArgb(26, 32, 44),
    Color.FromArgb(36, 42, 60),
    Color.FromArgb(46, 50, 66)
};
                Color[] coloresValor = { Color.Gold, Color.Silver, Color.DarkOrange };
                Image[] iconos = {
    Properties.Resources.oro,
    Properties.Resources.plata,
    Properties.Resources.bronze
};

                int alturaBase = 10;
                int separacionVertical = 90;

                for (int i = 0; i < Math.Min(3, destacados.Count); i++)
                {
                    var cliente = destacados[i];

                    string titulo = $"{cliente.Nombre}";//{titulos[i]} del Mes: 
                    string valorPrincipal = $"{V_Menu_Principal.moneda}{cliente.ComprasTotales:N2}";
                    string variacion = $"Compras: {cliente.NumeroTransacciones} | Última: {cliente.UltimaCompra?.ToString("dd/MM/yyyy") ?? "N/D"}";
                    Point ubicacion = new Point(10, alturaBase + i * separacionVertical);

                    var tarjeta = new TarjetaDashboard(titulo, valorPrincipal, variacion, iconos[i], fondos[i], ubicacion);

                    // Tipografía compacta
                    tarjeta.LabelTitulo.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                    tarjeta.LabelTitulo.ForeColor = Color.White;

                    tarjeta.LabelValor.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                    tarjeta.LabelValor.ForeColor = coloresValor[i];

                    tarjeta.LabelVariacion.Font = new Font("Segoe UI", 8, FontStyle.Regular);
                    tarjeta.LabelVariacion.ForeColor = Color.LightGray;

                    // 🧱 Tamaño más pequeño
                    tarjeta.Panel.Size = new Size(250, 80);
                    tarjeta.Panel.Padding = new Padding(8);
                    tarjeta.Panel.BorderStyle = BorderStyle.FixedSingle;

                    tarjeta.AsignarClickComun((s, e) =>
                    {
                        //AbrirHistorialCliente(cliente.Secuencial);
                    });

                    panelClientes.Controls.Add(tarjeta.Panel);
                }




            }



        }


        void EjecutarAccionTarjeta(string titulo)
        {
            var servicio = new DashboardService(V_Menu_Principal.Secuencial_Empresa);

            switch (titulo)
            {
                case "Agotados":
                    var agotados = servicio.ObtenerProductosAgotados();
                    MostrarProductosAgotados(V_Menu_Principal.Secuencial_Empresa);
                    break;

                case "Productos Vencidos":
                    var vencidos = servicio.ObtenerProductosVencidosLista();
                    MostrarProductosVencidos(V_Menu_Principal.Secuencial_Empresa);
                    break;

                case "Stock Crítico":
                    var criticos = servicio.ObtenerProductosConStockCriticoLista();
                    MostrarStockCritico(V_Menu_Principal.Secuencial_Empresa);
                    break;

                case "Por Pagar Vencidas":
                    var cuentasPagarVencidas = servicio.ObtenerCuentasPorPagarVencidas();
                    MostrarCuentasPorPagarVencidas(V_Menu_Principal.Secuencial_Empresa);
                    break;

                case "Por Cobrar Vencidas":
                    var cuentasCobrarVencidas = servicio.ObtenerCuentasPorCobrarVencidas();
                    MostrarCuentasPorCobrarVencidas(V_Menu_Principal.Secuencial_Empresa);
                    break;
            }
        }



        private void MostrarProductosAgotados(int secuencialEmpresa)
        {
            var agotados = new DashboardService(secuencialEmpresa).ObtenerProductosAgotados();

            var ventana = new Form
            {
                Text = "Productos Agotados",
                Size = new Size(600, 400)
            };

            var grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                DataSource = agotados.ToList(),
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.LightGray,
                    ForeColor = Color.Black
                },

            };
            ventana.ShowIcon = false;
            ventana.ShowInTaskbar = false;
            ventana.StartPosition = FormStartPosition.CenterParent;
            ventana.FormBorderStyle = FormBorderStyle.FixedDialog;
            ventana.MaximizeBox = false;
            ventana.MinimizeBox = false;
            ventana.Controls.Add(grid);
            ventana.ShowDialog();
        }




        private void MostrarProductosVencidos(int secuencialEmpresa)
        {
            var vencidos = new DashboardService(secuencialEmpresa).ObtenerProductosVencidosLista();

            var ventana = new Form
            {
                Text = "Productos Vencidos",
                Size = new Size(600, 400),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                ShowIcon = false,
                ShowInTaskbar = false
            };

            var grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                DataSource = vencidos.ToList(),
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.MistyRose,
                    ForeColor = Color.Black
                }
            };

            ventana.Controls.Add(grid);
            ventana.ShowDialog();
        }





        private void MostrarStockCritico(int secuencialEmpresa)
        {
            var critico = new DashboardService(secuencialEmpresa).ObtenerProductosConStockCriticoLista();

            var ventana = new Form
            {
                Text = "Stock Crítico",
                Size = new Size(600, 400),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                ShowIcon = false,
                ShowInTaskbar = false
            };

            var grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                DataSource = critico.ToList(),
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.LemonChiffon,
                    ForeColor = Color.Black
                }
            };

            ventana.Controls.Add(grid);
            ventana.ShowDialog();
        }





        private void MostrarCuentasPorCobrarVencidas(int secuencialEmpresa)
        {
            var vencidasCobrar = new DashboardService(secuencialEmpresa).ObtenerCuentasPorCobrarVencidasConCliente();

            var ventana = new Form
            {
                Text = "Cuentas Por Cobrar Vencidas",
                Size = new Size(600, 400),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                ShowIcon = false,
                ShowInTaskbar = false
            };

            var grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                DataSource = vencidasCobrar.ToList(),
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.LightPink,
                    ForeColor = Color.Black
                }
            };

            ventana.Controls.Add(grid);
            ventana.ShowDialog();
        }




        private void MostrarCuentasPorPagarVencidas(int secuencialEmpresa)
        {
            var vencidasPagar = new DashboardService(secuencialEmpresa).ObtenerCuentasPorPagarVencidasConProveedor();

            var ventana = new Form
            {
                Text = "Cuentas Por Pagar Vencidas",
                Size = new Size(600, 400),
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                ShowIcon = false,
                ShowInTaskbar = false
            };

            var grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                DataSource = vencidasPagar.ToList(),
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.LightSteelBlue,
                    ForeColor = Color.Black
                }
            };

            ventana.Controls.Add(grid);
            ventana.ShowDialog();
        }





        private void CargarTarjetasParaFecha(DateTime fecha)
        {
            listBox1.DataSource = null;

            DashboardService servicio = new DashboardService(V_Menu_Principal.Secuencial_Empresa);

            listBox1.Items.Clear();
            listBox1.DataSource = servicio.ObtenerActividadReciente();
            flowLayoutPanel1.Controls.Clear();

            fechaSeleccionada = fecha.Date;
            string fechaCorta = fechaSeleccionada.ToString("dd/MM/yyyy");

            // --- Variaciones mensuales corregidas ---
            decimal vVentasMes = servicio.CalcularCambioPorcentualVentasMes(fechaSeleccionada);
            string textoVentasMes = vVentasMes != 0
                ? (vVentasMes > 0 ? $"↑ {vVentasMes:F1}% vs mes anterior" : $"↓ {Math.Abs(vVentasMes):F1}% vs mes anterior")
                : "Sin cambio";

            decimal vComprasMes = servicio.CalcularCambioPorcentualComprasMes(fechaSeleccionada);
            string textoComprasMes = vComprasMes != 0
                ? (vComprasMes > 0 ? $"↑ {vComprasMes:F1}% vs mes anterior" : $"↓ {Math.Abs(vComprasMes):F1}% vs mes anterior")
                : "Sin cambio";

            decimal vIngresosMes = servicio.CalcularCambioPorcentualIngresosMes(fechaSeleccionada);
            string textoIngresosMes = vIngresosMes != 0
                ? (vIngresosMes > 0 ? $"↑ {vIngresosMes:F1}% vs mes anterior" : $"↓ {Math.Abs(vIngresosMes):F1}% vs mes anterior")
                : "Sin cambio";

            decimal vEgresosMes = servicio.CalcularCambioPorcentualEgresosMes(fechaSeleccionada);
            string textoEgresosMes = vEgresosMes != 0
                ? (vEgresosMes > 0 ? $"↑ {vEgresosMes:F1}% vs mes anterior" : $"↓ {Math.Abs(vEgresosMes):F1}% vs mes anterior")
                : "Sin cambio";

            // --- Variaciones diarias ---
            decimal vVentas = servicio.CalcularCambioPorcentualVentas(); // Este aún compara con ayer (puedes hacer uno por fecha también si quieres)
            string textoVentas = vVentas != 0
                ? (vVentas > 0 ? $"↑ {vVentas:F1}% vs ayer" : $"↓ {Math.Abs(vVentas):F1}% vs ayer")
                : "Sin cambio";

            decimal vCompras = servicio.CalcularCambioPorcentualCompras(fechaSeleccionada);
            string textoCompras = vCompras != 0
                ? (vCompras > 0 ? $"↑ {vCompras:F1}% vs ayer" : $"↓ {Math.Abs(vCompras):F1}% vs ayer")
                : "Sin cambio";

            // --- Tarjetas del Dashboard ---
            var tarjetas = new List<TarjetaDashboard>
    {
        new TarjetaDashboard("Ventas Hoy", servicio.ObtenerTotalVentasHoy(fechaSeleccionada).ToString("C"), textoVentas, Properties.Resources.coins_add, Color.FromArgb(11, 8, 20), new Point(20, 20)),
        new TarjetaDashboard("Ventas Mes", servicio.ObtenerTotalVentasPorRango(new DateTime(fechaSeleccionada.Year, fechaSeleccionada.Month, 1), fechaSeleccionada).ToString("C"), textoVentasMes, Properties.Resources.coins, Color.FromArgb(0, 168, 107), new Point(240, 20)),

        new TarjetaDashboard("Compras Hoy", servicio.ObtenerTotalComprasHoy(fechaSeleccionada).ToString("C"), textoCompras, Properties.Resources.coins_delete, Color.FromArgb(11, 8, 20), new Point(460, 20)),
        new TarjetaDashboard("Compras Mes", servicio.ObtenerTotalComprasPorRango(new DateTime(fechaSeleccionada.Year, fechaSeleccionada.Month, 1), fechaSeleccionada).ToString("C"), textoComprasMes, Properties.Resources.coins, Color.FromArgb(0, 75, 160), new Point(680, 20)),

        new TarjetaDashboard("Ingresos Hoy", servicio.ObtenerTotalIngresosHoy(fechaSeleccionada).ToString("C"), "", Properties.Resources.chart_line_add, Color.FromArgb(11, 8, 20), new Point(20, 140)),
        new TarjetaDashboard("Ingresos Mes", servicio.ObtenerTotalIngresosPorRango(new DateTime(fechaSeleccionada.Year, fechaSeleccionada.Month, 1), fechaSeleccionada).ToString("C"), textoIngresosMes, Properties.Resources.chart_line, Color.FromArgb(76, 154, 105), new Point(240, 140)),

        new TarjetaDashboard("Egresos Hoy", servicio.ObtenerTotalEgresosHoy(fechaSeleccionada).ToString("C"), "", Properties.Resources.chart_line_delete, Color.FromArgb(11, 8, 20), new Point(460, 140)),
        new TarjetaDashboard("Egresos Mes", servicio.ObtenerTotalEgresosPorRango(new DateTime(fechaSeleccionada.Year, fechaSeleccionada.Month, 1), fechaSeleccionada).ToString("C"), textoEgresosMes, Properties.Resources.chart_line, Color.Firebrick, new Point(680, 140)),

        new TarjetaDashboard("Otros Ingresos", servicio.ObtenerOtrosIngresosHoy(fechaSeleccionada).ToString("C"), "", Properties.Resources.coins_add, Color.YellowGreen, new Point(20, 260)),
        new TarjetaDashboard("Otros Egresos", servicio.ObtenerOtrosEgresosHoy(fechaSeleccionada).ToString("C"), "", Properties.Resources.coins_delete, Color.DodgerBlue, new Point(240, 260)),

        new TarjetaDashboard("Agotados", servicio.ObtenerAgotados().ToString(), "", Properties.Resources.tag_red, Color.FromArgb(31, 31, 31), new Point(20, 380)),
        new TarjetaDashboard("Stock Crítico", servicio.ObtenerStockCritico().ToString(), "", Properties.Resources.tag_yellow, Color.FromArgb(31, 31, 31), new Point(460, 260)),
        new TarjetaDashboard("Productos Vencidos", servicio.ObtenerProductosVencidos().ToString(), "", Properties.Resources.bin, Color.FromArgb(31, 31, 31), new Point(680, 260)),

        new TarjetaDashboard("Por Cobrar Vencidas", servicio.ObtenerCuentasPorCobrarVencidas().Count.ToString(), "", Properties.Resources.add, Color.DarkSlateGray, new Point(240, 380)),
        new TarjetaDashboard("Por Pagar Vencidas", servicio.ObtenerCuentasPorPagarVencidas().Count.ToString(), "", Properties.Resources.delete1, Color.DarkSlateBlue, new Point(460, 380))
    };

            // --- Renderizado en el panel ---
            foreach (var tarjeta in tarjetas)
            {
                var tarjetaActual = tarjeta;
                flowLayoutPanel1.Controls.Add(tarjetaActual.Panel);

                tarjetaActual.Panel.Click += (s, eArgs) => EjecutarAccionTarjeta(tarjetaActual.Titulo);

                foreach (Control control in tarjetaActual.Panel.Controls)
                {
                    control.Click += (s, eArgs) => EjecutarAccionTarjeta(tarjetaActual.Titulo);
                }
            }

            Cargar_Destacados();
        }





        private void btnRefrescar_Click(object sender, EventArgs e)
        {



        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            fechaSeleccionada = dateTimePicker1.Value.Date;
            CargarTarjetasParaFecha(fechaSeleccionada);
        }





        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {



        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Cargar_Datos();
        }
    }
}
