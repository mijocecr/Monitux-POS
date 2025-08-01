﻿using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monitux_POS.Ventanas
{
    public partial class V_Captura_Imagen : Form
    {
        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCaptureDevice;
        string URL;
        int secuencial;
        public string titulo;
        public static Bitmap ?Imagen;
        public V_Captura_Imagen(int secuencial,string titulo="Capturando...")
        {
            InitializeComponent();
            this.URL = URL;
            this.secuencial = secuencial;
            this.titulo = titulo;
            label6.Text = titulo;
            

        }





        public V_Captura_Imagen()
        {
            InitializeComponent();
        }

        private void picImage_Click(object sender, EventArgs e)
        {

        }


        private void VideoCaptureDevice_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();

           
            try
            {
                picImagen.Image = bitmap;
            }
            catch (Exception ex)
            {
                V_Menu_Principal.MSG.ShowMSG("Error al capturar la imagen: " + ex.Message, "Error");
            }
            }



        private void V_Captura_Imagen_Load(object sender, EventArgs e)
        {
            this.Text = "Monitux-POS v." + V_Menu_Principal.VER;
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in filterInfoCollection)
                cboCamaras.Items.Add(device.Name);
            cboCamaras.SelectedIndex = 0;

            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[cboCamaras.SelectedIndex].MonikerString);
            videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
            videoCaptureDevice.Start();


        }

        private void V_Captura_Imagen_FormClosing(object sender, FormClosingEventArgs e)
        {
            


            if (videoCaptureDevice != null)
            {
                if (videoCaptureDevice.IsRunning)
                {
                    videoCaptureDevice.SignalToStop();
                    videoCaptureDevice.WaitForStop();
                }
            }


        }





        public static Bitmap Get_Imagen() {


            if (Imagen != null)
            {
                try
                {
                    Bitmap imagenClonada = new Bitmap(Imagen);
                    return imagenClonada;
                }
                catch (Exception ex)
                {
                    V_Menu_Principal.MSG.ShowMSG($"Error al clonar la imagen: {ex.Message}", "Error");
                    return null;
                }
            }
            else
            {
                V_Menu_Principal.MSG.ShowMSG("No hay imagen capturada o el valor es nulo.", "Error");
                return null;
            }



        }

        private void button1_Click(object sender, EventArgs e)
        {

            Imagen = picImagen.Image.Clone() as Bitmap;

            this.Close();


            // this.Close();
        }
    }
}
