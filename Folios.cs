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
    public partial class Folios : Form
    {
        public Folios()
        {
            InitializeComponent();
        }

        string servidor;
        string usuario;
        string passw;

        MySqlConnection conn;
        MySqlCommand comm = new MySqlCommand();
        DataSet ds;
        MySqlDataAdapter adapt;
        string serie;
        string FoliosT;

        private void button1_Click(object sender, EventArgs e)
        {
            Acceso acceso= new Acceso();
            acceso.Show();
        }

        private void Folios_Load(object sender, EventArgs e)
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
                adapt = new MySqlDataAdapter(busemp, conn);
                DataSet ds = new DataSet();
                adapt.Fill(ds, "Clave");
                conn.Close();
                cb_cEmp.DataSource = ds.Tables["Clave"].DefaultView;
                cb_cEmp.DisplayMember = "Clave";
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

            LoadGrid();
        }

        public void ConxPos()
        {
            conn.Open();
            comm.Connection = conn;
            comm.CommandText = "Select Nombre FROM empresa WHERE (Clave = " + cb_cEmp.Text + ")";
            adapt.SelectCommand = comm;
            ds = new DataSet();
            adapt.Fill(ds, "empresa");
            DataRow row = ds.Tables["empresa"].Rows[0];
            lbl_emp.Text = row["Nombre"].ToString().Trim();
            conn.Close();
        }

        private void LoadGrid()
        {
            try
            {
                this.dataGridView1.DataSource = null;
                ds = new DataSet();

                conn.Open();
                comm.Connection = conn;
                comm.CommandText = "SELECT control_folios.Serie, control_folios.FolioInicial AS 'Fol. Inicial', MAX(movimientos.Folio) AS 'No. Folios', control_folios.Estatus, MIN(movimientos.Folio) AS 'Actual', control_folios.Fecha_Crea AS 'Fecha Creacion'"
                    + " FROM control_folios INNER JOIN movimientos ON control_folios.Clave_Emp = movimientos.Clave_Emp WHERE (movimientos.Estatus = 'D') AND (movimientos.Clave_Emp = " + this.cb_cEmp.Text + ") AND (control_folios.Estatus = '1') GROUP BY control_folios.Serie";

                adapt = new MySqlDataAdapter(comm);
                if (adapt.Fill(ds) == 0)
                {
                    dataGridView1.DataSource = ds.Tables[0];
                }
                else
                {
                    DataRow row = ds.Tables[0].Rows[0];
                    serie = row["Serie"].ToString();

                    dataGridView1.DataSource = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al buscar Folios de la empresa "+cb_cEmp.Text+". \n" + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                comm.Dispose();
                conn.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void cb_cEmp_SelectedValueChanged(object sender, EventArgs e)
        {
            if ((this.cb_cEmp.Text == null) || (cb_cEmp.Text == "") || (cb_cEmp.Text == "System.Data.DataRowView"))
            {

            }
            else
            {
                ConxPos();
                LoadGrid();
            }
        }
       
        private void btn_Eliminar_Click(object sender, EventArgs e)
        {            
            if (MessageBox.Show("¿Esta seguro que desea eliminar un total de "+FoliosT+" de su lista de folios?", "Eliminar", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                conn.Open();
                comm.Connection = conn;
                comm.CommandText = "UPDATE control_folios SET Estatus = '0', Fecha_Can ='"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"' "
                                   + " WHERE (Clave_Emp = " + cb_cEmp.Text + ") AND (Serie = '" + serie + "')";
                comm.ExecuteNonQuery();

                MessageBox.Show("La Lista de folio han sido eliminadas de la base de datos ", "Folios Eliminados", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                EliminarMov();
                conn.Close();
                LoadGrid(); 
            }                      
        }

        private void EliminarMov()
        {
            comm.Connection = conn;
            comm.CommandText = "DELETE FROM movimientos WHERE (Clave_Emp = " + cb_cEmp.Text + " ) AND (Serie = '" + serie + "')";
            comm.ExecuteNonQuery();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dataGridView1.SelectedCells[0].RowIndex;
            serie = dataGridView1.Rows[i].Cells[0].Value.ToString();
            FoliosT = dataGridView1.Rows[i].Cells[2].Value.ToString();
        }

        
        
    }
}
