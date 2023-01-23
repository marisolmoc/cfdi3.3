using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using MySql.Data.MySqlClient;
using System.Xml;
using System.Collections;
using System.Configuration;
using System.IO;
using Chilkat;
using System.Xml.Schema;
using ThoughtWorks.QRCode;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Drawing.Imaging;
using Microsoft.Win32;

namespace SiscomCFDI
{
    public partial class Factura : Form
    {
        #region VARIABLES DE CONEXIÓN
        //Validando la conexion con Access y inicializando las variables del Query
        OleDbConnection conn;
        OleDbCommand com = new OleDbCommand();
        OleDbDataAdapter adapt = new OleDbDataAdapter();
        DataSet accsds = new DataSet();
        String Nombre;
        String RFC;

        String TipoFac;        

        DateTime Fecha;
        string BDPOS;

        //Creando comandos y conexion con MySql
        MySqlConnection con2;
        MySqlCommand com2 = new MySqlCommand();
        
        #endregion

        #region VARIABLES GLOBALES
        DataSet dsDatos;
        DataSet dsItems; 
        string servidor;
        string usuario;
        string passw;
        
        public string Fechaxml;

        public string TCmabio;
        public string Contrib;
        DataTable impuestos = new DataTable();
        public string Total;
        public string Subtotal;
        public string Impuesto;
        public string RetISR;
        public string RetIVA;
        public string RetIEPS;
        public string Ieps;
        
        public string ST;
        string strOriginal;
        string folio;
        string Moneda;
        string FormaPago;
        string MetodoPago;
        string UsoCFDI;
        string noCta;
        string ConPago;
        string Desc;
        double DescPorcentaje;
        string Pais;

        string ArchivoXML;
        string ArchivoXML_Timbrado;
        string RutaXML_Timbrado;
        string ArchivoPDF_CFDI;
        string PDFArchivoXML_Timbrado;
        string RutaXML;
        string RutaPDF;
        string Rpt_Fac;

        string vendedor;
        string CorreoCli;

        string Ruta_cbb;
        string TelEmi;
        string TelSucu;
        string TelCli;
        string DiasCredito;

        //Variables addendas
        string EmisorRI;
        string ReceptorRI;
        string NumProv;
        string FolioAddenda;
        string ProveEkt;
        string NumPedido;
        string NumProveedor;
        string NumRecepcion;
        string UUID;

        //Variables Retenciones Locales
        DataTable RetenList;
        bool RetenValue;
        string sumRetLocal;
        string totalRetLocal;

        //Variables Comprobante INE
        bool INEValue = false;
        string TipoProceso;
        string TipoComite;
        string TipoAmbito;
        string CveEntidad;
        string IdContabilidad;

        string cuentaPredial;

        bool hasDocsRelacionados;
        DataTable docRelacionados;
        string tipo_relacion;
        #endregion

        public Factura()
        {
            InitializeComponent();
        }

        private void btn_fSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }                

        #region METODOS DE CONEXIÓN A BD
        private void Factura_Load(object sender, EventArgs e)
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
            con2 = new MySqlConnection(queryCon);

