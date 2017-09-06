namespace WindowsFormsApp1
{
	partial class Inicio
	{
		/// <summary>
		/// Variable del diseñador necesaria.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Limpiar los recursos que se estén usando.
		/// </summary>
		/// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Código generado por el Diseñador de Windows Forms

		/// <summary>
		/// Método necesario para admitir el Diseñador. No se puede modificar
		/// el contenido de este método con el editor de código.
		/// </summary>
		private void InitializeComponent()
		{
			this.label11 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.txtCodDactilar = new System.Windows.Forms.TextBox();
			this.txtCedula = new System.Windows.Forms.TextBox();
			this.cmdVerificar = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label11.Location = new System.Drawing.Point(26, 96);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(187, 32);
			this.label11.TabIndex = 110;
			this.label11.Text = "Cód. Dactilar:";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(26, 42);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(174, 32);
			this.label1.TabIndex = 109;
			this.label1.Text = "Nro. Cédula:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtCodDactilar
			// 
			this.txtCodDactilar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtCodDactilar.Location = new System.Drawing.Point(255, 93);
			this.txtCodDactilar.Name = "txtCodDactilar";
			this.txtCodDactilar.Size = new System.Drawing.Size(433, 39);
			this.txtCodDactilar.TabIndex = 108;
			// 
			// txtCedula
			// 
			this.txtCedula.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtCedula.Location = new System.Drawing.Point(255, 39);
			this.txtCedula.Name = "txtCedula";
			this.txtCedula.Size = new System.Drawing.Size(433, 39);
			this.txtCedula.TabIndex = 107;
			// 
			// cmdVerificar
			// 
			this.cmdVerificar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmdVerificar.Location = new System.Drawing.Point(448, 164);
			this.cmdVerificar.Name = "cmdVerificar";
			this.cmdVerificar.Size = new System.Drawing.Size(251, 59);
			this.cmdVerificar.TabIndex = 111;
			this.cmdVerificar.Text = "Verificar identidad";
			this.cmdVerificar.UseVisualStyleBackColor = true;
			this.cmdVerificar.Click += new System.EventHandler(this.cmdVerificar_Click);
			// 
			// Inicio
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(732, 249);
			this.Controls.Add(this.cmdVerificar);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtCodDactilar);
			this.Controls.Add(this.txtCedula);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Inicio";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "  Verificación de Identidad Ecuatoriana";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtCodDactilar;
		private System.Windows.Forms.TextBox txtCedula;
		private System.Windows.Forms.Button cmdVerificar;
	}
}

