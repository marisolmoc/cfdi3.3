using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using Microsoft.Win32;


namespace SiscomCFDI
{
    public partial class Form1 : Form
    {       
        string servidor;
        string usuario;
        string passw;

        string TimbFol;
        string NoTimb;
        string PDFfol;
        string Certifol;
        string QRfol;
        string ieps;

        MySqlConnection con;        
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataAdapter da = new MySqlDataAdapter();
        DataSet dst1 = new DataSet();
        FolderBrowserDialog dialogruta = new FolderBrowserDialog();
        
        public Form1()
        {
            InitializeComponent();
        }

        public bool Existencia()
        {
            bool exist = false;            
            cmd.Connection = con;
            cmd.CommandText = "Select Clave from empresa where (Clave = " + tb_clave.Text + ")";
            object Result = cmd.ExecuteScalar();            

            if (Result != null)
            {
                exist = true;
            } 
            
            return exist;
        }
        
        private void tbn_Guardar_Click(object sender, EventArgs e)
        {
            try
            {                
                con.Open();
                if (tb_rptFac.Text != String.Empty)
                {
                    if (Existencia() == true)
                    {
                        if (MessageBox.Show("La empresa ya se encuentra en la base de datos, ¿Esta seguro que desea sobrescribir la información?", "Sobrescribir", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            cmd = new MySqlCommand();
                            cmd.Connection = con;
                            cmd.CommandText = "UPDATE empresa SET Nombre = '" + this.tb_nombre.Text + "', RFC = '" + this.tb_rfc.Text + "', fCalle = '" + this.tb_fcalle.Text +
                                                "', fNoExterior = '" + this.tb_fNoExt.Text + "', fNoInterior = '" + this.tb_fNoInt.Text + "', fColonia = '" + this.tb_fColonia.Text + "', fLocalidad = '" + this.tb_fLocalidad.Text +
                                                "', fMunicipio = '" + this.tb_fMuni.Text + "', fEstado = '" + this.cb_fEstado.Text + "', fPais = '" + this.tb_fPais.Text + "', fCP = '" + this.tb_fCP.Text +
                                                "', sCalle = '" + this.tb_sCalle.Text + "', sNoExterior = '" + this.tb_sNoExt.Text + "', sNoInterior = '" + this.tb_sNoInt.Text + "', sColonia = '" + this.tb_sColonia.Text +
                                                "', sLocalidad = '" + this.tb_sLocalidad.Text + "', sMunicipio = '" + this.tb_sMunicipio.Text + "', sEstado = '" + this.cb_sEstado.Text + "', sPais = '" + this.tb_sPais.Text +
                                                "', sCP = '" + this.tb_sCP.Text + "', Directorio_base = '" + this.tb_Directorio.Text.Replace("\\", "\\\\") + "', TM = '" + this.tb_tm.Text + "', Cuenta_Timbrado = '" + this.tb_cutimb.Text +
                                                "', Token_Timbrado = '" + this.tb_TokTimb.Text + "', Usuario_Timbrado = '" + tb_UsuTimb.Text + "', Pass_Timbrado = '" + this.tb_PassTimb.Text + "', Contribuyente = '" + this.cb_contrib.SelectedItem.ToString() +
                                                "', RegFiscal = '" + cb_regiFiscal.SelectedValue + "', TipoFac = 'CFDI', Ruta_logo = '" + tb_logo.Text.Replace("\\", "\\\\") + "', Correo = '" + tb_correo.Text +
                                                "', Password = '" + tb_pass.Text + "', fTelefono = '" + tb_ftel.Text + "', sTelefono = '" + tb_Stel.Text + "', Reporte_fac ='" + tb_rptFac.Text.Replace("\\", "\\\\") +
                                                "' WHERE (empresa.Clave = " + tb_clave.Text + ")";
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Se a guardado la información correctamente", "Sobrescribir", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Limpiar();
                        }
                    }
                    else
                    {
                        CreateFolder();
                        cmd = new MySqlCommand();
                        cmd.Connection = con;
                        cmd.CommandText = "INSERT INTO empresa (Clave, Nombre, RFC, Estatus, Fecha_Crea, fCalle, fNoExterior, fNoInterior, fColonia," +
                                            " fLocalidad, fMunicipio, fEstado, fPais, fCP, sCalle, sNoExterior, sNoInterior, sColonia, sLocalidad, sMunicipio, sEstado, sPais" +
                                            ", sCP, Ruta_fact, Ruta_NoTimb, Ruta_PDF, Directorio_base, Ruta_BMP, TM, Cuenta_Timbrado, Token_Timbrado, Usuario_Timbrado, Pass_Timbrado, " +
                                            "Contribuyente, RegFiscal, TipoFac, Ruta_logo, Correo, Password, fTelefono, sTelefono, Reporte_fac, IEPS) VALUES ('" + this.tb_clave.Text +
                                            "','" + this.tb_nombre.Text + "','" + this.tb_rfc.Text + "','1','" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "','" + this.tb_fcalle.Text + "','" + this.tb_fNoExt.Text + "','" + this.tb_fNoInt.Text + "','" + this.tb_fColonia.Text +
                                            "','" + this.tb_fLocalidad.Text + "','" + this.tb_fMuni.Text + "','" + this.cb_fEstado.Text + "','" + this.tb_fPais.Text +
                                            "','" + this.tb_fCP.Text + "','" + this.tb_sCalle.Text + "','" + this.tb_sNoExt.Text + "','" + this.tb_sNoInt.Text + "','" + this.tb_sColonia.Text +
                                            "','" + this.tb_sLocalidad.Text + "','" + this.tb_sMunicipio.Text + "','" + this.cb_sEstado.Text + "','" + this.tb_sPais.Text +
                                            "','" + this.tb_sCP.Text + "','" + TimbFol.Replace("\\", "\\\\") + "\\\\" + "','" + NoTimb.Replace("\\", "\\\\") + "\\\\" + "','" + PDFfol.Replace("\\", "\\\\") + "\\\\" +
                                            "','" + this.tb_Directorio.Text.Replace("\\", "\\\\") + "','" + QRfol.Replace("\\", "\\\\") + "\\\\" + "','" + this.tb_tm.Text + "', '" + this.tb_cutimb.Text +
                                            "','" + this.tb_TokTimb.Text + "','" + this.tb_UsuTimb.Text + "','" + this.tb_PassTimb.Text + "','" + this.cb_contrib.SelectedItem.ToString() + "','" + cb_regiFiscal.SelectedValue +
                                            "','CFDI', '" + tb_logo.Text.Replace("\\", "\\\\") + "','" + tb_correo.Text + "','" + tb_pass.Text + "','" + tb_ftel.Text + "','" + tb_Stel.Text + "','" + tb_rptFac.Text.Replace("\\", "\\\\") + "', '0')";

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Se a guardado la información correctamente", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Limpiar();
                    }
                }
                else
                {
                    MessageBox.Show("No se puede guardar los datos, verifique que todos los datos estén completos.", "Dato Vacio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception m)
            {
                MessageBox.Show("No se ha podido guardar la información. Intentelo nuevamente \n" + m.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            { 
                cmd.Dispose();
                con.Close();            
            }
        }
              
        public void CreateFolder()
        {
            if (servidor == "localhost")
            {
                string path = @"C:\CFDI";
                string subfol = System.IO.Path.Combine(path, this.tb_clave.Text + this.tb_nombre.Text);
                System.IO.Directory.CreateDirectory(subfol);
                TimbFol = System.IO.Path.Combine(subfol, "XML");
                System.IO.Directory.CreateDirectory(TimbFol);
                NoTimb = System.IO.Path.Combine(subfol, "NO TIMBRADO");
                System.IO.Directory.CreateDirectory(NoTimb);
                PDFfol = System.IO.Path.Combine(subfol, "PDF");
                System.IO.Directory.CreateDirectory(PDFfol);
                Certifol = System.IO.Path.Combine(subfol, "CERTIFICADOS");
                System.IO.Directory.CreateDirectory(Certifol);
                QRfol = System.IO.Path.Combine(subfol, "QR");
                System.IO.Directory.CreateDirectory(QRfol);
            }
            else
            {
                string path = "\\\\" + servidor + @"\c\CFDI";
                string subfol = System.IO.Path.Combine(path, this.tb_clave.Text + this.tb_nombre.Text);
                System.IO.Directory.CreateDirectory(subfol);
                TimbFol = System.IO.Path.Combine(subfol, "XML");
                System.IO.Directory.CreateDirectory(TimbFol);
                NoTimb = System.IO.Path.Combine(subfol, "NO TIMBRADO");
                System.IO.Directory.CreateDirectory(NoTimb);
                PDFfol = System.IO.Path.Combine(subfol, "PDF");
                System.IO.Directory.CreateDirectory(PDFfol);
                Certifol = System.IO.Path.Combine(subfol, "CERTIFICADOS");
                System.IO.Directory.CreateDirectory(Certifol);
                QRfol = System.IO.Path.Combine(subfol, "QR");
                System.IO.Directory.CreateDirectory(QRfol);
            }
        }

        public void Limpiar()
        {
            this.cb_fEstado.Text = "Seleccione..";
            this.cb_sEstado.Text = "Seleccione..";
            this.cb_contrib.Text = "Seleccione..";
            this.cb_regiFiscal.DataSource = null;
            this.cb_regiFiscal.Text = "Seleccione..";
            this.tb_logo.Clear();        
            this.tb_Directorio.Text = "";
            this.tb_fcalle.Text = "";
            this.tb_fColonia.Text = "";
            this.tb_fCP.Text = "";
            this.tb_fLocalidad.Text = "";
            this.tb_fMuni.Text = "";
            this.tb_fNoExt.Text = "";
            this.tb_fNoInt.Text = "";
            this.tb_fPais.Text = "";
            this.tb_nombre.Text = "";
            this.tb_rfc.Text = "";            
            this.tb_sCalle.Text = "";
            this.tb_sColonia.Text = "";
            this.tb_sCP.Text = "";
            this.tb_sLocalidad.Text = "";
            this.tb_sMunicipio.Text = "";
            this.tb_sNoExt.Text = "";
            this.tb_sNoInt.Text = "";
            this.tb_sPais.Text = "";
            this.tb_tm.Text = "";
            this.tb_cutimb.Text = "";
            this.tb_PassTimb.Text = "";
            this.tb_TokTimb.Text = "";
            this.tb_UsuTimb.Text = "";
            tb_correo.Clear();
            tb_pass.Clear();
            tb_Stel.Clear();
            tb_ftel.Clear();
            tb_pathcbb.Clear();
            tb_rptFac.Clear();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void btn_ExDir_Click(object sender, EventArgs e)
        {
            string dbpos = "";
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "MDB files (*.mdb)|*.mdb";
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                dbpos = open.FileName;
                tb_Directorio.Text = dbpos;
            }
            else
            {
                MessageBox.Show("No se a podido abrir el archivo");
            }           
        }

        private void btn_logo_Click(object sender, EventArgs e)
        {
            OpenFileDialog logoDialog = new OpenFileDialog();
            logoDialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            logoDialog.Filter = "JPG files (*.jpg)| *.jpg";
            if (logoDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tb_logo.Text = logoDialog.FileName;
            }
            else
            {
                MessageBox.Show("No se a podido abrir el archivo imagen");
            }   
        }
        
        private void btn_exCbb_Click(object sender, EventArgs e)
        {
            OpenFileDialog cbbDialog = new OpenFileDialog();
            cbbDialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            cbbDialog.Filter = "JPG files (*.jpg)| *.jpg";
            if (cbbDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.tb_pathcbb.Text = cbbDialog.FileName;
            }
            else
            {
                MessageBox.Show("No se a podido abrir el archivo imagen");
            }   
        }   
        
        private void Form1_Load(object sender, EventArgs e)
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
                cmd.CommandText = "Select Max(Clave)+1 as ClaveMax FROM empresa";
                da.SelectCommand = cmd;
                da.Fill(dst1, "Clave");

                DataRow row = dst1.Tables["Clave"].Rows[0];
                tb_clave.Text = row["ClaveMax"].ToString();

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

        private void tb_clave_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buscarDatos();
            }
        }

        private void buscarDatos()
        {
            Limpiar();
            con.Open();
            da = new MySqlDataAdapter();
            dst1 = new DataSet();

            cmd.Connection = con;
            cmd.CommandText = "SELECT Nombre, RFC, fCalle, fNoExterior, fNoInterior, fColonia, fLocalidad, fMunicipio, fEstado, fPais, fCP, "+
                                "sCalle, sNoExterior, sNoInterior, sColonia, sLocalidad, sMunicipio, sEstado, sPais, sCP, " +                                 
                                "Directorio_base, TM, Cuenta_Timbrado, Token_Timbrado, Usuario_Timbrado, Pass_Timbrado, Contribuyente, RegFiscal,"+ 
                                "Ruta_logo, Correo, Password, fTelefono, sTelefono, Ruta_cbb, Reporte_fac, IEPS " + 
                                "FROM empresa WHERE (Clave= "+tb_clave.Text+")";
            da.SelectCommand = cmd;            
            con.Close();

            if (da.Fill(dst1, "Datos") == 0)
            {
                MessageBox.Show("No se ha encontrado la empresa con la clave "+tb_clave.Text+". ","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DataRow datos = dst1.Tables["Datos"].Rows[0];
                this.tb_nombre.Text = datos["Nombre"].ToString();
                this.tb_rfc.Text = datos["RFC"].ToString();
                this.tb_cutimb.Text = datos["Cuenta_Timbrado"].ToString();
                this.tb_tm.Text = datos["TM"].ToString();
                this.tb_fcalle.Text = datos["fCalle"].ToString();
                this.tb_fNoExt.Text = datos["fNoExterior"].ToString();
                this.tb_fNoInt.Text = datos["fNoInterior"].ToString();
                this.tb_fColonia.Text = datos["fColonia"].ToString();
                this.tb_fLocalidad.Text = datos["fLocalidad"].ToString();
                this.tb_fMuni.Text = datos["fMunicipio"].ToString();
                this.cb_fEstado.Text = datos["fEstado"].ToString();
                this.tb_fPais.Text = datos["fPais"].ToString();
                this.tb_fCP.Text = datos["fCP"].ToString();
                this.tb_sCalle.Text = datos["sCalle"].ToString();
                this.tb_sNoExt.Text = datos["sNoExterior"].ToString();
                this.tb_sNoInt.Text = datos["sNoInterior"].ToString();
                this.tb_sColonia.Text = datos["sColonia"].ToString();
                this.tb_sLocalidad.Text = datos["sLocalidad"].ToString();
                this.tb_sMunicipio.Text = datos["sMunicipio"].ToString();
                this.cb_sEstado.Text = datos["sEstado"].ToString();
                this.tb_sPais.Text = datos["sPais"].ToString();
                this.tb_sCP.Text = datos["sCP"].ToString();
                this.tb_Directorio.Text = datos["Directorio_base"].ToString();
                this.tb_UsuTimb.Text = datos["Usuario_Timbrado"].ToString();
                this.tb_TokTimb.Text = datos["Token_Timbrado"].ToString();
                this.tb_PassTimb.Text = datos["Pass_Timbrado"].ToString();
                this.cb_contrib.Text = datos["Contribuyente"].ToString();
                string regiFiscal = datos["RegFiscal"].ToString();
                if (regiFiscal.Length > 3)
                    this.cb_regiFiscal.Text = "Seleccione..";
                else
                    this.cb_regiFiscal.SelectedValue = datos["RegFiscal"].ToString();                
                this.tb_logo.Text = datos["Ruta_logo"].ToString();
                this.tb_correo.Text = datos["Correo"].ToString();
                this.tb_pass.Text = datos["Password"].ToString();
                this.tb_ftel.Text = datos["fTelefono"].ToString();
                this.tb_Stel.Text = datos["sTelefono"].ToString();
                this.tb_pathcbb.Text = datos["Ruta_cbb"].ToString();
                this.tb_rptFac.Text = datos["Reporte_fac"].ToString();                
            }
        }

        private void btn_rptFac_Click(object sender, EventArgs e)
        {            
            OpenFileDialog open = new OpenFileDialog();
            open.InitialDirectory = Application.StartupPath;
            open.Filter = "RPT file (*.rpt)|*.rpt";
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.tb_rptFac.Text = open.FileName;
            }
            else 
            { 
                MessageBox.Show("No se a podido abrir el archivo"); 
            }
        }

        private void cb_contrib_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string contribuyente = cb_contrib.Text;
                string query = "";
                if (contribuyente == "Moral")
                {
                    query = "SELECT * FROM cfdi.c_regimenfiscal Where Moral = true;";
                }
                else
                {
                    query = "SELECT * FROM cfdi.c_regimenfiscal Where Fisica = true;";
                }
                MySqlDataAdapter mysqlda = new MySqlDataAdapter(query, con);
                con.Open();
                DataSet ds = new DataSet();
                con.Close();
                mysqlda.Fill(ds, "c_regimenfiscal");
                cb_regiFiscal.DisplayMember = "Descripcion";
                cb_regiFiscal.ValueMember = "id_regimenfiscal";
                cb_regiFiscal.DataSource = ds.Tables["c_regimenfiscal"];
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al buscar Régimen Fiscal. Verifique la base de datos. Error:" + ex.Message, "Error al Cargar Régimen Fiscal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }            
    }
}
