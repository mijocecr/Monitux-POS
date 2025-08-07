using Monitux_POS.Ventanas;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitux_POS.Clases
{
    public class FacturaCompletaPDF_Venta : IDocument
    {
        public int Secuencial { get; set; }
        public string Cliente { get; set; }
        public string TipoVenta { get; set; }
        public string MetodoPago { get; set; }
        public string Fecha { get; set; }
        public List<Item_Factura> Items { get; set; }

        public double OtrosCargos { get; set; }
        public double ISV { get; set; }
        public double Descuento { get; set; }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;


        public byte[] GeneratePdfToBytes()
        {
            using var stream = new MemoryStream();
            Document.Create(container => Compose(container))
                    .GeneratePdf(stream);
            return stream.ToArray();
        }



        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(40);
                page.Size(PageSizes.A4);
                page.DefaultTextStyle(x => x.FontSize(11));

                // ENCABEZADO
                page.Header().Column(header =>
                {
                    header.Item().Row(row =>
                    {
                        row.RelativeColumn().Column(col =>
                        {
                            col.Item().Text(V_Menu_Principal.Nombre_Empresa.ToUpper()).Bold().FontSize(16);
                            col.Item().Text(V_Menu_Principal.Direccion_Empresa);
                            col.Item().Text($"Tel: {V_Menu_Principal.Telefono_Empresa} | {V_Menu_Principal.Email_Empresa}");
                        });

                        row.ConstantColumn(160).Column(col =>
                        {
                            col.Item().Text($"Factura N°: {Secuencial}").AlignRight();//cambiar esto
                            col.Item().Text($"Fecha: {Fecha:dd/MM/yyyy}").AlignRight();
                        });
                    });

                    header.Item().PaddingVertical(10).LineHorizontal(1);
                });

                // CONTENIDO
                page.Content().Column(col =>
                {
                    col.Item().Text($"Cliente: {Cliente}");
                    col.Item().Text($"Tipo de venta: {TipoVenta}");
                    col.Item().Text($"Método de pago: {MetodoPago}");
                    col.Item().PaddingVertical(10).LineHorizontal(1);

                    col.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(1); // Código
                            columns.RelativeColumn(3); // Descripción
                            columns.RelativeColumn(1); // Cantidad
                            columns.RelativeColumn(1); // Precio
                            columns.RelativeColumn(1); // Total
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Código").Bold();
                            header.Cell().Text("Descripción").Bold();
                            header.Cell().AlignRight().Text("Cantidad").Bold();
                            header.Cell().AlignRight().Text("Precio").Bold();
                            header.Cell().AlignRight().Text("Total").Bold();
                        });

                        foreach (var item in Items)
                        {
                            table.Cell().Text(item.Codigo);
                            table.Cell().Text(item.Descripcion);
                            table.Cell().AlignRight().Text(item.Cantidad.ToString());
                            table.Cell().AlignRight().Text($"{item.Precio:C}");
                            table.Cell().AlignRight().Text($"{item.Total:C}");
                        }
                    });

                    col.Item().PaddingTop(10).LineHorizontal(1);

                    double subtotal = Items.Sum(i => i.Total);
                    double totalFinal = subtotal + ISV + OtrosCargos - Descuento;

                    col.Item().AlignRight().Column(totals =>
                    {
                        totals.Item().Text($"Subtotal: {subtotal:C}");
                        totals.Item().Text($"ISV: {ISV:C}");
                        totals.Item().Text($"Otros cargos: {OtrosCargos:C}");
                        totals.Item().Text($"Descuento: -{Descuento:C}");
                        totals.Item().PaddingTop(5).Text($"Total: {totalFinal:C}").Bold();
                    });
                });

                page.Footer().AlignCenter().Text("Gracias por su compra").FontSize(10);
            });
        }



    }//
}//
