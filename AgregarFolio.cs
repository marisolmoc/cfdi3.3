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
    public partial class AgregarFolio : Form
    {
        public AgregarFolio()
        {
            InitializeComponent();
        }
        MySqlConnection conn;
        MySqlCommand comm = new MySqlCommand();
        MySqlDataAdapter da = new MySqlDataAdapter();
        DataSet dst1 = new DataSet();

        string servidor;
        string usuario;
        string passw;

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_aceptar_Click(object sender, EventArgs e)
        {
            string TM = "";
            if (rb_tmFac.Checked == true)
            {
                TM = "FA";
            }
            else if (rb_tmNC.Checked == true)
            {
                TM = "NC";
            }
            
            try
            {
                conn.Open();
                comm.Connection = conn;

                comm.CommandText = "INSERT INTO control_folios(Clave_Emp, Serie, FolioInicial, FoliosT, Estatus, Fecha_Crea, TipoMov)" +
                                    "VALUES (" + cb_clveEmp.Text + ", '" + this.tb_serie.Text + "', " + this.tb_folIni.Text + ", "
                                    + this.tb_folTot.Text + ", '1', '" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "','" + TM + "')";        
                               
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex), "Error");
            }
            
            MySqlCommand loopcom = new MySqlCommand();
            loopcom.Connection = conn;
            int l = int.Parse(this.tb_folTot.Text);
            for (int i = 0; i < l; i++)
            {
                loopcom.CommandText = "INSERT INTO movimientos (Serie, Clave_Emp, Estatus, AnoAprobacion, NoAprobacion, TipoMov) VALUES ('"
                   + this.tb_serie.Text + "'," + cb_clveEmp.Text + ",'D','"+tb_anoAprob.Text+"', '"+tb_noAprob.Text+"','" + TM + "')";
                loopcom.ExecuteNonQuery();
            }            
            
            comm.ExecuteNonQuery();
            conn.Close();
            this.Close();
        }

        private void AgregarFolio_Load(object sender, EventArgs e)
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
            conn = new MySqlConnection(queryCon);

            try
            {
                conn.Open();
                string busemp = "Select Clave from empresa where (Estatus = 1)";
                da = new MySqlDataAdapter(busemp, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "Clave");
                conn.Close();
                cb_clveEmp.DataSource = ds.Tables["Clave"].DefaultView;
                cb_clveEmp.DisplayMember = "Clave";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar empresas. Consulte al Administrador del Sistema. \n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {                
                conn.Close();                
                comm.Dispose();
            }          
        }
        
        private void validFolios()
        {
            this.tb_folIni.Text = "";
            this.tb_folTot.Text = "";
            this.tb_serie.Text = "";

            if ((this.cb_clveEmp.Text == null) || (cb_clveEmp.Text == "") || (cb_clveEmp.Text == "System.Data.DataRowView"))
            {

            }
            else
            {
                try
                {
                    conn.Open();
                    //*******Validando tipo de facturación
                    comm.Connection = conn;
                    comm.CommandText = "SELECT TipoFac From empresa WHERE (Clave = " + this.cb_clveEmp.Text + ")";
                    string TipoFac = comm.ExecuteScalar().ToString();

                    if (TipoFac == "CFDI")
                    {
                        label5.Visible = false;
                        label6.Visible = false;
                        tb_anoAprob.Visible = false;
                        tb_noAprob.Visible = false;
                        label7.Visible = false;
                        panel_TM.Visible = false;
                    }
                    else if ((TipoFac == "CFD") || (TipoFac == "CFD"))
                    {
                        label5.Visible = true;
                        label6.Visible = true;
                        tb_anoAprob.Visible = true;
                        tb_noAprob.Visible = true;
                        label7.Visible = true;
                        panel_TM.Visible = true;
                    }


                    //*******Ultimo Folio de la empresa
                    comm.Connection = conn;
                    comm.CommandText = "SELECT Max(Folio)+1 as FolioMax, Serie FROM movimientos WHERE (Clave_Emp = " + this.cb_clveEmp.Text + ") Group by Serie";
                    da.SelectCommand = comm;
                    da.Fill(dst1, "Folios");

                    if (da.Fill(dst1, "Folios") == 0)
                    {
                        this.tb_folIni.ReadOnly = false;
                    }
                    else
                    {
                        foreach (DataRow row in dst1.Tables["Folios"].Rows)
                        {
                            object Max = row["FolioMax"];
                            object Serie = row["Serie"];
                            this.tb_folIni.Text = Max.ToString();
                            this.tb_serie.Text = Serie.ToString();
                            this.tb_folIni.ReadOnly = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se a almacenado los folios correctamente, intentelo de nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void cb_clveEmp_SelectedValueChanged(object sender, EventArgs e)
        {
            validFolios();
        }

    }
}
