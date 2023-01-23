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
    public partial class Impuestos_SAT : Form
    {
        MySqlConnection con;
        MySqlCommand cmd = new MySqlCommand();
        MySqlDataAdapter da = new MySqlDataAdapter();
        DataSet ds_impuesto = new DataSet();
        DataSet ds_tasa = new DataSet();
        string servidor;
        string usuario;
        string passw;
        double _base;
        string _prod_codigo;
        DataTable _impuestos = new DataTable();
        bool _isIngreso;
        int _folio;

        public Impuestos_SAT(double cant_base, string prod_codigo, bool isIngreso, int folio)
        {
            _base = cant_base;
            _prod_codigo = prod_codigo;
            _isIngreso = isIngreso;
            _folio = folio;
            InitializeComponent();
        }

        private void Impuestos_SAT_Load(object sender, EventArgs e)
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

            _impuestos = new DataTable();
            if (_folio != 0)
                _impuestos.Columns.Add("Folio", typeof(int));
            _impuestos.Columns.Add("Codigo Producto", typeof(string));
            _impuestos.Columns.Add("Tipo", typeof(string));
            _impuestos.Columns.Add("Base",typeof(decimal));
            _impuestos.Columns.Add("Impuesto",typeof(string));
            _impuestos.Columns.Add("Factor",typeof(string));
            _impuestos.Columns.Add("Tasa o Cuota",typeof(string));
            _impuestos.Columns.Add("Importe",typeof(decimal));

            if (!_isIngreso)
            {
                rb_retenciones.Visible = false;
                rb_traslado.Checked = true;
            }
        }
        
        public DataTable impuestos_tabla
        {
            get { return _impuestos; }
        }

        void rb_tipo_CheckedChanged(object sender, EventArgs e)
        {
            gb_retencion.Visible = false;
            gb_traslado.Visible = false;

            RadioButton rb = sender as RadioButton;
            if (rb.Checked) 
            {
                if (rb.Text == "Traslado") {                    
                    gb_traslado.Visible = true;                    
                    tb_trasBase.Text = _base.ToString();
                    load_tipo_impuestos(true);                    
                }
                if (rb.Text == "Retención") {
                    gb_retencion.Visible = true;                    
                    tb_retBase.Text = _base.ToString();
                    load_tipo_impuestos(false);
                }
            }
        }

        public void load_tipo_impuestos(bool is_traslado)
        {
            string query_impuesto;
            cb_trasImpuesto.DataSource = null;
            cb_retImpuesto.DataSource = null;

            try
            {
                con.Open();
                if (is_traslado)
                {
                    query_impuesto = "SELECT * FROM cfdi.c_impuesto WHERE Traslado = true;";
                }
                else {
                    query_impuesto = "SELECT * FROM cfdi.c_impuesto WHERE Retencion = true;";
                }
                da = new MySqlDataAdapter(query_impuesto, con);
                con.Close();
                if (is_traslado)
                {
                    da.Fill(ds_impuesto, "impuesto_traslado");
                    cb_trasImpuesto.DisplayMember = "Descripcion";
                    cb_trasImpuesto.ValueMember = "id_impuesto";                    
                    cb_trasImpuesto.DataSource = ds_impuesto.Tables["impuesto_traslado"];                    
                }
                else 
                {
                    da.Fill(ds_impuesto, "impuesto_retencion");
                    cb_retImpuesto.DisplayMember = "Descripcion";
                    cb_retImpuesto.ValueMember = "id_impuesto";
                    cb_retImpuesto.DataSource = ds_impuesto.Tables["impuesto_retencion"];                    
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al buscar los tipos de Impuesto. Verifique la base de datos. Error:" + ex.Message, "Error al Cargar Impuesto", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void cb_trasImpuesto_SelectedIndexChanged(object sender, EventArgs e)
        {
            ds_tasa = new DataSet();
            cb_trasTasa.DataSource = null;

            ComboBox cb = sender as ComboBox;
            string impuesto_text = cb.Text;
            string query_tasa;

            try
            {
                con.Open();
                query_tasa = "SELECT * FROM cfdi.c_tasa WHERE Impuesto = '" + impuesto_text + "' and Traslado = true;";
                da = new MySqlDataAdapter(query_tasa, con);
                da.Fill(ds_tasa, "tasa_traslado");
                cb_trasTasa.DisplayMember = "Valor";
                cb_trasTasa.ValueMember = "id_tasa";
                cb_trasTasa.DataSource = ds_tasa.Tables["tasa_traslado"];                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al buscar los tipos de Tasa. Verifique la base de datos. Error:" + ex.Message, "Error al Cargar Tasa", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void cb_retImpuesto_SelectedIndexChanged(object sender, EventArgs e)
        {
            ds_tasa = new DataSet();
            cb_retTasa.DataSource = null;

            ComboBox cb = sender as ComboBox;
            string impuesto_text = cb.Text;
            string query_tasa;

            try
            {
                con.Open();
                query_tasa = "SELECT * FROM cfdi.c_tasa WHERE Impuesto = '" + impuesto_text + "' and Retencion = true;";
                da = new MySqlDataAdapter(query_tasa, con);
                da.Fill(ds_tasa, "tasa_retencion");
                cb_retTasa.DisplayMember = "Valor";
                cb_retTasa.ValueMember = "id_tasa";
                cb_retTasa.DataSource = ds_tasa.Tables["tasa_retencion"];                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al buscar los tipos de Tasa. Verifique la base de datos. Error:" + ex.Message, "Error al Cargar Tasa", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void cb_trasTasa_SelectedIndexChanged(object sender, EventArgs e)
        {
            double tasa;
            ComboBox cb_tasa = sender as ComboBox;
            if (cb_tasa.SelectedIndex != -1)
            {
                tasa = Convert.ToDouble(cb_tasa.Text);
                double importe = (_base * tasa);
                tb_trasImporte.Text = (Math.Round(importe, 2)).ToString();
            }
        }

        private void cb_retTasa_SelectedIndexChanged(object sender, EventArgs e)
        {
            double tasa;
            ComboBox cb_tasa = sender as ComboBox;
            if (cb_tasa.SelectedIndex != -1)
            {
                tasa = Convert.ToDouble(cb_tasa.Text);
                double importe = (_base * tasa);
                tb_retImporte.Text = (Math.Round(importe, 2)).ToString();
            }
        }
        
        private void btn_addRetencion_Click(object sender, EventArgs e)
        {
            bool is_dgVisible = dg_impuestos.Visible;
            if (!is_dgVisible) 
            {
                dg_impuestos.Visible = true;
            }
            string factor = "Tasa";
            if (cb_retTasa.Text =="43.770000")
                factor = "Cuota";

            if(_folio != 0)
                _impuestos.Rows.Add(_folio,_prod_codigo, "Retención", Convert.ToDecimal(tb_retBase.Text), cb_retImpuesto.SelectedValue.ToString() + " " + cb_retImpuesto.Text, factor, cb_retTasa.Text, Convert.ToDecimal(tb_retImporte.Text));
            else
                _impuestos.Rows.Add(_prod_codigo, "Retención", Convert.ToDecimal(tb_retBase.Text), cb_retImpuesto.SelectedValue.ToString() + " " + cb_retImpuesto.Text, factor, cb_retTasa.Text, Convert.ToDecimal(tb_retImporte.Text));
            dg_impuestos.DataSource = _impuestos;
            limpiar(false);
            btn_aceptar.Enabled = true;
        }

        private void btn_addTraslado_Click(object sender, EventArgs e)
        {
            bool is_dgVisible = dg_impuestos.Visible;
            if (!is_dgVisible) 
            {
                dg_impuestos.Visible = true;
            }
            if (_folio != 0)
                _impuestos.Rows.Add(_folio,_prod_codigo, "Traslado", Convert.ToDecimal(tb_trasBase.Text), cb_trasImpuesto.SelectedValue.ToString() + " " + cb_trasImpuesto.Text, "Tasa", cb_trasTasa.Text, Convert.ToDecimal(tb_trasImporte.Text));
            else
                _impuestos.Rows.Add(_prod_codigo, "Traslado", Convert.ToDecimal(tb_trasBase.Text), cb_trasImpuesto.SelectedValue.ToString() + " " + cb_trasImpuesto.Text, "Tasa", cb_trasTasa.Text, Convert.ToDecimal(tb_trasImporte.Text));
            dg_impuestos.DataSource = _impuestos;
            limpiar(true);
            btn_aceptar.Enabled = true;
        }        
        
        public void limpiar(bool is_taslado)
        {
            if (is_taslado) 
            {
                rb_traslado.Checked = false;
                gb_traslado.Visible = false;
                tb_trasBase.Text = "";
                tb_trasImporte.Text = "";
                cb_trasImpuesto.DataSource = null;
                cb_trasTasa.DataSource = null;
            } 
            else 
            {
                rb_retenciones.Checked = false;
                gb_retencion.Visible = false;
                tb_retBase.Text = "";
                tb_retImporte.Text = "";
                cb_retImpuesto.DataSource = null;
                cb_retTasa.DataSource = null;
            }
            ds_impuesto.Clear();            
        }

        private void btn_aceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
