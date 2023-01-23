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
    public partial class Contraseña : Form
    {
        public Contraseña()
        {
            InitializeComponent();
        }
        private string Pass = "";

        public string Resultado
        {
            get { return Pass; }
        }

        private void btn_aceptar_Click(object sender, EventArgs e)
        {
            label3.Visible = false;
            label4.Visible = false;

            if((tb_usu.Text == "") || (tb_usu.Text== null) || (tb_pass.Text == null) || (tb_usu.Text == ""))
            {
                label3.Visible = true;
                label4.Visible = true;
            }
            else if ((tb_usu.Text == "") || (tb_usu.Text == null))
            {
                label3.Visible = true;
                label4.Visible = false;
            }
            else if ((tb_pass.Text == null) || (tb_usu.Text == ""))
            {
                label3.Visible = false;
                label4.Visible = true;
            }
            else
            {
                Pass = tb_pass.Text;
                this.Close();
            }
        }
    }
}
