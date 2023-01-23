using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CHILKATMAILLib2;
using System.IO;
using Microsoft.Win32;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace SiscomCFDI
{
    public partial class Correo : Form
    {
        string xml;
        string pdf;
        string Clave_emp;
        string pathXML;
        string pathPDF;
        string correoCli;

        public Correo(string pathxml, string xmlNom, string pathpdf, string pdfNom, string emp, string emailCli)
        {            
            InitializeComponent();
            xml = xmlNom;
            pdf = pdfNom;
            Clave_emp = emp;
            pathXML = pathxml;
            pathPDF = pathpdf;
            correoCli = emailCli;
        }

        string servidor;
        string usuario;
        string passw;        

        public string MailPass = "";
        string smpt;
        int puerto;
        string correo;

        MySqlConnection con;
        MySqlCommand com = new MySqlCommand();
        MySqlDataAdapter da = new MySqlDataAdapter();
        DataSet ds = new DataSet();

        private void Correo_Load(object sender, EventArgs e)
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
                com.Connection = con;
                com.CommandText = "SELECT Correo, Password FROM cfdi.empresa WHERE (Clave = " + Clave_emp + ")";
                da.SelectCommand = com;
                if (da.Fill(ds) != 0)
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    correo = row["Correo"].ToString();
                    tb_de.Text = correo;
                    MailPass = row["Password"].ToString();
                }
                
                validEmail();
                lbl_xml.Text = xml;
                lbl_pdf.Text = pdf;
                tb_para.Text = correoCli.ToLower();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al buscar el correo emisor. Verifique su conexión. \n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
                com.Dispose();
            }
        }
        public void validEmail()
        { 
            string email;
            email = tb_de.Text.Substring(tb_de.Text.Length - 10);
            if(email == "@gmail.com")
            {
                smpt = "smtp.gmail.com";
                puerto = 587;
            }
            else if (email == "omsoft.com")
            {
                smpt = "smtp.gmail.com";
                puerto = 587;
            }
            else 
            {
                MessageBox.Show("El correo electrónico emisor no es válido. \n Tiene que ser un correo de gmail.", "Correo Invalido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tb_de.Focus();
            }
            
        }

        public void checkEmail()
        {
            if (correo != tb_de.Text)
            {
                Contraseña pass = new Contraseña();
                pass.tb_usu.Text = tb_de.Text;
                pass.ShowDialog();
                MailPass = pass.Resultado;
                validEmail();
            }
            validEmail();
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            checkEmail();

            if (MailPass == string.Empty)
            {
                MessageBox.Show("Falta su contraseña para completar el envio.\n", "Datos restantes", MessageBoxButtons.OK, MessageBoxIcon.Warning);                
            }
            else
            {
                string emailDe = tb_de.Text;
                string emailPara = tb_para.Text;
                string emailCC = tb_cc.Text;
                string emailCCO = tb_cco.Text;
                string emailAsunto = tb_asunto.Text;
                string emailCuerpo = rtb_coment.Text;
                string emailArchivo1 = pathXML + xml;
                string emailArchivo2 = pathPDF + pdf;
                string emailArchivo3 = "";
                string emailArchivo4 = "";
                string emailSmtpHost = smpt;
                string emailSmtpUsername = emailDe; ;
                string emailSmtpPassword = MailPass;

                string correo = "";
                string error = "";
                
                try
                {
                    ChilkatEmail2 oemail = new ChilkatEmail2();
                    ChilkatMailMan2 oMailMan = new ChilkatMailMan2();

                    // Se desbloquea el componente de eMail con el serial 
                    int succes = oMailMan.UnlockComponent("MAILT34MB34N_3ED277200UHK");
                    if (succes != 1)
                    {
                        error = oMailMan.LastErrorText + "\r\n";
                    }

                    //emailSmtpHost = oMailMan.MxLookup(tb_de.Text);
                    //if (emailSmtpHost == null)
                    //{
                    //    MessageBox.Show(oMailMan.LastErrorText);
                    //    return;
                    //}
                    
                    oMailMan.SmtpSsl = 1;
                    oMailMan.SmtpPort = puerto;
                    oMailMan.SmtpHost = emailSmtpHost; 
                    oMailMan.SmtpUsername = emailSmtpUsername;
                    oMailMan.SmtpPassword = emailSmtpPassword;

                    oemail.From = "<" + emailDe + ">"; // OjO tiene que ir entre "<" ">" 
                    oemail.subject = emailAsunto;
                    oemail.Body = emailCuerpo;

                    #region oemail.AddFileAttachment
                    if (emailArchivo1.Trim().Length != 0)
                        oemail.AddFileAttachment(emailArchivo1.Trim());

                    if (emailArchivo2.Trim().Length != 0)
                        oemail.AddFileAttachment(emailArchivo2.Trim());

                    if (emailArchivo3.Trim().Length != 0)
                        oemail.AddFileAttachment(emailArchivo3.Trim());

                    if (emailArchivo4.Trim().Length != 0)
                        oemail.AddFileAttachment(emailArchivo4.Trim());
                    #endregion

                    #region oemail.AddTo
                    correo = "";
                    for (int i = 0; i < emailPara.ToString().Trim().Length; i++)
                    {
                        if (emailPara.ToString().Trim().Substring(i, 1) == ",")
                        {
                            oemail.AddTo("", correo);
                            correo = "";
                        }
                        correo = correo + emailPara.ToString().Trim().Substring(i, 1);
                        if (emailPara.ToString().Trim().Length == (i + 1))
                            oemail.AddTo("", correo);
                    }
                    #endregion

                    #region oemail.AddCC
                    correo = "";
                    for (int i = 0; i < emailCC.ToString().Trim().Length; i++)
                    {
                        if (emailCC.ToString().Trim().Substring(i, 1) == ",")
                        {
                            oemail.AddCC("", correo);
                            correo = "";
                        }
                        correo = correo + emailCC.ToString().Trim().Substring(i, 1);
                        if (emailCC.ToString().Trim().Length == (i + 1))
                            oemail.AddCC("", correo);
                    }
                    #endregion

                    #region oemail.AddBcc
                    correo = "";
                    for (int i = 0; i < emailCCO.ToString().Trim().Length; i++)
                    {
                        if (emailCCO.ToString().Trim().Substring(i, 1) == ",")
                        {
                            oemail.AddBcc("", correo);
                            correo = "";
                        }
                        correo = correo + emailCCO.ToString().Trim().Substring(i, 1);
                        if (emailCCO.ToString().Trim().Length == (i + 1))
                            oemail.AddBcc("", correo);
                    }
                    #endregion

                    // Se envia el correo 
                    succes = oMailMan.SendEmail(oemail);
                    if (succes != 1)
                    {
                        MessageBox.Show("No se ha enviado el correo electronico verifique su cuenta y su conexion a internet.\n", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        error = oMailMan.LastErrorText;
                    }
                    else
                    {
                        succes = oMailMan.CloseSmtpConnection();
                        MessageBox.Show("Se ha enviado correo electrónico correctamente.\n Aceptar para continuar. ", "Enviado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se ha enviado el correo electronico verifique su cuenta y su conexion a internet.\n"+ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tb_para_Leave(object sender, EventArgs e)
        {
            Regex RegExp;

            if (tb_para.Text.Trim() != string.Empty)
            {
                RegExp = new Regex(@"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$"); 

                if(!RegExp.IsMatch(tb_para.Text.Trim()))
                {
                    MessageBox.Show("El formato de correo no es incorrecto", "Correo Destino", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tb_para.Focus();
                }
            }
        }

        private void tb_de_Leave(object sender, EventArgs e)
        {
            Regex RegExp;

            if (tb_de.Text.Trim() != string.Empty)
            {
                RegExp = new Regex(@"^([a-zA-Z0-9_\-])([a-zA-Z0-9_\-\.]*)@(\[((25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\.){3}|((([a-zA-Z0-9\-]+)\.)+))([a-zA-Z]{2,}|(25[0-5]|2[0-4][0-9]|1[0-9][0-9]|[1-9][0-9]|[0-9])\])$");

                if (!RegExp.IsMatch(tb_de.Text.Trim()))
                {
                    MessageBox.Show("El formato de correo no es incorrecto", "Correo Emisor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tb_de.Focus();
                }
            }
        }  
    }
}
