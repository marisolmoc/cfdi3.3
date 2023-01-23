using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Microsoft.Win32;

namespace SiscomCFDI
{
    public partial class Clave_SAT : Form
    {
        MySqlConnection con;
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataAdapter da = new MySqlDataAdapter();
        DataSet dst = new DataSet();
        string desc_pos;       
        bool isSearch_ClaveSAT;
        string servidor;
        string usuario;
        string passw;

        string clave_SAT;
        string claveUnidad_SAT;

        public Clave_SAT(string desc, bool isSearchClaveSAT)
        {
            desc_pos = desc;
            isSearch_ClaveSAT = isSearchClaveSAT;
                                    
            InitializeComponent();
        }

        private void Clave_SAT_Load(object sender, EventArgs e)
        {
            if (isSearch_ClaveSAT)
            {
                codigoSat_grp.Visible = true;
                tb_desc.Text = desc_pos;
            }
            else 
            {
                claveUnidad_grp.Visible = true;
                tb_claveUnidad.Text = desc_pos;
            }

            try
            {
                RegistryKey conectar = Registry.CurrentUser.OpenSubKey("SiscomCFDI");

                using (RegistryKey i = conectar.OpenSubKey("Conexion"))
                {
                    servidor = i.GetValue("Servidor").ToString();
                    usuario = i.GetValue("Usuario").ToString();
                    passw = i.GetValue("Password").ToString();
                }
            }
            catch (Exception ex)
            {
                Admin admin = new Admin();
                admin.Show();
            }
            string queryCon = ("server='" + servidor + "';Port=3306;User Id='" + usuario + "';password='" + passw + "';Persist Security Info=True;database=cfdi");
            con = new MySqlConnection(queryCon);                        
        }

        private void buscar_codigoSat_Click(object sender, EventArgs e)
        {
            string query = "";
            int n;
            var isNumeric = int.TryParse(tb_desc.Text, out n);

            if (tb_desc.Text.Length == 8 && isNumeric)
            {
                query = "SELECT id_claveprodserv as 'Clave SAT', Descripcion FROM cfdi.c_claveprodserv Where id_claveprodserv = '" + tb_desc.Text + "';";
            }
            else
            {
                query = "SELECT id_claveprodserv as 'Clave SAT', Descripcion FROM cfdi.c_claveprodserv Where Descripcion like '%" + tb_desc.Text + "%';";
            }

            if (tb_desc.Text.Length >= 3)
            {
                da = new MySqlDataAdapter();
                dst = new DataSet();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = query;
                da.SelectCommand = cmd;
                da.Fill(dst, "Codigo_SAT");

                if (dst.Tables[0].Rows.Count == 0)
                {
                    da = new MySqlDataAdapter();
                    dst = new DataSet();
                    cmd.CommandText = "SELECT id_claveprodserv as 'Clave SAT', Descripcion FROM cfdi.c_claveprodserv Where id_claveprodserv = '01010101';";
                    da.SelectCommand = cmd;
                    da.Fill(dst, "Codigo_SAT");
                }

                con.Close();
                dataGrid_codigoSat.DataSource = dst.Tables[0];
                Application.DoEvents();
            }
        }

        private void dataGrid_codigoSat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                clave_SAT = dataGrid_codigoSat[dataGrid_codigoSat.Columns["Clave SAT"].Index, e.RowIndex].Value.ToString();
                this.Close();
            }
        }
                
        public string clave_sat 
        {
            get { return clave_SAT; }
        }

        public string claveUnidad_sat 
        {
            get { return claveUnidad_SAT; }
        }

        private void tb_desc_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buscar_codigoSat_Click(sender, e);
            }            
        }

        private void dataGrid_claveUnidad_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                claveUnidad_SAT = dataGrid_claveUnidad[dataGrid_claveUnidad.Columns["Clave Unidad SAT"].Index, e.RowIndex].Value.ToString();
                this.Close();
            }
        }

        private void buscar_claveUnidad_Click(object sender, EventArgs e)
        {
            string query = "";

            if (tb_claveUnidad.Text.Length <= 3)
            {
                query = "SELECT id_claveunidad as 'Clave Unidad SAT', Nombre FROM cfdi.c_claveunidad Where id_claveunidad = '" + this.tb_claveUnidad.Text + "';";
            }
            else if (tb_claveUnidad.Text.Length > 3)
            {
                query = "SELECT id_claveunidad as 'Clave Unidad SAT', Nombre FROM cfdi.c_claveunidad Where Nombre like '%" + this.tb_claveUnidad.Text + "%';";
            }

            da = new MySqlDataAdapter();
            dst = new DataSet();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = query;//"SELECT id_claveunidad as 'Clave Unidad SAT', Nombre FROM cfdi.c_claveunidad Where Nombre like '%" + this.tb_claveUnidad.Text + "%';";
            da.SelectCommand = cmd;
            da.Fill(dst, "ClaveUnidad_SAT");

            if (dst.Tables[0].Rows.Count == 0)
            {
                da = new MySqlDataAdapter();
                dst = new DataSet();
                cmd.CommandText = "SELECT id_claveunidad as 'Clave Unidad SAT', Nombre FROM cfdi.c_claveunidad Where Nombre = 'No disponible';";
                da.SelectCommand = cmd;
                da.Fill(dst, "ClaveUnidad_SAT");
            }

            con.Close();
            dataGrid_claveUnidad.DataSource = dst.Tables[0];
            //}
        }

        private void tb_claveUnidad_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buscar_claveUnidad_Click(sender, e);
            }            
        }
    }
}
