using Humanizer;
using Microsoft.Data.Sqlite;
using Monitux_POS.Ventanas;
using System;
using System;
using System.Drawing;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.Management;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms;
using ZXing;
using ZXing.Common;
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

            // Soporte para coma o punto decimal
            input = input.Replace(',', '.');

            if (double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out var number))
            {
                var partes = input.Split('.');
                string parteEnteraTexto = int.Parse(partes[0]).ToWords();

                string centesimasTexto = "cero";
                if (partes.Length > 1)
                {
                    // Solo tomamos los primeros dos dígitos decimales
                    var decimalStr = partes[1].PadRight(2, '0').Substring(0, 2);
                    centesimasTexto = int.Parse(decimalStr).ToWords();
                }

                resultado = $"{parteEnteraTexto} con {centesimasTexto} centésimas";
            }
            else
            {
                resultado = "";
            }

            return resultado.ToUpper();
        }

        #endregion


        #region Obtener los Productos mas vendidos - Retorna Lista de Productos Top

        //OJO: Esta funcion tengo que mejorarla, ya que no esta tomando en cuenta el Secuencial de la Empresa
        
        public static List<Producto_Top_VR> ObtenerTopProductosVendidos(int cantidadTop = 7)
        {
            using (var db = new Monitux_DB_Context())
            {
                var resumen = db.Kardex
                    .Where(k => k.Movimiento == "Salida" && k.Secuencial_Empresa==V_Menu_Principal.Secuencial_Empresa)
                    .GroupBy(k => k.Secuencial_Producto)
                    .Select(g => new {
                        Secuencial_Producto = g.Key,
                        TotalVendido = g.Sum(x => x.Cantidad)
                    })
                    .OrderByDescending(x => x.TotalVendido)
                    .Take(cantidadTop)
                    .ToList();

                var resultado = (from r in resumen
                                 join p in db.Productos on r.Secuencial_Producto equals p.Secuencial
                                 select new Producto_Top_VR
                                 {
                                     Secuencial_Producto = p.Secuencial,
                                     Codigo = p.Codigo,
                                     Descripcion = p.Descripcion,
                                     Venta = p.Precio_Venta,
                                     TotalVendido = r.TotalVendido
                                 }).ToList();

                return resultado;
            }
        }


        #endregion
        public static Image Generar_Codigo_Barra(int secuencial,string codigo,int secuencial_empresa) {



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
            bitmap.Save(Path.GetFullPath(Directory.GetCurrentDirectory() + "\\Resources\\BC\\"+secuencial_empresa+"-BC-" + secuencial +".PNG"));

            // Mostrar el código QR en el PictureBox
            return bitmap;



        }



        public static void EnviarCorreoConPdf(
       string remitente,
       string destinatario,
       string asunto,
       string cuerpo,
       string rutaPdf,
       string smtpServidor,
       int puerto,
       string usuario,
       string contraseña)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(remitente);
                mail.To.Add(destinatario);
                mail.Subject = asunto;
                mail.Body = cuerpo;

                Attachment adjunto = new Attachment(rutaPdf, MediaTypeNames.Application.Pdf);
                mail.Attachments.Add(adjunto);

                SmtpClient smtp = new SmtpClient(smtpServidor= "smtp.gmail.com", puerto=587);
                smtp.Credentials = new NetworkCredential(usuario, contraseña);
                smtp.EnableSsl = true;

                smtp.Send(mail);
                V_Menu_Principal.MSG.ShowMSG("Correo enviado existosamente.", "Exito");
            }
            catch (Exception ex)
            {
                V_Menu_Principal.MSG.ShowMSG("Error al enviar el correo: " + ex.Message,"Error");
            }
        }




        public static void Registrar_Actividad(int secuencial_usuario,string descripcion,int secuencial_empresa)
        {


            // **Create**
            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            var actividad = new Actividad();

            
            actividad.Secuencial_Usuario = secuencial_usuario;
            actividad.Secuencial_Empresa = secuencial_empresa;
            actividad.Descripcion = descripcion;

            context.Actividades.Add(actividad);
            context.SaveChanges();


        }

        /*
        public static void Registrar_Movimiento_Kardex(int secuencial_producto,double existencia,
            string descripcion,double cantidad_unidades,double costo,double venta, string movimiento,int secuencial_empresa)
        {


            // **Create**
            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            var kardex = new Kardex();

            kardex.Secuencial_Empresa = secuencial_empresa;
            kardex.Secuencial_Producto= secuencial_producto;
            kardex.Descripcion = descripcion;
            kardex.Cantidad = cantidad_unidades;
            kardex.Costo = costo;
            kardex.Venta = venta;
            kardex.Movimiento = movimiento;
            if (movimiento == "Entrada")
            {
                kardex.Saldo = existencia + cantidad_unidades;
            }
            else
            {
                kardex.Saldo = existencia - cantidad_unidades;
            }
            
            kardex.Costo_Total = Math.Round(kardex.Saldo * costo,2);
            kardex.Venta_Total = Math.Round(kardex.Saldo * venta,2);



            context.Kardex.Add(kardex);
            context.SaveChanges();


        }
        */




        public static void ExportarDataGridViewAExcel(DataGridView dgv, string nombreHoja, string nombreArchivoBase)
        {
            try
            {
                using var wb = new ClosedXML.Excel.XLWorkbook();
                var ws = wb.Worksheets.Add(nombreHoja);

                // 🔠 Encabezados
                for (int col = 0; col < dgv.Columns.Count; col++)
                {
                    ws.Cell(1, col + 1).Value = dgv.Columns[col].HeaderText;
                    ws.Cell(1, col + 1).Style.Font.Bold = true;
                }

                // 📋 Filas
                for (int row = 0; row < dgv.Rows.Count; row++)
                {
                    for (int col = 0; col < dgv.Columns.Count; col++)
                    {
                        var value = dgv.Rows[row].Cells[col].Value;
                        ws.Cell(row + 2, col + 1).Value = value?.ToString();
                    }
                }

                // 💾 Guardar archivo
                string timestamp = DateTime.Now.ToString("dd-MM-yyyy");
                string nombreArchivo = $"{nombreArchivoBase}.{timestamp}.xlsx";
                string ruta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), nombreArchivo);

                wb.SaveAs(ruta);
                V_Menu_Principal.MSG.ShowMSG($"Exportado correctamente a:\n{ruta}", "Excel generado");
            }
            catch (Exception ex)
            {
                V_Menu_Principal.MSG.ShowMSG($"Error al exportar a Excel: {ex.Message}", "Error");
            }
        }



        public static void Registrar_Movimiento_Kardex(int secuencial_producto, double existencia,
    string descripcion, double cantidad_unidades, double costo, double venta,
    string movimiento, int secuencial_empresa)
        {
            if (cantidad_unidades <= 0 || costo < 0 || venta < 0)
                throw new ArgumentException("Valores inválidos para movimiento de inventario.");

            double saldoNuevo = movimiento == "Entrada"
                ? existencia + cantidad_unidades
                : existencia - cantidad_unidades;

            var kardex = new Kardex
            {
                Secuencial_Empresa = secuencial_empresa,
                Secuencial_Producto = secuencial_producto,
                Descripcion = descripcion,
                Cantidad = cantidad_unidades,
                Costo = costo,
                Venta = venta,
                Movimiento = movimiento,
                Saldo = saldoNuevo,
                Costo_Total = Math.Round(saldoNuevo * costo, 2),
                Venta_Total = Math.Round(saldoNuevo * venta, 2),
                Fecha = DateTime.Now.ToString(), // ¡Registro de cuándo ocurrió!
                //Secuencial_Usuario = secuencial_usuario // Quién lo hizo
            };

            using var context = new Monitux_DB_Context();
            context.Kardex.Add(kardex);
            context.SaveChanges();
        }


        public static Image Generar_Codigo_QR(int secuencial,string codigo, int secuencial_empresa)
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




        public static void Limpiar_Cache(int secuencial_empresa) {

            Limpiar_Cache_Imagenes(secuencial_empresa);//Ya
            Limpiar_Cache_Codigo_QR(secuencial_empresa);//Ya
            Limpiar_Cache_Codigo_Barra(secuencial_empresa);
            Limpiar_Cache_Categoria(secuencial_empresa);//Ya
            Limpiar_Cache_Proveedor(secuencial_empresa);//Ya
            Limpiar_Cache_Cliente(secuencial_empresa);//Ya
            Limpiar_Cache_Usuario(secuencial_empresa);//ya

        }



        public static void Limpiar_Cache_Imagenes(int secuencial_empresa)
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



        public static Image Cargar_Imagen_Local(string ruta)
        {

            if (!string.IsNullOrEmpty(ruta))
            {


                Image original = Image.FromFile(ruta);


                Image clon = new Bitmap(original);
                original.Dispose(); // Libera el archivo original

                return clon;


            }
            else { 
            
                return null;  // Retorna null si la ruta es nula o vacía
            }



        }




        public static Image CargarImagenDesdeUrl(string url)
        {
            try
            {
                using (var cliente = new WebClient())
                {
                    byte[] datos = cliente.DownloadData(url);

                    using (var stream = new MemoryStream(datos))
                    {
                        return Image.FromStream(stream); // Puedes devolverla directamente
                    }
                }
            }
            catch (Exception ex)
            {
                V_Menu_Principal.MSG.ShowMSG("Error al cargar imagen desde URL:\n" + ex.Message, "Error");
                return null;
            }
        }






