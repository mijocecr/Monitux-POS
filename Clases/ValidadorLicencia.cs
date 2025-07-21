namespace Monitux_POS.Clases
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;

    /// <summary>
    /// Representa el resultado de la validación de una licencia.
    /// </summary>
    public class ResultadoLicencia
    {
        public bool EsValido { get; set; }
        public string NombreCliente { get; set; } = "";
        public DateTime FechaExpiracion { get; set; }
        public string Estado { get; set; } = "INDEFINIDO";
    }

    /// <summary>
    /// Clase encargada de validar una licencia contra una hoja de cálculo de Google.
    /// </summary>
    public class ValidadorLicencia
    {
        // Cambia esto por el ID real de tu hoja
        private const string GoogleSheetId = "1Y7dfYNYySdANwfjfvEFgWFap54QA-lJ6KoBabjuiHIs";
        private const string Gid = "0"; // Número de hoja (usualmente 0 para la primera)

        /// <summary>
        /// Valida una licencia accediendo a Google Sheets como CSV.
        /// </summary>
        /// <param name="codigoIngresado">Código que escribió el usuario</param>
        /// <returns>Un objeto ResultadoLicencia con los datos evaluados</returns>
        public async Task<ResultadoLicencia> ValidarAsync(string codigoIngresado)
        {
            var resultado = new ResultadoLicencia();


            //https://docs.google.com/spreadsheets/d/1Y7dfYNYySdANwfjfvEFgWFap54QA-lJ6KoBabjuiHIs/export?format=csv&gid=0

            try
            {
                string url = $"https://docs.google.com/spreadsheets/d/{GoogleSheetId}/export?format=csv&gid={Gid}";
                using HttpClient client = new HttpClient();
                string csv = await client.GetStringAsync(url);

                using StringReader reader = new StringReader(csv);
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] campos = line.Split(',');

                    // Esperamos al menos 4 columnas: Código, Nombre, Expira, Estado
                    if (campos.Length >= 4)
                    {
                        string codigo = campos[0].Trim();
                        string nombre = campos[1].Trim();
                        string fecha = campos[2].Trim();
                        string estado = campos[3].Trim().ToUpper();

                        if (codigo.Equals(codigoIngresado, StringComparison.OrdinalIgnoreCase) &&
                            DateTime.TryParse(fecha, out DateTime expira))
                        {
                            resultado.NombreCliente = nombre;
                            resultado.FechaExpiracion = expira;
                            resultado.Estado = estado;

                            if (estado == "ACTIVO" && expira >= DateTime.Today)
                            {
                                resultado.EsValido = true;
                            }

                            break; // Ya encontramos el código, no seguimos leyendo
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Puedes logear el error si lo deseas
                resultado.Estado = $"ERROR: {ex.Message}";
            }

            return resultado;
        }
    }





}