using Microsoft.EntityFrameworkCore;
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
    public partial class V_Reportes_Inventario : Form
    {
        public string ruta = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Resources\\Reportes\\");
        public V_Reportes_Inventario()
        {
            InitializeComponent();
        }

        private void button8_Click(object sender, EventArgs e)
        {

            //////////////////



            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            // Definir la empresa activa (por ejemplo obtenida de sesión, variable, combo, etc.)
            int secuencialEmpresaActiva = V_Menu_Principal.Secuencial_Empresa; // ← reemplaza con tu valor dinámico

            var productos = context.Productos
                .Where(p => p.Secuencial_Empresa == secuencialEmpresaActiva) // ← filtro aplicado aquí
                .Join(context.Proveedores,
                    producto => producto.Secuencial_Proveedor,
                    proveedor => proveedor.Secuencial,
                    (producto, proveedor) => new { producto, proveedor })
                .Join(context.Categorias,
                    x => x.producto.Secuencial_Categoria,
                    categoria => categoria.Secuencial,
                    (x, categoria) => new
                    {
                        Codigo = x.producto.Codigo,
                        Descripcion = x.producto.Descripcion,
                        Marca = x.producto.Marca,
                        PrecioVenta = x.producto.Precio_Venta,
                        PrecioCosto = x.producto.Precio_Costo,
                        Cantidad = x.producto.Cantidad,
                        StockMinimo = x.producto.Existencia_Minima,
                        Categoria = categoria.Nombre,
                        Proveedor = x.proveedor.Nombre
                    })
                .OrderBy(p => p.Codigo)
                .ToList();

            // 🖨️ Generar PDF directamente
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(9));

                    // Encabezado
                    page.Header().Column(header =>
                    {
                        header.Item().Text("📦 Reporte de Productos Registrados").FontSize(20).Bold();
                        header.Item().Text($"Total productos: {productos.Count}");
                        header.Item().Text($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}");
                    });

                    // Tabla
                    page.Content().Table(tabla =>
                    {
                        tabla.ColumnsDefinition(cols =>
                        {
                            cols.RelativeColumn(1); // Código
                            cols.RelativeColumn(2); // Descripción
                            cols.RelativeColumn(1); // Marca
                            cols.RelativeColumn(1); // Precio Venta
                            cols.RelativeColumn(1); // Precio Costo
                            cols.RelativeColumn(1); // Cantidad
                            cols.RelativeColumn(1); // Stock Mínimo
                            cols.RelativeColumn(1); // Categoría
                            cols.RelativeColumn(1); // Proveedor
                        });

                        // Encabezado de columnas
                        tabla.Header(header =>
                        {
                            header.Cell().Element(c => c.Background(Colors.Grey.Lighten3).PaddingVertical(4).PaddingHorizontal(2).ShowOnce()).Text("Código").Bold();
                            header.Cell().Element(c => c.Background(Colors.Grey.Lighten3).PaddingVertical(4).PaddingHorizontal(2).ShowOnce()).Text("Descripción").Bold();
                            header.Cell().Element(c => c.Background(Colors.Grey.Lighten3).PaddingVertical(4).PaddingHorizontal(2).ShowOnce()).Text("Marca").Bold();
                            header.Cell().Element(c => c.Background(Colors.Grey.Lighten3).PaddingVertical(4).PaddingHorizontal(2).ShowOnce()).Text("Venta (€)").Bold();
                            header.Cell().Element(c => c.Background(Colors.Grey.Lighten3).PaddingVertical(4).PaddingHorizontal(2).ShowOnce()).Text("Costo (€)").Bold();
                            header.Cell().Element(c => c.Background(Colors.Grey.Lighten3).PaddingVertical(4).PaddingHorizontal(2).ShowOnce()).Text("Cantidad").Bold();
                            header.Cell().Element(c => c.Background(Colors.Grey.Lighten3).PaddingVertical(4).PaddingHorizontal(2).ShowOnce()).Text("Stock Min.").Bold();
                            header.Cell().Element(c => c.Background(Colors.Grey.Lighten3).PaddingVertical(4).PaddingHorizontal(2).ShowOnce()).Text("Categoría").Bold();
                            header.Cell().Element(c => c.Background(Colors.Grey.Lighten3).PaddingVertical(4).PaddingHorizontal(2).ShowOnce()).Text("Proveedor").Bold();
                        });

                        // Filas de productos
                        foreach (var p in productos)
                        {
                            tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).PaddingVertical(2).PaddingHorizontal(2)).Text(p.Codigo);
                            tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).PaddingVertical(2).PaddingHorizontal(2)).Text(p.Descripcion);
                            tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).PaddingVertical(2).PaddingHorizontal(2)).Text(p.Marca);
                            tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).PaddingVertical(2).PaddingHorizontal(2)).Text($"{p.PrecioVenta:N2}");
                            tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).PaddingVertical(2).PaddingHorizontal(2)).Text($"{p.PrecioCosto:N2}");
                            tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).PaddingVertical(2).PaddingHorizontal(2)).Text($"{p.Cantidad}");
                            tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).PaddingVertical(2).PaddingHorizontal(2)).Text($"{p.StockMinimo}");
                            tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).PaddingVertical(2).PaddingHorizontal(2)).Text(p.Categoria);
                            tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).PaddingVertical(2).PaddingHorizontal(2)).Text(p.Proveedor);
                        }
                    });

                    // Footer
                    page.Footer().AlignCenter()
                        .Text("Sistema Monitux-POS · Reporte generado automáticamente")
                        .FontSize(10).Italic();
                });
            })
            .GeneratePdf($"{ruta}Reporte_ProductosRegistrados.pdf");



            V_Menu_Principal.MSG.ShowMSG("🖨️ Reporte generado correctamente", "Monitux-POS");
            V_Visor_Factura _Visor_Factura = new V_Visor_Factura();
            _Visor_Factura.rutaArchivo = ($"{ruta}Reporte_ProductosRegistrados.pdf");
            _Visor_Factura.ShowDialog();

            //System.Diagnostics.Process.Start("Reporte_ProductosRegistrados.pdf");


            /////////////////

        }

        private void button7_Click(object sender, EventArgs e)
        {
            /////////////////

            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            // Definir empresa activa
            int secuencialEmpresaActiva = V_Menu_Principal.Secuencial_Empresa; // ← sustituye con tu valor dinámico

            // Consulta con filtro por empresa y JOINs
            var productosPorMarca = context.Productos
                .Where(p => p.Secuencial_Empresa == secuencialEmpresaActiva) // ✅ filtro por empresa
                .Join(context.Proveedores,
                    producto => producto.Secuencial_Proveedor,
                    proveedor => proveedor.Secuencial,
                    (producto, proveedor) => new { producto, proveedor })
                .Join(context.Categorias,
                    x => x.producto.Secuencial_Categoria,
                    categoria => categoria.Secuencial,
                    (x, categoria) => new
                    {
                        Marca = x.producto.Marca,
                        Codigo = x.producto.Codigo,
                        Descripcion = x.producto.Descripcion,
                        PrecioVenta = x.producto.Precio_Venta,
                        PrecioCosto = x.producto.Precio_Costo,
                        Cantidad = x.producto.Cantidad,
                        StockMinimo = x.producto.Existencia_Minima,
                        Categoria = categoria.Nombre,
                        Proveedor = x.proveedor.Nombre
                    })
                .OrderBy(p => p.Marca)
                .ThenBy(p => p.Codigo)
                .ToList()
                .GroupBy(p => p.Marca);

            // 🖨️ Generar PDF
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(9));
                    page.PageColor(Colors.White);

                    // Encabezado
                    page.Header().Column(header =>
                    {
                        header.Item().Text("🏷️ Reporte de Productos por Marca").FontSize(20).Bold();
                        header.Item().Text($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}");
                    });

                    // Contenido agrupado
                    page.Content().Column(col =>
                    {
                        foreach (var grupo in productosPorMarca)
                        {
                            col.Item().Text($"🔹 Marca: {grupo.Key}").FontSize(11).Bold().FontColor(Colors.Blue.Darken2);
                            col.Item().Table(tabla =>
                            {
                                tabla.ColumnsDefinition(cols =>
                                {
                                    cols.RelativeColumn(1); // Código
                                    cols.RelativeColumn(2); // Descripción
                                    cols.RelativeColumn(1); // Venta
                                    cols.RelativeColumn(1); // Costo
                                    cols.RelativeColumn(1); // Cantidad
                                    cols.RelativeColumn(1); // Stock Min
                                    cols.RelativeColumn(1); // Categoría
                                    cols.RelativeColumn(1); // Proveedor
                                });

                                tabla.Header(header =>
                                {
                                    header.Cell().Element(c => c.Background(Colors.Grey.Lighten3).Padding(4).ShowOnce()).Text("Código").Bold();
                                    header.Cell().Element(c => c.Background(Colors.Grey.Lighten3).Padding(4).ShowOnce()).Text("Descripción").Bold();
                                    header.Cell().Element(c => c.Background(Colors.Grey.Lighten3).Padding(4).ShowOnce()).Text("Venta (€)").Bold();
                                    header.Cell().Element(c => c.Background(Colors.Grey.Lighten3).Padding(4).ShowOnce()).Text("Costo (€)").Bold();
                                    header.Cell().Element(c => c.Background(Colors.Grey.Lighten3).Padding(4).ShowOnce()).Text("Cantidad").Bold();
                                    header.Cell().Element(c => c.Background(Colors.Grey.Lighten3).Padding(4).ShowOnce()).Text("Stock Min.").Bold();
                                    header.Cell().Element(c => c.Background(Colors.Grey.Lighten3).Padding(4).ShowOnce()).Text("Categoría").Bold();
                                    header.Cell().Element(c => c.Background(Colors.Grey.Lighten3).Padding(4).ShowOnce()).Text("Proveedor").Bold();
                                });

                                foreach (var p in grupo)
                                {
                                    tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text(p.Codigo);
                                    tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text(p.Descripcion);
                                    tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text($"{p.PrecioVenta:N2}");
                                    tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text($"{p.PrecioCosto:N2}");
                                    tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text($"{p.Cantidad}");
                                    tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text($"{p.StockMinimo}");
                                    tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text(p.Categoria);
                                    tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text(p.Proveedor);
                                }
                            });

                            col.Item().PaddingBottom(10);
                        }
                    });

                    // Footer
                    page.Footer().AlignCenter()
                        .Text("Sistema Monitux-POS · Reporte agrupado por marca")
                        .FontSize(10).Italic();
                });
            })
            .GeneratePdf($"{ruta}Reporte_ProductosPorMarca.pdf");



            V_Menu_Principal.MSG.ShowMSG("🖨️ Reporte generado correctamente", "Monitux-POS");
            V_Visor_Factura _Visor_Factura = new V_Visor_Factura();
            _Visor_Factura.rutaArchivo = ($"{ruta}Reporte_ProductosPorMarca.pdf");
            _Visor_Factura.ShowDialog();





            /////////////////
        }

        private void button1_Click(object sender, EventArgs e)
        {


            ////////////////////////////////
            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            // 🔐 Definir la empresa activa
            int secuencialEmpresaActiva = V_Menu_Principal.Secuencial_Empresa; // ← cámbialo por tu valor dinámico actual

            // 🔍 Obtener proveedor seleccionado desde combo
            var secuencialProveedor = comboProveedor.SelectedItem.ToString().Split('-')[0].Trim();

            if (string.IsNullOrEmpty(secuencialProveedor))
            {
                MessageBox.Show("Seleccione un proveedor para generar el reporte.", "Monitux-POS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 🗂️ Consulta filtrando por proveedor y empresa
            var productos = context.Productos
                .Where(p => p.Secuencial_Empresa == secuencialEmpresaActiva &&
                            p.Secuencial_Proveedor.ToString().Trim() == secuencialProveedor) // ✅ doble filtro aplicado
                .Join(context.Proveedores,
                    producto => producto.Secuencial_Proveedor,
                    proveedor => proveedor.Secuencial,
                    (producto, proveedor) => new { producto, proveedor })
                .Join(context.Categorias,
                    x => x.producto.Secuencial_Categoria,
                    categoria => categoria.Secuencial,
                    (x, categoria) => new
                    {
                        Codigo = x.producto.Codigo,
                        Descripcion = x.producto.Descripcion,
                        Marca = x.producto.Marca,
                        PrecioVenta = x.producto.Precio_Venta,
                        PrecioCosto = x.producto.Precio_Costo,
                        Cantidad = x.producto.Cantidad,
                        StockMinimo = x.producto.Existencia_Minima,
                        Categoria = categoria.Nombre,
                        Proveedor = x.proveedor.Nombre
                    })
                .OrderBy(p => p.Codigo)
                .ToList();

            if (!productos.Any())
            {
                MessageBox.Show($"No hay productos registrados para el proveedor: {secuencialProveedor}", "Monitux-POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 🖨️ Generar PDF
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(9));
                    page.PageColor(Colors.White);

                    // Encabezado
                    page.Header().Column(header =>
                    {
                        header.Item().Text("📦 Reporte de Productos por Proveedor").FontSize(20).Bold();
                        header.Item().Text($"Proveedor: {comboProveedor.SelectedItem}");
                        header.Item().Text($"Total productos: {productos.Count}");
                        header.Item().Text($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}");
                    });

                    // Tabla
                    page.Content().Table(tabla =>
                    {
                        tabla.ColumnsDefinition(cols =>
                        {
                            cols.RelativeColumn(1); // Código
                            cols.RelativeColumn(2); // Descripción
                            cols.RelativeColumn(1); // Marca
                            cols.RelativeColumn(1); // Precio Venta
                            cols.RelativeColumn(1); // Precio Costo
                            cols.RelativeColumn(1); // Cantidad
                            cols.RelativeColumn(1); // Stock Minimo
                            cols.RelativeColumn(1); // Categoría
                        });

                        // Encabezado
                        tabla.Header(header =>
                        {
                            header.Cell().Element(c => c.Background(Colors.Grey.Lighten3).Padding(4).ShowOnce()).Text("Código").Bold();
                            header.Cell().Element(c => c.Background(Colors.Grey.Lighten3).Padding(4).ShowOnce()).Text("Descripción").Bold();
                            header.Cell().Element(c => c.Background(Colors.Grey.Lighten3).Padding(4).ShowOnce()).Text("Marca").Bold();
                            header.Cell().Element(c => c.Background(Colors.Grey.Lighten3).Padding(4).ShowOnce()).Text("Venta (€)").Bold();
                            header.Cell().Element(c => c.Background(Colors.Grey.Lighten3).Padding(4).ShowOnce()).Text("Costo (€)").Bold();
                            header.Cell().Element(c => c.Background(Colors.Grey.Lighten3).Padding(4).ShowOnce()).Text("Cantidad").Bold();
                            header.Cell().Element(c => c.Background(Colors.Grey.Lighten3).Padding(4).ShowOnce()).Text("Stock Min.").Bold();
                            header.Cell().Element(c => c.Background(Colors.Grey.Lighten3).Padding(4).ShowOnce()).Text("Categoría").Bold();
                        });

                        // Filas
                        foreach (var p in productos)
                        {
                            tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text(p.Codigo);
                            tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text(p.Descripcion);
                            tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text(p.Marca);
                            tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text($"{p.PrecioVenta:N2}");
                            tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text($"{p.PrecioCosto:N2}");
                            tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text($"{p.Cantidad}");
                            tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text($"{p.StockMinimo}");
                            tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text(p.Categoria);
                        }
                    });

                    // Footer
                    page.Footer().AlignCenter()
                        .Text("Sistema Monitux-POS · Reporte filtrado por proveedor")
                        .FontSize(10).Italic();
                });
            })
            .GeneratePdf($"{ruta}Reporte_ProductosProveedor_{comboProveedor.SelectedItem}.pdf");


            V_Menu_Principal.MSG.ShowMSG("🖨️ Reporte generado correctamente", "Monitux-POS");
            V_Visor_Factura _Visor_Factura = new V_Visor_Factura();
            _Visor_Factura.rutaArchivo = ($"{ruta}Reporte_ProductosProveedor_{comboProveedor.SelectedItem}.pdf");
            _Visor_Factura.ShowDialog();









            ///////////////////////////////



        }



        public void llenar_Combo_Proveedor()
        {


            comboProveedor.Items.Clear();

            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            // Filtrar solo clientes activos
            var proveedoresActivos = context.Proveedores.Where(c => (bool)c.Activo && c.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa).ToList();

            foreach (var item in proveedoresActivos)
            {
                comboProveedor.Items.Add(item.Secuencial + " - " + item.Nombre);
            }



        }


        private void V_Reportes_Inventario_Load(object sender, EventArgs e)
        {
            llenar_Combo_Proveedor();
            llenar_Combo_Categoria();

           // comboCategoria.SelectedIndex = 0;
           // comboProveedor.SelectedIndex = 0;

        }



        public void llenar_Combo_Categoria()
        {

            comboCategoria.Items.Clear();

            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe


            var categorias = context.Categorias
    .Where(c => c.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa)
    .ToList();



            foreach (var item in categorias)
            {
                comboCategoria.Items.Add(item.Secuencial + " - " + item.Nombre);



            }


        }



        private void button2_Click(object sender, EventArgs e)
        {



            /////////////
            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            // 🔐 Empresa activa
            int secuencialEmpresaActiva = V_Menu_Principal.Secuencial_Empresa; // ← remplaza con tu valor dinámico actual

            // 📂 Categoría seleccionada desde combo
            var categoriaSeleccionada = comboCategoria.SelectedItem.ToString().Split('-')[0].Trim();

            if (string.IsNullOrEmpty(categoriaSeleccionada))
            {
                MessageBox.Show("Seleccione una categoría válida para generar el reporte.", "Monitux-POS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 🗂️ Consulta con doble filtro
            var productos = context.Productos
                .Where(p => p.Secuencial_Empresa == secuencialEmpresaActiva &&
                            p.Secuencial_Categoria.ToString() == categoriaSeleccionada)
                .Join(context.Categorias,
                    p => p.Secuencial_Categoria,
                    c => c.Secuencial,
                    (p, c) => new { p, c })
                .Join(context.Proveedores,
                    x => x.p.Secuencial_Proveedor,
                    proveedor => proveedor.Secuencial,
                    (x, proveedor) => new
                    {
                        Codigo = x.p.Codigo,
                        Descripcion = x.p.Descripcion,
                        Marca = x.p.Marca,
                        PrecioVenta = x.p.Precio_Venta,
                        PrecioCosto = x.p.Precio_Costo,
                        Cantidad = x.p.Cantidad,
                        StockMinimo = x.p.Existencia_Minima,
                        Categoria = x.c.Nombre,
                        Proveedor = proveedor.Nombre
                    })
                .OrderBy(p => p.Codigo)
                .ToList();


            if (!productos.Any())
            {
                MessageBox.Show($"No hay productos registrados para la categoría: {comboCategoria.SelectedItem}", "Monitux-POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 🖨️ Generar PDF
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(9));
                    page.PageColor(Colors.White);

                    // Encabezado
                    page.Header().Column(header =>
                    {
                        header.Item().Text("📦 Reporte de Productos por Categoría").FontSize(20).Bold();
                        header.Item().Text($"Categoría: {comboCategoria.SelectedItem}");
                        header.Item().Text($"Total productos: {productos.Count}");
                        header.Item().Text($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}");
                    });

                    // Tabla
                    page.Content().Table(tabla =>
                    {
                        tabla.ColumnsDefinition(cols =>
                        {
                            cols.RelativeColumn(1); // Código
                            cols.RelativeColumn(2); // Descripción
                            cols.RelativeColumn(1); // Marca
                            cols.RelativeColumn(1); // Precio Venta
                            cols.RelativeColumn(1); // Precio Costo
                            cols.RelativeColumn(1); // Cantidad
                            cols.RelativeColumn(1); // Stock Mínimo
                            cols.RelativeColumn(1); // Proveedor
                        });

                        // Encabezado
                        tabla.Header(header =>
                        {
                            string[] titulos = { "Código", "Descripción", "Marca", "Venta (€)", "Costo (€)", "Cantidad", "Stock Min.", "Proveedor" };
                            foreach (var titulo in titulos)
                            {
                                header.Cell().Element(c => c.Background(Colors.Grey.Lighten3).Padding(4).ShowOnce()).Text(titulo).Bold();
                            }
                        });

                        // Filas
                        foreach (var p in productos)
                        {
                            tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text(p.Codigo);
                            tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text(p.Descripcion);
                            tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text(p.Marca);
                            tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text($"{p.PrecioVenta:N2}");
                            tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text($"{p.PrecioCosto:N2}");
                            tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text($"{p.Cantidad}");
                            tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text($"{p.StockMinimo}");
                            tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text(p.Proveedor);
                        }
                    });

                    // Footer
                    page.Footer().AlignCenter()
                        .Text("Sistema Monitux-POS · Reporte filtrado por categoría")
                        .FontSize(10).Italic();
                });
            })
            .GeneratePdf($"{ruta}Reporte_ProductosCategoria_{categoriaSeleccionada}.pdf");



            V_Menu_Principal.MSG.ShowMSG("🖨️ Reporte generado correctamente", "Monitux-POS");
            V_Visor_Factura _Visor_Factura = new V_Visor_Factura();
            _Visor_Factura.rutaArchivo = ($"{ruta}Reporte_ProductosCategoria_{categoriaSeleccionada}.pdf");
            _Visor_Factura.ShowDialog();







            /////////////


        }

        private void button3_Click(object sender, EventArgs e)
        {



        }

        private void button4_Click(object sender, EventArgs e)
        {


            ///////////////////////
            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            // 🔐 Definir la empresa activa
            int secuencialEmpresaActiva = V_Menu_Principal.Secuencial_Empresa; // ← cambia este valor por el dinámico actual

            // 📦 Obtener productos que caducan para esta empresa
            var productosAgrupados = context.Productos
                .Where(p => p.Expira && p.Fecha_Caducidad != null && p.Secuencial_Empresa == secuencialEmpresaActiva)
                .Join(context.Proveedores,
                    p => p.Secuencial_Proveedor,
                    prov => prov.Secuencial,
                    (p, prov) => new { p, prov })
                .Join(context.Categorias,
                    x => x.p.Secuencial_Categoria,
                    cat => cat.Secuencial,
                    (x, cat) => new
                    {
                        Proveedor = x.prov.Nombre,
                        Codigo = x.p.Codigo,
                        Descripcion = x.p.Descripcion,
                        Marca = x.p.Marca,
                        PrecioVenta = x.p.Precio_Venta,
                        PrecioCosto = x.p.Precio_Costo,
                        Cantidad = x.p.Cantidad,
                        StockMinimo = x.p.Existencia_Minima,
                        FechaCaducidad = x.p.Fecha_Caducidad,
                        Categoria = cat.Nombre
                    })
                .OrderBy(x => x.Proveedor)
                .ThenBy(x => x.FechaCaducidad)
                .ToList()
                .GroupBy(x => x.Proveedor);

            // 🖨️ Generar PDF
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(9));
                    page.PageColor(Colors.White);

                    // Encabezado general
                    page.Header().Column(header =>
                    {
                        header.Item().Text("🧊 Reporte de Productos que Caducan").FontSize(20).Bold();
                        header.Item().Text($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}");
                    });

                    // Contenido agrupado por proveedor
                    page.Content().Column(col =>
                    {
                        foreach (var grupo in productosAgrupados)
                        {
                            col.Item().Text($"🔹 Proveedor: {grupo.Key}").FontSize(11).Bold().FontColor(Colors.Blue.Darken2);

                            col.Item().Table(tabla =>
                            {
                                tabla.ColumnsDefinition(cols =>
                                {
                                    cols.RelativeColumn(1); // Código
                                    cols.RelativeColumn(2); // Descripción
                                    cols.RelativeColumn(1); // Marca
                                    cols.RelativeColumn(1); // Venta
                                    cols.RelativeColumn(1); // Costo
                                    cols.RelativeColumn(1); // Cantidad
                                    cols.RelativeColumn(1); // Stock Min.
                                    cols.RelativeColumn(1); // Caducidad
                                    cols.RelativeColumn(1); // Categoría
                                });

                                // Encabezado
                                tabla.Header(header =>
                                {
                                    string[] titulos = { "Código", "Descripción", "Marca", "Venta (€)", "Costo (€)", "Cantidad", "Stock Min.", "Caduca el", "Categoría" };
                                    foreach (var titulo in titulos)
                                    {
                                        header.Cell().Element(c => c.Background(Colors.Grey.Lighten3).Padding(4).ShowOnce()).Text(titulo).Bold();
                                    }
                                });

                                // Filas
                                foreach (var p in grupo)
                                {
                                    tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text(p.Codigo);
                                    tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text(p.Descripcion);
                                    tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text(p.Marca);
                                    tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text($"{p.PrecioVenta:N2}");
                                    tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text($"{p.PrecioCosto:N2}");
                                    tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text($"{p.Cantidad}");
                                    tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text($"{p.StockMinimo}");
                                    tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text($"{p.FechaCaducidad:dd/MM/yyyy}");
                                    tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text(p.Categoria);
                                }
                            });

                            col.Item().PaddingBottom(10);
                        }
                    });

                    // Pie
                    page.Footer().AlignCenter()
                        .Text("Sistema Monitux-POS · Reporte de productos caducables agrupado por proveedor")
                        .FontSize(10).Italic();
                });
            })
            .GeneratePdf($"{ruta}Reporte_Productos_Caducan.pdf");


            V_Menu_Principal.MSG.ShowMSG("🖨️ Reporte generado correctamente", "Monitux-POS");
            V_Visor_Factura _Visor_Factura = new V_Visor_Factura();
            _Visor_Factura.rutaArchivo = ($"{ruta}Reporte_Productos_Caducan.pdf");
            _Visor_Factura.ShowDialog();








            //////////////////////





        }

        private void button9_Click(object sender, EventArgs e)
        {

            ///////

            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            // 🔐 Empresa activa
            int secuencialEmpresaActiva = V_Menu_Principal.Secuencial_Empresa; // ← sustituye por valor dinámico

            // 🗂️ Consulta: productos de la empresa, agrupados por proveedor
            var preciosPorProveedor = context.Productos
                .Where(p => p.Secuencial_Empresa == secuencialEmpresaActiva)
                .Join(context.Proveedores,
                    p => p.Secuencial_Proveedor,
                    prov => prov.Secuencial,
                    (p, prov) => new
                    {
                        Proveedor = prov.Nombre,
                        Codigo = p.Codigo,
                        Descripcion = p.Descripcion,
                        Marca = p.Marca,
                        PrecioVenta = p.Precio_Venta
                    })
                .OrderBy(x => x.Proveedor)
                .ThenBy(x => x.Descripcion)
                .ToList()
                .GroupBy(x => x.Proveedor);




            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(9));
                    page.PageColor(Colors.White);

                    // Encabezado
                    page.Header().Column(header =>
                    {
                        header.Item().Text("💸 Listado de Precios de Venta").FontSize(20).Bold();
                        header.Item().Text($"Empresa: {secuencialEmpresaActiva}");
                        header.Item().Text($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}");
                    });

                    // Agrupamiento por proveedor
                    page.Content().Column(col =>
                    {
                        foreach (var grupo in preciosPorProveedor)
                        {
                            col.Item().Text($"🔹 Proveedor: {grupo.Key}").FontSize(11).Bold().FontColor(Colors.Blue.Darken2);

                            col.Item().Table(tabla =>
                            {
                                tabla.ColumnsDefinition(cols =>
                                {
                                    cols.RelativeColumn(1); // Código
                                    cols.RelativeColumn(2); // Descripción
                                    cols.RelativeColumn(1); // Marca
                                    cols.RelativeColumn(1); // Precio Venta
                                });

                                tabla.Header(header =>
                                {
                                    string[] titulos = { "Código", "Descripción", "Marca", "Venta (€)" };
                                    foreach (var titulo in titulos)
                                        header.Cell().Element(c => c.Background(Colors.Grey.Lighten3).Padding(4).ShowOnce()).Text(titulo).Bold();
                                });

                                foreach (var p in grupo)
                                {
                                    tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text(p.Codigo);
                                    tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text(p.Descripcion);
                                    tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text(p.Marca);
                                    tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text($"{p.PrecioVenta:N2}");
                                }
                            });

                            col.Item().PaddingBottom(10);
                        }
                    });

                    // Footer
                    page.Footer().AlignCenter()
                        .Text("Sistema Monitux-POS · Listado de precios de venta")
                        .FontSize(10).Italic();
                });
            })