public static class AnimacionesUI
    {
        public static void AnimarCrecimiento(Control control, Size tamañoFinal, int pasos = 10, int velocidadMs = 5)
        {
            if (control == null) return;

            int pasoActual = 0;
            Size tamañoInicial = new Size(0, 156);
            control.Size = tamañoInicial;

            int incrementoAncho = (tamañoFinal.Width - tamañoInicial.Width) / pasos;
            int incrementoAlto = (tamañoFinal.Height - tamañoInicial.Height) / pasos;

                System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

                timer.Interval = velocidadMs;

            timer.Tick += (s, e) =>
            {
                if (pasoActual < pasos)
                {
                    control.Size = new Size(
                        control.Width + incrementoAncho,
                        control.Height + incrementoAlto
                    );
                    pasoActual++;
                }
                else
                {
                    control.Size = tamañoFinal;
                    timer.Stop();
                    timer.Dispose();
                }
            };

            timer.Start();
        }
}







        public static void Limpiar_Cache_Categoria(int secuencial_empresa)
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



        public static void Limpiar_Cache_Proveedor(int secuencial_empresa)
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







        public static void Limpiar_Cache_Cliente(int secuencial_empresa)
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




        public static void Limpiar_Cache_Usuario(int secuencial_empresa)
        {



            // Inicializar SQLite
            SQLitePCL.Batteries.Init();

            // Crear el contexto de la base de datos
            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            // Obtener la lista de imágenes permitidas
            List<string> listaImagenespermitidas = context.Usuarios
                                                          .Select(p => Path.GetFileName(p.Imagen))
                                                          .ToList();



            // Definir la ruta de la carpeta de imágenes
            string folderPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Resources", "USR"));

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







        public static void Limpiar_Cache_Codigo_QR(int secuencial_empresa)
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



        public static void Limpiar_Cache_Codigo_Barra(int secuencial_empresa)
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
                listaImagenespermitidas.Add(secuencial_empresa+"-BC-" +item+".PNG");
                
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

        internal static string Obtener_Mensaje_Cinta()
        {
            //Revisar esto y Mejorar la logica de mensajes
            Dictionary<int, string> mensajes = new Dictionary<int, string>();
            mensajes.Add(mensajes.Count + 1, "Monitux-Pos by:" + Environment.UserName + " - " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));





            mensajes.TryGetValue(mensajes.Count, out string mensaje);// "Monitux-Pos by: miguel.cerrato.es@gmail.com";
            return mensaje;
        }




        public class LoginManager
        {
            public Usuario ValidarUsuario(string codigo, string password, int secuencial_empresa)
            {
                using var context = new Monitux_DB_Context();
                var usuario = context.Usuarios
                    .FirstOrDefault(u => u.Codigo == codigo && u.Password == password && u.Secuencial_Empresa==secuencial_empresa);

                return usuario; //!= null; // Retorna true si las credenciales son correctas
            }
        }







    }//Fin de Clase
}//NameSpace
