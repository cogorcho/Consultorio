/*
 * Created by SharpDevelop.
 * User: arcej
 * Date: 12/10/2012
 * Time: 03:52 p.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
//using Finisar.SQLite;
using System.Windows.Forms;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Collections.Generic;

namespace Consultorio
{
	/// <summary>
	/// Description of DBSql.
	/// </summary>
	public class DBSql
	{
		SQLiteConnection conn;
		string db = @"Data Source=C:\Users\arcej\Documents\sqllite\Consultorio.db;Version=3;New=False;Compress=True;";
		string dbFile = @"C:\Users\arcej\Documents\sqllite\Consultorio.db";
		
		public DBSql()
		{
			if (!File.Exists(dbFile))
			{
				SQLiteConnection.CreateFile(dbFile);
				Connect();
				createTables();
				basicData();
				
			}
			else 
			{
				Connect();
			}
		}
		
		private void createTables() {
			MessageBox.Show("createTables");
			CTTipoDocumento();
			CTPersonas();
			CTTipoTelefono();
			CTTelefonos();
		}
		private void basicData() {
			MessageBox.Show("BasicData");
			List<string> tdocs = new List<string>();
			tdocs.Add("insert into TipoDocumento(id,tipodoc) values (1,'DNI')");
			tdocs.Add("insert into TipoDocumento(id,tipodoc) values (2,'Pasaporte')");
			tdocs.Add("insert into TipoDocumento(id,tipodoc) values (3,'Cédula')");
			tdocs.Add("insert into TipoDocumento(id,tipodoc) values (4,'Libreta de Enrolamiento')");
			tdocs.Add("insert into TipoDocumento(id,tipodoc) values (5,'Libreta Cívica')");
			tdocs.Add("insert into TipoDocumento(id,tipodoc) values (6,'Otro')");
			
			List<string> ttels = new List<string>();
			ttels.Add("insert into TipoTelefono (id,tipotel) values (1,'Fijo')");
			ttels.Add("insert into TipoTelefono (id,tipotel) values (2,'Celular')");
			ttels.Add("insert into TipoTelefono (id,tipotel) values (5,'Otro')");
			
			foreach(string s in tdocs) {
				ExecuteCmd(s);
			}
			foreach(string s in ttels) {
				ExecuteCmd(s);
			}
		}

		private void ExecuteCmd(string cmdstr) {
			try {
				SQLiteCommand cmd = new SQLiteCommand(cmdstr,conn);
				int retval= cmd.ExecuteNonQuery();
				if (retval == 0)
					MessageBox.Show("OK![" + cmdstr + "]");
				else
					MessageBox.Show("ERROR![" + cmdstr + "]");				
			} catch (Exception ce) {
				MessageBox.Show(ce.StackTrace);
			}
		}
		
		private void ExecuteDML(string dml) {
			try {
				SQLiteCommand cmd = new SQLiteCommand(dml,conn);
				int retval= cmd.ExecuteNonQuery();
				if (retval == 0)
					MessageBox.Show("OK![" + dml + "]");
				else
					MessageBox.Show("ERROR![" + dml + "]");				
			} catch (Exception ce) {
				MessageBox.Show(ce.StackTrace);
			}			
		}
		private void CTTipoDocumento() {
			string sql = "create table TipoDocumento (id integer primary key,tipodoc text not null)";
			ExecuteCmd(sql);
		}
		
		private void CTPersonas() {
			string sql = "create table Personas (tipodoc int not null,documento text not null," +
			"apellido text not null,nombre text not null,fechanac date not null,email text," +
			"sexo char(1) not null check (sexo in ('F','M'))," +
			"constraint pk_personas primary key (tipodoc,documento)," +
			"constraint fk_pers_tipodoc foreign key (tipodoc) references TiposDocumento(id))";
			ExecuteCmd(sql);
		}

		private void CTTipoTelefono() {
			string sql = "create table TipoTelefono (id integer primary key,tipotel text not null)";
			ExecuteCmd(sql);
		}
		
		private void CTTelefonos() {
			string sql = "create table Telefonos (tipodoc int not null,	documento text not null," +
			"tipotelefono int not null,	telefono text not null," +
			"activo_sn integer not null check (activo_sn in (0,1)) default 1," +
			"constraint pk_telefonos primary key (tipodoc,documento,tipotelefono)," +
			"constraint fk_tel_tipotel foreign key (tipotelefono) references TiposTelefono(id)," +
			"constraint fk_tel_tipodoc foreign key (tipodoc) references TiposDocumento(id))";
			ExecuteCmd(sql);
		}
		
		
		public void Connect() {
			try {
				conn = new SQLiteConnection(db);
				conn.Open();
				MessageBox.Show("Connected to " + db);
			} catch (Exception e) {
				MessageBox.Show(e.Message);
			}
		}
		
		public void CreateDB() {
			try {
				SQLiteConnection.CreateFile(dbFile);
			} catch (Exception dbe) {
				MessageBox.Show(dbe.Message);
			}
		}
		
		public void insertarPersona(int tipodoc, string documento,string apellido, string nombre, string fechanac, string email, string sexo) {
			SQLiteTransaction trans;
			string SQL = "INSERT INTO PERSONAS (tipodoc, documento,apellido,nombre,fechanac,email,sexo) VALUES (?,?,?,?,?,?,?)";
			//	SQL += "(@tipodoc, @documento, @apellido, @nombre, @fechanac, @email, @sexo)";
			DateTime pdate = new DateTime();
			pdate = Convert.ToDateTime(fechanac);
	 
			SQLiteCommand cmd = new SQLiteCommand(SQL,conn);
			SQLiteParameter ptipodoc = new SQLiteParameter("@tipodoc", DbType.Decimal);
			ptipodoc.Value = tipodoc;
			SQLiteParameter pdocumento = new SQLiteParameter("@documento", DbType.AnsiString);
			pdocumento.Value = documento;
			SQLiteParameter papellido = new SQLiteParameter("@apellido", DbType.AnsiString);
			papellido.Value = apellido;
			SQLiteParameter pnombre = new SQLiteParameter("@nombre", DbType.AnsiString);
			pnombre.Value = nombre;
			SQLiteParameter pfechanac = new SQLiteParameter("@fechanac", DbType.Date);
			pfechanac.Value = pdate;
			SQLiteParameter pemail = new SQLiteParameter("@email", DbType.AnsiString);
			pemail.Value = email;
			SQLiteParameter psexo = new SQLiteParameter("@sexo", DbType.AnsiString);
			char csexo = sexo.Substring(0,1).ToCharArray()[0];
			psexo.Value = csexo;
			
			cmd.Parameters.Add(ptipodoc);
			cmd.Parameters.Add(pdocumento);
			cmd.Parameters.Add(papellido);
			cmd.Parameters.Add(pnombre);
			cmd.Parameters.Add(pfechanac);
			cmd.Parameters.Add(pemail);
			cmd.Parameters.Add(psexo);
			
			trans = conn.BeginTransaction();
			int retval = 0;
			try
			{
				retval= cmd.ExecuteNonQuery();
				if (retval == 1)
					MessageBox.Show("Row inserted!");
				else
					MessageBox.Show("Row NOT inserted.");
			} catch (Exception ex)	{
				MessageBox.Show(ex.Message);
				trans.Rollback();
			}
			finally	{
				trans.Commit();
				cmd.Dispose();
				//conn.Close();
			}
 		}
		
		public void disconnect() {
			conn.Close();
		}
	}
}
