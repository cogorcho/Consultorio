/*
 * Created by SharpDevelop.
 * User: arcej
 * Date: 20/08/2012
 * Time: 03:09 p.m.
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
	/// Description of Configuracion.
	/// </summary>
	/// 
	
	public class Configuracion
	{
		string archivocfg = "consultorio.cfg";
		string fecha = "";
		static string directorio = "";
		string[] archivos = {};
		static string dirPacientes;
		static string dirBackup;
		static string dirLog;
		static string dirTurnos;
		static string dirImagenes;
		static string dirReportes;
		static string dirHC;

		public Configuracion()
		{
			if (File.Exists(archivocfg)) {
				leerConfiguracion();
			}
			else {
				crearConfiguracion();
                leerConfiguracion();
			}
		}
		
		void leerConfiguracion() {
			string[] lineas = File.ReadAllLines(archivocfg);
			foreach ( string linea in lineas ) {
				int indx = linea.IndexOf(":");
				string marca = linea.Substring(0,indx);
				switch (marca) {
					case "Fecha" : { fecha = linea.Substring(indx+1); break;}
					case "ArchivoCfg" : { archivocfg = linea.Substring(indx+1); break;}
					case "Directorio" : { directorio = linea.Substring(indx+1); break;}
					case "Pacientes" : { dirPacientes = linea.Substring(indx+1); break;}
					case "Log" : { dirLog = linea.Substring(indx+1); break;}
					case "Turnos" : { dirTurnos = linea.Substring(indx+1); break;}
					case "Backup" : { dirBackup = linea.Substring(indx+1); break;}
					case "Imagenes" : { dirImagenes = linea.Substring(indx+1); break;}
					case "Reportes" : { dirReportes = linea.Substring(indx+1); break;}
					case "HC" : { dirHC = linea.Substring(indx+1); break;}
				}
			}
			archivos = Directory.GetFiles(directorio);
		}
		
		public string getFecha() { return fecha; }
		public string getArchivocfg() { return archivocfg; }
		public static string getDirectorio() { return directorio; }
		public static string getDirectorioPacientes() { return dirPacientes; }
		public static string getDirectorioLog() { return dirLog; }
		public static string getDirectorioBackup() { return dirBackup; }
		public static string getDirectorioTurnos() { return dirTurnos; }
		public static string getDirectorioImagenes() { return dirImagenes; }
		public static string getDirectorioReportes() { return dirReportes; }
		public static string getDirectorioHC() { return dirHC; }
		public string[] getArchivos() { return archivos; }
		
		void crearConfiguracion() {
			string[] lineas = { "Fecha:" + DateTime.Now, "ArchivoCfg:"+archivocfg };
			File.WriteAllLines(archivocfg, lineas);
			crearDirectorio();
		}
		
		void crearDirectorio() {
			FolderBrowserDialog fbd = new FolderBrowserDialog();
			fbd.Description = "Creación del directorio para archivos";
			DialogResult result = fbd.ShowDialog();
			directorio = fbd.SelectedPath + "\\" + "Consultorio";
			string[] maslineas = { "Directorio:" + directorio, 
									"Pacientes:" + directorio + "\\Pacientes",
									"Log:"  + directorio + "\\Log",
									"Turnos:" + directorio + "\\Turnos",
									"Backup:" + directorio + "\\Backup",
									"Imagenes:" + directorio + "\\Imagenes",
									"HC:" + directorio + "\\HC",
									"Reportes:" + directorio + "\\Reportes"};
			File.AppendAllLines(archivocfg,maslineas);
			crearSubDirectorios(directorio);
		}
		
		void crearSubDirectorios(String dir) {
			if (!Directory.Exists(dir))
				Directory.CreateDirectory(dir);
			if (!Directory.Exists(dir + "\\Pacientes"))
				Directory.CreateDirectory(dir + "\\Pacientes");
			if (!Directory.Exists(dir + "\\Backup"))
				Directory.CreateDirectory(dir + "\\Backup");
			if (!Directory.Exists(dir + "\\Log"))
				Directory.CreateDirectory(dir + "\\Log");
			if (!Directory.Exists(dir + "\\Turnos"))
				Directory.CreateDirectory(dir + "\\Turnos");
			if (!Directory.Exists(dir + "\\Imagenes"))
				Directory.CreateDirectory(dir + "\\Imagenes");
			if (!Directory.Exists(dir + "\\Reportes"))
				Directory.CreateDirectory(dir + "\\Reportes");
			if (!Directory.Exists(dir + "\\HC"))
				Directory.CreateDirectory(dir + "\\HC");
		}
	}
}
