/*
 * Created by SharpDevelop.
 * User: arcej
 * Date: 16/09/2012
 * Time: 01:39 p.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace Consultorio
{
	/// <summary>
	/// Description of Imagen.
	/// </summary>
	public class Imagen : Form
	{
		string dir;
		//String[] files;
		string documento, nombre, apellido;
		Label lblImagenes;
		Button btnSalir;
		Button btnAgregar;
		Button btnBorrar;
		ListBox lbxImagen;
		PictureBox pbox;
		
		public Imagen(string apellido, string nombre, string documento)
		{
			this.apellido = apellido;
			this.nombre = nombre;
			this.documento = documento;
			dir = Configuracion.getDirectorioImagenes();
			
			this.Width = 600;
			this.Height = 600;
			this.Text = "Imágenes " + apellido + ", " + nombre;
			
			lblImagenes = new Label();
			lblImagenes.Text = "Archivos de imágenes del paciente: " + apellido + ", " + nombre;
			lblImagenes.Location = new Point(10,10);
			lblImagenes.ForeColor = Color.DarkBlue;
			lblImagenes.AutoSize = true;

			btnSalir = new Button();
			btnSalir.Text = "Salir";
			btnSalir.Location = new Point(500,93);
			btnSalir.Click += new EventHandler(btnSalir_Click);
			
			btnAgregar = new Button();
			btnAgregar.Text = "Agregar";
			btnAgregar.Location = new Point(500,33);
			btnAgregar.Click += new EventHandler(btnAgregar_Click);
			
			btnBorrar = new Button();
			btnBorrar.Text = "Borrar";
			btnBorrar.Location = new Point(500,63);
			btnBorrar.Click += new EventHandler(btnBorrar_Click);

			//btnSalir = new Button();
			//btnSalir.Text = "Salir";
			//btnSalir.Location = new Point(500,93);
			//btnSalir.Click += new System.EventHandler(btnSalir_Click);
			
			lbxImagen = new ListBox();
			lbxImagen.Location = new Point(10,33);
			lbxImagen.Size = new Size(400,100);
			foreach (string s in getFiles())
				lbxImagen.Items.Add(s.Substring(s.LastIndexOf("\\") + 1));
			lbxImagen.DoubleClick += new EventHandler(lbxImagen_DoubleClick);
			
			pbox = new PictureBox();
			pbox.Location = new Point(10,140);
			pbox.Size = new Size(560,400);
			pbox.Visible = true;
			pbox.MouseDoubleClick += new MouseEventHandler(pbox_MouseDoubleClick);
			pbox.BorderStyle = BorderStyle.Fixed3D;
			
			this.Controls.Add(lblImagenes);
			this.Controls.Add(btnSalir);
			this.Controls.Add(btnAgregar);
			this.Controls.Add(btnBorrar);
			this.Controls.Add(lbxImagen);
			this.Controls.Add(pbox);
			
			this.ShowDialog();
		}
		
		public string[] getFiles() {
			string[] files = Directory.GetFiles(dir, documento + "*");
			return files;
		}
		
		private void btnSalir_Click(Object sender, System.EventArgs e)
		{
			this.Close();
		}
		
		private void lbxImagen_DoubleClick(Object sender, System.EventArgs e)
		{
			Image image = Image.FromFile(Configuracion.getDirectorioImagenes() + "\\" + lbxImagen.SelectedItem.ToString());
			pbox.Image = image;
		}

		private void btnAgregar_Click(Object sender, System.EventArgs e)
		{
			FileDialog imf = new OpenFileDialog();
			imf.Title = "Selección de imagen para el paciente " + apellido + ", "+ nombre;
			DialogResult result = imf.ShowDialog();
			if ( result == DialogResult.OK ) {
				String imagefile = imf.FileName.Substring(imf.FileName.LastIndexOf("\\")+1);
				File.Copy(imf.FileName, Configuracion.getDirectorioImagenes() + "\\" + documento + "_" + imagefile);
				lbxImagen.Items.Clear();
				foreach (string s in getFiles())
					lbxImagen.Items.Add(s.Substring(s.LastIndexOf("\\") + 1));
			}
		}

		private void btnBorrar_Click(Object sender, System.EventArgs e)
		{
			DialogResult r = MessageBox.Show("¿Desea eliminar la imagen?", lbxImagen.SelectedItem.ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button1);
	    	if ( r == DialogResult.Yes ) {
				pbox.Image.Dispose();
				pbox.Image = null;
				pbox.Refresh();
				File.Delete(Configuracion.getDirectorioImagenes() + "\\" + lbxImagen.SelectedItem.ToString());
	    	}
			lbxImagen.Items.Clear();
			foreach (string s in getFiles())
				lbxImagen.Items.Add(s.Substring(s.LastIndexOf("\\") + 1));
		}
		
		private void pbox_MouseDoubleClick(Object sender, System.EventArgs e) {
			String file = Configuracion.getDirectorioImagenes() + "\\" + lbxImagen.SelectedItem.ToString();
			System.Diagnostics.Process.Start( "mspaint.exe", file);
		}
			
	}
}
