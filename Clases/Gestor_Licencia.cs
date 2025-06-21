using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitux_POS.Clases
{
    public class Gestor_Licencia
    {
        ///////////////////////////////////////////////////


        
            private const string UrlHoja = "https://docs.google.com/spreadsheets/d/1Y7dfYNYySdANwfjfvEFgWFap54QA-lJ6KoBabjuiHIs/export?format=csv&gid=0";
            private const string UrlActivacion = "https://script.google.com/macros/s/AKfycbw4q6q0yvqEo8SisdTMt95xOuU797RaEWKw9v6-zFqRamtPmPErvIwrzQfj-EwqDh8CiA/exec";

            public async Task<bool> ValidarYActivarLicenciaAsync(string codigo)
            {
                // 1. ¿Ya está validada localmente?
                if (Properties.Settings.Default.LicenciaValida)
                    return true;

                if (string.IsNullOrWhiteSpace(codigo))
                    return false;

                try
                {
                    using var http = new HttpClient();
                    string csv = await http.GetStringAsync(UrlHoja);

                    using var reader = new StringReader(csv);
                    string? line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] campos = line.Split(',');

                        if (campos.Length >= 5)
                        {
                            string cod = campos[0].Trim();
                            string nombre = campos[1].Trim();
                            string fechaStr = campos[2].Trim();
                            string estado = campos[3].Trim().ToUpper();
                            string usada = campos[4].Trim().ToUpper();

                            if (cod.Equals(codigo, StringComparison.OrdinalIgnoreCase) &&
                                estado == "ACTIVO" &&
                                DateTime.TryParse(fechaStr, out DateTime expira) &&
                                expira >= DateTime.Today &&
                                usada != "SI")
                            {
                                // Paso extra: marcar como activado con Google Apps Script
                                string urlActivacionFinal = $"{UrlActivacion}?codigo={codigo}";
                                string respuesta = await http.GetStringAsync(urlActivacionFinal);

                                if (respuesta.Trim().ToUpper() == "ACTIVADA")
                                {
                                    // Guardamos localmente los datos de activación
                                    Properties.Settings.Default.LicenciaValida = true;
                                    Properties.Settings.Default.NombreCliente = nombre;
                                    Properties.Settings.Default.FechaExpiracion = expira;
                                    Properties.Settings.Default.Save();

                                    return true;
                                }
                            }
                        }
                    }
                }
                catch
                {
                    // Silenciar errores o loguearlos si deseas
                }

                return false;
            }
        




        ///////////////////////////////////////////////////

    }
}