.GeneratePdf($"{ruta}Listado_PreciosPorProveedor.pdf");




            V_Menu_Principal.MSG.ShowMSG("🖨️ Reporte generado correctamente", "Monitux-POS");
            V_Visor_Factura _Visor_Factura = new V_Visor_Factura();
            _Visor_Factura.rutaArchivo = ($"{ruta}Listado_PreciosPorProveedor.pdf");
            _Visor_Factura.ShowDialog();








            //////




        }

        private void button10_Click(object sender, EventArgs e)
        {

            ////////////////

            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            // 🔐 Empresa activa
            int secuencialEmpresaActiva = V_Menu_Principal.Secuencial_Empresa; // ← reemplaza por tu valor dinámico

            // 🗂️ Consulta: productos agrupados por proveedor con precio de costo
            var costosPorProveedor = context.Productos
                .Where(p => p.Secuencial_Empresa == secuencialEmpresaActiva)
                .Join(context.Proveedores,
                    p => p.Secuencial_Proveedor,
                    prov => prov.Secuencial,
                    (p, prov) => new
                    {
                        Proveedor = prov.Nombre,
                        Codigo = p.Codigo,
                        Descripcion = p.Descripcion,
                        Marca = p.Marca,
                        PrecioCosto = p.Precio_Costo
                    })
                .OrderBy(x => x.Proveedor)
                .ThenBy(x => x.Descripcion)
                .ToList()
                .GroupBy(x => x.Proveedor);


            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(9));
                    page.PageColor(Colors.White);

                    // Encabezado
                    page.Header().Column(header =>
                    {
                        header.Item().Text("💰 Listado de Precios de Costo").FontSize(20).Bold();
                        header.Item().Text($"Empresa: {secuencialEmpresaActiva}");
                        header.Item().Text($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}");
                    });

                    // Contenido agrupado por proveedor
                    page.Content().Column(col =>
                    {
                        foreach (var grupo in costosPorProveedor)
                        {
                            col.Item().Text($"🔹 Proveedor: {grupo.Key}").FontSize(11).Bold().FontColor(Colors.Blue.Darken2);

                            col.Item().Table(tabla =>
                            {
                                tabla.ColumnsDefinition(cols =>
                                {
                                    cols.RelativeColumn(1); // Código
                                    cols.RelativeColumn(2); // Descripción
                                    cols.RelativeColumn(1); // Marca
                                    cols.RelativeColumn(1); // Precio Costo
                                });

                                tabla.Header(header =>
                                {
                                    string[] titulos = { "Código", "Descripción", "Marca", "Costo (€)" };
                                    foreach (var titulo in titulos)
                                        header.Cell().Element(c => c.Background(Colors.Grey.Lighten3).Padding(4).ShowOnce()).Text(titulo).Bold();
                                });

                                foreach (var p in grupo)
                                {
                                    tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text(p.Codigo);
                                    tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text(p.Descripcion);
                                    tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text(p.Marca);
                                    tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text($"{p.PrecioCosto:N2}");
                                }
                            });

                            col.Item().PaddingBottom(10);
                        }
                    });

                    // Pie
                    page.Footer().AlignCenter()
                        .Text("Sistema Monitux-POS · Listado de precios de costo por proveedor")
                        .FontSize(10).Italic();
                });
            })
