namespace SiscomCFDI
{
    partial class Cancelaciones
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Cancelaciones));
            this.cb_ClaveEmp = new System.Windows.Forms.ComboBox();
            this.empresaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cfdiDataSet = new SiscomCFDI.cfdiDataSet();
            this.cfdiDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btn_buscar = new System.Windows.Forms.Button();
            this.tb_fFactura = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Clave_lbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_Serie = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tb_noAp = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.tb_anioAp = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.tb_tCamb = new System.Windows.Forms.TextBox();
            this.tb_IVA = new System.Windows.Forms.TextBox();
            this.tb_imp = new System.Windows.Forms.TextBox();
            this.tb_total = new System.Windows.Forms.TextBox();
            this.tb_nCerSAT = new System.Windows.Forms.TextBox();
            this.tb_uuid = new System.Windows.Forms.TextBox();
            this.rtb_sCFD = new System.Windows.Forms.RichTextBox();
            this.rtb_sSAT = new System.Windows.Forms.RichTextBox();
            this.rtb_cOrig = new System.Windows.Forms.RichTextBox();
            this.tb_noCer = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_aceptar = new System.Windows.Forms.Button();
            this.btn_cerrar = new System.Windows.Forms.Button();
            this.empresaTableAdapter = new SiscomCFDI.cfdiDataSetTableAdapters.empresaTableAdapter();
            this.tb_notC = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rb_notC = new System.Windows.Forms.RadioButton();
            this.rb_fac = new System.Windows.Forms.RadioButton();
            this.lbl_emp = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.empresaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cfdiDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cfdiDataSetBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cb_ClaveEmp
            // 
            this.cb_ClaveEmp.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.empresaBindingSource, "Clave", true));
            this.cb_ClaveEmp.DataSource = this.cfdiDataSetBindingSource;
            this.cb_ClaveEmp.FormattingEnabled = true;
            this.cb_ClaveEmp.Location = new System.Drawing.Point(110, 12);
            this.cb_ClaveEmp.Name = "cb_ClaveEmp";
            this.cb_ClaveEmp.Size = new System.Drawing.Size(59, 21);
            this.cb_ClaveEmp.TabIndex = 25;
            this.cb_ClaveEmp.SelectedIndexChanged += new System.EventHandler(this.cb_ClaveEmp_SelectedIndexChanged);
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
            // btn_buscar
            // 
            this.btn_buscar.Enabled = false;
            this.btn_buscar.Location = new System.Drawing.Point(553, 14);
            this.btn_buscar.Name = "btn_buscar";
            this.btn_buscar.Size = new System.Drawing.Size(89, 42);
            this.btn_buscar.TabIndex = 24;
            this.btn_buscar.Text = "Buscar";
            this.btn_buscar.UseVisualStyleBackColor = true;
            this.btn_buscar.Click += new System.EventHandler(this.btn_buscar_Click);
            // 
            // tb_fFactura
            // 
            this.tb_fFactura.Location = new System.Drawing.Point(368, 44);
            this.tb_fFactura.Name = "tb_fFactura";
            this.tb_fFactura.ReadOnly = true;
            this.tb_fFactura.Size = new System.Drawing.Size(69, 20);
            this.tb_fFactura.TabIndex = 23;
            this.tb_fFactura.Visible = false;
            this.tb_fFactura.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tb_fFactura_KeyUp);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(266, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 16);
            this.label4.TabIndex = 22;
            this.label4.Text = "No Factura:";
            this.label4.Visible = false;
            // 
            // Clave_lbl
            // 
            this.Clave_lbl.AutoSize = true;
            this.Clave_lbl.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Clave_lbl.Location = new System.Drawing.Point(15, 14);
            this.Clave_lbl.Name = "Clave_lbl";
            this.Clave_lbl.Size = new System.Drawing.Size(89, 16);
            this.Clave_lbl.TabIndex = 21;
            this.Clave_lbl.Text = "Clave Empresa:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(457, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 26;
            this.label1.Text = "Serie:";
            // 
            // tb_Serie
            // 
            this.tb_Serie.Location = new System.Drawing.Point(503, 44);
            this.tb_Serie.Name = "tb_Serie";
            this.tb_Serie.ReadOnly = true;
            this.tb_Serie.Size = new System.Drawing.Size(41, 20);
            this.tb_Serie.TabIndex = 27;
            this.tb_Serie.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tb_Serie_KeyUp);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.tb_noAp);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.tb_anioAp);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.tb_tCamb);
            this.panel1.Controls.Add(this.tb_IVA);
            this.panel1.Controls.Add(this.tb_imp);
            this.panel1.Controls.Add(this.tb_total);
            this.panel1.Controls.Add(this.tb_nCerSAT);
            this.panel1.Controls.Add(this.tb_uuid);
            this.panel1.Controls.Add(this.rtb_sCFD);
            this.panel1.Controls.Add(this.rtb_sSAT);
            this.panel1.Controls.Add(this.rtb_cOrig);
            this.panel1.Controls.Add(this.tb_noCer);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(-3, 79);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(662, 359);
            this.panel1.TabIndex = 28;
            // 
            // tb_noAp
            // 
            this.tb_noAp.Location = new System.Drawing.Point(505, 202);
            this.tb_noAp.Name = "tb_noAp";
            this.tb_noAp.ReadOnly = true;
            this.tb_noAp.Size = new System.Drawing.Size(105, 20);
            this.tb_noAp.TabIndex = 51;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(438, 206);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(63, 16);
            this.label17.TabIndex = 50;
            this.label17.Text = "No Aprob:";
            // 
            // tb_anioAp
            // 
            this.tb_anioAp.Location = new System.Drawing.Point(505, 167);
            this.tb_anioAp.Name = "tb_anioAp";
            this.tb_anioAp.ReadOnly = true;
            this.tb_anioAp.Size = new System.Drawing.Size(105, 20);
            this.tb_anioAp.TabIndex = 49;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(438, 169);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(71, 16);
            this.label16.TabIndex = 48;
            this.label16.Text = "Año Aprob:";
            // 
            // tb_tCamb
            // 
            this.tb_tCamb.Location = new System.Drawing.Point(505, 131);
            this.tb_tCamb.Name = "tb_tCamb";
            this.tb_tCamb.ReadOnly = true;
            this.tb_tCamb.Size = new System.Drawing.Size(105, 20);
            this.tb_tCamb.TabIndex = 47;
            // 
            // tb_IVA
            // 
            this.tb_IVA.Location = new System.Drawing.Point(505, 94);
            this.tb_IVA.Name = "tb_IVA";
            this.tb_IVA.ReadOnly = true;
            this.tb_IVA.Size = new System.Drawing.Size(105, 20);
            this.tb_IVA.TabIndex = 46;
            // 
            // tb_imp
            // 
            this.tb_imp.Location = new System.Drawing.Point(505, 58);
            this.tb_imp.Name = "tb_imp";
            this.tb_imp.ReadOnly = true;
            this.tb_imp.Size = new System.Drawing.Size(105, 20);
            this.tb_imp.TabIndex = 45;
            // 
            // tb_total
            // 
            this.tb_total.Location = new System.Drawing.Point(504, 19);
            this.tb_total.Name = "tb_total";
            this.tb_total.ReadOnly = true;
            this.tb_total.Size = new System.Drawing.Size(106, 20);
            this.tb_total.TabIndex = 44;
            // 
            // tb_nCerSAT
            // 
            this.tb_nCerSAT.Location = new System.Drawing.Point(118, 319);
            this.tb_nCerSAT.Name = "tb_nCerSAT";
            this.tb_nCerSAT.ReadOnly = true;
            this.tb_nCerSAT.Size = new System.Drawing.Size(289, 20);
            this.tb_nCerSAT.TabIndex = 43;
            // 
            // tb_uuid
            // 
            this.tb_uuid.Location = new System.Drawing.Point(118, 284);
            this.tb_uuid.Name = "tb_uuid";
            this.tb_uuid.ReadOnly = true;
            this.tb_uuid.Size = new System.Drawing.Size(289, 20);
            this.tb_uuid.TabIndex = 42;
            // 
            // rtb_sCFD
            // 
            this.rtb_sCFD.Location = new System.Drawing.Point(118, 133);
            this.rtb_sCFD.Name = "rtb_sCFD";
            this.rtb_sCFD.ReadOnly = true;
            this.rtb_sCFD.Size = new System.Drawing.Size(289, 65);
            this.rtb_sCFD.TabIndex = 41;
            this.rtb_sCFD.Text = "";
            // 
            // rtb_sSAT
            // 
            this.rtb_sSAT.Location = new System.Drawing.Point(118, 209);
            this.rtb_sSAT.Name = "rtb_sSAT";
            this.rtb_sSAT.ReadOnly = true;
            this.rtb_sSAT.Size = new System.Drawing.Size(289, 65);
            this.rtb_sSAT.TabIndex = 40;
            this.rtb_sSAT.Text = "";
            // 
            // rtb_cOrig
            // 
            this.rtb_cOrig.Location = new System.Drawing.Point(118, 58);
            this.rtb_cOrig.Name = "rtb_cOrig";
            this.rtb_cOrig.ReadOnly = true;
            this.rtb_cOrig.Size = new System.Drawing.Size(289, 65);
            this.rtb_cOrig.TabIndex = 39;
            this.rtb_cOrig.Text = "";
            // 
            // tb_noCer
            // 
            this.tb_noCer.Location = new System.Drawing.Point(118, 19);
            this.tb_noCer.Name = "tb_noCer";
            this.tb_noCer.ReadOnly = true;
            this.tb_noCer.Size = new System.Drawing.Size(289, 20);
            this.tb_noCer.TabIndex = 37;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(438, 135);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(61, 16);
            this.label12.TabIndex = 36;
            this.label12.Text = "T Cambio:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(438, 96);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(31, 16);
            this.label11.TabIndex = 35;
            this.label11.Text = "IVA:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(438, 60);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 16);
            this.label10.TabIndex = 34;
            this.label10.Text = "Impuesto:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(438, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(39, 16);
            this.label9.TabIndex = 33;
            this.label9.Text = "Total:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(16, 321);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 16);
            this.label8.TabIndex = 32;
            this.label8.Text = "No Cert SAT:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(16, 286);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 16);
            this.label7.TabIndex = 31;
            this.label7.Text = "UUID:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(16, 209);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 16);
            this.label6.TabIndex = 30;
            this.label6.Text = "Sello SAT:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(16, 135);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(61, 16);
            this.label5.TabIndex = 29;
            this.label5.Text = "Sello CFD:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 16);
            this.label3.TabIndex = 28;
            this.label3.Text = "Cadena Original:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 16);
            this.label2.TabIndex = 27;
            this.label2.Text = "No Certificado:";
            // 
            // btn_aceptar
            // 
            this.btn_aceptar.Location = new System.Drawing.Point(393, 456);
            this.btn_aceptar.Name = "btn_aceptar";
            this.btn_aceptar.Size = new System.Drawing.Size(75, 23);
            this.btn_aceptar.TabIndex = 29;
            this.btn_aceptar.Text = "Aceptar";
            this.btn_aceptar.UseVisualStyleBackColor = true;
            this.btn_aceptar.Click += new System.EventHandler(this.btn_aceptar_Click);
            // 
            // btn_cerrar
            // 
            this.btn_cerrar.Location = new System.Drawing.Point(520, 456);
            this.btn_cerrar.Name = "btn_cerrar";
            this.btn_cerrar.Size = new System.Drawing.Size(75, 23);
            this.btn_cerrar.TabIndex = 30;
            this.btn_cerrar.Text = "Cerrar";
            this.btn_cerrar.UseVisualStyleBackColor = true;
            this.btn_cerrar.Click += new System.EventHandler(this.btn_cerrar_Click);
            // 
            // empresaTableAdapter
            // 
            this.empresaTableAdapter.ClearBeforeFill = true;
            // 
            // tb_notC
            // 
            this.tb_notC.Location = new System.Drawing.Point(368, 44);
            this.tb_notC.Name = "tb_notC";
            this.tb_notC.ReadOnly = true;
            this.tb_notC.Size = new System.Drawing.Size(69, 20);
            this.tb_notC.TabIndex = 32;
            this.tb_notC.Visible = false;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(266, 46);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(96, 16);
            this.label13.TabIndex = 31;
            this.label13.Text = "No Nota Crédito:";
            this.label13.Visible = false;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(15, 46);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(36, 16);
            this.label14.TabIndex = 33;
            this.label14.Text = "Tipo:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rb_notC);
            this.panel2.Controls.Add(this.rb_fac);
            this.panel2.Location = new System.Drawing.Point(57, 39);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(164, 26);
            this.panel2.TabIndex = 34;
            // 
            // rb_notC
            // 
            this.rb_notC.AutoSize = true;
            this.rb_notC.Location = new System.Drawing.Point(70, 5);
            this.rb_notC.Name = "rb_notC";
            this.rb_notC.Size = new System.Drawing.Size(84, 17);
            this.rb_notC.TabIndex = 1;
            this.rb_notC.TabStop = true;
            this.rb_notC.Text = "Nota Crédito";
            this.rb_notC.UseVisualStyleBackColor = true;
            this.rb_notC.CheckedChanged += new System.EventHandler(this.rb_fac_CheckedChanged);
            // 
            // rb_fac
            // 
            this.rb_fac.AutoSize = true;
            this.rb_fac.Location = new System.Drawing.Point(3, 6);
            this.rb_fac.Name = "rb_fac";
            this.rb_fac.Size = new System.Drawing.Size(61, 17);
            this.rb_fac.TabIndex = 0;
            this.rb_fac.TabStop = true;
            this.rb_fac.Text = "Factura";
            this.rb_fac.UseVisualStyleBackColor = true;
            this.rb_fac.CheckedChanged += new System.EventHandler(this.rb_fac_CheckedChanged);
            // 
            // lbl_emp
            // 
            this.lbl_emp.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_emp.Location = new System.Drawing.Point(175, 15);
            this.lbl_emp.Name = "lbl_emp";
            this.lbl_emp.Size = new System.Drawing.Size(369, 21);
            this.lbl_emp.TabIndex = 35;
            // 
            // Cancelaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 500);
            this.Controls.Add(this.lbl_emp);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.tb_notC);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.btn_cerrar);
            this.Controls.Add(this.btn_aceptar);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tb_Serie);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_ClaveEmp);
            this.Controls.Add(this.btn_buscar);
            this.Controls.Add(this.tb_fFactura);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.Clave_lbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Cancelaciones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cancelaciones";
            this.Load += new System.EventHandler(this.Cancelaciones_Load);
            ((System.ComponentModel.ISupportInitialize)(this.empresaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cfdiDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cfdiDataSetBindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox cb_ClaveEmp;
        private System.Windows.Forms.Button btn_buscar;
        private System.Windows.Forms.TextBox tb_fFactura;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label Clave_lbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_Serie;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_nCerSAT;
        private System.Windows.Forms.TextBox tb_uuid;
        private System.Windows.Forms.RichTextBox rtb_sCFD;
        private System.Windows.Forms.RichTextBox rtb_sSAT;
        private System.Windows.Forms.RichTextBox rtb_cOrig;
        private System.Windows.Forms.TextBox tb_noCer;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tb_tCamb;
        private System.Windows.Forms.TextBox tb_IVA;
        private System.Windows.Forms.TextBox tb_imp;
        private System.Windows.Forms.TextBox tb_total;
        private System.Windows.Forms.Button btn_aceptar;
        private System.Windows.Forms.Button btn_cerrar;
        private cfdiDataSet cfdiDataSet;
        private System.Windows.Forms.BindingSource empresaBindingSource;
        private SiscomCFDI.cfdiDataSetTableAdapters.empresaTableAdapter empresaTableAdapter;
        private System.Windows.Forms.BindingSource cfdiDataSetBindingSource;
        private System.Windows.Forms.TextBox tb_notC;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rb_notC;
        private System.Windows.Forms.RadioButton rb_fac;
        private System.Windows.Forms.TextBox tb_noAp;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox tb_anioAp;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lbl_emp;

    }
}