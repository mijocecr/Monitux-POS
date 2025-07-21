using DocumentFormat.OpenXml.InkML;
using Monitux_POS.Clases;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
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
    public partial class V_Reportes_Facturas : Form
    {

        public int secuencialCliente = 0; // Para filtrar por cliente si es necesario
        public int secuencial_Proveedor = 0; // Para filtrar por proveedor si es necesario
        public V_Reportes_Facturas()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {


            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            var fechaInicio = dateTimePicker1.Value.Date;
            var fechaFin = dateTimePicker2.Value.Date.AddDays(1).AddTicks(-1); // incluye todo el segundo día

            // 🔧 Procesar los registros con conversión segura
            var registrosFiltrados = context.Ventas_Detalles
                .Join(context.Ventas,
                    detalles => detalles.Secuencial_Factura,
                    venta => venta.Secuencial,
                    (detalles, venta) => new { Detalles = detalles, Venta = venta })
                .Join(context.Usuarios,
                    combinado => combinado.Detalles.Secuencial_Usuario,
                    usuario => usuario.Secuencial,
                    (combinado, usuario) => new
                    {
                        Codigo = combinado.Detalles.Codigo,
                        Descripcion = combinado.Detalles.Descripcion,
                        Cantidad = combinado.Detalles.Cantidad,
                        FechaTexto = combinado.Venta.Fecha,
                        Usuario = usuario.Nombre
                    })
                .ToList() // rompe el árbol para trabajar en memoria
                .Select(r =>
                {
                    // Intenta convertir la fecha de texto a DateTime usando formato conocido
                    var fechaOk = DateTime.TryParseExact(
                        r.FechaTexto?.Trim(),
                        "dd/MM/yyyy HH:mm:ss", // ajusta según formato real de tu campo
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out var fechaConvertida
                    );

                    return new
                    {
                        r.Usuario,
                        r.Codigo,
                        r.Descripcion,
                        r.Cantidad,
                        Fecha = fechaOk ? fechaConvertida : (DateTime?)null
                    };
                })
                .Where(r => r.Fecha.HasValue && r.Fecha.Value >= fechaInicio && r.Fecha.Value <= fechaFin)
                .ToList();

            // 📊 Agrupar y sumar cantidades
            var datosAgrupados = registrosFiltrados
                .GroupBy(r => new { r.Usuario, r.Codigo, r.Descripcion })
                .Select(g => new
                {
                    Usuario = g.Key.Usuario,
                    CodigoProducto = g.Key.Codigo,
                    Descripción = g.Key.Descripcion,
                    CantidadVendida = g.Sum(x => x.Cantidad)
                })
                .OrderByDescending(r => r.CantidadVendida)
                .ToList();











            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    // 🧩 Encabezado general
                    page.Header().Column(header =>
                    {
                        header.Item().Text("📊 Productos Vendidos por Usuario").FontSize(20).Bold();
                        header.Item().Text($"Periodo: {fechaInicio:dd/MM/yyyy} - {fechaFin:dd/MM/yyyy}");
                        header.Item().Text($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}\n");


                    });

                    // 📊 Contenido agrupado por usuario
                    page.Content().Column(content =>
                    {
                        foreach (var grupo in datosAgrupados.GroupBy(g => g.Usuario))
                        {
                            // 👤 Encabezado del usuario
                            content.Item().Element(c =>
                                c.PaddingBottom(10)
                                  .Text($"👤 Usuario: {grupo.Key}")
                                  .FontSize(10).Bold()
 );
                            content.Item().Table(table =>
                            {
                                table.ColumnsDefinition(cols =>
                                {
                                    cols.RelativeColumn(); // Código
                                    cols.RelativeColumn(); // Descripción
                                    cols.ConstantColumn(80); // Cantidad
                                });

                                // 🏷 Encabezado de la tabla por grupo
                                table.Header(header =>
                                {
                                    header.Cell().Element(c => CellStyle(c)).Text("Código").Bold();
                                    header.Cell().Element(c => CellStyle(c)).Text("Producto").Bold();
                                    header.Cell().Element(c => CellStyle(c)).Text("Cantidad").Bold();
                                });

                                // 🧮 Filas del grupo
                                foreach (var item in grupo)
                                {
                                    table.Cell().Element(c => CellStyle(c)).Text(item.CodigoProducto);
                                    table.Cell().Element(c => CellStyle(c)).Text(item.Descripción);
                                    table.Cell().Element(c => CellStyle(c)).Text(item.CantidadVendida.ToString());
                                }
                            });
                        }
                    });

                    // 📝 Pie de página
                    page.Footer()
                        .AlignCenter()
                        .Text("Sistema Monitux-POS · Reporte generado automáticamente")
                        .FontSize(10).Italic();
                });
            })
