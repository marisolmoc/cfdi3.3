using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System.IO;

namespace SiscomCFDI
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }


        private void Admin_Load(object sender, EventArgs e)
        {
            try
            {
                RegistryKey conectar = Registry.CurrentUser.OpenSubKey("SiscomCFDI");

                using (RegistryKey i = conectar.OpenSubKey("Conexion"))
                {
                    tb_servidor.Text = i.GetValue("Servidor").ToString();
                    tb_usuario.Text = i.GetValue("Usuario").ToString();
                    tb_passw.Text = i.GetValue("Password").ToString();
                } 
            }
            catch (Exception ex)
            { 
            
            }
            
        }

        private void btn_guardar_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection("server='" + tb_servidor.Text + "';User Id='" + tb_usuario.Text + "';password='" + tb_passw.Text + "';Persist Security Info=True;database=test");

            try 
            {
                con.Open();
                MessageBox.Show("Conexion Exitosa, se han guardado sus datos correctamente.", "Conexion completa", MessageBoxButtons.OK, MessageBoxIcon.Information);            
                con.Close();
            } 
            catch(Exception ex) 
            {
                MessageBox.Show("No se a realizado la conexion verifique sus datos", "Error de Conexion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            try
            {
                RegistryKey conectar = Registry.CurrentUser.CreateSubKey("SiscomCFDI");

                using (RegistryKey i = conectar.CreateSubKey("Conexion"))
                {
                    i.SetValue("Servidor", tb_servidor.Text.ToString());
                    i.SetValue("Usuario", tb_usuario.Text.ToString());
                    i.SetValue("Password", tb_passw.Text.ToString());
                }

                 if (CheckDatabaseExists() == false)
                {
                    string mysqlConnectionString = "server='" + tb_servidor.Text + "';Port=3306;User Id='" + tb_usuario.Text + "';password='" + tb_passw.Text + "';Persist Security Info=True;";
                 
                  FileInfo file = new FileInfo(Application.StartupPath + @"\cfdi.sql");
                  string script = file.OpenText().ReadToEnd();
                  
                  MySqlConnection conn = new MySqlConnection(mysqlConnectionString);
                  MySqlCommand cmd = new MySqlCommand(script, conn);
                  conn.Open();
                 
                  cmd.ExecuteNonQuery();
                 
                  cmd.Dispose();
                  conn.Close();
                }
                 this.Close();
            }
            catch (Exception ex)
            { 
                MessageBox.Show("Ha ocurrido un error al crear la base de datos, verifique su conexión", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool CheckDatabaseExists()
        {
            string mysqlConnectionString = "server='" + tb_servidor.Text + "';Port=3306;User Id='" + tb_usuario.Text + "';password='" + tb_passw.Text + "';Persist Security Info=True;database=test";	
            MySqlConnection conn = new MySqlConnection(mysqlConnectionString);
	        string cmdText = ("SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = 'cfdi'");
	        MySqlCommand cmd = new MySqlCommand(cmdText, conn);
	        bool bRet = false;
	        object result;
        try
        {
	        conn.Open();
	        result = cmd.ExecuteScalar();
	        cmd.Dispose();
	        conn.Close();

	        if (result != null)
	        {
		        bRet = true;
	        }        	
        }
        catch (Exception ex)
        {	        
	        bRet = true;
        }        	
	        return bRet;
        }

        private void tb_passw_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_guardar_Click(sender, e);
            }
        }
    }
}
