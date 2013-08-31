/*
 * Created by SharpDevelop.
 * User: arcej
 * Date: 15/09/2012
 * Time: 12:17 p.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Collections.Generic;

namespace Consultorio
{
	/// <summary>
	/// Description of HC.
	/// </summary>
	public class HC : Form
	{
		string apellido, nombre, tipodoc, documento, telefono, sexo, email;
		Label lblPacienteHC;
		RichTextBox txtHHCC;
		Button btnGuardarHC;
		Button btnSalirHC;
		
		public HC(string apellido, string nombre, string tipodoc, string documento, string telefono, string sexo, string email) 
		{
			this.apellido = apellido;
			this.nombre = nombre;
			this.tipodoc = tipodoc;
			this.documento = documento;
			this.telefono = telefono;
			this.sexo = sexo;
			this.email = email;
			
			lblPacienteHC = new Label();
			txtHHCC = new RichTextBox();
			btnGuardarHC = new Button();
			btnSalirHC = new Button();

			this.Width = 500;
			this.Height = 500;
			this.Text = "Historia Clínica " + apellido + ", " + nombre;
			
			lblPacienteHC.Text = "Historia Clínica " + apellido + ", " + nombre;
			lblPacienteHC.Location = new Point(10,10);
			
			txtHHCC.Multiline = true;
			txtHHCC.Location = new Point(10,25);
			txtHHCC.Width = 450;
			txtHHCC.Height = 395;
			txtHHCC.Font = new Font("Tahoma", 10, FontStyle.Bold);
			
			btnGuardarHC.Text = "Guardar";
			btnGuardarHC.Location = new Point(10,426);
			btnSalirHC.Text = "Salir";
			btnSalirHC.Location = new Point(100,426);
			
			btnSalirHC.Click += new System.EventHandler(BtnSalirHC_Click);
			btnGuardarHC.Click += new System.EventHandler(BtnGuardarHC_Click);

			this.Controls.Add(txtHHCC);
			this.Controls.Add(btnGuardarHC);
			this.Controls.Add(btnSalirHC);
			this.Controls.Add(lblPacienteHC);
		
			if (File.Exists(Configuracion.getDirectorioHC() + "\\" + documento + "_HC.dat")) 
			{
				txtHHCC.Lines = File.ReadAllLines(Configuracion.getDirectorioHC() + "\\" + documento + "_HC.dat");
			}
		
			txtHHCC.Focus();
			txtHHCC.SelectionStart = txtHHCC.Text.Length + 2;

			this.ShowDialog();
		}
		
		private void BtnSalirHC_Click(Object sender, System.EventArgs e)
		{
			this.Close();
		}
		
		private void BtnGuardarHC_Click(Object sender, System.EventArgs e)
		{
			if (txtHHCC.Text.Length > 0) {
				if (File.Exists(Configuracion.getDirectorioHC() + "\\" + documento + "_HC.dat")) 
				{
					DialogResult r = MessageBox.Show("¿Desea reemplazarlo?", documento + "_HC.dat ya existe!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button1);
			    	if ( r == DialogResult.Yes ) {
						List<string> data = new List<string>();
						foreach(string s in txtHHCC.Lines)
							data.Add(s);
						data.Add("-------------------------------------------------");
						data.Add("Ultima entrada de datos: " + DateTime.Now);
						data.Add("-------------------------------------------------" + Environment.NewLine);
						File.WriteAllLines(Configuracion.getDirectorioHC() + "\\" + documento + "_HC.dat", data.ToArray());
			    	}
				}
				else 
				{
					List<string> data = new List<string>();
					foreach(string s in txtHHCC.Lines)
						data.Add(s);
					data.Add("--------------------------------------------------------------------" + Environment.NewLine);
					data.Add("Ultima entrada de datos: " + DateTime.Now + Environment.NewLine);
					data.Add("--------------------------------------------------------------------" + Environment.NewLine);
					File.WriteAllLines(Configuracion.getDirectorioHC() + "\\" + documento + "_HC.dat", data.ToArray());
				}
			}
			this.Close();
		}
	}
}
