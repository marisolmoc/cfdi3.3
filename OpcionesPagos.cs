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
    public partial class OpcionesPagos : Form
    {
        string rfc_origen;
        string banco;
        string cuenta_ordenante;
        string rfc_beneficiaria;
        string cuenta_beneficiaria;
        public OpcionesPagos()
        {
            InitializeComponent();
        }

        public string rfcOrigen
        {
            get { return rfc_origen; }
        }

        public string _banco
        {
            get { return banco; }
        }

        public string rfcBeneficiaria
        {
            get { return rfc_beneficiaria; }
        }

        public string cuentaOrdenante
        {
            get { return cuenta_ordenante; }
        }

        public string cuentaBeneficiaria
        {
            get { return cuenta_beneficiaria; }
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_agregar_Click(object sender, EventArgs e)
        {
            rfc_origen = tb_rfc_orig.Text != "" ? tb_rfc_orig.Text : null;
            banco = tb_banco.Text != "" ? tb_banco.Text : null;
            cuenta_ordenante = tb_cta_ordenante.Text != "" ? tb_cta_ordenante.Text : null;
            rfc_beneficiaria = tb_rfc_benef.Text != "" ? tb_rfc_benef.Text : null;
            cuenta_beneficiaria = tb_cta_benef.Text != "" ? tb_cta_benef.Text : null;

            this.Close();
        }
    }
}
