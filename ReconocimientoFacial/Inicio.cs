using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GLOBALSistemas.GeneralTools;
using Services = GLOBALSistemas.ServiceConsumers;

namespace WindowsFormsApp1
{
	public partial class Inicio : Form
	{
		public Inicio()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{

        }

		private void cmdVerificar_Click(object sender, EventArgs e)
		{
			var frm = new CedulaEcuador();
			frm.VerificarIdentidad(txtCedula.Text, txtCodDactilar.Text);
		}
	}
}