.GeneratePdf("Reporte_Productos_Por_Usuario.pdf");

            // 🧱 Estilo de celda compartido
            static QuestPDF.Infrastructure.IContainer CellStyle(QuestPDF.Infrastructure.IContainer container) =>
                container.PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten2);





        }

        private void button8_Click(object sender, EventArgs e)
        {


            int secuencialProveedor = int.Parse(combo_Proveedor.SelectedItem.ToString().Split('-')[0].Trim());

            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            var fechaInicio = dateTimePicker1.Value.Date;
            var fechaFin = dateTimePicker2.Value.Date.AddDays(1).AddTicks(-1);

            // 🔍 Obtener facturas de compras del proveedor
            var registrosCompras = context.Compras
                .Where(c => c.Secuencial_Proveedor == secuencialProveedor)
                .Join(context.Proveedores,
                    compra => compra.Secuencial_Proveedor,
                    proveedor => proveedor.Secuencial,
                    (compra, proveedor) => new
                    {
                        SecuencialFactura = compra.Secuencial,
                        FechaTexto = compra.Fecha,
                        Proveedor = proveedor.Nombre,
                        TipoCompra = compra.Tipo,
                        TotalFactura = compra.Gran_Total
                    })
                .ToList()
                .Select(r =>
                {
                    var ok = DateTime.TryParseExact(
                        r.FechaTexto?.Trim(),
                        "dd/MM/yyyy HH:mm:ss",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out var fechaConvertida
                    );

                    return new
                    {
                        r.Proveedor,
                        r.TipoCompra,
                        r.TotalFactura,
                        r.SecuencialFactura,
                        Fecha = ok ? fechaConvertida : (DateTime?)null
                    };
                })
                .Where(r => r.Fecha.HasValue && r.Fecha.Value >= fechaInicio && r.Fecha.Value <= fechaFin)
                .ToList();

            // 📊 Agrupación por proveedor
            var datosAgrupados = registrosCompras
                .GroupBy(r => r.Proveedor)
                .Select(g => new
                {
                    Proveedor = g.Key,
                    Registros = g.OrderBy(r => r.Fecha).ToList(),
                    TotalProveedor = g.Sum(r => r.TotalFactura)
                })
                .ToList();

            // 📋 Totales por tipo de compra
            var totalContado = registrosCompras
                .Where(r => r.TipoCompra == "Contado")
                .Sum(r => r.TotalFactura);

            var totalCredito = registrosCompras
                .Where(r => r.TipoCompra == "Credito")
                .Sum(r => r.TotalFactura);

            var totalGeneral = registrosCompras.Sum(r => r.TotalFactura);






            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    page.Header().Column(header =>
                    {
                        header.Item().Text("📦 Compras por Proveedor").FontSize(20).Bold();
                        header.Item().Text($"Periodo: {fechaInicio:dd/MM/yyyy} - {fechaFin:dd/MM/yyyy}");
                        header.Item().Text($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}\n");
                    });

                    page.Content().Column(content =>
                    {
                        foreach (var grupo in datosAgrupados)
                        {
                            content.Item().Element(c =>
                                c.PaddingBottom(10)
                                 .Text($"🏢 Proveedor: {grupo.Proveedor}")
                                 .FontSize(10).Bold());

                            content.Item().Table(table =>
                            {
                                table.ColumnsDefinition(cols =>
                                {
                                    cols.ConstantColumn(60);   // Nº Factura
                                    cols.ConstantColumn(100);  // Fecha
                                    cols.RelativeColumn();     // Tipo compra
                                    cols.ConstantColumn(100);  // Total
                                });

                                table.Header(header =>
                                {
                                    header.Cell().Element(c => CellStyle(c)).Text("Factura").Bold();
                                    header.Cell().Element(c => CellStyle(c)).Text("Fecha").Bold();
                                    header.Cell().Element(c => CellStyle(c)).Text("Tipo de Compra").Bold();
                                    header.Cell().Element(c => CellStyle(c)).Text($"Total ({V_Menu_Principal.moneda})").Bold();
                                });

                                foreach (var registro in grupo.Registros)
                                {
                                    table.Cell().Element(c => CellStyle(c)).Text($"{registro.SecuencialFactura}");
                                    table.Cell().Element(c => CellStyle(c)).Text($"{registro.Fecha:dd/MM/yyyy}");
                                    table.Cell().Element(c => CellStyle(c)).Text($"{registro.TipoCompra}");
                                    table.Cell().Element(c => CellStyle(c)).Text($"{registro.TotalFactura:N2}");
                                }
                            });

                            content.Item().Element(c =>
                                c.PaddingBottom(10)
                                 .Text($"📥 Total comprado al proveedor: {grupo.TotalProveedor:N2} {V_Menu_Principal.moneda}")
                                 .FontSize(10).Bold());
                        }

                        content.Item().Element(c =>
                            c.PaddingTop(20)
                             .Column(col =>
                             {
                                 col.Item().Text("📋 Compras Totales").FontSize(12).Bold();
                                 col.Item().Text($"💳 Total CRÉDITO: {totalCredito:N2} {V_Menu_Principal.moneda}").FontSize(10);
                                 col.Item().Text($"💵 Total CONTADO: {totalContado:N2} {V_Menu_Principal.moneda}").FontSize(10);
                                 col.Item().Text($"🧮 Total GENERAL: {totalGeneral:N2} {V_Menu_Principal.moneda}").FontSize(10).Bold();
                             }));
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text("Sistema Monitux-POS · Reporte generado automáticamente")
                        .FontSize(10).Italic();
                });
            })
