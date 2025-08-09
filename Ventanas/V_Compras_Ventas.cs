using Microsoft.EntityFrameworkCore;
using Monitux_POS.Clases;
using PdfiumViewer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monitux_POS.Ventanas
{
    public partial class V_Compras_Ventas : Form
    {

        public int Secuencial_Venta { get; set; }
        public int Secuencial_Compra { get; set; }
        public int Secuencial_Cliente { get; set; }

        public int Secuencial_Proveedor { get; set; }

        //public int Secuencial { get; set; }
        public static Dictionary<string, double> Lista = new Dictionary<string, double>();
        public static string cliente_seleccionado;
        public static string proveedor_seleccionado;




        public V_Compras_Ventas()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }



        public void Configurar_DataGridView_Ventas()
        {
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Configurar las columnas del DataGridView
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("Secuencial", "No.");
            dataGridView1.Columns.Add("Fecha", "Fecha");
            dataGridView1.Columns.Add("Tipo", "Tipo");

            dataGridView1.Columns.Add("Total", "Total");
            dataGridView1.Columns.Add("Gran_Total", "Gran Total");
            dataGridView1.Columns.Add("Secuencial_Cliente", "SC");


            dataGridView1.AutoSizeColumnsMode
                = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
        }




        public void Configurar_DataGridView_Compras()
        {
            dataGridView3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Configurar las columnas del DataGridView
            dataGridView3.Columns.Clear();
            dataGridView3.Columns.Add("Secuencial", "No.");
            dataGridView3.Columns.Add("Fecha", "Fecha");
            dataGridView3.Columns.Add("Tipo", "Tipo");

            dataGridView3.Columns.Add("Total", "Total");
            dataGridView3.Columns.Add("Gran_Total", "Gran Total");
            dataGridView3.Columns.Add("Secuencial_Proveedor", "SP");


            dataGridView3.AutoSizeColumnsMode
                = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView3.ReadOnly = true;
        }



        public void Configurar_DataGridView_Detalle_Venta()
        {
            // Configurar las columnas del DataGridView
            dataGridView2.Enabled = true;

            dataGridView2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;



            dataGridView2.Columns.Clear();
            dataGridView2.Columns.Add("Secuencial", "S");
            dataGridView2.Columns.Add("Codigo", "Código");
            dataGridView2.Columns.Add("Descripcion", "Descripción");
            dataGridView2.Columns.Add("Cantidad", "Cantidad");
            dataGridView2.Columns.Add("Precio_Venta", "Precio");

            dataGridView2.Columns.Add("Total", "Total");
            dataGridView2.Columns.Add("Secuencial_Producto", "SP");
            dataGridView2.Columns.Add("Tipo", "Tipo");

            dataGridView2.AutoSizeColumnsMode
                = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.ReadOnly = true;
        }





        public void Configurar_DataGridView_Detalle_Compra()
        {
            // Configurar las columnas del DataGridView
            dataGridView4.Enabled = true;

            dataGridView4.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;



            dataGridView4.Columns.Clear();
            dataGridView4.Columns.Add("Secuencial", "S");
            dataGridView4.Columns.Add("Codigo", "Código");
            dataGridView4.Columns.Add("Descripcion", "Descripción");
            dataGridView4.Columns.Add("Cantidad", "Cantidad");
            dataGridView4.Columns.Add("Precio_Venta", "Precio");

            dataGridView4.Columns.Add("Total", "Total");
            dataGridView4.Columns.Add("Secuencial_Producto", "SP");
            dataGridView4.Columns.Add("Tipo", "Tipo");

            dataGridView4.AutoSizeColumnsMode
                = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView4.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView4.ReadOnly = true;
        }



        /////////////////////////////////////////////////////////



        private void Filtrar_Compra(int secuencial_proveedor)
        {
            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            var compras = context.Compras
                .Where(c => c.Secuencial_Proveedor == secuencial_proveedor &&
                            c.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa)
                .ToList();

            // Configurar DataGridView si es necesario
            if (dataGridView3.Columns.Count == 0)
            {
                Configurar_DataGridView_Compras();
            }

            dataGridView3.Rows.Clear();
            dataGridView3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            foreach (var item in compras)
            {
                dataGridView3.Rows.Add(
                    item.Secuencial,
                    item.Fecha,
                    item.Tipo,
                    item.Total,
                    item.Gran_Total,
                    item.Secuencial_Proveedor
                );
            }

            // Si no hay compras, limpiar detalle
            if (dataGridView3.Rows.Count == 0)
            {
                dataGridView3.Rows.Clear();
                return;
            }

            // Actualizar variable si estás usando ComboBox para seleccionar proveedor
            Secuencial_Proveedor = int.Parse(comboProveedor.SelectedItem.ToString().Split('-')[0].Trim());
        }








        private void Filtrar_Venta(int secuencial_cliente)
        {
            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            var ventas = context.Ventas
                .Where(v => v.Secuencial_Cliente == secuencial_cliente &&
                            v.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa)
                .ToList();

            // Configurar columnas si es necesario
            if (dataGridView1.Columns.Count == 0)
            {
                Configurar_DataGridView_Ventas();
            }

            dataGridView1.Rows.Clear();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            foreach (var item in ventas)
            {
                dataGridView1.Rows.Add(
                    item.Secuencial,
                    item.Fecha,
                    item.Tipo,
                    item.Total,
                    item.Gran_Total,
                    item.Secuencial_Cliente
                );
            }

            // Si no hay resultados, limpiar detalle
            if (ventas.Count == 0)
            {
                dataGridView2.Rows.Clear();
                return;
            }

            // Actualizar variable si estás usando ComboBox para seleccionar cliente
            Secuencial_Cliente = int.Parse(comboCliente.SelectedItem.ToString().Split('-')[0].Trim());
        }



        private void Filtrar_Detalle_Venta()
        {
            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            var detalles = context.Ventas_Detalles
                .Where(v => v.Secuencial_Factura == Secuencial_Venta &&
                            v.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa)
                .ToList();

            // Configurar columnas si es necesario
            if (dataGridView2.Columns.Count == 0)
            {
                Configurar_DataGridView_Detalle_Venta();
            }

            dataGridView2.Rows.Clear();
            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            foreach (var item in detalles)
            {
                dataGridView2.Rows.Add(
                    item.Secuencial,
                    item.Codigo,
                    item.Descripcion,
                    item.Cantidad,
                    item.Precio,
                    Math.Round((double)item.Total, 2),
                    item.Secuencial_Producto,
                    item.Tipo
                );
            }
        }





        private void Filtrar_Detalle_Compra()
        {
            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            var detalles = context.Compras_Detalles
                .Where(c => c.Secuencial_Factura == Secuencial_Compra &&
                            c.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa)
                .ToList();

            // Configurar columnas si es necesario
            if (dataGridView4.Columns.Count == 0)
            {
                Configurar_DataGridView_Detalle_Compra();
            }

            dataGridView4.Rows.Clear();
            dataGridView4.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            foreach (var item in detalles)
            {
                dataGridView4.Rows.Add(
                    item.Secuencial,
                    item.Codigo,
                    item.Descripcion,
                    item.Cantidad,
                    item.Precio,
                    Math.Round((double)item.Total, 2),
                    item.Secuencial_Producto,
                    item.Tipo
                );
            }
        }





        public void llenar_Combo_Cliente()
        {


            comboCliente.Items.Clear();

            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            // Filtrar solo clientes activos
            var clientesActivos = context.Clientes.Where(c => (bool)c.Activo && c.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa).ToList();

            foreach (var item in clientesActivos)
            {
                comboCliente.Items.Add(item.Secuencial + " - " + item.Nombre);
            }



        }



        public void llenar_Combo_Proveedor()
        {


            comboProveedor.Items.Clear();

            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            // Filtrar solo clientes activos
            var proveedoresActivos = context.Proveedores.Where(c => (bool)c.Activo && c.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa).ToList();

            foreach (var item in proveedoresActivos)
            {
                comboProveedor.Items.Add(item.Secuencial + " - " + item.Nombre);
            }



        }




        private void V_Compras_Ventas_Load(object sender, EventArgs e)
        {

            if (V_Menu_Principal.Acceso_Usuario == "Administrador")
            {
                button1.Visible = true;
                button6.Visible = true;
            }
            else {                 
                
                button1.Visible = false;
                button6.Visible = false;
            }

            Configurar_DataGridView_Ventas();
            Configurar_DataGridView_Detalle_Venta();
            Configurar_DataGridView_Compras();
            Configurar_DataGridView_Detalle_Compra();
            llenar_Combo_Cliente();
            llenar_Combo_Proveedor();


        }

        private void comboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filtrar_Venta(int.Parse(comboCliente.SelectedItem.ToString().Split('-')[0].Trim()));
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            comboCliente.Items.Clear();

            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            // Filtrar solo clientes activos cuyo teléfono contiene el texto ingresado
            var clientes = context.Clientes
                .Where(c => (bool)c.Activo && c.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa && EF.Property<string>(c, "Telefono").Contains(textBox2.Text))
                .ToList();

            foreach (var item in clientes)
            {
                comboCliente.Items.Add(item.Secuencial.ToString() + " - " + item.Nombre);
                comboCliente.SelectedItem = item.Secuencial.ToString() + " - " + item.Nombre;
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {


            try
            {



                if (dataGridView1.Rows[e.RowIndex].Cells["Secuencial"].Value != null)
                {
                    this.Secuencial_Venta = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Secuencial"].Value);

                    Filtrar_Detalle_Venta();

                }



            }
            catch (Exception ex)
            {

            }




        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void comboProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            Filtrar_Compra(int.Parse(comboProveedor.SelectedItem.ToString().Split('-')[0].Trim()));
        }

        private void dataGridView3_CellEnter(object sender, DataGridViewCellEventArgs e)
        {


            try
            {



                if (dataGridView3.Rows[e.RowIndex].Cells["Secuencial"].Value != null)
                {
                    this.Secuencial_Compra = Convert.ToInt32(dataGridView3.Rows[e.RowIndex].Cells["Secuencial"].Value);

                    Filtrar_Detalle_Compra();

                }



            }
            catch (Exception ex)
            {

            }


        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {


            comboProveedor.Items.Clear();

            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated(); // Crea la base de datos si no existe

            // Filtrar solo clientes activos cuyo teléfono contiene el texto ingresado
            var proveedores = context.Proveedores
                .Where(c => (bool)c.Activo && c.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa && EF.Property<string>(c, "Telefono").Contains(textBox1.Text))
                .ToList();

            foreach (var item in proveedores)
            {
                comboProveedor.Items.Add(item.Secuencial.ToString() + " - " + item.Nombre);
                comboProveedor.SelectedItem = item.Secuencial.ToString() + " - " + item.Nombre;
            }



        }

        private void button4_Click(object sender, EventArgs e)
        {

            SQLitePCL.Batteries.Init();

            using var context = new Monitux_DB_Context();
            context.Database.EnsureCreated();

            // Validar cliente
            var clienteTexto = comboCliente.SelectedItem?.ToString();
            if (string.IsNullOrWhiteSpace(clienteTexto) || !clienteTexto.Contains("- "))
            {
                V_Menu_Principal.MSG.ShowMSG("Por favor, selecciona un cliente válido antes de continuar.", "Aviso");
                return;
            }

            string nombreCliente = clienteTexto.Substring(clienteTexto.IndexOf("- ") + 2).Trim();

            // Buscar venta
            var venta = context.Ventas.FirstOrDefault(v =>
                v.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa &&
                v.Secuencial == Secuencial_Venta);

            if (venta == null)
            {
                V_Menu_Principal.MSG.ShowMSG("No se encontró la factura en la base de datos.", "Error");
                return;
            }

            if (venta.Documento == null || venta.Documento.Length == 0)
            {
                V_Menu_Principal.MSG.ShowMSG("La factura no tiene documento PDF asociado.", "Error");
                return;
            }

            // Buscar cliente
            var destinatariocliente = context.Clientes.FirstOrDefault(c => c.Secuencial == Secuencial_Cliente);
            string? destinatario = destinatariocliente?.Email;

            if (string.IsNullOrWhiteSpace(destinatario))
            {
                V_Menu_Principal.MSG.ShowMSG("El cliente no tiene un correo electrónico registrado.", "Aviso");
                return;
            }

            // Enviar correo con PDF en memoria
            Util.EnviarCorreoConPdfBytes("monitux.pos@gmail.com",
                destinatario,
                $"{V_Menu_Principal.Nombre_Empresa} - Comprobante",
                "Gracias por su compra. Adjunto tiene su comprobante.",
                venta.Documento,
                "smtp.gmail.com",
                587,
                "monitux.pos", "ffeg qqnx zaij otmb");
            V_Menu_Principal.MSG.ShowMSG("Se envio la factura correctamente.", "Exito");


        }

        private void button3_Click(object sender, EventArgs e)
        {


            try
            {
                // Validar selección del cliente
                if (comboCliente.SelectedItem == null)
                {
                    V_Menu_Principal.MSG.ShowMSG("Por favor, selecciona un cliente antes de continuar.", "Aviso");
                    return;
                }

                string clienteTexto = comboCliente.SelectedItem.ToString();
                int indice = clienteTexto.IndexOf("- ");
                if (indice == -1)
                {
                    V_Menu_Principal.MSG.ShowMSG("El formato del cliente no es válido.", "Error");
                    return;
                }

                string nombreCliente = clienteTexto.Substring(indice + 2).Trim();

                using var context = new Monitux_DB_Context();
                var venta = context.Ventas.FirstOrDefault(v =>
                    v.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa &&
                    v.Secuencial == Secuencial_Venta);

                if (venta == null)
                {
                    V_Menu_Principal.MSG.ShowMSG("No se encontró la factura en la base de datos.", "Error");
                    return;
                }

                if (venta.Documento == null || venta.Documento.Length == 0)
                {
                    V_Menu_Principal.MSG.ShowMSG("La factura no tiene documento PDF asociado.", "Error");
                    return;
                }

                var visor = new V_Visor_Factura
                {
                    DocumentoEnBytes = venta.Documento,
                    titulo = $"Factura de Venta No. {venta.Secuencial}"
                };
                visor.ShowDialog();
            }
            catch (Exception ex)
            {
                V_Menu_Principal.MSG.ShowMSG($"Se produjo un error inesperado:\n{ex.Message}", "Error");
            }




        }

        private void button5_Click(object sender, EventArgs e)
        {

            try
            {
                using var context = new Monitux_DB_Context();
                context.Database.EnsureCreated();

                // Validar proveedor seleccionado
                if (comboProveedor.SelectedItem == null)
                {
                    V_Menu_Principal.MSG.ShowMSG("Por favor, selecciona un proveedor antes de continuar.", "Aviso");
                    return;
                }

                string proveedorTexto = comboProveedor.SelectedItem.ToString();
                int indice = proveedorTexto.IndexOf("- ");
                if (indice == -1)
                {
                    V_Menu_Principal.MSG.ShowMSG("El formato del proveedor no es válido.", "Error");
                    return;
                }

                string nombreProveedor = proveedorTexto.Substring(indice + 2).Trim();

                // Buscar la compra en la base de datos
                var compra = context.Compras.FirstOrDefault(c =>
                    c.Secuencial == Secuencial_Compra &&
                    c.Secuencial_Empresa == V_Menu_Principal.Secuencial_Empresa);

                if (compra?.Documento == null || compra.Documento.Length == 0)
                {
                    V_Menu_Principal.MSG.ShowMSG("No se encontró el documento PDF para esta compra.", "Archivo no encontrado");
                    return;
                }

                // Mostrar visor de factura desde memoria
                var visor = new V_Visor_Factura
                {
                    DocumentoEnBytes = compra.Documento,
                    titulo = $"Factura de Compra No. {compra.Secuencial}"
                };
                visor.ShowDialog();
            }
            catch (Exception ex)
            {
                V_Menu_Principal.MSG.ShowMSG($"Ha ocurrido un error inesperado:\n{ex.Message}", "Error");
            }




        }

        private void button1_Click(object sender, EventArgs e)
        {




            Lista.Clear();
            V_Editar_Factura_Venta.Lista_de_Items.Clear();
            if (dataGridView1.Rows.Count == 0)
            {
                V_Menu_Principal.MSG.ShowMSG("No hay factura seleccionada para modificar.", "Error");
                return;
            }


            cliente_seleccionado = comboCliente.SelectedItem.ToString();


            Lista.Clear(); // Limpiar la lista antes de importar
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (!row.IsNewRow && row.Cells["Codigo"].Value != null && row.Cells["Cantidad"].Value != null)
                {
                    string codigo = row.Cells["Codigo"].Value.ToString();

                    if (!Lista.ContainsKey(codigo) && double.TryParse(row.Cells["Cantidad"].Value.ToString(), out double cantidad))
                    {
                        Lista.Add(codigo, cantidad);
                    }
                }
            }


            if (Secuencial_Venta == 0)
            {

                V_Menu_Principal.MSG.ShowMSG("Seleccione Factura", "Monitux-POS");
                return;
            }

            V_Editar_Factura_Venta v_Editar_Factura_Venta = new V_Editar_Factura_Venta();

            v_Editar_Factura_Venta.Secuencial_Cliente = Secuencial_Cliente;
            v_Editar_Factura_Venta.Secuencial_Venta = Secuencial_Venta;
            v_Editar_Factura_Venta.Secuencial_Empresa = V_Menu_Principal.Secuencial_Empresa;





            v_Editar_Factura_Venta.ShowDialog();

            Filtrar_Venta(int.Parse(comboCliente.SelectedItem.ToString().Split('-')[0].Trim()));
            Filtrar_Detalle_Venta();



        }

        private void button6_Click(object sender, EventArgs e)
        {



            Lista.Clear();
            V_Editar_Factura_Compra.Lista_de_Items.Clear();

            if (dataGridView3.Rows.Count == 0)
            {
                V_Menu_Principal.MSG.ShowMSG("No hay factura seleccionada para modificar.", "Error");
                return;
            }

            // ✅ Asignar Secuencial_Proveedor correctamente desde el combo
            string proveedorSeleccionado = comboProveedor.SelectedItem.ToString();
            Secuencial_Proveedor = int.Parse(proveedorSeleccionado.Split('-')[0].Trim());

            proveedor_seleccionado = proveedorSeleccionado;

            // ✅ Importar productos y cantidades desde dataGridView4
            Lista.Clear();
            foreach (DataGridViewRow row in dataGridView4.Rows)
            {
                if (!row.IsNewRow && row.Cells["Codigo"].Value != null && row.Cells["Cantidad"].Value != null)
                {
                    string codigo = row.Cells["Codigo"].Value.ToString();

                    if (!Lista.ContainsKey(codigo) && double.TryParse(row.Cells["Cantidad"].Value.ToString(), out double cantidad))
                    {
                        Lista.Add(codigo, cantidad);
                    }
                }
            }

         

            if (Secuencial_Compra == 0)
            {
                V_Menu_Principal.MSG.ShowMSG("Seleccione Factura", "Monitux-POS");
                return;
            }

            V_Editar_Factura_Compra v_Editar_Factura_Compra = new V_Editar_Factura_Compra();

            v_Editar_Factura_Compra.Secuencial_Proveedor = Secuencial_Proveedor;
            v_Editar_Factura_Compra.Secuencial_Compra = Secuencial_Compra;
            v_Editar_Factura_Compra.Secuencial_Empresa = V_Menu_Principal.Secuencial_Empresa;

            v_Editar_Factura_Compra.ShowDialog();

            // ✅ Refresca vista
            Filtrar_Compra(int.Parse(comboProveedor.SelectedItem.ToString().Split('-')[0].Trim()));
            Filtrar_Detalle_Compra();




        }
    }
}
