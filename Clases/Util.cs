using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic;

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



    }//Fin de Clase
    }//NameSpace
