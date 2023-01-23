using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Schema;
using ThoughtWorks.QRCode;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Drawing.Imaging;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Win32;
using System.Data.OleDb;

namespace SiscomCFDI
{
    public partial class Pagos : Form
    {
        string servidor;
        string usuario;
        string passw;
        string RutaPDF;
        string RutaXML;
        string RutaNoTimb;
        string Ruta_fac;
        string RutaBMP;
        string RutaLogo;
        string Ruta_cbb;
        string ArchivoXML;
        string ArchivoXML_Timbrado;
        string RutaXML_Timbrado;
        string ArchivoPDF_CFDI;
        string PDFArchivoXML_Timbrado;
        string Rpt_Fac;        
        string strOriginal;
        private string sTargetFolder;
        string Estatus;
        string path;
        string emisorRFC;
        string emisorRegFiscal;
        string emisorCP;
        string emisorEmail;
        string receptorRFC;
        string receptorNombre;
        string receptorUsoCFDI;
        public string Fechaxml;
        string UUID_SAT;
        public string fechaPago;
        string FormaPago;
        string metodoPago_DR;
        bool is_search;
        string rfc_origen;
        string banco;
        string cta_ordenante;
        string rfc_beneficiaria;
        string cta_beneficiaria;
        string TM;
        string BDPOS;

        DataSet dsCFDI;
        DataSet ds_CFDI_timbrado;
        OpenFileDialog XMLDialog = new OpenFileDialog();        
        MySqlConnection con;
        MySqlCommand com = new MySqlCommand();
        MySqlDataAdapter da = new MySqlDataAdapter();
        DataSet ds = new DataSet();
        DataTable _pagos = new DataTable();


        public Pagos()
        {
            InitializeComponent();
            dtp_fechaPago.MaxDate = DateTime.Now;
            dtp_fechaPago.Value = DateTime.Now;
        }

        private void Pagos_Load(object sender, EventArgs e)
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
                da.Fill(ds, "Clave");
                con.Close();
                cb_ClaveEmp.DataSource = ds.Tables["Clave"].DefaultView;
                cb_ClaveEmp.DisplayMember = "Clave";

                //load Forma de pago
                string query = "SELECT * FROM cfdi.c_formapago Where id_formapago <> 99;";
                MySqlDataAdapter mysqlda = new MySqlDataAdapter(query, con);
                DataSet ds2 = new DataSet();
                mysqlda.Fill(ds2, "c_formapago");
                cb_formapago.DisplayMember = "Descripcion";
                cb_formapago.ValueMember = "id_formapago";
                cb_formapago.DataSource = ds2.Tables["c_formapago"];
                cb_formapago.Text = "Seleccione..";
                               
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
            getEmisor();

            _pagos = new DataTable();
            _pagos.Columns.Add("Id Documento", typeof(string));
            _pagos.Columns.Add("Serie",typeof(string));
            _pagos.Columns.Add("Folio", typeof(string));
            _pagos.Columns.Add("Moneda", typeof(string));
            _pagos.Columns.Add("Tipo de Cambio", typeof(decimal));
            _pagos.Columns.Add("Método de Pago", typeof(string));
            _pagos.Columns.Add("Número de Parcialidad", typeof(int));
            _pagos.Columns.Add("Saldo Anterior", typeof(decimal));
            _pagos.Columns.Add("Importe Pagado", typeof(decimal));
            _pagos.Columns.Add("Saldo Insoluto", typeof(decimal));
        }
                
        public void getEmisor()
        {
            con.Open();
            com.Connection = con;
            com.CommandText = "Select Nombre, RFC, RegFiscal, sCP, Ruta_PDF, Ruta_BMP, Ruta_logo, Ruta_fact, Ruta_NoTimb, Ruta_cbb, Reporte_fac, Correo, Directorio_base FROM empresa WHERE (Clave = " + cb_ClaveEmp.Text + ")";
            da.SelectCommand = com;
            ds = new DataSet();
            da.Fill(ds, "Emisor");
            DataRow row = ds.Tables["Emisor"].Rows[0];            
            lbl_emp.Text = row["Nombre"].ToString().Trim();
            emisorRFC = row["RFC"].ToString().Trim();
            emisorRegFiscal = row["RegFiscal"].ToString().Trim();
            emisorCP = row["sCP"].ToString().Trim();
            emisorEmail = row["Correo"].ToString().Trim();
            Rpt_Fac = @"" + row["Reporte_fac"].ToString() + "";            
            Ruta_cbb = @"" + row["Ruta_cbb"].ToString() + "";            
            RutaLogo = @"" + row["Ruta_logo"].ToString() + "";            
            RutaBMP = @"" + row["Ruta_BMP"].ToString() + "";
            RutaPDF = @"" + row["Ruta_PDF"].ToString() + "";
            Ruta_fac = @"" + row["Ruta_fact"].ToString() + "";
            RutaNoTimb = @"" + row["Ruta_NoTimb"].ToString() + "";
            BDPOS = @"" + row["Directorio_base"].ToString() + "";
            con.Close();                        
        }

