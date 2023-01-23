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
    public partial class Certificados : Form
    {
        private string sTargetFolder;
        string servidor;
        string usuario;
        string passw;
        MySqlConnection con;
        MySqlCommand com = new MySqlCommand();
        MySqlDataAdapter da = new MySqlDataAdapter();
        DataSet ds = new DataSet();

        public Certificados()
        {
            InitializeComponent();
        }

        private void btn_exCer_Click(object sender, EventArgs e)
        {
            using (var oBrowser = new FolderBrowserDialog())
            {
                if (oBrowser.ShowDialog() == DialogResult.OK)
                {
                    sTargetFolder = oBrowser.SelectedPath;
                    this.tb_rutaCer.Text = sTargetFolder + @"\";
                }
            }            
        }
        
        private void btn_opCer_Click(object sender, EventArgs e)
        {
            string certname = "";
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "CER files (*.cer)|*.cer";
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                certname = open.SafeFileName;
                this.tb_cerN.Text = certname;
            }
            else
            {
                MessageBox.Show("No se a podido abrir el archivo");
            }
        }

        private void btn_opKey_Click(object sender, EventArgs e)
        {
            string keyname = "";
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "CER files (*.key)|*.key";
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                keyname = open.SafeFileName;
                this.tb_keyN.Text = keyname;
            }
            else
            {
                MessageBox.Show("No se a podido abrir el archivo");
            } 
        }

        private void Certificados_Load(object sender, EventArgs e)
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
                string busemp = "Select Clave from empresa where (Estatus = 1)";
                da = new MySqlDataAdapter(busemp, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "Clave");
                con.Close();
                cb_cEmp.DataSource = ds.Tables["Clave"].DefaultView;
                cb_cEmp.DisplayMember = "Clave";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar empresas. Consulte al Administrador del Sistema. \n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
                com.Dispose();
            }                      
            
            this.lb_noCer.Text = "";
            this.lb_validDe.Text = "";
            this.lb_valiHas.Text = "";

        }

        public void ConxPos()
        {
            con.Open();
            com.Connection = con;
            com.CommandText = "Select Nombre FROM empresa WHERE (Clave = " + cb_cEmp.Text + ")";
            da.SelectCommand = com;
            ds = new DataSet();
            da.Fill(ds, "empresa");
            DataRow row = ds.Tables["empresa"].Rows[0];
            lbl_emp.Text = row["Nombre"].ToString().Trim();
            con.Close();
        }

        private void btn_cerrar_Click(object sender, EventArgs e)
        {
            this.Close();            
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {            
            string noCertificado = "";
            string ErrorCod = "0";
            string ArchivoCertificado = this.tb_rutaCer.Text + this.tb_cerN.Text;
            string ArchivoKey = this.tb_rutaCer.Text + this.tb_keyN.Text;

            bool success;
            Chilkat.PrivateKey loPkey = new Chilkat.PrivateKey();
            Chilkat.Cert loCert = new Chilkat.Cert();
            Chilkat.Rsa loRsa = new Chilkat.Rsa();


            // Desbloquea componente
            success = loRsa.UnlockComponent("RSAT34MB34N_7F1CD986683M");
            if (success != true)
            {
                ErrorCod = "1";
                MessageBox.Show("No se pudo debloquear componente Chilkat RSA", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //  Load the Certificado 
            success = loCert.LoadFromFile(ArchivoCertificado);
            if (success != true)
            {
                ErrorCod = "1";
                MessageBox.Show("No se pudo abrir el archivo certificado: " + ArchivoCertificado + " Verifique que el archivo se encuentre en la Carpeta Seleccionada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Load the private key from an RSA PEM file:     
            success = loPkey.LoadPkcs8EncryptedFile(ArchivoKey, this.tb_pwC.Text);// ArchivoKey, PasswordKey);
            if (success != true)
            {
                ErrorCod = "1";
                MessageBox.Show("No se pudo abrir archivo KEY o contraseña invalida: " + ArchivoKey, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //  Get the private key in XML format:
            string lcPkeyXml = loPkey.GetXml();

            // Import the private key into the RSA component:
            success = loRsa.ImportPrivateKey(lcPkeyXml);
            if (success != true)
            {
                ErrorCod = "1";
                MessageBox.Show("Problemas al importar la llave pribada al componente RSA", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //  OpenSSL uses BigEndian byte ordering
            loRsa.LittleEndian = false;
            loRsa.Charset = "utf-8";
            loRsa.EncodingMode = "base64";      

            string N = loCert.SerialNumber;
            noCertificado = N.Substring(02 - 1, 1) +
                            N.Substring(04 - 1, 1) +
                            N.Substring(06 - 1, 1) +
                            N.Substring(08 - 1, 1) +
                            N.Substring(10 - 1, 1) +
                            N.Substring(12 - 1, 1) +
                            N.Substring(14 - 1, 1) +
                            N.Substring(16 - 1, 1) +
                            N.Substring(18 - 1, 1) +
                            N.Substring(20 - 1, 1) +
                            N.Substring(22 - 1, 1) +
                            N.Substring(24 - 1, 1) +
                            N.Substring(26 - 1, 1) +
                            N.Substring(28 - 1, 1) +
                            N.Substring(30 - 1, 1) +
                            N.Substring(32 - 1, 1) +
                            N.Substring(34 - 1, 1) +
                            N.Substring(36 - 1, 1) +
                            N.Substring(38 - 1, 1) +
                            N.Substring(40 - 1, 1);
            
            this.lb_validDe.Text = loCert.ValidFrom.Date.ToString("yyyy/MM/dd");
            this.lb_valiHas.Text = loCert.ValidTo.Date.ToString("yyyy/MM/dd");
            this.lb_noCer.Text = noCertificado;
        }

        private void btn_Guardar_Click(object sender, EventArgs e)
        {
            MySqlConnection con = new MySqlConnection("server='" + servidor + "';Port=3306;User Id='" + usuario + "';password='" + passw + "';Persist Security Info=True;database=cfdi");
            MySqlCommand com = new MySqlCommand("SELECT COUNT(*) AS Valid FROM certificados WHERE (Clave_emp = "+cb_cEmp.Text+")", con);
            
            int valida = 0;
            try
            {
                con.Open();
                valida = Convert.ToInt32(com.ExecuteScalar());

                if (valida > 0)
                {
                    if (MessageBox.Show("Se ha encontrado certificados ya estableceidos para esta empresa, ¿Desea remplazar los certificados ya existentes? ", "Certificados", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        com.CommandText = "UPDATE certificados SET Arch_cer = '" + this.tb_cerN.Text + "', Arch_key ='" + this.tb_keyN.Text + "', Fecha_Crea ='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "', Contrasena ='" + this.tb_pwC.Text + "', NoCertificado ='"
                                          + this.lb_noCer.Text + "', ValidoDe ='" + this.lb_validDe.Text + "', ValidoHasta ='" + this.lb_valiHas.Text + "', Ruta_cer = '" + this.tb_rutaCer.Text.Replace("\\", "\\\\") + "' " +
                                            "WHERE (Clave_emp = " + cb_cEmp.Text + ")";
                        com.ExecuteNonQuery();
                        MessageBox.Show("Se a actualizado la información correctamente", "Actualización", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    com.CommandText = "INSERT INTO certificados (Clave_emp, Arch_cer, Arch_key, Estatus, Fecha_Crea, Contrasena, NoCertificado, ValidoDe, ValidoHasta, Ruta_cer)" +
                                      "VALUES (" + this.cb_cEmp.Text + ",'" + this.tb_cerN.Text + "','" + this.tb_keyN.Text + "','A','" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") +
                                      "','" + this.tb_pwC.Text + "','" + this.lb_noCer.Text + "','" + this.lb_validDe.Text + "','" + this.lb_valiHas.Text + "','" + this.tb_rutaCer.Text.Replace("\\", "\\\\") + "')";
                    com.ExecuteNonQuery();
                    MessageBox.Show("Se a guardado la información correctamente", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se ha podido guardar la información. Intentelo nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                com.Dispose();
                con.Close();
            }
            clear();
        }
        
        public void clear()
        {
            this.tb_cerN.Text = "";
            this.tb_keyN.Text = "";
            this.tb_pwC.Text = "";
            this.tb_rutaCer.Text = "";
            this.lb_noCer.Text = "";
            this.lb_validDe.Text = "";
            this.lb_valiHas.Text = "";
        }

        private void tb_pwC_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_ok_Click(sender, e);
            }
        }

        private void cb_cEmp_TextChanged(object sender, EventArgs e)
        {
            if ((cb_cEmp.Text == "") || (cb_cEmp.Text == null) || (cb_cEmp.Text == "System.Data.DataRowView"))
            {
            }
            else
            {
                clear();
                ConxPos();
            }
        }
    }
}
