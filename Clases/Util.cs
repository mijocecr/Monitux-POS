using System;
using System.Management;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

using Humanizer;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using ZXing.Common;
using ZXing;
using ZXing.Windows.Compatibility;




namespace Monitux_POS.Clases
{
    public static class Util
    {

        #region Abrir Dialogo de Seleccion - Retorna URL

        //Abre dialogo de seleccion para abrir archivo
        public static string Abrir_Dialogo_Seleccion_URL()
        {
            var x = new OpenFileDialog();

            x.FilterIndex = 0;
            if (x.ShowDialog() == DialogResult.OK)
            {
                return x.FileName;
            }
            else
            { return ""; }


        }
        #endregion

        #region Abrir Dialogo de Seleccion - Retorna Filename

        //Abre dialogo de seleccion para abrir archivo
        public static string Abrir_Dialogo_Seleccion_Filename()
        {
            var x = new OpenFileDialog();

            x.FilterIndex = 0;
            if (x.ShowDialog() == DialogResult.OK)
            {
                return Path.GetFileName(x.FileName);
            }
            else
            { return ""; }

        }
        #endregion

        #region Clonar Control - Retorna Control


        //Copia un control con todas sus propiedades

        public static T Clonar_Control<T>(T controlOriginal) where T : Control
        {
            T nuevoControl = Activator.CreateInstance<T>();

            foreach (PropertyInfo propiedad in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (propiedad.CanWrite && propiedad.Name != "WindowTarget") // Evitar propiedades internas del sistema
                {
                    propiedad.SetValue(nuevoControl, propiedad.GetValue(controlOriginal, null), null);
                }
            }

            return nuevoControl;
        }

        #endregion


        #region Comprueba si un dispositivo esta conectado - Retorna Boolean

        public static bool Comprobar_Llave_USB(string id)
        {
            using var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity");
            var collection = searcher.Get();

            // Recorre todos los dispositivos conectados
            foreach (ManagementObject device in collection)
            {
                string deviceID = device["DeviceID"]?.ToString(); // Obtiene el ID del dispositivo
                if (deviceID != null && deviceID == id) // Compara si contiene el ID buscado
                    return true;
            }

            return false; // Si no encontró coincidencias, retorna false
        }
        #endregion


        #region Obtener Nombre de Archivo Pasando URL como parametro - Retorna String

        public static string Obtener_Nombre_Archivo(string url)
        {
            return Path.GetFileName(url);
        }
        #endregion


        #region Clase para obtener las especificaciones de los dispositivos conectados

        public class USBInfo
        {


            public USBInfo(string deviceID, string pnpDeviceID, string description)
            {
                this.DeviceID = deviceID;
                this.PnpDeviceID = pnpDeviceID;
                this.Description = description;
            }


            public string DeviceID { get; private set; }


            public string PnpDeviceID { get; private set; }


            public string Description { get; private set; }
        }









        public static bool ComprobarDispositivoConectado(string id)
        {
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"SELECT * FROM Win32_PnPEntity"))
            {
                return searcher.Get().Cast<ManagementObject>().Any();
            }
        }




        public class Usb
        {


            public List<USBInfo> GetUSBDevices()
            {

                //creamos una lista de USBInfo
                List<USBInfo> lstDispositivos = new List<USBInfo>();

                //creamos un ManagementObjectCollection para obtener nuestros dispositivos
                ManagementObjectCollection collection;

                //utilizando la WMI clase Win32_USBHub obtenemos todos los dispositivos USB
                using (var searcher = new ManagementObjectSearcher(@"Select * From Win32_USBHub"))

                    //asignamos los dispositivos a nuestra coleccion
                    collection = searcher.Get();

                //recorremos la colección
                foreach (var device in collection)
                {
                    //asignamos el dispositivo a nuestra lista
                    lstDispositivos.Add(new USBInfo(
                    (string)device.GetPropertyValue("DeviceID"),
                    (string)device.GetPropertyValue("PNPDeviceID"),
                    (string)device.GetPropertyValue("Description")
                    ));
                }

                //liberamos el objeto collection
                collection.Dispose();
                //regresamos la lista
                return lstDispositivos;
            }
        }




        #endregion


        #region Clase para encriptar y desencriptar texto -Retorna String
        public class Encriptador
        {
            private static readonly byte[] Clave = Encoding.UTF8.GetBytes("0123456789abcdef"); // 16 bytes (128 bits)
            private static readonly byte[] IV = Encoding.UTF8.GetBytes("abcdef0123456789"); // 16 bytes (128 bits)

            public static string Encriptar(string texto)
            {
                using (System.Security.Cryptography.Aes aes = Aes.Create())
                {
                    aes.Key = Clave;
                    aes.IV = IV;

                    using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                    {
                        byte[] textoBytes = Encoding.UTF8.GetBytes(texto);
                        byte[] textoEncriptado = encryptor.TransformFinalBlock(textoBytes, 0, textoBytes.Length);
                        return Convert.ToBase64String(textoEncriptado);
                    }
                }


            }

            public static string Desencriptar(string textoEncriptado)
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Clave;
                    aes.IV = IV;

                    using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                    {
                        byte[] textoBytes = Convert.FromBase64String(textoEncriptado);
                        byte[] textoDesencriptado = decryptor.TransformFinalBlock(textoBytes, 0, textoBytes.Length);
                        return Encoding.UTF8.GetString(textoDesencriptado);
                    }
                }
            }



        }

