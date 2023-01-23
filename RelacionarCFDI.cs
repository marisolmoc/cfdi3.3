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
using System.Xml;
using System.IO;

namespace SiscomCFDI
{
    public partial class RelacionarCFDI : Form
    {
        MySqlConnection con;
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataAdapter da = new MySqlDataAdapter();
        DataSet dsCFDI = new DataSet();        
        OpenFileDialog XMLDialog = new OpenFileDialog();
        MySqlCommand com = new MySqlCommand();
        string servidor;
        string usuario;
        string passw;
        DataTable _lista_facturas = new DataTable();
        DataTable _docRelacionados = new DataTable();
        string _tipo_mov;
        string _rfc;
        int _cve_emp;

        public RelacionarCFDI(string tipo_mov, string rfc, int clave_empresa)
        {
            _tipo_mov = tipo_mov;
            _rfc = rfc;
            _cve_emp = clave_empresa;
            InitializeComponent();
        }

        private void RelacionarCFDI_Load(object sender, EventArgs e)
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
                //load Tipos de Relacion
                string query = "SELECT * FROM cfdi.c_tiporelacion;";
                MySqlDataAdapter mysqlda = new MySqlDataAdapter(query, con);
                DataSet ds = new DataSet();
                mysqlda.Fill(ds, "c_tiporelacion");
                cb_tipoRelacion.DisplayMember = "Descripcion";
                cb_tipoRelacion.ValueMember = "id_tiporelacion";
                cb_tipoRelacion.DataSource = ds.Tables["c_tiporelacion"];
                cb_tipoRelacion.Text = "Seleccione..";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al cargar lista de Tipo de Relación. Verifique la base de datos. Error:" + ex.Message, "Error al Cargar Tipo de Relación", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void btn_buscardoc_Click(object sender, EventArgs e)
        {
            cleanValues();
            try
            {
                con.Open();
                string query = "Select Folio_SAT as UUID, Serie, Factura as Folio, Fecha, Operacion as Total, "
                                + "(CASE "
                                + "    WHEN Estatus = 'C' THEN 'Si'"
                                + "    ELSE 'No' "
                                + "END) as Cancelada "
                                + "from cfdi.movimientos where RFC = '" + _rfc + "' and Serie = '" + _tipo_mov + "' and Clave_Emp = " + _cve_emp + ";";
                MySqlDataAdapter _adapter = new MySqlDataAdapter(query, con);
                _adapter.Fill(_lista_facturas);                
                dg_lista_facturas.DataSource = _lista_facturas;
                dg_lista_facturas.CurrentCell.Selected = false;
                btn_agregar.Enabled = _lista_facturas.Rows.Count > 0 ? true : false;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error. Puede que no existan documentos relacionados a este RFC "+_rfc+". Error:" + ex.Message, "Error al buscar documentos relacionados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        public void cleanValues()
        {
            dg_docRelacionados.DataSource = null;
            dg_lista_facturas.DataSource = null;
        }

        public DataTable docsRelacionados_tabla
        {
            get { return _docRelacionados; }
        }

        public string tipoRelacion
        {
            get { return cb_tipoRelacion.SelectedValue.ToString(); }
        }

        private void btn_agregar_Click(object sender, EventArgs e)
        {
            _docRelacionados = new DataTable();
            var dtTemp = dg_lista_facturas.DataSource as DataTable; 
            _docRelacionados = dtTemp.Clone();
            DataTable table = new DataTable();
            for (int i = 0; i < dg_lista_facturas.Rows.Count; i++)
            {
                if (dg_lista_facturas.Rows[i].Selected)
                {
                    var row = _docRelacionados.NewRow();
                    for (int j = 0; j < dg_lista_facturas.Columns.Count; j++)
                    {
                        row[j] = dg_lista_facturas[j, i].Value;
                    }
                    _docRelacionados.Rows.Add(row);
                }
            }
            dg_docRelacionados.DataSource = _docRelacionados;
            dg_docRelacionados.CurrentCell.Selected = false;

            btn_aceptar.Enabled = true;
        }

        private void btn_aceptar_Click(object sender, EventArgs e)
        {
            if (validar())
                this.Close();
            else
                MessageBox.Show("Verifique sus valores para continuar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public bool validar()
        {
            bool hasValues = false;
            if (cb_tipoRelacion.Text != "Seleccione.." && _docRelacionados.Rows.Count > 0)
                hasValues = true;

            return hasValues;
        }
    }
}