.GeneratePdf($"Reporte_Compras_Proveedor_{secuencialProveedor}.pdf");

            static QuestPDF.Infrastructure.IContainer CellStyle(QuestPDF.Infrastructure.IContainer container) =>
                container.PaddingVertical(5)
                         .BorderBottom(1)
                         .BorderColor(Colors.Grey.Lighten2);










        }

        private void button9_Click(object sender, EventArgs e)
        {




            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            var fechaInicio = dateTimePicker1.Value.Date;
            var fechaFin = dateTimePicker2.Value.Date.AddDays(1).AddTicks(-1); // incluye todo el segundo día

            var registrosFiltrados = context.Compras_Detalles
                .Join(context.Compras,
                    detalles => detalles.Secuencial_Factura,
                    compra => compra.Secuencial,
                    (detalles, compra) => new { Detalles = detalles, Compra = compra })
                .Join(context.Usuarios,
                    combinado => combinado.Compra.Secuencial_Usuario,
                    usuario => usuario.Secuencial,
                    (combinado, usuario) => new
                    {
                        Codigo = combinado.Detalles.Codigo,
                        Descripcion = combinado.Detalles.Descripcion,
                        Cantidad = combinado.Detalles.Cantidad,
                        FechaTexto = combinado.Compra.Fecha,
                        Usuario = usuario.Nombre
                    })
                .ToList()
                .Select(r =>
                {
                    var ok = DateTime.TryParseExact(
                        r.FechaTexto?.Trim(),
                        "dd/MM/yyyy HH:mm:ss", // ajusta según tu formato real
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out var fechaConvertida
                    );

                    return new
                    {
                        r.Usuario,
                        r.Codigo,
                        r.Descripcion,
                        r.Cantidad,
                        Fecha = ok ? fechaConvertida : (DateTime?)null
                    };
                })
                .Where(r => r.Fecha.HasValue && r.Fecha.Value >= fechaInicio && r.Fecha.Value <= fechaFin)
                .ToList();

            var datosAgrupados = registrosFiltrados
                .GroupBy(r => new { r.Usuario, r.Codigo, r.Descripcion })
                .Select(g => new
                {
                    Usuario = g.Key.Usuario,
                    CodigoProducto = g.Key.Codigo,
                    Descripción = g.Key.Descripcion,
                    CantidadComprada = g.Sum(x => x.Cantidad)
                })
                .OrderByDescending(r => r.CantidadComprada)
                .ToList();




            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    page.Header().Column(header =>
                    {
                        header.Item().Text("🧾 Productos Comprados por Usuario").FontSize(20).Bold();
                        header.Item().Text($"Periodo: {fechaInicio:dd/MM/yyyy} - {fechaFin:dd/MM/yyyy}");
                        header.Item().Text($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}\n");
                    });

                    page.Content().Column(content =>
                    {
                        foreach (var grupo in datosAgrupados.GroupBy(g => g.Usuario))
                        {
                            content.Item().Element(c =>
                                c.PaddingBottom(10)
                                  .Text($"👤 Usuario: {grupo.Key}")
                                  .FontSize(10).Bold());

                            content.Item().Table(table =>
                            {
                                table.ColumnsDefinition(cols =>
                                {
                                    cols.RelativeColumn(); // Código
                                    cols.RelativeColumn(); // Descripción
                                    cols.ConstantColumn(80); // Cantidad
                                });

                                table.Header(header =>
                                {
                                    header.Cell().Element(c => CellStyle(c)).Text("Código").Bold();
                                    header.Cell().Element(c => CellStyle(c)).Text("Producto").Bold();
                                    header.Cell().Element(c => CellStyle(c)).Text("Cantidad").Bold();
                                });

                                foreach (var item in grupo)
                                {
                                    table.Cell().Element(c => CellStyle(c)).Text(item.CodigoProducto);
                                    table.Cell().Element(c => CellStyle(c)).Text(item.Descripción);
                                    table.Cell().Element(c => CellStyle(c)).Text(item.CantidadComprada.ToString());
                                }
                            });
                        }
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text("Sistema Monitux-POS · Reporte generado automáticamente")
                        .FontSize(10).Italic();
                });
            })
