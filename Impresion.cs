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
using System.Text.RegularExpressions;

namespace SiscomCFDI
{
    public partial class Impresion : Form
    {
        string servidor;
        string usuario;
        string passw;
        string RutaPDF;
        string RutaXML;
        string ArchivoPDF_CFDI;
        string Rpt_Fac;
        string TipoFac;
        string strOriginal;
        private string sTargetFolder;        
        string TelEmi;
        string TelSucu;
        string TelCli;
        string DiasCredito;
        string Estatus;
        bool RetenValue = false;
        bool INEValue = false;
        bool is_pagos = false;
        string _fileName;
        bool isFilePagos = false;
        string rfcCliente;

        OpenFileDialog XMLDialog = new OpenFileDialog();
        string path;
        MySqlConnection con;
        MySqlCommand com = new MySqlCommand();
        MySqlDataAdapter da = new MySqlDataAdapter();
        DataSet ds = new DataSet();

        OleDbConnection acon;
        OleDbCommand acom = new OleDbCommand();
        OleDbDataAdapter ada = new OleDbDataAdapter();

        
        public Impresion()
        {
            InitializeComponent();
        }

        private void btn_pdf_Click(object sender, EventArgs e)
        {            
            con.Open();
            com.Connection = con;
            com.CommandText = "SELECT Ruta_PDF, Ruta_BMP, Ruta_logo, TipoFac, Ruta_cbb, fTelefono, sTelefono, Reporte_fac FROM empresa WHERE Clave = "+cb_ClaveEmp.Text+"";
            da.SelectCommand = com;
            da.Fill(ds, "Rutas");
            con.Close();
            DataRow row = ds.Tables["Rutas"].Rows[0];

            Rpt_Fac = @"" + row["Reporte_fac"].ToString() +"";
            TipoFac = row["TipoFac"].ToString();
            string Ruta_cbb = @""+ row["Ruta_cbb"].ToString() + "";
            TelEmi = row["fTelefono"].ToString();
            TelSucu = row["sTelefono"].ToString();
            string RutaLogo = @"" + row["Ruta_logo"].ToString() + ""; 
            RutaXML = path + "\\";
            string RutaBMP = @""+row["Ruta_BMP"].ToString()+"";  
            RutaPDF = @""+row["Ruta_PDF"].ToString()+"";
            string EncaCFD = "";
            
            if (tb_xml.Text.Substring(0, 2) == "NC")
            {
                EncaCFD = "Nota de Crédito";
            }
            else
            {
                EncaCFD = "Factura";
            }
            string PDFArchivoXML_Timbrado = this.tb_xml.Text;
            string ArchivoBMP_CBBQR = tb_xml.Text.Substring(0, tb_xml.Text.Length - 13) + ".BMP"; 
            ArchivoPDF_CFDI = tb_xml.Text.Substring(0, tb_xml.Text.Length - 13) + ".PDF";
  
            
            // Obtiene los datos del CFD timbrado tomandolos del XML timbrado
            DataSet dsCFD = GetCFDTimbrado(RutaXML + PDFArchivoXML_Timbrado, EncaCFD);

            #region Generamos el Codigo de Barra Bidimensional QR en un archivo BMP            
            
            string eRFC = dsCFD.Tables["FacturaG"].Rows[0]["erfc"].ToString().Trim();
            string rRFC = dsCFD.Tables["FacturaG"].Rows[0]["rrfc"].ToString().Trim();
            rfcCliente = rRFC;
            string Total = dsCFD.Tables["FacturaG"].Rows[0]["Total"].ToString().Trim();
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

            //dsCFD.Tables[0].Rows[0]["EncaCFD"] = EncaCFD;

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
            else if (is_pagos == true) 
            {
                CrReport.Load(Application.StartupPath.ToString() + @"\SCFRRepImpCFD-Pagos.rpt");
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
                MessageBox.Show("Se a creado exitosamente la factura en formato PDF ahora puede imprimirla","Finalizar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btn_enviar.Enabled = true;

            }
            #endregion
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
            #region Lee Atributo del nodo Cfdi RELACIONADOS
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
            #endregion
            _item++;
            #region Lee Atributos del nodo DOMICILIO DEL EMISOR COMENTADO
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

            #endregion
            _item++;
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

                #region Buscando el contribuyente
                con.Open();
                com.Connection = con;

                com.CommandText = "Select Contribuyente From cfdi.empresa where (Clave = " + cb_ClaveEmp.Text + ")";
                drFacturaD["Contribuyente"] = com.ExecuteScalar().ToString();

                com.CommandText = "Select TM From cfdi.empresa where (Clave = " + cb_ClaveEmp.Text + ")";
                string TM = com.ExecuteScalar().ToString();
                int _nameLength = tb_xml.Text.Length - 13;
                _fileName = tb_xml.Text.Substring(13, _nameLength);
                isFilePagos = tb_xml.Text.Contains("_PA_") ? true : false;
                if (tb_xml.Text.Substring(0, 2) == "NC")
                {
                    com.CommandText = "Select Estatus From cfdi.movimientos WHERE (NotaCredito = " + drFacturaD["folio"] + ") AND (Clave_Emp = " + cb_ClaveEmp.Text + ")";
                }
                else if (isFilePagos) //_fileName.Substring(0, 2) == "PA") 
                {
                    com.CommandText = "Select Estatus From cfdi.movimientos WHERE (Folio = " + drFacturaD["folio"] + ") AND (Clave_Emp = " + cb_ClaveEmp.Text + ")";
                }
                else
                {
                    com.CommandText = "Select Estatus From cfdi.movimientos WHERE (Factura = " + drFacturaD["folio"] + ") AND (Clave_Emp = " + cb_ClaveEmp.Text + ")";
                }
                Estatus = com.ExecuteScalar().ToString();
                if (Estatus == "C")
                {
                    drFacturaG["Nota3"] = "FACTURA CANCELADA";
                }
                else if (Estatus == "NCC")
                {
                    drFacturaG["Nota3"] = "NOTA DE CRÉDITO CANCELADA";
                }
                com.Dispose();
                con.Close();
                #endregion

                #region Introducir Campo vendedor en factura, el telefono del cliente y Dias de Crédito
                try
                {
                    acom.Connection = acon;
                    string vendedor = "";
                    acom.CommandText = "SELECT Vendedor FROM Fac_Mov Where (Factura = " + drFacturaD["folio"] + ") AND (TipoM = '" + TM + "')";//DISTINCT 
                    acon.Open();
                    vendedor = acom.ExecuteScalar().ToString();
                    drFacturaG["Vendedor"] = vendedor;

                    acom.CommandText = "Select Telefono From Fac_Cab Where (Factura = " + drFacturaD["folio"] + ") AND (TipoM = '" + TM + "')";
                    drFacturaD["cTelefono"] = acom.ExecuteScalar().ToString();
                }
                catch (Exception ex)
                { }
                finally
                {
                    acon.Close();
                }
                #endregion

                for (int j = 0; j < TotalAtributos; j++)
                {
                    sAtributeName = nConcepto.Item(i).Attributes.Item(j).Name;
                    sAtributeValues = nConcepto.Item(i).Attributes.Item(j).Value;
                    CargaDato_drFacturaD(ref drFacturaD, sNodoName, sAtributeName, sAtributeValues);
                }
                
                //Condicion si no es Pagos
                if (!isFilePagos)
                {
                    #region Descripcion Renglones
                    DataTable dtFac = new DataTable();
                    try
                    {
                        ConxPos();
                        acon.Open();
                        acom.Connection = acon;
                        if (tb_xml.Text.Substring(0, 2) == "NC")
                        {
                            acom.CommandText = "SELECT COUNT(*) AS Expr1 FROM NC_MDes WHERE (NotaC = " + drFacturaD["folio"] + ")";
                            int num = Convert.ToInt32(acom.ExecuteScalar());
                            //acom.CommandText = "SELECT ";

                            acom.CommandText = "Select DiasCredito From NC_Cab where (NotaC = " + drFacturaD["folio"] + ")";
                            drFacturaD["DiasCredito"] = acom.ExecuteScalar().ToString();

                            if (num >= 1)
                            {
                                acom.CommandText = "SELECT Descripcion FROM Fac_MDes WHERE (Factura = " + drFacturaD["folio"] + ") AND (Codigo = '" + drFacturaD["noIdentificacion"] + "')";
                                ada.SelectCommand = acom;
                                ada.Fill(dtFac);

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
                        else
                        {
                            acom.CommandText = "SELECT COUNT(*) AS Expr1 FROM Fac_MDes WHERE (Factura = " + drFacturaD["folio"] + ")";
                            int num = Convert.ToInt32(acom.ExecuteScalar());

                            string condicion = drFacturaG["condicionesDePago"].ToString();

                            if (condicion == "Credito")
                            {
                                acom.CommandText = "Select DiasCredito From Fac_Cab where (Factura = " + drFacturaD["folio"] + ") AND (TipoM = '" + TM + "')";
                                drFacturaD["DiasCredito"] = acom.ExecuteScalar().ToString();
                            }


                            if (num >= 1)
                            {
                                acom.CommandText = "SELECT Descripcion FROM Fac_MDes WHERE (Factura = " + drFacturaD["folio"] + ") AND (Codigo = '" + drFacturaD["noIdentificacion"] + "')";
                                ada.SelectCommand = acom;
                                ada.Fill(dtFac);

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
                    }
                    catch (Exception ex)
                    {
                    }
                    finally
                    {
                        acon.Close();
                        acom.Dispose();
                    }

                    dtFac = dtFacturaD.Copy();
                    #endregion
                }
                dtFacturaD.Rows.Add(drFacturaD);
                
                //Condicion si no es Pagos
                if (!isFilePagos)
                {
                    //CFDI 3.3 Get Tipo de Impuestos del Concepto                
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
            }
            _item++;
            dsFactura.Tables.Add(dtFacturaD);
            #endregion
            
            //Condicion si no es Pagos
            if (!isFilePagos)
            {
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
                //_item++;
                
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
            }
            
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
                    RetenValue = true;

                    XmlNodeList nImpuestosLocales = nComplemento.Item(0).ChildNodes;
                    TotalNodosRetLocal = nImpuestosLocales.Count;

                    DataRow drFacturaRetLoc = dtFacturaRetLoc.NewRow();

                    for (int ii = 0; ii < TotalNodosRetLocal; ii++)
                    {
                        TotalAtributos = nImpuestosLocales.Item(ii).Attributes.Count;
                        sNodoName = nImpuestosLocales.Item(ii).Name;

                        drFacturaRetLoc["Serie"] = drFacturaG["Serie"];
                        drFacturaRetLoc["Folio"] = drFacturaG["Folio"];

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
                    INEValue = true;
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
                else if (sNodoName == "pago10:Pagos") 
                {
                    is_pagos = true;                    
                    XmlNodeList nPagos = nComplemento.Item(0).ChildNodes;
                    int TotalNodosPagos = nPagos.Count;

                    DataRow drFacturaPagos = dtFacturaPagos.NewRow();
                    drFacturaPagos["serie"] = drFacturaG["Serie"];
                    drFacturaPagos["folio"] = drFacturaG["Folio"];

                    for (int pas = 0; pas < TotalNodosPagos; pas++)
                    {
                        int TotalAtributosPagos = nPagos.Item(pas).Attributes.Count;
                        string NodoNamePagos = nPagos.Item(pas).Name;

                        for (int p = 0; p < TotalAtributosPagos; p++)
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
                        TotalAtributos = nDoctosRelacionados.Item(dr).Attributes.Count;
                        sNodoName = nDoctosRelacionados.Item(dr).Name;

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
                        CargaDato_drFacturaG(ref drFacturaG, sNodoName, sAtributeName, sAtributeValues, TipoFac);
                    }
                }
            }
            
            #endregion
            
            dtFacturaG.Rows.Add(drFacturaG);
            dsFactura.Tables.Add(dtFacturaG);
            dsFactura.Tables.Add(dtFacturaPagos);
            dsFactura.Tables.Add(dtDocRelacionados);
            

            if (RetenValue == true)
                dsFactura.Tables.Add(dtFacturaRetLoc);
            //Condicion si no es Pagos
                if (!isFilePagos)               
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
                    drFacturaRetLoc["DescImpLocRetenido" + i] = Valor;
                    break;

                case "Importe":
                    drFacturaRetLoc["ImporteLocRetenido" + i] = Valor;
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

        public void CargarDato_drFacturaPago(ref DataRow drFacturaPago, string Nodo, string Atributo, string Valor)
        {
            switch (Nodo)
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
                case "pago10:DoctoRelacionado":
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

        public static Byte[] ImageToByte(Image pImagen)
        { 
            Byte[] mImage = null;

            MemoryStream ms = new MemoryStream();
            pImagen.Save(ms, pImagen.RawFormat);
            mImage = ms.GetBuffer();
            ms.Close();

            return mImage;
        }

        private void btn_xml_Click(object sender, EventArgs e)
        {
            XMLDialog.Title = "Abrir Archivo XML";
            XMLDialog.Filter = "XML Files|*.xml";
            XMLDialog.InitialDirectory = @"C:\";

            if(XMLDialog.ShowDialog() == DialogResult.OK)
            {
                sTargetFolder = XMLDialog.SafeFileName.ToString();
                this.tb_xml.Text = sTargetFolder;
                path = Path.GetDirectoryName(XMLDialog.FileName);
            }           
        }

        private void busc_cadena()
        {
            if (tb_xml.Text.Substring(0, 2) == "NC")
            {
                string Fac = tb_xml.Text.Substring(0, tb_xml.Text.Length - 13);
                Fac = Fac.Substring(21);
                strOriginal = "";
                try
                {
                    con.Open();
                    com.Connection = con;
                    com.CommandText = "SELECT CadenaOriginal FROM cfdi.movimientos WHERE (Clave_Emp = " + cb_ClaveEmp.Text + ") AND (NotaCredito = " + Fac + ");";
                    strOriginal = com.ExecuteScalar().ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha occurido un error al extraer la Cadena Original. \n" + Convert.ToString(ex), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                    
                    
                }
                finally
                {
                    con.Close();
                    com.Dispose();
                }
            }
            else
            {
                string Fac = tb_xml.Text.Substring(0, tb_xml.Text.Length - 13);
                Fac = Fac.Substring(Fac.Length-10 );
                strOriginal = "";
                try
                {
                    con.Open();
                    com.Connection = con;
                    com.CommandText = "SELECT CadenaOriginal FROM cfdi.movimientos WHERE (Clave_Emp = " + cb_ClaveEmp.Text + ") AND (Factura = " + Fac + ");";
                    strOriginal = com.ExecuteScalar().ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha occurido un error al extraer la Cadena Original. \n" + Convert.ToString(ex), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);                                     
                }
                finally
                {
                    con.Close();
                    com.Dispose();
                }
            }
        }

        private void btn_cerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Impresion_Load(object sender, EventArgs e)
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

            ConxPos();
            
        }

        public void ConxPos()
        {
            con.Open();
            com.Connection = con;
            com.CommandText = "Select Nombre, Directorio_base FROM empresa WHERE (Clave = " + cb_ClaveEmp.Text + ")";
            da.SelectCommand = com;
            ds = new DataSet();
            da.Fill(ds, "POS");
            DataRow row = ds.Tables["POS"].Rows[0];
            string BDPOS = row["Directorio_base"].ToString().Trim();
            lbl_emp.Text = row["Nombre"].ToString().Trim();
            con.Close();

            acon = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + BDPOS + "");
        }

        private void btn_enviar_Click(object sender, EventArgs e)
        {
            Correo send = new Correo(RutaXML, tb_xml.Text, RutaPDF, ArchivoPDF_CFDI, cb_ClaveEmp.Text, getEmailCliente());
            send.Show();
        }

        private void cb_ClaveEmp_TextChanged(object sender, EventArgs e)
        {
            if ((cb_ClaveEmp.Text == "") || (cb_ClaveEmp.Text == null) || (cb_ClaveEmp.Text == "System.Data.DataRowView"))
            {
            }
            else
            {
                lbl_emp.Text = "";
                ConxPos();
            }
        }                        
    
        public string getRegimenFiscal(string id)
        {
            if (con.State.ToString() == "Closed")
                con.Open();
            com.Connection = con;
            com.CommandText = "SELECT Descripcion FROM cfdi.c_regimenfiscal WHERE id_regimenfiscal = " + id + ";";
            string regimen = com.ExecuteScalar().ToString();
            con.Close();
            return id + " - " + regimen;
        }
        
        public string getUsoCFDI(string id)
        {
            if (con.State.ToString() == "Closed")
                con.Open();
            com.Connection = con;
            com.CommandText = "SELECT descripcion FROM cfdi.c_usocfdi where id_usocfdi ='" + id +"';";
            string usocfdi = com.ExecuteScalar().ToString();
            con.Close();
            return id + " - " + usocfdi;
        }
        
        public string getFormaPago(string id)
        {
            if (con.State.ToString() == "Closed")
                con.Open();
            com.Connection = con;
            com.CommandText = "SELECT Descripcion FROM cfdi.c_formapago where id_formapago ='" + id + "';";
            string formaPago = com.ExecuteScalar().ToString();
            con.Close();
            return id + " - " + formaPago;
        }

        public string getImpuesto(string id) 
        {
            if (con.State.ToString() == "Closed")
                con.Open();
            com.Connection = con;
            com.CommandText = "SELECT Descripcion FROM cfdi.c_impuesto WHERE id_impuesto = '" + id + "';";
            string impuesto = com.ExecuteScalar().ToString();
            con.Close();
            return id + " " + impuesto;
        }
        public string getTipoRelacion(string id)
        {
            if (con.State.ToString() == "Closed")
                con.Open();
            com.Connection = con;
            com.CommandText = "SELECT Descripcion FROM cfdi.c_tiporelacion WHERE id_tiporelacion = '" + id + "';";
            string tipoRelacion = com.ExecuteScalar().ToString();
            return id + " " + tipoRelacion;
            con.Close();
        }

        public string getEmailCliente()
        {
            if(acon.State.ToString() == "Closed")
                acon.Open();

            acom.Connection = acon;
            acom.CommandText = "Select Def1, Def2, Def3 FROM Clientes WHERE RFC = '"+ rfcCliente+"';";
            ada.SelectCommand = acom;
            ada.Fill(ds, "Cliente");
            acon.Close();
            DataRow row = ds.Tables["Cliente"].Rows[0];

            string def1 = row["Def1"].ToString().Trim();
            string def2 = row["Def2"].ToString().Trim();
            string def3 = row["Def3"].ToString().Trim();

            bool isDef1Email = Regex.IsMatch(def1, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            bool isDef2Email = Regex.IsMatch(def2, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            bool isDef3Email = Regex.IsMatch(def3, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);

            string emailClient = isDef1Email ? def1 : isDef2Email ? def2 : isDef3Email ? def3 : "";

            return emailClient; 
        }
    }
}
