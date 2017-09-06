namespace WindowsFormsApp1
{
	partial class CapturaCamara
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.webCameraControl1 = new WebEye.Controls.WinForms.WebCameraControl.WebCameraControl();
			this.ListaCamaras = new System.Windows.Forms.ComboBox();
			this.cmdCapture = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.cmdCapture)).BeginInit();
			this.SuspendLayout();
			// 
			// webCameraControl1
			// 
			this.webCameraControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.webCameraControl1.AutoScroll = true;
			this.webCameraControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.webCameraControl1.Location = new System.Drawing.Point(0, 0);
			this.webCameraControl1.Name = "webCameraControl1";
			this.webCameraControl1.Size = new System.Drawing.Size(674, 432);
			this.webCameraControl1.TabIndex = 7;
			// 
			// ListaCamaras
			// 
			this.ListaCamaras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ListaCamaras.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ListaCamaras.FormattingEnabled = true;
			this.ListaCamaras.Location = new System.Drawing.Point(12, 12);
			this.ListaCamaras.Name = "ListaCamaras";
			this.ListaCamaras.Size = new System.Drawing.Size(286, 33);
			this.ListaCamaras.TabIndex = 10;
			this.ListaCamaras.SelectedIndexChanged += new System.EventHandler(this.ListaCamaras_SelectedIndexChanged);
			// 
			// cmdCapture
			// 
			this.cmdCapture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdCapture.Cursor = System.Windows.Forms.Cursors.Hand;
			this.cmdCapture.Image = global::WindowsFormsApp1.Properties.Resources.Camera_WF;
			this.cmdCapture.Location = new System.Drawing.Point(561, 316);
			this.cmdCapture.Name = "cmdCapture";
			this.cmdCapture.Size = new System.Drawing.Size(90, 90);
			this.cmdCapture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.cmdCapture.TabIndex = 11;
			this.cmdCapture.TabStop = false;
			this.cmdCapture.Click += new System.EventHandler(this.cmdCapture_Click);
			// 
			// CapturaCamara
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Yellow;
			this.ClientSize = new System.Drawing.Size(674, 432);
			this.Controls.Add(this.cmdCapture);
			this.Controls.Add(this.ListaCamaras);
			this.Controls.Add(this.webCameraControl1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CapturaCamara";
			this.Text = "Capturar Fotografia";
			this.Load += new System.EventHandler(this.CapturaCamara_Load);
			((System.ComponentModel.ISupportInitialize)(this.cmdCapture)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private WebEye.Controls.WinForms.WebCameraControl.WebCameraControl webCameraControl1;
		private System.Windows.Forms.ComboBox ListaCamaras;
		private System.Windows.Forms.PictureBox cmdCapture;
	}
}