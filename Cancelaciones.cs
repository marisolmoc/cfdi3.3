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
    public partial class Cancelaciones : Form
    {
        string servidor;
        string usuario;
        string passw;

        MySqlConnection con;
        MySqlCommand com = new MySqlCommand();
        MySqlDataAdapter dar = new MySqlDataAdapter();
        DataSet dst = new DataSet();

        public Cancelaciones()
        {
            InitializeComponent();
        }

        private void Cancelaciones_Load(object sender, EventArgs e)
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
                dar = new MySqlDataAdapter(busemp, con);
                dar.Fill(dst, "Clave");
                con.Close();
                cb_ClaveEmp.DataSource = dst.Tables["Clave"].DefaultView;
                cb_ClaveEmp.DisplayMember = "Clave";
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
        }

        public void ConxPos()
        {
            con.Open();
            com.Connection = con;
            com.CommandText = "Select Nombre FROM empresa WHERE (Clave = " + cb_ClaveEmp.Text + ")";
            dar.SelectCommand = com;
            dst = new DataSet();
            dar.Fill(dst, "empresa");
            DataRow row = dst.Tables["empresa"].Rows[0];
            lbl_emp.Text = row["Nombre"].ToString().Trim();
            con.Close();
        }
        
        private void tb_Serie_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_buscar_Click(sender, e);
            }
        }

        private void tb_fFactura_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_buscar_Click(sender, e);
            }
        }

        private void btn_buscar_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                
                com.Connection = con;
                if(rb_fac.Checked == true)
                {
                    dar = new MySqlDataAdapter();

                    com.CommandText = "SELECT CadenaOriginal, No_Certificado, SelloCFD, SelloSAT, Folio_SAT, NoCSD_SAT, Operacion, Impuesto, TipoCambio, IVA, AnoAprobacion, NoAprobacion " +
                                      "FROM movimientos WHERE (Clave_Emp = " + cb_ClaveEmp.Text + ") AND (Serie = '" + this.tb_Serie.Text + 
                                      "') AND (Factura = " + this.tb_fFactura.Text + ") AND (Estatus = 'T')";
                    dar.SelectCommand = com;

                    if (dar.Fill(dst, "Factura") == 0)
                    {
                        MessageBox.Show("No se ha encontrado la factura, intentelo de nuevo", "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {  
                        DataRow row = dst.Tables["Factura"].Rows[0];

                        tb_noCer.Text = row["No_Certificado"].ToString();
                        rtb_cOrig.Text = row["CadenaOriginal"].ToString();
                        rtb_sCFD.Text = row["SelloCFD"].ToString();                        
                        rtb_sSAT.Text = row["SelloSAT"].ToString();                        
                        tb_uuid.Text = row["Folio_SAT"].ToString();                        
                        tb_nCerSAT.Text = row["NoCSD_SAT"].ToString();
                        tb_total.Text = row["Operacion"].ToString();
                        tb_imp.Text = row["Impuesto"].ToString();
                        tb_tCamb.Text = row["TipoCambio"].ToString();
                        tb_IVA.Text = row["IVA"].ToString();
                        tb_anioAp.Text = row["AnoAprobacion"].ToString();
                        tb_noAp.Text = row["NoAprobacion"].ToString();
                    }
                }
                else if (rb_notC.Checked == true)
                {
                    dar = new MySqlDataAdapter();

                    com.CommandText = "SELECT CadenaOriginal, No_Certificado, SelloCFD, Operacion, Impuesto, TipoCambio, IVA, AnoAprobacion, NoAprobacion " +
                                      "FROM movimientos WHERE (Clave_Emp = " + cb_ClaveEmp.Text + ") AND (Serie = '" + this.tb_Serie.Text +
                                      "') AND (NotaCredito = " + this.tb_notC.Text + ") AND (Estatus = 'NC')";
                    
                    dar.SelectCommand = com;
                    if (dar.Fill(dst, "NotaCred") == 0)
                    {
                        MessageBox.Show("No se ha encontrado la nota de crédito, intentelo de nuevo", "¡ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        DataRow row = dst.Tables["NotaCred"].Rows[0];

                        tb_noCer.Text = row["No_Certificado"].ToString();
                        rtb_cOrig.Text = row["CadenaOriginal"].ToString();
                        rtb_sCFD.Text = row["SelloCFD"].ToString();                                        
                        tb_total.Text = row["Operacion"].ToString();
                        tb_imp.Text = row["Impuesto"].ToString();
                        tb_tCamb.Text = row["TipoCambio"].ToString();
                        tb_IVA.Text = row["IVA"].ToString();
                        tb_anioAp.Text = row["AnoAprobacion"].ToString();
                        tb_noAp.Text = row["NoAprobacion"].ToString();
                    }                
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error, contacte al administrador del sistema." + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
                com.Dispose();
            }
        }

        public void limpiar()
        {
            tb_noCer.Clear();
            tb_fFactura.Clear();
            tb_Serie.Clear();
            tb_noCer.Clear();
            rtb_cOrig.Clear();
            rtb_sCFD.Clear();
            rtb_sSAT.Clear();
            tb_uuid.Clear();
            tb_nCerSAT.Clear();
            tb_total.Clear();
            tb_imp.Clear();
            tb_tCamb.Clear();
            tb_IVA.Clear();
            tb_noAp.Clear();
            tb_anioAp.Clear();
            tb_notC.Clear();
        }

        private void btn_aceptar_Click(object sender, EventArgs e)
        {
            con.Open();
            if (rb_fac.Checked == true)
            {
                if (MessageBox.Show("¿Esta seguro que desea cancelar la Factura electronica Numero " + this.tb_fFactura.Text + "?", "Cancelar", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
                {

                    com.Connection = con;
                    com.CommandText = "UPDATE movimientos SET Estatus ='C' WHERE " +
                                      "(Clave_Emp = " + cb_ClaveEmp.Text + ") AND (Serie = '" + this.tb_Serie.Text + "') AND (Factura = " + this.tb_fFactura.Text + ")";

                    com.ExecuteNonQuery();

                    if (MessageBox.Show("La Factura a sido cancelada exitosamente en el sistema. \n "
                    + "Ahora proceda a cancelarla delante del SAT. ¿Desea dirigirse al portal del sat para realizar la cancelación? ", "Factura Cancelada", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start("http://www.sat.gob.mx/sitio_internet/asistencia_contribuyente/principiantes/comprobantes_fiscales/66_19592.html");
                    }                    
                }
            }
            else if (rb_notC.Checked == true)
            {
                if (MessageBox.Show("¿Esta seguro que desea cancelar la Nota de Crédito Numero " + this.tb_notC.Text + "?", "Cancelar", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    com.Connection = con;
                    com.CommandText = "UPDATE movimientos SET Estatus ='NCC' WHERE " +
                                      "(Clave_Emp = " + cb_ClaveEmp.Text + ") AND (Serie = '" + this.tb_Serie.Text + "') AND (NotaCredito = " + this.tb_notC.Text + ")";

                    com.ExecuteNonQuery();

                    if (MessageBox.Show("La Nota de crédito a sido cancelada exitosamente en el sistema. \n "
                    + "Ahora proceda a cancelarla delante del SAT. ¿Desea dirigirse al portal del sat para realizar la cancelación? ", "Factura Cancelada", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start("http://www.sat.gob.mx/sitio_internet/asistencia_contribuyente/principiantes/comprobantes_fiscales/66_19592.html");
                    }
                    
                }
            }
            con.Close();
            limpiar();        

        } 
        
        private void btn_cerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rb_fac_CheckedChanged(object sender, EventArgs e)
        {
            limpiar();

            if (rb_fac.Checked == true)
            {
                label4.Visible = true;
                tb_fFactura.Visible = true;
                tb_fFactura.ReadOnly = false;
                
                tb_Serie.ReadOnly = false;

                label13.Visible = false;
                tb_notC.Visible = false;
                tb_notC.ReadOnly = true;

                btn_buscar.Enabled = true;
            }
            else if (rb_notC.Checked == true)
            {
                label4.Visible = false;
                tb_fFactura.Visible = false;
                tb_fFactura.ReadOnly = true;
                
                tb_Serie.ReadOnly = false;

                label13.Visible = true;
                tb_notC.Visible = true;
                tb_notC.ReadOnly = false;

                btn_buscar.Enabled = true;
            }
            
        }

        private void cb_ClaveEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((cb_ClaveEmp.Text == "") || (cb_ClaveEmp.Text == null) || (cb_ClaveEmp.Text == "System.Data.DataRowView"))
            {
            }
            else
            {
                limpiar();
                ConxPos();
            }
        }               
    }
}
