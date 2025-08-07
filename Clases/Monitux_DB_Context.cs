namespace Monitux_POS.Clases
{

    using Microsoft.EntityFrameworkCore;

    using Microsoft.Data.Sqlite;
    
    using System;
    using System.IO;
    using System.Reflection;

    public class Monitux_DB_Context : DbContext
    {
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }

        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Proveedor> Proveedores { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Actividad> Actividades { get; set; }

        public DbSet<Kardex> Kardex { get; set; }

        public DbSet<Venta> Ventas { get; set; }

        public DbSet<Venta_Detalle> Ventas_Detalles { get; set; }
        public DbSet<Cuentas_Cobrar> Cuentas_Cobrar { get; set; }

        public DbSet<Ingreso> Ingresos { get; set; }

        public DbSet<Egreso> Egresos { get; set; }

        public DbSet<Cotizacion> Cotizaciones { get; set; }

        public DbSet<Cotizacion_Detalle> Cotizaciones_Detalles { get; set; }

        public DbSet<Abono_Compra> Abonos_Compras { get; set; }

        public DbSet<Abono_Venta> Abonos_Ventas { get; set; }

        public DbSet<Compra> Compras { get; set; }

        public DbSet<Compra_Detalle> Compras_Detalles { get; set; }

        public DbSet<Orden> Ordenes { get; set; }

        public DbSet<Orden_Detalle> Ordenes_Detalles { get; set; }

        public DbSet<Cuentas_Pagar> Cuentas_Pagar { get; set; }

        public DbSet<Empresa> Empresas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            //string dbPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "monitux.db"));

            string dbPath = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Resources\\Database\\monitux.db");

            
            // Verificar si la base de datos ya existe y si contiene la tabla "Productos"


            optionsBuilder.UseSqlite($"Data Source={dbPath}");
        }




        private bool DatabaseExists(string dbPath)
        {
            try
            {
                using var connection = new SqliteConnection($"Data Source={dbPath}");
                connection.Open();
                using var command = new SqliteCommand("SELECT name FROM sqlite_master WHERE type='table' AND name='Productos';", connection);
                return command.ExecuteScalar() != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error verificando la base de datos: {ex.Message}");
                return false;
            }
        }


        private bool ResourceExists(string resourceName)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceNames().Contains(resourceName);
        }
    }



}
