/*
 * Created by SharpDevelop.
 * User: arcej
 * Date: 02/09/2012
 * Time: 01:02 p.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Drawing.Printing;

namespace Consultorio
{
	/// <summary>
	/// Description of Reporte.
	/// </summary>
	public class Reporte : Form
	{
		WebBrowser wb;
		Button btnSalir;
		Button btnImprimir;
		String[] hc;
		string documento;
		String html;
		
		public Reporte(string apellido, string nombre, string tipodoc, string documento, string telefono, string sexo, string email)
		{
			this.documento = documento;
			html = "<html>";
			html += "<head><title>Reporte pacientes</title></head>";
			html += "<body>";
			html += "<h1 align=\"center\">Reporte pacientes</h1>";
			html += "<table border=\"1\">";
			html += "<tr>";
			html += "<td> Paciente: </td><td>" + apellido + ", " + nombre + " (" + sexo + ")</td></tr>";
			html += "<td>" + tipodoc + ":</td><td>" + documento + "</td></tr>";
			html += "<td> Teléfono: </td><td>" + telefono + "</td></tr>";
			html += "<td> Email: </td><td>" + email + "</td></tr>";
			html += "</tr>";
			html += "</table>";
			html += "<h2>Historia clínica</h2>";
			html += "<textarea rows=\"14\" cols=\"65\">";
			if (File.Exists(Configuracion.getDirectorioHC() + "\\" + documento + "_HC.dat")) {
				hc = File.ReadAllLines(Configuracion.getDirectorioHC() + "\\" + documento + "_HC.dat");
				foreach ( string s in hc)
					html += s + Environment.NewLine;
			}
			html += "</textarea>";
			html += "</body>";
			html += "</html>";
			
			this.Width = 600;
			this.Height = 550;

			//MessageBox.Show(html);
			wb = new WebBrowser();
			wb.Width = 600;
			wb.Height = 480;
			wb.DocumentText = html;
			
			
			btnSalir = new Button();
			btnImprimir = new Button();
			btnSalir.Text = "Salir";
			btnImprimir.Text = "Imprimir";
			btnImprimir.Location = new System.Drawing.Point(10,484);
			btnSalir.Location = new System.Drawing.Point(100,484);
			btnSalir.Click += new System.EventHandler(btnSalir_Click);
			btnImprimir.Click += new System.EventHandler(btnImprimir_Click);
			this.Controls.Add(wb);
			this.Controls.Add(btnImprimir);
			this.Controls.Add(btnSalir);
			
			this.ShowDialog();
		}
	
		private void btnSalir_Click(Object sender, System.EventArgs e)
		{
			this.Close();
		}
		
		private void btnImprimir_Click(Object sender, System.EventArgs e)
		{
			MessageBox.Show("No está listo!");
		}
	}
}
