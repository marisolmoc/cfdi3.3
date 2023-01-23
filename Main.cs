using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Microsoft.Win32;

namespace SiscomCFDI
{
    public partial class Main : Form
    {
        string servidor;
        string usuario;
        string passw;

        public Main()
        {
            InitializeComponent();
        }
  

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Impresion imp = new Impresion();
            imp.ShowDialog();
        }        

        private void cancelacionesToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Cancelaciones cancel = new Cancelaciones();
            cancel.ShowDialog();
        }

        private void certificadosToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Factura fac = new Factura();
            fac.ShowDialog();
        }

        private void Main_Load(object sender, EventArgs e)
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
                admin.ShowDialog();
            }           
            
        }

        private void conexiónBDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Admin admin = new Admin();
            admin.ShowDialog();
        }

        private void altaEmpresaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form1 emp = new Form1();
            emp.ShowDialog();
        }

        private void certificadosToolStripMenuItem_Click_2(object sender, EventArgs e)
        {
            Certificados cert = new Certificados();
            cert.ShowDialog(); 
        }

        private void foliosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Folios Folios = new Folios();
            Folios.ShowDialog();
        }

        private void notasDeCréditoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotasCred NC = new NotasCred();
            NC.ShowDialog();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        
        private void btn_pagos_Click(object sender, EventArgs e)
        {
            Pagos pagos = new Pagos();
            pagos.ShowDialog();
        }
    }
}
