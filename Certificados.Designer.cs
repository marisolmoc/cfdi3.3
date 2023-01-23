namespace SiscomCFDI
{
    partial class Certificados
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Certificados));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_rutaCer = new System.Windows.Forms.TextBox();
            this.btn_exFold = new System.Windows.Forms.Button();
            this.tb_cerN = new System.Windows.Forms.TextBox();
            this.btn_opCer = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_pwC = new System.Windows.Forms.TextBox();
            this.btn_cerrar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lb_validDe = new System.Windows.Forms.Label();
            this.lb_valiHas = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lb_noCer = new System.Windows.Forms.Label();
            this.btn_Guardar = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cb_cEmp = new System.Windows.Forms.ComboBox();
            this.empresaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cfdiDataSet = new SiscomCFDI.cfdiDataSet();
            this.cfdiDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.empresaTableAdapter = new SiscomCFDI.cfdiDataSetTableAdapters.empresaTableAdapter();
            this.tb_keyN = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_opKey = new System.Windows.Forms.Button();
            this.btn_ok = new System.Windows.Forms.Button();
            this.lbl_emp = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.empresaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cfdiDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cfdiDataSetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Folder Certificado:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Certificado:";
            // 
            // tb_rutaCer
            // 
            this.tb_rutaCer.Location = new System.Drawing.Point(110, 41);
            this.tb_rutaCer.Name = "tb_rutaCer";
            this.tb_rutaCer.Size = new System.Drawing.Size(264, 20);
            this.tb_rutaCer.TabIndex = 2;
            // 
            // btn_exFold
            // 
            this.btn_exFold.Location = new System.Drawing.Point(380, 39);
            this.btn_exFold.Name = "btn_exFold";
            this.btn_exFold.Size = new System.Drawing.Size(75, 23);
            this.btn_exFold.TabIndex = 3;
            this.btn_exFold.Text = "Examinar";
            this.btn_exFold.UseVisualStyleBackColor = true;
            this.btn_exFold.Click += new System.EventHandler(this.btn_exCer_Click);
            // 
            // tb_cerN
            // 
            this.tb_cerN.Location = new System.Drawing.Point(78, 82);
            this.tb_cerN.Name = "tb_cerN";
            this.tb_cerN.Size = new System.Drawing.Size(268, 20);
            this.tb_cerN.TabIndex = 4;
            // 
            // btn_opCer
            // 
            this.btn_opCer.Location = new System.Drawing.Point(352, 80);
            this.btn_opCer.Name = "btn_opCer";
            this.btn_opCer.Size = new System.Drawing.Size(75, 23);
            this.btn_opCer.TabIndex = 5;
            this.btn_opCer.Text = "Examinar";
            this.btn_opCer.UseVisualStyleBackColor = true;
            this.btn_opCer.Click += new System.EventHandler(this.btn_opCer_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Contraseña:";
            // 
            // tb_pwC
            // 
            this.tb_pwC.Location = new System.Drawing.Point(78, 137);
            this.tb_pwC.Name = "tb_pwC";
            this.tb_pwC.PasswordChar = '•';
            this.tb_pwC.Size = new System.Drawing.Size(143, 20);
            this.tb_pwC.TabIndex = 7;
            this.tb_pwC.UseSystemPasswordChar = true;
            this.tb_pwC.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tb_pwC_KeyUp);
            // 
            // btn_cerrar
            // 
            this.btn_cerrar.Location = new System.Drawing.Point(380, 280);
            this.btn_cerrar.Name = "btn_cerrar";
            this.btn_cerrar.Size = new System.Drawing.Size(75, 23);
            this.btn_cerrar.TabIndex = 8;
            this.btn_cerrar.Text = "Cerrar";
            this.btn_cerrar.UseVisualStyleBackColor = true;
            this.btn_cerrar.Click += new System.EventHandler(this.btn_cerrar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 215);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Validez de:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 241);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Validez hasta:";
            // 
            // lb_validDe
            // 
            this.lb_validDe.AutoSize = true;
            this.lb_validDe.Location = new System.Drawing.Point(92, 215);
            this.lb_validDe.Name = "lb_validDe";
            this.lb_validDe.Size = new System.Drawing.Size(0, 13);
            this.lb_validDe.TabIndex = 11;
            // 
            // lb_valiHas
            // 
            this.lb_valiHas.AutoSize = true;
            this.lb_valiHas.Location = new System.Drawing.Point(92, 241);
            this.lb_valiHas.Name = "lb_valiHas";
            this.lb_valiHas.Size = new System.Drawing.Size(0, 13);
            this.lb_valiHas.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 185);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 13);
            this.label8.TabIndex = 13;
            this.label8.Text = "No Certificado:";
            // 
            // lb_noCer
            // 
            this.lb_noCer.AutoSize = true;
            this.lb_noCer.Location = new System.Drawing.Point(92, 185);
            this.lb_noCer.Name = "lb_noCer";
            this.lb_noCer.Size = new System.Drawing.Size(0, 13);
            this.lb_noCer.TabIndex = 14;
            // 
            // btn_Guardar
            // 
            this.btn_Guardar.Location = new System.Drawing.Point(290, 280);
            this.btn_Guardar.Name = "btn_Guardar";
            this.btn_Guardar.Size = new System.Drawing.Size(75, 23);
            this.btn_Guardar.TabIndex = 15;
            this.btn_Guardar.Text = "Guardar";
            this.btn_Guardar.UseVisualStyleBackColor = true;
            this.btn_Guardar.Click += new System.EventHandler(this.btn_Guardar_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Clave Empresa";
            // 
            // cb_cEmp
            // 
            this.cb_cEmp.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.empresaBindingSource, "Clave", true));
            this.cb_cEmp.DataSource = this.cfdiDataSetBindingSource;
            this.cb_cEmp.FormattingEnabled = true;
            this.cb_cEmp.Location = new System.Drawing.Point(95, 10);
            this.cb_cEmp.Name = "cb_cEmp";
            this.cb_cEmp.Size = new System.Drawing.Size(52, 21);
            this.cb_cEmp.TabIndex = 17;
            this.cb_cEmp.TextChanged += new System.EventHandler(this.cb_cEmp_TextChanged);
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
            // empresaTableAdapter
            // 
            this.empresaTableAdapter.ClearBeforeFill = true;
            // 
            // tb_keyN
            // 
            this.tb_keyN.Location = new System.Drawing.Point(78, 109);
            this.tb_keyN.Name = "tb_keyN";
            this.tb_keyN.Size = new System.Drawing.Size(268, 20);
            this.tb_keyN.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 112);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(28, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Key:";
            // 
            // btn_opKey
            // 
            this.btn_opKey.Location = new System.Drawing.Point(352, 109);
            this.btn_opKey.Name = "btn_opKey";
            this.btn_opKey.Size = new System.Drawing.Size(75, 23);
            this.btn_opKey.TabIndex = 20;
            this.btn_opKey.Text = "Examinar";
            this.btn_opKey.UseVisualStyleBackColor = true;
            this.btn_opKey.Click += new System.EventHandler(this.btn_opKey_Click);
            // 
            // btn_ok
            // 
            this.btn_ok.Location = new System.Drawing.Point(253, 135);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(75, 23);
            this.btn_ok.TabIndex = 21;
            this.btn_ok.Text = "Aceptar";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // lbl_emp
            // 
            this.lbl_emp.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_emp.Location = new System.Drawing.Point(153, 14);
            this.lbl_emp.Name = "lbl_emp";
            this.lbl_emp.Size = new System.Drawing.Size(302, 22);
            this.lbl_emp.TabIndex = 22;
            // 
            // Certificados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(468, 315);
            this.Controls.Add(this.lbl_emp);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.btn_opKey);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tb_keyN);
            this.Controls.Add(this.cb_cEmp);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btn_Guardar);
            this.Controls.Add(this.lb_noCer);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lb_valiHas);
            this.Controls.Add(this.lb_validDe);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_cerrar);
            this.Controls.Add(this.tb_pwC);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_opCer);
            this.Controls.Add(this.tb_cerN);
            this.Controls.Add(this.btn_exFold);
            this.Controls.Add(this.tb_rutaCer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Certificados";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Certificados";
            this.Load += new System.EventHandler(this.Certificados_Load);
            ((System.ComponentModel.ISupportInitialize)(this.empresaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cfdiDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cfdiDataSetBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_rutaCer;
        private System.Windows.Forms.Button btn_exFold;
        private System.Windows.Forms.TextBox tb_cerN;
        private System.Windows.Forms.Button btn_opCer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_pwC;
        private System.Windows.Forms.Button btn_cerrar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lb_validDe;
        private System.Windows.Forms.Label lb_valiHas;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lb_noCer;
        private System.Windows.Forms.Button btn_Guardar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cb_cEmp;
        private cfdiDataSet cfdiDataSet;
        private System.Windows.Forms.BindingSource empresaBindingSource;
        private SiscomCFDI.cfdiDataSetTableAdapters.empresaTableAdapter empresaTableAdapter;
        private System.Windows.Forms.BindingSource cfdiDataSetBindingSource;
        private System.Windows.Forms.TextBox tb_keyN;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btn_opKey;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Label lbl_emp;
    }
}