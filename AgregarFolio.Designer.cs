namespace SiscomCFDI
{
    partial class AgregarFolio
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgregarFolio));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cb_clveEmp = new System.Windows.Forms.ComboBox();
            this.empresaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cfdiDataSet = new SiscomCFDI.cfdiDataSet();
            this.cfdiDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tb_serie = new System.Windows.Forms.TextBox();
            this.tb_folIni = new System.Windows.Forms.TextBox();
            this.tb_folTot = new System.Windows.Forms.TextBox();
            this.btn_aceptar = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.empresaTableAdapter = new SiscomCFDI.cfdiDataSetTableAdapters.empresaTableAdapter();
            this.tb_noAprob = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_anoAprob = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel_TM = new System.Windows.Forms.Panel();
            this.rb_tmNC = new System.Windows.Forms.RadioButton();
            this.rb_tmFac = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.empresaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cfdiDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cfdiDataSetBindingSource)).BeginInit();
            this.panel_TM.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Clave Empresa:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Serie:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Folio Inicial:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Total de Folios:";
            // 
            // cb_clveEmp
            // 
            this.cb_clveEmp.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.empresaBindingSource, "Clave", true));
            this.cb_clveEmp.DataSource = this.cfdiDataSetBindingSource;
            this.cb_clveEmp.FormattingEnabled = true;
            this.cb_clveEmp.Location = new System.Drawing.Point(126, 11);
            this.cb_clveEmp.Name = "cb_clveEmp";
            this.cb_clveEmp.Size = new System.Drawing.Size(121, 21);
            this.cb_clveEmp.TabIndex = 4;
            //this.cb_clveEmp.SelectionChangeCommitted += new System.EventHandler(this.cb_clveEmp_SelectionChangeCommitted);
            this.cb_clveEmp.SelectedValueChanged += new System.EventHandler(this.cb_clveEmp_SelectedValueChanged);
            // 
            // empresaBindingSource
            // 
            this.empresaBindingSource.DataMember = "empresa";
            this.empresaBindingSource.DataSource = this.cfdiDataSet;
            // 
            // cfdiDataSet
            // 
            this.cfdiDataSet.DataSetName = "cfdiDataSet";
            this.cfdiDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // cfdiDataSetBindingSource
            // 
            this.cfdiDataSetBindingSource.DataSource = this.cfdiDataSet;
            this.cfdiDataSetBindingSource.Position = 0;
            // 
            // tb_serie
            // 
            this.tb_serie.Location = new System.Drawing.Point(126, 39);
            this.tb_serie.Name = "tb_serie";
            this.tb_serie.Size = new System.Drawing.Size(41, 20);
            this.tb_serie.TabIndex = 5;
            // 
            // tb_folIni
            // 
            this.tb_folIni.Location = new System.Drawing.Point(126, 68);
            this.tb_folIni.Name = "tb_folIni";
            this.tb_folIni.ReadOnly = true;
            this.tb_folIni.Size = new System.Drawing.Size(79, 20);
            this.tb_folIni.TabIndex = 6;
            // 
            // tb_folTot
            // 
            this.tb_folTot.Location = new System.Drawing.Point(126, 102);
            this.tb_folTot.Name = "tb_folTot";
            this.tb_folTot.Size = new System.Drawing.Size(79, 20);
            this.tb_folTot.TabIndex = 7;
            // 
            // btn_aceptar
            // 
            this.btn_aceptar.Location = new System.Drawing.Point(145, 222);
            this.btn_aceptar.Name = "btn_aceptar";
            this.btn_aceptar.Size = new System.Drawing.Size(75, 23);
            this.btn_aceptar.TabIndex = 8;
            this.btn_aceptar.Text = "Aceptar";
            this.btn_aceptar.UseVisualStyleBackColor = true;
            this.btn_aceptar.Click += new System.EventHandler(this.btn_aceptar_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(235, 222);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 9;
            this.btn_cancel.Text = "Cancelar";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // empresaTableAdapter
            // 
            this.empresaTableAdapter.ClearBeforeFill = true;
            // 
            // tb_noAprob
            // 
            this.tb_noAprob.Location = new System.Drawing.Point(126, 162);
            this.tb_noAprob.Name = "tb_noAprob";
            this.tb_noAprob.Size = new System.Drawing.Size(79, 20);
            this.tb_noAprob.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(34, 165);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "No Aprobación:";
            // 
            // tb_anoAprob
            // 
            this.tb_anoAprob.Location = new System.Drawing.Point(126, 188);
            this.tb_anoAprob.Name = "tb_anoAprob";
            this.tb_anoAprob.Size = new System.Drawing.Size(79, 20);
            this.tb_anoAprob.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(34, 191);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Año Aprobación:";
            // 
            // panel_TM
            // 
            this.panel_TM.Controls.Add(this.rb_tmNC);
            this.panel_TM.Controls.Add(this.rb_tmFac);
            this.panel_TM.Location = new System.Drawing.Point(110, 128);
            this.panel_TM.Name = "panel_TM";
            this.panel_TM.Size = new System.Drawing.Size(200, 28);
            this.panel_TM.TabIndex = 14;
            // 
            // rb_tmNC
            // 
            this.rb_tmNC.AutoSize = true;
            this.rb_tmNC.Location = new System.Drawing.Point(83, 5);
            this.rb_tmNC.Name = "rb_tmNC";
            this.rb_tmNC.Size = new System.Drawing.Size(99, 17);
            this.rb_tmNC.TabIndex = 1;
            this.rb_tmNC.Text = "Nota de Crédito";
            this.rb_tmNC.UseVisualStyleBackColor = true;
            // 
            // rb_tmFac
            // 
            this.rb_tmFac.AutoSize = true;
            this.rb_tmFac.Checked = true;
            this.rb_tmFac.Location = new System.Drawing.Point(16, 5);
            this.rb_tmFac.Name = "rb_tmFac";
            this.rb_tmFac.Size = new System.Drawing.Size(61, 17);
            this.rb_tmFac.TabIndex = 0;
            this.rb_tmFac.TabStop = true;
            this.rb_tmFac.Text = "Factura";
            this.rb_tmFac.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(34, 135);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(61, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Tipo Folios:";
            // 
            // AgregarFolio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 258);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.panel_TM);
            this.Controls.Add(this.tb_anoAprob);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tb_noAprob);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_aceptar);
            this.Controls.Add(this.tb_folTot);
            this.Controls.Add(this.tb_folIni);
            this.Controls.Add(this.tb_serie);
            this.Controls.Add(this.cb_clveEmp);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AgregarFolio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Agregar Folio";
            this.Load += new System.EventHandler(this.AgregarFolio_Load);
            ((System.ComponentModel.ISupportInitialize)(this.empresaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cfdiDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cfdiDataSetBindingSource)).EndInit();
            this.panel_TM.ResumeLayout(false);
            this.panel_TM.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cb_clveEmp;
        private System.Windows.Forms.TextBox tb_serie;
        private System.Windows.Forms.TextBox tb_folIni;
        private System.Windows.Forms.TextBox tb_folTot;
        private System.Windows.Forms.Button btn_aceptar;
        private System.Windows.Forms.Button btn_cancel;
        private cfdiDataSet cfdiDataSet;
        private System.Windows.Forms.BindingSource empresaBindingSource;
        private SiscomCFDI.cfdiDataSetTableAdapters.empresaTableAdapter empresaTableAdapter;
        private System.Windows.Forms.BindingSource cfdiDataSetBindingSource;
        private System.Windows.Forms.TextBox tb_noAprob;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_anoAprob;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel_TM;
        private System.Windows.Forms.RadioButton rb_tmNC;
        private System.Windows.Forms.RadioButton rb_tmFac;
        private System.Windows.Forms.Label label7;
    }
}