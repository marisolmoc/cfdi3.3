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
using System.IO;

namespace SiscomCFDI
{
    public partial class RptMensual : Form
    {
        string servidor;
        string usuario;
        string passw;
        
        MySqlConnection con;
        MySqlCommand com = new MySqlCommand();
        MySqlDataAdapter da = new MySqlDataAdapter();
        DataSet ds = new DataSet();

        public RptMensual()
        {
            InitializeComponent();
        }

        private void RptMensual_Load(object sender, EventArgs e)
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
                string busemp = "Select Clave from empresa where (Estatus = 1) AND (TipoFac = 'CFD')";
                da = new MySqlDataAdapter(busemp, con);
                da.Fill(ds, "Clave");
                con.Close();
                cb_ClaveEmp.DataSource = ds.Tables["Clave"].DefaultView;
                cb_ClaveEmp.DisplayMember = "Clave";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar empresas. Consulte al Administrador del Sistema. \n"+ ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            da.SelectCommand = com;
            ds = new DataSet();
            da.Fill(ds, "empresa");
            DataRow row = ds.Tables["empresa"].Rows[0];
            lbl_emp.Text = row["Nombre"].ToString().Trim();
            con.Close();
        }

        private void btn_crear_Click(object sender, EventArgs e)
        {
            try
            {   con.Open();
                com.Connection = con;
                com.CommandText = "SELECT RFC from empresa where (Clave = "+cb_ClaveEmp.Text+")";
                string erfc = com.ExecuteScalar().ToString();

                string path = "";
                SaveFileDialog savetxt = new SaveFileDialog();
                savetxt.Title = "Guardar Reporte Mensual";
                savetxt.InitialDirectory = @"C:\";                
                savetxt.DefaultExt = "txt";
                savetxt.Filter = "txt files (*.txt)|*.txt|All files(*.*)|*.*";
                
                savetxt.FileName = "1" + erfc + dp_fecha.Value.ToString("yyyyMM");

                if (savetxt.ShowDialog() == DialogResult.OK)
                {
                    path = savetxt.FileName;      
                
                    string datos = "Select DISTINCT RFC, Serie, Factura, AnoAprobacion, NoAprobacion, Fecha, Operacion As Importe, Impuesto, Estatus, NotaCredito "+
                                    "FROM movimientos WHERE (Clave_Emp = " + cb_ClaveEmp.Text + ")AND (MONTH(Fecha) ='" + dp_fecha.Value.ToString("MM") +
                                    "') AND (YEAR(Fecha) ='" + dp_fecha.Value.ToString("yyyy") + "') ";
                    da = new MySqlDataAdapter(datos, con);
                    ds = new DataSet();
                    da.Fill(ds, "Datos");
                    DataTable dt;
                    dt= ds.Tables["Datos"];
                    
                    StreamWriter rpttxt = new StreamWriter(path);
                    
                    foreach(DataRow dr in dt.Rows)                
                    {
                        string NotaCredito = "";
                        string rfc =  dr.ItemArray[0].ToString().Trim();
                        string serie = dr.ItemArray[1].ToString().Trim();
                        string folio = dr.ItemArray[2].ToString().Trim();
                        string aprobacion = dr.ItemArray[3].ToString() + dr.ItemArray[4].ToString();
                        
                        string fecha = dr.ItemArray[5].ToString();
                        fecha = Convert.ToDateTime(fecha).ToString("dd/MM/yyyy HH:mm:ss");
                        
                        string importe = dr.ItemArray[6].ToString();
                        importe = Convert.ToDouble(importe).ToString("N2").Replace(",", "");
                        
                        string impuesto = dr.ItemArray[7].ToString();
                        impuesto = Convert.ToDouble(impuesto).ToString("N2").Replace(",", "");

                        string estatus = dr.ItemArray[8].ToString(); 
                        string status = "";
                        if ((estatus == "T") || (estatus == "NC"))
                        {
                            status = "1";
                        }
                        else if ((estatus == "C") || (estatus == "NCC"))
                        {
                            status = "0";
                        }

                        string comprobante = "";
                        if ((estatus == "T")||(estatus == "C"))
                        {
                            comprobante = "I";
                        }
                        else if ((estatus == "NC")||(estatus == "NCC"))
                        {
                            comprobante = "E";
                            NotaCredito = dr.ItemArray[9].ToString();
                            folio = NotaCredito;
                        }

                        rpttxt.WriteLine("|" + rfc + "|" + serie + "|" + folio + "|" + aprobacion + "|" + fecha + "|" + importe + "|" + impuesto + "|" + status + "|" + comprobante + "||||");
                    }
                    rpttxt.Close();
                    MessageBox.Show("Se ha creado exitosamente el Reporte", "Reporte Mensual", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el Reporte. Consulte al Administrador del Sistema. \n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
                com.Dispose();
            }

        }

        private void cb_ClaveEmp_TextChanged(object sender, EventArgs e)
        {
            if ((cb_ClaveEmp.Text == "") || (cb_ClaveEmp.Text == null) || (cb_ClaveEmp.Text == "System.Data.DataRowView"))
            {
            }
            else
            {
                ConxPos();
            }
        }

        private void btn_preview_Click(object sender, EventArgs e)
        {
            da = new MySqlDataAdapter();
            DataTable dt = new DataTable();

            try
            {
                con.Open();
                com.Connection= con;
                com.CommandText = "SELECT DISTINCT movimientos.RFC, movimientos.Serie, movimientos.Factura, movimientos.AnoAprobacion, " +
                          "movimientos.NoAprobacion, movimientos.Fecha, movimientos.Operacion AS Importe, movimientos.Impuesto, " +
                          "movimientos.Estatus, movimientos.NotaCredito, empresa.Nombre FROM movimientos INNER JOIN empresa ON movimientos.Clave_Emp = empresa.Clave " +
                          "WHERE (MONTH(movimientos.Fecha) = '" + dp_fecha.Value.ToString("MM") + "') AND (YEAR(movimientos.Fecha) = '" + dp_fecha.Value.ToString("yyyy") +
                          "') AND (movimientos.Clave_Emp = " + cb_ClaveEmp.Text + ")";
                da.SelectCommand = com;
                da.Fill(dt);
                visor preview = new visor(dt);
                preview.ShowDialog();                
            }
            catch(Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex), "Error vista previa", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                com.Dispose();
                con.Close();
            }
        }
    }
}
