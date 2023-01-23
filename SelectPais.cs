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
    public partial class SelectPais : Form
    {
        public SelectPais()
        {
            InitializeComponent();
        }
        //public string pais;
        private string Pais = "";

        public string Resultado
        {
            get { return Pais; }
        }

        private void btn_pais_Click(object sender, EventArgs e)
        {
            Pais = tb_pais.Text;
            this.Close();
        }
    }
}