#endregion


        #region Convertir Numeros a Palabras - Retorna String
        public static string Convertir_Numeros_Palabras(string input)
        {

            string resultado;

            if (double.TryParse(input, out var number))
            {
                var partes = input.Split('.');
                var entero = int.Parse(partes[0]).ToWords();
                var decimalParte = partes.Length > 1 ? partes[1] : "0";
                 resultado = $"{entero} con {int.Parse(decimalParte).ToWords()} centésimas";
                //MessageBox.Show($"{entero} con {int.Parse(decimalParte).ToWords()} centésimas");
            }
            else
            {
                //MessageBox.Show("Entrada no válida.");
                resultado= "";
            }
            return resultado.ToUpper(); ;
        }

        #endregion



        public static Image Generar_Codigo_Barra(int secuencial,string codigo) {



            // Crear una instancia del generador de código QR
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.CODE_128,
                Options = new EncodingOptions
                {
                    Width = 300,
                    Height = 200,
                    Margin = 1
                }
            };

            // Generar el código Barra
            Bitmap bitmap = writer.Write(codigo);
          //  picture.Image?.Dispose();
            // Guardar la imagen
            bitmap.Save(Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Resources\\BC\\BC-" + secuencial +".PNG"));

            // Mostrar el código QR en el PictureBox
            return bitmap;



        }



        public static Image Generar_Codigo_QR(int secuencial,string codigo)
        {



            // Crear una instancia del generador de código QR
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new EncodingOptions
                {
                    Width = 300,
                    Height = 300,
                    Margin = 1
                }
            };

            // Generar el código Barra
            Bitmap bitmap = writer.Write(codigo);

            // Guardar la imagen
            //picture.Image?.Dispose();
          //  bitmap.Save(Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Resources\\Imagenes\\QR-" + secuencial + "-" + codigo + ".PNG"));

            // Mostrar el código QR en el PictureBox
            return bitmap;



        }




        public static void Limpiar_Cache() {

            Limpiar_Cache_Imagenes();
            Limpiar_Cache_Codigo_QR();
            Limpiar_Cache_Codigo_Barra();
            Limpiar_Cache_Categoria();
            Limpiar_Cache_Proveedor();
            Limpiar_Cache_Cliente();

        }



        public static void Limpiar_Cache_Imagenes()
        {



            // Inicializar SQLite
            SQLitePCL.Batteries.Init();

            // Crear el contexto de la base de datos
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            // Obtener la lista de imágenes permitidas
            List<string> listaImagenespermitidas = context.Productos
                                                          .Select(p => Path.GetFileName(p.Imagen))
                                                          .ToList();

           

            // Definir la ruta de la carpeta de imágenes
            string folderPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Imagenes"));

            // Verificar si la carpeta existe antes de proceder
            if (Directory.Exists(folderPath))
            {
                var filesInDirectory = Directory.GetFiles(folderPath);

                foreach (var file in filesInDirectory)
                {
                    string fileName = Path.GetFileName(file);

                    // Comparar directamente con la lista de imágenes permitidas
                    if (!listaImagenespermitidas.Contains(fileName))
                    {
                        File.Delete(file);
                        Console.WriteLine($"Eliminado: {fileName}");
                    }
                }
            }
            else
            {
                Console.WriteLine("La carpeta de imágenes no existe.");
            }

        }



        public static void Limpiar_Cache_Categoria()
        {



            // Inicializar SQLite
            SQLitePCL.Batteries.Init();

            // Crear el contexto de la base de datos
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            // Obtener la lista de imágenes permitidas
            List<string> listaImagenespermitidas = context.Categorias
                                                          .Select(p => Path.GetFileName(p.Imagen))
                                                          .ToList();



            // Definir la ruta de la carpeta de imágenes
            string folderPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Resources", "CAT"));

            // Verificar si la carpeta existe antes de proceder
            if (Directory.Exists(folderPath))
            {
                var filesInDirectory = Directory.GetFiles(folderPath);

                foreach (var file in filesInDirectory)
                {
                    string fileName = Path.GetFileName(file);

                    // Comparar directamente con la lista de imágenes permitidas
                    if (!listaImagenespermitidas.Contains(fileName))
                    {
                        File.Delete(file);
                        Console.WriteLine($"Eliminado: {fileName}");
                    }
                }
            }
            else
            {
                Console.WriteLine("La carpeta de imágenes no existe.");
            }

        }



        public static void Limpiar_Cache_Proveedor()
        {



            // Inicializar SQLite
            SQLitePCL.Batteries.Init();

            // Crear el contexto de la base de datos
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            // Obtener la lista de imágenes permitidas
            List<string> listaImagenespermitidas = context.Proveedores
                                                          .Select(p => Path.GetFileName(p.Imagen))
                                                          .ToList();



            // Definir la ruta de la carpeta de imágenes
            string folderPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Resources", "PRO"));

            // Verificar si la carpeta existe antes de proceder
            if (Directory.Exists(folderPath))
            {
                var filesInDirectory = Directory.GetFiles(folderPath);

                foreach (var file in filesInDirectory)
                {
                    string fileName = Path.GetFileName(file);

                    // Comparar directamente con la lista de imágenes permitidas
                    if (!listaImagenespermitidas.Contains(fileName))
                    {
                        File.Delete(file);
                        Console.WriteLine($"Eliminado: {fileName}");
                    }
                }
            }
            else
            {
                Console.WriteLine("La carpeta de imágenes no existe.");
            }

        }







        public static void Limpiar_Cache_Cliente()
        {



            // Inicializar SQLite
            SQLitePCL.Batteries.Init();

            // Crear el contexto de la base de datos
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            // Obtener la lista de imágenes permitidas
            List<string> listaImagenespermitidas = context.Clientes
                                                          .Select(p => Path.GetFileName(p.Imagen))
                                                          .ToList();



            // Definir la ruta de la carpeta de imágenes
            string folderPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Resources", "CLI"));

            // Verificar si la carpeta existe antes de proceder
            if (Directory.Exists(folderPath))
            {
                var filesInDirectory = Directory.GetFiles(folderPath);

                foreach (var file in filesInDirectory)
                {
                    string fileName = Path.GetFileName(file);

                    // Comparar directamente con la lista de imágenes permitidas
                    if (!listaImagenespermitidas.Contains(fileName))
                    {
                        File.Delete(file);
                        Console.WriteLine($"Eliminado: {fileName}");
                    }
                }
            }
            else
            {
                Console.WriteLine("La carpeta de imágenes no existe.");
            }

        }






        public static void Limpiar_Cache_Codigo_QR()
        {



            // Inicializar SQLite
            SQLitePCL.Batteries.Init();

            // Crear el contexto de la base de datos
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            // Obtener la lista de imágenes permitidas
            List<string> listaImagenespermitidas = context.Productos
                                                          .Select(p => Path.GetFileName(p.Codigo_QR))
                                                          .ToList();



            // Definir la ruta de la carpeta de imágenes
            string folderPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Resources", "QR"));

            // Verificar si la carpeta existe antes de proceder
            if (Directory.Exists(folderPath))
            {
                var filesInDirectory = Directory.GetFiles(folderPath);

                foreach (var file in filesInDirectory)
                {
                    string fileName = Path.GetFileName(file);

                    // Comparar directamente con la lista de imágenes permitidas
                    if (!listaImagenespermitidas.Contains(fileName))
                    {
                        File.Delete(file);
                        Console.WriteLine($"Eliminado: {fileName}");
                    }
                }
            }
            else
            {
                Console.WriteLine("La carpeta de imágenes no existe.");
            }

        }



        public static void Limpiar_Cache_Codigo_Barra()
        {



            // Inicializar SQLite
            SQLitePCL.Batteries.Init();

            // Crear el contexto de la base de datos
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            // Obtener la lista de imágenes permitidas

            //BC - 15 - 2266
            List<string> listaCodigo = context.Productos
                                                          .Select(p => Path.GetFileName(p.Secuencial.ToString()))
                                                          .ToList();

            List<string> listaImagenespermitidas = new List<string>();

        foreach (var item in listaCodigo)
            {
                listaImagenespermitidas.Add("BC-" +item+".PNG");
                
            }

            // Definir la ruta de la carpeta de imágenes
            string folderPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Resources", "BC"));

            // Verificar si la carpeta existe antes de proceder
            if (Directory.Exists(folderPath))
            {
                var filesInDirectory = Directory.GetFiles(folderPath);

                foreach (var file in filesInDirectory)
                {
                    string fileName = Path.GetFileName(file);

                    // Comparar directamente con la lista de imágenes permitidas
                    if (!listaImagenespermitidas.Contains(fileName))
                    {
                        File.Delete(file);
                        Console.WriteLine($"Eliminado: {fileName}");
                    }
                }
            }
            else
            {
                Console.WriteLine("La carpeta de imágenes no existe.");
            }

        }



    }//Fin de Clase
}//NameSpace
