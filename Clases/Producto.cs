using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Monitux_POS.Clases
{
    public class Producto
    {
        public Producto() { }

        public Producto(int secuencial, int secuencial_Proveedor, string codigo, string descripcion, double cantidad, double precio_Costo, double precio_Venta, string? marca, string? codigo_Barra, string? codigo_Fabricante, byte[]? imagen, int secuencial_Categoria, string? fecha_Caducidad, bool expira, string tipo, int secuencial_Empresa)
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
            
            Imagen = imagen;
            Secuencial_Categoria = secuencial_Categoria;
            Fecha_Caducidad = fecha_Caducidad;
            Expira = expira;
            Tipo = tipo;
            Secuencial_Empresa = secuencial_Empresa;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Secuencial { get; set; }

        public int Secuencial_Proveedor { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public double Cantidad { get; set; } = 0;
        public double Precio_Costo { get; set; } = 0;
        public double Precio_Venta { get; set; } = 0;
        public string? Marca { get; set; }
        public string? Codigo_Barra { get; set; }
        public string? Codigo_Fabricante { get; set; }
        
        public byte[]? Imagen { get; set; }

        public string? Fecha_Caducidad { get; set; } = null;
        public string? Tipo { get; set; } = "Producto";
        public int Secuencial_Categoria { get; set; }

        public bool Expira { get; set; } = false;
        public double Existencia_Minima { get; set; }

        public int Secuencial_Empresa { get; set; }

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
            
            Imagen = producto.Imagen;
            Secuencial_Categoria = producto.Secuencial_Categoria;
            Fecha_Caducidad = producto.Fecha_Caducidad;
            Expira = producto.Expira;
            Tipo = producto.Tipo;
            Existencia_Minima = producto.Existencia_Minima;
            Secuencial_Empresa = producto.Secuencial_Empresa;
        }

        public Producto getProducto()
        {
            return new Producto
            {
                Secuencial = this.Secuencial,
                Secuencial_Proveedor = this.Secuencial_Proveedor,
                Codigo = this.Codigo,
                Descripcion = this.Descripcion,
                Cantidad = this.Cantidad,
                Precio_Costo = this.Precio_Costo,
                Precio_Venta = this.Precio_Venta,
                Marca = this.Marca,
                Codigo_Barra = this.Codigo_Barra,
                Codigo_Fabricante = this.Codigo_Fabricante,
                
                Imagen = this.Imagen,
                Fecha_Caducidad = this.Fecha_Caducidad,
                Tipo = this.Tipo,
                Secuencial_Categoria = this.Secuencial_Categoria,
                Expira = this.Expira,
                Existencia_Minima = this.Existencia_Minima,
                Secuencial_Empresa = this.Secuencial_Empresa
            };
        }
    }
}
