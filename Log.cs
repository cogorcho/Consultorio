/*
 * Created by SharpDevelop.
 * User: arcej
 * Date: 07/09/2012
 * Time: 09:12 p.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Windows.Forms;

namespace Consultorio
{
	/// <summary>
	/// Description of Log.
	/// </summary>
	public class Log
	{
		public Log(){}
		public static void grabar(string s) {
			DateTime fecha = DateTime.Now;
			string format = "yyyyMMdd";
			string logfile = Configuracion.getDirectorioLog() + "\\" + fecha.ToString(format) + ".log";
			try {
				if (!File.Exists(logfile)) {
					//File.Create(logfile);
					File.AppendAllText(logfile, DateTime.Now + ": Log iniciado" + Environment.NewLine);
				}
				else {
					
					File.AppendAllText(logfile, DateTime.Now + ":" + s + Environment.NewLine);
				}
			} catch (IOException ioe) {
					MessageBox.Show("Log: " + ioe.Message + "(" + s + ")");
			}
		}
	}
}
