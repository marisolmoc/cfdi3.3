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
    public partial class Acceso : Form
    {
        public Acceso()
        {
            InitializeComponent();
        }

        private void btn_entrar_Click(object sender, EventArgs e)
        {
            if(this.tb_usuario.Text == "SiscomFE2012" & this.tb_passw.Text == "Siscom4875")
            {
                MessageBox.Show("Gracias por adquirir tus folios ahora puede darlos de alta", "Acceso Correcto", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Hide();
                AgregarFolio agregar = new AgregarFolio();
                agregar.Show();
            }
            else
            {
                MessageBox.Show("Usuario o Contraseña incorrectos, verifique sus datos con el administrador del sistema", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Hide();
            }            
        }

        private void tb_passw_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_entrar_Click(sender, e);
            }
        }
    }
}
