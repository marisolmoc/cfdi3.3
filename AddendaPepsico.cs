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
    public partial class AddendaPepsico : Form
    {
        string NumPedido;
        string NumProveedor;
        string NumRecepcion;

        public AddendaPepsico()
        {
            InitializeComponent();
        }
        public bool validacion()
        {
            bool Valid;

            if ((this.tb_NumPedido.Text == string.Empty) || (this.tb_NumProveedor.Text == string.Empty) || (this.tb_NumRecepcion.Text == string.Empty))
                Valid = false;
            else
                Valid = true;


            if (tb_NumPedido.Text == string.Empty)
                pedido_red.Visible = true;
            else
                pedido_red.Visible = false;

            if (tb_NumRecepcion.Text == string.Empty)
                recep_red.Visible = true;
            else
                recep_red.Visible = false;

            if (tb_NumProveedor.Text == string.Empty)
                prov_red.Visible = true;
            else
                prov_red.Visible = false;
                       

            return Valid;
        }
        private void btnContinueP_Click(object sender, EventArgs e)
        {
            if (validacion() == true)
            {
                NumPedido = tb_NumPedido.Text;
                NumProveedor = tb_NumProveedor.Text;
                NumRecepcion = tb_NumRecepcion.Text;

                this.Close();
            }
        }

        public string numPedido
        { 
            get { return NumPedido; } 
        }

        public string numProveedor 
        {
            get { return NumProveedor; }
        }

        public string numRecepcion 
        {
            get { return NumRecepcion; }
        }        
    }
}
