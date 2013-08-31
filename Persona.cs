/*
 * Created by SharpDevelop.
 * User: arcej
 * Date: 20/08/2012
 * Time: 04:50 p.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace Consultorio
{
	/// <summary>
	/// Description of Persona.
	/// </summary>
	public class Persona
	{
		string _nombre;
		string _apellido;
		string _fechanac;
		string _tipodoc;
		string _documento;
		string _telefono;
		string _sexo;
		string _email;
		
		public Persona() {}
		
		public Persona(string apellido, string nombre, string fechanac, string tipodoc, string documento, string telefono, string sexo, string email)
		{
			_nombre = nombre;
			_apellido = apellido;
			_fechanac = fechanac;
			_tipodoc = tipodoc;
			_documento = documento;
			_telefono = telefono;
			_sexo = sexo;
			_email = email;
		}
		
		public string apellido {
			get { return _apellido; }
			set { _apellido = value; }
		}
		public string nombre {
			get { return _nombre; }
			set { _nombre = value; }
		}
		public string fechanac {
			get { return _fechanac; }
			set { _fechanac = value; }
		}
		public string tipodoc {
			get { return _tipodoc; }
			set { _tipodoc = value; }
		}
		public string documento {
			get { return _documento; }
			set { _documento = value; }
		}
		public string telefono {
			get { return _telefono; }
			set { _telefono = value; }
		}
		public string sexo {
			get { return _sexo; }
			set { _sexo = value; }
		}
		public string email {
			get { return _email; }
			set { _email = value; }
		}
		
		public void guardar() {
			string directorio = Configuracion.getDirectorioPacientes();
			String[] data = {
				"Apellido:" + _apellido,
				"Nombre:" + _nombre,
				"Fecha de nacimiento:" + _fechanac,
				"Tipo documento:" + _tipodoc,
				"Documento:" + _documento,
				"Teléfono:" + _telefono,
				"Sexo:" + _sexo,
				"Email:" + _email};

			if (File.Exists(directorio + "\\" + _documento + ".dat")) {
			    	DialogResult r = MessageBox.Show("¿Desea reemplazarlo?", _documento+".dat ya existe!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button1);
			    	if ( r == DialogResult.Yes ) {
			    		File.WriteAllLines(directorio + "\\" + _documento + ".dat", data);
			    	}
			 } 
			 else {
		    		File.WriteAllLines(directorio + "\\" + _documento + ".dat", data);
			 }
		}
		
		public Persona cargar(string filename) {
			String[] lineas = buscar(filename);
			MessageBox.Show("cargar():" + lineas.Length);
			Persona p = new Persona();
			if (lineas.Length > 0 )
			{
				foreach(String line in lineas) {
					string campo = line.Substring(0, line.IndexOf(":"));
					MessageBox.Show(campo + "-" + line);
					switch (campo) {
						case "Apellido" : { p._apellido = line.Substring(line.IndexOf(":")+1);break;}
						case "Nombre" : { p._nombre = line.Substring(line.IndexOf(":")+1);break;}
						case "Fecha de nacimiento" : { p._fechanac = line.Substring(line.IndexOf(":")+1);;break;}
						case "Tipo documento" : { p._tipodoc = line.Substring(line.IndexOf(":")+1);break;}
						case "Documento" : { p._documento = line.Substring(line.IndexOf(":")+1);break;}
						case "Teléfono" : { p._telefono = line.Substring(line.IndexOf(":")+1);break;}
						case "Sexo" : { p._sexo = line.Substring(line.IndexOf(":")+1);break;}
						case "Email" : { p._email = line.Substring(line.IndexOf(":")+1); break;}
					}
				}
				return p;
			}
			else {
				return null;
			}
		}
		
		public static void borrar(string archivo) {
			if (File.Exists(Configuracion.getDirectorio() + "\\Pacientes\\" + archivo + ".dat")) {
		    	DialogResult r = MessageBox.Show("¿Desea eliminarlo?", archivo +".dat existe!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button1);
		    	if ( r == DialogResult.Yes ) {
		    		File.Delete(Configuracion.getDirectorio() + "\\Pacientes\\" + archivo + ".dat");
	    		}
			} else {
				MessageBox.Show("El registro no existe!");
			}
		}
		
		public string[] buscar(string file) {
			string[] data = {};
			if (File.Exists(file)) {
				data = File.ReadAllLines(file);
				return data;
			}
			else {
				MessageBox.Show(file + " no existe!");
				return data;
			}
		}

	}
}