.GeneratePdf("Reporte_Compras_Por_Usuario.pdf");

            static QuestPDF.Infrastructure.IContainer CellStyle(QuestPDF.Infrastructure.IContainer container) =>
                container.PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Grey.Lighten2);





        }

        private void button2_Click(object sender, EventArgs e)
        {







            ///////////////////////////





            secuencialCliente = int.Parse(combo_Cliente.SelectedItem.ToString().Split('-')[0].Trim()); ;

            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            var fechaInicio = dateTimePicker1.Value.Date;
            var fechaFin = dateTimePicker2.Value.Date.AddDays(1).AddTicks(-1);

            var registrosFacturas = context.Ventas
       .Where(v => v.Secuencial_Cliente == secuencialCliente)
       .Join(context.Clientes,
           venta => venta.Secuencial_Cliente,
           cliente => cliente.Secuencial,
           (venta, cliente) => new
           {
               SecuencialFactura = venta.Secuencial,            // 👈 número de factura
               FechaTexto = venta.Fecha,
               Cliente = cliente.Nombre,
               TipoVenta = venta.Tipo,
               TotalFactura = venta.Gran_Total
           })
       .ToList()
       .Select(r =>
       {
           var ok = DateTime.TryParseExact(
               r.FechaTexto?.Trim(),
               "dd/MM/yyyy HH:mm:ss",
               CultureInfo.InvariantCulture,
               DateTimeStyles.None,
               out var fechaConvertida
           );

           return new
           {
               r.Cliente,
               r.TipoVenta,
               r.TotalFactura,
               r.SecuencialFactura,                             // 👈 incluido en el modelo
               Fecha = ok ? fechaConvertida : (DateTime?)null
           };
       })
       .Where(r => r.Fecha.HasValue && r.Fecha.Value >= fechaInicio && r.Fecha.Value <= fechaFin)
       .ToList();


            // 📊 Agrupación por cliente (puede haber solo uno)
            var datosAgrupados = registrosFacturas
                .GroupBy(r => r.Cliente)
                .Select(g => new
                {
                    Cliente = g.Key,
                    Registros = g.OrderBy(r => r.Fecha).ToList(),
                    TotalCliente = g.Sum(r => r.TotalFactura)
                })
                .ToList();

            // 📋 Totales por tipo de venta
            var totalContado = registrosFacturas
                .Where(r => r.TipoVenta == "Contado")
                .Sum(r => r.TotalFactura);

            var totalCredito = registrosFacturas
                .Where(r => r.TipoVenta == "Credito")
                .Sum(r => r.TotalFactura);

            var totalGeneral = registrosFacturas.Sum(r => r.TotalFactura);

            // 🧾 PDF generado con QuestPDF
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    page.Header().Column(header =>
                    {
                        header.Item().Text("🧾 Ventas por Cliente").FontSize(20).Bold();
                        header.Item().Text($"Periodo: {fechaInicio:dd/MM/yyyy} - {fechaFin:dd/MM/yyyy}");
                        header.Item().Text($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}\n");
                    });

                    page.Content().Column(content =>
                    {
                        foreach (var grupo in datosAgrupados)
                        {
                            content.Item().Element(c =>
                                c.PaddingBottom(10)
                                 .Text($"👤 Cliente: {grupo.Cliente}")
                                 .FontSize(10).Bold());

                            content.Item().Table(table =>
                            {
                                table.ColumnsDefinition(cols =>
                                {

                                    cols.ConstantColumn(60);   // Nº Factura
                                    cols.ConstantColumn(100);  // Fecha
                                    cols.RelativeColumn();     // Tipo venta
                                    cols.ConstantColumn(100);  // Total


                                });

                                table.Header(header =>
                                {
                                    header.Cell().Element(c => CellStyle(c)).Text("Factura").Bold();
                                    header.Cell().Element(c => CellStyle(c)).Text("Fecha").Bold();
                                    header.Cell().Element(c => CellStyle(c)).Text("Tipo de Venta").Bold();
                                    header.Cell().Element(c => CellStyle(c)).Text($"Total ({V_Menu_Principal.moneda})").Bold();

                                });

                                foreach (var registro in grupo.Registros)
                                {
                                    table.Cell().Element(c => CellStyle(c)).Text($"{registro.SecuencialFactura}");
                                    table.Cell().Element(c => CellStyle(c)).Text($"{registro.Fecha:dd/MM/yyyy}");
                                    table.Cell().Element(c => CellStyle(c)).Text($"{registro.TipoVenta}");
                                    table.Cell().Element(c => CellStyle(c)).Text($"{registro.TotalFactura:N2}");

                                }
                            });

                            content.Item().Element(c =>
                                c.PaddingBottom(10)
                                 .Text($"📦 Total vendido al cliente: {grupo.TotalCliente:N2} {V_Menu_Principal.moneda}")
                                 .FontSize(10).Bold());
                        }

                        content.Item().Element(c =>
                            c.PaddingTop(20)
                             .Column(col =>
                             {
                                 col.Item().Text("📋 Ventas Totales").FontSize(12).Bold();
                                 col.Item().Text($"💳 Total CRÉDITO: {totalCredito:N2} {V_Menu_Principal.moneda}").FontSize(10);
                                 col.Item().Text($"💵 Total CONTADO: {totalContado:N2} {V_Menu_Principal.moneda}").FontSize(10);
                                 col.Item().Text($"🧮 Total GENERAL: {totalGeneral:N2}  {V_Menu_Principal.moneda}").FontSize(10).Bold();
                             }));
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text("Sistema Monitux-POS · Reporte generado automáticamente")
                        .FontSize(10).Italic();
                });
            })
            .GeneratePdf($"Reporte_Ventas_Cliente_{secuencialCliente}.pdf");

            // Estilo de celdas
            static QuestPDF.Infrastructure.IContainer CellStyle(QuestPDF.Infrastructure.IContainer container) =>
                container.PaddingVertical(5)
                         .BorderBottom(1)
                         .BorderColor(Colors.Grey.Lighten2);




            //  




            ////////////////////////










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





        private void V_Reportes_Facturas_Load(object sender, EventArgs e)
        {
            llenar_Combo_Cliente();
            llenar_Combo_Proveedor();
        }

        private void combo_Cliente_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void combo_Cliente_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {



            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            var fechaInicio = dateTimePicker1.Value.Date;
            var fechaFin = dateTimePicker2.Value.Date.AddDays(1).AddTicks(-1);

            // 🔍 Todas las ventas en el periodo
            var registrosVentas = context.Ventas
                .Join(context.Clientes,
                    venta => venta.Secuencial_Cliente,
                    cliente => cliente.Secuencial,
                    (venta, cliente) => new
                    {
                        SecuencialFactura = venta.Secuencial,
                        FechaTexto = venta.Fecha,
                        Cliente = cliente.Nombre,
                        TipoVenta = venta.Tipo,
                        TotalFactura = venta.Gran_Total
                    })
                .ToList()
                .Select(r =>
                {
                    var ok = DateTime.TryParseExact(
                        r.FechaTexto?.Trim(),
                        "dd/MM/yyyy HH:mm:ss",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out var fechaConvertida
                    );

                    return new
                    {
                        r.SecuencialFactura,
                        r.Cliente,
                        r.TipoVenta,
                        r.TotalFactura,
                        Fecha = ok ? fechaConvertida : (DateTime?)null
                    };
                })
                .Where(r => r.Fecha.HasValue && r.Fecha.Value >= fechaInicio && r.Fecha.Value <= fechaFin)
                .OrderBy(r => r.Fecha)
                .ToList();

            // 📊 Totales por tipo de venta
            var totalContado = registrosVentas
                .Where(r => r.TipoVenta == "Contado")
                .Sum(r => r.TotalFactura);

            var totalCredito = registrosVentas
                .Where(r => r.TipoVenta == "Credito")
                .Sum(r => r.TotalFactura);

            var totalGeneral = registrosVentas.Sum(r => r.TotalFactura);




            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    page.Header().Column(header =>
                    {
                        header.Item().Text("🧾 Reporte General de Ventas").FontSize(20).Bold();
                        header.Item().Text($"Periodo: {fechaInicio:dd/MM/yyyy} - {fechaFin:dd/MM/yyyy}");
                        header.Item().Text($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}\n");
                    });

                    page.Content().Column(content =>
                    {
                        content.Item().Table(table =>
                        {
                            table.ColumnsDefinition(cols =>
                            {
                                cols.ConstantColumn(60);   // Nº Factura
                                cols.ConstantColumn(100);  // Fecha
                                cols.RelativeColumn();     // Cliente
                                cols.RelativeColumn();     // Tipo venta
                                cols.ConstantColumn(100);  // Total
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(c => CellStyle(c)).Text("Factura").Bold();
                                header.Cell().Element(c => CellStyle(c)).Text("Fecha").Bold();
                                header.Cell().Element(c => CellStyle(c)).Text("Cliente").Bold();
                                header.Cell().Element(c => CellStyle(c)).Text("Tipo").Bold();
                                header.Cell().Element(c => CellStyle(c)).Text($"Total ({V_Menu_Principal.moneda})").Bold();
                            });

                            foreach (var r in registrosVentas)
                            {
                                table.Cell().Element(c => CellStyle(c)).Text($"{r.SecuencialFactura}");
                                table.Cell().Element(c => CellStyle(c)).Text($"{r.Fecha:dd/MM/yyyy}");
                                table.Cell().Element(c => CellStyle(c)).Text(r.Cliente);
                                table.Cell().Element(c => CellStyle(c)).Text(r.TipoVenta);
                                table.Cell().Element(c => CellStyle(c)).Text($"{r.TotalFactura:N2}");
                            }
                        });

                        content.Item().Element(c =>
                            c.PaddingTop(20)
                             .Column(col =>
                             {
                                 col.Item().Text("📋 Totales por Tipo de Venta").FontSize(12).Bold();
                                 col.Item().Text($"💳 Total CRÉDITO: {totalCredito:N2} {V_Menu_Principal.moneda}").FontSize(10);
                                 col.Item().Text($"💵 Total CONTADO: {totalContado:N2} {V_Menu_Principal.moneda}").FontSize(10);
                                 col.Item().Text($"🧮 TOTAL GENERAL: {totalGeneral:N2} {V_Menu_Principal.moneda}").FontSize(10).Bold();
                             }));
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text("Sistema Monitux-POS · Reporte generado automáticamente")
                        .FontSize(10).Italic();
                });
            })
