namespace Monitux_POS.Clases
{


    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;
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



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            //string dbPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "monitux.db"));

            string dbPath = Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Resources\\Database\\monitux.db");

            //MessageBox.Show("perro: "+dbPath);
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
