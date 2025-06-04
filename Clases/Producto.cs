using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Imaging;

namespace Monitux_POS.Clases
{
    public class Producto
    {
        public Producto()
        {
        }

        [Key]
        public int Secuencial { get; set; }
        public int Secuencial_Proveedor { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public double Cantidad { get; set; }
        public double Precio_Costo { get; set; }
        public double Precio_Venta { get; set; }
        public string? Marca { get; set; }
        public string? Codigo_Barra { get; set; }
        public string? Codigo_Fabricante { get; set; }
        public string? Codigo_QR { get; set; }
        public string? Imagen { get; set; }
        public string? Fecha_Caducidad { get; set; } 

        public int Secuencial_Categoria { get; set; }

        public double Existencia_Minima { get; set; } 
        public Producto(int secuencial, int secuencial_Proveedor,
            string? codigo, string? descripcion, double cantidad,
            double precio_Costo, double precio_Venta,
            string? marca, string? codigo_Barra, string? codigo_Fabricante,
            string? codigo_QR, string? imagen, int secuencial_Categoria,string fecha_caducidad)
        {
            Secuencial = secuencial;
            Secuencial_Proveedor = secuencial_Proveedor;
            Codigo = codigo;
            Descripcion = descripcion;
            Cantidad = cantidad;
            Precio_Costo = precio_Costo;
            Precio_Venta = precio_Venta;
            Marca = marca;
            Codigo_Barra = codigo_Barra;
            Codigo_Fabricante = codigo_Fabricante;
            Codigo_QR = codigo_QR;
            Imagen = imagen;
            Secuencial_Categoria = secuencial_Categoria;
            Fecha_Caducidad = fecha_caducidad;
        }

        public void setProducto(Producto producto)
        {
            Secuencial = producto.Secuencial;
            Secuencial_Proveedor = producto.Secuencial_Proveedor;
            Codigo = producto.Codigo;
            Descripcion = producto.Descripcion;
            Cantidad = producto.Cantidad;
            Precio_Costo = producto.Precio_Costo;
            Precio_Venta = producto.Precio_Venta;
            Marca = producto.Marca;
            Codigo_Barra = producto.Codigo_Barra;
            Codigo_Fabricante = producto.Codigo_Fabricante;
            Codigo_QR = producto.Codigo_QR;
            Imagen = producto.Imagen;
            Secuencial_Categoria = producto.Secuencial_Categoria;
            Fecha_Caducidad = producto.Fecha_Caducidad;
        }


        public Producto getProducto()
        {
            // Retorna una copia del objeto Producto actual
            return new Producto(Secuencial, Secuencial_Proveedor, Codigo, Descripcion, Cantidad,
                Precio_Costo, Precio_Venta, Marca, Codigo_Barra, Codigo_Fabricante,
                Codigo_QR, Imagen, Secuencial_Categoria, Fecha_Caducidad);
        }








       



            
        








        //Final de la clase

    }
}