.GeneratePdf("Reporte_Ventas_General.pdf");

            // Estilo de celdas
            static QuestPDF.Infrastructure.IContainer CellStyle(QuestPDF.Infrastructure.IContainer container) =>
                container.PaddingVertical(5)
                         .BorderBottom(1)
                         .BorderColor(Colors.Grey.Lighten2);







        }

        private void button7_Click(object sender, EventArgs e)
        {



            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            var fechaInicio = dateTimePicker1.Value.Date;
            var fechaFin = dateTimePicker2.Value.Date.AddDays(1).AddTicks(-1);

            // 🔍 Obtener compras en el rango de fechas
            var registrosCompras = context.Compras
                .Join(context.Proveedores,
                    compra => compra.Secuencial_Proveedor,
                    proveedor => proveedor.Secuencial,
                    (compra, proveedor) => new
                    {
                        SecuencialFactura = compra.Secuencial,
                        FechaTexto = compra.Fecha,
                        Proveedor = proveedor.Nombre,
                        TipoCompra = compra.Tipo,
                        TotalFactura = compra.Gran_Total
                    })
                .ToList()
                .Select(r =>
                {
                    var ok = DateTime.TryParseExact(
                        r.FechaTexto?.Trim(),
                        "dd/MM/yyyy HH:mm:ss",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out var fechaConvertida
                    );

                    return new
                    {
                        r.SecuencialFactura,
                        r.Proveedor,
                        r.TipoCompra,
                        r.TotalFactura,
                        Fecha = ok ? fechaConvertida : (DateTime?)null
                    };
                })
                .Where(r => r.Fecha.HasValue && r.Fecha.Value >= fechaInicio && r.Fecha.Value <= fechaFin)
                .OrderBy(r => r.Fecha)
                .ToList();

            // 📊 Totales por tipo
            var totalContado = registrosCompras
                .Where(r => r.TipoCompra == "Contado")
                .Sum(r => r.TotalFactura);

            var totalCredito = registrosCompras
                .Where(r => r.TipoCompra == "Credito")
                .Sum(r => r.TotalFactura);

            var totalGeneral = registrosCompras.Sum(r => r.TotalFactura);




            // 🧾 Generación del PDF
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    page.Header().Column(header =>
                    {
                        header.Item().Text("📦 Reporte General de Compras").FontSize(20).Bold();
                        header.Item().Text($"Periodo: {fechaInicio:dd/MM/yyyy} - {fechaFin:dd/MM/yyyy}");
                        header.Item().Text($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}\n");
                    });

                    page.Content().Column(content =>
                    {
                        content.Item().Table(table =>
                        {
                            table.ColumnsDefinition(cols =>
                            {
                                cols.ConstantColumn(60);   // Nº Factura
                                cols.ConstantColumn(100);  // Fecha
                                cols.RelativeColumn();     // Proveedor
                                cols.RelativeColumn();     // Tipo compra
                                cols.ConstantColumn(100);  // Total
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(c => CellStyle(c)).Text("Factura").Bold();
                                header.Cell().Element(c => CellStyle(c)).Text("Fecha").Bold();
                                header.Cell().Element(c => CellStyle(c)).Text("Proveedor").Bold(); // 🧑‍💼 Ajustado aquí
                                header.Cell().Element(c => CellStyle(c)).Text("Tipo").Bold();
                                header.Cell().Element(c => CellStyle(c)).Text($"Total ({V_Menu_Principal.moneda})").Bold();
                            });

                            foreach (var r in registrosCompras)
                            {
                                table.Cell().Element(c => CellStyle(c)).Text($"{r.SecuencialFactura}");
                                table.Cell().Element(c => CellStyle(c)).Text($"{r.Fecha:dd/MM/yyyy}");
                                table.Cell().Element(c => CellStyle(c)).Text(r.Proveedor); // 🧑‍💼 Ajustado aquí
                                table.Cell().Element(c => CellStyle(c)).Text(r.TipoCompra);
                                table.Cell().Element(c => CellStyle(c)).Text($"{r.TotalFactura:N2}");
                            }
                        });

                        content.Item().Element(c =>
                            c.PaddingTop(20)
                                 .Column(col =>
                                 {
                                     col.Item().Text("📋 Totales por Tipo de Compra").FontSize(12).Bold();
                                     col.Item().Text($"💳 Total CRÉDITO: {totalCredito:N2} {V_Menu_Principal.moneda}").FontSize(10);
                                     col.Item().Text($"💵 Total CONTADO: {totalContado:N2} {V_Menu_Principal.moneda}").FontSize(10);
                                     col.Item().Text($"🧮 TOTAL GENERAL: {totalGeneral:N2} {V_Menu_Principal.moneda}").FontSize(10).Bold();
                                 }));
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text("Sistema Monitux-POS · Reporte generado automáticamente")
                        .FontSize(10).Italic();
                });
            })
            .GeneratePdf("Reporte_Compras_General.pdf");

            // 🎨 Estilo de celda compartido
            static QuestPDF.Infrastructure.IContainer CellStyle(QuestPDF.Infrastructure.IContainer container) =>
                container.PaddingVertical(5)
                         .BorderBottom(1)
                         .BorderColor(Colors.Grey.Lighten2);




        }

        private void button1_Click(object sender, EventArgs e)
        {
            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            var fechaInicio = dateTimePicker1.Value.Date;
            var fechaFin = dateTimePicker2.Value.Date.AddDays(1).AddTicks(-1);
            var tipoSeleccionado = combo_TipoVenta.SelectedItem.ToString().Trim();

            var registrosVentas = context.Ventas
                .Where(v => v.Tipo == tipoSeleccionado)
                .Join(context.Clientes,
                    venta => venta.Secuencial_Cliente,
                    cliente => cliente.Secuencial,
                    (venta, cliente) => new
                    {
                        SecuencialFactura = venta.Secuencial,
                        FechaTexto = venta.Fecha,
                        Cliente = cliente.Nombre,
                        TipoVenta = venta.Tipo,
                        TotalFactura = venta.Gran_Total
                    })
                .ToList()
                .Select(r =>
                {
                    var ok = DateTime.TryParseExact(
                        r.FechaTexto?.Trim(),
                         "dd/MM/yyyy HH:mm:ss",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out var fechaConvertida
                    );

                    return new
                    {
                        r.SecuencialFactura,
                        r.Cliente,
                        r.TipoVenta,
                        r.TotalFactura,
                        Fecha = ok ? fechaConvertida : (DateTime?)null
                    };
                })
                .Where(r => r.Fecha.HasValue && r.Fecha.Value >= fechaInicio && r.Fecha.Value <= fechaFin)
                .OrderBy(r => r.Fecha)
                .ToList();

            var totalFiltrado = registrosVentas.Sum(r => r.TotalFactura);




            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    page.Header().Column(header =>
                    {
                        header.Item().Text("🧾 Reporte de Ventas por Tipo").FontSize(20).Bold();
                        header.Item().Text($"Tipo: {tipoSeleccionado.ToUpperInvariant()}");
                        header.Item().Text($"Periodo: {fechaInicio:dd/MM/yyyy} - {fechaFin:dd/MM/yyyy}");
                        header.Item().Text($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}");
                    });

                    page.Content().Column(content =>
                    {
                        content.Item().Table(table =>
                        {
                            table.ColumnsDefinition(cols =>
                            {
                                cols.ConstantColumn(60);
                                cols.ConstantColumn(100);
                                cols.RelativeColumn();
                                cols.ConstantColumn(100);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(c => CellStyle(c)).Text("Factura").Bold();
                                header.Cell().Element(c => CellStyle(c)).Text("Fecha").Bold();
                                header.Cell().Element(c => CellStyle(c)).Text("Cliente").Bold();
                                header.Cell().Element(c => CellStyle(c)).Text($"Total ({V_Menu_Principal.moneda})").Bold();
                            });

                            foreach (var r in registrosVentas)
                            {
                                table.Cell().Element(c => CellStyle(c)).Text($"{r.SecuencialFactura}");
                                table.Cell().Element(c => CellStyle(c)).Text($"{r.Fecha:dd/MM/yyyy}");
                                table.Cell().Element(c => CellStyle(c)).Text($"{r.Cliente}");
                                table.Cell().Element(c => CellStyle(c)).Text($"{r.TotalFactura:N2}");
                            }
                        });

                        content.Item().Element(c =>
                            c.PaddingTop(20)
                                 .Text($"🧮 TOTAL ({tipoSeleccionado.ToUpperInvariant()}): {totalFiltrado:N2} {V_Menu_Principal.moneda}")
                                 .FontSize(10).Bold());
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text("Sistema Monitux-POS · Reporte generado automáticamente")
                        .FontSize(10).Italic();
                });
            })
