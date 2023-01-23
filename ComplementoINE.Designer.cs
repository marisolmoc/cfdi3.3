namespace SiscomCFDI
{
    partial class ComplementoINE
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComplementoINE));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tb_idConta = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel_ambito = new System.Windows.Forms.Panel();
            this.dd_typeAmbito = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel_comite = new System.Windows.Forms.Panel();
            this.dd_typeComite = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dd_entidad = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dd_typeProceso = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnContinueINE = new System.Windows.Forms.Button();
            this.lbl_error = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.panel_ambito.SuspendLayout();
            this.panel_comite.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tb_idConta);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.panel_comite);
            this.groupBox1.Controls.Add(this.panel_ambito);
            this.groupBox1.Controls.Add(this.dd_entidad);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dd_typeProceso);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(259, 196);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Captura de datos";
            // 
            // tb_idConta
            // 
            this.tb_idConta.Location = new System.Drawing.Point(111, 113);
            this.tb_idConta.Name = "tb_idConta";
            this.tb_idConta.Size = new System.Drawing.Size(130, 20);
            this.tb_idConta.TabIndex = 8;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(13, 115);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 16);
            this.label6.TabIndex = 7;
            this.label6.Text = "Id Contabilidad:";
            // 
            // panel_ambito
            // 
            this.panel_ambito.BackColor = System.Drawing.Color.Transparent;
            this.panel_ambito.Controls.Add(this.dd_typeAmbito);
            this.panel_ambito.Controls.Add(this.label4);
            this.panel_ambito.Location = new System.Drawing.Point(6, 145);
            this.panel_ambito.Name = "panel_ambito";
            this.panel_ambito.Size = new System.Drawing.Size(246, 39);
            this.panel_ambito.TabIndex = 4;
            this.panel_ambito.Visible = false;
            // 
            // dd_typeAmbito
            // 
            this.dd_typeAmbito.FormattingEnabled = true;
            this.dd_typeAmbito.Items.AddRange(new object[] {
            "Local",
            "Federal"});
            this.dd_typeAmbito.Location = new System.Drawing.Point(62, 9);
            this.dd_typeAmbito.Name = "dd_typeAmbito";
            this.dd_typeAmbito.Size = new System.Drawing.Size(172, 21);
            this.dd_typeAmbito.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "Ámbito:";
            // 
            // panel_comite
            // 
            this.panel_comite.BackColor = System.Drawing.Color.Transparent;
            this.panel_comite.Controls.Add(this.dd_typeComite);
            this.panel_comite.Controls.Add(this.label3);
            this.panel_comite.Location = new System.Drawing.Point(6, 145);
            this.panel_comite.Name = "panel_comite";
            this.panel_comite.Size = new System.Drawing.Size(250, 45);
            this.panel_comite.TabIndex = 3;
            this.panel_comite.Visible = false;
            // 
            // dd_typeComite
            // 
            this.dd_typeComite.FormattingEnabled = true;
            this.dd_typeComite.Items.AddRange(new object[] {
            "Ejecutivo Estatal",
            "Ejecutivo Nacional"});
            this.dd_typeComite.Location = new System.Drawing.Point(105, 9);
            this.dd_typeComite.Name = "dd_typeComite";
            this.dd_typeComite.Size = new System.Drawing.Size(129, 21);
            this.dd_typeComite.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tipo de Comité:";
            // 
            // dd_entidad
            // 
            this.dd_entidad.AutoCompleteCustomSource.AddRange(new string[] {
            "AGU",
            "BCN",
            "BCS",
            "CAM",
            "CHP",
            "CHH",
            "COA",
            "COL",
            "DIF",
            "DUR",
            "GUA",
            "GRO",
            "HID",
            "JAL",
            "MEX",
            "MIC",
            "MOR",
            "NAY",
            "NLE",
            "OAX",
            "PUE",
            "QTO",
            "ROO",
            "SLP",
            "SIN",
            "SON",
            "TAB",
            "TAM",
            "TLA",
            "VER",
            "YUC",
            "ZAC"});
            this.dd_entidad.FormattingEnabled = true;
            this.dd_entidad.Items.AddRange(new object[] {
            "AGU",
            "BCN",
            "BCS",
            "CAM",
            "CHP",
            "CHH",
            "COA",
            "COL",
            "DIF",
            "DUR",
            "GUA",
            "GRO",
            "HID",
            "JAL",
            "MEX",
            "MIC",
            "MOR",
            "NAY",
            "NLE",
            "OAX",
            "PUE",
            "QTO",
            "ROO",
            "SLP",
            "SIN",
            "SON",
            "TAB",
            "TAM",
            "TLA",
            "VER",
            "YUC",
            "ZAC"});
            this.dd_entidad.Location = new System.Drawing.Point(103, 70);
            this.dd_entidad.Name = "dd_entidad";
            this.dd_entidad.Size = new System.Drawing.Size(138, 21);
            this.dd_entidad.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(13, 72);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(84, 16);
            this.label5.TabIndex = 5;
            this.label5.Text = "Clave Entidad:";
            // 
            // dd_typeProceso
            // 
            this.dd_typeProceso.FormattingEnabled = true;
            this.dd_typeProceso.Items.AddRange(new object[] {
            "Ordinario",
            "Precampaña",
            "Campaña"});
            this.dd_typeProceso.Location = new System.Drawing.Point(120, 28);
            this.dd_typeProceso.Name = "dd_typeProceso";
            this.dd_typeProceso.Size = new System.Drawing.Size(121, 21);
            this.dd_typeProceso.TabIndex = 2;
            this.dd_typeProceso.SelectedIndexChanged += new System.EventHandler(this.dd_typeProceso_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tipo de Proceso:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 0;
            // 
            // btnContinueINE
            // 
            this.btnContinueINE.Location = new System.Drawing.Point(195, 215);
            this.btnContinueINE.Name = "btnContinueINE";
            this.btnContinueINE.Size = new System.Drawing.Size(77, 34);
            this.btnContinueINE.TabIndex = 4;
            this.btnContinueINE.Text = "Continuar";
            this.btnContinueINE.UseVisualStyleBackColor = true;
            this.btnContinueINE.Click += new System.EventHandler(this.btnContinueINE_Click);
            // 
            // lbl_error
            // 
            this.lbl_error.AutoSize = true;
            this.lbl_error.ForeColor = System.Drawing.Color.Red;
            this.lbl_error.Location = new System.Drawing.Point(13, 216);
            this.lbl_error.Name = "lbl_error";
            this.lbl_error.Size = new System.Drawing.Size(168, 13);
            this.lbl_error.TabIndex = 5;
            this.lbl_error.Text = "*Todas los datos son obligatorios. ";
            // 
            // ComplementoINE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 258);
            this.Controls.Add(this.lbl_error);
            this.Controls.Add(this.btnContinueINE);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ComplementoINE";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Complemento INE";
            this.Load += new System.EventHandler(this.ComplementoINE_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel_ambito.ResumeLayout(false);
            this.panel_ambito.PerformLayout();
            this.panel_comite.ResumeLayout(false);
            this.panel_comite.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox dd_typeProceso;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox dd_typeComite;
        private System.Windows.Forms.Panel panel_comite;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel_ambito;
        private System.Windows.Forms.ComboBox dd_typeAmbito;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox dd_entidad;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_idConta;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnContinueINE;
        private System.Windows.Forms.Label lbl_error;
    }
}