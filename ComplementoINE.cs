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
    public partial class ComplementoINE : Form
    {

        string procesoType;
        string comiteType;
        string ambito;
        string entidad;
        string idConta;

        public ComplementoINE()
        {
            InitializeComponent();
        }

        private void dd_typeProceso_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel_ambito.Visible = false;
            panel_comite.Visible = false;
            if (dd_typeProceso.SelectedItem != "Ordinario")
                panel_ambito.Visible = true;
            else
                panel_comite.Visible = true;

        }

        public bool validacion() 
        {
            bool valid = true;

            if (dd_typeProceso.SelectedItem == string.Empty || dd_entidad.SelectedItem == string.Empty || tb_idConta.Text == string.Empty)
                valid = false;

            if (dd_typeProceso.SelectedItem == "Ordinario")
            {
                if (dd_typeComite.SelectedItem == string.Empty)
                    valid = false;
            }
            else {
                if (dd_typeAmbito.SelectedItem == string.Empty)
                    valid = false;
            }

            return valid;
        }

        private void btnContinueINE_Click(object sender, EventArgs e)
        {
            lbl_error.Visible = false;

            if (validacion())
            {
                procesoType = dd_typeProceso.SelectedItem.ToString();
                comiteType = dd_typeComite.SelectedItem == null ? "" : dd_typeComite.SelectedItem.ToString();
                ambito = dd_typeAmbito.SelectedItem == null ? "" : dd_typeAmbito.SelectedItem.ToString();
                entidad = dd_entidad.SelectedItem.ToString();
                idConta = tb_idConta.Text;

                this.Close();
            }
            else {
                lbl_error.Visible = true;
            }
        }

        public string tipoProceso 
        {
            get { return procesoType; }    
        }

        public string tipoComite
        { 
            get { return comiteType; } 
        }

        public string tipoAmbito
        {
            get { return ambito; }
        }

        public string cveEntidad
        {
            get { return entidad; }
        }

        public string idContabilidad
        {
            get { return idConta; }
        }

        private void ComplementoINE_Load(object sender, EventArgs e)
        {
            lbl_error.Visible = false;
        }
    }
}
