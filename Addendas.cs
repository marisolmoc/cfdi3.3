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
    public partial class Addendas : Form
    {
        MySqlConnection con;
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataAdapter da = new MySqlDataAdapter();
        DataSet dst = new DataSet();

        string servidor;
        string usuario;
        string passw;
        string cveEmpre;

        string adEmisorRI;
        string adReceptor;
        string adNumProv;
        string adFolio;
        string adProvEKT;

        public Addendas(string Cve_Empresa)
        {
            cveEmpre = Cve_Empresa;
            InitializeComponent();
        }

        private void Addendas_Load(object sender, EventArgs e)
        {
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

            try
            {
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "Select EmisorRI FROM empresa WHERE Clave ="+ cveEmpre +"";
                string cve_emp = cmd.ExecuteScalar().ToString();
                tb_emisorRI.Text = cve_emp;
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se a podido conectar con la base de datos verifique la conexion", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
        }

        public bool validacion()
        {
            bool Valid;

            if ((tb_emisorRI.Text == string.Empty) || (tb_receptorRI.Text == string.Empty) || (tb_NumProv.Text == string.Empty) || (tb_folioCompra.Text == string.Empty) || (tb_provEKT.Text == string.Empty))
                Valid = false;                
            else
                Valid = true;                
            

            if (tb_emisorRI.Text == string.Empty)
                emi_red.Visible = true;
            else
                emi_red.Visible = false;                       

            if (tb_receptorRI.Text == string.Empty)
                rec_red.Visible = true;            
            else
                rec_red.Visible = false;            
            
            if (tb_NumProv.Text == string.Empty) 
                prov_red.Visible = true;
            else
                prov_red.Visible = false;            

            if (tb_folioCompra.Text == string.Empty)
                folio_red.Visible = true;
            else 
                folio_red.Visible = false;

            if (tb_provEKT.Text == string.Empty)
                ekt_red.Visible = true;
            else
                ekt_red.Visible = false;

            return Valid;
        }

        private void btnContinue_Click_1(object sender, EventArgs e)
        {          
            if (validacion() == true) 
            {
                con.Open();
                                
                cmd.Connection = con;
                cmd.CommandText = "Select EmisorRI FROM empresa WHERE Clave =" + cveEmpre + "";
                string Result = cmd.ExecuteScalar().ToString();

                if (Result == string.Empty)
                {
                    if (MessageBox.Show("¿Desea almacenar dato Emisor RI en la base de datos?", "Almacenar EmisorRI", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    { 
                        cmd = new MySqlCommand();
                        cmd.Connection = con;
                        cmd.CommandText = "UPDATE empresa SET EmisorRI ='" + tb_emisorRI.Text + "' WHERE (empresa.Clave = " + cveEmpre + ")";
                        cmd.ExecuteNonQuery();                    
                    }
                }

                adEmisorRI = tb_emisorRI.Text;
                adReceptor = tb_receptorRI.Text;
                adNumProv = tb_NumProv.Text;
                adFolio = tb_folioCompra.Text;
                adProvEKT = tb_provEKT.Text;

                this.Close();
            }
            
        }

        public string emisorRI
        {
            get { return adEmisorRI; }
        }

        public string receptorRI
        {
            get { return adReceptor; }
        }

        public string numProv
        {
            get { return adNumProv; }
        }

        public string folioCompra
        {
            get { return adFolio; }
        }

        public string proveEkt 
        {
            get { return adProvEKT; }
        }
    }
}
