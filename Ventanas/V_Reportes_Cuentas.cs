using Monitux_POS.Clases;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
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
    public partial class V_Reportes_Cuentas : Form
    {
        public string ruta = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Resources\\Reportes\\");
        public V_Reportes_Cuentas()
        {
            InitializeComponent();
        }


        public void llenar_Combo_Cliente()
        {


            combo_Cliente.Items.Clear();

            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            // Filtrar solo clientes activos
            var clientesActivos = context.Clientes.Where(c => (bool)c.Activo && c.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa).ToList();

            foreach (var item in clientesActivos)
            {
                combo_Cliente.Items.Add(item.Secuencial + " - " + item.Nombre);
            }



        }



        private void button2_Click(object sender, EventArgs e)
        {
            ///////////////////////
            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            int secuencialEmpresaActiva = V_Menu_Principal.Secuencial_Empresa;
            int secuencialClienteFiltrado = int.Parse(combo_Cliente.SelectedItem.ToString().Split('-')[0].Trim());

            // Obtener nombre del cliente
            string nombreCliente = context.Clientes
                .Where(c => c.Secuencial == secuencialClienteFiltrado)
                .Select(c => c.Nombre)
                .FirstOrDefault() ?? "Sin nombre";

            // Obtener cuentas por cobrar del cliente
            var cuentas = context.Cuentas_Cobrar
                .Where(c => c.Secuencial_Empresa == secuencialEmpresaActiva
                    && c.Secuencial_Cliente == secuencialClienteFiltrado)
                .ToList() // 👈 se trae a memoria antes de usar ParseExact
                .OrderBy(c => DateTime.ParseExact(c.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null))
                .ToList();

            decimal totalDeuda = (decimal)cuentas.Sum(c => c.Saldo);

            string nombreReporte = $"Cuentas_Cobrar_Cliente_{secuencialClienteFiltrado}.pdf";
            string rutaCompleta = $"{ruta}{nombreReporte}";

            Document.Create(doc =>
            {
                doc.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(9));

                    page.Header().Column(header =>
                    {
                        header.Item().Text("📑 Reporte de cuentas por cobrar").FontSize(20).Bold();
                        header.Item().Text($"Cliente: {nombreCliente} (Código: {secuencialClienteFiltrado})");
                        header.Item().Text($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}");
                    });

                    page.Content().Table(tabla =>
                    {
                        tabla.ColumnsDefinition(cols =>
                        {
                            cols.RelativeColumn(1); // Fecha
                            cols.RelativeColumn(2); // Referencia
                            cols.RelativeColumn(1); // Total
                            cols.RelativeColumn(1); // Pagado
                            cols.RelativeColumn(1); // Saldo
                        });

                        string[] titulos = { "Fecha", "Factura No.", "Total", "Pagado", "Saldo" };

                        tabla.Header(header =>
                        {
                            foreach (var titulo in titulos)
                            {
                                header.Cell().Element(c => c
                                    .Background(Colors.Grey.Lighten3)
                                    .PaddingVertical(4)
                                    .PaddingHorizontal(2)
                                    .ShowOnce())
                                    .Text(titulo).Bold();
                            }
                        });

                        foreach (var cuenta in cuentas)
                        {
                            tabla.Cell().Element(c => c
                                .PaddingVertical(3)
                                .BorderBottom(0.25f)
                                .BorderColor(Colors.Grey.Lighten2))
                                .Text(DateTime.ParseExact(cuenta.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null).ToString("dd/MM/yyyy"));

                            tabla.Cell().Text($"{cuenta.Secuencial_Factura}");
                            tabla.Cell().Text($"{cuenta.Total:N2}");
                            tabla.Cell().Text($"{cuenta.Pagado:N2}");
                            tabla.Cell().Text($"{cuenta.Saldo:N2}");
                        }

                        // Fila de resumen total
                        tabla.Cell().ColumnSpan(4).Element(c => c
                            .PaddingVertical(6)
                            .AlignRight())
                            .Text("🔢 Total deuda acumulada:").Bold();

                        tabla.Cell().Element(c => c
                            .Background(Colors.Grey.Lighten4)
                            .Padding(4))
                            .Text($"{totalDeuda:N2}").Bold();
                    });

                    page.Footer().AlignCenter()
                        .Text("Sistema Monitux-POS · Reporte generado automáticamente")
                        .FontSize(10)
                        .Italic();
                });
            })
            .GeneratePdf(rutaCompleta);

            V_Menu_Principal.MSG.ShowMSG("🖨️ Reporte generado correctamente", "Monitux-POS");

            var visor = new V_Visor_Factura
            {
                rutaArchivo = rutaCompleta
            };

            visor.ShowDialog();



            /////////////////////
        }



        public void llenar_Combo_Proveedor()
        {


            combo_Proveedor.Items.Clear();

            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            // Filtrar solo clientes activos
            var proveedoresActivos = context.Proveedores.Where(c => (bool)c.Activo && c.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa).ToList();

            foreach (var item in proveedoresActivos)
            {
                combo_Proveedor.Items.Add(item.Secuencial + " - " + item.Nombre);
            }



        }



        private void V_Reportes_Cuentas_Load(object sender, EventArgs e)
        {
            llenar_Combo_Cliente();
            llenar_Combo_Proveedor();
        }

        private void button6_Click(object sender, EventArgs e)
        {


            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            int secuencialEmpresaActiva = V_Menu_Principal.Secuencial_Empresa;
            int secuencialProveedorFiltrado = int.Parse(combo_Proveedor.SelectedItem.ToString().Split('-')[0].Trim());

            // Obtener nombre del proveedor
            string nombreProveedor = context.Proveedores
                .Where(p => p.Secuencial == secuencialProveedorFiltrado)
                .Select(p => p.Nombre)
                .FirstOrDefault() ?? "Sin nombre";

            // Obtener cuentas por pagar del proveedor
            var cuentas = context.Cuentas_Pagar
                .Where(p => p.Secuencial_Empresa == secuencialEmpresaActiva
                    && p.Secuencial_Proveedor == secuencialProveedorFiltrado)
                .ToList() // 👈 se trae a memoria antes de usar ParseExact
                .OrderBy(p => DateTime.ParseExact(p.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null))
                .ToList();

            decimal totalDeuda = (decimal)cuentas.Sum(p => p.Saldo);

            string nombreReporte = $"Cuentas_Pagar_Proveedor_{secuencialProveedorFiltrado}.pdf";
            string rutaCompleta = $"{ruta}{nombreReporte}";

            Document.Create(doc =>
            {
                doc.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(9));

                    page.Header().Column(header =>
                    {
                        header.Item().Text("💰 Reporte de cuentas por pagar").FontSize(20).Bold();
                        header.Item().Text($"Proveedor: {nombreProveedor} (Código: {secuencialProveedorFiltrado})");
                        header.Item().Text($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}");
                    });

                    page.Content().Table(tabla =>
                    {
                        tabla.ColumnsDefinition(cols =>
                        {
                            cols.RelativeColumn(1); // Fecha
                            cols.RelativeColumn(2); // Factura No.
                            cols.RelativeColumn(1); // Total
                            cols.RelativeColumn(1); // Pagado
                            cols.RelativeColumn(1); // Saldo
                        });

                        string[] titulos = { "Fecha", "Factura No.", "Total", "Pagado", "Saldo" };

                        tabla.Header(header =>
                        {
                            foreach (var titulo in titulos)
                            {
                                header.Cell().Element(c => c
                                    .Background(Colors.Grey.Lighten3)
                                    .PaddingVertical(4)
                                    .PaddingHorizontal(2)
                                    .ShowOnce())
                                    .Text(titulo).Bold();
                            }
                        });

                        foreach (var cuenta in cuentas)
                        {
                            tabla.Cell().Element(c => c
                                .PaddingVertical(3)
                                .BorderBottom(0.25f)
                                .BorderColor(Colors.Grey.Lighten2))
                                .Text(DateTime.ParseExact(cuenta.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null).ToString("dd/MM/yyyy"));

                            tabla.Cell().Text($"{cuenta.Secuencial_Factura}");
                            tabla.Cell().Text($"{cuenta.Total:N2}");
                            tabla.Cell().Text($"{cuenta.Pagado:N2}");
                            tabla.Cell().Text($"{cuenta.Saldo:N2}");
                        }

                        // Fila de resumen total
                        tabla.Cell().ColumnSpan(4).Element(c => c
                            .PaddingVertical(6)
                            .AlignRight())
                            .Text("🔢 Total deuda acumulada:").Bold();

                        tabla.Cell().Element(c => c
                            .Background(Colors.Grey.Lighten4)
                            .Padding(4))
                            .Text($"{totalDeuda:N2}").Bold();
                    });

                    page.Footer().AlignCenter()
                        .Text("Sistema Monitux-POS · Reporte generado automáticamente")
                        .FontSize(10)
                        .Italic();
                });
            })
            .GeneratePdf(rutaCompleta);

            V_Menu_Principal.MSG.ShowMSG("🖨️ Reporte generado correctamente", "Monitux-POS");

            var visor = new V_Visor_Factura
            {
                rutaArchivo = rutaCompleta
            };

            visor.ShowDialog();



        }

        private void button4_Click(object sender, EventArgs e)
        {
            //////////////
            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            int secuencialEmpresaActiva = V_Menu_Principal.Secuencial_Empresa;
            DateTime hoy = DateTime.Today;

            var cuentasVencidas = context.Cuentas_Cobrar
                .Where(c => c.Secuencial_Empresa == secuencialEmpresaActiva)
                .ToList()
                .Where(c => DateTime.TryParseExact(c.Fecha_Vencimiento?.Trim(), "dd/MM/yyyy", null,
                    System.Globalization.DateTimeStyles.None, out DateTime fechaVencimiento)
                    && fechaVencimiento < hoy
                    && c.Saldo > 0)
                .OrderBy(c => DateTime.ParseExact(c.Fecha_Vencimiento.Trim(), "dd/MM/yyyy", null)) // 👈 se repite el parse aquí
                .ToList();


            // Traer los nombres de los clientes relacionados
            var clientes = context.Clientes.ToList();
            var cuentasConNombre = cuentasVencidas
                .Join(clientes,
                    cuenta => cuenta.Secuencial_Cliente,
                    cliente => cliente.Secuencial,
                    (cuenta, cliente) => new
                    {
                        cliente.Nombre,
                        cuenta.Fecha,
                        cuenta.Fecha_Vencimiento,
                        cuenta.Secuencial_Factura,
                        cuenta.Total,
                        cuenta.Pagado,
                        cuenta.Saldo
                    })
                .ToList();

            decimal totalPendiente = (decimal)cuentasConNombre.Sum(c => c.Saldo);
            string nombreReporte = "Cuentas_Cobrar_Vencidas.pdf";
            string rutaCompleta = $"{ruta}{nombreReporte}";

            Document.Create(doc =>
            {
                doc.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(9));

                    page.Header().Column(header =>
                    {
                        header.Item().Text("📅 Reporte de cuentas por cobrar vencidas").FontSize(20).Bold();
                        header.Item().Text($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}");
                    });

                    page.Content().Table(tabla =>
                    {
                        tabla.ColumnsDefinition(cols =>
                        {
                            cols.RelativeColumn(2); // Cliente
                            cols.RelativeColumn(1); // Fecha
                            cols.RelativeColumn(1); // Vencimiento
                            cols.RelativeColumn(1); // Referencia
                            cols.RelativeColumn(1); // Total
                            cols.RelativeColumn(1); // Pagado
                            cols.RelativeColumn(1); // Saldo
                        });

                        string[] titulos = { "Cliente", "Fecha", "Vencimiento", "Factura No.", "Total", "Pagado", "Saldo" };

                        tabla.Header(header =>
                        {
                            foreach (var titulo in titulos)
                            {
                                header.Cell().Element(c => c
                                    .Background(Colors.Red.Lighten3)
                                    .PaddingVertical(4)
                                    .PaddingHorizontal(2)
                                    .ShowOnce())
                                    .Text(titulo).Bold();
                            }
                        });

                        foreach (var cuenta in cuentasConNombre)
                        {
                            tabla.Cell().Element(c => c
                                .BorderBottom(0.25f)
                                .BorderColor(Colors.Grey.Lighten2)
                                .Padding(2)).Text(cuenta.Nombre);
                            tabla.Cell().Text(DateTime.ParseExact(cuenta.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null).ToString("dd/MM/yyyy"));
                            tabla.Cell().Text(cuenta.Fecha_Vencimiento);
                            tabla.Cell().Text($"{cuenta.Secuencial_Factura}");
                            tabla.Cell().Text($"{cuenta.Total:N2}");
                            tabla.Cell().Text($"{cuenta.Pagado:N2}");
                            tabla.Cell().Text($"{cuenta.Saldo:N2}");
                        }

                        // Total final
                        tabla.Cell().ColumnSpan(6).Element(c => c
                            .PaddingVertical(6)
                            .AlignRight())
                            .Text("🔻 Total vencido acumulado:").Bold();

                        tabla.Cell().Element(c => c
                            .Background(Colors.Red.Lighten4)
                            .Padding(4))
                            .Text($"{totalPendiente:N2}").Bold();
                    });

                    page.Footer().AlignCenter()
                        .Text("Sistema Monitux-POS · Reporte generado automáticamente")
                        .FontSize(10)
                        .Italic();
                });
            })
            .GeneratePdf(rutaCompleta);

            V_Menu_Principal.MSG.ShowMSG("🖨️ Reporte de vencidos generado correctamente", "Monitux-POS");

            var visor = new V_Visor_Factura
            {
                rutaArchivo = rutaCompleta
            };

            visor.ShowDialog();

            /////////////
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //////////////////

            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            int secuencialEmpresaActiva = V_Menu_Principal.Secuencial_Empresa;
            DateTime hoy = DateTime.Today;

            // Traer las cuentas por pagar vencidas
            var cuentasVencidas = context.Cuentas_Pagar
                .Where(p => p.Secuencial_Empresa == secuencialEmpresaActiva)
                .ToList()
                .Where(p => DateTime.TryParseExact(p.Fecha_Vencimiento?.Trim(), "dd/MM/yyyy", null,
                    System.Globalization.DateTimeStyles.None, out DateTime fechaVencimiento)
                    && fechaVencimiento < hoy
                    && p.Saldo > 0)
                .OrderBy(p => DateTime.ParseExact(p.Fecha_Vencimiento.Trim(), "dd/MM/yyyy", null))
                .ToList();

            // Traer los nombres de los proveedores relacionados
            var proveedores = context.Proveedores.ToList();

            var cuentasConNombre = cuentasVencidas
                .Join(proveedores,
                    cuenta => cuenta.Secuencial_Proveedor,
                    proveedor => proveedor.Secuencial,
                    (cuenta, proveedor) => new
                    {
                        proveedor.Nombre,
                        cuenta.Fecha,
                        cuenta.Fecha_Vencimiento,
                        cuenta.Secuencial_Factura,
                        cuenta.Total,
                        cuenta.Pagado,
                        cuenta.Saldo
                    })
                .ToList();

            decimal totalPendiente = (decimal)cuentasConNombre.Sum(p => p.Saldo);
            string nombreReporte = "Cuentas_Pagar_Vencidas.pdf";
            string rutaCompleta = $"{ruta}{nombreReporte}";

            Document.Create(doc =>
            {
                doc.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(9));

                    page.Header().Column(header =>
                    {
                        header.Item().Text("📅 Reporte de cuentas por pagar vencidas").FontSize(20).Bold();
                        header.Item().Text($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}");
                    });

                    page.Content().Table(tabla =>
                    {
                        tabla.ColumnsDefinition(cols =>
                        {
                            cols.RelativeColumn(2); // Proveedor
                            cols.RelativeColumn(1); // Fecha
                            cols.RelativeColumn(1); // Vencimiento
                            cols.RelativeColumn(1); // Factura No.
                            cols.RelativeColumn(1); // Total
                            cols.RelativeColumn(1); // Pagado
                            cols.RelativeColumn(1); // Saldo
                        });

                        string[] titulos = { "Proveedor", "Fecha", "Vencimiento", "Factura No.", "Total", "Pagado", "Saldo" };

                        tabla.Header(header =>
                        {
                            foreach (var titulo in titulos)
                            {
                                header.Cell().Element(c => c
                                    .Background(Colors.Red.Lighten3)
                                    .PaddingVertical(4)
                                    .PaddingHorizontal(2)
                                    .ShowOnce())
                                    .Text(titulo).Bold();
                            }
                        });

                        foreach (var cuenta in cuentasConNombre)
                        {
                            tabla.Cell().Element(c => c
                                .BorderBottom(0.25f)
                                .BorderColor(Colors.Grey.Lighten2)
                                .Padding(2)).Text(cuenta.Nombre);
                            tabla.Cell().Text(DateTime.ParseExact(cuenta.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null).ToString("dd/MM/yyyy"));
                            tabla.Cell().Text(cuenta.Fecha_Vencimiento);
                            tabla.Cell().Text($"{cuenta.Secuencial_Factura}");
                            tabla.Cell().Text($"{cuenta.Total:N2}");
                            tabla.Cell().Text($"{cuenta.Pagado:N2}");
                            tabla.Cell().Text($"{cuenta.Saldo:N2}");
                        }

                        // Total final
                        tabla.Cell().ColumnSpan(6).Element(c => c
                            .PaddingVertical(6)
                            .AlignRight())
                            .Text("🔻 Total vencido acumulado:").Bold();

                        tabla.Cell().Element(c => c
                            .Background(Colors.Red.Lighten4)
                            .Padding(4))
                            .Text($"{totalPendiente:N2}").Bold();
                    });

                    page.Footer().AlignCenter()
                        .Text("Sistema Monitux-POS · Reporte generado automáticamente")
                        .FontSize(10)
                        .Italic();
                });
            })
            .GeneratePdf(rutaCompleta);

            V_Menu_Principal.MSG.ShowMSG("🖨️ Reporte de vencidos generado correctamente", "Monitux-POS");

            var visor = new V_Visor_Factura
            {
                rutaArchivo = rutaCompleta
            };

            visor.ShowDialog();


            /////////////////
        }

        private void button5_Click(object sender, EventArgs e)
        {

            //////////////////////////


            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            int secuencialEmpresaActiva = V_Menu_Principal.Secuencial_Empresa;

            // Fechas seleccionadas por el usuario
            DateTime fechaInicio = dateTimePicker1.Value.Date;
            DateTime fechaFin = dateTimePicker2.Value.Date.AddDays(1).AddSeconds(-1); // incluye todo el día final

            var cuentasFiltradas = context.Cuentas_Pagar
     .Where(p => p.Secuencial_Empresa == secuencialEmpresaActiva)
     .ToList() // 👈 se trae a memoria antes de usar funciones .NET
     .Where(p => DateTime.TryParseExact(p.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null,
         System.Globalization.DateTimeStyles.None, out DateTime _) // usamos _ porque no lo necesitas fuera
         && DateTime.ParseExact(p.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null) >= fechaInicio
         && DateTime.ParseExact(p.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null) <= fechaFin)
     .OrderBy(p => DateTime.ParseExact(p.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null)) // 👈 usamos parse directo aquí
     .ToList();


            // Traer nombres de proveedores
            var proveedores = context.Proveedores.ToList();

            var cuentasConNombre = cuentasFiltradas
                .Join(proveedores,
                    cuenta => cuenta.Secuencial_Proveedor,
                    proveedor => proveedor.Secuencial,
                    (cuenta, proveedor) => new
                    {
                        proveedor.Nombre,
                        cuenta.Fecha,
                        cuenta.Fecha_Vencimiento,
                        cuenta.Secuencial_Factura,
                        cuenta.Total,
                        cuenta.Pagado,
                        cuenta.Saldo
                    })
                .ToList();

            decimal totalDeuda = (decimal)cuentasConNombre.Sum(p => p.Saldo);
            string nombreReporte = $"Cuentas_Pagar_Rango_{fechaInicio:ddMMyyyy}_a_{fechaFin:ddMMyyyy}.pdf";
            string rutaCompleta = $"{ruta}{nombreReporte}";

            Document.Create(doc =>
            {
                doc.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(9));

                    page.Header().Column(header =>
                    {
                        header.Item().Text("📆 Reporte de cuentas por pagar en rango de fechas").FontSize(20).Bold();
                        header.Item().Text($"Desde: {fechaInicio:dd/MM/yyyy}  Hasta: {fechaFin:dd/MM/yyyy}");
                        header.Item().Text($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}");
                    });

                    page.Content().Table(tabla =>
                    {
                        tabla.ColumnsDefinition(cols =>
                        {
                            cols.RelativeColumn(2); // Proveedor
                            cols.RelativeColumn(1); // Fecha
                            cols.RelativeColumn(1); // Vencimiento
                            cols.RelativeColumn(1); // Factura No.
                            cols.RelativeColumn(1); // Total
                            cols.RelativeColumn(1); // Pagado
                            cols.RelativeColumn(1); // Saldo
                        });

                        string[] titulos = { "Proveedor", "Fecha", "Vencimiento", "Factura No.", "Total", "Pagado", "Saldo" };

                        tabla.Header(header =>
                        {
                            foreach (var titulo in titulos)
                            {
                                header.Cell().Element(c => c
                                    .Background(Colors.Grey.Lighten3)
                                    .PaddingVertical(4)
                                    .PaddingHorizontal(2)
                                    .ShowOnce())
                                    .Text(titulo).Bold();
                            }
                        });

                        foreach (var cuenta in cuentasConNombre)
                        {
                            tabla.Cell().Element(c => c
                                .BorderBottom(0.25f)
                                .BorderColor(Colors.Grey.Lighten2)
                                .Padding(2)).Text(cuenta.Nombre);
                            tabla.Cell().Text(DateTime.ParseExact(cuenta.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null).ToString("dd/MM/yyyy"));
                            tabla.Cell().Text(cuenta.Fecha_Vencimiento);
                            tabla.Cell().Text($"{cuenta.Secuencial_Factura}");
                            tabla.Cell().Text($"{cuenta.Total:N2}");
                            tabla.Cell().Text($"{cuenta.Pagado:N2}");
                            tabla.Cell().Text($"{cuenta.Saldo:N2}");
                        }

                        tabla.Cell().ColumnSpan(6).Element(c => c
                            .PaddingVertical(6)
                            .AlignRight())
                            .Text("💰 Total en el rango:").Bold();

                        tabla.Cell().Element(c => c
                            .Background(Colors.Grey.Lighten4)
                            .Padding(4))
                            .Text($"{totalDeuda:N2}").Bold();
                    });

                    page.Footer().AlignCenter()
                        .Text("Sistema Monitux-POS · Reporte generado automáticamente")
                        .FontSize(10)
                        .Italic();
                });
            })
            .GeneratePdf(rutaCompleta);

            V_Menu_Principal.MSG.ShowMSG("🖨️ Reporte generado correctamente", "Monitux-POS");

            var visor = new V_Visor_Factura
            {
                rutaArchivo = rutaCompleta
            };

            visor.ShowDialog();



            /////////////////////////



        }

        private void button1_Click(object sender, EventArgs e)
        {



            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            int secuencialEmpresaActiva = V_Menu_Principal.Secuencial_Empresa;

            // Fechas seleccionadas por el usuario
            DateTime fechaInicio = dateTimePicker1.Value.Date;
            DateTime fechaFin = dateTimePicker2.Value.Date.AddDays(1).AddSeconds(-1); // incluye todo el día final

            var cuentasFiltradas = context.Cuentas_Cobrar
                .Where(c => c.Secuencial_Empresa == secuencialEmpresaActiva)
                .ToList() // 👈 se trae a memoria antes de usar funciones .NET
                .Where(c => DateTime.TryParseExact(c.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null,
                    System.Globalization.DateTimeStyles.None, out DateTime _) // usamos _ porque no lo necesitas fuera
                    && DateTime.ParseExact(c.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null) >= fechaInicio
                    && DateTime.ParseExact(c.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null) <= fechaFin)
                .OrderBy(c => DateTime.ParseExact(c.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null))
                .ToList();

            // Traer nombres de clientes
            var clientes = context.Clientes.ToList();

            var cuentasConNombre = cuentasFiltradas
                .Join(clientes,
                    cuenta => cuenta.Secuencial_Cliente,
                    cliente => cliente.Secuencial,
                    (cuenta, cliente) => new
                    {
                        cliente.Nombre,
                        cuenta.Fecha,
                        cuenta.Fecha_Vencimiento,
                        cuenta.Secuencial_Factura,
                        cuenta.Total,
                        cuenta.Pagado,
                        cuenta.Saldo
                    })
                .ToList();

            decimal totalDeuda = (decimal)cuentasConNombre.Sum(c => c.Saldo);
            string nombreReporte = $"Cuentas_Cobrar_Rango_{fechaInicio:ddMMyyyy}_a_{fechaFin:ddMMyyyy}.pdf";
            string rutaCompleta = $"{ruta}{nombreReporte}";

            Document.Create(doc =>
            {
                doc.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(9));

                    page.Header().Column(header =>
                    {
                        header.Item().Text("📆 Reporte de cuentas por cobrar en rango de fechas").FontSize(20).Bold();
                        header.Item().Text($"Desde: {fechaInicio:dd/MM/yyyy}  Hasta: {fechaFin:dd/MM/yyyy}");
                        header.Item().Text($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}");
                    });

                    page.Content().Table(tabla =>
                    {
                        tabla.ColumnsDefinition(cols =>
                        {
                            cols.RelativeColumn(2); // Cliente
                            cols.RelativeColumn(1); // Fecha
                            cols.RelativeColumn(1); // Vencimiento
                            cols.RelativeColumn(1); // Factura No.
                            cols.RelativeColumn(1); // Total
                            cols.RelativeColumn(1); // Pagado
                            cols.RelativeColumn(1); // Saldo
                        });

                        string[] titulos = { "Cliente", "Fecha", "Vencimiento", "Factura No.", "Total", "Pagado", "Saldo" };

                        tabla.Header(header =>
                        {
                            foreach (var titulo in titulos)
                            {
                                header.Cell().Element(c => c
                                    .Background(Colors.Grey.Lighten3)
                                    .PaddingVertical(4)
                                    .PaddingHorizontal(2)
                                    .ShowOnce())
                                    .Text(titulo).Bold();
                            }
                        });

                        foreach (var cuenta in cuentasConNombre)
                        {
                            tabla.Cell().Element(c => c
                                .BorderBottom(0.25f)
                                .BorderColor(Colors.Grey.Lighten2)
                                .Padding(2)).Text(cuenta.Nombre);
                            tabla.Cell().Text(DateTime.ParseExact(cuenta.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null).ToString("dd/MM/yyyy"));
                            tabla.Cell().Text(cuenta.Fecha_Vencimiento);
                            tabla.Cell().Text($"{cuenta.Secuencial_Factura}");
                            tabla.Cell().Text($"{cuenta.Total:N2}");
                            tabla.Cell().Text($"{cuenta.Pagado:N2}");
                            tabla.Cell().Text($"{cuenta.Saldo:N2}");
                        }

                        tabla.Cell().ColumnSpan(6).Element(c => c
                            .PaddingVertical(6)
                            .AlignRight())
                            .Text("💰 Total en el rango:").Bold();

                        tabla.Cell().Element(c => c
                            .Background(Colors.Grey.Lighten4)
                            .Padding(4))
                            .Text($"{totalDeuda:N2}").Bold();
                    });

                    page.Footer().AlignCenter()
                        .Text("Sistema Monitux-POS · Reporte generado automáticamente")
                        .FontSize(10)
                        .Italic();
                });
            })
            .GeneratePdf(rutaCompleta);

            V_Menu_Principal.MSG.ShowMSG("🖨️ Reporte generado correctamente", "Monitux-POS");

            var visor = new V_Visor_Factura
            {
                rutaArchivo = rutaCompleta
            };

            visor.ShowDialog();



        }

        private void button3_Click(object sender, EventArgs e)
        {



            ////////////////////


            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            int secuencialEmpresaActiva = V_Menu_Principal.Secuencial_Empresa;

            // Fechas seleccionadas por el usuario
            DateTime fechaInicio = dateTimePicker1.Value.Date;
            DateTime fechaFin = dateTimePicker2.Value.Date.AddDays(1).AddSeconds(-1); // incluye todo el día final

            // Rangos de saldo definidos por el usuario
            double saldoMinimo = (double)decimal.Parse(vmin.Text); // por ejemplo: 0
            double saldoMaximo = (double)decimal.Parse(vmax.Text); // por ejemplo: 999999

            var cuentasFiltradas = context.Cuentas_Cobrar
                .Where(c => c.Secuencial_Empresa == secuencialEmpresaActiva)
                .ToList()
                .Where(c => DateTime.TryParseExact(c.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null,
                    System.Globalization.DateTimeStyles.None, out DateTime _)
                    && DateTime.ParseExact(c.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null) >= fechaInicio
                    && DateTime.ParseExact(c.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null) <= fechaFin
                    && c.Saldo >= saldoMinimo && c.Saldo <= saldoMaximo)
                .OrderBy(c => DateTime.ParseExact(c.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null))
                .ToList();

            var clientes = context.Clientes.ToList();

            var cuentasConNombre = cuentasFiltradas
                .Join(clientes,
                    cuenta => cuenta.Secuencial_Cliente,
                    cliente => cliente.Secuencial,
                    (cuenta, cliente) => new
                    {
                        cliente.Nombre,
                        cuenta.Fecha,
                        cuenta.Fecha_Vencimiento,
                        cuenta.Secuencial_Factura,
                        cuenta.Total,
                        cuenta.Pagado,
                        cuenta.Saldo
                    })
                .ToList();

            decimal totalDeuda = (decimal)cuentasConNombre.Sum(c => c.Saldo);
            string nombreReporte = $"Cuentas_Cobrar_Rango_{fechaInicio:ddMMyyyy}_a_{fechaFin:ddMMyyyy}_Saldos.pdf";
            string rutaCompleta = $"{ruta}{nombreReporte}";

            Document.Create(doc =>
            {
                doc.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(9));

                    page.Header().Column(header =>
                    {
                        header.Item().Text("📆 Reporte de CTAS por cobrar en rango de fechas y saldos").FontSize(20).Bold();
                        header.Item().Text($"Fechas: {fechaInicio:dd/MM/yyyy} al {fechaFin:dd/MM/yyyy}");
                        header.Item().Text($"Saldos: entre {saldoMinimo:N2} y {saldoMaximo:N2}");
                        header.Item().Text($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}");
                    });

                    page.Content().Table(tabla =>
                    {
                        tabla.ColumnsDefinition(cols =>
                        {
                            cols.RelativeColumn(2); // Cliente
                            cols.RelativeColumn(1); // Fecha
                            cols.RelativeColumn(1); // Vencimiento
                            cols.RelativeColumn(1); // Factura No.
                            cols.RelativeColumn(1); // Total
                            cols.RelativeColumn(1); // Pagado
                            cols.RelativeColumn(1); // Saldo
                        });

                        string[] titulos = { "Cliente", "Fecha", "Vencimiento", "Factura No.", "Total", "Pagado", "Saldo" };

                        tabla.Header(header =>
                        {
                            foreach (var titulo in titulos)
                            {
                                header.Cell().Element(c => c
                                    .Background(Colors.Grey.Lighten3)
                                    .PaddingVertical(4)
                                    .PaddingHorizontal(2)
                                    .ShowOnce())
                                    .Text(titulo).Bold();
                            }
                        });

                        foreach (var cuenta in cuentasConNombre)
                        {
                            tabla.Cell().Element(c => c
                                .BorderBottom(0.25f)
                                .BorderColor(Colors.Grey.Lighten2)
                                .Padding(2)).Text(cuenta.Nombre);
                            tabla.Cell().Text(DateTime.ParseExact(cuenta.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null).ToString("dd/MM/yyyy"));
                            tabla.Cell().Text(cuenta.Fecha_Vencimiento);
                            tabla.Cell().Text($"{cuenta.Secuencial_Factura}");
                            tabla.Cell().Text($"{cuenta.Total:N2}");
                            tabla.Cell().Text($"{cuenta.Pagado:N2}");
                            tabla.Cell().Text($"{cuenta.Saldo:N2}");
                        }

                        tabla.Cell().ColumnSpan(6).Element(c => c
                            .PaddingVertical(6)
                            .AlignRight())
                            .Text("💰 Total en el rango:").Bold();

                        tabla.Cell().Element(c => c
                            .Background(Colors.Grey.Lighten4)
                            .Padding(4))
                            .Text($"{totalDeuda:N2}").Bold();
                    });

                    page.Footer().AlignCenter()
                        .Text("Sistema Monitux-POS · Reporte generado automáticamente")
                        .FontSize(10)
                        .Italic();
                });
            })
            .GeneratePdf(rutaCompleta);

            V_Menu_Principal.MSG.ShowMSG("🖨️ Reporte generado correctamente", "Monitux-POS");

            var visor = new V_Visor_Factura
            {
                rutaArchivo = rutaCompleta
            };

            visor.ShowDialog();



            ///////////////////

        }

        private void button8_Click(object sender, EventArgs e)
        {

            //////////////////

            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            int secuencialEmpresaActiva = V_Menu_Principal.Secuencial_Empresa;

            // Fechas seleccionadas por el usuario
            DateTime fechaInicio = dateTimePicker1.Value.Date;
            DateTime fechaFin = dateTimePicker2.Value.Date.AddDays(1).AddSeconds(-1); // incluye todo el día final

            // Rangos de saldo definidos por el usuario
            double saldoMinimo = (double)decimal.Parse(cmin.Text); // por ejemplo: 0
            double saldoMaximo = (double)decimal.Parse(cmax.Text); // por ejemplo: 999999

            var cuentasFiltradas = context.Cuentas_Pagar
                .Where(p => p.Secuencial_Empresa == secuencialEmpresaActiva)
                .ToList()
                .Where(p => DateTime.TryParseExact(p.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null,
                    System.Globalization.DateTimeStyles.None, out DateTime _)
                    && DateTime.ParseExact(p.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null) >= fechaInicio
                    && DateTime.ParseExact(p.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null) <= fechaFin
                    && p.Saldo >= saldoMinimo && p.Saldo <= saldoMaximo)
                .OrderBy(p => DateTime.ParseExact(p.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null))
                .ToList();

            var proveedores = context.Proveedores.ToList();

            var cuentasConNombre = cuentasFiltradas
                .Join(proveedores,
                    cuenta => cuenta.Secuencial_Proveedor,
                    proveedor => proveedor.Secuencial,
                    (cuenta, proveedor) => new
                    {
                        proveedor.Nombre,
                        cuenta.Fecha,
                        cuenta.Fecha_Vencimiento,
                        cuenta.Secuencial_Factura,
                        cuenta.Total,
                        cuenta.Pagado,
                        cuenta.Saldo
                    })
                .ToList();

            decimal totalDeuda = (decimal)cuentasConNombre.Sum(p => p.Saldo);
            string nombreReporte = $"Cuentas_Pagar_Rango_{fechaInicio:ddMMyyyy}_a_{fechaFin:ddMMyyyy}_Saldos.pdf";
            string rutaCompleta = $"{ruta}{nombreReporte}";

            Document.Create(doc =>
            {
                doc.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(9));

                    page.Header().Column(header =>
                    {
                        header.Item().Text("📆 Reporte de CTAS por pagar en rango de fechas y saldos").FontSize(20).Bold();
                        header.Item().Text($"Fechas: {fechaInicio:dd/MM/yyyy} al {fechaFin:dd/MM/yyyy}");
                        header.Item().Text($"Saldos: entre {saldoMinimo:N2} y {saldoMaximo:N2}");
                        header.Item().Text($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}");
                    });

                    page.Content().Table(tabla =>
                    {
                        tabla.ColumnsDefinition(cols =>
                        {
                            cols.RelativeColumn(2); // Proveedor
                            cols.RelativeColumn(1); // Fecha
                            cols.RelativeColumn(1); // Vencimiento
                            cols.RelativeColumn(1); // Factura No.
                            cols.RelativeColumn(1); // Total
                            cols.RelativeColumn(1); // Pagado
                            cols.RelativeColumn(1); // Saldo
                        });

                        string[] titulos = { "Proveedor", "Fecha", "Vencimiento", "Factura No.", "Total", "Pagado", "Saldo" };

                        tabla.Header(header =>
                        {
                            foreach (var titulo in titulos)
                            {
                                header.Cell().Element(c => c
                                    .Background(Colors.Grey.Lighten3)
                                    .PaddingVertical(4)
                                    .PaddingHorizontal(2)
                                    .ShowOnce())
                                    .Text(titulo).Bold();
                            }
                        });

                        foreach (var cuenta in cuentasConNombre)
                        {
                            tabla.Cell().Element(c => c
                                .BorderBottom(0.25f)
                                .BorderColor(Colors.Grey.Lighten2)
                                .Padding(2)).Text(cuenta.Nombre);
                            tabla.Cell().Text(DateTime.ParseExact(cuenta.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null).ToString("dd/MM/yyyy"));
                            tabla.Cell().Text(cuenta.Fecha_Vencimiento);
                            tabla.Cell().Text($"{cuenta.Secuencial_Factura}");
                            tabla.Cell().Text($"{cuenta.Total:N2}");
                            tabla.Cell().Text($"{cuenta.Pagado:N2}");
                            tabla.Cell().Text($"{cuenta.Saldo:N2}");
                        }

                        tabla.Cell().ColumnSpan(6).Element(c => c
                            .PaddingVertical(6)
                            .AlignRight())
                            .Text("💰 Total en el rango:").Bold();

                        tabla.Cell().Element(c => c
                            .Background(Colors.Grey.Lighten4)
                            .Padding(4))
                            .Text($"{totalDeuda:N2}").Bold();
                    });

                    page.Footer().AlignCenter()
                        .Text("Sistema Monitux-POS · Reporte generado automáticamente")
                        .FontSize(10)
                        .Italic();
                });
            })
            .GeneratePdf(rutaCompleta);

            V_Menu_Principal.MSG.ShowMSG("🖨️ Reporte generado correctamente", "Monitux-POS");

            var visor = new V_Visor_Factura
            {
                rutaArchivo = rutaCompleta
            };

            visor.ShowDialog();


            /////////////////

        }
    }
}