.GeneratePdf($"Reporte_Ventas_Tipo_{tipoSeleccionado.ToUpperInvariant()}.pdf");

            static QuestPDF.Infrastructure.IContainer CellStyle(QuestPDF.Infrastructure.IContainer container) =>
                container.PaddingVertical(5)
                         .BorderBottom(1)
                         .BorderColor(Colors.Grey.Lighten2);



        }

        private void button6_Click(object sender, EventArgs e)
        {



            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            var fechaInicio = dateTimePicker1.Value.Date;
            var fechaFin = dateTimePicker2.Value.Date.AddDays(1).AddTicks(-1);
            var tipoSeleccionado = combo_TipoCompra.SelectedItem.ToString().Trim();

            // 🔍 Filtrar compras por tipo
            var registrosFiltrados = context.Compras
                .Where(c => c.Tipo == tipoSeleccionado)
                .Join(context.Proveedores,
                    compra => compra.Secuencial_Proveedor,
                    proveedor => proveedor.Secuencial,
                    (compra, proveedor) => new
                    {
                        SecuencialFactura = compra.Secuencial,
                        FechaTexto = compra.Fecha,
                        Proveedor = proveedor.Nombre,
                        TipoCompra = compra.Tipo,
                        TotalFactura = compra.Gran_Total
                    })
                .ToList()
                .Select(r =>
                {
                    var ok = DateTime.TryParseExact(
                        r.FechaTexto?.Trim(),
                        "dd/MM/yyyy HH:mm:ss",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out var fechaConvertida
                    );

                    return new
                    {
                        r.SecuencialFactura,
                        r.Proveedor,
                        r.TipoCompra,
                        r.TotalFactura,
                        Fecha = ok ? fechaConvertida : (DateTime?)null
                    };
                })
                .Where(r => r.Fecha.HasValue && r.Fecha.Value >= fechaInicio && r.Fecha.Value <= fechaFin)
                .OrderBy(r => r.Fecha)
                .ToList();

            var totalFiltrado = registrosFiltrados.Sum(r => r.TotalFactura);



            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    page.Header().Column(header =>
                    {
                        header.Item().Text("📦 Compras por Tipo").FontSize(20).Bold();
                        header.Item().Text($"Tipo: {tipoSeleccionado.ToUpperInvariant()}");
                        header.Item().Text($"Periodo: {fechaInicio:dd/MM/yyyy} - {fechaFin:dd/MM/yyyy}");
                        header.Item().Text($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}");
                    });

                    page.Content().Column(content =>
                    {
                        content.Item().Table(table =>
                        {
                            table.ColumnsDefinition(cols =>
                            {
                                cols.ConstantColumn(60);   // Nº Factura
                                cols.ConstantColumn(100);  // Fecha
                                cols.RelativeColumn();     // Proveedor
                                cols.ConstantColumn(100);  // Total
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(c => CellStyle(c)).Text("Factura").Bold();
                                header.Cell().Element(c => CellStyle(c)).Text("Fecha").Bold();
                                header.Cell().Element(c => CellStyle(c)).Text("Proveedor").Bold();
                                header.Cell().Element(c => CellStyle(c)).Text($"Total ({V_Menu_Principal.moneda})").Bold();
                            });

                            foreach (var r in registrosFiltrados)
                            {
                                table.Cell().Element(c => CellStyle(c)).Text($"{r.SecuencialFactura}");
                                table.Cell().Element(c => CellStyle(c)).Text($"{r.Fecha:dd/MM/yyyy}");
                                table.Cell().Element(c => CellStyle(c)).Text(r.Proveedor);
                                table.Cell().Element(c => CellStyle(c)).Text($"{r.TotalFactura:N2}");
                            }
                        });

                        content.Item().Element(c =>
                            c.PaddingTop(20)
                                 .Text($"🧮 TOTAL ({tipoSeleccionado.ToUpperInvariant()}): {totalFiltrado:N2} {V_Menu_Principal.moneda}")
                                 .FontSize(10).Bold());
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text("Sistema Monitux-POS · Reporte generado automáticamente")
                        .FontSize(10).Italic();
                });
            })