        private void cb_ClaveEmp_TextChanged(object sender, EventArgs e)
        {
            if ((cb_ClaveEmp.Text == "") || (cb_ClaveEmp.Text == null) || (cb_ClaveEmp.Text == "System.Data.DataRowView"))
            {
            }
            else
            {
                imprimir_btn.Enabled = false;
                btn_search.Enabled = false;
                btn_timbrar.Enabled = false;
                btn_enviar.Enabled = false;
                Clear();
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT COUNT(Folio) AS Suma FROM movimientos WHERE (Clave_Emp = " + cb_ClaveEmp.Text + ") AND (Estatus = 'D')";
                int sumFol = Convert.ToInt32(com.ExecuteScalar());
                con.Close();
                if (sumFol == 0)
                {
                    MessageBox.Show("No cuenta con folios para realizar transacciones, contacte a su proveedor de folios. \nEvite interrumpir su generación de facturas.", "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                }
                else if (sumFol <= 20)
                {
                    MessageBox.Show("Sus Folios están por terminar, contacte a su proveedor de folios. \nEvite interrumpir su generación de facturas. \n Total Folios = " + sumFol.ToString(), "Aviso Importante", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else {
                    getEmisor();
                    getFolioDisponible();
                    btn_search.Enabled = true;                    
                }
            }
        }

        public void getFolioDisponible() 
        {
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT * FROM cfdi.movimientos Where Clave_Emp = " + cb_ClaveEmp.Text + " and Estatus = 'D' LIMIT 1";
            string folio = com.ExecuteScalar().ToString();
            tb_folio.Text = folio;
            con.Close();   
        }

        public void Clear() 
        {
            lbl_emp.Text = "";
            tb_monto.Text = "";
            tb_num_oper.Text = "";
            rfc_origen = null;
            banco = null;
            cta_ordenante = null;
            cta_beneficiaria = null;
            rfc_beneficiaria = null;            
            tb_tipoCambio.Text = "";
            tb_tipoCambio.Enabled = false;
            cb_moneda.SelectedItem = 0;            
            tb_uuid.Text = "";
            tb_serieR.Text = "";
            tb_folioR.Text = "";
            tb_monedaR.Text = "";
            tb_tipoCambioR.Text = "";
            tb_monedaR.Text = "";
            tb_num_parcialidad.Text = "";
            tb_saldo_anterior.Text = "";
            tb_imp_pagado.Text = "";
            tb_saldo_insoluto.Text = "";
        }

        private void btn_search_Click(object sender, EventArgs e)
        {
            XMLDialog.Title = "Abrir Archivo XML";
            XMLDialog.Filter = "XML Files|*.xml";
            XMLDialog.InitialDirectory = @"C:\";

            if (XMLDialog.ShowDialog() == DialogResult.OK)
            {
                is_search = true;
                sTargetFolder = XMLDialog.SafeFileName.ToString();
                this.tb_xml.Text = sTargetFolder;
                path = Path.GetDirectoryName(XMLDialog.FileName);
            }
            
            RutaXML = path + "\\";
            string PDFArchivoXML_Timbrado = tb_xml.Text;
            string EncaCFD = "Factura";
            dsCFDI = GetCFDTimbrado(RutaXML + PDFArchivoXML_Timbrado, EncaCFD);

            fillFields(dsCFDI);
            btn_timbrar.Enabled = true;
        }

        public void fillFields(DataSet _cfdi) 
        {
            tb_uuid.Text = _cfdi.Tables["FacturaG"].Rows[0]["UUID"].ToString().Trim();
            tb_serieR.Text = _cfdi.Tables["FacturaG"].Rows[0]["Serie"].ToString().Trim();
            tb_folioR.Text = _cfdi.Tables["FacturaG"].Rows[0]["Folio"].ToString().Trim();
            string moneda = _cfdi.Tables["FacturaG"].Rows[0]["Moneda"].ToString().Trim();
            tb_monedaR.Text = moneda;
            if(moneda != "MXN")
                tb_tipoCambioR.Text = _cfdi.Tables["FacturaG"].Rows[0]["TipoCambio"].ToString().Trim();
            metodoPago_DR = _cfdi.Tables["FacturaG"].Rows[0]["MetodoPago"].ToString().Trim();
            tb_metodoPagoR.Text = (metodoPago_DR == "PPD") ? "Pago en parcialidades o diferido" : "Pago en una sola exhibición";
            tb_num_parcialidad.Text = "1";
            double _saldoAnterior = Convert.ToDouble(_cfdi.Tables["FacturaG"].Rows[0]["Total"].ToString());
            tb_saldo_anterior.Text = _saldoAnterior.ToString();
            double _impPagado = Convert.ToDouble(_cfdi.Tables["FacturaG"].Rows[0]["Total"].ToString());
            tb_imp_pagado.Text = _impPagado.ToString();
            tb_saldo_insoluto.Text = "0.00";

            //double _montoR = Convert.ToDouble(_cfdi.Tables["FacturaG"].Rows[0]["Total"].ToString());
            //tb_monto.Text = _montoR.ToString();

            receptorNombre = _cfdi.Tables["FacturaG"].Rows[0]["rnombre"].ToString().Trim();
            receptorRFC = _cfdi.Tables["FacturaG"].Rows[0]["rrfc"].ToString().Trim();
            receptorUsoCFDI = "P01";
        }

        public DataSet GetCFDTimbrado(string ArchivoXML, string EncaCFDI)
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
            dtFacturaG.Columns.Add("anoAprobacion");
            dtFacturaG.Columns.Add("noAprobacion");
            dtFacturaG.Columns.Add("FormaPago");
            dtFacturaG.Columns.Add("CondicionesDePago");
            dtFacturaG.Columns.Add("SubTotal");
            dtFacturaG.Columns.Add("Descuento");
            dtFacturaG.Columns.Add("TipoCambio");//10
            dtFacturaG.Columns.Add("Moneda");
            dtFacturaG.Columns.Add("Total");
            dtFacturaG.Columns.Add("TipoDeComprobante");
            dtFacturaG.Columns.Add("MetodoPago");
            dtFacturaG.Columns.Add("LugarExpedicion");//CFDI v3.3            
            dtFacturaG.Columns.Add("Sello");
            dtFacturaG.Columns.Add("NoCertificado");
            dtFacturaG.Columns.Add("Certificado");
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
            dtFacturaD.Columns.Add("sTelefono");
            dtFacturaD.Columns.Add("cTelefono");
            dtFacturaD.Columns.Add("DiasCredito");
            dtFacturaD.Columns.Add("NumeroPredial");

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

            DataTable dtFacturaPagos = new DataTable("FacturaPagos");
            dtFacturaPagos.Columns.Add("serie");
            dtFacturaPagos.Columns.Add("folio");
            dtFacturaPagos.Columns.Add("FechaDePago");
            dtFacturaPagos.Columns.Add("MonedaP");
            dtFacturaPagos.Columns.Add("FormaDePagoP");
            dtFacturaPagos.Columns.Add("TipoCambioP");
            dtFacturaPagos.Columns.Add("Monto");
            dtFacturaPagos.Columns.Add("NumOperacion");

            DataTable dtDocRelacionados = new DataTable("DocRelacionados");
            dtDocRelacionados.Columns.Add("idDocumento");
            dtDocRelacionados.Columns.Add("serieDR");
            dtDocRelacionados.Columns.Add("folioDR");
            dtDocRelacionados.Columns.Add("MonedaDR");
            dtDocRelacionados.Columns.Add("TipoCambioDR");
            dtDocRelacionados.Columns.Add("MetodoDePagoDR");
            dtDocRelacionados.Columns.Add("NumParcialidad");
            dtDocRelacionados.Columns.Add("SaldoAnt");
            dtDocRelacionados.Columns.Add("ImpPagado");
            dtDocRelacionados.Columns.Add("SaldoInsoluto");
            #endregion

            #region Abrimos el archivo XML
            DataRow drFacturaG = dtFacturaG.NewRow();
            drFacturaG["Serie"] = " ";
            drFacturaG["Descuento"] = "0.00";
            drFacturaG["EncaCFD"] = EncaCFDI;
            drFacturaG["EncaTitulo1"] = " ";
            drFacturaG["EncaTitulo2"] = " ";
            drFacturaG["EncaTitulo3"] = " ";
            drFacturaG["Nota1"] = " ";
            drFacturaG["Nota2"] = " ";
            drFacturaG["Nota3"] = " ";
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
            int TotalNodosPagos = 0;            
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
                CargaDato_drFacturaG(ref drFacturaG, sNodoName, sAtributeName, sAtributeValues);
            }
            #endregion
            int _item = 0;           
            #region Lee Atributos del nodo EMISOR
            XmlNodeList nEmisor = nComprobante.Item(1).ChildNodes;

            TotalAtributos = nEmisor.Item(_item).Attributes.Count;
            sNodoName = nEmisor.Item(_item).Name;
            for (int j = 0; j < TotalAtributos; j++)
            {
                sAtributeName = nEmisor.Item(_item).Attributes.Item(j).Name.ToString();
                sAtributeValues = nEmisor.Item(_item).Attributes.Item(j).Value.ToString();
                CargaDato_drFacturaG(ref drFacturaG, sNodoName, sAtributeName, sAtributeValues);
            }
            #endregion
            _item++;
            
            #region Lee nodo RECEPTOR
            XmlNodeList nReceptor = nComprobante.Item(1).ChildNodes;

            TotalAtributos = nReceptor.Item(_item).Attributes.Count;
            sNodoName = nReceptor.Item(_item).Name;
            for (int j = 0; j < TotalAtributos; j++)
            {
                sAtributeName = nReceptor.Item(_item).Attributes.Item(j).Name.ToString();
                sAtributeValues = nReceptor.Item(_item).Attributes.Item(j).Value.ToString();
                CargaDato_drFacturaG(ref drFacturaG, sNodoName, sAtributeName, sAtributeValues);
            }

            #endregion
            _item++;
            
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
                
                #region Buscando el contribuyente
                con.Open();
                com.Connection = con;

                com.CommandText = "Select Contribuyente From cfdi.empresa where (Clave = " + cb_ClaveEmp.Text + ")";
                drFacturaD["Contribuyente"] = com.ExecuteScalar().ToString();

                com.CommandText = "Select TM From cfdi.empresa where (Clave = " + cb_ClaveEmp.Text + ")";
                TM = com.ExecuteScalar().ToString();
                                
                com.Dispose();
                con.Close();
                #endregion
                
                for (int j = 0; j < TotalAtributos; j++)
                {
                    sAtributeName = nConcepto.Item(i).Attributes.Item(j).Name;
                    sAtributeValues = nConcepto.Item(i).Attributes.Item(j).Value;
                    CargaDato_drFacturaD(ref drFacturaD, sNodoName, sAtributeName, sAtributeValues);
                }
                
                dtFacturaD.Rows.Add(drFacturaD);                
            }
            _item++;
            dsFactura.Tables.Add(dtFacturaD);
            #endregion
            if(is_search)            
                _item++;
            #region Lee nodo Complemento timbrado

            XmlNodeList nComplementos = nComprobante.Item(1).ChildNodes;
            XmlNodeList nComplemento = nComplementos.Item(_item).ChildNodes;
            TotalNodos = nComplemento.Count;

            for (int i = 0; i < TotalNodos; i++)
            {
                TotalAtributos = nComplemento.Item(i).Attributes.Count;
                sNodoName = nComplemento.Item(i).Name;
                if (sNodoName == "pago10:Pagos")
                {
                    XmlNodeList nPagos = nComplemento.Item(0).ChildNodes;
                    TotalNodosPagos = nPagos.Count;

                    DataRow drFacturaPagos = dtFacturaPagos.NewRow();
                    drFacturaPagos["serie"] = drFacturaG["Serie"];
                    drFacturaPagos["folio"] = drFacturaG["Folio"];

                    for (int pas = 0; pas < TotalNodosPagos; pas++) 
                    {
                        int TotalAtributosPagos = nPagos.Item(pas).Attributes.Count;
                        string NodoNamePagos = nPagos.Item(pas).Name;

                        for (int p = 0; p < TotalAtributosPagos; p ++)
                        {
                            sAtributeName = nPagos.Item(pas).Attributes.Item(p).Name;
                            sAtributeValues = nPagos.Item(pas).Attributes.Item(p).Value;
                            CargarDato_drFacturaPago(ref drFacturaPagos, NodoNamePagos, sAtributeName, sAtributeValues);
                        }
                    }

                    XmlNodeList nDoctosRelacionados = nPagos.Item(0).ChildNodes;
                    int TotalNodosDR = nDoctosRelacionados.Count;
                    for (int dr = 0; dr < TotalNodosDR; dr++)
                    {
                        DataRow drDocRelacionados = dtDocRelacionados.NewRow();
                        TotalAtributos = nDoctosRelacionados.Item(0).Attributes.Count;
                        sNodoName = nDoctosRelacionados.Item(0).Name;

                        for (int j = 0; j < TotalAtributos; j++)
                        {
                            sAtributeName = nDoctosRelacionados.Item(dr).Attributes.Item(j).Name;
                            sAtributeValues = nDoctosRelacionados.Item(dr).Attributes.Item(j).Value;
                            CargarDato_drFacturaPago(ref drDocRelacionados, sNodoName, sAtributeName, sAtributeValues);
                        }
                        dtDocRelacionados.Rows.Add(drDocRelacionados);
                    }

                    dtFacturaPagos.Rows.Add(drFacturaPagos);
                }                
                else
                {
                    for (int j = 0; j < TotalAtributos; j++)
                    {
                        sAtributeName = nComplemento.Item(i).Attributes.Item(j).Name;
                        sAtributeValues = nComplemento.Item(i).Attributes.Item(j).Value;
                        CargaDato_drFacturaG(ref drFacturaG, sNodoName, sAtributeName, sAtributeValues);
                    }
                }
            }

            #endregion

            dtFacturaG.Rows.Add(drFacturaG);
            dsFactura.Tables.Add(dtFacturaG);
            dsFactura.Tables.Add(dtFacturaPagos);
            dsFactura.Tables.Add(dtDocRelacionados);

            return dsFactura;
        }

        public void CargaDato_drFacturaG(ref DataRow drFacturaG, string Nodo, string Atributo, string Valor)
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

        public void CargarDato_drFacturaPago(ref DataRow drFacturaPago, string Nodo, string Atributo, string Valor) 
        { 
            switch(Nodo)
            {
                case "pago10:Pago":
                    switch (Atributo) 
                    { 
                        case "FechaPago":
                            drFacturaPago["FechaDePago"] = Valor;
                            break;
                        case "FormaDePagoP":
                            drFacturaPago["FormaDePagoP"] = getFormaPago(Valor);
                            break;
                        case "MonedaP":
                            drFacturaPago["MonedaP"] = Valor;
                            break;
                        case "TipoCambioP":
                            drFacturaPago["TipoCambioP"] = Valor;
                            break;
                        case "Monto":
                            drFacturaPago["Monto"] = Valor;
                            break;
                        case "NumOperacion":
                            drFacturaPago["NumOperacion"] = Valor;
                            break;
                    }
                    break;
                case"pago10:DoctoRelacionado":
                    switch (Atributo) 
                    {
                        case "IdDocumento":
                            drFacturaPago["idDocumento"] = Valor;
                            break;
                        case "Serie":
                            drFacturaPago["serieDR"] = Valor;
                            break;
                        case "Folio":
                            drFacturaPago["folioDR"] = Valor;
                            break;
                        case "MonedaDR":
                            drFacturaPago["MonedaDR"] = Valor;
                            break;
                        case "TipoCambioDR":
                            drFacturaPago["TipoCambioDR"] = Valor;
                            break;
                        case "MetodoDePagoDR":
                            drFacturaPago["MetodoDePagoDR"] = Valor;
                            break;
                        case "NumParcialidad":
                            drFacturaPago["NumParcialidad"] = Valor;
                            break;
                        case "ImpSaldoAnt":
                            drFacturaPago["SaldoAnt"] = Valor;
                            break;
                        case "ImpPagado":
                            drFacturaPago["ImpPagado"] = Valor;
                            break;
                        case "ImpSaldoInsoluto":
                            drFacturaPago["SaldoInsoluto"] = Valor;
                            break;
                    }
                    break;
            }        
        }

        public string getRegimenFiscal(string id)
        {
            if(con.State.ToString() != "Open")
                con.Open();
            com.Connection = con;
            com.CommandText = "SELECT Descripcion FROM cfdi.c_regimenfiscal WHERE id_regimenfiscal = " + id + ";";
            string regimen = com.ExecuteScalar().ToString();
            con.Close();
            return id + " - " + regimen;
        }

        public string getUsoCFDI(string id)
        {
            if (con.State.ToString() != "Open")
                con.Open();
            com.Connection = con;
            com.CommandText = "SELECT descripcion FROM cfdi.c_usocfdi where id_usocfdi ='" + id + "';";
            string usocfdi = com.ExecuteScalar().ToString();
            con.Close();
            return id + " - " + usocfdi;
        }

        public string getFormaPago(string id)
        {
            if (con.State.ToString() != "Open")
                con.Open();
            com.Connection = con;
            com.CommandText = "SELECT Descripcion FROM cfdi.c_formapago where id_formapago ='" + id + "';";
            string formaPago = com.ExecuteScalar().ToString();
            con.Close();
            return id + " - " + formaPago;
        }

        private void btn_timbrar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                Fechaxml = DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss");
                string folio = tb_folio.Text;
                string SelloDigital = "";
                string noCertificado = "";
                string Certificado = "";
                string ErrorCod, ErrorMsj;
                string Origen = "FACTURA";
                FormaPago = cb_formapago.SelectedValue.ToString();
                fechaPago = dtp_fechaPago.Value.ToString("yyyy-MM-dd'T'HH:mm:ss");                
                noCertificado = get_noCertificacdo();
                strOriginal = CadenaOriginal(Origen, Fechaxml, folio, noCertificado, FormaPago, fechaPago);
                Sello(strOriginal, out SelloDigital, out Certificado, out ErrorCod, out ErrorMsj);

                #region Genera el XML
                               
                int i = 0;
                String PU = "";
                string RFC = emisorRFC;
                string Serie = tb_serie.Text;
                string _Folio = "0000000000" + tb_folio.Text.Trim();
                _Folio = _Folio.Substring(_Folio.Length - 10, 10);

                string RutaXML = RutaNoTimb;
                RutaXML_Timbrado = Ruta_fac;
                ArchivoXML = RFC + "_" + Serie + "_" + _Folio + ".XML";
                ArchivoXML_Timbrado = RFC + "_" + Serie + "_" + _Folio + "_Timbrado.XML";
                string ArchivoXML_ConError = RFC + "_" + Serie + "_" + _Folio + "_Error.XML";

                #region Se define el XML de Tipo de Factura CFDI 3.3
                XmlDeclaration CCDeclaracionXML;
                XmlNode nCFDI;
                XmlNode nEmisor;
                XmlNode nReceptor;
                XmlNode nConceptos;
                XmlNode nConcepto;
                XmlNode nComplemento;               
                XmlNode pPagos;
                XmlNode pPago;
                XmlNode pDoctoRelacionado;

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
                valor1 += " ";
                valor1 += "http://www.sat.gob.mx/Pagos http://www.sat.gob.mx/sitio_internet/cfd/Pagos/Pagos10.xsd";
                
                xsi = CCXml.CreateAttribute(atributo1, Name);
                xsi.Value = valor1;
                nCFDI.Attributes.Append(xsi);

                #region Atributos al nodo cfdi:Comprobante
                AgregaAtributo("S", "C", ref CCXml, ref nCFDI, "xmlns:cfdi", "http://www.sat.gob.mx/cfd/3", "");
                AgregaAtributo("S", "C", ref CCXml, ref nCFDI, "xmlns:xsi", "http://www.w3.org/2001/XMLSchema-instance", "");
                AgregaAtributo("S", "C", ref CCXml, ref nCFDI, "xmlns:pago10", "http://www.sat.gob.mx/Pagos", "");
                AgregaAtributo("S", "N", ref CCXml, ref nCFDI, "Version", "3.3", "");
                AgregaAtributo("N", "C", ref CCXml, ref nCFDI, "Serie", tb_serie.Text, "");
                AgregaAtributo("N", "N", ref CCXml, ref nCFDI, "Folio", folio, "");
                AgregaAtributo("S", "D", ref CCXml, ref nCFDI, "Fecha", Fechaxml, "");                
                AgregaAtributo("S", "N", ref CCXml, ref nCFDI, "NoCertificado", noCertificado, "");
                AgregaAtributo("S", "C", ref CCXml, ref nCFDI, "SubTotal", "0", "");
                AgregaAtributo("N", "C", ref CCXml, ref nCFDI, "Moneda", "XXX", "");
                AgregaAtributo("S", "C", ref CCXml, ref nCFDI, "Total", "0", "");
                AgregaAtributo("S", "C", ref CCXml, ref nCFDI, "TipoDeComprobante", "P", "");                
                AgregaAtributo("N", "C", ref CCXml, ref nCFDI, "LugarExpedicion", emisorCP, "");
                AgregaAtributo("S", "C", ref CCXml, ref nCFDI, "Sello", SelloDigital, "");
                AgregaAtributo("S", "C", ref CCXml, ref nCFDI, "Certificado", Certificado, "");
                #endregion

                #region Se agrega el nodo EMISOR al nodo nCFDI
                nEmisor = CCXml.CreateNode(XmlNodeType.Element, "Emisor", NameSpace);
                nEmisor.Prefix = Prefijo;

                AgregaAtributo("S", "C", ref CCXml, ref nEmisor, "Rfc", emisorRFC, "");
                AgregaAtributo("S", "C", ref CCXml, ref nEmisor, "Nombre", lbl_emp.Text, "");
                AgregaAtributo("S", "C", ref CCXml, ref nEmisor, "RegimenFiscal", emisorRegFiscal, "");

                nCFDI.AppendChild(nEmisor);
                #endregion

                #region Se agrega el nodo RECEPTOR al nodo nCFDI
                nReceptor = CCXml.CreateNode(XmlNodeType.Element, "Receptor", NameSpace);
                nReceptor.Prefix = Prefijo;

                AgregaAtributo("S", "C", ref CCXml, ref nReceptor, "Rfc", receptorRFC, "");
                AgregaAtributo("N", "C", ref CCXml, ref nReceptor, "Nombre", receptorNombre, "");
                AgregaAtributo("S", "C", ref CCXml, ref nReceptor, "UsoCFDI", receptorUsoCFDI, "");

                nCFDI.AppendChild(nReceptor);
                #endregion

                #region Se agrega el nodo CONCEPTOS al nodo nCFDI
                nConceptos = CCXml.CreateNode(XmlNodeType.Element, "Conceptos", NameSpace);
                nConceptos.Prefix = Prefijo;

                //Se agrega el nodo CONCEPTO al nodo CONCEPTOS
                nConcepto = CCXml.CreateNode(XmlNodeType.Element, "Concepto", NameSpace);
                nConcepto.Prefix = Prefijo;
                
                AgregaAtributo("S", "C", ref CCXml, ref nConcepto, "ClaveProdServ", "84111506", "");                
                AgregaAtributo("S", "N", ref CCXml, ref nConcepto, "Cantidad", "1", "");
                AgregaAtributo("S", "C", ref CCXml, ref nConcepto, "ClaveUnidad", "ACT", "");
                AgregaAtributo("S", "C", ref CCXml, ref nConcepto, "Descripcion", "Pago", "");
                AgregaAtributo("S", "C", ref CCXml, ref nConcepto, "ValorUnitario", "0", "");
                AgregaAtributo("S", "C", ref CCXml, ref nConcepto, "Importe", "0", "");

                nConceptos.AppendChild(nConcepto);
                
                nCFDI.AppendChild(nConceptos);
                #endregion
                
                #region Se agrega el nodo Complemento para Complemento Pago10
                string pagosNameSpace = "http://www.sat.gob.mx/Pagos";
                string pagoPrefijo = "pago10";

                nComplemento = CCXml.CreateNode(XmlNodeType.Element, "Complemento", NameSpace);
                nComplemento.Prefix = Prefijo;

                //Se agrege el nodo de PAGO10:Pagos
                pPagos = CCXml.CreateNode(XmlNodeType.Element, "Pagos", pagosNameSpace);
                pPagos.Prefix = pagoPrefijo;

                AgregaAtributo("S", "C", ref CCXml, ref pPagos, "Version", "1.0", "");

                //Se agrege el nodo de PAGO10:Pago
                pPago = CCXml.CreateNode(XmlNodeType.Element, "Pago", pagosNameSpace);
                pPago.Prefix = pagoPrefijo;
                AgregaAtributo("S", "D", ref CCXml, ref pPago, "FechaPago", fechaPago, "");
                AgregaAtributo("S", "C", ref CCXml, ref pPago, "FormaDePagoP", FormaPago, "");
                AgregaAtributo("N", "C", ref CCXml, ref pPago, "MonedaP", cb_moneda.SelectedItem.ToString(), "");
                if (cb_moneda.SelectedItem.ToString() != "MXN")
                    AgregaAtributo("N", "$", ref CCXml, ref pPago, "TipoCambioP", tb_tipoCambio.Text, "");
                AgregaAtributo("S", "$", ref CCXml, ref pPago, "Monto", tb_monto.Text, ""); 
                if (tb_num_oper.Text != null && tb_num_oper.Text != "")
                    AgregaAtributo("N", "N", ref CCXml, ref pPago, "NumOperacion",tb_num_oper.Text, "");
                if (rfc_origen != null && rfc_origen != "")
                    AgregaAtributo("S", "C", ref CCXml, ref pPago, "RfcEmisorCtaOrd", rfc_origen, "");
                if (banco != null && banco != "")
                    AgregaAtributo("S", "C", ref CCXml, ref pPago, "NomBancoOrdExt", banco, "");
                if (cta_ordenante != null && cta_ordenante != "")
                    AgregaAtributo("S", "C", ref CCXml, ref pPago, "CtaOrdenante", cta_ordenante, "");
                if (rfc_beneficiaria != null && rfc_beneficiaria != "")
                    AgregaAtributo("S", "C", ref CCXml, ref pPago, "RfcEmisorCtaBen", rfc_beneficiaria, "");
                if (cta_beneficiaria != null && cta_beneficiaria != "")
                    AgregaAtributo("S", "C", ref CCXml, ref pPago, "CtaBeneficiario", cta_beneficiaria, "");
                
                //TODO: PENDIENE AGREGAR SI LA FORMA DE PAGO ES TRASNFERENCIA TIPO_CADENA, CERT_PAGO, CAD_PAGO, SELLO_PAGO
                
                //Se agrega el nodo Pago10:DoctoRelacionado
                for (int p = 0; p <= _pagos.Rows.Count - 1; p++)
                {
                    pDoctoRelacionado = CCXml.CreateNode(XmlNodeType.Element, "DoctoRelacionado", pagosNameSpace);
                    pDoctoRelacionado.Prefix = pagoPrefijo;

                    AgregaAtributo("S", "C", ref CCXml, ref pDoctoRelacionado, "IdDocumento", _pagos.Rows[p][0].ToString(), "");

                    if (_pagos.Rows[p][1].ToString() != null && _pagos.Rows[p][1].ToString() != "")
                        AgregaAtributo("N", "C", ref CCXml, ref pDoctoRelacionado, "Serie", _pagos.Rows[p][1].ToString(), "");
                    
                    AgregaAtributo("N", "N", ref CCXml, ref pDoctoRelacionado, "Folio", _pagos.Rows[p][2].ToString(), "");
                    AgregaAtributo("N", "C", ref CCXml, ref pDoctoRelacionado, "MonedaDR", _pagos.Rows[p][3].ToString(), "");

                    if (_pagos.Rows[p][3].ToString() != "MXN")
                        AgregaAtributo("N", "$", ref CCXml, ref pDoctoRelacionado, "TipoCambioDR", _pagos.Rows[p][4].ToString(), "");
                    
                    AgregaAtributo("N", "C", ref CCXml, ref pDoctoRelacionado, "MetodoDePagoDR", "PPD", "");//_pagos.Rows[p][5].ToString()

                    if (_pagos.Rows[p][6].ToString() != null && _pagos.Rows[p][6].ToString() != "")
                        AgregaAtributo("N", "N", ref CCXml, ref pDoctoRelacionado, "NumParcialidad", _pagos.Rows[p][6].ToString(), "");
                    
                    if (_pagos.Rows[p][7].ToString() != null && _pagos.Rows[p][7].ToString() != "")
                        AgregaAtributo("S", "$", ref CCXml, ref pDoctoRelacionado, "ImpSaldoAnt", _pagos.Rows[p][7].ToString(), "");

                    if (_pagos.Rows[p][8].ToString() != null && _pagos.Rows[p][8].ToString() != "")
                        AgregaAtributo("S", "$", ref CCXml, ref pDoctoRelacionado, "ImpPagado", _pagos.Rows[p][8].ToString(), "");

                    if (_pagos.Rows[p][9].ToString() != null && _pagos.Rows[p][9].ToString() != "")
                        AgregaAtributo("S", "$", ref CCXml, ref pDoctoRelacionado, "ImpSaldoInsoluto", _pagos.Rows[p][9].ToString(), "");


                    pPago.AppendChild(pDoctoRelacionado);
                }
                pPagos.AppendChild(pPago);
                nComplemento.AppendChild(pPagos);
                
                nCFDI.AppendChild(nComplemento);
                
                #endregion

                //Se le Agrega al nodo cfdi  al XML 
                CCXml.AppendChild(nCFDI);
                #endregion

                CCXml.Save(RutaXML + ArchivoXML);
                #endregion XML

                #region  Se manda a TIMBRAR el Archivo XML
                
                try
                {
                    con.Open();
                    com.CommandText = "SELECT Cuenta_Timbrado, Usuario_Timbrado, Token_Timbrado, Pass_Timbrado FROM empresa WHERE (Clave = " + this.cb_ClaveEmp.Text + ")";
                    MySqlDataAdapter tda = new MySqlDataAdapter();
                    DataSet tds = new DataSet();
                    tda.SelectCommand = com;
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
                            mx.com.tusfacturas.www.TimbradorIntegradores oTimbrar = new mx.com.tusfacturas.www.TimbradorIntegradores();
                            XML_CFD_Timbrado = oTimbrar.get(XML_CFD, token, user, password, cuenta);
                        }

                        StreamWriter streamWriter = new StreamWriter(RutaXML_Timbrado + ArchivoXML_Timbrado);
                        streamWriter.Write(XML_CFD_Timbrado);
                        streamWriter.Close();

                        if (XML_CFD_Timbrado.Contains("Error"))
                        {
                            throw new IOException("Vea el archivo " + RutaXML_Timbrado + ArchivoXML_Timbrado);
                        }
                        
                        is_search = false;
                        GuardarDatos(strOriginal, RutaXML_Timbrado, ArchivoXML_Timbrado, Serie);

                        btn_enviar.Enabled = true;
                        imprimir_btn.Enabled = true;
                        MessageBox.Show("Se Ha creado correctamene CFDI", "Finalizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btn_timbrar.Enabled = false;
                        
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
                    com.Dispose();
                    con.Close();
                }

                #endregion

                #endregion
            }
            else
            {
                MessageBox.Show("Verifique los campos obligatorios marcados con un asterisco.\n", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public bool validar() 
        {
            bool valid = false;
            if ((cb_formapago.Text != "") && (tb_monto.Text != "") && (cb_moneda.Text != ""))
            {
                double _monto = Convert.ToDouble(tb_monto.Text);
                if (_monto <= 0)
                    valid = false;
                else
                    valid = true;
            }
            return valid;
        }
        
        private void GuardarDatos(string strOriginal, string RutaXML_Timbrado, string ArchivoXML_Timbrado, string Serie)
        {
            ds_CFDI_timbrado = new DataSet();
            ds_CFDI_timbrado = GetCFDTimbrado(RutaXML_Timbrado + ArchivoXML_Timbrado, strOriginal);
            string noCer = ds_CFDI_timbrado.Tables["FacturaG"].Rows[0]["NoCertificado"].ToString().Trim();
            string Total = ds_CFDI_timbrado.Tables["FacturaG"].Rows[0]["Total"].ToString().Trim();
            string SelloSAT = ds_CFDI_timbrado.Tables["FacturaG"].Rows[0]["SelloSAT"].ToString().Trim();
            string noCSD = ds_CFDI_timbrado.Tables["FacturaG"].Rows[0]["NoCertificadoSAT"].ToString().Trim();
            UUID_SAT = ds_CFDI_timbrado.Tables["FacturaG"].Rows[0]["UUID"].ToString().Trim();
            string SelloCFD = ds_CFDI_timbrado.Tables["FacturaG"].Rows[0]["Sello"].ToString().Trim();
            string fecha = ds_CFDI_timbrado.Tables["FacturaG"].Rows[0]["Fecha"].ToString().Trim();
            string rrfc = ds_CFDI_timbrado.Tables["FacturaG"].Rows[0]["rrfc"].ToString().Trim();
            fecha = fecha.Replace("T", " ");
            rrfc = rrfc.Replace("-", "");            

            MySqlDataAdapter da = new MySqlDataAdapter();
            DataSet dst1 = new DataSet();
            MySqlCommand com3 = new MySqlCommand();

            try
            {
                com.Connection = con;
                if (con.State.ToString() == "Closed")
                    con.Open();

                com.CommandText = "UPDATE movimientos SET No_Certificado ='" + noCer + "', CadenaOriginal ='" + strOriginal + "', SelloCFD ='" + SelloCFD + "', SelloSAT ='" +
                                    SelloSAT + "', Estatus ='P', Factura =" + tb_folio.Text + ", Folio_SAT ='" + UUID_SAT + "', NoCSD_SAT ='" + noCSD + "', Operacion =" +
                                    Total + ", MetodoPago ='" + FormaPago + "', Fecha = '" + fecha + "', RFC = '" + rrfc + "'" +
                                    " WHERE (Folio = " + tb_folio.Text + ")";

                com.ExecuteNonQuery();                
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se han guarado los datos correctamente, Verifique su información.\n" + ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                com.Dispose();
                con.Close();
            }
        }

        private void btn_enviar_Click(object sender, EventArgs e)
        {
            Correo send = new Correo(RutaXML, PDFArchivoXML_Timbrado, RutaPDF, ArchivoPDF_CFDI, cb_ClaveEmp.Text, "");
            send.Show();
        }
            
        #region METODOS CREACIÓN SELLO - CADENA ORIGINAL
        public String CadenaOriginal(string Origen, string Fechaxml, string folio, string noCertificado, string FormaPago, string _fechaPago) 
        {           
            string CO = "||";
            //    1. Informacion general                
            AgregaDatoCO(ref CO, "S", "N", "Version", "3.3", "0");
            AgregaDatoCO(ref CO, "N", "C", "Serie", tb_serie.Text, "0");
            AgregaDatoCO(ref CO, "N", "C", "Folio", folio,"0");
            AgregaDatoCO(ref CO, "S", "D", "Fecha", Fechaxml, "0");
            AgregaDatoCO(ref CO, "S", "N", "NoCertificado", noCertificado, "0");
            AgregaDatoCO(ref CO, "S", "C", "SubTotal", "0", "0");   
            AgregaDatoCO(ref CO, "N", "C", "Moneda", "XXX", "0");
            AgregaDatoCO(ref CO, "S", "C", "Total", "0", "0");
            AgregaDatoCO(ref CO, "S", "C", "TipoDeComprobante", "P", "0");
            AgregaDatoCO(ref CO, "S", "C", "LugarExpedicion", emisorCP, "0");
                
            //    2.Información del nodo Emisor
            AgregaDatoCO(ref CO, "S", "C", "Rfc", emisorRFC, "0");
            AgregaDatoCO(ref CO, "S", "C", "Nombre", lbl_emp.Text, "");
            AgregaDatoCO(ref CO, "S", "C", "RegimenFiscal", emisorRegFiscal, "0"); 
            //    5.Información del nodo Receptor
            AgregaDatoCO(ref CO, "S", "C", "Rfc", receptorRFC, "0");
            AgregaDatoCO(ref CO, "N", "C", "Nombre", receptorNombre, "0");
            AgregaDatoCO(ref CO, "S", "C", "UsoCFDI", receptorUsoCFDI, "0");
            
            //    6. Informacion del nodo Concepto
            AgregaDatoCO(ref CO, "S", "C", "ClaveProdServ", "84111506", "0");
            AgregaDatoCO(ref CO, "S", "N", "Cantidad", "1", "0");
            AgregaDatoCO(ref CO, "S", "C", "ClaveUnidad", "ACT", "0");
            AgregaDatoCO(ref CO, "S", "C", "Descripcion", "Pago", "0");
            AgregaDatoCO(ref CO, "S", "C", "ValorUnitario", "0", "0");
            AgregaDatoCO(ref CO, "S", "C", "Importe", "0", "0");
           
            //    7. Informacion de nodo Pagos
            AgregaDatoCO(ref CO, "S", "C", "Version", "1.0", "0");
            AgregaDatoCO(ref CO, "S", "D", "FechaPago", fechaPago, "0");
            AgregaDatoCO(ref CO, "S", "C", "FormaDePagoP", FormaPago, "0");
            AgregaDatoCO(ref CO, "N", "C", "MonedaP", cb_moneda.SelectedItem.ToString(), "0");
            if(cb_moneda.SelectedItem.ToString() != "MXN")
                AgregaDatoCO(ref CO, "N", "$", "TipoCambioP", tb_tipoCambio.Text, "0");
            AgregaDatoCO(ref CO, "S", "$", "Monto", tb_monto.Text, "0");
            if (tb_num_oper.Text != null && tb_num_oper.Text != "")
                AgregaDatoCO(ref CO, "N", "N", "NumOperacion", tb_num_oper.Text, "0");
            if (rfc_origen != null && rfc_origen != "")
                AgregaDatoCO(ref CO, "S", "C", "RfcEmisorCtaOrd", rfc_origen, "0");
            if (banco != null && banco != "")
                AgregaDatoCO(ref CO, "S", "C", "NomBancoOrdExt", banco, "0");
            if (cta_ordenante != null && cta_ordenante != "")
                AgregaDatoCO(ref CO, "S", "C", "CtaOrdenante", cta_ordenante, "0");
            if (rfc_beneficiaria != null && rfc_beneficiaria != "")
                AgregaDatoCO(ref CO, "S", "C", "RfcEmisorCtaBen", rfc_beneficiaria, "0");
            if (cta_beneficiaria != null && cta_beneficiaria != "")
                AgregaDatoCO(ref CO, "S", "C", "CtaBeneficiario", cta_beneficiaria, "0");

            //TODO: PENDIENE AGREGAR SI LA FORMA DE PAGO ES TRASNFERENCIA TIPO_CADENA, CERT_PAGO, CAD_PAGO, SELLO_PAGO
            for (int i = 0; i <= _pagos.Rows.Count - 1; i++)
            {
                AgregaDatoCO(ref CO, "S", "C", "IdDocumento", _pagos.Rows[i][0].ToString(), "0");
                
                if (_pagos.Rows[i][1].ToString() != null && _pagos.Rows[i][1].ToString() != "")
                    AgregaDatoCO(ref CO, "N", "C", "Serie", _pagos.Rows[i][1].ToString(), "0");
                
                AgregaDatoCO(ref CO, "N", "N", "Folio", _pagos.Rows[i][2].ToString(), "0");
                AgregaDatoCO(ref CO, "N", "C", "MonedaDR", _pagos.Rows[i][3].ToString(), "0");
                
                if (_pagos.Rows[i][3].ToString() != "MXN" && _pagos.Rows[i][3].ToString() != "")
                    AgregaDatoCO(ref CO, "N", "$", "TipoCambioDR", _pagos.Rows[i][4].ToString(), "0");

                AgregaDatoCO(ref CO, "N", "C", "MetodoDePagoDR", "PPD", "0"); //_pagos.Rows[i][5].ToString()

                if (_pagos.Rows[i][6].ToString() != null && _pagos.Rows[i][6].ToString() != "")
                    AgregaDatoCO(ref CO, "N", "N", "NumParcialidad", _pagos.Rows[i][6].ToString(), "0");

                if (_pagos.Rows[i][7].ToString() != null && _pagos.Rows[i][7].ToString() != "")
                    AgregaDatoCO(ref CO, "S", "$", "ImpSaldoAnt", _pagos.Rows[i][7].ToString(), "0");

                if (_pagos.Rows[i][8].ToString() != null && _pagos.Rows[i][8].ToString() != "")
                    AgregaDatoCO(ref CO, "S", "$", "ImpPagado", _pagos.Rows[i][8].ToString(), "0");

                if (_pagos.Rows[i][9].ToString() != null && _pagos.Rows[i][9].ToString() != "")
                    AgregaDatoCO(ref CO, "S", "$", "ImpSaldoInsoluto", _pagos.Rows[i][9].ToString(), "0");
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
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT Arch_cer, Arch_key, Contrasena, Ruta_cer FROM certificados WHERE (Clave_emp = " + this.cb_ClaveEmp.Text + ") AND (Estatus = 'A')";

                da.SelectCommand = com;
                con.Close();

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
                con.Close();
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

        private void imprimir_btn_Click(object sender, EventArgs e)
        {
            if (ds_CFDI_timbrado == null) 
            {
                ds_CFDI_timbrado = new DataSet();
                ds_CFDI_timbrado = GetCFDTimbrado(RutaXML + PDFArchivoXML_Timbrado, strOriginal);
            }

            PDFArchivoXML_Timbrado = ArchivoXML_Timbrado;
            string ArchivoBMP_CBBQR = ArchivoXML_Timbrado.Substring(0, ArchivoXML_Timbrado.Length - 13) + ".BMP";
            ArchivoPDF_CFDI = ArchivoXML_Timbrado.Substring(0, ArchivoXML_Timbrado.Length - 13) + ".PDF";
            string EncaCFD = "Factura";

            #region Generamos el Codigo de Barra Bidimensional QR en un archivo BMP

            string eRFC = ds_CFDI_timbrado.Tables["FacturaG"].Rows[0]["erfc"].ToString().Trim();
            string rRFC = ds_CFDI_timbrado.Tables["FacturaG"].Rows[0]["rrfc"].ToString().Trim();
            string Total = ds_CFDI_timbrado.Tables["FacturaG"].Rows[0]["total"].ToString().Trim();
            Total = String.Format("{0:0000000000.000000}", Convert.ToDouble(Total));
            string UUID = ds_CFDI_timbrado.Tables["FacturaG"].Rows[0]["UUID"].ToString().Trim();
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
            ds_CFDI_timbrado.Tables[1].Rows[0]["CBBQR"] = ImageToByte(Image.FromFile(archivocbb));

            FileStream fs = new FileStream(RutaLogo, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            byte[] imgbyte = new byte[fs.Length + 1];
            imgbyte = br.ReadBytes(Convert.ToInt32(fs.Length));
            ds_CFDI_timbrado.Tables[1].Rows[0]["Logotipo1"] = imgbyte;

            br.Close();
            fs.Close();

            ds_CFDI_timbrado.Tables[1].Rows[0]["EncaCFD"] = EncaCFD;

            #region Imprime y Exporta a PDF la representacion Impresa el CFD
            CrystalDecisions.CrystalReports.Engine.ReportDocument CrReport = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
                        
            CrReport.Load(Application.StartupPath.ToString() + @"\SCFRRepImpCFD-Pagos.rpt");
            
            CrReport.SetDataSource(ds_CFDI_timbrado);

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

        public static Byte[] ImageToByte(Image pImagen)
        {
            Byte[] mImage = null;

            MemoryStream ms = new MemoryStream();
            pImagen.Save(ms, pImagen.RawFormat);
            mImage = ms.GetBuffer();
            ms.Close();

            return mImage;
        }

        private void btn_fSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cb_moneda_SelectedIndexChanged(object sender, EventArgs e)
        {
            tb_tipoCambio.Enabled = cb_moneda.SelectedItem != "MXN" ? true : false;            
        }

        private void tb_monto_TextChanged(object sender, EventArgs e)
        {
            double _monto = Convert.ToDouble(tb_monto.Text);
            double _saldoA = Convert.ToDouble(tb_saldo_anterior.Text);

            double calc = _saldoA - _monto;

            tb_imp_pagado.Text = _monto.ToString();
            tb_saldo_insoluto.Text = calc.ToString("N2");
        }

        private void btn_agregar_Click(object sender, EventArgs e)
        {
            decimal t_cambio = tb_tipoCambioR.Text != "" ? Convert.ToDecimal(tb_tipoCambioR.Text) : 0;
            _pagos.Rows.Add(
                tb_uuid.Text, 
                tb_serieR.Text, 
                tb_folioR.Text, 
                tb_monedaR.Text,
                t_cambio, 
                tb_metodoPagoR.Text,
               Convert.ToInt32(tb_num_parcialidad.Text), 
               Convert.ToDecimal(tb_saldo_anterior.Text), 
               Convert.ToDecimal(tb_imp_pagado.Text),
               Convert.ToDecimal(tb_saldo_insoluto.Text)
            );
            dg_pagos.DataSource = _pagos;

            string _monto = tb_monto.Text != "" ? tb_monto.Text : "0.00";
            tb_monto.Text =  (Convert.ToDecimal(tb_imp_pagado.Text) + Convert.ToDecimal(_monto)).ToString();

            tb_xml.Text = "";                       
            tb_uuid.Text = "";
            tb_serieR.Text = "";
            tb_folioR.Text = "";
            tb_monedaR.Text = "";
            tb_tipoCambioR.Text = "";
            tb_monedaR.Text = "";
            tb_metodoPagoR.Text = "";
            tb_num_parcialidad.Text = "";
            tb_saldo_anterior.Text = "";
            tb_imp_pagado.Text = "";
            tb_saldo_insoluto.Text = "";            
        }

        private void btn_mas_Click(object sender, EventArgs e)
        {
            OpcionesPagos opciones = new OpcionesPagos();
            opciones.ShowDialog();

            rfc_origen = opciones.rfcOrigen;
            banco = opciones._banco;
            cta_ordenante = opciones.cuentaOrdenante;
            rfc_beneficiaria = opciones.rfcBeneficiaria;
            cta_beneficiaria = opciones.cuentaBeneficiaria;
            
        }

        private void tb_imp_pagado_TextChanged(object sender, EventArgs e)
        {
            if (tb_imp_pagado.Text != "")
            {
                double _abono = Convert.ToDouble(tb_imp_pagado.Text);
                double _saldoA = Convert.ToDouble(tb_saldo_anterior.Text);
                double calc = _saldoA - _abono;
                tb_saldo_insoluto.Text = calc.ToString("N2");
            }
        }     
    }
}
