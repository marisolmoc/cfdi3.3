using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SiscomCFDI
{
    public partial class RetencionesLocales : Form
    {
        bool retLocales;
        DataTable RetLocalesList;

        public RetencionesLocales(DataTable dt_RetLoc)
        {
            InitializeComponent();

            if (dt_RetLoc != null)    
                RetLocalesList = dt_RetLoc;
            
        }

        private void RetencionesLocales_Load(object sender, EventArgs e)
        {
            if (RetLocalesList != null)
            {                
                dgv.Columns.Clear();
                dgv.DataSource = RetLocalesList;

                dgv.Columns[0].HeaderText = "Descripción";
                dgv.Columns[1].HeaderText = "Importe";
                dgv.Columns[2].HeaderText = "Tasa de Retención %";
                dgv.Columns[0].Width = 200;
            }
        }

        private void cancel_btn_Click(object sender, EventArgs e)
        {
            retLocales = false;
            dgv.Columns.Clear();
            dgv.DataSource = null;
            this.Close();
            
        }

        private void aceptar_btn_Click(object sender, EventArgs e)
        {
            RetLocalesList = new DataTable();
            RetLocalesList.Columns.Add("descr_retLocal", typeof(string));
            RetLocalesList.Columns.Add("importe_retLocal", typeof(decimal));
            RetLocalesList.Columns.Add("tasa_retLocal", typeof(decimal));
            
            foreach (DataGridViewRow r in dgv.Rows) 
            {
                DataRow drow = RetLocalesList.NewRow();
                
                    foreach (DataGridViewCell cell in r.Cells)
                    {
                        if (cell.Value != null)
                        {
                            drow[cell.OwningColumn.Name] = cell.Value;
                        }
                    }

                    if (!drow.IsNull(0))
                        RetLocalesList.Rows.Add(drow);                
            }

            retLocales = true;                       

            this.Close();
        }

        public DataTable dtRetLocales {
            get { return RetLocalesList; }
        }

        public bool RetLocalesValue {
            get { return retLocales; }
        }
                
        private void dgv_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            int rows = dgv.Rows.Count;
            if (rows >= 6)
            { dgv.AllowUserToAddRows = false; }
            else { dgv.AllowUserToAddRows = true; }
        }

    }
}