.GeneratePdf($"Reporte_Compras_Tipo_{tipoSeleccionado.ToUpperInvariant()}.pdf");

            static QuestPDF.Infrastructure.IContainer CellStyle(QuestPDF.Infrastructure.IContainer container) =>
                container.PaddingVertical(5)
                         .BorderBottom(1)
                         .BorderColor(Colors.Grey.Lighten2);




        }

        private void button3_Click(object sender, EventArgs e)
        {
            double montoMinimo = (double)Convert.ToDecimal(vmin.Text);
            double montoMaximo = (double)Convert.ToDecimal(vmax.Text);

            var fechaInicio = dateTimePicker1.Value.Date;
            var fechaFin = dateTimePicker2.Value.Date.AddDays(1).AddTicks(-1);

            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            var registrosFiltrados = context.Ventas
    .Join(context.Clientes,
        venta => venta.Secuencial_Cliente,
        cliente => cliente.Secuencial,
        (venta, cliente) => new
        {
            SecuencialFactura = venta.Secuencial,
            FechaTexto = venta.Fecha,
            Cliente = cliente.Nombre,
            TipoVenta = venta.Tipo,
            TotalFactura = venta.Gran_Total
        })
    .ToList()
    .Select(r =>
    {
        var ok = DateTime.TryParseExact(
            r.FechaTexto?.Trim(),
            "dd/MM/yyyy HH:mm:ss",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out var fechaConvertida
        );

        return new
        {
            r.SecuencialFactura,
            r.Cliente,
            r.TipoVenta,
            r.TotalFactura,
            Fecha = ok ? fechaConvertida : (DateTime?)null
        };
    })
    .Where(r => r.Fecha.HasValue
             && r.Fecha.Value >= fechaInicio
             && r.Fecha.Value <= fechaFin
             && r.TotalFactura >= montoMinimo
             && r.TotalFactura <= montoMaximo)
    .OrderBy(r => r.Fecha)
    .ToList();

            var totalFiltrado = registrosFiltrados.Sum(r => r.TotalFactura);



            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    page.Header().Column(header =>
                    {
                        header.Item().Text("🧾 Ventas por Rango de Monto").FontSize(20).Bold();
                        header.Item().Text($"Monto entre: {montoMinimo:N2} y {montoMaximo:N2} {V_Menu_Principal.moneda}");
                        header.Item().Text($"Periodo: {fechaInicio:dd/MM/yyyy} - {fechaFin:dd/MM/yyyy}");
                        header.Item().Text($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}");
                    });

                    page.Content().Column(content =>
                    {
                        content.Item().Table(table =>
                        {
                            table.ColumnsDefinition(cols =>
                            {
                                cols.ConstantColumn(60);   // Nº Factura
                                cols.ConstantColumn(100);  // Fecha
                                cols.RelativeColumn();     // Cliente
                                cols.RelativeColumn();     // Tipo Venta
                                cols.ConstantColumn(100);  // Total
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(c => CellStyle(c)).Text("Factura").Bold();
                                header.Cell().Element(c => CellStyle(c)).Text("Fecha").Bold();
                                header.Cell().Element(c => CellStyle(c)).Text("Cliente").Bold();
                                header.Cell().Element(c => CellStyle(c)).Text("Tipo").Bold();
                                header.Cell().Element(c => CellStyle(c)).Text($"Total ({V_Menu_Principal.moneda})").Bold();
                            });

                            foreach (var r in registrosFiltrados)
                            {
                                table.Cell().Element(c => CellStyle(c)).Text($"{r.SecuencialFactura}");
                                table.Cell().Element(c => CellStyle(c)).Text($"{r.Fecha:dd/MM/yyyy}");
                                table.Cell().Element(c => CellStyle(c)).Text($"{r.Cliente}");
                                table.Cell().Element(c => CellStyle(c)).Text($"{r.TipoVenta}");
                                table.Cell().Element(c => CellStyle(c)).Text($"{r.TotalFactura:N2}");
                            }
                        });

                        content.Item().Element(c =>
                            c.PaddingTop(20)
                                 .Text($"🧮 TOTAL DEL RANGO: {totalFiltrado:N2} {V_Menu_Principal.moneda}")
                                 .FontSize(10).Bold());
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text("Sistema Monitux-POS · Reporte generado automáticamente")
                        .FontSize(10).Italic();
                });
            })
