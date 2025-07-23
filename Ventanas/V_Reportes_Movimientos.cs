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
    public partial class V_Reportes_Movimientos : Form
    {

        public string ruta = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Resources\\Reportes\\");
        public V_Reportes_Movimientos()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            /////////////////////

            using var context = new Monitux_DB_Context();
            int empresa = V_Menu_Principal.Secuencial_Empresa;
            DateTime desde = dateTimePicker1.Value.Date;
            DateTime hasta = dateTimePicker2.Value.Date.AddDays(1).AddSeconds(-1);

            // ---------------------------
            // VENTAS Contado
            // ---------------------------
            var ventasBase = context.Ventas
                .Where(v => v.Secuencial_Empresa == empresa && v.Gran_Total > 0 && v.Forma_Pago == "Contado")
                .ToList();

            var ventasContado = ventasBase
                .Where(v => DateTime.ParseExact(v.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null) >= desde &&
                            DateTime.ParseExact(v.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null) <= hasta)
                .Select(v =>
                {
                    var cliente = context.Clientes.FirstOrDefault(c => c.Secuencial == v.Secuencial_Cliente);
                    string nombre = cliente != null && !string.IsNullOrWhiteSpace(cliente.Nombre)
                        ? cliente.Nombre
                        : $"Cliente #{v.Secuencial_Cliente}";
                    return new
                    {
                        Fecha = DateTime.ParseExact(v.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null),
                        Cliente = nombre,
                        Tipo = "Venta - Contado",
                        Descripcion = $"Factura #{v.Secuencial}",
                        Monto = (double)v.Gran_Total
                    };
                }).ToList();

            // ---------------------------
            // COBROS CXC
            // ---------------------------
            var cxcBase = context.Cuentas_Cobrar
                .Where(c => c.Secuencial_Empresa == empresa && c.Total > 0 && c.Saldo < c.Total)
                .ToList();

            var ingresosCXC = cxcBase
                .Where(c => DateTime.ParseExact(c.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null) >= desde &&
                            DateTime.ParseExact(c.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null) <= hasta)
                .Select(c =>
                {
                    var cliente = context.Clientes.FirstOrDefault(cl => cl.Secuencial == c.Secuencial_Cliente);
                    string nombre = cliente != null && !string.IsNullOrWhiteSpace(cliente.Nombre)
                        ? cliente.Nombre
                        : $"Cliente #{c.Secuencial_Cliente}";
                    return new
                    {
                        Fecha = DateTime.ParseExact(c.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null),
                        Cliente = nombre,
                        Tipo = "Cobro CXC",
                        Descripcion = $"Factura #{c.Secuencial_Factura}",
                        Monto = (double)(c.Total - c.Saldo)
                    };
                }).ToList();

            // ---------------------------
            // INGRESOS Manuales
            // ---------------------------
            var ingresosBase = context.Ingresos
                .Where(i => i.Secuencial_Empresa == empresa && i.Total > 0)
                .ToList();

            var ingresosDirectos = ingresosBase
                .Where(i => DateTime.ParseExact(i.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null) >= desde &&
                            DateTime.ParseExact(i.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null) <= hasta)
                .Select(i =>
                {
                    string nombreCliente;

                    var facturaCxC = context.Cuentas_Cobrar.FirstOrDefault(c => c.Secuencial_Factura == i.Secuencial_Factura);
                    if (facturaCxC != null)
                    {
                        var cliente = context.Clientes.FirstOrDefault(cl => cl.Secuencial == facturaCxC.Secuencial_Cliente);
                        nombreCliente = cliente != null && !string.IsNullOrWhiteSpace(cliente.Nombre)
                            ? cliente.Nombre
                            : $"Cliente #{facturaCxC.Secuencial_Cliente}";
                    }
                    else
                    {
                        var facturaVenta = context.Ventas.FirstOrDefault(v => v.Secuencial == i.Secuencial_Factura);
                        if (facturaVenta != null)
                        {
                            var cliente = context.Clientes.FirstOrDefault(cl => cl.Secuencial == facturaVenta.Secuencial_Cliente);
                            nombreCliente = cliente != null && !string.IsNullOrWhiteSpace(cliente.Nombre)
                                ? cliente.Nombre
                                : $"Cliente #{facturaVenta.Secuencial_Cliente}";
                        }
                        else
                        {
                            nombreCliente = "Cliente N/D";
                        }
                    }

                    return new
                    {
                        Fecha = DateTime.ParseExact(i.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null),
                        Cliente = nombreCliente,
                        Tipo = $"Ingreso - {i.Tipo_Ingreso}",
                        Descripcion = i.Descripcion,
                        Monto = (double)i.Total
                    };
                }).ToList();

            // ---------------------------
            // Consolidación
            // ---------------------------
            var todosLosIngresos = ventasContado
                .Concat(ingresosCXC)
                .Concat(ingresosDirectos)
                .OrderBy(i => i.Fecha)
                .ToList();

            var agrupadoPorCliente = todosLosIngresos
                .GroupBy(i => i.Cliente)
                .ToDictionary(g => g.Key, g => g.ToList());

            // ---------------------------
            // Generación del PDF
            // ---------------------------
            string nombreArchivo = $"Reporte_Ingresos_{desde:ddMMyyyy}_a_{hasta:ddMMyyyy}.pdf";
            string rutaPDF = $"{ruta}{nombreArchivo}";

            Document.Create(doc =>
            {
                doc.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header().Column(col =>
                    {
                        col.Item().Text("📊 Reporte de Ingresos").FontSize(20).Bold();
                        col.Item().Text($"Fechas: {desde:dd/MM/yyyy} al {hasta:dd/MM/yyyy}").FontSize(10);
                    });

                    page.Content().Table(tabla =>
                    {
                        tabla.ColumnsDefinition(cols =>
                        {
                            cols.RelativeColumn(2); // Fecha
                            cols.RelativeColumn(3); // Descripción
                            cols.RelativeColumn(2); // Tipo
                            cols.RelativeColumn(2); // Monto
                        });

                        foreach (var grupo in agrupadoPorCliente)
                        {
                            string cliente = grupo.Key;
                            var ingresos = grupo.Value;
                            double totalCliente = ingresos.Sum(i => i.Monto);

                            tabla.Cell().ColumnSpan(4).Element(c => c.Background(Colors.Grey.Lighten3).Padding(4))
                                .Text($"👤 Cliente: {cliente}").Bold();

                            foreach (var i in ingresos)
                            {
                                tabla.Cell().Text(i.Fecha.ToString("dd/MM/yyyy"));
                                tabla.Cell().Text(i.Descripcion);
                                tabla.Cell().Text(i.Tipo);
                                tabla.Cell().Text($"{i.Monto:N2}");
                            }

                            tabla.Cell().ColumnSpan(3).Text("🧮 Total del cliente:");
                            tabla.Cell().Text($"{totalCliente:N2}").Bold();
                        }

                        double totalGeneral = todosLosIngresos.Sum(i => i.Monto);

                        tabla.Cell().ColumnSpan(3).Element(c => c.AlignRight().Padding(6))
                            .Text("💰 TOTAL GENERAL DE INGRESOS:").Bold();

                        tabla.Cell().Element(c => c.Background(Colors.Grey.Lighten4).Padding(4))
                            .Text($"{totalGeneral:N2}").Bold();
                    });

                    page.Footer().AlignCenter()
                        .Text("Monitux-POS · Reporte generado automáticamente").FontSize(9).Italic();
                });
            })
            .GeneratePdf(rutaPDF);

            // ---------------------------
            // Mostrar visor
            // ---------------------------
            V_Menu_Principal.MSG.ShowMSG("✅ Reporte generado correctamente", "Monitux-POS");

            var visor = new V_Visor_Factura
            {
                rutaArchivo = rutaPDF
            };

            visor.ShowDialog();


            ////////////////////




        }

        private void button5_Click(object sender, EventArgs e)
        {
            //////////////////////


            using var context = new Monitux_DB_Context();
            int empresa = V_Menu_Principal.Secuencial_Empresa;
            DateTime desde = dateTimePicker1.Value.Date;
            DateTime hasta = dateTimePicker2.Value.Date.AddDays(1).AddSeconds(-1);

            // ---------------------------
            // COMPRAS Contado
            // ---------------------------
            var comprasBase = context.Compras
                .Where(c => c.Secuencial_Empresa == empresa && c.Gran_Total > 0 && c.Forma_Pago == "Contado")
                .ToList();

            var comprasContado = comprasBase
                .Where(c => DateTime.ParseExact(c.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null) >= desde &&
                            DateTime.ParseExact(c.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null) <= hasta)
                .Select(c =>
                {
                    var proveedor = context.Proveedores.FirstOrDefault(p => p.Secuencial == c.Secuencial_Proveedor);
                    string nombre = proveedor != null && !string.IsNullOrWhiteSpace(proveedor.Nombre)
                        ? proveedor.Nombre
                        : $"Proveedor #{c.Secuencial_Proveedor}";
                    return new
                    {
                        Fecha = DateTime.ParseExact(c.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null),
                        Proveedor = nombre,
                        Tipo = "Compra - Contado",
                        Descripcion = $"Factura #{c.Secuencial}",
                        Monto = (double)c.Gran_Total
                    };
                }).ToList();

            // ---------------------------
            // PAGOS CXP
            // ---------------------------
            var cxpBase = context.Cuentas_Pagar
                .Where(c => c.Secuencial_Empresa == empresa && c.Total > 0 && c.Saldo < c.Total)
                .ToList();

            var pagosCXP = cxpBase
                .Where(c => DateTime.ParseExact(c.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null) >= desde &&
                            DateTime.ParseExact(c.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null) <= hasta)
                .Select(c =>
                {
                    var proveedor = context.Proveedores.FirstOrDefault(p => p.Secuencial == c.Secuencial_Proveedor);
                    string nombre = proveedor != null && !string.IsNullOrWhiteSpace(proveedor.Nombre)
                        ? proveedor.Nombre
                        : $"Proveedor #{c.Secuencial_Proveedor}";
                    return new
                    {
                        Fecha = DateTime.ParseExact(c.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null),
                        Proveedor = nombre,
                        Tipo = "Pago CXP",
                        Descripcion = $"Factura #{c.Secuencial_Factura}",
                        Monto = (double)(c.Total - c.Saldo)
                    };
                }).ToList();

            // ---------------------------
            // EGRESOS Manuales
            // ---------------------------
            var egresosBase = context.Egresos
                .Where(e => e.Secuencial_Empresa == empresa && e.Total > 0)
                .ToList();

            var egresosDirectos = egresosBase
                .Where(e => DateTime.ParseExact(e.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null) >= desde &&
                            DateTime.ParseExact(e.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null) <= hasta)
                .Select(e =>
                {
                    string nombreProveedor;

                    var facturaCXP = context.Cuentas_Pagar.FirstOrDefault(c => c.Secuencial_Factura == e.Secuencial_Factura);
                    if (facturaCXP != null)
                    {
                        var proveedor = context.Proveedores.FirstOrDefault(p => p.Secuencial == facturaCXP.Secuencial_Proveedor);
                        nombreProveedor = proveedor != null && !string.IsNullOrWhiteSpace(proveedor.Nombre)
                            ? proveedor.Nombre
                            : $"Proveedor #{facturaCXP.Secuencial_Proveedor}";
                    }
                    else
                    {
                        var compra = context.Compras.FirstOrDefault(c => c.Secuencial == e.Secuencial_Factura);
                        if (compra != null)
                        {
                            var proveedor = context.Proveedores.FirstOrDefault(p => p.Secuencial == compra.Secuencial_Proveedor);
                            nombreProveedor = proveedor != null && !string.IsNullOrWhiteSpace(proveedor.Nombre)
                                ? proveedor.Nombre
                                : $"Proveedor #{compra.Secuencial_Proveedor}";
                        }
                        else
                        {
                            nombreProveedor = "Proveedor N/D";
                        }
                    }

                    return new
                    {
                        Fecha = DateTime.ParseExact(e.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null),
                        Proveedor = nombreProveedor,
                        Tipo = $"Egreso - {e.Tipo_Egreso}",
                        Descripcion = e.Descripcion,
                        Monto = (double)e.Total
                    };
                }).ToList();

            // ---------------------------
            // Consolidación
            // ---------------------------
            var todosLosEgresos = comprasContado
                .Concat(pagosCXP)
                .Concat(egresosDirectos)
                .OrderBy(e => e.Fecha)
                .ToList();

            var agrupadoPorProveedor = todosLosEgresos
                .GroupBy(e => e.Proveedor)
                .ToDictionary(g => g.Key, g => g.ToList());

            // ---------------------------
            // Generación del PDF
            // ---------------------------
            string nombreArchivo = $"Reporte_Egresos_{desde:ddMMyyyy}_a_{hasta:ddMMyyyy}.pdf";
            string rutaPDF = $"{ruta}{nombreArchivo}";

            Document.Create(doc =>
            {
                doc.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header().Column(col =>
                    {
                        col.Item().Text("📦 Reporte de Egresos").FontSize(20).Bold();
                        col.Item().Text($"Fechas: {desde:dd/MM/yyyy} al {hasta:dd/MM/yyyy}").FontSize(10);
                    });

                    page.Content().Table(tabla =>
                    {
                        tabla.ColumnsDefinition(cols =>
                        {
                            cols.RelativeColumn(2); // Fecha
                            cols.RelativeColumn(3); // Descripción
                            cols.RelativeColumn(2); // Tipo
                            cols.RelativeColumn(2); // Monto
                        });

                        foreach (var grupo in agrupadoPorProveedor)
                        {
                            string proveedor = grupo.Key;
                            var movimientos = grupo.Value;
                            double totalProveedor = movimientos.Sum(e => e.Monto);

                            tabla.Cell().ColumnSpan(4).Element(c => c.Background(Colors.Blue.Lighten4).Padding(4))
                                .Text($"🏢 Proveedor: {proveedor}").Bold();

                            foreach (var e in movimientos)
                            {
                                tabla.Cell().Text(e.Fecha.ToString("dd/MM/yyyy"));
                                tabla.Cell().Text(e.Descripcion);
                                tabla.Cell().Text(e.Tipo);
                                tabla.Cell().Text($"{e.Monto:N2}");
                            }

                            tabla.Cell().ColumnSpan(3).Text("🧾 Total del proveedor:");
                            tabla.Cell().Text($"{totalProveedor:N2}").Bold();
                        }

                        double totalGeneral = todosLosEgresos.Sum(e => e.Monto);

                        tabla.Cell().ColumnSpan(3).Element(c => c.AlignRight().Padding(6))
                            .Text("💸 TOTAL GENERAL DE COMPRAS:").Bold();

                        tabla.Cell().Element(c => c.Background(Colors.Blue.Lighten3).Padding(4))
                            .Text($"{totalGeneral:N2}").Bold();
                    });

                    page.Footer().AlignCenter()
                        .Text("Monitux-POS · Reporte generado automáticamente")
                        .FontSize(9).Italic();
                });
            })
            .GeneratePdf(rutaPDF);

            // ---------------------------
            // Mostrar visor en Monitux-POS
            // ---------------------------
            V_Menu_Principal.MSG.ShowMSG("✅ Reporte de egresos generado correctamente", "Monitux-POS");

            var visor = new V_Visor_Factura
            {
                rutaArchivo = rutaPDF
            };

            visor.ShowDialog();




            /////////////////////
        }

        private void button2_Click(object sender, EventArgs e)
        {

            ///////////////////


            using var context = new Monitux_DB_Context();
            int empresa = V_Menu_Principal.Secuencial_Empresa;
            DateTime desde = dateTimePicker1.Value.Date;
            DateTime hasta = dateTimePicker2.Value.Date.AddDays(1).AddSeconds(-1);

            // ---------------------------
            // INGRESOS con Tipo = "Pago Recibido"
            // ---------------------------
            var ingresosBase = context.Ingresos
                .Where(i => i.Secuencial_Empresa == empresa && i.Total > 0 && i.Tipo_Ingreso == "Pago Recibido")
                .ToList();

            var pagosRecibidos = ingresosBase
                .Where(i => DateTime.ParseExact(i.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null) >= desde &&
                            DateTime.ParseExact(i.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null) <= hasta)
                .Select(i => new
                {
                    Fecha = DateTime.ParseExact(i.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null),
                    Descripcion = i.Descripcion,
                    Monto = (double)i.Total
                }).OrderBy(i => i.Fecha)
                .ToList();

            // ---------------------------
            // Generar PDF
            // ---------------------------
            string nombreArchivo = $"Pagos_Recibidos_{desde:ddMMyyyy}_a_{hasta:ddMMyyyy}.pdf";
            string rutaPDF = $"{ruta}{nombreArchivo}";

            Document.Create(doc =>
            {
                doc.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header().Column(col =>
                    {
                        col.Item().Text("💵 Reporte de Pagos Recibidos").FontSize(20).Bold();
                        col.Item().Text($"Fechas: {desde:dd/MM/yyyy} al {hasta:dd/MM/yyyy}").FontSize(10);
                    });

                    page.Content().Table(tabla =>
                    {
                        tabla.ColumnsDefinition(cols =>
                        {
                            cols.RelativeColumn(2); // Fecha
                            cols.RelativeColumn(5); // Descripción
                            cols.RelativeColumn(2); // Monto
                        });

                        foreach (var pago in pagosRecibidos)
                        {
                            tabla.Cell().Text(pago.Fecha.ToString("dd/MM/yyyy"));
                            tabla.Cell().Text(pago.Descripcion);
                            tabla.Cell().Text($"{pago.Monto:N2}");
                        }

                        double totalPagos = pagosRecibidos.Sum(i => i.Monto);

                        tabla.Cell().ColumnSpan(2).Element(c => c.AlignRight().Padding(6))
                            .Text("🧾 TOTAL DE PAGOS RECIBIDOS:").Bold();

                        tabla.Cell().Element(c => c.Background(Colors.Green.Lighten4).Padding(4))
                            .Text($"{totalPagos:N2}").Bold();
                    });

                    page.Footer().AlignCenter()
                        .Text("Monitux-POS · Reporte generado automáticamente").FontSize(9).Italic();
                });
            })
            .GeneratePdf(rutaPDF);

            // ---------------------------
            // Mostrar visor
            // ---------------------------
            V_Menu_Principal.MSG.ShowMSG("✅ Reporte de pagos generado correctamente", "Monitux-POS");

            var visor = new V_Visor_Factura
            {
                rutaArchivo = rutaPDF
            };

            visor.ShowDialog();


            ///////////////////



        }

        private void button6_Click(object sender, EventArgs e)
        {

            using var context = new Monitux_DB_Context();
            int empresa = V_Menu_Principal.Secuencial_Empresa;
            DateTime desde = dateTimePicker1.Value.Date;
            DateTime hasta = dateTimePicker2.Value.Date.AddDays(1).AddSeconds(-1);

            // ---------------------------
            // EGRESOS con Tipo = "Pago Realizado"
            // ---------------------------
            var egresosBase = context.Egresos
                .Where(e => e.Secuencial_Empresa == empresa && e.Total > 0 && e.Tipo_Egreso == "Pago Realizado")
                .ToList();

            var pagosRealizados = egresosBase
                .Where(e => DateTime.ParseExact(e.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null) >= desde &&
                            DateTime.ParseExact(e.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null) <= hasta)
                .Select(e => new
                {
                    Fecha = DateTime.ParseExact(e.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null),
                    Descripcion = e.Descripcion,
                    Monto = (double)e.Total
                }).OrderBy(e => e.Fecha)
                .ToList();

            // ---------------------------
            // Generar PDF
            // ---------------------------
            string nombreArchivo = $"Pagos_Realizados_{desde:ddMMyyyy}_a_{hasta:ddMMyyyy}.pdf";
            string rutaPDF = $"{ruta}{nombreArchivo}";

            Document.Create(doc =>
            {
                doc.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header().Column(col =>
                    {
                        col.Item().Text("💸 Reporte de Pagos Realizados").FontSize(20).Bold();
                        col.Item().Text($"Fechas: {desde:dd/MM/yyyy} al {hasta:dd/MM/yyyy}").FontSize(10);
                    });

                    page.Content().Table(tabla =>
                    {
                        tabla.ColumnsDefinition(cols =>
                        {
                            cols.RelativeColumn(2); // Fecha
                            cols.RelativeColumn(5); // Descripción
                            cols.RelativeColumn(2); // Monto
                        });

                        foreach (var pago in pagosRealizados)
                        {
                            tabla.Cell().Text(pago.Fecha.ToString("dd/MM/yyyy"));
                            tabla.Cell().Text(pago.Descripcion);
                            tabla.Cell().Text($"{pago.Monto:N2}");
                        }

                        double totalPagos = pagosRealizados.Sum(e => e.Monto);

                        tabla.Cell().ColumnSpan(2).Element(c => c.AlignRight().Padding(6))
                            .Text("🧾 TOTAL DE PAGOS REALIZADOS:").Bold();

                        tabla.Cell().Element(c => c.Background(Colors.Red.Lighten3).Padding(4))
                            .Text($"{totalPagos:N2}").Bold();
                    });

                    page.Footer().AlignCenter()
                        .Text("Monitux-POS · Reporte generado automáticamente").FontSize(9).Italic();
                });
            })
            .GeneratePdf(rutaPDF);

            // ---------------------------
            // Mostrar visor
            // ---------------------------
            V_Menu_Principal.MSG.ShowMSG("✅ Reporte de pagos generado correctamente", "Monitux-POS");

            var visor = new V_Visor_Factura
            {
                rutaArchivo = rutaPDF
            };

            visor.ShowDialog();



        }

        private void button4_Click(object sender, EventArgs e)
        {



            ////////////////////


            using var context = new Monitux_DB_Context();
            int empresa = V_Menu_Principal.Secuencial_Empresa;
            DateTime desde = dateTimePicker1.Value.Date;
            DateTime hasta = dateTimePicker2.Value.Date.AddDays(1).AddSeconds(-1);

            // ---------------------------
            // INGRESOS filtrados por fecha
            // ---------------------------
            var ingresosBase = context.Ingresos
                .Where(i => i.Secuencial_Empresa == empresa && i.Total > 0)
                .ToList();

            var ingresosPorUsuario = ingresosBase
                .Where(i => DateTime.ParseExact(i.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null) >= desde &&
                            DateTime.ParseExact(i.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null) <= hasta)
                .Select(i =>
                {
                    var usuario = context.Usuarios.FirstOrDefault(u => u.Secuencial == i.Secuencial_Usuario);
                    string nombreUsuario = usuario != null && !string.IsNullOrWhiteSpace(usuario.Nombre)
                        ? usuario.Nombre
                        : $"Usuario #{i.Secuencial_Usuario}";

                    return new
                    {
                        Fecha = DateTime.ParseExact(i.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null),
                        Usuario = nombreUsuario,
                        Tipo = $"Ingreso - {i.Tipo_Ingreso}",
                        Descripcion = i.Descripcion,
                        Monto = (double)i.Total
                    };
                }).OrderBy(i => i.Fecha)
                .ToList();

            var agrupadoPorUsuario = ingresosPorUsuario
                .GroupBy(i => i.Usuario)
                .ToDictionary(g => g.Key, g => g.ToList());





            string nombreArchivo = $"Reporte_Ingresos_por_Usuario_{desde:ddMMyyyy}_a_{hasta:ddMMyyyy}.pdf";
            string rutaPDF = $"{ruta}{nombreArchivo}";

            Document.Create(doc =>
            {
                doc.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header().Column(col =>
                    {
                        col.Item().Text("👥 Reporte de Ingresos por Usuario").FontSize(20).Bold();
                        col.Item().Text($"Fechas: {desde:dd/MM/yyyy} al {hasta:dd/MM/yyyy}").FontSize(10);
                    });

                    page.Content().Table(tabla =>
                    {
                        tabla.ColumnsDefinition(cols =>
                        {
                            cols.RelativeColumn(2); // Fecha
                            cols.RelativeColumn(3); // Descripción
                            cols.RelativeColumn(2); // Tipo
                            cols.RelativeColumn(2); // Monto
                        });

                        foreach (var grupo in agrupadoPorUsuario)
                        {
                            string usuario = grupo.Key;
                            var ingresos = grupo.Value;
                            double totalUsuario = ingresos.Sum(i => i.Monto);

                            tabla.Cell().ColumnSpan(4).Element(c => c.Background(Colors.Grey.Lighten3).Padding(4))
                                .Text($"👤 Usuario: {usuario}").Bold();

                            foreach (var i in ingresos)
                            {
                                tabla.Cell().Text(i.Fecha.ToString("dd/MM/yyyy"));
                                tabla.Cell().Text(i.Descripcion);
                                tabla.Cell().Text(i.Tipo);
                                tabla.Cell().Text($"{i.Monto:N2}");
                            }

                            tabla.Cell().ColumnSpan(3).Text("🧮 Total del usuario:");
                            tabla.Cell().Text($"{totalUsuario:N2}").Bold();
                        }

                        double totalGeneral = ingresosPorUsuario.Sum(i => i.Monto);

                        tabla.Cell().ColumnSpan(3).Element(c => c.AlignRight().Padding(6))
                            .Text("💰 TOTAL GENERAL DE INGRESOS:").Bold();

                        tabla.Cell().Element(c => c.Background(Colors.Grey.Lighten4).Padding(4))
                            .Text($"{totalGeneral:N2}").Bold();
                    });

                    page.Footer().AlignCenter()
                        .Text("Monitux-POS · Reporte generado automáticamente").FontSize(9).Italic();
                });
            })
            .GeneratePdf(rutaPDF);

            // ---------------------------
            // Mostrar visor en Monitux-POS
            // ---------------------------
            V_Menu_Principal.MSG.ShowMSG("✅ Reporte por usuario generado correctamente", "Monitux-POS");

            var visor = new V_Visor_Factura
            {
                rutaArchivo = rutaPDF
            };

            visor.ShowDialog();



            ///////////////////




        }

        private void button8_Click(object sender, EventArgs e)
        {

            /////////////////////

            using var context = new Monitux_DB_Context();
            int empresa = V_Menu_Principal.Secuencial_Empresa;
            DateTime desde = dateTimePicker1.Value.Date;
            DateTime hasta = dateTimePicker2.Value.Date.AddDays(1).AddSeconds(-1);

            // ---------------------------
            // EGRESOS filtrados por fecha
            // ---------------------------
            var egresosBase = context.Egresos
                .Where(e => e.Secuencial_Empresa == empresa && e.Total > 0)
                .ToList();

            var egresosPorUsuario = egresosBase
                .Where(e => DateTime.ParseExact(e.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null) >= desde &&
                            DateTime.ParseExact(e.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null) <= hasta)
                .Select(e =>
                {
                    var usuario = context.Usuarios.FirstOrDefault(u => u.Secuencial == e.Secuencial_Usuario);
                    string nombreUsuario = usuario != null && !string.IsNullOrWhiteSpace(usuario.Nombre)
                        ? usuario.Nombre
                        : $"Usuario #{e.Secuencial_Usuario}";

                    return new
                    {
                        Fecha = DateTime.ParseExact(e.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null),
                        Usuario = nombreUsuario,
                        Tipo = $"Egreso - {e.Tipo_Egreso}",
                        Descripcion = e.Descripcion,
                        Monto = (double)e.Total
                    };
                }).OrderBy(e => e.Fecha)
                .ToList();

            var agrupadoPorUsuario = egresosPorUsuario
                .GroupBy(e => e.Usuario)
                .ToDictionary(g => g.Key, g => g.ToList());



            string nombreArchivo = $"Reporte_Egresos_por_Usuario_{desde:ddMMyyyy}_a_{hasta:ddMMyyyy}.pdf";
            string rutaPDF = $"{ruta}{nombreArchivo}";

            Document.Create(doc =>
            {
                doc.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header().Column(col =>
                    {
                        col.Item().Text("👥 Reporte de Egresos por Usuario").FontSize(20).Bold();
                        col.Item().Text($"Fechas: {desde:dd/MM/yyyy} al {hasta:dd/MM/yyyy}").FontSize(10);
                    });

                    page.Content().Table(tabla =>
                    {
                        tabla.ColumnsDefinition(cols =>
                        {
                            cols.RelativeColumn(2); // Fecha
                            cols.RelativeColumn(3); // Descripción
                            cols.RelativeColumn(2); // Tipo
                            cols.RelativeColumn(2); // Monto
                        });

                        foreach (var grupo in agrupadoPorUsuario)
                        {
                            string usuario = grupo.Key;
                            var egresos = grupo.Value;
                            double totalUsuario = egresos.Sum(e => e.Monto);

                            tabla.Cell().ColumnSpan(4).Element(c => c.Background(Colors.Red.Lighten4).Padding(4))
                                .Text($"👤 Usuario: {usuario}").Bold();

                            foreach (var e in egresos)
                            {
                                tabla.Cell().Text(e.Fecha.ToString("dd/MM/yyyy"));
                                tabla.Cell().Text(e.Descripcion);
                                tabla.Cell().Text(e.Tipo);
                                tabla.Cell().Text($"{e.Monto:N2}");
                            }

                            tabla.Cell().ColumnSpan(3).Text("🧾 Total del usuario:");
                            tabla.Cell().Text($"{totalUsuario:N2}").Bold();
                        }

                        double totalGeneral = egresosPorUsuario.Sum(e => e.Monto);

                        tabla.Cell().ColumnSpan(3).Element(c => c.AlignRight().Padding(6))
                            .Text("💸 TOTAL GENERAL DE EGRESOS:").Bold();

                        tabla.Cell().Element(c => c.Background(Colors.Red.Lighten3).Padding(4))
                            .Text($"{totalGeneral:N2}").Bold();
                    });

                    page.Footer().AlignCenter()
                        .Text("Monitux-POS · Reporte generado automáticamente").FontSize(9).Italic();
                });
            })
            .GeneratePdf(rutaPDF);

            // ---------------------------
            // Mostrar visor en Monitux-POS
            // ---------------------------
            V_Menu_Principal.MSG.ShowMSG("✅ Reporte de egresos por usuario generado correctamente", "Monitux-POS");

            var visor = new V_Visor_Factura
            {
                rutaArchivo = rutaPDF
            };

            visor.ShowDialog();



            /////////////////////



        }

        private void button3_Click(object sender, EventArgs e)
        {
            ////////////////

            using var context = new Monitux_DB_Context();
            int empresa = V_Menu_Principal.Secuencial_Empresa;
            DateTime desde = dateTimePicker1.Value.Date;
            DateTime hasta = dateTimePicker2.Value.Date.AddDays(1).AddSeconds(-1);

            // ---------------------------
            // INGRESOS tipo "Ingreso Manual" filtrados por fecha
            // ---------------------------
            var ingresosBase = context.Ingresos
                .Where(i => i.Secuencial_Empresa == empresa && i.Total > 0 && i.Tipo_Ingreso == "Ingreso Manual")
                .ToList();

            var ingresosManual = ingresosBase
                .Where(i => DateTime.ParseExact(i.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null) >= desde &&
                            DateTime.ParseExact(i.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null) <= hasta)
                .Select(i => new
                {
                    Fecha = DateTime.ParseExact(i.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null),
                    Descripcion = i.Descripcion,
                    Monto = (double)i.Total
                }).OrderBy(i => i.Fecha)
                .ToList();

            // ---------------------------
            // Generación del PDF
            // ---------------------------
            string nombreArchivo = $"Reporte_Ingreso_Manual_{desde:ddMMyyyy}_a_{hasta:ddMMyyyy}.pdf";
            string rutaPDF = $"{ruta}{nombreArchivo}";

            Document.Create(doc =>
            {
                doc.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header().Column(col =>
                    {
                        col.Item().Text("📝 Reporte de Ingresos Manuales").FontSize(20).Bold();
                        col.Item().Text($"Fechas: {desde:dd/MM/yyyy} al {hasta:dd/MM/yyyy}").FontSize(10);
                    });

                    page.Content().Table(tabla =>
                    {
                        tabla.ColumnsDefinition(cols =>
                        {
                            cols.RelativeColumn(2); // Fecha
                            cols.RelativeColumn(6); // Descripción
                            cols.RelativeColumn(2); // Monto
                        });

                        foreach (var ingreso in ingresosManual)
                        {
                            tabla.Cell().Text(ingreso.Fecha.ToString("dd/MM/yyyy"));
                            tabla.Cell().Text(ingreso.Descripcion);
                            tabla.Cell().Text($"{ingreso.Monto:N2}");
                        }

                        double total = ingresosManual.Sum(i => i.Monto);

                        tabla.Cell().ColumnSpan(2).Element(c => c.AlignRight().Padding(6))
                            .Text("🧮 TOTAL DE INGRESOS MANUALES:").Bold();

                        tabla.Cell().Element(c => c.Background(Colors.Green.Lighten3).Padding(4))
                            .Text($"{total:N2}").Bold();
                    });

                    page.Footer().AlignCenter()
                        .Text("Monitux-POS · Reporte generado automáticamente").FontSize(9).Italic();
                });
            })
            .GeneratePdf(rutaPDF);

            // ---------------------------
            // Mostrar visor
            // ---------------------------
            V_Menu_Principal.MSG.ShowMSG("✅ Reporte de ingresos manuales generado correctamente", "Monitux-POS");

            var visor = new V_Visor_Factura
            {
                rutaArchivo = rutaPDF
            };

            visor.ShowDialog();



            ////////////////
        }

        private void button7_Click(object sender, EventArgs e)
        {


            ////////////////

            using var context = new Monitux_DB_Context();
            int empresa = V_Menu_Principal.Secuencial_Empresa;
            DateTime desde = dateTimePicker1.Value.Date;
            DateTime hasta = dateTimePicker2.Value.Date.AddDays(1).AddSeconds(-1);

            // ---------------------------
            // EGRESOS tipo "Egreso Manual" filtrados por fecha
            // ---------------------------
            var egresosBase = context.Egresos
                .Where(e => e.Secuencial_Empresa == empresa && e.Total > 0 && e.Tipo_Egreso == "Egreso Manual")
                .ToList();

            var egresosManual = egresosBase
                .Where(e => DateTime.ParseExact(e.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null) >= desde &&
                            DateTime.ParseExact(e.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null) <= hasta)
                .Select(e => new
                {
                    Fecha = DateTime.ParseExact(e.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null),
                    Descripcion = e.Descripcion,
                    Monto = (double)e.Total
                }).OrderBy(e => e.Fecha)
                .ToList();

            // ---------------------------
            // Generación del PDF
            // ---------------------------
            string nombreArchivo = $"Reporte_Egreso_Manual_{desde:ddMMyyyy}_a_{hasta:ddMMyyyy}.pdf";
            string rutaPDF = $"{ruta}{nombreArchivo}";

            Document.Create(doc =>
            {
                doc.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header().Column(col =>
                    {
                        col.Item().Text("📤 Reporte de Egresos Manuales").FontSize(20).Bold();
                        col.Item().Text($"Fechas: {desde:dd/MM/yyyy} al {hasta:dd/MM/yyyy}").FontSize(10);
                    });

                    page.Content().Table(tabla =>
                    {
                        tabla.ColumnsDefinition(cols =>
                        {
                            cols.RelativeColumn(2); // Fecha
                            cols.RelativeColumn(6); // Descripción
                            cols.RelativeColumn(2); // Monto
                        });

                        foreach (var egreso in egresosManual)
                        {
                            tabla.Cell().Text(egreso.Fecha.ToString("dd/MM/yyyy"));
                            tabla.Cell().Text(egreso.Descripcion);
                            tabla.Cell().Text($"{egreso.Monto:N2}");
                        }

                        double total = egresosManual.Sum(e => e.Monto);

                        tabla.Cell().ColumnSpan(2).Element(c => c.AlignRight().Padding(6))
                            .Text("🧾 TOTAL DE EGRESOS MANUALES:").Bold();

                        tabla.Cell().Element(c => c.Background(Colors.Red.Lighten3).Padding(4))
                            .Text($"{total:N2}").Bold();
                    });

                    page.Footer().AlignCenter()
                        .Text("Monitux-POS · Reporte generado automáticamente").FontSize(9).Italic();
                });
            })
            .GeneratePdf(rutaPDF);

            // ---------------------------
            // Mostrar visor
            // ---------------------------
            V_Menu_Principal.MSG.ShowMSG("✅ Reporte de egresos manuales generado correctamente", "Monitux-POS");

            var visor = new V_Visor_Factura
            {
                rutaArchivo = rutaPDF
            };

            visor.ShowDialog();


            ////////////////

        }

        private void button9_Click(object sender, EventArgs e)
        {

            /////////////////////


            using var context = new Monitux_DB_Context();
            int empresa = V_Menu_Principal.Secuencial_Empresa;
            DateTime desde = dateTimePicker1.Value.Date;
            DateTime hasta = dateTimePicker2.Value.Date.AddDays(1).AddSeconds(-1);

            // ---------------------------
            // ACTIVIDADES filtradas por fecha
            // ---------------------------
            var actividadesBase = context.Actividades
                .Where(a => a.Secuencial_Empresa == empresa)
                .ToList();

            var actividadesFiltradas = actividadesBase
                .Where(a => DateTime.ParseExact(a.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null) >= desde &&
                            DateTime.ParseExact(a.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null) <= hasta)
                .Select(a =>
                {
                    var usuario = context.Usuarios.FirstOrDefault(u => u.Secuencial == a.Secuencial_Usuario);
                    string nombreUsuario = usuario != null && !string.IsNullOrWhiteSpace(usuario.Nombre)
                        ? usuario.Nombre
                        : $"Usuario #{a.Secuencial_Usuario}";

                    return new
                    {
                        Fecha = DateTime.ParseExact(a.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null),
                        Usuario = nombreUsuario,
                        Descripcion = a.Descripcion
                    };
                }).OrderBy(a => a.Fecha)
                .ToList();

            var agrupadoPorUsuario = actividadesFiltradas
                .GroupBy(a => a.Usuario)
                .ToDictionary(g => g.Key, g => g.ToList());



            string nombreArchivo = $"Reporte_Actividades_{desde:ddMMyyyy}_a_{hasta:ddMMyyyy}.pdf";
            string rutaPDF = $"{ruta}{nombreArchivo}";

            Document.Create(doc =>
            {
                doc.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header().Column(col =>
                    {
                        col.Item().Text("📋 Reporte de Actividades").FontSize(20).Bold();
                        col.Item().Text($"Fechas: {desde:dd/MM/yyyy} al {hasta:dd/MM/yyyy}").FontSize(10);
                    });

                    page.Content().Table(tabla =>
                    {
                        tabla.ColumnsDefinition(cols =>
                        {
                            cols.RelativeColumn(2); // Fecha
                            cols.RelativeColumn(6); // Descripción
                        });

                        foreach (var grupo in agrupadoPorUsuario)
                        {
                            string usuario = grupo.Key;
                            var actividades = grupo.Value;

                            tabla.Cell().ColumnSpan(2).Element(c => c.Background(Colors.Grey.Lighten3).Padding(4))
                                .Text($"👤 Usuario: {usuario}").Bold();

                            foreach (var a in actividades)
                            {
                                tabla.Cell().Text(a.Fecha.ToString("dd/MM/yyyy HH:mm"));
                                tabla.Cell().Text(a.Descripcion);
                            }

                            tabla.Cell().ColumnSpan(1).Text("🧾 Total actividades:");
                            tabla.Cell().Text($"{actividades.Count}").Bold();
                        }

                        int totalGeneral = actividadesFiltradas.Count;

                        tabla.Cell().ColumnSpan(1).Element(c => c.AlignRight().Padding(6))
                            .Text("🔢 TOTAL GENERAL DE ACTIVIDADES:").Bold();

                        tabla.Cell().Element(c => c.Background(Colors.Grey.Lighten4).Padding(4))
                            .Text($"{totalGeneral}").Bold();
                    });

                    page.Footer().AlignCenter()
                        .Text("Monitux-POS · Reporte generado automáticamente").FontSize(9).Italic();
                });
            })
            .GeneratePdf(rutaPDF);

            // ---------------------------
            // Mostrar visor en Monitux-POS
            // ---------------------------
            V_Menu_Principal.MSG.ShowMSG("✅ Reporte de actividades generado correctamente", "Monitux-POS");

            var visor = new V_Visor_Factura
            {
                rutaArchivo = rutaPDF
            };

            visor.ShowDialog();





            /////////////////////



        }

        private void button10_Click(object sender, EventArgs e)
        {


            ///////////////////////


            using var context = new Monitux_DB_Context();
            int empresa = V_Menu_Principal.Secuencial_Empresa;
            int secuencialUsuario = int.Parse(combo_Usuario.SelectedItem.ToString().Split('-')[0].Trim());
            DateTime desde = dateTimePicker1.Value.Date;
            DateTime hasta = dateTimePicker2.Value.Date.AddDays(1).AddSeconds(-1);

            // ---------------------------
            // ACTIVIDADES del usuario específico en rango de fechas
            // ---------------------------
            var actividadesBase = context.Actividades
                .Where(a => a.Secuencial_Empresa == empresa && a.Secuencial_Usuario == secuencialUsuario)
                .ToList();

            var actividadesFiltradas = actividadesBase
                .Where(a => DateTime.ParseExact(a.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null) >= desde &&
                            DateTime.ParseExact(a.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null) <= hasta)
                .Select(a =>
                {
                    var usuario = context.Usuarios.FirstOrDefault(u => u.Secuencial == a.Secuencial_Usuario);
                    string nombreUsuario = usuario != null && !string.IsNullOrWhiteSpace(usuario.Nombre)
                        ? usuario.Nombre
                        : $"Usuario #{a.Secuencial_Usuario}";

                    return new
                    {
                        Fecha = DateTime.ParseExact(a.Fecha.Trim(), "dd/MM/yyyy HH:mm:ss", null),
                        Usuario = nombreUsuario,
                        Descripcion = a.Descripcion
                    };
                }).OrderBy(a => a.Fecha)
                .ToList();




            string nombreArchivo = $"Actividades_Usuario_{secuencialUsuario}_{desde:ddMMyyyy}_a_{hasta:ddMMyyyy}.pdf";
            string rutaPDF = $"{ruta}{nombreArchivo}";

            Document.Create(doc =>
            {
                doc.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    string nombreUsuario = actividadesFiltradas.FirstOrDefault()?.Usuario ?? $"Usuario #{secuencialUsuario}";

                    page.Header().Column(col =>
                    {
                        col.Item().Text("📒 Reporte de Actividades del Usuario").FontSize(20).Bold();
                        col.Item().Text($"Usuario: {nombreUsuario}").FontSize(11);
                        col.Item().Text($"Fechas: {desde:dd/MM/yyyy} al {hasta:dd/MM/yyyy}").FontSize(10);
                    });

                    page.Content().Table(tabla =>
                    {
                        tabla.ColumnsDefinition(cols =>
                        {
                            cols.RelativeColumn(2); // Fecha
                            cols.RelativeColumn(6); // Descripción
                        });

                        foreach (var actividad in actividadesFiltradas)
                        {
                            tabla.Cell().Text(actividad.Fecha.ToString("dd/MM/yyyy HH:mm"));
                            tabla.Cell().Text(actividad.Descripcion);
                        }

                        tabla.Cell().ColumnSpan(1).Text("🔢 Total actividades:");
                        tabla.Cell().Text($"{actividadesFiltradas.Count}").Bold();
                    });

                    page.Footer().AlignCenter()
                        .Text("Monitux-POS · Reporte generado automáticamente").FontSize(9).Italic();
                });
            })
            .GeneratePdf(rutaPDF);

            // ---------------------------
            // Mostrar visor en Monitux-POS
            // ---------------------------
            V_Menu_Principal.MSG.ShowMSG("✅ Reporte de actividades del usuario generado correctamente", "Monitux-POS");

            var visor = new V_Visor_Factura
            {
                rutaArchivo = rutaPDF
            };

            visor.ShowDialog();




            ///////////////////////


        }



        public void llenar_Combo_Usuario()
        {


            combo_Usuario.Items.Clear();

            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            // Filtrar solo clientes activos
            var usuarios = context.Usuarios.Where(c => (bool)c.Activo && c.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa).ToList();

            foreach (var item in usuarios)
            {
                combo_Usuario.Items.Add(item.Secuencial + " - " + item.Nombre);
            }



        }





        private void V_Reportes_Movimientos_Load(object sender, EventArgs e)
        {

            llenar_Combo_Usuario();



        }
    }
}