            try
            {
                con2.Open();
                string busemp = "Select Clave from empresa where (Estatus = 1)";
                MySqlDataAdapter da = new MySqlDataAdapter(busemp, con2);
                DataSet ds = new DataSet();
                da.Fill(ds, "Clave");
                cb_ClaveEmp.DataSource = ds.Tables["Clave"].DefaultView;
                cb_ClaveEmp.DisplayMember = "Clave";

                //load Forma de pago
                string query = "SELECT * FROM cfdi.c_formapago;";
                MySqlDataAdapter mysqlda = new MySqlDataAdapter(query, con2);
                DataSet ds2 = new DataSet();                
                mysqlda.Fill(ds2, "c_formapago");
                cb_formapago.DisplayMember = "Descripcion";
                cb_formapago.ValueMember = "id_formapago";
                cb_formapago.DataSource = ds2.Tables["c_formapago"];
                cb_formapago.Text = "Seleccione..";
                                
                cb_usocfdi.Text = "Seleccione..";

                //load Metodo Pago
                string queryMetodo = "SELECT * FROM cfdi.c_metodopago;";
                MySqlDataAdapter mysqlda3 = new MySqlDataAdapter(queryMetodo, con2);                
                DataSet ds4 = new DataSet();
                mysqlda3.Fill(ds4, "c_metodopago");
                cb_metodoPago.DisplayMember = "Descripcion";
                cb_metodoPago.ValueMember = "id_metodopago";
                cb_metodoPago.DataSource = ds4.Tables["c_metodopago"];
                cb_metodoPago.Text = "Seleccione..";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar empresas. Consulte al Administrador del Sistema. \n" + ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con2.Close();
                com2.Dispose();
            }                
        }

        public void ConectPOS()
        {
            MySqlCommand comm = new MySqlCommand();
            MySqlDataAdapter dataA = new MySqlDataAdapter();
            DataSet dataS = new DataSet();
            
            try
            {                    
                con2.Open();
                comm.Connection = con2;
                comm.CommandText = "Select Nombre, Directorio_base, Contribuyente FROM empresa WHERE (Clave = " + cb_ClaveEmp.Text + ")";
                dataA.SelectCommand = comm;
                dataA.Fill(dataS, "POS");
                DataRow row = dataS.Tables["POS"].Rows[0];
                BDPOS = dataS.Tables["POS"].Rows[0]["Directorio_base"].ToString().Trim();
                lbl_Nombre.Text = dataS.Tables["POS"].Rows[0]["Nombre"].ToString().Trim();
                Contrib = dataS.Tables["POS"].Rows[0]["Contribuyente"].ToString().Trim();                    

                conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + BDPOS + "");
                conn.Open();
                com.Connection = conn;
                com.CommandText = "SELECT TipoCambioV FROM TipoCambio WHERE (TFecha = (SELECT MAX(TFecha) AS Expr1 FROM TipoCambio TipoCambio_1))";
                TCmabio = com.ExecuteScalar().ToString();
                tb_cambio.Text = TCmabio;

                com.CommandText = "SELECT MAX(Factura) AS Ultima FROM Fac_Cab WHERE (TipoM = '"+tb_tm.Text+"')";
                 tb_fFactura.Text = com.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al conectase a la Base de datos. Verifíque su conexión. \n"+ ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                com.Dispose();
                conn.Close();
                comm.Dispose();
                con2.Close();
            }            
        }
                
        private void TipoMov()
        {           
            MySqlDataAdapter mda = new MySqlDataAdapter();
            DataSet mds = new DataSet();
            this.lbl_Nombre.Text = "";
            TipoFac = null;

            if ((this.cb_ClaveEmp.Text == null) || (cb_ClaveEmp.Text == "") || cb_ClaveEmp.Text == "System.Data.DataRowView")
            {
            }
            else
            {
                try
                {
                    if (con2.State.ToString() == "Closed")
                        con2.Open();
                    com2.Connection = con2;
                    com2.CommandText = "SELECT Nombre, TM, TipoFac FROM empresa WHERE (Clave = " + this.cb_ClaveEmp.Text + ")";
                    mda.SelectCommand = com2;

                    mda.Fill(mds, "TM");

                    DataRow row = mds.Tables["TM"].Rows[0];
                    object TM = row["TM"].ToString();
                    this.tb_tm.Text = TM.ToString();
                    this.lbl_Nombre.Text = row["Nombre"].ToString();
                    TipoFac = row["TipoFac"].ToString();
                                        
                    gb_addenda.Visible = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error al buscar datos de la empresa.\n" + ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    com2.Dispose();
                    con2.Close();
                }
            }
        }
        #endregion

        #region METODOS PARA BUSCAR EMPRESA Y VALIDACIÓN
        
        private void cb_ClaveEmp_TextChanged(object sender, EventArgs e)
        {
            if ((cb_ClaveEmp.Text == "") || (cb_ClaveEmp.Text == null) || (cb_ClaveEmp.Text == "System.Data.DataRowView"))
            {
            }
            else
            {
                imprimir_btn.Enabled = false;
                btn_xml.Enabled = false;
                this.tb_tm.Text = "";
                Clear();
                TipoMov();
                ConectPOS();

                con2.Open();
                com2.Connection = con2;
                com2.CommandText = "SELECT COUNT(Folio) AS Suma FROM movimientos WHERE (Clave_Emp = " + cb_ClaveEmp.Text + ") AND (Estatus = 'D')";
                int sumFol = Convert.ToInt32(com2.ExecuteScalar());
                con2.Close();
                if (sumFol == 0)
                {
                    MessageBox.Show("No cuenta con folios para realizar transacciones, contacte a su proveedor de folios. \nEvite interrumpir su generación de facturas.", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();                    
                }
                else if (sumFol <= 20)
                {
                    MessageBox.Show("Sus Folios están por terminar, contacte a su proveedor de folios. \nEvite interrumpir su generación de facturas. \n Total Folios = " + sumFol.ToString(), "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        
        public void Clear()
        {
            dataGridView1.DataSource = null;
            //tb_fFactura.Clear();
            lbl_fnomb.Text = "";
            lbl_fFecha.Text = "";
            lbl_fRFC.Text = "";
            tb_subtotal.Clear();
            tb_iva.Clear();
            tb_total.Clear();
            tb_ieps.Text = "0.00";
            tb_rtIVA.Text = "0";
            tb_retISR.Text = "0";
            tb_rtIEPS.Text = "0";
            rb_addenda.Checked = false;
            tb_retLocales.Clear();
            tb_RetTotal.Clear();
            rb_INE.Checked = false;
            dg_impuestos.DataSource = null;
            cb_formapago.Text = "Seleccione..";
            cb_usocfdi.Text = "Seleccione..";
            cb_metodoPago.Text = "Seleccione..";
            tb_predial.Text = "";            
            imprimir_btn.Enabled = false;
            btn_xml.Enabled = false;
            btn_retLocales.Enabled = true;
            toolTip1.RemoveAll();
            CorreoCli = "";
            Nombre = null;
            RFC = null;
            Fecha = default(DateTime);
            this.lbl_nota2.Text = "";
            TelCli = "";
            label7.Text = "Total:";
            lbl_retLocales.Visible = false;
            lbl_RetTotal.Visible = false;
        }

        private void btn_buscar_Click(object sender, EventArgs e)
        {
            Clear();           

            if (rb_fac.Checked == true)
            {
                com.Connection = conn;
                com.CommandText = "SELECT Fac_Cab.Fecha, Fac_Cab.RFC, Fac_Cab.Nombre, Clientes.Def1, Clientes.Def2 FROM (Fac_Cab INNER JOIN Clientes ON Fac_Cab.Cliente = Clientes.Cliente) " +
                                  "WHERE (Fac_Cab.Factura = " + tb_fFactura.Text + ") AND (Fac_Cab.TipoM = '" + tb_tm.Text + "') AND (Fac_Cab.Status = False)";
                adapt.SelectCommand = com;

                if (adapt.Fill(accsds, "Factura") == 0)
                {
                    MessageBox.Show("El Numero de Factura no se ha encontrada o se encuentra cancelada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    adapt.Fill(accsds, "Factura");
                    DataTable fac;
                    fac = accsds.Tables["Factura"];

                    foreach (DataRow row in fac.Rows)                  
                    {
                        Nombre = row["Nombre"].ToString();
                        RFC = row["RFC"].ToString();
                        Fecha = (DateTime)row["Fecha"];                        
                        CorreoCli = row["Def2"].ToString();
                    }

                    this.lbl_fnomb.Text = Nombre;
                    this.lbl_fRFC.Text = RFC;
                    this.lbl_fFecha.Text = Fecha.ToString("dd/MM/yyyy");                    
                    DataGridLoad();
                    validarFac();
                }
            }
            else
            {
                com.Connection = conn;
                com.CommandText = "SELECT Fac_Cab.Fecha, Fac_Cab.RFC, Fac_Cab.Nombre, Clientes.Def1 FROM (Fac_Cab INNER JOIN Clientes ON Fac_Cab.Cliente = Clientes.Cliente) "+
                                  "WHERE (Fac_Cab.Factura = "+tb_fFactura.Text+") AND (Fac_Cab.TipoM = '"+tb_tm.Text+"') AND (Fac_Cab.Status = False)";
                adapt.SelectCommand = com;

                if (adapt.Fill(accsds, "Ticket") == 0)
                {                    
                    MessageBox.Show("Numero de ticket no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    adapt.Fill(accsds, "Ticket");
                    DataTable tick;
                    tick = accsds.Tables["Ticket"];
                    foreach (DataRow row in tick.Rows)
                    {
                        Nombre = row["Nombre"].ToString();
                        RFC = row["RFC"].ToString();
                        Fecha = (DateTime)row["Fecha"];                        
                    }

                    this.lbl_fnomb.Text = Nombre;
                    this.lbl_fRFC.Text = RFC;
                    this.lbl_fFecha.Text = Fecha.ToString("dd/MM/yyyy");
                    DataGridLoad();
                    validarFac();
                }
            }
            cb_metodoPago.SelectedIndex = 1;
            btn_enviar.Enabled = false;
            rb_dolares.Enabled = false;
            rb_pesos.Enabled = false;
        }

        public void validarFac()
        {            
            try
            {
                con2.Open();
                com2.Connection = con2;
                com2.CommandText = "Select Factura From movimientos Where (Clave_Emp = " + cb_ClaveEmp.Text + ") AND (Factura= " + this.tb_fFactura.Text + ") AND (Estatus = 'T')";
                object res = com2.ExecuteScalar();
                if (res == null)
                {
                    btn_xml.Enabled = true;
                    btn_retLocales.Enabled = true;
                }
                else if (res.ToString() == tb_fFactura.Text)
                {
                    btn_xml.Enabled = false;
                    btn_retLocales.Enabled = false;
                    toolTip1.SetToolTip(tb_fFactura, "La Factura ya se encuentra timbrada");                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Convert.ToString(ex));
            }
            finally
            {
                com2.Dispose();
                con2.Close();
            }
        }
        #endregion

        #region METODO PARA CARGAR GRID
        private void DataGridLoad()
        {            
            this.dataGridView1.DataSource = null;
            this.dg_impuestos.DataSource = null;
            
            impuestos = new DataTable();
            if (this.rb_tick.Checked == true)
                impuestos.Columns.Add("Folio");
            impuestos.Columns.Add("Código Producto");
            impuestos.Columns.Add("Tipo");
            impuestos.Columns.Add("Base");
            impuestos.Columns.Add("Impuesto");
            impuestos.Columns.Add("Factor");
            impuestos.Columns.Add("Tasa O Cuota");
            impuestos.Columns.Add("Importe");
            
            if(Contrib == "Física")
            {
                fisica();
            }
            else if (Contrib == "Moral")
            {
                moral();
            }
            
            conn.Close(); 
        }

        public void moral()
        {
            DataSet gvds = new DataSet();
            OleDbDataAdapter adpgv;
            OleDbCommand dgcom = new OleDbCommand();

            try
            {                
                conn.Open();
                dgcom.Connection = conn;
                OleDbDataAdapter datik;
                if (this.rb_tick.Checked == true)
                {
                    #region Moral ticket
                    dgcom.CommandText = "SELECT Venta_Fac.Folio, Venta_Fac.Codigo, Venta_Fac.Cantidad, Articulos.UnidadA as Unidad, Venta_Fac.Descripcion, Venta_Fac.SubTotal AS Precio, Venta_Fac.SubTotal AS Importe, ROUND((Venta_Fac.SubTotal * vm.Descuento2)/100,2)  as Descuento"
                                        + " FROM (((Fac_Cab INNER JOIN Venta_Fac ON Fac_Cab.Factura = Venta_Fac.Factura) INNER JOIN Articulos ON Articulos.Codigo = Venta_Fac.Codigo) INNER JOIN Venta_Mov as vm ON (vm.Folio = Venta_Fac.Folio AND vm.Codigo = Venta_Fac.Codigo ))"
                                        + " WHERE (Fac_Cab.TipoM = '" + this.tb_tm.Text + "') AND (Fac_Cab.Factura = " + this.tb_fFactura.Text + ")";

                    //Llenar textbox de total del los articulos de factura de ticket
                    string TotTicket = ("SELECT SUM(Venta_Fac.SubTotal) AS SubtotalTotal " +
                                        " FROM (Fac_Cab INNER JOIN Venta_Fac ON Fac_Cab.Factura = Venta_Fac.Factura)" +
                                        "WHERE (Fac_Cab.TipoM = '" + this.tb_tm.Text + "') AND (Fac_Cab.Factura = " + this.tb_fFactura.Text + ")");
                    
                    adpgv = new OleDbDataAdapter(dgcom);
                    adpgv.Fill(gvds, "Ticket");
                    
                    //CFDI v3.3
                    DataTable temp = gvds.Tables[0];
                    if (temp.Rows.Count == 0)
                    {
                        throw new Exception();
                    }
                    dataGridView1.DataSource = newColumns_datagridview(temp);
                    dataGridView1.CellClick += dataGridView1_CellClick;

                    
                    datik = new OleDbDataAdapter(TotTicket, conn);
                    
                    datik.Fill(gvds, "TTicket");
                    DataRow tikrow = gvds.Tables["TTicket"].Rows[0];
                    double subtotalTik = Convert.ToDouble(tikrow["SubtotalTotal"].ToString());
                    this.tb_subtotal.Text = Math.Round(subtotalTik, 2).ToString("N2");
                    ST = tb_subtotal.Text;

                    getTicketDescuento();
                    getImpuestos(temp, 0);
                    #endregion
                }
                else
                {
                    dgcom.CommandText = "SELECT Fac_Mov.Codigo, Fac_Mov.Cantidad, Fac_Mov.UnidadM As Unidad, Articulos.Descripcion, Round(Fac_Mov.Precio, 2) AS Precio, Round(Precio * Fac_Mov.Cantidad, 2) AS Importe " +
                                        "FROM (Articulos INNER JOIN Fac_Mov ON Articulos.Codigo = Fac_Mov.Codigo)" +
                                        "WHERE (Fac_Mov.Factura = " + this.tb_fFactura.Text + ") AND (Fac_Mov.TipoM = '" + this.tb_tm.Text + "') ORDER BY Fac_Mov.Renglon";

                    adpgv = new OleDbDataAdapter(dgcom);
                    adpgv.Fill(gvds, "Ticket");
                    //CFDI 3.3
                    DataTable temp = gvds.Tables[0];
                    dataGridView1.DataSource = newColumns_datagridview(temp);
                    dataGridView1.CellClick += dataGridView1_CellClick;

                    string query = ("SELECT SUM(ROUND(ROUND(Fac_Mov.Precio,2) * Fac_Mov.Cantidad,2)) AS Importe " +
                                        "FROM (Articulos INNER JOIN Fac_Mov ON Articulos.Codigo = Fac_Mov.Codigo) " +
                                        "WHERE (Fac_Mov.Factura = " + this.tb_fFactura.Text + ") AND (Fac_Mov.TipoM = '" + this.tb_tm.Text + "')");

                    OleDbDataAdapter da2 = new OleDbDataAdapter(query, conn);
                    DataSet ds2 = new DataSet();
                    da2.Fill(ds2, "Precios");

                    DataRow row2 = ds2.Tables["Precios"].Rows[0];
                    this.tb_subtotal.Text = Convert.ToDecimal(row2["Importe"]).ToString("N2");
                    
                    ST = row2["Importe"].ToString();
                    
                    getDescuento(temp);
                }

                string queryUso = "SELECT * FROM cfdi.c_usocfdi Where moral = true;";
                MySqlDataAdapter mysqlda2 = new MySqlDataAdapter(queryUso, con2);
                if(con2.State.ToString() == "Closed")
                    con2.Open();
                DataSet ds3 = new DataSet();
                if (con2.State.ToString() != "Closed")
                    con2.Close();
                mysqlda2.Fill(ds3, "c_usocfdi");
                cb_usocfdi.DisplayMember = "descripcion";
                cb_usocfdi.ValueMember = "id_usocfdi";
                cb_usocfdi.DataSource = ds3.Tables["c_usocfdi"];
                cb_usocfdi.Text = "Seleccione..";
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se ha econtrado la Factura, intentalo de nuevo. \n" + ex.Message.ToString(), "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dgcom.Dispose();
                com.Dispose();
                conn.Close();
            }

            if (rb_pesos.Checked == true)            
                Moneda = "MXN";            
            else
                Moneda = "USD";
        }

        public void fisica()
        {
            DataSet gvds = new DataSet();
            OleDbDataAdapter adpgv;            
            OleDbCommand dgcom = new OleDbCommand();
            
            try
            {
                conn.Open();
                dgcom.Connection = conn;
                OleDbDataAdapter datik;
                string TotTicket;
                if (this.rb_tick.Checked == true)
                {
                    #region Fisica Ticket

                    dgcom.CommandText = "SELECT Venta_Fac.Folio, Venta_Fac.Codigo, Venta_Fac.Cantidad, Articulos.UnidadA as Unidad, Venta_Fac.Descripcion, Venta_Fac.SubTotal AS Precio, Venta_Fac.SubTotal AS Importe, ROUND((Venta_Fac.SubTotal * vm.Descuento2)/100,2)  as Descuento"
                                        + " FROM (((Fac_Cab INNER JOIN Venta_Fac ON Fac_Cab.Factura = Venta_Fac.Factura) INNER JOIN Articulos ON Articulos.Codigo = Venta_Fac.Codigo) INNER JOIN Venta_Mov as vm ON (vm.Folio = Venta_Fac.Folio AND vm.Codigo = Venta_Fac.Codigo ))"
                                        + " WHERE (Fac_Cab.TipoM = '" + this.tb_tm.Text + "') AND (Fac_Cab.Factura = " + this.tb_fFactura.Text + ")";
                        
                    //Llenar textbox de total del los articulos de factura de ticket
                    TotTicket = ("SELECT SUM(Venta_Fac.SubTotal) AS SubtotalTotal " +
                                        " FROM (Fac_Cab INNER JOIN Venta_Fac ON Fac_Cab.Factura = Venta_Fac.Factura)" +
                                        "WHERE (Fac_Cab.TipoM = '" + this.tb_tm.Text + "') AND (Fac_Cab.Factura = " + this.tb_fFactura.Text + ")");

                    adpgv = new OleDbDataAdapter(dgcom);
                    adpgv.Fill(gvds, "Ticket");

                    //CFDI 3.3
                    DataTable temp = gvds.Tables[0];
                    if (temp.Rows.Count == 0)
                    {
                        throw new Exception();
                    }
                    dataGridView1.DataSource = newColumns_datagridview(temp);
                    dataGridView1.CellClick += dataGridView1_CellClick;
                                        
                    datik = new OleDbDataAdapter(TotTicket, conn);
                    datik.Fill(gvds, "TTicket");
                    DataRow tikrow = gvds.Tables["TTicket"].Rows[0];
                    double subtotalTiket = Convert.ToDouble(tikrow["SubtotalTotal"].ToString());
                    this.tb_subtotal.Text = Math.Round(subtotalTiket,2).ToString("N2");
                    ST = tb_subtotal.Text;

                    getTicketDescuento();
                    getImpuestos(temp, 0);
                    #endregion
                }
                else
                {
                    dgcom.CommandText = "SELECT Fac_Mov.Codigo, Fac_Mov.Cantidad, Fac_Mov.UnidadM As Unidad, Articulos.Descripcion, Round(Fac_Mov.Precio, 2) as Precio, Round(Precio * Fac_Mov.Cantidad, 2) AS Importe " + 
                                        "FROM (Articulos INNER JOIN Fac_Mov ON Articulos.Codigo = Fac_Mov.Codigo) " +
                                        "WHERE (Fac_Mov.Factura = " + this.tb_fFactura.Text + ") AND (Fac_Mov.TipoM = '" + this.tb_tm.Text + "') ORDER BY Fac_Mov.Renglon";
                 
                    adpgv = new OleDbDataAdapter(dgcom);
                    adpgv.Fill(gvds, "Ticket");
                    
                    //CFDI 3.3
                    DataTable temp = gvds.Tables[0];                    
                    dataGridView1.DataSource = newColumns_datagridview(temp);
                    dataGridView1.CellClick += dataGridView1_CellClick;

                    if (rb_dolares.Checked == true)
                    {
                        #region Dolares

                        string query = ("SELECT SUM(ROUND(ROUND(Fac_Mov.Precio,2) * Fac_Mov.Cantidad,2)) * " + this.tb_cambio.Text + " AS Importe " +
                                        " FROM (Articulos INNER JOIN Fac_Mov ON Articulos.Codigo = Fac_Mov.Codigo)" +
                                        " WHERE (Fac_Mov.Factura = " + this.tb_fFactura.Text + ") AND (Fac_Mov.TipoM = '" + this.tb_tm.Text + "')");
                                                        
                        OleDbDataAdapter da2 = new OleDbDataAdapter(query, conn);
                        DataSet ds2 = new DataSet();
                        da2.Fill(ds2, "Precios");

                        DataRow row2 = ds2.Tables["Precios"].Rows[0];
                        this.tb_subtotal.Text = Convert.ToDecimal(row2["Importe"]).ToString("N2");
                        
                        ST = row2["Importe"].ToString();                       
                        
                        lbl_nota2.Text = "*Los datos que a continuación se muestran han sido cambias a Moneda Nacional para su adecuada facturación";
                    #endregion
                    }
                    else
                    {
                        string query = ("SELECT SUM(ROUND(ROUND(Fac_Mov.Precio,2) * Fac_Mov.Cantidad,2)) AS Importe " + 
                                        "FROM (Articulos INNER JOIN Fac_Mov ON Articulos.Codigo = Fac_Mov.Codigo) " +
                                        "WHERE (Fac_Mov.Factura = " + this.tb_fFactura.Text + ") AND (Fac_Mov.TipoM = '" + this.tb_tm.Text + "')");

                        OleDbDataAdapter da2 = new OleDbDataAdapter(query, conn);
                        DataSet ds2 = new DataSet();
                        da2.Fill(ds2, "Precios");

                        DataRow row2 = ds2.Tables["Precios"].Rows[0];
                        this.tb_subtotal.Text = Convert.ToDecimal(row2["Importe"]).ToString("N2");
                        
                        ST = row2["Importe"].ToString();
                       
                    }
                    getDescuento(temp);
                }

                string queryUso = "SELECT * FROM cfdi.c_usocfdi Where fisica = true;";
                MySqlDataAdapter mysqlda2 = new MySqlDataAdapter(queryUso, con2);
                if (con2.State.ToString() == "Closed")
                    con2.Open();
                DataSet ds3 = new DataSet();
                if (con2.State.ToString() != "Closed")
                    con2.Close();
                mysqlda2.Fill(ds3, "c_usocfdi");
                cb_usocfdi.DisplayMember = "descripcion";
                cb_usocfdi.ValueMember = "id_usocfdi";
                cb_usocfdi.DataSource = ds3.Tables["c_usocfdi"];
                cb_usocfdi.Text = "Seleccione..";
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se ha econtrado la Factura, intentalo de nuevo.\n" + ex.Message.ToString(), "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dgcom.Dispose();
                com.Dispose();
                conn.Close();
            }

            Moneda = "MXN";
        }

        public void getDescuento(DataTable tempTable) 
        {
            //Calcualando el Descuento
            if (conn.State.ToString() == "Closed")
                conn.Open();
            com.Connection = conn;
            com.CommandText = "SELECT DescuentoG FROM Fac_Cab WHERE (Factura = " + this.tb_fFactura.Text + ") AND (TipoM = '" + this.tb_tm.Text + "')";
            string desc = com.ExecuteScalar().ToString();

            double sub = double.Parse(ST);
            DescPorcentaje = Convert.ToDouble(desc);
            double Descu = sub * DescPorcentaje / 100;
            Desc = Convert.ToString(Descu);
            tb_desc.Text = Convert.ToDouble(Desc).ToString("N2");
            
            getImpuestos(tempTable, DescPorcentaje);
        }

        public void getTicketDescuento() {

            if (conn.State.ToString() == "Closed")
                conn.Open();
            com.Connection = conn;
            com.CommandText = "SELECT SUM(ROUND( (Venta_Fac.SubTotal * vm.Descuento2 ) /100 , 2 )) FROM (Venta_Fac INNER JOIN Articulos ON Venta_Fac.Codigo = Articulos.Codigo) INNER JOIN Venta_Mov as vm ON vm.Folio = Venta_Fac.Folio AND vm.Codigo = Venta_Fac.Codigo "
                            + " WHERE (Venta_Fac.Factura = " + this.tb_fFactura.Text + ") AND (Venta_Fac.FacturaTM = '" + this.tb_tm.Text + "')";
            string desc = com.ExecuteScalar().ToString();
            Desc = Convert.ToString(desc);
            tb_desc.Text = Convert.ToDouble(desc).ToString("N2");
        }

        public DataTable newColumns_datagridview(DataTable tempTable)
        {
            DataTable temp = tempTable;
            temp.Columns.Add("Impuesto");
            temp.Columns.Add("Clave SAT", typeof(System.String));
            temp.Columns.Add("ClaveUnidad SAT", typeof(System.String));
            foreach (DataRow row in temp.Rows)
            {
                row["Impuesto"] = "Click para agregar Impuesto";

                string codigoProdPOS = row["Codigo"].ToString();

                MySqlDataAdapter dataAdap = new MySqlDataAdapter();
                DataSet dsTemp = new DataSet();
                con2.Open();
                com2.Connection = con2;
                com2.CommandText = "SELECT * FROM cfdi.productos_sat Where codigo_pos = '" + codigoProdPOS + "';";
                dataAdap.SelectCommand = com2;
                con2.Close();
                dataAdap.Fill(dsTemp, "Prod_SAT");

                if (dsTemp.Tables["Prod_SAT"].Rows.Count > 0)
                {
                    row["Clave SAT"] = dsTemp.Tables["Prod_SAT"].Rows[0]["codigo_sat"].ToString().Trim();
                    row["ClaveUnidad SAT"] = dsTemp.Tables["Prod_SAT"].Rows[0]["codigoUnidad_sat"].ToString().Trim();
                }
                else
                {
                    row["Clave SAT"] = "Click para agregar Clave SAT";
                    row["ClaveUnidad SAT"] = "Click para agregar Unidad SAT";
                }
            }
            return temp;
        }

        public void getImpuestos(DataTable TablaArticulos, double descuento) 
        {
            foreach (DataRow row in TablaArticulos.Rows)
            {
                string codigoProdPOS = row["Codigo"].ToString();
                string importePos = row["Importe"].ToString();
                string folio = "";
                double _descuento;
                if (this.rb_tick.Checked == true)
                {
                    folio = row["Folio"].ToString();
                    _descuento = Convert.ToDouble(row["Descuento"]);

                    if (_descuento > 0)
                    {
                        double _importePos = Convert.ToDouble(importePos);
                        //double calcDesc = (_importePos * _descuento) / 100;
                        importePos = (_importePos - _descuento).ToString();
                    }
                }
                defaultInpuestos(codigoProdPOS, importePos, folio);
            }
            calcularSubTotales(impuestos);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) 
        {
            if (e.RowIndex != -1)
            {
                string prod_codigo = dataGridView1[dataGridView1.Columns["Codigo"].Index, e.RowIndex].Value.ToString();
                string unidad_desc_pos = dataGridView1[dataGridView1.Columns["Unidad"].Index, e.RowIndex].Value.ToString();
                string prod_desc_pos = dataGridView1[dataGridView1.Columns["Descripcion"].Index, e.RowIndex].Value.ToString();
                double importe_pos = Convert.ToDouble(dataGridView1[dataGridView1.Columns["Importe"].Index, e.RowIndex].Value);
                
                int folio = 0;

                if (rb_tick.Checked == true)
                {
                    folio = Convert.ToInt32(dataGridView1[dataGridView1.Columns["Folio"].Index, e.RowIndex].Value.ToString());
                }
                
                if (e.ColumnIndex == dataGridView1.Columns["Clave SAT"].Index)
                {
                    Clave_SAT csat = new Clave_SAT(prod_desc_pos, true);
                    csat.ShowDialog();
                    dataGridView1[dataGridView1.Columns["Clave SAT"].Index, e.RowIndex].Value = csat.clave_sat;
                }
                if (e.ColumnIndex == dataGridView1.Columns["ClaveUnidad SAT"].Index)
                {
                    Clave_SAT csat = new Clave_SAT(unidad_desc_pos, false);
                    csat.ShowDialog();
                    dataGridView1[dataGridView1.Columns["ClaveUnidad SAT"].Index, e.RowIndex].Value = csat.claveUnidad_sat;
                }
                if (e.ColumnIndex == dataGridView1.Columns["Impuesto"].Index)
                {
                    impuestos = new DataTable();
                    bool isIngreso = true;
                    Impuestos_SAT impuesto = new Impuestos_SAT(importe_pos, prod_codigo, isIngreso, folio);
                    impuesto.ShowDialog();
                    impuestos = impuesto.impuestos_tabla;
                    dg_impuestos.Visible = true;
                    if (dg_impuestos.DataSource != null)
                    {
                        DataTable dtOld = (DataTable)dg_impuestos.DataSource;
                        foreach (DataRow dr in impuestos.Rows)
                        {
                            dtOld.Rows.Add(dr.ItemArray);
                        }
                        dg_impuestos.DataSource = dtOld;                        
                    }
                    else
                        dg_impuestos.DataSource = impuestos;

                    calcularSubTotales(impuestos);
                }
            }
        }

        public void calcularSubTotales(DataTable tablaimpuestos) 
        {
            if (tablaimpuestos != null) 
            {
                foreach (DataRow row in tablaimpuestos.Rows) 
                {
                    string tipo_impuesto = row["Tipo"].ToString();
                    string impuesto = row["Impuesto"].ToString();
                    decimal importe = Convert.ToDecimal(row["Importe"]);

                    if (tipo_impuesto == "Traslado")
                    {
                        if (impuesto.Contains("IVA"))
                        {
                            if (tb_iva.Text == String.Empty)
                                tb_iva.Text = importe.ToString("N2");
                            else
                                tb_iva.Text = (Convert.ToDecimal(tb_iva.Text)+ importe).ToString("N2");
                        }
                        else
                        {
                            if (tb_ieps.Text == String.Empty)
                                tb_ieps.Text = importe.ToString("N2");
                            else
                                tb_ieps.Text = (Convert.ToDecimal(tb_ieps.Text) + importe).ToString("N2");                                                        
                        }
                    }
                    else 
                    {
                        if (impuesto.Contains("IVA"))
                        {
                            if (tb_rtIVA.Text == "0")
                                tb_rtIVA.Text = importe.ToString("N2");
                            else
                                tb_rtIVA.Text = (Convert.ToDecimal(tb_rtIVA.Text) + importe).ToString("N2");
                        }
                        if (impuesto.Contains("ISR"))
                        {
                            if (tb_retISR.Text == "0")
                                tb_retISR.Text = importe.ToString("N2");
                            else
                                tb_retISR.Text = (Convert.ToDecimal(tb_retISR.Text) + importe).ToString("N2");
                        }
                        if (impuesto.Contains("IEPS")) 
                        {
                            if (tb_rtIEPS.Text == "0")
                                tb_rtIEPS.Text = importe.ToString("N2");
                            else
                                tb_rtIEPS.Text = (Convert.ToDecimal(tb_rtIEPS.Text) + importe).ToString("N2");
                        }
                    }
                }

                decimal _Total = ((Convert.ToDecimal(tb_subtotal.Text) - Convert.ToDecimal(tb_desc.Text)) + Convert.ToDecimal(tb_ieps.Text) + Convert.ToDecimal(tb_iva.Text))
                                 - Convert.ToDecimal(tb_retISR.Text) - Convert.ToDecimal(tb_rtIVA.Text) - Convert.ToDecimal(tb_rtIEPS.Text);
                tb_total.Text = _Total.ToString("N2");
            }
        }
        #endregion

        public void defaultInpuestos(string prod_codigo, string importe_pos, string folio) 
        {
            decimal _base = Convert.ToDecimal(importe_pos);
            decimal tasa = getArticuloIva(prod_codigo);
            decimal importe =  (Math.Round(_base, 2) * tasa);
            string _importe = (Math.Round(importe, 2)).ToString();

            object[] o = null;
            if(string.IsNullOrEmpty(folio))
            {
                o = new object[7];
                o[0] = prod_codigo;
                o[1] = "Traslado";
                o[2] = importe_pos;
                o[3] = "002 IVA";
                o[4] = "Tasa";
                o[5] = tasa.ToString();
                o[6] = _importe;
            }
            else
            {
                o = new object[8];
                o[0] = folio;
                o[1] = prod_codigo;
                o[2] = "Traslado";
                o[3] = importe_pos;
                o[4] = "002 IVA";
                o[5] = "Tasa";
                o[6] = tasa.ToString();
                o[7] = _importe;
            }
            
            impuestos.Rows.Add(o);
            dg_impuestos.DataSource = impuestos;            
        }
        
        public decimal getArticuloIva(string codigo)
        {
            if (conn.State.ToString() == "Closed")
                conn.Open();
            com.Connection = conn;
            com.CommandText = "SELECT Iva.IVA FROM Articulos INNER JOIN Iva ON Articulos.CIva = Iva.CIva Where Articulos.Codigo = '"+codigo+"'; ";

            string iva = "";
            object queryResult = com.ExecuteScalar();
            conn.Close();
            if (queryResult == null)
                iva = "16";
            else
                iva = queryResult.ToString();

            decimal articuloIva;
            decimal.TryParse(iva, out articuloIva);            
            articuloIva = articuloIva / 100;
            articuloIva += 0.000000m;
            
            return articuloIva;
        }

        private void btn_xml_Click(object sender, EventArgs e)
        {
            if (validarValores())
            {
                //Subtotal = this.tb_subtotal.Text;
                Impuesto = this.tb_iva.Text;
                Total = this.tb_total.Text;
                RetISR = this.tb_retISR.Text;
                RetIVA = this.tb_rtIVA.Text;
                RetIEPS = this.tb_rtIEPS.Text;
                folio = tb_fFactura.Text;
                Ieps = this.tb_ieps.Text;
                FormaPago = cb_formapago.SelectedValue.ToString();
                UsoCFDI = cb_usocfdi.SelectedValue.ToString();
                MetodoPago = cb_metodoPago.SelectedValue.ToString();

                string SelloDigital = "";
                string noCertificado = "";
                string Certificado = "";
                string ErrorCod, ErrorMsj;
                string Origen = "FACTURA";

                dsDatos = GetDatosDeFactura();
                dsItems = GetItemsDeFactura();

                sumRetLocal = tb_retLocales.Text;
                totalRetLocal = tb_RetTotal.Text;
                cuentaPredial = tb_predial.Text;
                TCmabio = tb_cambio.Text;

                noCertificado = get_noCertificacdo();
                strOriginal = CadenaOriginal(Origen, dsDatos, dsItems, Fechaxml, folio, noCertificado, TCmabio, Total, ST, Impuesto, Moneda, RetISR, RetIVA, FormaPago, ConPago, Desc, Pais, TipoFac, RetenValue, RetenList, sumRetLocal, totalRetLocal, Ieps, UsoCFDI, MetodoPago, cuentaPredial, hasDocsRelacionados, docRelacionados, tipo_relacion); //IEPS_bd,
                Sello(strOriginal, out SelloDigital, out Certificado, out ErrorCod, out ErrorMsj); //out noCertificado,

                #region Genera el XML

                MySqlDataAdapter mda = new MySqlDataAdapter();
                DataSet mds = new DataSet();
                con2.Open();
                com2.Connection = con2;
                com2.CommandText = "SELECT Ruta_fact, Ruta_NoTimb FROM empresa WHERE (Clave = " + this.cb_ClaveEmp.Text + ")";
                mda.SelectCommand = com2;
                mda.Fill(mds, "Rutas");
                con2.Close();

                DataRow row = mds.Tables["Rutas"].Rows[0];

                int i = 0;
                String PU = "";
                string RFC = dsDatos.Tables[0].Rows[0]["RFC"].ToString().Trim();
                string Serie = dsDatos.Tables[1].Rows[0]["Serie"].ToString().Trim();
                string Folio = "0000000000" + this.tb_fFactura.Text.Trim();
                Folio = Folio.Substring(Folio.Length - 10, 10);

                string RutaXML = @"" + row["Ruta_NoTimb"].ToString() + "";
                RutaXML_Timbrado = @"" + row["Ruta_fact"].ToString() + "";
                ArchivoXML = RFC + "_" + Serie + "_" + Folio + ".XML";
                ArchivoXML_Timbrado = RFC + "_" + Serie + "_" + Folio + "_Timbrado.XML";
                string ArchivoXML_ConError = RFC + "_" + Serie + "_" + Folio + "_Error.XML";

                #region Se define el XML de Tipo de Factura CFDI 3.3
                XmlDeclaration CCDeclaracionXML;
                XmlNode nCFDI;
                XmlNode nEmisor;
                XmlNode nReceptor;
                XmlNode nConceptos;
                XmlNode nConcepto;
                XmlNode nCuentaPredial;
                XmlNode nImpuestos;
                XmlNode nRetenciones;
                XmlNode nRetencion;
                XmlNode nTrasladados;
                XmlNode nTrasladado;
                XmlNode nComplemento;
                XmlNode nImpuestosLocales;
                XmlNode nRetencionesLocales;
                XmlNode nINE;
                XmlNode nINEEntidad;
                XmlNode nINEContabilidad;
                XmlNode nRelacionados; //TODO en Cadena Original/XML y Reporte
                XmlNode nRelacionado;

                string NameSpace = "http://www.sat.gob.mx/cfd/3";
                string Prefijo = "cfdi";

                XmlDocument CCXml = new XmlDocument();
                CCDeclaracionXML = CCXml.CreateXmlDeclaration("1.0", "UTF-8", "");

                //Add the new node to the document.
                XmlElement root = CCXml.DocumentElement;
                CCXml.InsertBefore(CCDeclaracionXML, root);

                #region Se define el nodo Comprobante
                nCFDI = CCXml.CreateNode(XmlNodeType.Element, "Comprobante", NameSpace);
                nCFDI.Prefix = Prefijo;

                XmlAttribute xsi;
                string atributo1 = "xsi:schemaLocation";
                string valor1 = "http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd";
                if (INEValue == true)
                {
                    valor1 += " http://www.sat.gob.mx/ine http://www.sat.gob.mx/sitio_internet/cfd/ine/ine11.xsd";
                }
                if (RetenValue == true)
                {
                    valor1 += " http://www.sat.gob.mx/implocal http://www.sat.gob.mx/sitio_internet/cfd/implocal/implocal.xsd";
                }

                xsi = CCXml.CreateAttribute(atributo1, Name);
                xsi.Value = valor1;
                nCFDI.Attributes.Append(xsi);

                #region Atributos al nodo cfdi:Comprobante
                AgregaAtributo("S", "C", ref CCXml, ref nCFDI, "xmlns:cfdi", "http://www.sat.gob.mx/cfd/3", "");
                AgregaAtributo("S", "C", ref CCXml, ref nCFDI, "xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance", "");
                if (INEValue == true)
                {
                    AgregaAtributo("S", "C", ref CCXml, ref nCFDI, "xmlns:ine", "http://www.sat.gob.mx/ine", "");
                }
                if (RetenValue == true)
                {
                    AgregaAtributo("S", "C", ref CCXml, ref nCFDI, "xmlns:implocal", "http://www.sat.gob.mx/implocal", "");
                }
                AgregaAtributo("S", "N", ref CCXml, ref nCFDI, "Version", "3.3", "");
                AgregaAtributo("N", "C", ref CCXml, ref nCFDI, "Serie", dsDatos.Tables[1].Rows[0]["Serie"].ToString(), "");
                AgregaAtributo("N", "N", ref CCXml, ref nCFDI, "Folio", this.tb_fFactura.Text, "");
                AgregaAtributo("S", "D", ref CCXml, ref nCFDI, "Fecha", Fechaxml, "");
                AgregaAtributo("S", "C", ref CCXml, ref nCFDI, "FormaPago", cb_formapago.SelectedValue.ToString(), "");
                AgregaAtributo("S", "N", ref CCXml, ref nCFDI, "NoCertificado", noCertificado, "");
                AgregaAtributo("N", "C", ref CCXml, ref nCFDI, "CondicionesDePago", ConPago, "");
                AgregaAtributo("S", "$", ref CCXml, ref nCFDI, "SubTotal", ST, ""); //this.tb_subtotal.Text
                AgregaAtributo("N", "$", ref CCXml, ref nCFDI, "Descuento", Desc, "");
                AgregaAtributo("N", "C", ref CCXml, ref nCFDI, "Moneda", Moneda, "");
                if (Moneda != "MXN")
                    AgregaAtributo("N", "$", ref CCXml, ref nCFDI, "TipoCambio", TCmabio, "");

                if (RetenValue == true)
                { AgregaAtributo("S", "$", ref CCXml, ref nCFDI, "Total", this.tb_RetTotal.Text, ""); }
                else
                    AgregaAtributo("S", "$", ref CCXml, ref nCFDI, "Total", this.tb_total.Text, "");

                AgregaAtributo("S", "C", ref CCXml, ref nCFDI, "TipoDeComprobante", "I", "");
                AgregaAtributo("N", "C", ref CCXml, ref nCFDI, "MetodoPago", cb_metodoPago.SelectedValue.ToString(), ""); //CFDI 3.3 Pago en una sola exhibicion=PUE                    
                AgregaAtributo("N", "C", ref CCXml, ref nCFDI, "LugarExpedicion", dsDatos.Tables[0].Rows[0]["sCP"].ToString(), ""); // CFDI 3.3 Codigo postal sucursal
                AgregaAtributo("S", "C", ref CCXml, ref nCFDI, "Sello", SelloDigital, "");
                AgregaAtributo("S", "C", ref CCXml, ref nCFDI, "Certificado", Certificado, "");

                #endregion
                #region Se agrega el nodo RELACIONADO al nodo nCFDI
                if (hasDocsRelacionados)
                {                    
                    nRelacionados = CCXml.CreateNode(XmlNodeType.Element, "CfdiRelacionados", NameSpace);
                    nRelacionados.Prefix = Prefijo;
                    AgregaAtributo("S", "C", ref CCXml, ref nRelacionados, "TipoRelacion", tipo_relacion, "");
                    foreach (DataRow _docs in docRelacionados.Rows)
                    {
                        nRelacionado = CCXml.CreateNode(XmlNodeType.Element, "CfdiRelacionado", NameSpace);
                        nRelacionado.Prefix = Prefijo;
                        AgregaAtributo("S", "C", ref CCXml, ref nRelacionado, "UUID", _docs["UUID"].ToString(), "");
                        nRelacionados.AppendChild(nRelacionado);
                    }

                    nCFDI.AppendChild(nRelacionados);                    
                }
                #endregion

                #region Se agrega el nodo EMISOR al nodo nCFDI
                nEmisor = CCXml.CreateNode(XmlNodeType.Element, "Emisor", NameSpace);
                nEmisor.Prefix = Prefijo;

                AgregaAtributo("S", "C", ref CCXml, ref nEmisor, "Rfc", dsDatos.Tables[0].Rows[0]["RFC"].ToString(), "");
                AgregaAtributo("S", "C", ref CCXml, ref nEmisor, "Nombre", dsDatos.Tables[0].Rows[0]["Nombre"].ToString(), "");
                AgregaAtributo("S", "C", ref CCXml, ref nEmisor, "RegimenFiscal", dsDatos.Tables[0].Rows[0]["RegFiscal"].ToString(), "");

                nCFDI.AppendChild(nEmisor);
                #endregion

                #region Se agrega el nodo RECEPTOR al nodo nCFDI
                nReceptor = CCXml.CreateNode(XmlNodeType.Element, "Receptor", NameSpace);
                nReceptor.Prefix = Prefijo;

                AgregaAtributo("S", "C", ref CCXml, ref nReceptor, "Rfc", dsDatos.Tables[2].Rows[0]["RFC"].ToString().Replace("-", ""), "");
                AgregaAtributo("N", "C", ref CCXml, ref nReceptor, "Nombre", dsDatos.Tables[2].Rows[0]["Nombre"].ToString(), "");
                AgregaAtributo("S", "C", ref CCXml, ref nReceptor, "UsoCFDI", cb_usocfdi.SelectedValue.ToString(), "");

                nCFDI.AppendChild(nReceptor);
                #endregion

                #region Se agrega el nodo CONCEPTOS al nodo nCFDI
                nConceptos = CCXml.CreateNode(XmlNodeType.Element, "Conceptos", NameSpace);
                nConceptos.Prefix = Prefijo;

                if (Origen == "FACTURA")
                {
                    for (i = 0; i <= dsItems.Tables[0].Rows.Count - 1; i++)
                    {
                        //Se agrega el nodo CONCEPTO al nodo CONCEPTOS
                        nConcepto = CCXml.CreateNode(XmlNodeType.Element, "Concepto", NameSpace);
                        nConcepto.Prefix = Prefijo;

                        PU = string.Format("{0:0.00}", Convert.ToDouble(dsItems.Tables[0].Rows[i]["Importe"].ToString()) / Convert.ToDouble(dsItems.Tables[0].Rows[i]["Cantidad"].ToString()));

                        //CFDI 3.3
                        AgregaAtributo("S", "C", ref CCXml, ref nConcepto, "ClaveProdServ", dsItems.Tables[0].Rows[i]["Clave_SAT"].ToString(), "");
                        AgregaAtributo("N", "C", ref CCXml, ref nConcepto, "NoIdentificacion", dsItems.Tables[0].Rows[i]["Codigo"].ToString(), "");
                        AgregaAtributo("S", "N", ref CCXml, ref nConcepto, "Cantidad", dsItems.Tables[0].Rows[i]["Cantidad"].ToString(), "");
                        AgregaAtributo("S", "C", ref CCXml, ref nConcepto, "ClaveUnidad", dsItems.Tables[0].Rows[i]["claveUnidad_SAT"].ToString(), "");
                        AgregaAtributo("N", "C", ref CCXml, ref nConcepto, "Unidad", dsItems.Tables[0].Rows[i]["UnidadM"].ToString(), "");
                        AgregaAtributo("S", "C", ref CCXml, ref nConcepto, "Descripcion", dsItems.Tables[0].Rows[i]["Descripcion"].ToString(), "");
                        AgregaAtributo("S", "$", ref CCXml, ref nConcepto, "ValorUnitario", PU, "");
                        AgregaAtributo("S", "$", ref CCXml, ref nConcepto, "Importe", dsItems.Tables[0].Rows[i]["Importe"].ToString(), "");
                        if (rb_tick.Checked == true)
                        {
                            double _descuento = Convert.ToDouble(dsItems.Tables[0].Rows[i]["Descuento"]);
                            if(_descuento > 0)
                                AgregaAtributo("N", "$", ref CCXml, ref nConcepto, "Descuento", _descuento.ToString(), "");
                        }
                        else
                        {
                            if (DescPorcentaje > 0)
                                AgregaAtributo("N", "$", ref CCXml, ref nConcepto, "Descuento", dsItems.Tables[0].Rows[i]["Descuento"].ToString(), "");
                        }
                        //CFDI 3.3 Se agrega Nodo Impuestos por cada Concepto
                        #region Se agrega el nodo IMPUESTOS al nodo nCFDI
                        nImpuestos = CCXml.CreateNode(XmlNodeType.Element, "Impuestos", NameSpace);
                        nImpuestos.Prefix = Prefijo;

                        #region Se agrega el nodo Trasladados al nodo Impuestos                        
                        string prodCodigo = dsItems.Tables[0].Rows[i]["Codigo"].ToString();
                        string importe_base = dsItems.Tables[0].Rows[i]["Importe"].ToString();

                        if (existeTraslado(dg_impuestos, prodCodigo, importe_base))
                        {
                            nTrasladados = CCXml.CreateNode(XmlNodeType.Element, "Traslados", NameSpace);
                            nTrasladados.Prefix = Prefijo;
                            //Se agrega el nodo Trasladado al nodo Trasladadoss
                            foreach (DataGridViewRow imp_row in dg_impuestos.Rows)
                            {
                                
                                string folio_tick = "";

                                if (this.rb_tick.Checked == true)
                                {
                                    folio_tick = dsItems.Tables[0].Rows[i]["Folio"].ToString();

                                    if (folio_tick == imp_row.Cells[0].Value.ToString() && prodCodigo == imp_row.Cells[1].Value.ToString() && imp_row.Cells[2].Value.ToString() == "Traslado")
                                    {
                                        string _impuestoCodigo = imp_row.Cells[4].Value.ToString().Substring(0, 3);
                                        nTrasladado = CCXml.CreateNode(XmlNodeType.Element, "Traslado", NameSpace);
                                        nTrasladado.Prefix = Prefijo;

                                        AgregaAtributo("S", "$", ref CCXml, ref nTrasladado, "Base", imp_row.Cells[3].Value.ToString(), "");
                                        AgregaAtributo("S", "C", ref CCXml, ref nTrasladado, "Impuesto", _impuestoCodigo, "");
                                        AgregaAtributo("S", "C", ref CCXml, ref nTrasladado, "TipoFactor", imp_row.Cells[5].Value.ToString(), "");
                                        AgregaAtributo("S", "C", ref CCXml, ref nTrasladado, "TasaOCuota", imp_row.Cells[6].Value.ToString(), "");
                                        AgregaAtributo("S", "$", ref CCXml, ref nTrasladado, "Importe", imp_row.Cells[7].Value.ToString(), "");

                                        nTrasladados.AppendChild(nTrasladado);
                                    }
                                    else
                                    { continue; }
                                }
                                else
                                {
                                    if (importe_base == imp_row.Cells[2].Value.ToString() && prodCodigo == imp_row.Cells[0].Value.ToString() && imp_row.Cells[1].Value.ToString() == "Traslado")
                                    {
                                        string _impuestoCodigo = imp_row.Cells[3].Value.ToString().Substring(0, 3);
                                        nTrasladado = CCXml.CreateNode(XmlNodeType.Element, "Traslado", NameSpace);
                                        nTrasladado.Prefix = Prefijo;

                                        AgregaAtributo("S", "$", ref CCXml, ref nTrasladado, "Base", imp_row.Cells[2].Value.ToString(), "");
                                        AgregaAtributo("S", "C", ref CCXml, ref nTrasladado, "Impuesto", _impuestoCodigo, "");
                                        AgregaAtributo("S", "C", ref CCXml, ref nTrasladado, "TipoFactor", imp_row.Cells[4].Value.ToString(), "");
                                        AgregaAtributo("S", "C", ref CCXml, ref nTrasladado, "TasaOCuota", imp_row.Cells[5].Value.ToString(), "");
                                        AgregaAtributo("S", "$", ref CCXml, ref nTrasladado, "Importe", imp_row.Cells[6].Value.ToString(), "");

                                        nTrasladados.AppendChild(nTrasladado);
                                    }
                                    else 
                                    { continue; }
                                }
                            }
                            nImpuestos.AppendChild(nTrasladados);
                        }
                        #endregion

                        #region Se agrega el nodo Retenciones al nodo Impuestos
                        if (existeRetencion(dg_impuestos, prodCodigo, importe_base))
                        {
                            nRetenciones = CCXml.CreateNode(XmlNodeType.Element, "Retenciones", NameSpace);
                            nRetenciones.Prefix = Prefijo;
                            //Se agrega el nodo Retencion al nodo Retenciones
                            foreach (DataGridViewRow ret_row in dg_impuestos.Rows)
                            {
                                //string prodCodigo = dsItems.Tables[0].Rows[i]["Codigo"].ToString();
                                //string importe_base = dsItems.Tables[0].Rows[i]["Importe"].ToString();
                                string folio_tick = "";

                                if (this.rb_tick.Checked == true)
                                {
                                    folio_tick = dsItems.Tables[0].Rows[i]["Folio"].ToString();

                                    if (folio_tick == ret_row.Cells[0].Value.ToString() && prodCodigo == ret_row.Cells[1].Value.ToString() && ret_row.Cells[2].Value.ToString() == "Retención")
                                    {
                                        string _impuestoCodigo = ret_row.Cells[4].Value.ToString().Substring(0, 3);
                                        nRetencion = CCXml.CreateNode(XmlNodeType.Element, "Retencion", NameSpace);
                                        nRetencion.Prefix = Prefijo;

                                        AgregaAtributo("S", "$", ref CCXml, ref nRetencion, "Base", ret_row.Cells[3].Value.ToString(), "");
                                        AgregaAtributo("S", "C", ref CCXml, ref nRetencion, "Impuesto", _impuestoCodigo, "");
                                        AgregaAtributo("S", "C", ref CCXml, ref nRetencion, "TipoFactor", ret_row.Cells[5].Value.ToString(), "");
                                        AgregaAtributo("S", "C", ref CCXml, ref nRetencion, "TasaOCuota", ret_row.Cells[6].Value.ToString(), "");
                                        AgregaAtributo("S", "$", ref CCXml, ref nRetencion, "Importe", ret_row.Cells[7].Value.ToString(), "");

                                        nRetenciones.AppendChild(nRetencion);
                                    }
                                    else
                                    { continue; }
                                }
                                else
                                {
                                    if (importe_base == ret_row.Cells[2].Value.ToString() && prodCodigo == ret_row.Cells[0].Value.ToString() && ret_row.Cells[1].Value.ToString() == "Retención")
                                    {
                                        string _impuestoCodigo = ret_row.Cells[3].Value.ToString().Substring(0, 3);
                                        nRetencion = CCXml.CreateNode(XmlNodeType.Element, "Retencion", NameSpace);
                                        nRetencion.Prefix = Prefijo;

                                        AgregaAtributo("S", "$", ref CCXml, ref nRetencion, "Base", ret_row.Cells[2].Value.ToString(), "");
                                        AgregaAtributo("S", "C", ref CCXml, ref nRetencion, "Impuesto", _impuestoCodigo, "");
                                        AgregaAtributo("S", "C", ref CCXml, ref nRetencion, "TipoFactor", ret_row.Cells[4].Value.ToString(), "");
                                        AgregaAtributo("S", "C", ref CCXml, ref nRetencion, "TasaOCuota", ret_row.Cells[5].Value.ToString(), "");
                                        AgregaAtributo("S", "$", ref CCXml, ref nRetencion, "Importe", ret_row.Cells[6].Value.ToString(), "");
                                        
                                        nRetenciones.AppendChild(nRetencion);
                                    }
                                }
                            }
                            nImpuestos.AppendChild(nRetenciones);
                        }
                        #endregion

                        nConcepto.AppendChild(nImpuestos);
                        #endregion

                        if (tb_predial.Text != null && tb_predial.Text != String.Empty)
                        {
                            nCuentaPredial = CCXml.CreateNode(XmlNodeType.Element, "CuentaPredial", NameSpace);
                            nCuentaPredial.Prefix = Prefijo;

                            AgregaAtributo("S", "N", ref CCXml, ref nCuentaPredial, "Numero", tb_predial.Text, "");

                            nConcepto.AppendChild(nCuentaPredial);
                        }

                        nConceptos.AppendChild(nConcepto);
                    }
                }
                else
                {
                    //Se agrega el nodo CONCEPTO al nodo CONCEPTOS
                    nConcepto = CCXml.CreateNode(XmlNodeType.Element, "Concepto", NameSpace);
                    nConcepto.Prefix = Prefijo;
                    //CFDI 3.3
                    AgregaAtributo("S", "C", ref CCXml, ref nConcepto, "ClaveProdServ", "01010101", "");
                    AgregaAtributo("N", "C", ref CCXml, ref nConcepto, "NoIdentificacion", "", "");
                    AgregaAtributo("S", "N", ref CCXml, ref nConcepto, "Cantidad", "1", "");
                    AgregaAtributo("S", "C", ref CCXml, ref nConcepto, "ClaveUnidad", "XNA", "");
                    AgregaAtributo("N", "C", ref CCXml, ref nConcepto, "Unidad", "PZ", "");
                    AgregaAtributo("S", "C", ref CCXml, ref nConcepto, "Descripcion", dsDatos.Tables[0].Rows[0]["Descripcion"].ToString(), "");
                    AgregaAtributo("S", "$", ref CCXml, ref nConcepto, "ValorUnitario", dsDatos.Tables[0].Rows[0]["Precio"].ToString(), "");
                    AgregaAtributo("S", "$", ref CCXml, ref nConcepto, "Importe", dsDatos.Tables[0].Rows[0]["Importe"].ToString(), "");

                    nConceptos.AppendChild(nConcepto);
                }

                nCFDI.AppendChild(nConceptos);
                #endregion

                #region Se agrega el nodo IMPUESTOS TOTALES al nodo nCFDI
                nImpuestos = CCXml.CreateNode(XmlNodeType.Element, "Impuestos", NameSpace);
                nImpuestos.Prefix = Prefijo;


                #region Se agrega el nodo Retenciones al nodo Impuestos
                if (existeRetencion(dg_impuestos, "", ""))
                {
                    double TotalRetenido = Convert.ToDouble(tb_rtIVA.Text) + Convert.ToDouble(tb_rtIEPS.Text) + Convert.ToDouble(tb_retISR.Text);
                    AgregaAtributo("S", "$", ref CCXml, ref nImpuestos, "TotalImpuestosRetenidos", string.Format("{0:0.00}", TotalRetenido), "");

                    nRetenciones = CCXml.CreateNode(XmlNodeType.Element, "Retenciones", NameSpace);
                    nRetenciones.Prefix = Prefijo;

                    //Se agrega el nodo Retencion al nodo Retenciones
                    string impCodigoPrevio = "-1";
                    List<string> tasa_lista = new List<string>();

                    foreach (DataGridViewRow ret_row in dg_impuestos.Rows)
                    {
                        if (this.rb_tick.Checked == true)
                        {
                            if (ret_row.Cells[2].Value.ToString() == "Retención")
                            {
                                string _impuestoCodigo = ret_row.Cells[4].Value.ToString().Substring(0, 3);
                                string _tasa = ret_row.Cells[6].Value.ToString();

                                if (!tasa_lista.Contains(_impuestoCodigo + "-" + _tasa))
                                {
                                    if (_tasa == "0.000000")
                                    {
                                        nRetencion = CCXml.CreateNode(XmlNodeType.Element, "Retencion", NameSpace);
                                        nRetencion.Prefix = Prefijo;

                                        AgregaAtributo("S", "C", ref CCXml, ref nRetencion, "Impuesto", _impuestoCodigo, "");
                                        AgregaAtributo("S", "$", ref CCXml, ref nRetencion, "Importe", "0.00", "");

                                        nRetenciones.AppendChild(nRetencion);
                                        tasa_lista.Add(_impuestoCodigo + "-" + _tasa);
                                        continue;
                                    }
                                                                        
                                    decimal sum_ret_importe = TotalImpuestos("Retención", _impuestoCodigo, dg_impuestos);
                                    nRetencion = CCXml.CreateNode(XmlNodeType.Element, "Retencion", NameSpace);
                                    nRetencion.Prefix = Prefijo;

                                    AgregaAtributo("S", "C", ref CCXml, ref nRetencion, "Impuesto", _impuestoCodigo, "");
                                    AgregaAtributo("S", "$", ref CCXml, ref nRetencion, "Importe", sum_ret_importe.ToString(), "");

                                    nRetenciones.AppendChild(nRetencion);
                                    
                                    tasa_lista.Add(_impuestoCodigo + "-" + _tasa);
                                }
                            }
                        }
                        else
                        {
                            if (ret_row.Cells[1].Value.ToString() == "Retención")
                            {
                                string _impuestoCodigo = ret_row.Cells[3].Value.ToString().Substring(0, 3);
                                string _tasa = ret_row.Cells[5].Value.ToString();

                                if (!tasa_lista.Contains(_impuestoCodigo + "-" + _tasa))
                                {
                                    if (_tasa == "0.000000")
                                    {
                                        nRetencion = CCXml.CreateNode(XmlNodeType.Element, "Retencion", NameSpace);
                                        nRetencion.Prefix = Prefijo;

                                        AgregaAtributo("S", "C", ref CCXml, ref nRetencion, "Impuesto", _impuestoCodigo, "");
                                        AgregaAtributo("S", "$", ref CCXml, ref nRetencion, "Importe","0.00", "");

                                        nRetenciones.AppendChild(nRetencion);
                                        tasa_lista.Add(_impuestoCodigo + "-" + _tasa);
                                        continue;
                                    }

                                    decimal sum_ret_importe = TotalImpuestos("Retención", _impuestoCodigo, dg_impuestos);
                                    nRetencion = CCXml.CreateNode(XmlNodeType.Element, "Retencion", NameSpace);
                                    nRetencion.Prefix = Prefijo;

                                    AgregaAtributo("S", "C", ref CCXml, ref nRetencion, "Impuesto", _impuestoCodigo, "");
                                    AgregaAtributo("S", "$", ref CCXml, ref nRetencion, "Importe", sum_ret_importe.ToString(), "");

                                    nRetenciones.AppendChild(nRetencion);
                                    
                                    tasa_lista.Add(_impuestoCodigo + "-" + _tasa);
                                }
                            }
                        }
                    }

                    nImpuestos.AppendChild(nRetenciones);
                }
                #endregion

                #region Se agrega el nodo Trasladados al nodo Impuestos
                if (existeTraslado(dg_impuestos, "",""))
                {
                    double TotalTrasladado = Convert.ToDouble(tb_ieps.Text) + Convert.ToDouble(tb_iva.Text);
                    AgregaAtributo("S", "$", ref CCXml, ref nImpuestos, "TotalImpuestosTrasladados", string.Format("{0:0.00}", TotalTrasladado), "");

                    nTrasladados = CCXml.CreateNode(XmlNodeType.Element, "Traslados", NameSpace);
                    nTrasladados.Prefix = Prefijo;

                    //Se agrega el nodo Trasladado al nodo Trasladados
                    string impCodigoPrevio = "-1";
                    List<string> tasa_lista = new List<string>();

                    foreach (DataGridViewRow _row in dg_impuestos.Rows)
                    {
                        if (this.rb_tick.Checked == true)
                        {
                            if (_row.Cells[2].Value.ToString() == "Traslado")
                            {
                                string _impuestoCodigo = _row.Cells[4].Value.ToString().Substring(0, 3);
                                string _tasa = _row.Cells[6].Value.ToString();

                                if (!tasa_lista.Contains(_impuestoCodigo + "-" + _tasa))
                                {
                                    if (_tasa == "0.000000")
                                    {
                                        nTrasladado = CCXml.CreateNode(XmlNodeType.Element, "Traslado", NameSpace);
                                        nTrasladado.Prefix = Prefijo;

                                        AgregaAtributo("S", "C", ref CCXml, ref nTrasladado, "Impuesto", _impuestoCodigo, "");
                                        AgregaAtributo("S", "C", ref CCXml, ref nTrasladado, "TipoFactor", _row.Cells[5].Value.ToString(), "");
                                        AgregaAtributo("S", "C", ref CCXml, ref nTrasladado, "TasaOCuota", _row.Cells[6].Value.ToString(), "");
                                        AgregaAtributo("S", "$", ref CCXml, ref nTrasladado, "Importe", "0.00", "");

                                        nTrasladados.AppendChild(nTrasladado);
                                        tasa_lista.Add(_impuestoCodigo + "-" + _tasa);
                                        continue;
                                    }

                                    decimal sum_tras_importe = TotalImpuestos("Traslado", _impuestoCodigo, dg_impuestos);

                                    nTrasladado = CCXml.CreateNode(XmlNodeType.Element, "Traslado", NameSpace);
                                    nTrasladado.Prefix = Prefijo;

                                    AgregaAtributo("S", "C", ref CCXml, ref nTrasladado, "Impuesto", _impuestoCodigo, "");
                                    AgregaAtributo("S", "C", ref CCXml, ref nTrasladado, "TipoFactor", _row.Cells[5].Value.ToString(), "");
                                    AgregaAtributo("S", "C", ref CCXml, ref nTrasladado, "TasaOCuota", _row.Cells[6].Value.ToString(), "");
                                    AgregaAtributo("S", "$", ref CCXml, ref nTrasladado, "Importe", sum_tras_importe.ToString(), "");

                                    nTrasladados.AppendChild(nTrasladado);
                                    
                                    tasa_lista.Add(_impuestoCodigo + "-" + _tasa);
                                }
                            }
                        }
                        else
                        {
                            if (_row.Cells[1].Value.ToString() == "Traslado")
                            {
                                string _impuestoCodigo = _row.Cells[3].Value.ToString().Substring(0, 3);
                                string _tasa = _row.Cells[5].Value.ToString();
                                
                                if (!tasa_lista.Contains(_impuestoCodigo + "-" + _tasa))
                                {
                                    if (_tasa == "0.000000")
                                    {
                                        nTrasladado = CCXml.CreateNode(XmlNodeType.Element, "Traslado", NameSpace);
                                        nTrasladado.Prefix = Prefijo;

                                        AgregaAtributo("S", "C", ref CCXml, ref nTrasladado, "Impuesto", _impuestoCodigo, "");
                                        AgregaAtributo("S", "C", ref CCXml, ref nTrasladado, "TipoFactor", _row.Cells[4].Value.ToString(), "");
                                        AgregaAtributo("S", "C", ref CCXml, ref nTrasladado, "TasaOCuota", _row.Cells[5].Value.ToString(), "");
                                        AgregaAtributo("S", "$", ref CCXml, ref nTrasladado, "Importe", "0.00", "");

                                        nTrasladados.AppendChild(nTrasladado);
                                        tasa_lista.Add(_impuestoCodigo + "-" + _tasa);
                                        continue;
                                    }

                                    decimal sum_tras_importe = TotalImpuestos("Traslado", _impuestoCodigo, dg_impuestos);

                                    nTrasladado = CCXml.CreateNode(XmlNodeType.Element, "Traslado", NameSpace);
                                    nTrasladado.Prefix = Prefijo;

                                    AgregaAtributo("S", "C", ref CCXml, ref nTrasladado, "Impuesto", _impuestoCodigo, "");
                                    AgregaAtributo("S", "C", ref CCXml, ref nTrasladado, "TipoFactor", _row.Cells[4].Value.ToString(), "");
                                    AgregaAtributo("S", "C", ref CCXml, ref nTrasladado, "TasaOCuota", _row.Cells[5].Value.ToString(), "");
                                    AgregaAtributo("S", "$", ref CCXml, ref nTrasladado, "Importe", sum_tras_importe.ToString(), "");

                                    nTrasladados.AppendChild(nTrasladado);
                                    
                                    tasa_lista.Add(_impuestoCodigo + "-" + _tasa);
                                }
                            }
                        }
                    }
                    nImpuestos.AppendChild(nTrasladados);
                }
                #endregion

                nCFDI.AppendChild(nImpuestos);
                #endregion

                #region Se agrega el nodo Complemento para Retenciones Locales
                if (RetenValue == true)
                {
                    string RetLocNameSpace = "http://www.sat.gob.mx/implocal";
                    string RetLocPrefijo = "implocal";

                    nComplemento = CCXml.CreateNode(XmlNodeType.Element, "Complemento", NameSpace);
                    nComplemento.Prefix = Prefijo;

                    //Se agrege el nodo de Impuestos Locales
                    nImpuestosLocales = CCXml.CreateNode(XmlNodeType.Element, "ImpuestosLocales", RetLocNameSpace);
                    nImpuestosLocales.Prefix = RetLocPrefijo;

                    AgregaAtributo("S", "N", ref CCXml, ref nImpuestosLocales, "version", "1.0", "");
                    AgregaAtributo("S", "$", ref CCXml, ref nImpuestosLocales, "TotaldeTraslados", "0.00", "");
                    AgregaAtributo("S", "$", ref CCXml, ref nImpuestosLocales, "TotaldeRetenciones", tb_retLocales.Text, "");

                    //Se agregar retenciones locales
                    i = 0;
                    for (i = 0; i <= RetenList.Rows.Count - 1; i++)
                    {
                        nRetencionesLocales = CCXml.CreateNode(XmlNodeType.Element, "RetencionesLocales", RetLocNameSpace);
                        nRetencionesLocales.Prefix = RetLocPrefijo;

                        AgregaAtributo("S", "$", ref CCXml, ref nRetencionesLocales, "Importe", RetenList.Rows[i]["importe_retLocal"].ToString(), ""); ;
                        AgregaAtributo("S", "N", ref CCXml, ref nRetencionesLocales, "TasadeRetencion", RetenList.Rows[i]["tasa_retLocal"].ToString(), "");
                        AgregaAtributo("S", "C", ref CCXml, ref nRetencionesLocales, "ImpLocRetenido", RetenList.Rows[i]["descr_retLocal"].ToString(), "");

                        nImpuestosLocales.AppendChild(nRetencionesLocales);
                    }
                    nComplemento.AppendChild(nImpuestosLocales);

                    nCFDI.AppendChild(nComplemento);
                }
                #endregion

                #region Se agrega el nodo Complemento para Complemento INE
                if (INEValue == true)
                {
                    string INENameSpace = "http://www.sat.gob.mx/ine";
                    string INEPrefijo = "ine";

                    nComplemento = CCXml.CreateNode(XmlNodeType.Element, "Complemento", NameSpace);
                    nComplemento.Prefix = Prefijo;

                    //Se agrege el nodo de INE
                    nINE = CCXml.CreateNode(XmlNodeType.Element, "INE", INENameSpace);
                    nINE.Prefix = INEPrefijo;

                    AgregaAtributo("S", "C", ref CCXml, ref nINE, "Version", "1.1", "");
                    AgregaAtributo("N", "C", ref CCXml, ref nINE, "TipoProceso", TipoProceso, "");
                    if (TipoProceso == "Ordinario")
                        AgregaAtributo("N", "C", ref CCXml, ref nINE, "TipoComite", TipoComite, "");

                    //Se agrege el nodo de INE Entidad
                    nINEEntidad = CCXml.CreateNode(XmlNodeType.Element, "Entidad", INENameSpace);
                    nINEEntidad.Prefix = INEPrefijo;

                    AgregaAtributo("N", "C", ref CCXml, ref nINEEntidad, "ClaveEntidad", CveEntidad, "");
                    if (TipoProceso != "Ordinario")
                        AgregaAtributo("N", "C", ref CCXml, ref nINEEntidad, "Ambito", TipoAmbito, "");

                    nINEContabilidad = CCXml.CreateNode(XmlNodeType.Element, "Contabilidad", INENameSpace);
                    nINEContabilidad.Prefix = INEPrefijo;

                    AgregaAtributo("N", "C", ref CCXml, ref nINEContabilidad, "IdContabilidad", IdContabilidad, "");

                    nINEEntidad.AppendChild(nINEContabilidad);
                    nINE.AppendChild(nINEEntidad);
                    nComplemento.AppendChild(nINE);

                    nCFDI.AppendChild(nComplemento);
                }
                #endregion


                //Se le Agrega al nodo cfdi  al XML 
                CCXml.AppendChild(nCFDI);
                #endregion

                CCXml.Save(RutaXML + ArchivoXML);
                #endregion XML

                #region  Se manda a TIMBRAR el Archivo XML

                //Busca la cuenta de la Empresa

                try
                {
                    con2.Open();
                    com2.CommandText = "SELECT Cuenta_Timbrado, Usuario_Timbrado, Token_Timbrado, Pass_Timbrado FROM empresa WHERE (Clave = " + this.cb_ClaveEmp.Text + ")";
                    MySqlDataAdapter tda = new MySqlDataAdapter();
                    DataSet tds = new DataSet();
                    tda.SelectCommand = com2;
                    tda.Fill(tds, "Timbrado");
                    DataRow trow = tds.Tables["Timbrado"].Rows[0];

                    string XML_CFD_Timbrado = "";
                    string user = trow["Usuario_Timbrado"].ToString();
                    string password = trow["Pass_Timbrado"].ToString();
                    string token = trow["Token_Timbrado"].ToString();
                    string cuenta = trow["Cuenta_Timbrado"].ToString();

                    try
                    {
                        // Archivo de Entrada
                        StreamReader streamReader = new StreamReader(RutaXML + ArchivoXML);
                        string XML_CFD = streamReader.ReadToEnd();
                        streamReader.Close();

                        if (cb_webservice.Checked == true)
                        {
                            //*****Timbre de Pruebas 
                            mx.com.cfdinova.test02.TimbradorIntegradores oTimbrar = new SiscomCFDI.mx.com.cfdinova.test02.TimbradorIntegradores();
                            XML_CFD_Timbrado = oTimbrar.get(XML_CFD, token, user, password, cuenta);
                        }
                        else
                        {
                            //mx.com.tusfacturas.www.TimbradorIntegradores oTimbrar = new mx.com.tusfacturas.www.TimbradorIntegradores();
                            cfdinova_produccion.TimbradorIntegradoresPortTypeClient oTimbrar = new cfdinova_produccion.TimbradorIntegradoresPortTypeClient();
                            XML_CFD_Timbrado = oTimbrar.get(XML_CFD, token, user, password, cuenta);
                        }

                        StreamWriter streamWriter = new StreamWriter(RutaXML_Timbrado + ArchivoXML_Timbrado);
                        streamWriter.Write(XML_CFD_Timbrado);
                        streamWriter.Close();

                        if (XML_CFD_Timbrado.Contains("Error"))
                        {
                            throw new IOException("Vea el archivo " + RutaXML_Timbrado + ArchivoXML_Timbrado);
                        }

                        GuardarDatos(strOriginal, RutaXML_Timbrado, ArchivoXML_Timbrado, Serie);
                        Addenda(CCXml, Prefijo, NameSpace, Origen);

                        imprimir_btn.Enabled = true;
                        MessageBox.Show("Se Ha creado correctamene CFDI", "Finalizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btn_xml.Enabled = false;
                        rb_dolares.Enabled = true;
                        rb_pesos.Enabled = true;
                    }
                    catch (System.IO.IOException ex)
                    {
                        imprimir_btn.Enabled = false;
                        MessageBox.Show("No se pudo timbrar el CFDI. Puede ser causado por problemas con el PAQ o con su servicio de internet.\n" + ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error durante el almacenamiento de la información, verifíque su XML.\n" + ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    com2.Dispose();
                    con2.Close();
                }

                //Fin de la busqueda            

                #endregion
                
                #endregion
            }
            else
            {
                MessageBox.Show("Selecciona campos obligatorios como UsoCFDI, Forma de Pago, Método de Pago.\n", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);                
            }
        }       
                       
        #region Se define el DataSet de la Factura
        private DataSet GetDatosDeFactura()
        {
            Fechaxml = DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss");

            con2.Open();
            com2.Connection = con2;

            //Query Empresa.cfdi
            string query = ("SELECT RFC, Nombre, fCalle, fNoExterior, fNoInterior, fColonia, fLocalidad, fMunicipio, fEstado, fPais, fCP, sCalle, sNoExterior, sNoInterior, sColonia, sLocalidad, sMunicipio, "
                            + "sEstado, sPais, sCP, RegFiscal, fTelefono, sTelefono FROM empresa WHERE (Clave = " + this.cb_ClaveEmp.Text + ")");

            MySqlDataAdapter ad = new MySqlDataAdapter(query, con2);
            DataSet dsFactura = new DataSet();
            ad.Fill(dsFactura, "Empresa");

            DataTable dtFacturaG;
            dtFacturaG = dsFactura.Tables["Empresa"];

            TelEmi = dsFactura.Tables[0].Rows[0]["fTelefono"].ToString();
            TelSucu = dsFactura.Tables[0].Rows[0]["sTelefono"].ToString();
            
            query = ("SELECT Serie, MIN(Folio) AS 'Folio', AnoAprobacion, NoAprobacion FROM movimientos WHERE (Estatus = 'D')AND (Clave_Emp = " + this.cb_ClaveEmp.Text + ") GROUP BY Serie");
            
            ad = new MySqlDataAdapter(query, con2);
            ad.Fill(dsFactura, "mov");

            DataTable dtF2;
            dtF2 = dsFactura.Tables["mov"];

            conn.Open();
            com.Connection = conn;

            //Query POS Clientes
            string clientes = ("SELECT Fac_Cab.RFC, Fac_Cab.Nombre, Fac_Cab.Direccion, Fac_Cab.Colonia, Fac_Cab.Ciudad, Estados.Nombre As Estado, Fac_Cab.CodigoPostal, Fac_Cab.Credito,Fac_Cab.DescuentoG As Descuento, Fac_Cab.Telefono, Fac_Cab.DiasCredito FROM (Fac_Cab INNER JOIN Estados ON Fac_Cab.Estado = Estados.Estado) WHERE (Fac_Cab.Factura = " + this.tb_fFactura.Text + ") AND (Fac_Cab.TipoM = '" + this.tb_tm.Text + "')");
            OleDbDataAdapter daClient = new OleDbDataAdapter(clientes, conn);
            daClient.Fill(dsFactura, "Cliente");
            DataTable dtCli;
            dtCli = dsFactura.Tables["Cliente"];

            
            //Query Tasa maxima de Articulos POS
            OleDbCommand comTasa = new OleDbCommand();
            comTasa.Connection = conn;
            if (rb_tick.Checked == true)
            {
                comTasa.CommandText = ("SELECT MAX(Articulos.CIva) AS Tasa, MAX(Articulos.Reorden) AS TasaIEPS " +
                         "FROM (Articulos INNER JOIN Venta_Fac ON Articulos.Codigo = Venta_Fac.Codigo)" +
                         "WHERE (Venta_Fac.Factura = " + this.tb_fFactura.Text + ") AND (Venta_Fac.FacturaTM = '" + this.tb_tm.Text + "')");
            }
            else
            {
                comTasa.CommandText = ("SELECT MAX(Articulos.CIva) AS Tasa, MAX(Articulos.Reorden) AS TasaIEPS " +
                         "FROM (Articulos INNER JOIN Fac_Mov ON Articulos.Codigo = Fac_Mov.Codigo)" +
                         "WHERE (Fac_Mov.Factura = " + this.tb_fFactura.Text + ") AND (Fac_Mov.TipoM = '" + this.tb_tm.Text + "')");
            }
            OleDbDataAdapter daTasa = new OleDbDataAdapter(comTasa);
            daTasa.Fill(dsFactura, "Tasa");
            DataTable Tasa;
            Tasa = dsFactura.Tables["Tasa"];           

            //Impuesto del IVA
            OleDbCommand iva = new OleDbCommand();
            iva.Connection = conn;
            OleDbDataAdapter daiva = new OleDbDataAdapter(iva);
            DataTable dtiva;
            if (rb_tick.Checked == true)
            {
                iva.CommandText = ("SELECT SUM(Venta_Fac.SubTotal) AS Sub_total, SUM(Venta_Fac.Iva) AS Impuesto, Sub_total + Impuesto AS Total" +
                                    " FROM (Fac_Cab INNER JOIN Venta_Fac ON Fac_Cab.Factura = Venta_Fac.Factura)" +
                                    " WHERE (Fac_Cab.TipoM = '" + this.tb_tm.Text + "') AND (Fac_Cab.Factura = " + this.tb_fFactura.Text + ")");

                daiva.Fill(dsFactura, "iva");
                dtiva = dsFactura.Tables["iva"];
            }
            else
            {
            }

            foreach (DataRow drCli in dtCli.Rows)
            {
                //Datos Cliente de POS = rXXXXX
                object RFC_c = drCli["RFC"];
                object Nombre_c = drCli["Nombre"];
                object Direccion_c = drCli["Direccion"];
                object Colonia_c = drCli["Colonia"];
                object Ciudad_c = drCli["Ciudad"];
                string Estado_c = drCli["Estado"].ToString();
                object CP_c = drCli["CodigoPostal"];
                object Descuento = drCli["Descuento"];

                string Credito = drCli["Credito"].ToString();
                
                TelCli = drCli["Telefono"].ToString();
                
                //Definimos Las Condiciones de Pago y País
                if (Credito == "True")
                {
                    ConPago = "Credito";
                    DiasCredito = drCli["DiasCredito"].ToString();
                }
                else
                {
                    ConPago = "Contado";
                }

                com.Connection = conn;
                com.CommandText = "SELECT Estado FROM Estados WHERE (Nombre = '" + Estado_c + "')";
                int Cod_EdoC = int.Parse(com.ExecuteScalar().ToString());

                if (Cod_EdoC > 32)
                {
                    SelectPais paiscr = new SelectPais();

                    paiscr.ShowDialog();
                    Pais = paiscr.Resultado;
                }
                else
                {
                    Pais = "MÉXICO";
                }
            }   
                   
            com2.Dispose();
            con2.Close();
            conn.Close();
            com.Dispose();            
            
            return dsFactura;
        }
        #endregion 
        public void Addenda(XmlDocument CCXml, String Prefijo, String NameSpace, String Origen) 
        {
            string filename = RutaXML_Timbrado + ArchivoXML_Timbrado;            
            CCXml = new XmlDocument();            
            CCXml.Load(filename);
            XmlElement root = CCXml.DocumentElement;  
          
            //Adendas Generales
            XmlNode nAddendas;
            XmlNode nFacturaInterFactura;
            XmlNode nAEmisor;
            XmlNode nAReceptor;
            XmlNode nAEncabezado;
            XmlNode nACuerpo;

            //Adendas Pepsico
            XmlNode nRequestCFD;
            XmlNode nDocumento;
            XmlNode nProveedor;
            XmlNode nRecepciones;
            XmlNode nRecepcion;
            XmlNode nConcepto;

            int i = 0;
            String PU = "";
            
            string NameSpaceAdd = "https://www.interfactura.com/Schemas/Documentos";
            string PrefijoAdd = "if";

            if (rb_pepsico.Checked == true)
            {
                nAddendas = CCXml.CreateNode(XmlNodeType.Element, "Addenda", NameSpace);
                nAddendas.Prefix = Prefijo;

                //Nodo RequestCFD
                nRequestCFD = CCXml.CreateNode(XmlNodeType.Element, "RequestCFD", "");
                AgregaAtributo("S", "C", ref CCXml, ref nRequestCFD, "tipo", "AddendaPCO", "");
                AgregaAtributo("S", "N", ref CCXml, ref nRequestCFD, "version", "2.0", "");
                AgregaAtributo("S", "C", ref CCXml, ref nRequestCFD, "idPedido", NumPedido, "");

                //Nodo Documento
                nDocumento = CCXml.CreateNode(XmlNodeType.Element, "Documento", "");
                AgregaAtributo("N", "N", ref CCXml, ref nDocumento, "tipoDoc", "1", "");
                AgregaAtributo("S", "C", ref CCXml, ref nDocumento, "folioUUID", UUID, "");

                nRequestCFD.AppendChild(nDocumento);

                //Nodo Proveedor
                nProveedor = CCXml.CreateNode(XmlNodeType.Element, "Proveedor", "");
                AgregaAtributo("S", "C", ref CCXml, ref nProveedor, "idProveedor", NumProveedor, "");
                
                nRequestCFD.AppendChild(nProveedor);

                //Nodo Recepciones
                nRecepciones = CCXml.CreateNode(XmlNodeType.Element, "Recepciones", "");

                //Nodo Reception
                nRecepcion = CCXml.CreateNode(XmlNodeType.Element, "Recepcion", "");
                AgregaAtributo("S", "C", ref CCXml, ref nRecepcion, "idRecepcion", NumRecepcion, "");

                if (Origen == "FACTURA")
                {
                    for (i = 0; i <= dsItems.Tables[0].Rows.Count - 1; i++)
                    {
                        //Se agrega el nodo CONCEPTO al nodo CONCEPTOS
                        nConcepto = CCXml.CreateNode(XmlNodeType.Element, "Concepto", "");

                        PU = string.Format("{0:0.00}", Convert.ToDouble(dsItems.Tables[0].Rows[i]["Importe"].ToString()) / Convert.ToDouble(dsItems.Tables[0].Rows[i]["Cantidad"].ToString()));

                        AgregaAtributo("S", "C", ref CCXml, ref nConcepto, "unidad", dsItems.Tables[0].Rows[i]["UnidadM"].ToString(), "");
                        AgregaAtributo("S", "C", ref CCXml, ref nConcepto, "descripcion", dsItems.Tables[0].Rows[i]["Descripcion"].ToString(), "");
                        AgregaAtributo("S", "N", ref CCXml, ref nConcepto, "cantidad", dsItems.Tables[0].Rows[i]["Cantidad"].ToString(), "");
                        AgregaAtributo("S", "$", ref CCXml, ref nConcepto, "valorUnitario", PU, "");
                        AgregaAtributo("S", "$", ref CCXml, ref nConcepto, "importe", dsItems.Tables[0].Rows[i]["Importe"].ToString(), "");

                        nRecepcion.AppendChild(nConcepto);
                    }
                }
                else
                {
                    //Se agrega el nodo CONCEPTO al nodo CONCEPTOS
                    nConcepto = CCXml.CreateNode(XmlNodeType.Element, "Concepto", "");

                    AgregaAtributo("S", "C", ref CCXml, ref nConcepto, "unidad", "1", "");
                    AgregaAtributo("S", "N", ref CCXml, ref nConcepto, "cantidad", "1", "");
                    AgregaAtributo("S", "C", ref CCXml, ref nConcepto, "descripcion", dsDatos.Tables[0].Rows[0]["Descripcion"].ToString(), "");
                    AgregaAtributo("S", "$", ref CCXml, ref nConcepto, "valorUnitario", dsDatos.Tables[0].Rows[0]["Precio"].ToString(), "");
                    AgregaAtributo("S", "$", ref CCXml, ref nConcepto, "importe", dsDatos.Tables[0].Rows[0]["Importe"].ToString(), "");

                    nRecepcion.AppendChild(nConcepto);
                }
                                
                nRecepciones.AppendChild(nRecepcion);
                nRequestCFD.AppendChild(nRecepciones);
                nAddendas.AppendChild(nRequestCFD);
                root.AppendChild(nAddendas);                                
            }
            else if (rb_addenda.Checked == true)
            {
                nAddendas = CCXml.CreateNode(XmlNodeType.Element, "Addenda", NameSpace);
                nAddendas.Prefix = Prefijo;

                //FacturaInterfactura Addenda
                nFacturaInterFactura = CCXml.CreateNode(XmlNodeType.Element, "FacturaInterfactura", NameSpaceAdd);
                nFacturaInterFactura.Prefix = PrefijoAdd;

                AgregaAtributo("S", "C", ref CCXml, ref nFacturaInterFactura, "xmlns:if", "https://www.interfactura.com/Schemas/Documentos", "");
                AgregaAtributo("S", "C", ref CCXml, ref nFacturaInterFactura, "TipoDocumento", "Factura", "");

                //Emisor Addenda
                nAEmisor = CCXml.CreateNode(XmlNodeType.Element, "Emisor", NameSpaceAdd);
                nAEmisor.Prefix = PrefijoAdd;

                AgregaAtributo("S", "C", ref CCXml, ref nAEmisor, "RI", EmisorRI, "");
                AgregaAtributo("S", "C", ref CCXml, ref nAEmisor, "NumProveedor", NumProv, "");

                nFacturaInterFactura.AppendChild(nAEmisor);

                //Receptor Addenda
                nAReceptor = CCXml.CreateNode(XmlNodeType.Element, "Receptor", NameSpaceAdd);
                nAReceptor.Prefix = PrefijoAdd;

                AgregaAtributo("S", "C", ref CCXml, ref nAReceptor, "RI", ReceptorRI, "");

                nFacturaInterFactura.AppendChild(nAReceptor);

                //Encabezado Addenda
                nAEncabezado = CCXml.CreateNode(XmlNodeType.Element, "Encabezado", NameSpaceAdd);
                nAEncabezado.Prefix = PrefijoAdd;

                AgregaAtributo("N", "C", ref CCXml, ref nAEncabezado, "TipoProveedorEKT", ProveEkt, "");
                AgregaAtributo("S", "D", ref CCXml, ref nAEncabezado, "Fecha", Fechaxml, "");
                AgregaAtributo("N", "C", ref CCXml, ref nAEncabezado, "MonedaDoc", Moneda, "");
                AgregaAtributo("S", "$", ref CCXml, ref nAEncabezado, "IVAPCT", "0.00", "");
                AgregaAtributo("S", "$", ref CCXml, ref nAEncabezado, "Iva", RetIVA, "");
                AgregaAtributo("S", "$", ref CCXml, ref nAEncabezado, "SubTotal", this.tb_subtotal.Text, "");
                AgregaAtributo("S", "$", ref CCXml, ref nAEncabezado, "Total", tb_total.Text, "");
                AgregaAtributo("S", "C", ref CCXml, ref nAEncabezado, "NumProveedor", NumProv, "");
                AgregaAtributo("N", "N", ref CCXml, ref nAEncabezado, "FolioOrdenCompra", FolioAddenda, "");
                AgregaAtributo("S", "C", ref CCXml, ref nAEncabezado, "PorcentajeDescuentoPromo", "0.00", "");
                AgregaAtributo("S", "C", ref CCXml, ref nAEncabezado, "PorcentajeMerma", "0", "");
                AgregaAtributo("S", "$", ref CCXml, ref nAEncabezado, "TotalDescuento", "0", "");
                AgregaAtributo("S", "$", ref CCXml, ref nAEncabezado, "TotalMerma", "0", "");
                AgregaAtributo("N", "$", ref CCXml, ref nAEncabezado, "Descuento", Desc, "");

                if (Origen == "FACTURA")
                {
                    for (i = 0; i <= dsItems.Tables[0].Rows.Count - 1; i++)
                    {
                        //Se agrega el nodo CONCEPTO al nodo CONCEPTOS
                        nACuerpo = CCXml.CreateNode(XmlNodeType.Element, "Cuerpo", NameSpaceAdd);
                        nACuerpo.Prefix = PrefijoAdd;

                        PU = string.Format("{0:0.00}", Convert.ToDouble(dsItems.Tables[0].Rows[i]["Importe"].ToString()) / Convert.ToDouble(dsItems.Tables[0].Rows[i]["Cantidad"].ToString()));

                        AgregaAtributo("S", "C", ref CCXml, ref nACuerpo, "Renglon", "1", "");
                        AgregaAtributo("S", "C", ref CCXml, ref nACuerpo, "Concepto", dsItems.Tables[0].Rows[i]["Descripcion"].ToString(), "");
                        AgregaAtributo("S", "N", ref CCXml, ref nACuerpo, "Cantidad", dsItems.Tables[0].Rows[i]["Cantidad"].ToString(), "");
                        AgregaAtributo("S", "$", ref CCXml, ref nACuerpo, "PUnitario", PU, "");
                        AgregaAtributo("S", "$", ref CCXml, ref nACuerpo, "Importe", dsItems.Tables[0].Rows[i]["Importe"].ToString(), "");

                        nAEncabezado.AppendChild(nACuerpo);
                    }
                }
                else
                {
                    //Se agrega el nodo CONCEPTO al nodo CONCEPTOS
                    nACuerpo = CCXml.CreateNode(XmlNodeType.Element, "Cuerpo", NameSpaceAdd);
                    nACuerpo.Prefix = PrefijoAdd;

                    AgregaAtributo("S", "C", ref CCXml, ref nACuerpo, "Renglon", "1", "");
                    AgregaAtributo("S", "N", ref CCXml, ref nACuerpo, "Cantidad", "1", "");
                    AgregaAtributo("S", "C", ref CCXml, ref nACuerpo, "Concepto", dsDatos.Tables[0].Rows[0]["Descripcion"].ToString(), "");
                    AgregaAtributo("S", "$", ref CCXml, ref nACuerpo, "PUnitario", dsDatos.Tables[0].Rows[0]["Precio"].ToString(), "");
                    AgregaAtributo("S", "$", ref CCXml, ref nACuerpo, "Importe", dsDatos.Tables[0].Rows[0]["Importe"].ToString(), "");

                    nAEncabezado.AppendChild(nACuerpo);
                }

                nFacturaInterFactura.AppendChild(nAEncabezado);

                nAddendas.AppendChild(nFacturaInterFactura);

                root.AppendChild(nAddendas);
            }
            CCXml.AppendChild(root);            
            CCXml.Save(filename);            
        }
        
        #region Se define los Items o Partidas la Factura
        public DataSet GetItemsDeFactura()
        {
            OleDbConnection acon2 = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + BDPOS + "'");
            OleDbCommand acom2 = new OleDbCommand();
            acon2.Open();
            acom2.Connection = acon2;

            //Articulos de la factura
            if(rb_tick.Checked == true)
            {
                acom2.CommandText = "SELECT Venta_Fac.Folio, Venta_Fac.Codigo, Venta_Fac.Cantidad, Articulos.UnidadA as Unidad, Venta_Fac.Descripcion, Venta_Fac.SubTotal AS Importe, Venta_Fac.Total, Articulos.UnidadM, ROUND((Venta_Fac.SubTotal * vm.Descuento2)/100,2)  as Descuento " +
                                    "FROM (Venta_Fac INNER JOIN Articulos ON Venta_Fac.Codigo = Articulos.Codigo) INNER JOIN Venta_Mov as vm ON vm.Folio = Venta_Fac.Folio AND vm.Codigo = Venta_Fac.Codigo WHERE (Venta_Fac.Factura = " + this.tb_fFactura.Text + ") " +
                                    "AND (Venta_Fac.FacturaTM = '" + this.tb_tm.Text + "') ORDER BY Venta_Fac.Renglon";
            }
            else
            {
                //Round(Precio * Fac_Mov.Cantidad, 2) - ((Round(Precio * Fac_Mov.Cantidad, 2) * Descuento) / 100) AS Importe /*Seccion para sacar el precio despues de descuendo*/
                acom2.CommandText = "SELECT Fac_Mov.Codigo, Fac_Mov.Cantidad, Articulos.Descripcion, Fac_Mov.UnidadM , Round(Fac_Mov.Precio, 2) as Precio, Round(Precio * Fac_Mov.Cantidad, 2) AS Importe, (Round(Precio * Fac_Mov.Cantidad, 2) * Fac_Cab.DescuentoG) / 100  as Descuento " +
                                    "FROM ((Articulos INNER JOIN Fac_Mov ON Articulos.Codigo = Fac_Mov.Codigo)  INNER JOIN Fac_Cab ON Fac_Cab.Factura = Fac_Mov.Factura and Fac_Cab.TipoM =Fac_Mov.TipoM) " +
                                    "WHERE (Fac_Mov.Factura = " + this.tb_fFactura.Text + ") AND (Fac_Mov.TipoM = '" + this.tb_tm.Text + "') ORDER BY Fac_Mov.Renglon";
            }
            OleDbDataAdapter daa = new OleDbDataAdapter(acom2);
            DataSet dsit = new DataSet();
            daa.Fill(dsit, "items");
            DataTable dtit;
            dtit = dsit.Tables["items"];
            //CDFI 3.3
            dtit.Columns.Add("Clave_SAT");
            dtit.Columns.Add("ClaveUnidad_SAT");

            foreach (DataRow drit in dtit.Rows)
            {
                if (rb_tick.Checked == true)
                {
                    object Folio = drit.ItemArray[0];
                    object Clave = drit.ItemArray[1];
                    object Cantidad = drit.ItemArray[2];
                    object Descrip = drit.ItemArray[3];
                    object Unidad = drit.ItemArray[4];
                    object Precio = drit.ItemArray[5];
                    object Importe = drit.ItemArray[6];
                }
                else
                {
                    object Clave = drit.ItemArray[0];
                    object Cantidad = drit.ItemArray[1];
                    object Descrip = drit.ItemArray[2];
                    object Unidad = drit.ItemArray[3];
                    object Precio = drit.ItemArray[4];
                    object Importe = drit.ItemArray[5];
                    //object Descuento = drit.ItemArray[6];
                }

                foreach (DataGridViewRow row in dataGridView1.Rows) 
                {
                    if (row.Cells[0].Value != null)
                    {
                        string dritClave = drit["Codigo"].ToString();
                        
                        if (rb_tick.Checked == true)
                        {
                            string folioTick = drit["Folio"].ToString();
                            if (folioTick == row.Cells[0].Value.ToString() && dritClave == row.Cells[1].Value.ToString())
                            {
                                drit["Clave_SAT"] = row.Cells[9].Value;
                                drit["claveUnidad_SAT"] = row.Cells[10].Value;
                            }
                            else { continue; }
                        }
                        else
                        {
                            if (dritClave == row.Cells[0].Value.ToString())
                            {
                                drit["Clave_SAT"] = row.Cells[7].Value;
                                drit["claveUnidad_SAT"] = row.Cells[8].Value;
                            }
                            else { continue; }
                        }
                    }
                }
            }

            acom2.Dispose();
            acon2.Close();
            return dsit;

        #endregion

        }
        
        #region METODOS CREACIÓN SELLO - CADENA ORIGINAL
        public String CadenaOriginal(string Origen, DataSet dsDatos, DataSet dsItems, string Fechaxml, string folio, string noCertificado, string TCambio, string Total, string Subtotal, string Impuesto, string Moneda, string RetISR, string RetIVA, string FormaPago, string ConPago, string Desc, string Pais, string TipoFac, bool retenValue, DataTable RetLocDT, string SumRetLoc, string TotalRetLoc, string Ieps, string UsoCFDI, string MetodoPago, string cuentaPredial, bool _hasDocsRelacionados, DataTable _DocsRelacionados, string _tipoRelacion) 
        {           
            string CO = "||";
            //    Informacion general                
            AgregaDatoCO(ref CO, "S", "N", "Version", "3.3", "0");
            AgregaDatoCO(ref CO, "N", "C", "Serie", dsDatos.Tables[1].Rows[0]["Serie"].ToString(), "0");
            AgregaDatoCO(ref CO, "N", "C", "Folio", folio,"0");
            AgregaDatoCO(ref CO, "S", "D", "Fecha", Fechaxml, "0");
            AgregaDatoCO(ref CO, "S", "C", "FormaPago", FormaPago, "0");
            AgregaDatoCO(ref CO, "S", "N", "NoCertificado", noCertificado, "0"); 
            AgregaDatoCO(ref CO, "N", "C", "CondicionesDePago", ConPago, "0");    
            AgregaDatoCO(ref CO, "S", "$", "SubTotal", Subtotal, "0");
            AgregaDatoCO(ref CO, "N", "$", "Descuento", Desc, "0");
            AgregaDatoCO(ref CO, "N", "C", "Moneda", Moneda, "0");
            if(Moneda != "MXN")
                AgregaDatoCO(ref CO, "N", "$", "TipoCambio", TCambio, "0");// CFDI 3.3 Campo no requerido cuando sea Moneda = MXN
                
            if (retenValue == true) { 
                AgregaDatoCO(ref CO, "S", "$", "Total", TotalRetLoc, "0"); }
            else
                AgregaDatoCO(ref CO, "S", "$", "Total", Total, "0");
            
            AgregaDatoCO(ref CO, "S", "C", "TipoDeComprobante", "I", "0");
            AgregaDatoCO(ref CO, "S", "C", "MetodoPago", MetodoPago, "0");
            AgregaDatoCO(ref CO, "S", "C", "LugarExpedicion", dsDatos.Tables[0].Rows[0]["sCP"].ToString(), "0");

            //    Documentos Relacionados
            if (_hasDocsRelacionados)
            {
                AgregaDatoCO(ref CO, "S", "C", "TipoRelacion", _tipoRelacion, "0");
                foreach (DataRow _docs in _DocsRelacionados.Rows)
                {
                    AgregaDatoCO(ref CO, "S", "C", "UUID", _docs["UUID"].ToString(), "0");
                }
            }
                
            //    Información del nodo Emisor
            AgregaDatoCO(ref CO, "S", "C", "Rfc", dsDatos.Tables[0].Rows[0]["RFC"].ToString(), "0");
            AgregaDatoCO(ref CO, "S", "C", "Nombre", dsDatos.Tables[0].Rows[0]["Nombre"].ToString(), "");
            AgregaDatoCO(ref CO, "S", "C", "RegimenFiscal", dsDatos.Tables[0].Rows[0]["RegFiscal"].ToString(), "0"); 
            //    Información del nodo Receptor
            AgregaDatoCO(ref CO, "S", "C", "Rfc", dsDatos.Tables[2].Rows[0]["RFC"].ToString().Replace("-",""), "0");
            AgregaDatoCO(ref CO, "N", "C", "Nombre", dsDatos.Tables[2].Rows[0]["Nombre"].ToString(), "0");
            AgregaDatoCO(ref CO, "S", "C", "UsoCFDI", UsoCFDI, "0");
            

            if (Origen == "FACTURA")
            {
                int i = 0;
                for (i = 0; i <= dsItems.Tables[0].Rows.Count - 1; i++)
                {
                    string PU = string.Format("{0:0.00}", Convert.ToDouble(dsItems.Tables[0].Rows[i]["Importe"].ToString()) / Convert.ToDouble(dsItems.Tables[0].Rows[i]["Cantidad"].ToString()));

                    //CFDI 3.3
                    AgregaDatoCO(ref CO, "S", "C", "ClaveProdServ", dsItems.Tables[0].Rows[i]["Clave_SAT"].ToString(), "0");
                    AgregaDatoCO(ref CO, "N", "C", "NoIdentificacion", dsItems.Tables[0].Rows[i]["Codigo"].ToString(), "0");
                    AgregaDatoCO(ref CO, "S", "N", "Cantidad", dsItems.Tables[0].Rows[i]["Cantidad"].ToString(), "0");
                    AgregaDatoCO(ref CO, "S", "C", "ClaveUnidad", dsItems.Tables[0].Rows[i]["claveUnidad_SAT"].ToString(), "0");
                    AgregaDatoCO(ref CO, "N", "C", "Unidad", dsItems.Tables[0].Rows[i]["UnidadM"].ToString(), "0");                    
                    AgregaDatoCO(ref CO, "S", "C", "Descripcion", dsItems.Tables[0].Rows[i]["Descripcion"].ToString(), "0");
                    AgregaDatoCO(ref CO, "S", "$", "ValorUnitario", PU, "0");
                    AgregaDatoCO(ref CO, "S", "$", "Importe", dsItems.Tables[0].Rows[i]["Importe"].ToString(), "0");

                    if (rb_tick.Checked == true)
                    {
                        double _descuento = Convert.ToDouble(dsItems.Tables[0].Rows[i]["Descuento"]);
                        if (_descuento > 0)
                            AgregaDatoCO(ref CO, "N", "$", "Descuento", _descuento.ToString(), "0");
                    }
                    else
                    {
                        if (DescPorcentaje > 0)
                            AgregaDatoCO(ref CO, "N", "$", "Descuento", dsItems.Tables[0].Rows[i]["Descuento"].ToString(), "0");                        
                    }

                    string prodCodigo = dsItems.Tables[0].Rows[i]["Codigo"].ToString();
                    string importe_base = dsItems.Tables[0].Rows[i]["Importe"].ToString();

                    if(existeTraslado(dg_impuestos, prodCodigo, importe_base))
                    {
                        foreach (DataGridViewRow imp_row in dg_impuestos.Rows)
                        {
                            
                            string folio_tick = "";

                            if (this.rb_tick.Checked == true)
                            {
                                folio_tick = dsItems.Tables[0].Rows[i]["Folio"].ToString();
                                if (folio_tick == imp_row.Cells[0].Value.ToString() && prodCodigo == imp_row.Cells[1].Value.ToString() && imp_row.Cells[2].Value.ToString() == "Traslado")
                                {
                                    string _impuestoCodigo = imp_row.Cells[4].Value.ToString().Substring(0, 3);
                                    AgregaDatoCO(ref CO, "S", "$", "Base", imp_row.Cells[3].Value.ToString(), "");
                                    AgregaDatoCO(ref CO, "S", "C", "Impuesto", _impuestoCodigo, "");
                                    AgregaDatoCO(ref CO, "S", "C", "TipoFactor", imp_row.Cells[5].Value.ToString(), "");
                                    AgregaDatoCO(ref CO, "S", "C", "TasaOCuota", imp_row.Cells[6].Value.ToString(), "");
                                    AgregaDatoCO(ref CO, "S", "$", "Importe", imp_row.Cells[7].Value.ToString(), "");
                                }
                            }
                            else
                            {
                                if (importe_base == imp_row.Cells[2].Value.ToString() && prodCodigo == imp_row.Cells[0].Value.ToString() && imp_row.Cells[1].Value.ToString() == "Traslado")
                                {
                                    string _impuestoCodigo = imp_row.Cells[3].Value.ToString().Substring(0, 3);
                                    AgregaDatoCO(ref CO, "S", "$", "Base", imp_row.Cells[2].Value.ToString(), "");
                                    AgregaDatoCO(ref CO, "S", "C", "Impuesto", _impuestoCodigo, "");
                                    AgregaDatoCO(ref CO, "S", "C", "TipoFactor", imp_row.Cells[4].Value.ToString(), "");
                                    AgregaDatoCO(ref CO, "S", "C", "TasaOCuota", imp_row.Cells[5].Value.ToString(), "");
                                    AgregaDatoCO(ref CO, "S", "$", "Importe", imp_row.Cells[6].Value.ToString(), "");
                                }
                            }
                        }
                    }
                    if (existeRetencion(dg_impuestos, prodCodigo, importe_base))
                    {
                        foreach (DataGridViewRow ret_row in dg_impuestos.Rows)
                        {
                            //string prodCodigo = dsItems.Tables[0].Rows[i]["Codigo"].ToString();
                            //string Importe_base = dsItems.Tables[0].Rows[i]["Importe"].ToString();
                            string folio_tick = "";

                            if (this.rb_tick.Checked == true)
                            {
                                folio_tick = dsItems.Tables[0].Rows[i]["Folio"].ToString();
                                if (folio_tick == ret_row.Cells[0].Value.ToString() && prodCodigo == ret_row.Cells[1].Value.ToString() && ret_row.Cells[2].Value.ToString() == "Retención")
                                {
                                    string _impuestoCodigo = ret_row.Cells[4].Value.ToString().Substring(0, 3);
                                    AgregaDatoCO(ref CO, "S", "$", "Base", ret_row.Cells[3].Value.ToString(), "");
                                    AgregaDatoCO(ref CO, "S", "C", "Impuesto", _impuestoCodigo, "");
                                    AgregaDatoCO(ref CO, "S", "C", "TipoFactor", ret_row.Cells[5].Value.ToString(), "");
                                    AgregaDatoCO(ref CO, "S", "C", "TasaOCuota", ret_row.Cells[6].Value.ToString(), "");
                                    AgregaDatoCO(ref CO, "S", "$", "Importe", ret_row.Cells[7].Value.ToString(), "");
                                }
                            }
                            else
                            {
                                if (importe_base == ret_row.Cells[2].Value.ToString() && prodCodigo == ret_row.Cells[0].Value.ToString() && ret_row.Cells[1].Value.ToString() == "Retención")
                                {
                                    string _impuestoCodigo = ret_row.Cells[3].Value.ToString().Substring(0, 3);
                                    AgregaDatoCO(ref CO, "S", "$", "Base", ret_row.Cells[2].Value.ToString(), "");
                                    AgregaDatoCO(ref CO, "S", "C", "Impuesto", _impuestoCodigo, "");
                                    AgregaDatoCO(ref CO, "S", "C", "TipoFactor", ret_row.Cells[4].Value.ToString(), "");
                                    AgregaDatoCO(ref CO, "S", "C", "TasaOCuota", ret_row.Cells[5].Value.ToString(), "");
                                    AgregaDatoCO(ref CO, "S", "$", "Importe", ret_row.Cells[6].Value.ToString(), "");
                                }
                            }
                        }
                    }

                    if (cuentaPredial != null && cuentaPredial != String.Empty) 
                    {
                        AgregaDatoCO(ref CO, "S", "N", "Numero", cuentaPredial, "0");
                    }
                }
            }
            else
            {
                //CFDI 3.3
                AgregaDatoCO(ref CO, "S", "C", "ClaveProdServ", "01010101", "0");
                AgregaDatoCO(ref CO, "N", "C", "NoIdentificacion", "", "0");
                AgregaDatoCO(ref CO, "S", "N", "Cantidad", "1", "0");
                AgregaDatoCO(ref CO, "S", "C", "ClaveUnidad", "XNA", "0");
                AgregaDatoCO(ref CO, "N", "C", "Unidad", "PZ", "0");
                AgregaDatoCO(ref CO, "S", "C", "Descripcion", dsDatos.Tables[0].Rows[0]["Descripcion"].ToString(), "0");
                AgregaDatoCO(ref CO, "S", "$", "ValorUnitario", dsDatos.Tables[0].Rows[0]["Precio"].ToString(), "0");
                AgregaDatoCO(ref CO, "S", "$", "Importe", dsDatos.Tables[0].Rows[0]["Importe"].ToString(), "0");
            }

            //  9. Informacion de cada nodo Retenciones
            if (existeRetencion(dg_impuestos, "",""))
            {
                string impCodigoPrevio = "-1";
                List<string> tasa_lista = new List<string>();

                foreach (DataGridViewRow ret_row in dg_impuestos.Rows)
                {
                    if (this.rb_tick.Checked == true)
                    {
                        if (ret_row.Cells[2].Value.ToString() == "Retención")
                        {
                            string _impuestoCodigo = ret_row.Cells[4].Value.ToString().Substring(0, 3);
                            string _tasa = ret_row.Cells[6].Value.ToString();

                            if (!tasa_lista.Contains(_impuestoCodigo + "-" + _tasa))
                            {
                                if (_tasa == "0.000000")
                                {
                                    AgregaDatoCO(ref CO, "S", "C", "Impuesto", _impuestoCodigo, "");                                    
                                    AgregaDatoCO(ref CO, "S", "$", "Importe", "0.00", "");

                                    tasa_lista.Add(_impuestoCodigo + "-" + _tasa);
                                    continue;
                                }

                                decimal sum_ret_importe = TotalImpuestos("Retención", _impuestoCodigo, dg_impuestos);
                                AgregaDatoCO(ref CO, "S", "C", "Impuesto", _impuestoCodigo, "");
                                AgregaDatoCO(ref CO, "S", "$", "Importe", sum_ret_importe.ToString(), "");

                                tasa_lista.Add(_impuestoCodigo + "-" + _tasa);
                            }
                        }
                    }
                    else
                    {
                        if (ret_row.Cells[1].Value.ToString() == "Retención")
                        {
                            string _impuestoCodigo = ret_row.Cells[3].Value.ToString().Substring(0, 3);
                            string _tasa = ret_row.Cells[5].Value.ToString();

                            if (!tasa_lista.Contains(_impuestoCodigo + "-" + _tasa))
                            {
                                if (_tasa == "0.000000")
                                {
                                    AgregaDatoCO(ref CO, "S", "C", "Impuesto", _impuestoCodigo, "");
                                    AgregaDatoCO(ref CO, "S", "$", "Importe", "0.00", "");

                                    tasa_lista.Add(_impuestoCodigo + "-" + _tasa);
                                    continue;
                                }

                                decimal sum_ret_importe = TotalImpuestos("Retención", _impuestoCodigo, dg_impuestos);
                                AgregaDatoCO(ref CO, "S", "C", "Impuesto", _impuestoCodigo, "");
                                AgregaDatoCO(ref CO, "S", "$", "Importe", sum_ret_importe.ToString(), "");

                                tasa_lista.Add(_impuestoCodigo + "-" + _tasa);
                            }
                        }
                    }
                }
                
                double TotalRetenido = Convert.ToDouble(tb_rtIVA.Text) + Convert.ToDouble(tb_rtIEPS.Text) + Convert.ToDouble(tb_retISR.Text);
                AgregaDatoCO(ref CO, "S", "$", "TotalImpuestosRetenidos", string.Format("{0:0.00}", TotalRetenido), "");
            }
            
            //    10.Información de cada nodo Traslado
            if (existeTraslado(dg_impuestos, "", ""))
            {
                string impCodigoPrevio = "-1";
                List<string> tasa_lista = new List<string>();

                foreach (DataGridViewRow _row in dg_impuestos.Rows)
                {
                    if (this.rb_tick.Checked == true)
                    {
                        if (_row.Cells[2].Value.ToString() == "Traslado")
                        {
                            string _impuestoCodigo = _row.Cells[4].Value.ToString().Substring(0, 3);
                            string _tasa = _row.Cells[6].Value.ToString();

                            if (!tasa_lista.Contains(_impuestoCodigo + "-" + _tasa))
                            {
                                if (_tasa == "0.000000")
                                {
                                    AgregaDatoCO(ref CO, "S", "C", "Impuesto", _impuestoCodigo, "");
                                    AgregaDatoCO(ref CO, "S", "C", "TipoFactor", _row.Cells[5].Value.ToString(), "");
                                    AgregaDatoCO(ref CO, "S", "C", "TasaOCuota", _row.Cells[6].Value.ToString(), "");
                                    AgregaDatoCO(ref CO, "S", "$", "Importe", "0.00", "");
                                    
                                    tasa_lista.Add(_impuestoCodigo + "-" + _tasa);
                                    continue;
                                }
                                                                
                                decimal sum_tras_importe = TotalImpuestos("Traslado", _impuestoCodigo, dg_impuestos);

                                AgregaDatoCO(ref CO, "S", "C", "Impuesto", _impuestoCodigo, "");
                                AgregaDatoCO(ref CO, "S", "C", "TipoFactor", _row.Cells[5].Value.ToString(), "");
                                AgregaDatoCO(ref CO, "S", "C", "TasaOCuota", _row.Cells[6].Value.ToString(), "");
                                AgregaDatoCO(ref CO, "S", "$", "Importe", sum_tras_importe.ToString(), "");
                                impCodigoPrevio = _impuestoCodigo;
                                
                                tasa_lista.Add(_impuestoCodigo + "-" + _tasa);
                            }                            
                        }
                    }
                    else
                    {
                        if (_row.Cells[1].Value.ToString() == "Traslado")
                        {
                            string _impuestoCodigo = _row.Cells[3].Value.ToString().Substring(0, 3);
                            string _tasa = _row.Cells[5].Value.ToString();
                            
                            if (!tasa_lista.Contains(_impuestoCodigo + "-" + _tasa))
                            {
                                if (_tasa == "0.000000")
                                {
                                    AgregaDatoCO(ref CO, "S", "C", "Impuesto", _impuestoCodigo, "");
                                    AgregaDatoCO(ref CO, "S", "C", "TipoFactor", _row.Cells[4].Value.ToString(), "");
                                    AgregaDatoCO(ref CO, "S", "C", "TasaOCuota", _row.Cells[5].Value.ToString(), "");
                                    AgregaDatoCO(ref CO, "S", "$", "Importe", "0.00", "");
                                    
                                    tasa_lista.Add(_impuestoCodigo + "-" + _tasa);
                                    continue;
                                }
                                
                                decimal sum_tras_importe = TotalImpuestos("Traslado", _impuestoCodigo, dg_impuestos);

                                AgregaDatoCO(ref CO, "S", "C", "Impuesto", _impuestoCodigo, "");
                                AgregaDatoCO(ref CO, "S", "C", "TipoFactor", _row.Cells[4].Value.ToString(), "");
                                AgregaDatoCO(ref CO, "S", "C", "TasaOCuota", _row.Cells[5].Value.ToString(), "");
                                AgregaDatoCO(ref CO, "S", "$", "Importe", sum_tras_importe.ToString(), "");
                                impCodigoPrevio = _impuestoCodigo;
                                
                                tasa_lista.Add(_impuestoCodigo + "-" + _tasa);
                            }                            
                        }
                    }                    
                }
                
                double TotalTrasladado = Convert.ToDouble(tb_ieps.Text) + Convert.ToDouble(tb_iva.Text);
                AgregaDatoCO(ref CO, "S", "$", "TotalImpuestosTrasladados", string.Format("{0:0.00}", TotalTrasladado), "");
            }           

            //    11. Informacion de nodo Retencioenes Locales
            if (retenValue == true) { 
                AgregaDatoCO(ref CO, "S", "N", "version", "1.0", "0");
                AgregaDatoCO(ref CO, "S", "$", "TotaldeRetenciones", SumRetLoc, "0");
                AgregaDatoCO(ref CO, "S", "$", "TotaldeTraslados", "0.00", "0");
                    
                int i = 0;
                for (i = 0; i <= RetLocDT.Rows.Count - 1; i++)
                {
                    AgregaDatoCO(ref CO, "S", "C", "ImpLocRetenido", RetLocDT.Rows[i]["descr_retLocal"].ToString(), "");
                    AgregaDatoCO(ref CO, "S", "N", "TasadeRetencion", RetLocDT.Rows[i]["tasa_retLocal"].ToString(), "");
                    AgregaDatoCO(ref CO, "S", "$", "Importe", RetLocDT.Rows[i]["importe_retLocal"].ToString(), "");
                }
            }
            //    12. Informacion de nodo Complemento INE
            if (INEValue == true) 
            {
                AgregaDatoCO(ref CO, "S", "C", "Version", "1.1", "0");
                AgregaDatoCO(ref CO, "N", "C", "TipoProceso", TipoProceso, "");
                if(TipoProceso == "Ordinario")
                    AgregaDatoCO(ref CO, "N", "C", "TipoComite", TipoComite, "");
                AgregaDatoCO(ref CO, "N", "C", "ClaveEntidad", CveEntidad, "");
                if (TipoProceso != "Ordinario")
                    AgregaDatoCO(ref CO, "N", "C", "Ambito", TipoAmbito, "");
                AgregaDatoCO(ref CO, "N", "C", "IdContabilidad", IdContabilidad, "");
            }
            CO = CO + "|";
            return CO;
        }
        public string get_noCertificacdo() 
        {
            string ArchCer = "";
            string ArchKey = "";
            string PasswordKey = "";
            string RutaCer = "";
            string RutaKey = "";
            string noCertificado = "";

            loadCertificado(out ArchCer, out ArchKey, out PasswordKey, out RutaCer, out RutaKey);

            string ArchivoCertificado = RutaCer + ArchCer;
            string ArchivoKey = RutaKey + ArchKey;

            noCertificado = "";
            string ErrorCod = "0";
            string ErrorMsj = "No hay error";

            bool success;
            Chilkat.PrivateKey loPkey = new Chilkat.PrivateKey();
            Chilkat.Cert loCert = new Chilkat.Cert();
            Chilkat.Rsa loRsa = new Chilkat.Rsa();


            // Desbloquea componente
            success = loRsa.UnlockComponent("RSAT34MB34N_7F1CD986683M");
            if (success != true)
            {
                ErrorCod = "1";
                ErrorMsj = "No se pudo debloquear componente Chilkat RSA";
                return ErrorCod;
            }

            //  Load the Certificado 
            success = loCert.LoadFromFile(ArchivoCertificado);
            if (success != true)
            {
                ErrorCod = "1";
                ErrorMsj = "No se pudo abrir el archivo certificado: " + ArchivoCertificado;
                return ErrorCod;
            }

            // Load the private key from an RSA PEM file:     
            success = loPkey.LoadPkcs8EncryptedFile(ArchivoKey, PasswordKey);
            if (success != true)
            {
                ErrorCod = "1";
                ErrorMsj = "No se pudo abrir archivo KEY o contraseña invalida: " + ArchivoKey;
                return ErrorCod;
            }

            //  Get the private key in XML format:
            string lcPkeyXml = loPkey.GetXml();

            // Import the private key into the RSA component:
            success = loRsa.ImportPrivateKey(lcPkeyXml);
            if (success != true)
            {
                ErrorCod = "1";
                ErrorMsj = "Problemas al importar la llave pribada al componente RSA";
                return ErrorCod;
            }

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

            return noCertificado;
        }

        public void loadCertificado(out string ArchCer, out string ArchKey, out string PasswordKey, out string RutaCer, out string RutaKey) 
        {
            ArchCer = "";
            ArchKey = "";
            PasswordKey = "";
            RutaCer = "";
            RutaKey = "";

            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter();
                DataSet ds = new DataSet();
                con2.Open();
                com2.Connection = con2;
                com2.CommandText = "SELECT Arch_cer, Arch_key, Contrasena, Ruta_cer FROM certificados WHERE (Clave_emp = " + this.cb_ClaveEmp.Text + ") AND (Estatus = 'A')";

                da.SelectCommand = com2;
                con2.Close();

                da.Fill(ds, "Certificados");
                DataRow row = ds.Tables["Certificados"].Rows[0];
                object ArCer = row["Arch_cer"].ToString();
                object ArKey = row["Arch_key"].ToString();
                object Contrasena = row["Contrasena"].ToString();
                object Ruta = row["Ruta_cer"].ToString();

                ArchCer = ArCer.ToString();
                ArchKey = ArKey.ToString();
                PasswordKey = Contrasena.ToString();
                RutaCer = Ruta.ToString();
                RutaKey = Ruta.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar certificados." + ex, "Error Certificados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally 
            {
                con2.Close();
            }
        }

        private void Sello(string strOriginal, out string SelloDigital, out string Certificado, out string ErrorCod, out string ErrorMsj)
        {
            string ArchCer = "";
            string ArchKey = "";
            string PasswordKey = "";
            string RutaCer = "";
            string RutaKey = "";

            loadCertificado(out ArchCer, out ArchKey,out PasswordKey, out RutaCer, out RutaKey);

            string ArchivoCertificado = RutaCer + ArchCer;
            string ArchivoKey = RutaKey + ArchKey;

            SelloDigital = "";
            Certificado = "";
            ErrorCod = "0";
            ErrorMsj = "No hay error";
        
            bool success;
            Chilkat.PrivateKey loPkey = new Chilkat.PrivateKey();
            Chilkat.Cert loCert = new Chilkat.Cert();
            Chilkat.Rsa loRsa = new Chilkat.Rsa();

        
            // Desbloquea componente
            success = loRsa.UnlockComponent("RSAT34MB34N_7F1CD986683M");
            if (success != true)
            {
                ErrorCod = "1";
                ErrorMsj = "No se pudo debloquear componente Chilkat RSA";
                return;
            }

            //  Load the Certificado 
            success = loCert.LoadFromFile(ArchivoCertificado);
            if (success != true)
            {
                ErrorCod = "1";
                ErrorMsj = "No se pudo abrir el archivo certificado: " + ArchivoCertificado;
                return;
            }

            // Load the private key from an RSA PEM file:     
            success = loPkey.LoadPkcs8EncryptedFile(ArchivoKey, PasswordKey);
            if (success != true)
            {
                ErrorCod = "1";
                ErrorMsj = "No se pudo abrir archivo KEY o contraseña invalida: " + ArchivoKey;
            }
        
            //  Get the private key in XML format:
            string lcPkeyXml = loPkey.GetXml();

            // Import the private key into the RSA component:
            success = loRsa.ImportPrivateKey(lcPkeyXml);
            if (success != true)
            {
                ErrorCod = "1";
                ErrorMsj = "Problemas al importar la llave pribada al componente RSA";
            }

            //  OpenSSL uses BigEndian byte ordering
            loRsa.LittleEndian = false;
            loRsa.Charset = "utf-8";
            loRsa.EncodingMode = "base64";

            SelloDigital = loRsa.SignStringENC(strOriginal, "SHA-256");
            
            Certificado = loCert.GetEncoded();
            int LongitudCer = Certificado.Length;

            Certificado = Certificado.Substring(1 - 1, LongitudCer - 2);                
        }
        #endregion

        #region METODOS DE ATRIBUTOS/DATOCO/AGREGAR/QUITARCHAR
        private static void AgregaAtributo(string obligatorio, string tipo, ref XmlDocument CCXml, ref  XmlNode nodo, string atributo, string valor, string indicador)
        {
            XmlAttribute nTemporal;

            if (tipo == "$")
                valor = string.Format("{0:0.00}", Convert.ToDouble(valor));

            valor = QuitarCharInvalidos(valor);


            if (Agregar(obligatorio, tipo, valor) == "SI")
            {
                nTemporal = CCXml.CreateAttribute(atributo);
                nTemporal.Value = valor;
                nodo.Attributes.Append(nTemporal);
            }
        }

        private static void AgregaDatoCO(ref string CO, string obligatorio, string tipo, string campo, string valor, string SWReferencia)
        {
            if (tipo == "$")
                valor = string.Format("{0:0.00}", Convert.ToDouble(valor));

            valor = QuitarCharInvalidos(valor);

            if (Agregar(obligatorio, tipo, valor) == "SI")
                CO = CO + valor + "|";
        }

        private static string Agregar(string obligatorio, string tipo, string valor)
        {
            string Agrgar = "SI";
            if (obligatorio == "S")
                Agrgar = "SI";
            else
            {
                switch (tipo)
                {
                    case "C":
                        if (valor.ToString().Length == 0)
                            Agrgar = "NO";
                        break;                        
                    case "N":
                        if (Convert.ToDouble(valor) == 0)
                            Agrgar = "NO";
                        break;
                    case "D":
                        if (valor.ToString().Length == 0)
                            Agrgar = "NO";
                        break;
                    case "$":
                        if (Convert.ToDouble(valor) == 0)
                            Agrgar = "NO";
                        break;
                }

            }
            return Agrgar;
        }

        private static string QuitarCharInvalidos(string dato)
        {
            string CharAnterior = "";
            string CharActual = "";
            string NewDato = "";
            int i = 0;

            dato = dato.Trim();
            for (i = 0; i <= dato.Length - 1; i++)
            {
                CharActual = dato.Substring(i, 1);
                switch (CharActual)
                {
                    case "&":
                        CharActual = "&";
                        break;

                    //                case '"':
                    //                     CharActual = "&quot;";
                    //                     break;

                    case "<":
                        CharActual = "&lt;";
                        break;

                    case ">":
                        CharActual = "&gt;";
                        break;

                    case "'":
                        CharActual = "&apos;";
                        break;

                    default:
                        char caracter = Convert.ToChar(CharActual);
                        int ascii = caracter;
                        if (ascii == 127)
                        {
                            CharActual = "";
                        }
                        else
                        {
                            if (ascii >= 0 && ascii <= 31)
                            {
                                CharActual = "";
                            }
                        }
                        break;
                }

                if (!(CharAnterior == " " && CharActual == " "))
                    NewDato = NewDato + CharActual;

                CharAnterior = CharActual;
            }
            return NewDato;
        }     
        #endregion
        
        private void tb_fFactura_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btn_buscar_Click(sender, e);
            }
        }

        #region Imprecion y Almacenamiento de Factura timbrada  
        MySqlDataAdapter dai = new MySqlDataAdapter();
        DataSet dsi = new DataSet();

        private void imprimir_btn_Click(object sender, EventArgs e)
        {
            con2.Open();
            com2.Connection = con2;
            com2.CommandText = "SELECT Ruta_PDF, Ruta_BMP, Ruta_logo, Reporte_fac FROM empresa WHERE Clave = " + cb_ClaveEmp.Text + "";
            dai.SelectCommand = com2;
            dai.Fill(dsi, "Rutas");

            DataRow row = dsi.Tables["Rutas"].Rows[0];

            Rpt_Fac = @"" + row["Reporte_fac"].ToString() + "";
            string RutaLogo = @"" + row["Ruta_logo"].ToString() + "";
            RutaXML = RutaXML_Timbrado;
            string RutaBMP = @"" + row["Ruta_BMP"].ToString() + "";
            RutaPDF = @"" + row["Ruta_PDF"].ToString() + "";
            string EncaCFD = "Factura";
            PDFArchivoXML_Timbrado = ArchivoXML_Timbrado;
            string ArchivoBMP_CBBQR = ArchivoXML_Timbrado.Substring(0, ArchivoXML_Timbrado.Length - 13) + ".BMP";
            ArchivoPDF_CFDI = ArchivoXML_Timbrado.Substring(0, ArchivoXML_Timbrado.Length - 13) + ".PDF";

            con2.Close();
            // Obtiene los datos del CFD timbrado tomandolos del XML timbrado
            DataSet dsCFD = GetCFDTimbrado(RutaXML + PDFArchivoXML_Timbrado, strOriginal);

            #region Generamos el Codigo de Barra Bidimensional QR en un archivo BMP

            string eRFC = dsCFD.Tables["FacturaG"].Rows[0]["erfc"].ToString().Trim();
            string rRFC = dsCFD.Tables["FacturaG"].Rows[0]["rrfc"].ToString().Trim();
            string Total = dsCFD.Tables["FacturaG"].Rows[0]["total"].ToString().Trim();
            Total = String.Format("{0:0000000000.000000}", Convert.ToDouble(Total));
            string UUID = dsCFD.Tables["FacturaG"].Rows[0]["UUID"].ToString().Trim();
            string DatoCBB = "?re=" + eRFC + "&rr=" + rRFC + "&tt=" + Total + "&id=" + UUID;

            Int32 qrBackColor = System.Drawing.Color.FromArgb(255, 255, 255, 255).ToArgb();
            Int32 qrForeColor = System.Drawing.Color.FromArgb(255, 0, 0, 0).ToArgb();

            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
            qrCodeEncoder.QRCodeScale = 9;
            qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            qrCodeEncoder.QRCodeVersion = 0;
            qrCodeEncoder.QRCodeBackgroundColor = System.Drawing.Color.FromArgb(qrBackColor);
            qrCodeEncoder.QRCodeForegroundColor = System.Drawing.Color.FromArgb(qrForeColor);
            string archivocbb = RutaBMP + ArchivoBMP_CBBQR;

            if (!System.IO.File.Exists(archivocbb))
            {
                qrCodeEncoder.Encode(DatoCBB, System.Text.Encoding.UTF8).Save(archivocbb);
            }

            #endregion

            //Complementa el DataSet del CFD (dsCFD) por no venir estos datos en el XML 
            dsCFD.Tables[1].Rows[0]["CBBQR"] = ImageToByte(Image.FromFile(archivocbb));

            FileStream fs = new FileStream(RutaLogo, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            byte[] imgbyte = new byte[fs.Length + 1];
            imgbyte = br.ReadBytes(Convert.ToInt32(fs.Length));
            dsCFD.Tables[1].Rows[0]["Logotipo1"] = imgbyte;

            br.Close();
            fs.Close();

            dsCFD.Tables[1].Rows[0]["EncaCFD"] = EncaCFD;

            #region Imprime y Exporta a PDF la representacion Impresa el CFD
            CrystalDecisions.CrystalReports.Engine.ReportDocument CrReport = new CrystalDecisions.CrystalReports.Engine.ReportDocument();

            if (RetenValue == true)
            {
                CrReport.Load(Application.StartupPath.ToString() + @"\SCFRRepImpCFD-RetLoc.rpt");
            }
            else if (INEValue == true)
            {
                CrReport.Load(Application.StartupPath.ToString() + @"\SCFRRepImpCFD_INE.rpt");
            }
            else
                CrReport.Load(Rpt_Fac);//Application.StartupPath.ToString() + @"\SCFRRepImpCFD.rpt");
            CrReport.SetDataSource(dsCFD);

            ExportOptions CrExportOptions;
            DiskFileDestinationOptions CrDiskFileDestinationOptions = new DiskFileDestinationOptions();
            PdfRtfWordFormatOptions CrFormatTypeOptions = new PdfRtfWordFormatOptions();
            CrDiskFileDestinationOptions.DiskFileName = RutaPDF + ArchivoPDF_CFDI;

            CrExportOptions = CrReport.ExportOptions;
            CrExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            CrExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            CrExportOptions.DestinationOptions = CrDiskFileDestinationOptions;
            CrExportOptions.FormatOptions = CrFormatTypeOptions;
            CrReport.Export();

            if (System.Diagnostics.Process.Start(RutaPDF + ArchivoPDF_CFDI) == null)
            {
                MessageBox.Show("No se a encontrado el archivo PDF verifique la ubicación del documento ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            else
            {
                MessageBox.Show("Se a creado exitosamente la factura en formato PDF ahora puede imprimirla", "Finalizar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btn_enviar.Enabled = true;
            }
            #endregion
        }      

        private void GuardarDatos(string strOriginal, string RutaXML_Timbrado, string ArchivoXML_Timbrado, string Serie)
        {
            DataSet dsCFD = GetCFDTimbrado(RutaXML_Timbrado + ArchivoXML_Timbrado, strOriginal);
            string noCer = dsCFD.Tables["FacturaG"].Rows[0]["NoCertificado"].ToString().Trim();
            string cambio = dsCFD.Tables["FacturaG"].Rows[0]["TipoCambio"].ToString().Trim();
            string Total = dsCFD.Tables["FacturaG"].Rows[0]["Total"].ToString().Trim();
            string impuesto = "";
            if (dsCFD.Tables["FacturaG"].Rows[0]["TotalTrasladados"].ToString().Trim() == "")
                impuesto = dsCFD.Tables["FacturaG"].Rows[0]["TotalRetenidos"].ToString().Trim();
            else
                impuesto = dsCFD.Tables["FacturaG"].Rows[0]["TotalTrasladados"].ToString().Trim();
            string iva = "0";
            string SelloSAT = dsCFD.Tables["FacturaG"].Rows[0]["SelloSAT"].ToString().Trim();
            string noCSD = dsCFD.Tables["FacturaG"].Rows[0]["NoCertificadoSAT"].ToString().Trim();
            UUID = dsCFD.Tables["FacturaG"].Rows[0]["UUID"].ToString().Trim();
            string SelloCFD = dsCFD.Tables["FacturaG"].Rows[0]["Sello"].ToString().Trim();
            string fecha = dsCFD.Tables["FacturaG"].Rows[0]["Fecha"].ToString().Trim();
            string rrfc = dsCFD.Tables["FacturaG"].Rows[0]["rrfc"].ToString().Trim();
            fecha = fecha.Replace("T", " ");
            rrfc = rrfc.Replace("-", "");
            if (cambio == "")
            {cambio = "0";} 

            MySqlDataAdapter da = new MySqlDataAdapter();
            DataSet dst1 = new DataSet();
            MySqlCommand com3 = new MySqlCommand();

            try
            {
                string Folio;
                com3.Connection = con2;
                com3.CommandText = "SELECT MIN(Folio) AS 'Folio' FROM movimientos WHERE (Estatus = 'D') AND (Clave_Emp =" + this.cb_ClaveEmp.Text + ")";
                da.SelectCommand = com3;
                da.Fill(dst1, "Folio");
                DataRow row = dst1.Tables["Folio"].Rows[0];
                Folio = row["Folio"].ToString();

                com2.Connection = con2;
                if(con2.State.ToString() == "Closed")
                    con2.Open();

                com2.CommandText = "UPDATE movimientos SET No_Certificado ='" + noCer + "', CadenaOriginal ='" + strOriginal + "', SelloCFD ='" + SelloCFD + "', SelloSAT ='" +     //Version CFDI 3.2
                                    SelloSAT + "', Estatus ='T', Factura =" + folio + ", Folio_SAT ='" + UUID + "', NoCSD_SAT ='" + noCSD + "', Operacion =" +
                                    Total + ", Impuesto =" + impuesto + ", TipoCambio =" + cambio + ", IVA =" + iva + ", MetodoPago ='" + FormaPago + "', NoCtaPago ='" + 
                                    noCta + "', Fecha = '" + fecha + "', RFC = '" + rrfc + "'" +
                                    " WHERE (Folio = " + Folio + ")";

                com2.ExecuteNonQuery();
                GuardarProductosSAT();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se han guarado los datos correctamente, Verifique su información.\n" + ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
        {
                com3.Dispose();
                com2.Dispose();
                con2.Close();
                com.Dispose();
                conn.Close();
            }
        }
        private void GuardarProductosSAT() 
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    string codigoProdPOS = "";
                    string descripcionPOS = "";
                    string unidadPOS = "";
                    string codigoSAT = "";
                    string unidadSAT = "";

                    if (this.rb_tick.Checked == true)
                    {
                        codigoProdPOS = row.Cells[1].Value.ToString();
                        descripcionPOS = row.Cells[4].Value.ToString();
                        unidadPOS = row.Cells[3].Value.ToString();
                        codigoSAT = row.Cells[9].Value.ToString();
                        unidadSAT = row.Cells[10].Value.ToString();
                    }
                    else 
                    {
                        codigoProdPOS = row.Cells[0].Value.ToString();
                        descripcionPOS = row.Cells[3].Value.ToString();
                        unidadPOS = row.Cells[2].Value.ToString();
                        codigoSAT = row.Cells[7].Value.ToString();
                        unidadSAT = row.Cells[8].Value.ToString();
                    }
                    

                    MySqlDataAdapter dataAdap = new MySqlDataAdapter();
                    DataSet dsTemp = new DataSet();
                    if (con2.State.ToString() == "Closed")
                        con2.Open();                    
                    
                    com2.CommandText = "SELECT * FROM cfdi.productos_sat Where codigo_pos = '" + codigoProdPOS + "';";
                    dataAdap.SelectCommand = com2;
                    
                    dataAdap.Fill(dsTemp, "Prod_SAT");

                    if (dsTemp.Tables["Prod_SAT"].Rows.Count == 0)
                    {
                        com2.CommandText = "INSERT INTO cfdi.productos_sat " +
                                        "(`codigo_pos`,`descripcion_pos`,`unidad_pos`,`codigo_sat`,`codigoUnidad_sat`) " +
                                        "VALUES" +
                                        "('" + codigoProdPOS + "'," +
                                        "'" + descripcionPOS + "'," +
                                        "'" + unidadPOS + "'," +
                                        "'" + codigoSAT + "'," +
                                        "'" + unidadSAT + "');";
                        com2.ExecuteNonQuery();
                    }
                }
            }
        }
        public DataSet GetCFDTimbrado(string ArchivoXML, string strOriginal)
        {            
            #region Se define el DataSet del CFD
            // Este DataSet debe ser igual al DataSet de RepImpCFD.xsd            
            DataSet dsFactura = new DataSet("Factura");

            DataTable dtFacturaG = new DataTable("FacturaG");
            // DATOS GENERALES DE FACTURA 
            dtFacturaG.Columns.Add("Version");
            dtFacturaG.Columns.Add("Serie");
            dtFacturaG.Columns.Add("Folio");
            dtFacturaG.Columns.Add("Fecha");
            dtFacturaG.Columns.Add("anoAprobacion"); //Excluir
            dtFacturaG.Columns.Add("noAprobacion");//Excluir 
            dtFacturaG.Columns.Add("FormaPago");
            dtFacturaG.Columns.Add("CondicionesDePago");
            dtFacturaG.Columns.Add("SubTotal");
            dtFacturaG.Columns.Add("Descuento");
            dtFacturaG.Columns.Add("TipoCambio");
            dtFacturaG.Columns.Add("Moneda");
            dtFacturaG.Columns.Add("Total");
            dtFacturaG.Columns.Add("TipoDeComprobante");
            dtFacturaG.Columns.Add("MetodoPago");
            dtFacturaG.Columns.Add("LugarExpedicion");//CFDI v3.3
            dtFacturaG.Columns.Add("NumCtaPago");//Excluir
            dtFacturaG.Columns.Add("Sello");
            dtFacturaG.Columns.Add("NoCertificado");
            dtFacturaG.Columns.Add("Certificado");
            dtFacturaG.Columns.Add("Tipo_Relacion");
            dtFacturaG.Columns.Add("UUID_Relacionado");

            // DATOS EMISOR
            dtFacturaG.Columns.Add("erfc");
            dtFacturaG.Columns.Add("RegimenFiscal"); 
            dtFacturaG.Columns.Add("enombre");
            
            // DATOS DEL RECEPTOR
            dtFacturaG.Columns.Add("rrfc");
            dtFacturaG.Columns.Add("rnombre");
            dtFacturaG.Columns.Add("usoCFDI");
            
            // Total Impuestos RETENIDOS
            dtFacturaG.Columns.Add("TotalRetenidos");
            dtFacturaG.Columns.Add("RISR");
            dtFacturaG.Columns.Add("RIVA");
            dtFacturaG.Columns.Add("RIEPS");

            // Total Impuestos TRASLADADOS
            dtFacturaG.Columns.Add("TotalTrasladados");
            dtFacturaG.Columns.Add("Base");
            dtFacturaG.Columns.Add("Impuesto");
            dtFacturaG.Columns.Add("TipoFactor");
            dtFacturaG.Columns.Add("TasaOCouta");
            dtFacturaG.Columns.Add("Importe");
            dtFacturaG.Columns.Add("TIVA");
            dtFacturaG.Columns.Add("TIEPS");

            dtFacturaG.Columns.Add("SelloCFD");
            dtFacturaG.Columns.Add("FechaTimbrado");
            dtFacturaG.Columns.Add("UUID");
            dtFacturaG.Columns.Add("Versiont");
            dtFacturaG.Columns.Add("NoCertificadoSAT");
            dtFacturaG.Columns.Add("SelloSAT");

            // Datos Complementarios que no son parte del XML pero se pueden requerir
            // Para su representación impresa             
            dtFacturaG.Columns.Add("Logotipo1", typeof(byte[]));
            dtFacturaG.Columns.Add("Logotipo2", typeof(byte[]));
            dtFacturaG.Columns.Add("CBBQR", typeof(byte[]));
            dtFacturaG.Columns.Add("EncaCFD");
            dtFacturaG.Columns.Add("EncaTitulo1");
            dtFacturaG.Columns.Add("EncaTitulo2");
            dtFacturaG.Columns.Add("EncaTitulo3");
            dtFacturaG.Columns.Add("Nota1");
            dtFacturaG.Columns.Add("Nota2");
            dtFacturaG.Columns.Add("Nota3");
            dtFacturaG.Columns.Add("CadenaOrg"); 
            dtFacturaG.Columns.Add("Vendedor");
            //Datos de Complemento INE
            dtFacturaG.Columns.Add("TipoProceso");
            dtFacturaG.Columns.Add("TipoComite");
            dtFacturaG.Columns.Add("ClaveEntidad");
            dtFacturaG.Columns.Add("Ambito");
            dtFacturaG.Columns.Add("IdContabilidad");

            DataTable dtFacturaD = new DataTable("FacturaD");
            dtFacturaD.Columns.Add("Serie");
            dtFacturaD.Columns.Add("Folio");
            dtFacturaD.Columns.Add("ClaveProdServ");
            dtFacturaD.Columns.Add("NoIdentificacion");
            dtFacturaD.Columns.Add("Cantidad");
            dtFacturaD.Columns.Add("ClaveUnidad");
            dtFacturaD.Columns.Add("Unidad");
            dtFacturaD.Columns.Add("Descripcion");
            dtFacturaD.Columns.Add("ValorUnitario");
            dtFacturaD.Columns.Add("Importe");
            dtFacturaD.Columns.Add("descripcion1");
            dtFacturaD.Columns.Add("descripcion2");
            dtFacturaD.Columns.Add("descripcion3");
            dtFacturaD.Columns.Add("descripcion4");
            dtFacturaD.Columns.Add("descripcion5");
            dtFacturaD.Columns.Add("descripcion6");
            dtFacturaD.Columns.Add("descripcion7");
            dtFacturaD.Columns.Add("descripcion8");
            dtFacturaD.Columns.Add("descripcion9");
            dtFacturaD.Columns.Add("descripcion10");
            dtFacturaD.Columns.Add("descripcion11");
            dtFacturaD.Columns.Add("descripcion12");
            dtFacturaD.Columns.Add("descripcion13");
            dtFacturaD.Columns.Add("descripcion14");
            dtFacturaD.Columns.Add("descripcion15");
            dtFacturaD.Columns.Add("descripcion16");
            dtFacturaD.Columns.Add("descripcion17");
            dtFacturaD.Columns.Add("descripcion18");
            dtFacturaD.Columns.Add("descripcion19");
            dtFacturaD.Columns.Add("descripcion20");
            dtFacturaD.Columns.Add("Contribuyente");
            dtFacturaD.Columns.Add("eTelefono");
            dtFacturaD.Columns.Add("cTelefono");
            dtFacturaD.Columns.Add("sTelefono");
            dtFacturaD.Columns.Add("DiasCredito");
            dtFacturaD.Columns.Add("NumeroPredial"); //Numero Cuenta Predial CFDI v3.3

            DataTable dtFacturaRetLoc = new DataTable("FacturaRetLoc");
            dtFacturaRetLoc.Columns.Add("Serie");
            dtFacturaRetLoc.Columns.Add("Folio");
            dtFacturaRetLoc.Columns.Add("DescImpLocRetenido0");
            dtFacturaRetLoc.Columns.Add("ImporteLocRetenido0");
            dtFacturaRetLoc.Columns.Add("DescImpLocRetenido1");
            dtFacturaRetLoc.Columns.Add("ImporteLocRetenido1");
            dtFacturaRetLoc.Columns.Add("DescImpLocRetenido2");
            dtFacturaRetLoc.Columns.Add("ImporteLocRetenido2");
            dtFacturaRetLoc.Columns.Add("DescImpLocRetenido3");
            dtFacturaRetLoc.Columns.Add("ImporteLocRetenido3");
            dtFacturaRetLoc.Columns.Add("DescImpLocRetenido4");
            dtFacturaRetLoc.Columns.Add("ImporteLocRetenido4");

            DataTable dtTraslados = new DataTable("FacturaT");
            dtTraslados.Columns.Add("Serie");
            dtTraslados.Columns.Add("Folio");
            dtTraslados.Columns.Add("Base");
            dtTraslados.Columns.Add("Impuesto");
            dtTraslados.Columns.Add("TipoFactor");
            dtTraslados.Columns.Add("TasaOCuota");
            dtTraslados.Columns.Add("Importe");

            DataTable dtRetenciones = new DataTable("FacturaR");
            dtRetenciones.Columns.Add("Serie");
            dtRetenciones.Columns.Add("Folio");
            dtRetenciones.Columns.Add("Base");
            dtRetenciones.Columns.Add("Impuesto");
            dtRetenciones.Columns.Add("TipoFactor");
            dtRetenciones.Columns.Add("TasaOCuota");
            dtRetenciones.Columns.Add("Importe");
            #endregion

            #region Abrimos el archivo XML
            DataRow drFacturaG = dtFacturaG.NewRow();
            drFacturaG["Serie"] = " ";
            drFacturaG["descuento"] = "0.00";
            drFacturaG["EncaCFD"] = " ";
            drFacturaG["EncaTitulo1"] = " ";
            drFacturaG["EncaTitulo2"] = " ";
            drFacturaG["EncaTitulo3"] = " ";
            drFacturaG["Nota1"] = " ";
            drFacturaG["Nota2"] = " ";
            drFacturaG["Nota3"] = " ";
            drFacturaG["RISR"] = "0.00";
            drFacturaG["RIVA"] = "0.00";
            drFacturaG["RIEPS"] = "0.00";
            drFacturaG["TIVA"] = "0.00";
            drFacturaG["TIEPS"] = "0.00";
            //Datos Complemento INE
            drFacturaG["TipoProceso"] = " ";
            drFacturaG["TipoComite"] = " ";
            drFacturaG["ClaveEntidad"] = " ";
            drFacturaG["Ambito"] = " ";
            drFacturaG["IdContabilidad"] = " ";

            drFacturaG["UUID_Relacionado"] = " ";
            #endregion

            int TotalNodos = 0;
            int TotalAtributos = 0;
            int TotalNodosRetLocal = 0;
            int TotalNodosINE = 0;
            string sNodoName = "";
            string sAtributeName = "";
            string sAtributeValues = "";

            XmlDocument oXml = new XmlDocument();
            oXml.Load(ArchivoXML);


            #region Lee Atributos del nodo COMPONENTE
            XmlNodeList nComprobante = oXml.ChildNodes;

            TotalAtributos = nComprobante.Item(1).Attributes.Count;
            sNodoName = nComprobante.Item(1).Name;
            for (int j = 0; j < TotalAtributos; j++)
            {
                sAtributeName = nComprobante.Item(1).Attributes.Item(j).Name.ToString();
                sAtributeValues = nComprobante.Item(1).Attributes.Item(j).Value.ToString();
                CargaDato_drFacturaG(ref drFacturaG, sNodoName, sAtributeName, sAtributeValues, TipoFac);
            }
            #endregion
            int _item = 0;
            #region Lee Atributo del nodo CFDI RELACIONADOS
            XmlNodeList nRelacionados = nComprobante.Item(1).ChildNodes;
            sNodoName = nRelacionados.Item(_item).Name;
            if (sNodoName == "cfdi:CfdiRelacionados")
            {
                TotalAtributos = nRelacionados.Item(0).Attributes.Count;
                for (int j = 0; j < TotalAtributos; j++)
                {
                    sAtributeName = nRelacionados.Item(0).Attributes.Item(j).Name.ToString();
                    sAtributeValues = nRelacionados.Item(0).Attributes.Item(j).Value.ToString();
                    CargaDato_drFacturaG(ref drFacturaG, sNodoName, sAtributeName, sAtributeValues, TipoFac);
                }

                XmlNodeList nRelacionado = nRelacionados.Item(0).ChildNodes;
                TotalNodos = nRelacionado.Count;
                sAtributeValues = "";
                for (int i = 0; i < TotalNodos; i++)
                {
                    TotalAtributos = nRelacionado.Item(i).Attributes.Count;
                    sNodoName = nRelacionado.Item(i).Name;
                    for (int j = 0; j < TotalAtributos; j++)
                    {
                        sAtributeName = nRelacionado.Item(i).Attributes.Item(j).Name;
                        sAtributeValues += nRelacionado.Item(i).Attributes.Item(j).Value;
                        sAtributeValues += i < TotalNodos - 1 ? ", " : "";
                    }
                }
                CargaDato_drFacturaG(ref drFacturaG, sNodoName, sAtributeName, sAtributeValues, TipoFac);
                sAtributeValues = "";
                _item++;
            }
            #endregion

            #region Lee Atributos del nodo EMISOR
            XmlNodeList nEmisor = nComprobante.Item(1).ChildNodes;

            TotalAtributos = nEmisor.Item(_item).Attributes.Count;
            sNodoName = nEmisor.Item(_item).Name;
            for (int j = 0; j < TotalAtributos; j++)
            {
                sAtributeName = nEmisor.Item(_item).Attributes.Item(j).Name.ToString();
                sAtributeValues = nEmisor.Item(_item).Attributes.Item(j).Value.ToString();
                CargaDato_drFacturaG(ref drFacturaG, sNodoName, sAtributeName, sAtributeValues, TipoFac);
            }
            _item++;
            #endregion

            #region Lee Atributos del nodo DOMICILIO DEL EMISOR
            //XmlNodeList nEDomicilio = nEmisor.Item(0).ChildNodes;
            //TotalNodos = nEDomicilio.Count;

            //for (int i = 0; i < TotalNodos; i++)
            //{
            //    TotalAtributos = nEDomicilio.Item(i).Attributes.Count;
            //    sNodoName = nEDomicilio.Item(i).Name;

            //    for (int j = 0; j < TotalAtributos; j++)
            //    {
            //        sAtributeName = nEDomicilio.Item(i).Attributes.Item(j).Name;
            //        sAtributeValues = nEDomicilio.Item(i).Attributes.Item(j).Value;
            //        CargaDato_drFacturaG(ref drFacturaG, sNodoName, sAtributeName, sAtributeValues, TipoFac);

            //    }
            //}
            #endregion

            #region Lee nodo RECEPTOR
            XmlNodeList nReceptor = nComprobante.Item(1).ChildNodes;
            TotalAtributos = nReceptor.Item(_item).Attributes.Count;
            sNodoName = nReceptor.Item(_item).Name;
            for (int j = 0; j < TotalAtributos; j++)
            {
                sAtributeName = nReceptor.Item(_item).Attributes.Item(j).Name.ToString();
                sAtributeValues = nReceptor.Item(_item).Attributes.Item(j).Value.ToString();
                CargaDato_drFacturaG(ref drFacturaG, sNodoName, sAtributeName, sAtributeValues, TipoFac);
            }
            _item++;
            #endregion

            #region Lee nodo DOMICILIO del Receptor
            //XmlNodeList nRDomicilio = nReceptor.Item(1).ChildNodes;
            //TotalNodos = nRDomicilio.Count;

            //for (int i = 0; i < TotalNodos; i++)
            //{
            //    TotalAtributos = nRDomicilio.Item(i).Attributes.Count;
            //    sNodoName = nRDomicilio.Item(i).Name;

            //    for (int j = 0; j < TotalAtributos; j++)
            //    {
            //        sAtributeName = nRDomicilio.Item(i).Attributes.Item(j).Name;
            //        sAtributeValues = nRDomicilio.Item(i).Attributes.Item(j).Value;
            //        CargaDato_drFacturaG(ref drFacturaG, sNodoName, sAtributeName, sAtributeValues, TipoFac);

            //    }
            //}
            #endregion

            #region Lee nodo CONCEPTO
            XmlNodeList nConceptos = nComprobante.Item(1).ChildNodes;
            XmlNodeList nConcepto = nConceptos.Item(_item).ChildNodes;
            TotalNodos = nConcepto.Count;

            for (int i = 0; i < TotalNodos; i++)
            {
                TotalAtributos = nConcepto.Item(i).Attributes.Count;
                sNodoName = nConcepto.Item(i).Name + i.ToString();

                DataRow drFacturaD = dtFacturaD.NewRow();
                drFacturaD["Serie"] = drFacturaG["Serie"];
                drFacturaD["Folio"] = drFacturaG["Folio"];

                drFacturaD["eTelefono"] = TelEmi;
                drFacturaD["sTelefono"] = TelSucu;
                drFacturaD["cTelefono"] = TelCli;
                drFacturaD["DiasCredito"] = DiasCredito;

                #region Introducir Campo vendedor en factura
                if (rb_fac.Checked == true)
                {
                    com.Connection = conn;
                    vendedor = "";
                    com.CommandText = "SELECT DISTINCT Vendedor FROM Fac_Mov Where (Factura = " + folio + ") AND (TipoM = '" + tb_tm.Text + "')";
                    conn.Open();
                    vendedor = com.ExecuteScalar().ToString();
                    conn.Close();
                    drFacturaG["Vendedor"] = vendedor;
                }
                #endregion

                #region Buscando el contribuyente
                con2.Close();
                con2.Open();
                com2.Connection = con2;
                com2.CommandText = "Select Contribuyente From cfdi.empresa where (Clave = " + cb_ClaveEmp.Text + ")";
                drFacturaD["Contribuyente"] = com2.ExecuteScalar().ToString();
                com2.Dispose();
                con2.Close();
                #endregion 

                for (int j = 0; j < TotalAtributos; j++)
                {
                    sAtributeName = nConcepto.Item(i).Attributes.Item(j).Name;
                    sAtributeValues = nConcepto.Item(i).Attributes.Item(j).Value;
                    CargaDato_drFacturaD(ref drFacturaD, sNodoName, sAtributeName, sAtributeValues);
                }

                #region Descripcion Renglones
                DataTable dtFac = new DataTable();
                try
                {
                    ConectPOS();
                    conn.Open();
                    com.Connection = conn;
                    com.CommandText = "SELECT COUNT(*) AS Expr1 FROM Fac_MDes WHERE (Factura = " + folio + ")";
                    int num = Convert.ToInt32(com.ExecuteScalar());

                    if (num >= 1)
                    {
                        com.CommandText = "SELECT Descripcion FROM Fac_MDes WHERE (Factura = " + folio + ") AND (Codigo = '" + drFacturaD["noIdentificacion"] + "')";
                        adapt.SelectCommand = com;
                        adapt.Fill(dtFac);

                        drFacturaD["descripcion1"] = dtFac.Rows[0]["Descripcion"].ToString();
                        drFacturaD["descripcion2"] = dtFac.Rows[1]["Descripcion"].ToString();
                        drFacturaD["descripcion3"] = dtFac.Rows[2]["Descripcion"].ToString();
                        drFacturaD["descripcion4"] = dtFac.Rows[3]["Descripcion"].ToString();
                        drFacturaD["descripcion5"] = dtFac.Rows[4]["Descripcion"].ToString();
                        drFacturaD["descripcion6"] = dtFac.Rows[5]["Descripcion"].ToString();
                        drFacturaD["descripcion7"] = dtFac.Rows[6]["Descripcion"].ToString();
                        drFacturaD["descripcion8"] = dtFac.Rows[7]["Descripcion"].ToString();
                        drFacturaD["descripcion9"] = dtFac.Rows[8]["Descripcion"].ToString();
                        drFacturaD["descripcion10"] = dtFac.Rows[9]["Descripcion"].ToString();
                        drFacturaD["descripcion11"] = dtFac.Rows[10]["Descripcion"].ToString();
                        drFacturaD["descripcion12"] = dtFac.Rows[11]["Descripcion"].ToString();
                        drFacturaD["descripcion13"] = dtFac.Rows[12]["Descripcion"].ToString();
                        drFacturaD["descripcion14"] = dtFac.Rows[13]["Descripcion"].ToString();
                        drFacturaD["descripcion15"] = dtFac.Rows[14]["Descripcion"].ToString();
                        drFacturaD["descripcion16"] = dtFac.Rows[15]["Descripcion"].ToString();
                        drFacturaD["descripcion17"] = dtFac.Rows[16]["Descripcion"].ToString();
                        drFacturaD["descripcion18"] = dtFac.Rows[17]["Descripcion"].ToString();
                        drFacturaD["descripcion19"] = dtFac.Rows[18]["Descripcion"].ToString();
                        drFacturaD["descripcion20"] = dtFac.Rows[19]["Descripcion"].ToString();
                    }
                    else
                    {
                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    com.Dispose();
                    conn.Close();
                }
                dtFac = dtFacturaD.Copy();
                #endregion

                dtFacturaD.Rows.Add(drFacturaD);
                //CFDI 3.3 Get Impuesto Traslado del Concepto
                XmlNodeList cImpuestos = nConcepto.Item(i).ChildNodes;

                //CFDI v3.3 Get Numero Cuenta Predial 
                int countNodos = cImpuestos.Count;
                for (int p = 0; p < countNodos; p++)
                {
                    string nodoName = cImpuestos.Item(p).Name;
                    if (nodoName == "cfdi:CuentaPredial")
                    {
                        XmlNode cCuentaPredial = cImpuestos.Item(p);
                        string predsNodoName = cCuentaPredial.Name;
                        string predAtributeName = cCuentaPredial.Attributes.Item(0).Name.ToString();
                        string predAtributeValues = cCuentaPredial.Attributes.Item(0).Value.ToString();
                        CargaDato_drFacturaD(ref drFacturaD, predsNodoName, predAtributeName, predAtributeValues);
                    }
                }

                XmlNodeList cTipoImpuestos = cImpuestos.Item(0).ChildNodes;
                int ImpTotalNodos = cTipoImpuestos.Count;
                for (int t = 0; t < ImpTotalNodos; t++)
                {
                    string cNodoName = cTipoImpuestos.Item(t).Name;
                    if (cNodoName == "cfdi:Traslados")
                    {
                        XmlNodeList cTrasladado = cTipoImpuestos.Item(t).ChildNodes;
                        DataRow drTraslados = dtTraslados.NewRow();
                        int cTotalNodos = cTrasladado.Count;
                        string cAtributeName = "";
                        string cAtributeValues = "";
                        for (int c = 0; c < cTotalNodos; c++)
                        {
                            int cTotalAtributos = cTrasladado.Item(c).Attributes.Count;
                            string cNodoNameT = cTrasladado.Item(c).Name;
                            for (int cT = 0; cT < cTotalAtributos; cT++)
                            {
                                cAtributeName = cTrasladado.Item(c).Attributes.Item(cT).Name;
                                cAtributeValues = cTrasladado.Item(c).Attributes.Item(cT).Value;
                                CargaDato_drTraslados(ref drTraslados, cNodoNameT, cAtributeName, cAtributeValues);
                            }
                        }
                        dtTraslados.Rows.Add(drTraslados);
                    }
                    if (cNodoName == "cfdi:Retenciones")
                    {
                        XmlNodeList cRetencion = cTipoImpuestos.Item(t).ChildNodes;
                        DataRow drRetenciones = dtRetenciones.NewRow();
                        int cTotalNodosRet = cRetencion.Count;
                        string cAtributeNameRet = "";
                        string cAtributeValuesRet = "";
                        for (int c = 0; c < cTotalNodosRet; c++)
                        {
                            int cTotalAtributosRet = cRetencion.Item(c).Attributes.Count;
                            string cNodoNameRet = cRetencion.Item(c).Name;

                            for (int cT = 0; cT < cTotalAtributosRet; cT++)
                            {
                                cAtributeNameRet = cRetencion.Item(c).Attributes.Item(cT).Name;
                                cAtributeValuesRet = cRetencion.Item(c).Attributes.Item(cT).Value;
                                CargaDato_drRetenciones(ref drRetenciones, cNodoNameRet, cAtributeNameRet, cAtributeValuesRet);
                            }
                        }
                        dtRetenciones.Rows.Add(drRetenciones);
                    }
                }
            }

            dsFactura.Tables.Add(dtFacturaD);
            #endregion
            _item++;

            #region Lee nodo Total Impuesto
            XmlNodeList nImpuestosTotales = nComprobante.Item(1).ChildNodes;
            TotalAtributos = nImpuestosTotales.Item(_item).Attributes.Count;
            sNodoName = nImpuestosTotales.Item(_item).Name;
            for (int j = 0; j < TotalAtributos; j++)
            {
                sAtributeName = nImpuestosTotales.Item(_item).Attributes.Item(j).Name.ToString();
                sAtributeValues = nImpuestosTotales.Item(_item).Attributes.Item(j).Value.ToString();
                CargaDato_drFacturaG(ref drFacturaG, sNodoName, sAtributeName, sAtributeValues, TipoFac);
            }

            XmlNodeList nImpuesto = nImpuestosTotales.Item(_item).ChildNodes;
            TotalNodos = nImpuesto.Count;
            for (int n = 0; n < TotalNodos; n++)
            {
                sNodoName = nImpuesto.Item(n).Name;

                if (sNodoName == "cfdi:Retenciones")
                {
                    XmlNodeList nTotalRetenido = nImpuesto.Item(n).ChildNodes;
                    int TotalNodosR = nTotalRetenido.Count;

                    for (int i = 0; i < TotalNodosR; i++)
                    {
                        TotalAtributos = nTotalRetenido.Item(i).Attributes.Count;
                        sNodoName = nTotalRetenido.Item(i).Name;

                        string Ret_Type = "";

                        for (int j = 0; j < TotalAtributos; j++)
                        {
                            sAtributeName = nTotalRetenido.Item(i).Attributes.Item(j).Name;
                            sAtributeValues = nTotalRetenido.Item(i).Attributes.Item(j).Value;
                            if (sAtributeName == "Impuesto")
                            {
                                switch (sAtributeValues)
                                {
                                    case "001":
                                        Ret_Type = "RISR";
                                        break;
                                    case "002":
                                        Ret_Type = "RIVA";
                                        break;
                                    case "003":
                                        Ret_Type = "RIEPS";
                                        break;
                                }
                            }
                            if (sAtributeName == "Importe")
                                CargaDato_drFacturaG(ref drFacturaG, sNodoName, Ret_Type, sAtributeValues, "");
                        }
                    }
                }

                if (sNodoName == "cfdi:Traslados")
                {
                    XmlNodeList nTotalTrasladado = nImpuesto.Item(n).ChildNodes;
                    int TotalNodosT = nTotalTrasladado.Count;

                    for (int i = 0; i < TotalNodosT; i++)
                    {
                        TotalAtributos = nTotalTrasladado.Item(i).Attributes.Count;
                        sNodoName = nTotalTrasladado.Item(i).Name;

                        string Tras_Type = "";
                        for (int j = 0; j < TotalAtributos; j++)
                        {
                            sAtributeName = nTotalTrasladado.Item(i).Attributes.Item(j).Name;
                            sAtributeValues = nTotalTrasladado.Item(i).Attributes.Item(j).Value;
                            if (sAtributeName == "Impuesto")
                            {
                                switch (sAtributeValues)
                                {
                                    case "002":
                                        Tras_Type = "TIVA";
                                        break;
                                    case "003":
                                        Tras_Type = "TIEPS";
                                        break;
                                }
                            }
                            if (sAtributeName == "Importe" && sAtributeValues != "0.00")
                                CargaDato_drFacturaG(ref drFacturaG, sNodoName, Tras_Type, sAtributeValues, "");
                        }
                    }
                }
            }
            #endregion
            _item++;

            #region Lee nodo Complemento timbrado
            
            XmlNodeList nComplementos = nComprobante.Item(1).ChildNodes;
            XmlNodeList nComplemento = nComplementos.Item(_item).ChildNodes;
            TotalNodos = nComplemento.Count;

            for (int i = 0; i < TotalNodos; i++)
            {
                TotalAtributos = nComplemento.Item(i).Attributes.Count;
                sNodoName = nComplemento.Item(i).Name;

                if (sNodoName == "implocal:ImpuestosLocales")
                {
                    XmlNodeList nImpuestosLocales = nComplemento.Item(0).ChildNodes;
                    TotalNodosRetLocal = nImpuestosLocales.Count;

                    DataRow drFacturaRetLoc = dtFacturaRetLoc.NewRow();

                    for (int ii = 0; ii < TotalNodosRetLocal; ii++)
                    {
                        TotalAtributos = nImpuestosLocales.Item(ii).Attributes.Count;
                        sNodoName = nImpuestosLocales.Item(ii).Name;

                        drFacturaRetLoc["serie"] = drFacturaG["Serie"];
                        drFacturaRetLoc["folio"] = drFacturaG["Folio"];

                        for (int j = 0; j < TotalAtributos; j++)
                        {
                            sAtributeName = nImpuestosLocales.Item(ii).Attributes.Item(j).Name;
                            sAtributeValues = nImpuestosLocales.Item(ii).Attributes.Item(j).Value;
                            CargaDato_drFacturaRetLoc(ref drFacturaRetLoc, sNodoName, sAtributeName, sAtributeValues, ii);
                        }
                    }

                    dtFacturaRetLoc.Rows.Add(drFacturaRetLoc);
                }
                else if (sNodoName == "ine:INE")
                {
                    //Get ine:INE Attributes
                    for (int j = 0; j < TotalAtributos; j++)
                    {
                        sAtributeName = nComplemento.Item(i).Attributes.Item(j).Name;
                        sAtributeValues = nComplemento.Item(i).Attributes.Item(j).Value;
                        CargaDato_drFacturaG(ref drFacturaG, sNodoName, sAtributeName, sAtributeValues, TipoFac);
                    }

                    XmlNodeList nINEEmisor = nComplemento.Item(0).ChildNodes;
                    TotalNodosINE = nINEEmisor.Count;
                    if (TotalNodosINE > 0)
                    {
                        TotalAtributos = nINEEmisor.Item(0).Attributes.Count;
                        sNodoName = nINEEmisor.Item(0).Name;

                        for (int j = 0; j < TotalAtributos; j++)
                        {
                            sAtributeName = nINEEmisor.Item(i).Attributes.Item(j).Name;
                            sAtributeValues = nINEEmisor.Item(i).Attributes.Item(j).Value;
                            CargaDato_drFacturaG(ref drFacturaG, sNodoName, sAtributeName, sAtributeValues, TipoFac);
                        }
                    }

                    XmlNodeList nINEContabilidad = nINEEmisor.Item(0).ChildNodes;
                    TotalNodosINE = nINEContabilidad.Count;
                    if (TotalNodosINE > 0)
                    {
                        TotalAtributos = nINEContabilidad.Item(0).Attributes.Count;
                        sNodoName = nINEContabilidad.Item(0).Name;

                        for (int j = 0; j < TotalAtributos; j++)
                        {
                            sAtributeName = nINEContabilidad.Item(i).Attributes.Item(j).Name;
                            sAtributeValues = nINEContabilidad.Item(i).Attributes.Item(j).Value;
                            CargaDato_drFacturaG(ref drFacturaG, sNodoName, sAtributeName, sAtributeValues, TipoFac);
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < TotalAtributos; j++)
                    {
                        sAtributeName = nComplemento.Item(i).Attributes.Item(j).Name;
                        sAtributeValues = nComplemento.Item(i).Attributes.Item(j).Value;
                        CargaDato_drFacturaG(ref drFacturaG, sNodoName, sAtributeName, sAtributeValues, TipoFac);
                    }
                }
            }
            #endregion
            
            dtFacturaG.Rows.Add(drFacturaG);
            dsFactura.Tables.Add(dtFacturaG);
                     
            
            if(RetenValue == true)
                dsFactura.Tables.Add(dtFacturaRetLoc);
            
            dsFactura.Tables.Add(dtTraslados);
            
            return dsFactura;
        }

        public void CargaDato_drFacturaG(ref DataRow drFacturaG, string Nodo, string Atributo, string Valor, string TipoFac)
        {

            switch (Nodo)
            {
                case "cfdi:Comprobante":

                    switch (Atributo)
                    {
                        case "Version":
                            drFacturaG["Version"] = Valor;
                            break;
                        case "Serie":
                            drFacturaG["Serie"] = Valor;
                            break;
                        case "Folio":
                            drFacturaG["Folio"] = Valor;
                            break;
                        case "Fecha":
                            drFacturaG["Fecha"] = Valor;
                            break;
                        case "FormaPago":
                            drFacturaG["FormaPago"] = getFormaPago(Valor);
                            break;
                        case "CondicionesDePago":
                            drFacturaG["CondicionesDePago"] = Valor;
                            break;
                        case "SubTotal":
                            drFacturaG["SubTotal"] = Valor;
                            break;
                        case "Descuento":
                            drFacturaG["Descuento"] = Valor;
                            break;
                        case "TipoCambio":
                            drFacturaG["TipoCambio"] = Valor;
                            break;
                        case "Moneda":
                            drFacturaG["Moneda"] = Valor;
                            break;
                        case "Total":
                            drFacturaG["Total"] = Valor;
                            break;
                        case "MetodoPago":
                            drFacturaG["MetodoPago"] = Valor;
                            break;
                        case "TipoDeComprobante":
                            drFacturaG["TipoDeComprobante"] = Valor;
                            break;
                        case "LugarExpedicion":
                            drFacturaG["LugarExpedicion"] = Valor;
                            break;
                        case "Sello":
                            drFacturaG["Sello"] = Valor;
                            break;
                        case "NoCertificado":
                            drFacturaG["NoCertificado"] = Valor;
                            break;
                        case "Certificado":
                            drFacturaG["Certificado"] = Valor;
                            break;
                    }
                    break;
                case "cfdi:CfdiRelacionados":
                    switch (Atributo)
                    {
                        case "TipoRelacion":
                            drFacturaG["Tipo_Relacion"] = getTipoRelacion(Valor);
                            break;
                    }
                    break;
                case "cfdi:CfdiRelacionado":
                    switch (Atributo)
                    {
                        case "UUID":
                            drFacturaG["UUID_Relacionado"] = Valor;
                            break;
                    }
                    break;
                case "cfdi:Emisor":
                    switch (Atributo)
                    {
                        case "Rfc":
                            drFacturaG["erfc"] = Valor;
                            break;
                        case "Nombre":
                            drFacturaG["enombre"] = Valor;
                            break;
                        case "RegimenFiscal":
                            drFacturaG["RegimenFiscal"] = getRegimenFiscal(Valor);
                            break;
                    }
                    break;
                
                case "cfdi:Receptor":
                    switch (Atributo)
                    {
                        case "Rfc":
                            drFacturaG["rrfc"] = Valor;
                            break;
                        case "Nombre":
                            drFacturaG["rnombre"] = Valor;
                            break;
                        case "UsoCFDI":
                            drFacturaG["usoCFDI"] = getUsoCFDI(Valor);
                            break;
                    }
                    break;

                case "cfdi:Impuestos":
                    switch (Atributo) 
                    {
                        case "TotalImpuestosRetenidos":
                            drFacturaG["TotalRetenidos"] = Valor;
                            break;
                        case "TotalImpuestosTrasladados":
                            drFacturaG["TotalTrasladados"] = Valor;
                            break;
                    }
                    break;

                case "cfdi:Retencion":
                    switch (Atributo)
                    {
                        case "RISR":
                            drFacturaG["RISR"] = Valor;
                            break;
                        case "RIVA":
                            drFacturaG["RIVA"] = Valor;
                            break;
                        case "RIEPS":
                            drFacturaG["RIEPS"] = Valor;
                            break;
                    }
                    break;

                case "cfdi:Traslado":
                    switch (Atributo)
                    {
                        case "Base":
                            drFacturaG["Base"] = Valor;
                            break;
                        case "Impuesto":
                            drFacturaG["Impuesto"] = Valor;
                            break;
                        case "TipoFactor":
                            drFacturaG["TipoFactor"] = Valor;
                            break;
                        case "TasaOCuota":
                            drFacturaG["TadaOCuota"] = Valor;
                            break;
                        case "Importe":
                            drFacturaG["Importe"] = Valor;
                            break;
                        case "TIVA":
                            drFacturaG["TIVA"] = Valor;
                            break;
                        case "TIEPS":
                            drFacturaG["TIEPS"] = Valor;
                            break;
                    }
                    break;

                case "tfd:TimbreFiscalDigital":
                    switch (Atributo)
                    {
                        case "SelloCFD":
                            drFacturaG["SelloCFD"] = Valor;
                            break;

                        case "FechaTimbrado":
                            drFacturaG["FechaTimbrado"] = Valor;
                            break;

                        case "UUID":
                            drFacturaG["UUID"] = Valor;
                            break;

                        case "Version":
                            drFacturaG["Versiont"] = Valor;
                            break;

                        case "NoCertificadoSAT":
                            drFacturaG["NoCertificadoSAT"] = Valor;
                            break;

                        case "SelloSAT":
                            drFacturaG["SelloSAT"] = Valor;
                            break;
                    }
                    break;
                case "ine:INE":
                    switch (Atributo)
                    {
                        case "TipoProceso":
                            drFacturaG["TipoProceso"] = Valor;
                            break;
                        case "TipoComite":
                            drFacturaG["TipoComite"] = Valor;
                            break;
                    }
                    break;
                case "ine:Entidad":
                    switch (Atributo)
                    {
                        case "ClaveEntidad":
                            drFacturaG["ClaveEntidad"] = Valor;
                            break;
                        case "Ambito":
                            drFacturaG["Ambito"] = Valor;
                            break;
                    }
                    break;
                case "ine:Contabilidad":
                    switch (Atributo)
                    {
                        case "IdContabilidad":
                            drFacturaG["IdContabilidad"] = Valor;
                            break;
                    }
                    break;
            }
        }

        public static void CargaDato_drFacturaD(ref DataRow drFacturaD, string Nodo, string Atributo, string Valor)
        {
            switch (Nodo)
            {
                case "cfdi:CuentaPredial":
                    switch (Atributo)
                    {
                        case "Numero":
                            drFacturaD["NumeroPredial"] = Valor;
                            break;
                    }
                    break;
                default:
                    switch (Atributo)
                    {
                        case "ClaveProdServ":
                            drFacturaD["ClaveProdServ"] = Valor;
                            break;
                        case "NoIdentificacion":
                            drFacturaD["NoIdentificacion"] = Valor;
                            break;
                        case "Cantidad":
                            drFacturaD["Cantidad"] = Valor;
                            break;
                        case "ClaveUnidad":
                            drFacturaD["ClaveUnidad"] = Valor;
                            break;
                        case "Unidad":
                            drFacturaD["Unidad"] = Valor;
                            break;
                        case "Descripcion":
                            drFacturaD["Descripcion"] = Valor;
                            break;
                        case "ValorUnitario":
                            drFacturaD["ValorUnitario"] = Valor;
                            break;
                        case "Importe":
                            drFacturaD["Importe"] = Valor;
                            break;
                    }
                    break;                
            }
        }

        public static void CargaDato_drFacturaRetLoc(ref DataRow drFacturaRetLoc, string Nodo, string Atributo, string Valor, int i)
        {
            switch (Atributo)
            {
                case "ImpLocRetenido":
                    drFacturaRetLoc["DescImpLocRetenido"+i] = Valor;
                    break;

                case "Importe":
                    drFacturaRetLoc["ImporteLocRetenido"+i] = Valor;
                    break;
            }
        }

        public void CargaDato_drTraslados(ref DataRow drTraslados, string Nodo, string Atributo, string Valor)
        {            
            switch (Atributo)
            {                
                case "Base":
                    drTraslados["Base"] = Valor;
                    break;
                case "Impuesto":
                    drTraslados["Impuesto"] = getImpuesto(Valor);
                    break;
                case "TipoFactor":
                    drTraslados["TipoFactor"] = Valor;
                    break;
                case "TasaOCuota":
                    drTraslados["TasaOCuota"] = Valor;
                    break;
                case "Importe":
                    drTraslados["Importe"] = Valor;
                    break;
            }
        }
        
        public void CargaDato_drRetenciones(ref DataRow drRetenciones, string Nodo, string Atributo, string Valor)
        {
            switch (Atributo)
            {
                case "Base":
                    drRetenciones["Base"] = Valor;
                    break;
                case "Impuesto":
                    drRetenciones["Impuesto"] = getImpuesto(Valor);
                    break;
                case "TipoFactor":
                    drRetenciones["TipoFactor"] = Valor;
                    break;
                case "TasaOCuota":
                    drRetenciones["TasaOCuota"] = Valor;
                    break;
                case "Importe":
                    drRetenciones["Importe"] = Valor;
                    break;
            }
        }

        public static Byte[] ImageToByte(Image pImagen)
        {
            Byte[] mImage = null;

            MemoryStream ms = new MemoryStream();
            pImagen.Save(ms, pImagen.RawFormat);
            mImage = ms.GetBuffer();
            ms.Close();

            return mImage;
        }     
        #endregion

        private void btn_enviar_Click(object sender, EventArgs e)
        {
            Correo send = new Correo(RutaXML,  PDFArchivoXML_Timbrado, RutaPDF,  ArchivoPDF_CFDI, cb_ClaveEmp.Text, CorreoCli);
            send.Show();
        }

        private void rb_addenda_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_addenda.Checked != false)
            {
                Addendas addendas = new Addendas(cb_ClaveEmp.Text);
                addendas.ShowDialog();

                if (addendas.emisorRI != null)
                {
                    EmisorRI = addendas.emisorRI;
                    ReceptorRI = addendas.receptorRI;
                    NumProv = addendas.numProv;
                    FolioAddenda = addendas.folioCompra;
                    ProveEkt = addendas.proveEkt;
                }
                else 
                {
                    rb_addenda.Checked = false;
                }            
            }
        }

        private void rb_pepsico_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_pepsico.Checked != false)
            {
                AddendaPepsico addPepsico = new AddendaPepsico();
                addPepsico.ShowDialog();

                if (addPepsico.numPedido != null)
                {
                    NumPedido = addPepsico.numPedido;
                    NumProveedor = addPepsico.numProveedor;
                    NumRecepcion = addPepsico.numRecepcion;
                }
                else
                {
                    this.rb_pepsico.Checked = false;                    
                }
            }
        }

        private void retLocales_btn_Click(object sender, EventArgs e)
        {
            RetencionesLocales retLocales = new RetencionesLocales(RetenList);
            retLocales.ShowDialog();

            if (retLocales.RetLocalesValue != false)
            {
                RetenList = retLocales.dtRetLocales;
                RetenValue = retLocales.RetLocalesValue;

                object sumRetLoc;
                sumRetLoc = RetenList.Compute("Sum(importe_retLocal)", "tasa_retLocal > 0");

                label7.Text = "Importe";
                lbl_retLocales.Visible = true;
                tb_retLocales.Visible = true;
                tb_retLocales.Text = sumRetLoc.ToString();
                lbl_RetTotal.Visible = true;
                tb_RetTotal.Visible = true;

                double ret_total = (Convert.ToDouble(tb_total.Text)) - (Convert.ToDouble(tb_retLocales.Text));
                tb_RetTotal.Text = ret_total.ToString("N2");
            }
            else 
            {
                label7.Text = "Total:";
                lbl_retLocales.Visible = false;
                tb_retLocales.Visible = false;
                tb_retLocales.Clear();
                lbl_RetTotal.Visible = false;
                tb_RetTotal.Visible = false;
                tb_RetTotal.Clear();
            }
        }
        
        public bool validarValores() 
        {
            bool valid = false;
            if (cb_formapago.Text != "Seleccione.." && cb_usocfdi.Text != "Seleccione..")
                valid = true;
            
            return valid;
        }

        private void rb_INE_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_INE.Checked == true)
            {
                ComplementoINE ine = new ComplementoINE();
                ine.ShowDialog();

                if (ine.tipoProceso != null)
                {
                    TipoProceso = ine.tipoProceso;
                    TipoComite = ine.tipoComite;
                    TipoAmbito = ine.tipoAmbito;
                    CveEntidad = ine.cveEntidad;
                    IdContabilidad = ine.idContabilidad;
                    INEValue = true;
                }
                else
                {
                    rb_INE.Checked = false;
                    INEValue = false;
                }
            }
        }

        public bool existeTraslado(DataGridView grid_impuestos, string prod_codigo, string importe_base) 
        {
            bool existe = false;
            foreach (DataGridViewRow row in grid_impuestos.Rows)
            {
                if (this.rb_tick.Checked == true)
                {
                    if (prod_codigo != "" && importe_base != "")
                    {
                        if (row.Cells[2].Value.ToString().Contains("Traslado") && prod_codigo == row.Cells[1].Value.ToString())
                        {
                            existe = true;
                            return existe;
                        }
                    }
                    else
                    {
                        if (row.Cells[2].Value.ToString().Contains("Traslado"))
                        {
                            existe = true;
                            return existe;
                        }
                    }
                }
                else
                {
                    if (prod_codigo != "" && importe_base != "")
                    {
                        if (row.Cells[1].Value.ToString().Contains("Traslado") && importe_base == row.Cells[2].Value.ToString() && prod_codigo == row.Cells[0].Value.ToString())
                        {
                            existe = true;
                            return existe;
                        }
                    }
                    else
                    {
                        if (row.Cells[1].Value.ToString().Contains("Traslado"))
                        {
                            existe = true;
                            return existe;
                        }
                    }
                }
            }
            return existe;
        }
        
        public bool existeRetencion(DataGridView grid_impuestos, string prod_codigo, string importe_base) 
        {
            bool existe = false;
            foreach (DataGridViewRow row in grid_impuestos.Rows)
            {
                if (this.rb_tick.Checked == true)
                {
                    if (prod_codigo != "" && importe_base != "")
                    {
                        if (row.Cells[2].Value.ToString().Contains("Retención") && prod_codigo == row.Cells[1].Value.ToString())
                        {
                            existe = true;
                            return existe;
                        }
                    }
                    else
                    {
                        if (row.Cells[2].Value.ToString().Contains("Retención"))
                        {
                            existe = true;
                            return existe;
                        }
                    }
                }
                else
                {
                    if (prod_codigo != "" && importe_base != "")
                    {
                        if (row.Cells[1].Value.ToString().Contains("Retención") && importe_base == row.Cells[2].Value.ToString() && prod_codigo == row.Cells[0].Value.ToString())
                        {
                            existe = true;
                            return existe;
                        }
                    }
                    else
                    {
                        if (row.Cells[1].Value.ToString().Contains("Retención"))
                        {
                            existe = true;
                            return existe;
                        }
                    }
                }
            }
            return existe;
        }

        private decimal TotalImpuestos(string tipo, string impuesto_clave, DataGridView dgImpuestos) 
        { 
            decimal sumImpuesto = 0;
            foreach(DataGridViewRow row in dgImpuestos.Rows)
            {
                if (this.rb_tick.Checked == true)
                {
                    if (row.Cells[2].Value.ToString() == tipo && row.Cells[4].Value.ToString().Contains(impuesto_clave))
                    {
                        sumImpuesto = sumImpuesto + Convert.ToDecimal(row.Cells[7].Value);
                    }
                }
                else
                {
                    if (row.Cells[1].Value.ToString() == tipo && row.Cells[3].Value.ToString().Contains(impuesto_clave))
                    {
                        sumImpuesto = sumImpuesto + Convert.ToDecimal(row.Cells[6].Value);
                    }
                }
            }

            return sumImpuesto;
        }

        public string getRegimenFiscal(string id)
        {
            if (con2.State.ToString() == "Closed")
                con2.Open();
            com2.Connection = con2;
            com2.CommandText = "SELECT Descripcion FROM cfdi.c_regimenfiscal WHERE id_regimenfiscal = " + id + ";";
            string regimen = com2.ExecuteScalar().ToString();
            return id + " - " + regimen;
        }

        public string getUsoCFDI(string id)
        {
            if (con2.State.ToString() == "Closed")
                con2.Open();
            com2.Connection = con2;
            com2.CommandText = "SELECT descripcion FROM cfdi.c_usocfdi where id_usocfdi ='" + id + "';";
            string usocfdi = com2.ExecuteScalar().ToString();
            return id + " - " + usocfdi;
        }

        public string getFormaPago(string id)
        {
            if (con2.State.ToString() == "Closed")
                con2.Open();
            com2.Connection = con2;
            com2.CommandText = "SELECT Descripcion FROM cfdi.c_formapago where id_formapago ='" + id + "';";
            string formaPago = com2.ExecuteScalar().ToString();            
            return id + " - " + formaPago;
        }

        public string getImpuesto(string id)
        {
            if (con2.State.ToString() == "Closed")
                con2.Open();
            com2.Connection = con2;
            com2.CommandText = "SELECT Descripcion FROM cfdi.c_impuesto WHERE id_impuesto = '" + id + "';";
            string impuesto = com2.ExecuteScalar().ToString();            
            return id + " " + impuesto;
        }

        public string getTipoRelacion(string id)
        {
            if (con2.State.ToString() == "Closed")
                con2.Open();
            com2.Connection = con2;
            com2.CommandText = "SELECT Descripcion FROM cfdi.c_tiporelacion WHERE id_tiporelacion = '" + id + "';";
            string tipoRelacion = com2.ExecuteScalar().ToString();
            return id + " " + tipoRelacion;
        }

        public void ReCalcularSubTotales(string _tipo, string _impuesto, decimal _importe)
        {
            string tipo_impuesto = _tipo;
            string impuesto = _impuesto;
            decimal importe = _importe;

            if (tipo_impuesto == "Traslado")
            {
                if (impuesto.Contains("IVA"))
                {
                    tb_iva.Text = (Convert.ToDecimal(tb_iva.Text) - importe).ToString("N2");
                }
                else
                {
                    tb_ieps.Text = (Convert.ToDecimal(tb_ieps.Text) - importe).ToString("N2");
                }
            }
            else
            {
                if (impuesto.Contains("IVA"))
                {
                    tb_rtIVA.Text = (Convert.ToDecimal(tb_rtIVA.Text) - importe).ToString("N2");
                }
                if (impuesto.Contains("ISR"))
                {
                    tb_retISR.Text = (Convert.ToDecimal(tb_retISR.Text) - importe).ToString("N2");
                }
                if (impuesto.Contains("IEPS"))
                {
                    tb_rtIEPS.Text = (Convert.ToDecimal(tb_rtIEPS.Text) - importe).ToString("N2");
                }
            }

            decimal _Total = ((Convert.ToDecimal(tb_subtotal.Text) - Convert.ToDecimal(tb_desc.Text)) + Convert.ToDecimal(tb_ieps.Text) + Convert.ToDecimal(tb_iva.Text))
                             - Convert.ToDecimal(tb_retISR.Text) - Convert.ToDecimal(tb_rtIVA.Text) - Convert.ToDecimal(tb_rtIEPS.Text);
            tb_total.Text = _Total.ToString("N2");
        }

        private void dg_impuestos_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {            
            string tipo = "";
            string impuesto = "";
            decimal importe = 0;
            if (rb_tick.Checked == true)
            {
                tipo = e.Row.Cells[2].Value.ToString();
                impuesto = e.Row.Cells[4].Value.ToString();
                importe = Convert.ToDecimal(e.Row.Cells[7].Value);
            }
            else
            {
                tipo = e.Row.Cells[1].Value.ToString();
                impuesto = e.Row.Cells[3].Value.ToString();
                importe = Convert.ToDecimal(e.Row.Cells[6].Value);
            }            

            ReCalcularSubTotales(tipo, impuesto, importe);
        }

        private void cb_usocfdi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_usocfdi.SelectedValue.ToString() == "D05")
            {
                tb_predial.Enabled = true;
            }
            else
                tb_predial.Enabled = false;
        }

        private void relacionar_cfdi_btn_Click(object sender, EventArgs e)
        {
            RelacionarCFDI relacionarcfdi = new RelacionarCFDI(tb_tm.Text, lbl_fRFC.Text, Convert.ToInt32(cb_ClaveEmp.Text));
            relacionarcfdi.ShowDialog();
            
            if (relacionarcfdi.docsRelacionados_tabla.Rows.Count > 0)
            {
                hasDocsRelacionados = true;
                tipo_relacion = relacionarcfdi.tipoRelacion;
                docRelacionados = relacionarcfdi.docsRelacionados_tabla;
            }
        }

        //-------------------------------------------------------------------------------------
        // CFDO Desarrollado por Halcón Diviono. He implementado CFDI, CFD, CBB por Marisol Moreno C.
        //-------------------------------------------------------------------------------------
    }
}
