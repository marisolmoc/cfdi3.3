namespace SiscomCFDI
{
    partial class NotasCred
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotasCred));
            this.tb_iva = new System.Windows.Forms.TextBox();
            this.Clave_lbl = new System.Windows.Forms.Label();
            this.tb_total = new System.Windows.Forms.TextBox();
            this.lbl_fFecha = new System.Windows.Forms.Label();
            this.tb_subtotal = new System.Windows.Forms.TextBox();
            this.btn_xml = new System.Windows.Forms.Button();
            this.lbl_fRFC = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btn_fSalir = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_Nombre = new System.Windows.Forms.Label();
            this.imprimir_btn = new System.Windows.Forms.Button();
            this.tb_tm = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cb_ClaveEmp = new System.Windows.Forms.ComboBox();
            this.empresaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cfdiDataSet = new SiscomCFDI.cfdiDataSet();
            this.cfdiDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.btn_buscar = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_fnomb = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gb_impuestos = new System.Windows.Forms.GroupBox();
            this.dg_impuestos = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cb_formapago = new System.Windows.Forms.ComboBox();
            this.lbl_nota2 = new System.Windows.Forms.Label();
            this.tb_fFactura = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cb_usocfdi = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.empresaTableAdapter = new SiscomCFDI.cfdiDataSetTableAdapters.empresaTableAdapter();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rb_dolares = new System.Windows.Forms.RadioButton();
            this.label9 = new System.Windows.Forms.Label();
            this.rb_pesos = new System.Windows.Forms.RadioButton();
            this.tb_cambio = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cb_webservice = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.empresaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cfdiDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cfdiDataSetBindingSource)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.gb_impuestos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_impuestos)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tb_iva
            // 
            this.tb_iva.Location = new System.Drawing.Point(586, 216);
            this.tb_iva.Name = "tb_iva";
            this.tb_iva.ReadOnly = true;
            this.tb_iva.Size = new System.Drawing.Size(102, 23);
            this.tb_iva.TabIndex = 5;
            this.tb_iva.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Clave_lbl
            // 
            this.Clave_lbl.AutoSize = true;
            this.Clave_lbl.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Clave_lbl.Location = new System.Drawing.Point(14, 11);
            this.Clave_lbl.Name = "Clave_lbl";
            this.Clave_lbl.Size = new System.Drawing.Size(89, 16);
            this.Clave_lbl.TabIndex = 28;
            this.Clave_lbl.Text = "Clave Empresa:";
            // 
            // tb_total
            // 
            this.tb_total.Location = new System.Drawing.Point(586, 246);
            this.tb_total.Name = "tb_total";
            this.tb_total.ReadOnly = true;
            this.tb_total.Size = new System.Drawing.Size(102, 23);
            this.tb_total.TabIndex = 6;
            this.tb_total.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbl_fFecha
            // 
            this.lbl_fFecha.AutoSize = true;
            this.lbl_fFecha.Font = new System.Drawing.Font("Trebuchet MS", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_fFecha.Location = new System.Drawing.Point(540, 24);
            this.lbl_fFecha.Name = "lbl_fFecha";
            this.lbl_fFecha.Size = new System.Drawing.Size(12, 16);
            this.lbl_fFecha.TabIndex = 21;
            this.lbl_fFecha.Text = "-";
            // 
            // tb_subtotal
            // 
            this.tb_subtotal.Location = new System.Drawing.Point(586, 187);
            this.tb_subtotal.Name = "tb_subtotal";
            this.tb_subtotal.ReadOnly = true;
            this.tb_subtotal.Size = new System.Drawing.Size(102, 23);
            this.tb_subtotal.TabIndex = 4;
            this.tb_subtotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btn_xml
            // 
            this.btn_xml.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_xml.Location = new System.Drawing.Point(475, 602);
            this.btn_xml.Name = "btn_xml";
            this.btn_xml.Size = new System.Drawing.Size(76, 30);
            this.btn_xml.TabIndex = 2;
            this.btn_xml.Text = "Timbrar NC";
            this.btn_xml.UseVisualStyleBackColor = true;
            this.btn_xml.Click += new System.EventHandler(this.btn_xml_Click);
            // 
            // lbl_fRFC
            // 
            this.lbl_fRFC.AutoSize = true;
            this.lbl_fRFC.Font = new System.Drawing.Font("Trebuchet MS", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_fRFC.Location = new System.Drawing.Point(348, 24);
            this.lbl_fRFC.Name = "lbl_fRFC";
            this.lbl_fRFC.Size = new System.Drawing.Size(12, 16);
            this.lbl_fRFC.TabIndex = 19;
            this.lbl_fRFC.Text = "-";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Trebuchet MS", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(536, 251);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 16);
            this.label7.TabIndex = 3;
            this.label7.Text = "Total:  ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Trebuchet MS", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(511, 221);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 16);
            this.label5.TabIndex = 2;
            this.label5.Text = "Impuestos: ";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(6, 24);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(682, 157);
            this.dataGridView1.TabIndex = 0;
            // 
            // btn_fSalir
            // 
            this.btn_fSalir.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_fSalir.Location = new System.Drawing.Point(629, 602);
            this.btn_fSalir.Name = "btn_fSalir";
            this.btn_fSalir.Size = new System.Drawing.Size(75, 30);
            this.btn_fSalir.TabIndex = 4;
            this.btn_fSalir.Text = "Salir";
            this.btn_fSalir.UseVisualStyleBackColor = true;
            this.btn_fSalir.Click += new System.EventHandler(this.btn_fSalir_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(516, 192);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sub Total: ";
            // 
            // lbl_Nombre
            // 
            this.lbl_Nombre.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Nombre.Location = new System.Drawing.Point(171, 9);
            this.lbl_Nombre.Name = "lbl_Nombre";
            this.lbl_Nombre.Size = new System.Drawing.Size(389, 28);
            this.lbl_Nombre.TabIndex = 39;
            this.lbl_Nombre.Text = ":";
            // 
            // imprimir_btn
            // 
            this.imprimir_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.imprimir_btn.Enabled = false;
            this.imprimir_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.imprimir_btn.Location = new System.Drawing.Point(23, 602);
            this.imprimir_btn.Name = "imprimir_btn";
            this.imprimir_btn.Size = new System.Drawing.Size(69, 30);
            this.imprimir_btn.TabIndex = 3;
            this.imprimir_btn.Text = "Crear PDF";
            this.imprimir_btn.UseVisualStyleBackColor = true;
            this.imprimir_btn.Click += new System.EventHandler(this.imprimir_btn_Click);
            // 
            // tb_tm
            // 
            this.tb_tm.Location = new System.Drawing.Point(341, 43);
            this.tb_tm.Name = "tb_tm";
            this.tb_tm.Size = new System.Drawing.Size(30, 20);
            this.tb_tm.TabIndex = 37;
            this.tb_tm.Text = "DN";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(314, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 16);
            this.label3.TabIndex = 18;
            this.label3.Text = "RFC:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(308, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(27, 16);
            this.label8.TabIndex = 36;
            this.label8.Text = "TM:";
            // 
            // cb_ClaveEmp
            // 
            this.cb_ClaveEmp.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.empresaBindingSource, "Clave", true));
            this.cb_ClaveEmp.DataSource = this.cfdiDataSetBindingSource;
            this.cb_ClaveEmp.FormattingEnabled = true;
            this.cb_ClaveEmp.Location = new System.Drawing.Point(106, 9);
            this.cb_ClaveEmp.Name = "cb_ClaveEmp";
            this.cb_ClaveEmp.Size = new System.Drawing.Size(59, 21);
            this.cb_ClaveEmp.TabIndex = 34;
            this.cb_ClaveEmp.SelectedValueChanged += new System.EventHandler(this.cb_ClaveEmp_SelectedValueChanged);
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
            this.btn_buscar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_buscar.Location = new System.Drawing.Point(600, 13);
            this.btn_buscar.Name = "btn_buscar";
            this.btn_buscar.Size = new System.Drawing.Size(91, 49);
            this.btn_buscar.TabIndex = 1;
            this.btn_buscar.Text = "Buscar";
            this.btn_buscar.UseVisualStyleBackColor = true;
            this.btn_buscar.Click += new System.EventHandler(this.btn_buscar_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(493, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 16);
            this.label6.TabIndex = 20;
            this.label6.Text = "Fecha:";
            // 
            // lbl_fnomb
            // 
            this.lbl_fnomb.Font = new System.Drawing.Font("Trebuchet MS", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_fnomb.Location = new System.Drawing.Point(65, 24);
            this.lbl_fnomb.Name = "lbl_fnomb";
            this.lbl_fnomb.Size = new System.Drawing.Size(243, 16);
            this.lbl_fnomb.TabIndex = 17;
            this.lbl_fnomb.Text = "-";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gb_impuestos);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.lbl_nota2);
            this.groupBox1.Controls.Add(this.tb_total);
            this.groupBox1.Controls.Add(this.tb_iva);
            this.groupBox1.Controls.Add(this.tb_subtotal);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(14, 88);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(694, 415);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Detalle de Nota de Crédito";
            // 
            // gb_impuestos
            // 
            this.gb_impuestos.Controls.Add(this.dg_impuestos);
            this.gb_impuestos.Location = new System.Drawing.Point(6, 248);
            this.gb_impuestos.Name = "gb_impuestos";
            this.gb_impuestos.Size = new System.Drawing.Size(504, 156);
            this.gb_impuestos.TabIndex = 41;
            this.gb_impuestos.TabStop = false;
            this.gb_impuestos.Text = "Impuestos";
            // 
            // dg_impuestos
            // 
            this.dg_impuestos.AllowUserToAddRows = false;
            this.dg_impuestos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dg_impuestos.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dg_impuestos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_impuestos.Location = new System.Drawing.Point(6, 17);
            this.dg_impuestos.MultiSelect = false;
            this.dg_impuestos.Name = "dg_impuestos";
            this.dg_impuestos.ReadOnly = true;
            this.dg_impuestos.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dg_impuestos.Size = new System.Drawing.Size(492, 133);
            this.dg_impuestos.TabIndex = 36;
            this.dg_impuestos.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dg_impuestos_UserDeletingRow);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cb_formapago);
            this.groupBox3.Location = new System.Drawing.Point(6, 187);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(220, 55);
            this.groupBox3.TabIndex = 40;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Forma de Pago";
            // 
            // cb_formapago
            // 
            this.cb_formapago.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_formapago.FormattingEnabled = true;
            this.cb_formapago.Location = new System.Drawing.Point(6, 22);
            this.cb_formapago.Name = "cb_formapago";
            this.cb_formapago.Size = new System.Drawing.Size(208, 24);
            this.cb_formapago.TabIndex = 0;
            // 
            // lbl_nota2
            // 
            this.lbl_nota2.Font = new System.Drawing.Font("Trebuchet MS", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_nota2.Location = new System.Drawing.Point(510, 301);
            this.lbl_nota2.Name = "lbl_nota2";
            this.lbl_nota2.Size = new System.Drawing.Size(178, 59);
            this.lbl_nota2.TabIndex = 8;
            // 
            // tb_fFactura
            // 
            this.tb_fFactura.Location = new System.Drawing.Point(496, 43);
            this.tb_fFactura.Name = "tb_fFactura";
            this.tb_fFactura.Size = new System.Drawing.Size(50, 20);
            this.tb_fFactura.TabIndex = 0;
            this.tb_fFactura.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tb_fFactura_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 16);
            this.label2.TabIndex = 16;
            this.label2.Text = "Nombre:";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(-2, 82);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(726, 514);
            this.panel1.TabIndex = 31;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cb_usocfdi);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.lbl_fFecha);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.lbl_fRFC);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.lbl_fnomb);
            this.groupBox2.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(14, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(694, 79);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Datos Cliente";
            // 
            // cb_usocfdi
            // 
            this.cb_usocfdi.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_usocfdi.FormattingEnabled = true;
            this.cb_usocfdi.Location = new System.Drawing.Point(72, 48);
            this.cb_usocfdi.Name = "cb_usocfdi";
            this.cb_usocfdi.Size = new System.Drawing.Size(502, 24);
            this.cb_usocfdi.TabIndex = 24;
            this.cb_usocfdi.Tag = "";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(9, 51);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(57, 16);
            this.label16.TabIndex = 25;
            this.label16.Text = "Uso CFDI:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(377, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 16);
            this.label4.TabIndex = 29;
            this.label4.Text = "No Nota de Crédito:";
            // 
            // empresaTableAdapter
            // 
            this.empresaTableAdapter.ClearBeforeFill = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rb_dolares);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.rb_pesos);
            this.panel2.Controls.Add(this.tb_cambio);
            this.panel2.Location = new System.Drawing.Point(9, 36);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(293, 37);
            this.panel2.TabIndex = 40;
            // 
            // rb_dolares
            // 
            this.rb_dolares.AutoSize = true;
            this.rb_dolares.Location = new System.Drawing.Point(225, 9);
            this.rb_dolares.Name = "rb_dolares";
            this.rb_dolares.Size = new System.Drawing.Size(61, 17);
            this.rb_dolares.TabIndex = 31;
            this.rb_dolares.Text = "Dolares";
            this.rb_dolares.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(7, 9);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(78, 16);
            this.label9.TabIndex = 28;
            this.label9.Text = "Tipo Cambio:";
            // 
            // rb_pesos
            // 
            this.rb_pesos.AutoSize = true;
            this.rb_pesos.Checked = true;
            this.rb_pesos.Location = new System.Drawing.Point(165, 8);
            this.rb_pesos.Name = "rb_pesos";
            this.rb_pesos.Size = new System.Drawing.Size(54, 17);
            this.rb_pesos.TabIndex = 30;
            this.rb_pesos.TabStop = true;
            this.rb_pesos.Text = "Pesos";
            this.rb_pesos.UseVisualStyleBackColor = true;
            // 
            // tb_cambio
            // 
            this.tb_cambio.Location = new System.Drawing.Point(91, 7);
            this.tb_cambio.Name = "tb_cambio";
            this.tb_cambio.Size = new System.Drawing.Size(60, 20);
            this.tb_cambio.TabIndex = 29;
            // 
            // cb_webservice
            // 
            this.cb_webservice.AutoSize = true;
            this.cb_webservice.Location = new System.Drawing.Point(557, 49);
            this.cb_webservice.Name = "cb_webservice";
            this.cb_webservice.Size = new System.Drawing.Size(15, 14);
            this.cb_webservice.TabIndex = 41;
            this.toolTip1.SetToolTip(this.cb_webservice, "Opcion de pruebas");
            this.cb_webservice.UseVisualStyleBackColor = true;
            // 
            // NotasCred
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 639);
            this.Controls.Add(this.cb_webservice);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.Clave_lbl);
            this.Controls.Add(this.btn_xml);
            this.Controls.Add(this.btn_fSalir);
            this.Controls.Add(this.lbl_Nombre);
            this.Controls.Add(this.imprimir_btn);
            this.Controls.Add(this.tb_tm);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cb_ClaveEmp);
            this.Controls.Add(this.btn_buscar);
            this.Controls.Add(this.tb_fFactura);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NotasCred";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Notas de Crédito";
            this.Load += new System.EventHandler(this.NotasCred_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.empresaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cfdiDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cfdiDataSetBindingSource)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gb_impuestos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg_impuestos)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_iva;
        private System.Windows.Forms.Label Clave_lbl;
        private System.Windows.Forms.TextBox tb_total;
        private System.Windows.Forms.Label lbl_fFecha;
        private System.Windows.Forms.TextBox tb_subtotal;
        private System.Windows.Forms.Button btn_xml;
        private System.Windows.Forms.Label lbl_fRFC;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_fSalir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_Nombre;
        private System.Windows.Forms.Button imprimir_btn;
        private System.Windows.Forms.TextBox tb_tm;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.ComboBox cb_ClaveEmp;
        private System.Windows.Forms.Button btn_buscar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl_fnomb;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbl_nota2;
        private System.Windows.Forms.TextBox tb_fFactura;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private cfdiDataSet cfdiDataSet;
        private System.Windows.Forms.BindingSource empresaBindingSource;
        private SiscomCFDI.cfdiDataSetTableAdapters.empresaTableAdapter empresaTableAdapter;
        private System.Windows.Forms.BindingSource cfdiDataSetBindingSource;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rb_dolares;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RadioButton rb_pesos;
        private System.Windows.Forms.TextBox tb_cambio;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox cb_webservice;
        private System.Windows.Forms.ComboBox cb_usocfdi;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox gb_impuestos;
        private System.Windows.Forms.DataGridView dg_impuestos;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cb_formapago;
    }
}