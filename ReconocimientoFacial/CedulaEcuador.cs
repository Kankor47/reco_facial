using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GLOBALSistemas.GeneralTools;
using Services = GLOBALSistemas.ServiceConsumers;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace WindowsFormsApp1
{
    
	public partial class CedulaEcuador : Form
	{
		private Color color_alerta = Color.Pink;
		private string img_path = Utils.AppPath + "tmp\\";
		private bool img_captured = false;
        public string nombreArchivo = null;
        SqlCommand cmd;

		public CedulaEcuador()
		{
			InitializeComponent();
            nombreArchivo = ControlRF.generarNombreArchivo();
		}

		public void VerificarIdentidad(string nro_cedula, string cod_dactilar)
		{
			try
			{
				if (nro_cedula.IsEmpty())
					throw new ArgumentNullException("nro_cedula");
				else
					txtCedula.Text = nro_cedula.Trim();

				if (cod_dactilar.IsEmpty())
					throw new ArgumentNullException("cod_dactilar");
				else
					txtCodDactilar.Text = cod_dactilar.Trim();

				Show();
				VerificarDatosCedula();
				CapturarFotografia();
				if (img_captured) VerificarFacial();
			}
			catch (ArgumentNullException ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			catch (Exception) { }
		}

		private void CapturarFotografia()
		{
			try
			{
				using (var frm = new CapturaCamara())
				{
					frm.ImgPath = img_path;
					frm.ImgCaptured = false;
					frm.ShowDialog(this);
					img_captured = frm.ImgCaptured;
					if (frm.ImgCaptured)
						FotoCamara.ImageLocation = img_path + "captura.bmp";
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				throw ex;
			}
		}

		private void VerificarDatosCedula()
		{
			try
			{
				Cursor = Cursors.WaitCursor;

				DateTime?
					fechaCedulacion = null,
					fechaDefuncion = null,
					fechaNacimiento = null;

				using (var req = new Services.RegistroCivil.Request(true))
				{
					var res = req.ConsultaPorCedula(txtCedula.Text, txtCodDactilar.Text, img_path);
					txtNombre.BackColor = Control.DefaultBackColor;
					txtNombre.Text = res.Persona.Nombre;
					txtCalle.Text = res.Persona.Calle;
					txtCondicionCedula.Text = res.Persona.CondicionCedulado;
					txtConyuge.Text = res.Persona.Conyuge;
					txtDomicilio.Text = res.Persona.Domicilio;
					txtEstadoCivil.Text = res.Persona.EstadoCivil;
					txtGenero.Text = res.Persona.Genero;
					txtInstruccion.Text = res.Persona.Instruccion;
					txtNacionalidad.Text = res.Persona.Nacionalidad;
					txtNombresMadre.Text = res.Persona.NombreMadre;
					txtNombresPadre.Text = res.Persona.NombrePadre;
					txtNroCasa.Text = res.Persona.NumeroCasa;
					txtProfesion.Text = res.Persona.Profesion;
					FotoCedula.ImageLocation = res.FotografiaUrl;
					Firma.ImageLocation = res.FirmaUrl;

					// formatear fechas
					fechaCedulacion = Utils.GetDate(res.Persona.FechaCedulacion);
					fechaDefuncion = Utils.GetDate(res.Persona.FechaDefuncion);
					fechaNacimiento = Utils.GetDate(res.Persona.FechaNacimiento);

					if (fechaCedulacion.HasValue)
						txtFechaCedulacion.Text = fechaCedulacion.Value.ToString("dd - MMMM - yyyy");

					if (fechaDefuncion.HasValue)
						txtFechaDefuncion.Text = fechaDefuncion.Value.ToString("dd - MMMM - yyyy");

					if (fechaNacimiento.HasValue)
						txtFechaNacimiento.Text = fechaNacimiento.Value.ToString("dd - MMMM - yyyy");
				}

				// fecha cedulacion debe ser menor o igual a fecha actual
				if (fechaCedulacion.HasValue && fechaCedulacion.Value.CompareTo(DateTime.Now) > 0)
					txtFechaCedulacion.BackColor = color_alerta;

				// no debe ser fallecido
				if (txtCondicionCedula.Text.ToLower().StartsWith("fallecid"))
					txtCondicionCedula.BackColor = color_alerta;

				// no puede ser menor de edad
				if (fechaNacimiento.HasValue && fechaNacimiento.Value.CompareTo(DateTime.Now.AddYears(-18)) > 0)
				{
					txtCondicionCedula.BackColor = color_alerta;
					txtFechaNacimiento.BackColor = color_alerta;
					txtFechaNacimiento.Text += "   (Menor de Edad)";
				}

				// alerta de nacionalidad
				if (txtNacionalidad.Text.ToLower().StartsWith("ecuator") == false)
					txtNacionalidad.BackColor = color_alerta;

				Cursor = Cursors.Default;
			}
			catch (Exception ex)
			{
				Cursor = Cursors.Default;
				var error = ex.Message.Replace("(Error-RCivil)", "");
				txtNombre.Text = error;
				txtNombre.BackColor = color_alerta;
				MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				throw ex;
			}
		}

		private void VerificarFacial()
		{
			try
			{
				Cursor = Cursors.WaitCursor;

				string
					faceId1 = "",
					faceId2 = "";

				using (var req = new Services.CognitiveServices.Request())
				{
					var res = req.FaceDetect(FotoCedula.ImageLocation, Services.CognitiveServices.FaceAttributesEnum.All);
					if (res.FaceId.IsEmpty())
						throw new Exception("No hay rostro en la fotografía del Registro Civil");
					else
						faceId1 = res.FaceId;

					res = req.FaceDetect(img_path + "captura.bmp", Services.CognitiveServices.FaceAttributesEnum.All);
					if (res.FaceId.IsEmpty())
						throw new Exception("No hay rostro en la fotografía de la cámara");
					else
						faceId2 = res.FaceId;

                    txtResultadoFacial.Text = "ES IDÉNTICO";
				}

				using (var req = new Services.CognitiveServices.Request(true))
				{
                    var res1 = req.FaceDetect(FotoCedula.ImageLocation, Services.CognitiveServices.FaceAttributesEnum.All);
                    var res = req.FaceVerify(faceId1, faceId2);
					if (res.IsIdentical == false)
					{
						txtNombre.BackColor = color_alerta;
						txtNombre.Text += " - NO ES IDÉNTICO";
                        txtResultadoFacial.Text = "Edad: "+res1.FaceAttributes.Age + " Años \n" + "\nGénero: "
                            +res1.FaceAttributes.Gender + "\nNO ES IDENTICO";
					}
					else
					{
						txtNombre.BackColor = Control.DefaultBackColor;
						txtNombre.Text = txtNombre.Text.Replace(" - NO ES IDÉNTICO", "SI ES IDENTICO");
                    }

					//txtResultadoFacial.Text += res.ToString();
                    
				}
               

                Cursor = Cursors.Default;
			}
			catch (Exception ex)
			{
				Cursor = Cursors.Default;
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

        MemoryStream ms;

        void conv_foto()
        {
            if (FotoCedula.Image != null)
            {
                ms = new MemoryStream();
                FotoCedula.Image.Save(ms, ImageFormat.Jpeg);
                byte[] photo_aray = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(photo_aray, 0, photo_aray.Length);
                cmd.Parameters.AddWithValue("@foto", photo_aray);
            }
        }

        void conv_firma()
        {
            if (FotoCedula.Image != null)
            {
                ms = new MemoryStream();
                FotoCedula.Image.Save(ms, ImageFormat.Jpeg);
                byte[] photo_aray = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(photo_aray, 0, photo_aray.Length);
                cmd.Parameters.AddWithValue("@firma", photo_aray);
            }
        }

        private void cmdVerificar_Click(object sender, EventArgs e)
		{
			CapturarFotografia();
			if (img_captured) VerificarFacial();
		}

		private void txtCedula_Enter(object sender, EventArgs e)
		{
			txtCedula.SelectAll();
		}

		private void txtCodDactilar_Enter(object sender, EventArgs e)
		{
			txtCodDactilar.SelectAll();
		}

        private void CedulaEcuador_Load(object sender, EventArgs e)
        {

        }

        public static int insertar_dato(CedulaEcuador ce)
        {
            using (var req = new Services.CognitiveServices.Request(true))
            {
                var cedula = ce.txtCedula.Text.Trim();
                var foto = ce.FotoCedula.ImageLocation.Trim();
                var firma = ce.Firma.ImageLocation.Trim();
                
                int retorno = 0;
                using (SqlConnection conectar = ConectarBD.ObtenerConexion())
                {
                    SqlCommand cmd = new SqlCommand("insert into datos (cedula,foto,firma) values(@cedula,@foto,@firma)", conectar);

                    cmd.Parameters.Add("@cedula", SqlDbType.VarChar, 10).Value = cedula;
                    cmd.Parameters.Add("@foto", SqlDbType.VarChar, 100).Value = foto;
                    cmd.Parameters.Add("@firma", SqlDbType.VarChar, 100).Value = firma;

                    retorno = cmd.ExecuteNonQuery();
                    
                }
                //var res = req.FaceDetect(fot, Services.CognitiveServices.FaceAttributesEnum.All);

                //cedula = ce.txtCedula.Text.Trim();

                ////tabla face
                //var FaceID = res.FaceId;

                

                ////tabla atributos
                //var genero = res.FaceAttributes.Gender;
                //var edad = res.FaceAttributes.Age;
                //var barba = res.FaceAttributes.FacialHair;
                //var barba1 = res.FaceAttributes.FacialHair;
                //var barba2 = res.FaceAttributes.FacialHair.Beard;
                //var barba3 = res.FaceAttributes.FacialHair.Sideburns;
                //var gafas = res.FaceAttributes.Glasses;
                //var cabeza = res.FaceAttributes.HeadPose;
                //var cabeza1 = res.FaceAttributes.HeadPose.Pitch;
                //var cabeza2 = res.FaceAttributes.HeadPose.Roll;
                //var cabeza3 = res.FaceAttributes.HeadPose.Yaw;
                //var sonrisa = res.FaceAttributes.Smile;

                ////tabla referencia
                //var EyebrowLeftInner = res.FaceLandmarks.EyebrowLeftInner.X;
                //var EyebrowLeftInner2 = res.FaceLandmarks.EyebrowLeftInner.Y;
                //var EyebrowLeftOuter = res.FaceLandmarks.EyebrowLeftOuter.X;
                //var EyebrowLeftOuter2 = res.FaceLandmarks.EyebrowLeftOuter.Y;
                //var EyebrowRightInner = res.FaceLandmarks.EyebrowRightInner.X;
                //var EyebrowRightInner2 = res.FaceLandmarks.EyebrowRightInner.Y;
                //var EyebrowRightOuter = res.FaceLandmarks.EyebrowRightOuter.X;
                //var EyebrowRightOuter2 = res.FaceLandmarks.EyebrowRightOuter.Y;
                //var EyeLeftBottom = res.FaceLandmarks.EyeLeftBottom.X;
                //var EyeLeftBottom2 = res.FaceLandmarks.EyeLeftBottom.Y;
                //var EyeLeftInner = res.FaceLandmarks.EyeLeftInner.X;
                //var EyeLeftInner2 = res.FaceLandmarks.EyeLeftInner.Y;
                //var EyeLeftOuter = res.FaceLandmarks.EyeLeftOuter.X;
                //var EyeLeftOuter2 = res.FaceLandmarks.EyeLeftOuter.Y;
                //var EyeLeftTop = res.FaceLandmarks.EyeLeftTop.X;
                //var EyeLeftTop2 = res.FaceLandmarks.EyeLeftTop.Y;
                //var EyeRightBottom = res.FaceLandmarks.EyeRightBottom.X;
                //var EyeRightBottom2 = res.FaceLandmarks.EyeRightBottom.Y;
                //var EyeRightInner = res.FaceLandmarks.EyeRightInner.X;
                //var EyeRightInner2 = res.FaceLandmarks.EyeRightInner.Y;
                //var EyeRightOuter = res.FaceLandmarks.EyeRightOuter.X;
                //var EyeRightOuter2 = res.FaceLandmarks.EyeRightOuter.Y;
                //var EyeRightTop = res.FaceLandmarks.EyeRightTop.X;
                //var EyeRightTop2 = res.FaceLandmarks.EyeRightTop.Y;
                //var MouthLeft = res.FaceLandmarks.MouthLeft.X;
                //var MouthLeft2 = res.FaceLandmarks.MouthLeft.Y;
                //var MouthRight = res.FaceLandmarks.MouthRight.X;
                //var MouthRight2 = res.FaceLandmarks.MouthRight.Y;
                //var NoseLeftAlarOutTip = res.FaceLandmarks.NoseLeftAlarOutTip.X;
                //var NoseLeftAlarOutTip2 = res.FaceLandmarks.NoseLeftAlarOutTip.Y;
                //var NoseLeftAlarTop = res.FaceLandmarks.NoseLeftAlarTop.X;
                //var NoseLeftAlarTop2 = res.FaceLandmarks.NoseLeftAlarTop.Y;
                //var NoseRightAlarOutTip = res.FaceLandmarks.NoseRightAlarOutTip.X;
                //var NoseRightAlarOutTip2 = res.FaceLandmarks.NoseRightAlarOutTip.Y;
                //var NoseRightAlarTop = res.FaceLandmarks.NoseRightAlarTop.X;
                //var NoseRightAlarTop2 = res.FaceLandmarks.NoseRightAlarTop.Y;
                //var NoseRootLeft = res.FaceLandmarks.NoseRootLeft.X;
                //var NoseRootLeft2 = res.FaceLandmarks.NoseRootLeft.Y;
                //var NoseRootRight = res.FaceLandmarks.NoseRootRight.X;
                //var NoseRootRight2 = res.FaceLandmarks.NoseRootRight.Y;
                //var NoseTip = res.FaceLandmarks.NoseTip.X;
                //var NoseTip2 = res.FaceLandmarks.NoseTip.Y;
                //var PupilLeft = res.FaceLandmarks.PupilLeft.X;
                //var PupilLeft2 = res.FaceLandmarks.PupilLeft.Y;
                //var PupilRight = res.FaceLandmarks.PupilRight.X;
                //var PupilRight2 = res.FaceLandmarks.PupilRight.Y;
                //var UnderLipBottom = res.FaceLandmarks.UnderLipBottom.X;
                //var UnderLipBottom2 = res.FaceLandmarks.UnderLipBottom.Y;
                //var UnderLipTop = res.FaceLandmarks.UnderLipTop.X;
                //var UnderLipTop2 = res.FaceLandmarks.UnderLipTop.Y;

                return retorno;
            }
        }

        public static int insertar_facial()
        {
            CedulaEcuador ce= new CedulaEcuador();
            var fot = ce.FotoCedula.ImageLocation;

            using (var req = new Services.CognitiveServices.Request(true))
            {
                var res = req.FaceDetect(fot, Services.CognitiveServices.FaceAttributesEnum.All);

                var alto = res.FaceRectangle.Top;
                var ancho = res.FaceRectangle.Width;
                var posx = res.FaceRectangle.Height;
                var posy = res.FaceRectangle.Left;

                int retorno = 0;
                using (SqlConnection conectar = ConectarBD.ObtenerConexion())
                {

                    SqlCommand cmd = new SqlCommand("insert into facial (alto,ancho,posx,posy) values(@alto,@ancho,@posx,@posy)", conectar);

                    cmd.Parameters.Add("@alto", SqlDbType.Int).Value = alto;
                    cmd.Parameters.Add("@ancho", SqlDbType.Int).Value = ancho;
                    cmd.Parameters.Add("@posx", SqlDbType.Int).Value = posx;
                    cmd.Parameters.Add("@posy", SqlDbType.Int).Value = posy;

                    retorno = cmd.ExecuteNonQuery();
                    if (retorno > 0)
                    {
                        MessageBox.Show("Registro fue Grabado Correctamente", "Mensaje de Exito", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        MessageBox.Show("Registro No fue Grabado", "Mensaje de Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                return retorno;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CedulaEcuador ce = new CedulaEcuador();
            ce.txtCedula.Text=txtCedula.Text;
            ce.FotoCedula.ImageLocation = FotoCedula.ImageLocation;
            ce.Firma.ImageLocation = Firma.ImageLocation;
            int resul = insertar_dato(ce);
            insertar_facial();

            if (resul > 0)
            {
                MessageBox.Show("Registro fue Grabado Correctamente", "Mensaje de Exito", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                MessageBox.Show("Registro No fue Grabado", "Mensaje de Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            ConectarBD.CerrarConexion();
        }
    }


}
