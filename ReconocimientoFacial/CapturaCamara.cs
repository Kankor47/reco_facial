using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using WebEye.Controls.WinForms.WebCameraControl;
using GLOBALSistemas.GeneralTools;

namespace WindowsFormsApp1
{
	public partial class CapturaCamara : Form
	{
		private List<WebCameraId> camaras;

		public string ImgPath { get; set; }
		public bool ImgCaptured { get; set; }

		public CapturaCamara()
		{
			InitializeComponent();
		}

		private void CapturaCamara_Load(object sender, EventArgs e)
		{
			try
			{
				camaras = new List<WebCameraId>(webCameraControl1.GetVideoCaptureDevices());
				ListaCamaras.DataSource = camaras;
				ListaCamaras.DisplayMember = "name";
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void ListaCamaras_SelectedIndexChanged(object sender, EventArgs e)
		{
			double
				diagonal_deseada = 500,
				ancho_video = 0,
				alto_video = 0,
				diagonal = 0,
				factor = 0;
			try
			{
				webCameraControl1.StartCapture(camaras[ListaCamaras.SelectedIndex]);

				ancho_video = webCameraControl1.VideoSize.Width;
				alto_video = webCameraControl1.VideoSize.Height;
				diagonal = Math.Sqrt((double)(alto_video * alto_video + ancho_video * ancho_video));
				factor = diagonal_deseada / diagonal;

				this.Width = (int)(ancho_video * factor);
				this.Height = (int)(alto_video * factor) + 40;
				this.CenterToScreen();
			}
			catch (Exception ex)
			{
				webCameraControl1.StopCapture();
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void cmdCapture_Click(object sender, EventArgs e)
		{
			try
			{
				var borde = 3;
				webCameraControl1.Height -= borde * 2;
				webCameraControl1.Width -= borde * 2;
				webCameraControl1.Left = borde;
				webCameraControl1.Top = borde;
				this.Cursor = Cursors.WaitCursor;
				Thread.Sleep(200);

				webCameraControl1.GetCurrentImage().Save(ImgPath + "captura.bmp");
				webCameraControl1.StopCapture();
				ImgCaptured = true;
				this.Close();
			}
			catch (Exception ex)
			{
				this.Cursor = Cursors.Default;
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