.GeneratePdf("Reporte_Ventas_RangoTotales.pdf");

            static QuestPDF.Infrastructure.IContainer CellStyle(QuestPDF.Infrastructure.IContainer container) =>
                container.PaddingVertical(5)
                         .BorderBottom(1)
                         .BorderColor(Colors.Grey.Lighten2);




        }

        private void button10_Click(object sender, EventArgs e)
        {




         

            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            var fechaInicio = dateTimePicker1.Value.Date;
            var fechaFin = dateTimePicker2.Value.Date.AddDays(1).AddTicks(-1);
            double montoMinimo = (double)Convert.ToDecimal(cmin.Text);
            double montoMaximo = (double)Convert.ToDecimal(cmax.Text);

            // 🔍 Filtrar compras dentro del rango de totales
            var registrosFiltrados = context.Compras
                .Join(context.Proveedores,
                    compra => compra.Secuencial_Proveedor,
                    proveedor => proveedor.Secuencial,
                    (compra, proveedor) => new
                    {
                        SecuencialFactura = compra.Secuencial,
                        FechaTexto = compra.Fecha,
                        Proveedor = proveedor.Nombre,
                        TipoCompra = compra.Tipo,
                        TotalFactura = compra.Gran_Total
                    })
                .ToList()
                .Select(r =>
                {
                    var ok = DateTime.TryParseExact(
                        r.FechaTexto?.Trim(),
                        "dd/MM/yyyy HH:mm:ss",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out var fechaConvertida
                    );

                    return new
                    {
                        r.SecuencialFactura,
                        r.Proveedor,
                        r.TipoCompra,
                        r.TotalFactura,
                        Fecha = ok ? fechaConvertida : (DateTime?)null
                    };
                })
                .Where(r => r.Fecha.HasValue
                         && r.Fecha.Value >= fechaInicio
                         && r.Fecha.Value <= fechaFin
                         && r.TotalFactura >= montoMinimo
                         && r.TotalFactura <= montoMaximo)
                .OrderBy(r => r.Fecha)
                .ToList();

            var totalFiltrado = registrosFiltrados.Sum(r => r.TotalFactura);


            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    page.Header().Column(header =>
                    {
                        header.Item().Text("📦 Compras por Rango de Monto").FontSize(20).Bold();
                        header.Item().Text($"Monto entre: {montoMinimo:N2} y {montoMaximo:N2} {V_Menu_Principal.moneda}");
                        header.Item().Text($"Periodo: {fechaInicio:dd/MM/yyyy} - {fechaFin:dd/MM/yyyy}");
                        header.Item().Text($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}");
                    });

                    page.Content().Column(content =>
                    {
                        content.Item().Table(table =>
                        {
                            table.ColumnsDefinition(cols =>
                            {
                                cols.ConstantColumn(60);   // Nº Factura
                                cols.ConstantColumn(100);  // Fecha
                                cols.RelativeColumn();     // Proveedor
                                cols.RelativeColumn();     // Tipo compra
                                cols.ConstantColumn(100);  // Total
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(c => CellStyle(c)).Text("Factura").Bold();
                                header.Cell().Element(c => CellStyle(c)).Text("Fecha").Bold();
                                header.Cell().Element(c => CellStyle(c)).Text("Proveedor").Bold();
                                header.Cell().Element(c => CellStyle(c)).Text("Tipo").Bold();
                                header.Cell().Element(c => CellStyle(c)).Text($"Total ({V_Menu_Principal.moneda})").Bold();
                            });

                            foreach (var r in registrosFiltrados)
                            {
                                table.Cell().Element(c => CellStyle(c)).Text($"{r.SecuencialFactura}");
                                table.Cell().Element(c => CellStyle(c)).Text($"{r.Fecha:dd/MM/yyyy}");
                                table.Cell().Element(c => CellStyle(c)).Text(r.Proveedor);
                                table.Cell().Element(c => CellStyle(c)).Text(r.TipoCompra);
                                table.Cell().Element(c => CellStyle(c)).Text($"{r.TotalFactura:N2}");
                            }
                        });

                        content.Item().Element(c =>
                            c.PaddingTop(20)
                                 .Text($"🧮 TOTAL DEL RANGO: {totalFiltrado:N2} {V_Menu_Principal.moneda}")
                                 .FontSize(10).Bold());
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text("Sistema Monitux-POS · Reporte generado automáticamente")
                        .FontSize(10).Italic();
                });
            })
.GeneratePdf("Reporte_Compras_RangoTotales.pdf");

            static QuestPDF.Infrastructure.IContainer CellStyle(QuestPDF.Infrastructure.IContainer container) =>
                container.PaddingVertical(5)
                         .BorderBottom(1)
                         .BorderColor(Colors.Grey.Lighten2);



        }
    }
}