.GeneratePdf($"{ruta}Listado_CostosPorProveedor.pdf");





            V_Menu_Principal.MSG.ShowMSG("🖨️ Reporte generado correctamente", "Monitux-POS");
            V_Visor_Factura _Visor_Factura = new V_Visor_Factura();
            _Visor_Factura.rutaArchivo = ($"{ruta}Listado_CostosPorProveedor.pdf");
            _Visor_Factura.ShowDialog();







            ///////////////


        }

        private void button6_Click(object sender, EventArgs e)
        {


            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            // 🔐 Empresa activa
            int secuencialEmpresaActiva = V_Menu_Principal.Secuencial_Empresa;

            // 📋 Filtrar productos que son servicios (por ejemplo, Tipo = "Servicio")
            var productos = context.Productos
                .Where(p => p.Secuencial_Empresa == secuencialEmpresaActiva &&
                            p.Tipo.Trim() == "Servicio")
                .OrderBy(p => p.Codigo)
                .Select(p => new
                {
                    Codigo = p.Codigo,
                    Descripcion = p.Descripcion,
                    PrecioVenta = p.Precio_Venta,
                    PrecioCosto = p.Precio_Costo
                })
                .ToList();

            // 🖨️ Generar PDF
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(9));

                    // Encabezado
                    page.Header().Column(header =>
                    {
                        header.Item().Text("🛠️ Reporte de Servicios Registrados").FontSize(20).Bold();
                        header.Item().Text($"Total servicios: {productos.Count}");
                        header.Item().Text($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}");
                    });

                    // Tabla
                    page.Content().Table(tabla =>
                    {
                        tabla.ColumnsDefinition(cols =>
                        {
                            cols.RelativeColumn(1); // Código
                            cols.RelativeColumn(3); // Descripción
                            cols.RelativeColumn(1); // Precio Venta
                            cols.RelativeColumn(1); // Precio Costo
                        });

                        // Encabezado
                        string[] titulos = { "Código", "Descripción", "Venta (€)", "Costo (€)" };
                        tabla.Header(header =>
                        {
                            foreach (var titulo in titulos)
                                header.Cell().Element(c => c.Background(Colors.Grey.Lighten3).PaddingVertical(4).PaddingHorizontal(2).ShowOnce()).Text(titulo).Bold();
                        });

                        // Filas
                        foreach (var p in productos)
                        {
                            tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).PaddingVertical(2).PaddingHorizontal(2)).Text(p.Codigo);
                            tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).PaddingVertical(2).PaddingHorizontal(2)).Text(p.Descripcion);
                            tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).PaddingVertical(2).PaddingHorizontal(2)).Text($"{p.PrecioVenta:N2}");
                            tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).PaddingVertical(2).PaddingHorizontal(2)).Text($"{p.PrecioCosto:N2}");
                        }
                    });

                    // Footer
                    page.Footer().AlignCenter()
                        .Text("Sistema Monitux-POS · Reporte exclusivo de servicios")
                        .FontSize(10).Italic();
                });
            })
            .GeneratePdf($"{ruta}Reporte_ServiciosRegistrados.pdf");




            V_Menu_Principal.MSG.ShowMSG("🖨️ Reporte generado correctamente", "Monitux-POS");
            V_Visor_Factura _Visor_Factura = new V_Visor_Factura();
            _Visor_Factura.rutaArchivo = ($"{ruta}Reporte_ServiciosRegistrados.pdf");
            _Visor_Factura.ShowDialog();





        }

        private void button5_Click(object sender, EventArgs e)
        {



            ////////////////////////

            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            int secuencialEmpresaActiva = V_Menu_Principal.Secuencial_Empresa;

            // Rango de fechas en string (deberías obtenerlos desde inputs del usuario)
            string fechaInicioStr = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            string fechaFinStr = dateTimePicker2.Value.ToString("dd/MM/yyyy");

            // Productos vendidos en el rango
            var productosVendidos = context.Ventas_Detalles
                .Where(vd => vd.Secuencial_Empresa == secuencialEmpresaActiva
                    && string.Compare(vd.Fecha, fechaInicioStr) >= 0
                    && string.Compare(vd.Fecha, fechaFinStr) <= 0)
                .GroupBy(vd => new { vd.Codigo, vd.Descripcion })
                .Select(g => new
                {
                    Codigo = g.Key.Codigo,
                    Descripcion = g.Key.Descripcion,
                    CantidadVendida = g.Sum(x => x.Cantidad),
                    TotalVentas = g.Sum(x => x.Total)
                })
                .OrderByDescending(x => x.CantidadVendida)
                .ToList();

            // 🖨️ Crear PDF
            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(9));

                    page.Header().Column(header =>
                    {
                        header.Item().Text("📊 Reporte de productos más vendidos").FontSize(20).Bold();
                        header.Item().Text($"Rango: {fechaInicioStr} a {fechaFinStr}");
                        header.Item().Text($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}");
                    });

                    page.Content().Table(tabla =>
                    {
                        tabla.ColumnsDefinition(cols =>
                        {
                            cols.RelativeColumn(1); // Código
                            cols.RelativeColumn(3); // Descripción
                            cols.RelativeColumn(1); // Cantidad vendida
                            cols.RelativeColumn(1); // Total ventas
                        });

                        tabla.Header(header =>
                        {
                            header.Cell().Element(c =>
                                c.Background(Colors.Grey.Lighten3)
                                 .PaddingVertical(4)
                                 .PaddingHorizontal(2)
                                 .ShowOnce()).Text("Código").Bold();

                            header.Cell().Element(c =>
                                c.Background(Colors.Grey.Lighten3)
                                 .PaddingVertical(4)
                                 .PaddingHorizontal(2)
                                 .ShowOnce()).Text("Descripción").Bold();

                            header.Cell().Element(c =>
                                c.Background(Colors.Grey.Lighten3)
                                 .PaddingVertical(4)
                                 .PaddingHorizontal(2)
                                 .ShowOnce()).Text("Cantidad").Bold();

                            header.Cell().Element(c =>
                                c.Background(Colors.Grey.Lighten3)
                                 .PaddingVertical(4)
                                 .PaddingHorizontal(2)
                                 .ShowOnce()).Text($"Total ({V_Menu_Principal.moneda})").Bold();
                        });

                        foreach (var p in productosVendidos)
                        {
                            tabla.Cell().Element(c =>
                                c.BorderBottom(0.5f)
                                 .BorderColor(Colors.Grey.Lighten2)
                                 .PaddingVertical(2)
                                 .PaddingHorizontal(2)).Text(p.Codigo);

                            tabla.Cell().Element(c =>
                                c.BorderBottom(0.5f)
                                 .BorderColor(Colors.Grey.Lighten2)
                                 .PaddingVertical(2)
                                 .PaddingHorizontal(2)).Text(p.Descripcion);

                            tabla.Cell().Element(c =>
                                c.BorderBottom(0.5f)
                                 .BorderColor(Colors.Grey.Lighten2)
                                 .PaddingVertical(2)
                                 .PaddingHorizontal(2)).Text($"{p.CantidadVendida:N0}");

                            tabla.Cell().Element(c =>
                                c.BorderBottom(0.5f)
                                 .BorderColor(Colors.Grey.Lighten2)
                                 .PaddingVertical(2)
                                 .PaddingHorizontal(2)).Text($"{p.TotalVentas:N2}");
                        }
                    });

                    page.Footer().AlignCenter()
                        .Text("Sistema Monitux-POS · Reporte generado automáticamente")
                        .FontSize(10)
                        .Italic();
                });
            })
            .GeneratePdf($"{ruta}Reporte_Productos_Mas_Vendidos.pdf");

            V_Menu_Principal.MSG.ShowMSG("🖨️ Reporte generado correctamente", "Monitux-POS");

            var visor = new V_Visor_Factura
            {
                rutaArchivo = $"{ruta}Reporte_Productos_Mas_Vendidos.pdf"
            };

            visor.ShowDialog();

            ///////////////////////




        }

        private void button11_Click(object sender, EventArgs e)
        {

            /////////////////////////////////
            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            int secuencialEmpresaActiva = V_Menu_Principal.Secuencial_Empresa;

            string fechaInicioStr = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            string fechaFinStr = dateTimePicker2.Value.ToString("dd/MM/yyyy");

            DateTime fechaInicio = DateTime.ParseExact(fechaInicioStr, "dd/MM/yyyy", null);
            DateTime fechaFin = DateTime.ParseExact(fechaFinStr, "dd/MM/yyyy", null);

            var movimientosAgrupados = context.Kardex
                .Where(k => k.Secuencial_Empresa == secuencialEmpresaActiva)
                .ToList()
                .Where(k =>
                    DateTime.ParseExact(k.Fecha, "dd/MM/yyyy", null) >= fechaInicio &&
                    DateTime.ParseExact(k.Fecha, "dd/MM/yyyy", null) <= fechaFin)
                .GroupBy(k => new { k.Secuencial_Producto, k.Descripcion })
                .OrderBy(g => g.Key.Secuencial_Producto)
                .Select(g => new
                {
                    Producto = g.Key,
                    Movimientos = g.OrderBy(k => DateTime.ParseExact(k.Fecha, "dd/MM/yyyy", null)).ToList()
                })
                .ToList();

            string nombreReporte = "Reporte_Kardex_Agrupado.pdf";
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
                        header.Item().Text("📦 Reporte Kardex agrupado por producto").FontSize(20).Bold();
                        header.Item().Text($"Rango: {fechaInicioStr} a {fechaFinStr}");
                        header.Item().Text($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}");
                    });

                    page.Content().Table(tabla =>
                    {
                        tabla.ColumnsDefinition(columns =>
                        {
                            for (int i = 0; i < 8; i++)
                                columns.RelativeColumn(1);
                        });

                        tabla.Header(header =>
                        {
                            string[] titulos = {
                    "Fecha", "Código", "Movimiento", "Cantidad",
                    $"Costo ({V_Menu_Principal.moneda})",
                    $"Costo total ({V_Menu_Principal.moneda})",
                    $"Venta total ({V_Menu_Principal.moneda})",
                    "Saldo"
                };

                            foreach (var titulo in titulos)
                            {
                                header.Cell().Element(c =>
                                    c.Background(Colors.Grey.Lighten3)
                                     .PaddingVertical(4)
                                     .PaddingHorizontal(2)
                                     .ShowOnce())
                                    .Text(titulo).Bold();
                            }
                        });

                        foreach (var grupo in movimientosAgrupados)
                        {
                            tabla.Cell().ColumnSpan(8).Element(c => c.Background(Colors.Grey.Lighten2)
                                .PaddingVertical(6).PaddingHorizontal(4))
                                .Text($"Producto: {grupo.Producto.Descripcion} (Código: {grupo.Producto.Secuencial_Producto})").Bold();

                            foreach (var m in grupo.Movimientos)
                            {
                                tabla.Cell().Element(c => c.BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten2).Padding(2)).Text(m.Fecha);
                                tabla.Cell().Text($"{m.Secuencial_Producto}");
                                tabla.Cell().Text(m.Movimiento);
                                tabla.Cell().Text($"{m.Cantidad:N0}");
                                tabla.Cell().Text($"{m.Costo:N2}");
                                tabla.Cell().Text($"{m.Costo_Total:N2}");
                                tabla.Cell().Text($"{m.Venta_Total:N2}");
                                tabla.Cell().Text($"{m.Saldo:N0}");
                            }
                        }
                    });

                    page.Footer().AlignCenter()
                        .Text("Sistema Monitux-POS · Reporte generado automáticamente")
                        .FontSize(10).Italic();
                });
            })
            .GeneratePdf(rutaCompleta);

            V_Menu_Principal.MSG.ShowMSG("🖨️ Reporte generado correctamente", "Monitux-POS");

            var visor = new V_Visor_Factura
            {
                rutaArchivo = rutaCompleta
            };

            visor.ShowDialog();


            /////////////////////////////////



        }

        private void button12_Click(object sender, EventArgs e)
        {


            ///////////////////////////
            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            int secuencialEmpresaActiva = V_Menu_Principal.Secuencial_Empresa;

            // 👇 Código de producto a filtrar
            int codigoProductoFiltrado = Convert.ToInt32(textBox2.Text); // o desde ComboBox

            var entradasFiltradas = context.Kardex
                .Where(k => k.Secuencial_Empresa == secuencialEmpresaActiva
                    && k.Secuencial_Producto == codigoProductoFiltrado
                    && k.Movimiento == "Entrada")
                .ToList()
                .OrderBy(k => DateTime.ParseExact(k.Fecha, "dd/MM/yyyy", null))
                .ToList();

            string nombreReporte = $"Entradas_Producto_{codigoProductoFiltrado}.pdf";
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
                        header.Item().Text("📦 Reporte de entradas por producto").FontSize(20).Bold();
                        header.Item().Text($"Código: {codigoProductoFiltrado}");
                        header.Item().Text($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}");
                    });

                    page.Content().Table(tabla =>
                    {
                        tabla.ColumnsDefinition(cols =>
                        {
                            cols.RelativeColumn(1); // Fecha
                            cols.RelativeColumn(3); // Descripción
                            cols.RelativeColumn(1); // Cantidad
                            cols.RelativeColumn(1); // Costo unitario
                            cols.RelativeColumn(1); // Costo total
                            cols.RelativeColumn(1); // Saldo
                        });

                        string[] titulos = {
                "Fecha", "Descripción", "Cantidad",
                $"Costo ({V_Menu_Principal.moneda})",
                $"Costo total ({V_Menu_Principal.moneda})",
                "Saldo"
            };

                        tabla.Header(header =>
                        {
                            foreach (var titulo in titulos)
                            {
                                header.Cell().Element(c =>
                                    c.Background(Colors.Grey.Lighten3)
                                     .PaddingVertical(4)
                                     .PaddingHorizontal(2)
                                     .ShowOnce())
                                    .Text(titulo).Bold();
                            }
                        });

                        foreach (var entrada in entradasFiltradas)
                        {
                            tabla.Cell().Element(c => c.BorderBottom(0.5f)
                                .BorderColor(Colors.Grey.Lighten2)
                                .Padding(2)).Text(entrada.Fecha);

                            tabla.Cell().Text(entrada.Descripcion);
                            tabla.Cell().Text($"{entrada.Cantidad:N0}");
                            tabla.Cell().Text($"{entrada.Costo:N2}");
                            tabla.Cell().Text($"{entrada.Costo_Total:N2}");
                            tabla.Cell().Text($"{entrada.Saldo:N0}");
                        }
                    });

                    page.Footer().AlignCenter()
                        .Text("Sistema Monitux-POS · Reporte generado automáticamente")
                        .FontSize(10).Italic();
                });
            })
            .GeneratePdf(rutaCompleta);

            V_Menu_Principal.MSG.ShowMSG("🖨️ Reporte generado correctamente", "Monitux-POS");

            var visor = new V_Visor_Factura
            {
                rutaArchivo = rutaCompleta
            };

            visor.ShowDialog();




            ///////////////////////////


        }

        private void button13_Click(object sender, EventArgs e)
        {
            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            int secuencialEmpresaActiva = V_Menu_Principal.Secuencial_Empresa;

            // 👇 Código del producto a filtrar
            int codigoProductoFiltrado = Convert.ToInt32(textBox3.Text); // puede venir de un ComboBox

            var salidasFiltradas = context.Kardex
                .Where(k => k.Secuencial_Empresa == secuencialEmpresaActiva
                    && k.Secuencial_Producto == codigoProductoFiltrado
                    && k.Movimiento == "Salida")
                .ToList()
                .OrderBy(k => DateTime.ParseExact(k.Fecha, "dd/MM/yyyy", null))
                .ToList();

            string nombreReporte = $"Salidas_Producto_{codigoProductoFiltrado}.pdf";
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
                        header.Item().Text("📦 Reporte de salidas por producto").FontSize(20).Bold();
                        header.Item().Text($"Código: {codigoProductoFiltrado}");
                        header.Item().Text($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}");
                    });

                    page.Content().Table(tabla =>
                    {
                        tabla.ColumnsDefinition(cols =>
                        {
                            cols.RelativeColumn(1); // Fecha
                            cols.RelativeColumn(3); // Descripción
                            cols.RelativeColumn(1); // Cantidad
                            cols.RelativeColumn(1); // Venta unitaria
                            cols.RelativeColumn(1); // Venta total
                            cols.RelativeColumn(1); // Saldo
                        });

                        string[] titulos = {
                "Fecha", "Descripción", "Cantidad",
                $"Venta ({V_Menu_Principal.moneda})",
                $"Venta total ({V_Menu_Principal.moneda})",
                "Saldo"
            };

                        tabla.Header(header =>
                        {
                            foreach (var titulo in titulos)
                            {
                                header.Cell().Element(c =>
                                    c.Background(Colors.Grey.Lighten3)
                                     .PaddingVertical(4)
                                     .PaddingHorizontal(2)
                                     .ShowOnce())
                                    .Text(titulo).Bold();
                            }
                        });

                        foreach (var salida in salidasFiltradas)
                        {
                            tabla.Cell().Element(c => c.BorderBottom(0.5f)
                                .BorderColor(Colors.Grey.Lighten2)
                                .Padding(2)).Text(salida.Fecha);

                            tabla.Cell().Text(salida.Descripcion);
                            tabla.Cell().Text($"{salida.Cantidad:N0}");
                            tabla.Cell().Text($"{salida.Venta:N2}");
                            tabla.Cell().Text($"{salida.Venta_Total:N2}");
                            tabla.Cell().Text($"{salida.Saldo:N0}");
                        }
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

        private void button15_Click(object sender, EventArgs e)
        {



            /////////////////////////////////////
            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            int secuencialEmpresaActiva = V_Menu_Principal.Secuencial_Empresa;

            // Fechas seleccionadas por el usuario
            string fechaInicioStr = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            string fechaFinStr = dateTimePicker2.Value.ToString("dd/MM/yyyy");

            DateTime fechaInicio = DateTime.ParseExact(fechaInicioStr, "dd/MM/yyyy", null);
            DateTime fechaFin = DateTime.ParseExact(fechaFinStr, "dd/MM/yyyy", null);

            // Entradas filtradas por rango
            var entradas = context.Kardex
                .Where(k => k.Secuencial_Empresa == secuencialEmpresaActiva
                    && k.Movimiento == "Entrada")
                .ToList()
                .Where(k =>
                    DateTime.ParseExact(k.Fecha, "dd/MM/yyyy", null) >= fechaInicio &&
                    DateTime.ParseExact(k.Fecha, "dd/MM/yyyy", null) <= fechaFin)
                .OrderBy(k => DateTime.ParseExact(k.Fecha, "dd/MM/yyyy", null))
                .ToList();

            string nombreReporte = $"Entradas_Rango_{fechaInicioStr.Replace("/", "-")}_a_{fechaFinStr.Replace("/", "-")}.pdf";
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
                        header.Item().Text("📦 Reporte de todas las entradas").FontSize(20).Bold();
                        header.Item().Text($"Rango: {fechaInicioStr} a {fechaFinStr}");
                        header.Item().Text($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}");
                    });

                    page.Content().Table(tabla =>
                    {
                        tabla.ColumnsDefinition(cols =>
                        {
                            cols.RelativeColumn(1); // Fecha
                            cols.RelativeColumn(1); // Código
                            cols.RelativeColumn(3); // Descripción
                            cols.RelativeColumn(1); // Cantidad
                            cols.RelativeColumn(1); // Costo
                            cols.RelativeColumn(1); // Costo total
                            cols.RelativeColumn(1); // Saldo
                        });

                        string[] titulos = {
                "Fecha", "Código", "Descripción", "Cantidad",
                $"Costo ({V_Menu_Principal.moneda})",
                $"Costo total ({V_Menu_Principal.moneda})",
                "Saldo"
            };

                        tabla.Header(header =>
                        {
                            foreach (var titulo in titulos)
                            {
                                header.Cell().Element(c =>
                                    c.Background(Colors.Grey.Lighten3)
                                     .PaddingVertical(4)
                                     .PaddingHorizontal(2)
                                     .ShowOnce())
                                    .Text(titulo).Bold();
                            }
                        });

                        foreach (var entrada in entradas)
                        {
                            tabla.Cell().Element(c => c.BorderBottom(0.5f)
                                .BorderColor(Colors.Grey.Lighten2)
                                .Padding(2)).Text(entrada.Fecha);

                            tabla.Cell().Text($"{entrada.Secuencial_Producto}");
                            tabla.Cell().Text(entrada.Descripcion);
                            tabla.Cell().Text($"{entrada.Cantidad:N0}");
                            tabla.Cell().Text($"{entrada.Costo:N2}");
                            tabla.Cell().Text($"{entrada.Costo_Total:N2}");
                            tabla.Cell().Text($"{entrada.Saldo:N0}");
                        }
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




            ////////////////////////////////////





        }

        private void button14_Click(object sender, EventArgs e)
        {
            /////////////////

            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            int secuencialEmpresaActiva = V_Menu_Principal.Secuencial_Empresa;

            // Fechas seleccionadas por el usuario
            string fechaInicioStr = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            string fechaFinStr = dateTimePicker2.Value.ToString("dd/MM/yyyy");

            DateTime fechaInicio = DateTime.ParseExact(fechaInicioStr, "dd/MM/yyyy", null);
            DateTime fechaFin = DateTime.ParseExact(fechaFinStr, "dd/MM/yyyy", null);

            // Salidas filtradas por rango
            var salidas = context.Kardex
                .Where(k => k.Secuencial_Empresa == secuencialEmpresaActiva
                    && k.Movimiento == "Salida")
                .ToList()
                .Where(k =>
                    DateTime.ParseExact(k.Fecha, "dd/MM/yyyy", null) >= fechaInicio &&
                    DateTime.ParseExact(k.Fecha, "dd/MM/yyyy", null) <= fechaFin)
                .OrderBy(k => DateTime.ParseExact(k.Fecha, "dd/MM/yyyy", null))
                .ToList();

            string nombreReporte = $"Salidas_Rango_{fechaInicioStr.Replace("/", "-")}_a_{fechaFinStr.Replace("/", "-")}.pdf";
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
                        header.Item().Text("📦 Reporte de todas las salidas").FontSize(20).Bold();
                        header.Item().Text($"Rango: {fechaInicioStr} a {fechaFinStr}");
                        header.Item().Text($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}");
                    });

                    page.Content().Table(tabla =>
                    {
                        tabla.ColumnsDefinition(cols =>
                        {
                            cols.RelativeColumn(1); // Fecha
                            cols.RelativeColumn(1); // Código
                            cols.RelativeColumn(3); // Descripción
                            cols.RelativeColumn(1); // Cantidad
                            cols.RelativeColumn(1); // Venta
                            cols.RelativeColumn(1); // Venta total
                            cols.RelativeColumn(1); // Saldo
                        });

                        string[] titulos = {
                "Fecha", "Código", "Descripción", "Cantidad",
                $"Venta ({V_Menu_Principal.moneda})",
                $"Venta total ({V_Menu_Principal.moneda})",
                "Saldo"
            };

                        tabla.Header(header =>
                        {
                            foreach (var titulo in titulos)
                            {
                                header.Cell().Element(c =>
                                    c.Background(Colors.Grey.Lighten3)
                                     .PaddingVertical(4)
                                     .PaddingHorizontal(2)
                                     .ShowOnce())
                                    .Text(titulo).Bold();
                            }
                        });

                        foreach (var salida in salidas)
                        {
                            tabla.Cell().Element(c => c.BorderBottom(0.5f)
                                .BorderColor(Colors.Grey.Lighten2)
                                .Padding(2)).Text(salida.Fecha);

                            tabla.Cell().Text($"{salida.Secuencial_Producto}");
                            tabla.Cell().Text(salida.Descripcion);
                            tabla.Cell().Text($"{salida.Cantidad:N0}");
                            tabla.Cell().Text($"{salida.Venta:N2}");
                            tabla.Cell().Text($"{salida.Venta_Total:N2}");
                            tabla.Cell().Text($"{salida.Saldo:N0}");
                        }
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



            ////////////////
        }

        private void button16_Click(object sender, EventArgs e)
        {
            ///////////////////////////



            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            int secuencialEmpresaActiva = V_Menu_Principal.Secuencial_Empresa;

            // Filtra productos cuyo stock actual está por debajo del mínimo
            var productosBajos = context.Productos
                .Where(p => p.Secuencial_Empresa == secuencialEmpresaActiva
                    && p.Cantidad == p.Existencia_Minima && p.Tipo == "Producto")
                .OrderBy(p => p.Cantidad)
                .Select(p => new
                {
                    Codigo = p.Codigo,
                    Descripcion = p.Descripcion,
                    Cantidad = p.Cantidad,
                    Minimo = p.Existencia_Minima
                })
                .ToList();

            string nombreReporte = "Productos_Con_Existencia_Minima.pdf";
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
                        header.Item().Text("📉 Reporte de productos con existencia mínima").FontSize(20).Bold();
                        header.Item().Text($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}");
                    });

                    page.Content().Table(tabla =>
                    {
                        tabla.ColumnsDefinition(cols =>
                        {
                            cols.RelativeColumn(1); // Código
                            cols.RelativeColumn(3); // Descripción
                            cols.RelativeColumn(1); // Cantidad actual
                            cols.RelativeColumn(1); // Mínimo definido
                        });

                        string[] titulos = { "Código", "Descripción", "Cantidad", "Mínimo" };

                        tabla.Header(header =>
                        {
                            foreach (var titulo in titulos)
                            {
                                header.Cell().Element(c =>
                                    c.Background(Colors.Grey.Lighten3)
                                     .PaddingVertical(4)
                                     .PaddingHorizontal(2)
                                     .ShowOnce())
                                    .Text(titulo).Bold();
                            }
                        });

                        foreach (var producto in productosBajos)
                        {
                            tabla.Cell().Element(c => c.BorderBottom(0.5f)
                                .BorderColor(Colors.Grey.Lighten2)
                                .Padding(2)).Text($"{producto.Codigo}");
                            tabla.Cell().Text(producto.Descripcion);
                            tabla.Cell().Text($"{producto.Cantidad:N0}");
                            tabla.Cell().Text($"{producto.Minimo:N0}");
                        }
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



            ///////////////////////////
        }

        private void button17_Click(object sender, EventArgs e)
        {


            //////////////////////////


            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            int secuencialEmpresaActiva = V_Menu_Principal.Secuencial_Empresa;

            // Filtra productos agotados (cantidad == 0) o bajo mínimo, del tipo "Producto"
            var productosAgotados = context.Productos
                .Where(p => p.Secuencial_Empresa == secuencialEmpresaActiva
                    && (p.Cantidad <= 0 || p.Cantidad < p.Existencia_Minima)
                    && p.Tipo == "Producto")
                .OrderBy(p => p.Descripcion)
                .Select(p => new
                {
                    Codigo = p.Codigo,
                    Descripcion = p.Descripcion,
                    Minimo = p.Existencia_Minima,
                    Cantidad = p.Cantidad // 👈 Incluido en el reporte
                })
                .ToList();

            string nombreReporte = "Productos_Agotados.pdf";
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
                        header.Item().Text("🚫 Reporte de productos agotados").FontSize(20).Bold();
                        header.Item().Text($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}");
                    });

                    page.Content().Table(tabla =>
                    {
                        tabla.ColumnsDefinition(cols =>
                        {
                            cols.RelativeColumn(1); // Código
                            cols.RelativeColumn(3); // Descripción
                            cols.RelativeColumn(1); // Mínimo definido
                            cols.RelativeColumn(1); // Cantidad actual 👈
                        });

                        string[] titulos = { "Código", "Descripción", "Mínimo", "Cantidad" };

                        tabla.Header(header =>
                        {
                            foreach (var titulo in titulos)
                            {
                                header.Cell().Element(c =>
                                    c.Background(Colors.Grey.Lighten3)
                                     .PaddingVertical(4)
                                     .PaddingHorizontal(2)
                                     .ShowOnce())
                                    .Text(titulo).Bold();
                            }
                        });

                        foreach (var producto in productosAgotados)
                        {
                            tabla.Cell().Element(c => c.BorderBottom(0.5f)
                                .BorderColor(Colors.Grey.Lighten2)
                                .Padding(2)).Text(producto.Codigo);

                            tabla.Cell().Text(producto.Descripcion);
                            tabla.Cell().Text($"{producto.Minimo:N0}");

                            tabla.Cell().Text($"{producto.Cantidad:N0}"); // 👈 Mostrar cantidad actual
                        }
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


            //////////////////////////


        }

        private void button18_Click(object sender, EventArgs e)
        {
            //////////////////////

            SQLitePCL.Batteries.Init();
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            int secuencialEmpresaActiva = V_Menu_Principal.Secuencial_Empresa;
            DateTime fechaHoy = DateTime.Today;

            // Filtra productos vencidos cuyo tipo sea "Producto" y con fecha de vencimiento válida y anterior a hoy
            var productosVencidos = context.Productos
                .Where(p => p.Secuencial_Empresa == secuencialEmpresaActiva
                    && p.Tipo == "Producto"
                    && !string.IsNullOrWhiteSpace(p.Fecha_Caducidad)) // ✅ Validamos que no sea nulo o vacío
                .ToList()
                .Where(p => DateTime.TryParseExact(p.Fecha_Caducidad, "dd/MM/yyyy", null,
                                                    System.Globalization.DateTimeStyles.None,
                                                    out DateTime fechaCaducidad)
                            && fechaCaducidad < fechaHoy) // ✅ Filtramos vencidos
                .OrderBy(p => DateTime.ParseExact(p.Fecha_Caducidad, "dd/MM/yyyy", null)) // 👉 Aquí puedes usar TryParse también si quieres máxima robustez
                .Select(p => new
                {
                    Codigo = p.Codigo,
                    Descripcion = p.Descripcion,
                    FechaVencimiento = p.Fecha_Caducidad,
                    Cantidad = p.Cantidad
                })
                .ToList();





            string nombreReporte = "Productos_Vencidos.pdf";
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
                        header.Item().Text("⏳ Reporte de productos vencidos").FontSize(20).Bold();
                        header.Item().Text($"Generado el: {DateTime.Now:dd/MM/yyyy HH:mm}");
                    });

                    page.Content().Table(tabla =>
                    {
                        tabla.ColumnsDefinition(cols =>
                        {
                            cols.RelativeColumn(1); // Código
                            cols.RelativeColumn(3); // Descripción
                            cols.RelativeColumn(1); // Fecha de vencimiento
                            cols.RelativeColumn(1); // Cantidad actual
                        });

                        string[] titulos = { "Código", "Descripción", "Vencimiento", "Cantidad" };

                        tabla.Header(header =>
                        {
                            foreach (var titulo in titulos)
                            {
                                header.Cell().Element(c =>
                                    c.Background(Colors.Red.Lighten3)
                                     .PaddingVertical(4)
                                     .PaddingHorizontal(2)
                                     .ShowOnce())
                                    .Text(titulo).Bold();
                            }
                        });

                        foreach (var producto in productosVencidos)
                        {
                            tabla.Cell().Element(c => c.BorderBottom(0.5f)
                                .BorderColor(Colors.Grey.Lighten2)
                                .Padding(2)).Text(producto.Codigo);
                            tabla.Cell().Text(producto.Descripcion);
                            tabla.Cell().Text(producto.FechaVencimiento);
                            tabla.Cell().Text($"{producto.Cantidad:N0}");
                        }
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

        private void comboProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
