/*
 * Created by SharpDevelop.
 * User: arcej
 * Date: 01/09/2012
 * Time: 10:58 a.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Ionic.Zip;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Consultorio
{
	
	/// Description of Backup.
	/// </summary>
	public class Backup
	{
		public Backup()
		{
		}
		
		public static void doBackup() {
			try {
				List<string> bkpfiles = new List<string>();
				DateTime fecha = DateTime.Now;
				string format = "yyyyMMdd";    // Use this format
				String[] pacientes = Directory.GetFiles(Configuracion.getDirectorioPacientes(),"*.dat");
				String[] hhcc = Directory.GetFiles(Configuracion.getDirectorioHC() ,"*.dat");
				String[] imagenes = Directory.GetFiles(Configuracion.getDirectorioImagenes());
				foreach(string s in pacientes)
					bkpfiles.Add(s);
				foreach(string s in hhcc)
					bkpfiles.Add(s);
				foreach(string s in imagenes)
					bkpfiles.Add(s);
				String backupfile = Configuracion.getDirectorioBackup() + "\\" + "Backup_" + fecha.ToString(format) + ".zip";
				ZipFile zip = new ZipFile();
				zip.AddFiles(bkpfiles.ToArray());
			    zip.Save(backupfile);
			    MessageBox.Show("Backup realizado correctamente.\n" + backupfile);
			   
			} catch (Exception e) {
				MessageBox.Show("Error en el proceso de backup!\n" + e.Message);
			}
		}
	}
}
