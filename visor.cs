using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace SiscomCFDI
{
    public partial class visor : Form
    {
        DataTable Dtable;

        string servidor;
        string usuario;
        string passw;

        MySqlConnection con;
        MySqlCommand com = new MySqlCommand();
        MySqlDataAdapter da = new MySqlDataAdapter();
        DataSet ds = new DataSet();

        public visor(DataTable dtp)
        {
            InitializeComponent();
            Dtable = dtp;
        }

        private void visor_Load(object sender, EventArgs e)
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


            ReportDocument report = new ReportDocument();
            report.Load(Application.StartupPath.ToString() + @"\MensualRpt.rpt");
            report.SetDataSource(Dtable);
            crystalReportViewer1.ReportSource = report;
            
        }
    }
}
