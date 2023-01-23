namespace SiscomCFDI
{
    partial class Pagos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pagos));
            this.lbl_emp = new System.Windows.Forms.Label();
            this.cb_ClaveEmp = new System.Windows.Forms.ComboBox();
            this.Clave_lbl = new System.Windows.Forms.Label();
            this.btn_search = new System.Windows.Forms.Button();
            this.tb_xml = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_webservice = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dg_pagos = new System.Windows.Forms.DataGridView();
            this.btn_agregar = new System.Windows.Forms.Button();
            this.tb_saldo_insoluto = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.tb_imp_pagado = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.tb_saldo_anterior = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.tb_num_parcialidad = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.tb_metodoPagoR = new System.Windows.Forms.TextBox();
            this.tb_tipoCambioR = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.tb_monedaR = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.tb_folioR = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.tb_serieR = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.tb_uuid = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtp_fechaPago = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btn_mas = new System.Windows.Forms.Button();
            this.cb_moneda = new System.Windows.Forms.ComboBox();
            this.tb_tipoCambio = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.tb_num_oper = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_monto = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_formapago = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tb_serie = new System.Windows.Forms.TextBox();
            this.tb_folio = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.btn_enviar = new System.Windows.Forms.Button();
            this.imprimir_btn = new System.Windows.Forms.Button();
            this.btn_timbrar = new System.Windows.Forms.Button();
            this.btn_fSalir = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_pagos)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_emp
            // 
            this.lbl_emp.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_emp.Location = new System.Drawing.Point(429, 9);
            this.lbl_emp.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_emp.Name = "lbl_emp";
            this.lbl_emp.Size = new System.Drawing.Size(449, 27);
            this.lbl_emp.TabIndex = 41;
            this.lbl_emp.Text = ":";
            // 
            // cb_ClaveEmp
            // 
            this.cb_ClaveEmp.FormattingEnabled = true;
            this.cb_ClaveEmp.Location = new System.Drawing.Point(100, 9);
            this.cb_ClaveEmp.Margin = new System.Windows.Forms.Padding(4);
            this.cb_ClaveEmp.Name = "cb_ClaveEmp";
            this.cb_ClaveEmp.Size = new System.Drawing.Size(61, 24);
            this.cb_ClaveEmp.TabIndex = 40;
            this.cb_ClaveEmp.TextChanged += new System.EventHandler(this.cb_ClaveEmp_TextChanged);
            // 
            // Clave_lbl
            // 
            this.Clave_lbl.AutoSize = true;
            this.Clave_lbl.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Clave_lbl.Location = new System.Drawing.Point(16, 11);
            this.Clave_lbl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Clave_lbl.Name = "Clave_lbl";
            this.Clave_lbl.Size = new System.Drawing.Size(68, 18);
            this.Clave_lbl.TabIndex = 39;
            this.Clave_lbl.Text = "Empresa:";
            // 
            // btn_search
            // 
            this.btn_search.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_search.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_search.Location = new System.Drawing.Point(407, 26);
            this.btn_search.Margin = new System.Windows.Forms.Padding(4);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(100, 28);
            this.btn_search.TabIndex = 38;
            this.btn_search.Text = "Buscar";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // tb_xml
            // 
            this.tb_xml.Font = new System.Drawing.Font("Trebuchet MS", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_xml.Location = new System.Drawing.Point(104, 26);
            this.tb_xml.Margin = new System.Windows.Forms.Padding(4);
            this.tb_xml.Name = "tb_xml";
            this.tb_xml.Size = new System.Drawing.Size(293, 23);
            this.tb_xml.TabIndex = 37;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(23, 26);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 25);
            this.label2.TabIndex = 36;
            this.label2.Text = "Factura:";
            // 
            // cb_webservice
            // 
            this.cb_webservice.AutoSize = true;
            this.cb_webservice.Location = new System.Drawing.Point(893, 12);
            this.cb_webservice.Margin = new System.Windows.Forms.Padding(4);
            this.cb_webservice.Name = "cb_webservice";
            this.cb_webservice.Size = new System.Drawing.Size(18, 17);
            this.cb_webservice.TabIndex = 42;
            this.cb_webservice.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Location = new System.Drawing.Point(-4, 42);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(937, 570);
            this.panel1.TabIndex = 43;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dg_pagos);
            this.groupBox2.Controls.Add(this.btn_agregar);
            this.groupBox2.Controls.Add(this.tb_saldo_insoluto);
            this.groupBox2.Controls.Add(this.label27);
            this.groupBox2.Controls.Add(this.tb_imp_pagado);
            this.groupBox2.Controls.Add(this.label26);
            this.groupBox2.Controls.Add(this.tb_saldo_anterior);
            this.groupBox2.Controls.Add(this.label22);
            this.groupBox2.Controls.Add(this.tb_num_parcialidad);
            this.groupBox2.Controls.Add(this.label25);
            this.groupBox2.Controls.Add(this.tb_metodoPagoR);
            this.groupBox2.Controls.Add(this.tb_tipoCambioR);
            this.groupBox2.Controls.Add(this.label24);
            this.groupBox2.Controls.Add(this.tb_monedaR);
            this.groupBox2.Controls.Add(this.label21);
            this.groupBox2.Controls.Add(this.btn_search);
            this.groupBox2.Controls.Add(this.tb_xml);
            this.groupBox2.Controls.Add(this.label23);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.tb_folioR);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.tb_serieR);
            this.groupBox2.Controls.Add(this.label20);
            this.groupBox2.Controls.Add(this.tb_uuid);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(17, 17);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(889, 382);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Documentos Relacionados";
            // 
            // dg_pagos
            // 
            this.dg_pagos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dg_pagos.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dg_pagos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_pagos.Location = new System.Drawing.Point(21, 199);
            this.dg_pagos.Margin = new System.Windows.Forms.Padding(4);
            this.dg_pagos.Name = "dg_pagos";
            this.dg_pagos.ReadOnly = true;
            this.dg_pagos.RowTemplate.Height = 18;
            this.dg_pagos.RowTemplate.ReadOnly = true;
            this.dg_pagos.Size = new System.Drawing.Size(845, 169);
            this.dg_pagos.TabIndex = 69;
            // 
            // btn_agregar
            // 
            this.btn_agregar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_agregar.Font = new System.Drawing.Font("Trebuchet MS", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_agregar.Location = new System.Drawing.Point(767, 151);
            this.btn_agregar.Margin = new System.Windows.Forms.Padding(4);
            this.btn_agregar.Name = "btn_agregar";
            this.btn_agregar.Size = new System.Drawing.Size(100, 28);
            this.btn_agregar.TabIndex = 68;
            this.btn_agregar.Text = "Agregar";
            this.btn_agregar.UseVisualStyleBackColor = true;
            this.btn_agregar.Click += new System.EventHandler(this.btn_agregar_Click);
            // 
            // tb_saldo_insoluto
            // 
            this.tb_saldo_insoluto.Font = new System.Drawing.Font("Trebuchet MS", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_saldo_insoluto.Location = new System.Drawing.Point(477, 155);
            this.tb_saldo_insoluto.Margin = new System.Windows.Forms.Padding(4);
            this.tb_saldo_insoluto.Name = "tb_saldo_insoluto";
            this.tb_saldo_insoluto.Size = new System.Drawing.Size(123, 23);
            this.tb_saldo_insoluto.TabIndex = 67;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(473, 132);
            this.label27.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(101, 18);
            this.label27.TabIndex = 66;
            this.label27.Text = "Saldo insoluto:";
            // 
            // tb_imp_pagado
            // 
            this.tb_imp_pagado.Font = new System.Drawing.Font("Trebuchet MS", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_imp_pagado.Location = new System.Drawing.Point(337, 155);
            this.tb_imp_pagado.Margin = new System.Windows.Forms.Padding(4);
            this.tb_imp_pagado.Name = "tb_imp_pagado";
            this.tb_imp_pagado.Size = new System.Drawing.Size(116, 23);
            this.tb_imp_pagado.TabIndex = 65;
            this.tb_imp_pagado.TextChanged += new System.EventHandler(this.tb_imp_pagado_TextChanged);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(333, 132);
            this.label26.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(112, 18);
            this.label26.TabIndex = 64;
            this.label26.Text = "Importe pagado:";
            // 
            // tb_saldo_anterior
            // 
            this.tb_saldo_anterior.Font = new System.Drawing.Font("Trebuchet MS", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_saldo_anterior.Location = new System.Drawing.Point(196, 155);
            this.tb_saldo_anterior.Margin = new System.Windows.Forms.Padding(4);
            this.tb_saldo_anterior.Name = "tb_saldo_anterior";
            this.tb_saldo_anterior.Size = new System.Drawing.Size(120, 23);
            this.tb_saldo_anterior.TabIndex = 63;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(192, 132);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(101, 18);
            this.label22.TabIndex = 62;
            this.label22.Text = "Saldo anterior:";
            // 
            // tb_num_parcialidad
            // 
            this.tb_num_parcialidad.Font = new System.Drawing.Font("Trebuchet MS", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_num_parcialidad.Location = new System.Drawing.Point(21, 155);
            this.tb_num_parcialidad.Margin = new System.Windows.Forms.Padding(4);
            this.tb_num_parcialidad.Name = "tb_num_parcialidad";
            this.tb_num_parcialidad.Size = new System.Drawing.Size(129, 23);
            this.tb_num_parcialidad.TabIndex = 61;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(20, 132);
            this.label25.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(154, 18);
            this.label25.TabIndex = 60;
            this.label25.Text = "Número de parcialidad:";
            // 
            // tb_metodoPagoR
            // 
            this.tb_metodoPagoR.Enabled = false;
            this.tb_metodoPagoR.Font = new System.Drawing.Font("Trebuchet MS", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_metodoPagoR.Location = new System.Drawing.Point(641, 92);
            this.tb_metodoPagoR.Margin = new System.Windows.Forms.Padding(4);
            this.tb_metodoPagoR.Name = "tb_metodoPagoR";
            this.tb_metodoPagoR.Size = new System.Drawing.Size(224, 23);
            this.tb_metodoPagoR.TabIndex = 59;
            // 
            // tb_tipoCambioR
            // 
            this.tb_tipoCambioR.Enabled = false;
            this.tb_tipoCambioR.Font = new System.Drawing.Font("Trebuchet MS", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_tipoCambioR.Location = new System.Drawing.Point(511, 92);
            this.tb_tipoCambioR.Margin = new System.Windows.Forms.Padding(4);
            this.tb_tipoCambioR.Name = "tb_tipoCambioR";
            this.tb_tipoCambioR.Size = new System.Drawing.Size(111, 23);
            this.tb_tipoCambioR.TabIndex = 58;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(637, 68);
            this.label24.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(116, 18);
            this.label24.TabIndex = 57;
            this.label24.Text = "Método de pago:";
            // 
            // tb_monedaR
            // 
            this.tb_monedaR.Enabled = false;
            this.tb_monedaR.Font = new System.Drawing.Font("Trebuchet MS", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_monedaR.Location = new System.Drawing.Point(431, 92);
            this.tb_monedaR.Margin = new System.Windows.Forms.Padding(4);
            this.tb_monedaR.Name = "tb_monedaR";
            this.tb_monedaR.Size = new System.Drawing.Size(61, 23);
            this.tb_monedaR.TabIndex = 53;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(507, 68);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(110, 18);
            this.label21.TabIndex = 56;
            this.label21.Text = "Tipo de Cambio:";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(427, 69);
            this.label23.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(63, 18);
            this.label23.TabIndex = 54;
            this.label23.Text = "Moneda:";
            // 
            // tb_folioR
            // 
            this.tb_folioR.Enabled = false;
            this.tb_folioR.Font = new System.Drawing.Font("Trebuchet MS", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_folioR.Location = new System.Drawing.Point(347, 92);
            this.tb_folioR.Margin = new System.Windows.Forms.Padding(4);
            this.tb_folioR.Name = "tb_folioR";
            this.tb_folioR.Size = new System.Drawing.Size(56, 23);
            this.tb_folioR.TabIndex = 51;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(343, 69);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(43, 18);
            this.label19.TabIndex = 50;
            this.label19.Text = "Folio:";
            // 
            // tb_serieR
            // 
            this.tb_serieR.Enabled = false;
            this.tb_serieR.Font = new System.Drawing.Font("Trebuchet MS", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_serieR.Location = new System.Drawing.Point(277, 91);
            this.tb_serieR.Margin = new System.Windows.Forms.Padding(4);
            this.tb_serieR.Name = "tb_serieR";
            this.tb_serieR.Size = new System.Drawing.Size(37, 23);
            this.tb_serieR.TabIndex = 49;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(273, 69);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(45, 18);
            this.label20.TabIndex = 48;
            this.label20.Text = "Serie:";
            // 
            // tb_uuid
            // 
            this.tb_uuid.Enabled = false;
            this.tb_uuid.Font = new System.Drawing.Font("Trebuchet MS", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_uuid.Location = new System.Drawing.Point(24, 91);
            this.tb_uuid.Margin = new System.Windows.Forms.Padding(4);
            this.tb_uuid.Name = "tb_uuid";
            this.tb_uuid.Size = new System.Drawing.Size(236, 23);
            this.tb_uuid.TabIndex = 14;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(23, 68);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(124, 18);
            this.label17.TabIndex = 13;
            this.label17.Text = "Id del documento:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtp_fechaPago);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.btn_mas);
            this.groupBox1.Controls.Add(this.cb_moneda);
            this.groupBox1.Controls.Add(this.tb_tipoCambio);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.tb_num_oper);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tb_monto);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cb_formapago);
            this.groupBox1.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(17, 406);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(889, 154);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Recepción de Págos";
            // 
            // dtp_fechaPago
            // 
            this.dtp_fechaPago.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtp_fechaPago.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtp_fechaPago.Location = new System.Drawing.Point(196, 113);
            this.dtp_fechaPago.MaxDate = new System.DateTime(2019, 7, 25, 0, 0, 0, 0);
            this.dtp_fechaPago.MinDate = new System.DateTime(2012, 1, 1, 0, 0, 0, 0);
            this.dtp_fechaPago.Name = "dtp_fechaPago";
            this.dtp_fechaPago.Size = new System.Drawing.Size(138, 23);
            this.dtp_fechaPago.TabIndex = 25;
            this.dtp_fechaPago.Value = new System.DateTime(2019, 7, 25, 0, 0, 0, 0);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Firebrick;
            this.label7.Location = new System.Drawing.Point(296, 90);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 20);
            this.label7.TabIndex = 24;
            this.label7.Text = "*";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(193, 91);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 18);
            this.label8.TabIndex = 23;
            this.label8.Text = "Fecha de Pago:";
            // 
            // btn_mas
            // 
            this.btn_mas.Location = new System.Drawing.Point(12, 91);
            this.btn_mas.Margin = new System.Windows.Forms.Padding(4);
            this.btn_mas.Name = "btn_mas";
            this.btn_mas.Size = new System.Drawing.Size(100, 49);
            this.btn_mas.TabIndex = 22;
            this.btn_mas.Text = "Mas Opciones";
            this.btn_mas.UseVisualStyleBackColor = true;
            this.btn_mas.Click += new System.EventHandler(this.btn_mas_Click);
            // 
            // cb_moneda
            // 
            this.cb_moneda.Font = new System.Drawing.Font("Trebuchet MS", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_moneda.FormattingEnabled = true;
            this.cb_moneda.Items.AddRange(new object[] {
            "MXN",
            "USD"});
            this.cb_moneda.Location = new System.Drawing.Point(196, 55);
            this.cb_moneda.Margin = new System.Windows.Forms.Padding(4);
            this.cb_moneda.Name = "cb_moneda";
            this.cb_moneda.Size = new System.Drawing.Size(87, 26);
            this.cb_moneda.TabIndex = 8;
            this.cb_moneda.SelectedIndexChanged += new System.EventHandler(this.cb_moneda_SelectedIndexChanged);
            // 
            // tb_tipoCambio
            // 
            this.tb_tipoCambio.Enabled = false;
            this.tb_tipoCambio.Font = new System.Drawing.Font("Trebuchet MS", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_tipoCambio.Location = new System.Drawing.Point(315, 55);
            this.tb_tipoCambio.Margin = new System.Windows.Forms.Padding(4);
            this.tb_tipoCambio.Name = "tb_tipoCambio";
            this.tb_tipoCambio.Size = new System.Drawing.Size(97, 23);
            this.tb_tipoCambio.TabIndex = 9;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(311, 32);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(110, 18);
            this.label14.TabIndex = 21;
            this.label14.Text = "Tipo de Cambio:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Firebrick;
            this.label12.Location = new System.Drawing.Point(252, 31);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(15, 20);
            this.label12.TabIndex = 20;
            this.label12.Text = "*";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(192, 32);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(63, 18);
            this.label13.TabIndex = 19;
            this.label13.Text = "Moneda:";
            // 
            // tb_num_oper
            // 
            this.tb_num_oper.Font = new System.Drawing.Font("Trebuchet MS", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_num_oper.Location = new System.Drawing.Point(12, 55);
            this.tb_num_oper.Margin = new System.Windows.Forms.Padding(4);
            this.tb_num_oper.Name = "tb_num_oper";
            this.tb_num_oper.Size = new System.Drawing.Size(109, 23);
            this.tb_num_oper.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 34);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(149, 18);
            this.label6.TabIndex = 7;
            this.label6.Text = "Número de operación:";
            // 
            // tb_monto
            // 
            this.tb_monto.Enabled = false;
            this.tb_monto.Font = new System.Drawing.Font("Trebuchet MS", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_monto.Location = new System.Drawing.Point(456, 55);
            this.tb_monto.Margin = new System.Windows.Forms.Padding(4);
            this.tb_monto.Name = "tb_monto";
            this.tb_monto.Size = new System.Drawing.Size(92, 23);
            this.tb_monto.TabIndex = 6;
            this.tb_monto.TextChanged += new System.EventHandler(this.tb_monto_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Firebrick;
            this.label4.Location = new System.Drawing.Point(503, 30);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(15, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "*";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(452, 32);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 18);
            this.label5.TabIndex = 4;
            this.label5.Text = "Monto:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Firebrick;
            this.label3.Location = new System.Drawing.Point(678, 32);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(575, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Forma de pago:";
            // 
            // cb_formapago
            // 
            this.cb_formapago.Font = new System.Drawing.Font("Trebuchet MS", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_formapago.FormattingEnabled = true;
            this.cb_formapago.Location = new System.Drawing.Point(573, 55);
            this.cb_formapago.Margin = new System.Windows.Forms.Padding(4);
            this.cb_formapago.Name = "cb_formapago";
            this.cb_formapago.Size = new System.Drawing.Size(299, 26);
            this.cb_formapago.TabIndex = 5;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(185, 11);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(46, 18);
            this.label15.TabIndex = 44;
            this.label15.Text = "Serie:";
            // 
            // tb_serie
            // 
            this.tb_serie.Location = new System.Drawing.Point(247, 9);
            this.tb_serie.Margin = new System.Windows.Forms.Padding(4);
            this.tb_serie.Name = "tb_serie";
            this.tb_serie.Size = new System.Drawing.Size(37, 22);
            this.tb_serie.TabIndex = 45;
            this.tb_serie.Text = "PA";
            // 
            // tb_folio
            // 
            this.tb_folio.Location = new System.Drawing.Point(363, 9);
            this.tb_folio.Margin = new System.Windows.Forms.Padding(4);
            this.tb_folio.Name = "tb_folio";
            this.tb_folio.Size = new System.Drawing.Size(57, 22);
            this.tb_folio.TabIndex = 47;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(305, 11);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(44, 18);
            this.label16.TabIndex = 46;
            this.label16.Text = "Folio:";
            // 
            // btn_enviar
            // 
            this.btn_enviar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_enviar.Enabled = false;
            this.btn_enviar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_enviar.Location = new System.Drawing.Point(189, 626);
            this.btn_enviar.Margin = new System.Windows.Forms.Padding(4);
            this.btn_enviar.Name = "btn_enviar";
            this.btn_enviar.Size = new System.Drawing.Size(113, 37);
            this.btn_enviar.TabIndex = 49;
            this.btn_enviar.Text = "Enviar Correo";
            this.btn_enviar.UseVisualStyleBackColor = true;
            this.btn_enviar.Click += new System.EventHandler(this.btn_enviar_Click);
            // 
            // imprimir_btn
            // 
            this.imprimir_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.imprimir_btn.Enabled = false;
            this.imprimir_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.imprimir_btn.Location = new System.Drawing.Point(20, 626);
            this.imprimir_btn.Margin = new System.Windows.Forms.Padding(4);
            this.imprimir_btn.Name = "imprimir_btn";
            this.imprimir_btn.Size = new System.Drawing.Size(92, 37);
            this.imprimir_btn.TabIndex = 48;
            this.imprimir_btn.Text = "Crear PDF";
            this.imprimir_btn.UseVisualStyleBackColor = true;
            this.imprimir_btn.Click += new System.EventHandler(this.imprimir_btn_Click);
            // 
            // btn_timbrar
            // 
            this.btn_timbrar.Enabled = false;
            this.btn_timbrar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_timbrar.Location = new System.Drawing.Point(603, 626);
            this.btn_timbrar.Margin = new System.Windows.Forms.Padding(4);
            this.btn_timbrar.Name = "btn_timbrar";
            this.btn_timbrar.Size = new System.Drawing.Size(121, 37);
            this.btn_timbrar.TabIndex = 51;
            this.btn_timbrar.Text = "Timbrar";
            this.btn_timbrar.UseVisualStyleBackColor = true;
            this.btn_timbrar.Click += new System.EventHandler(this.btn_timbrar_Click);
            // 
            // btn_fSalir
            // 
            this.btn_fSalir.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_fSalir.Location = new System.Drawing.Point(807, 626);
            this.btn_fSalir.Margin = new System.Windows.Forms.Padding(4);
            this.btn_fSalir.Name = "btn_fSalir";
            this.btn_fSalir.Size = new System.Drawing.Size(100, 37);
            this.btn_fSalir.TabIndex = 50;
            this.btn_fSalir.Text = "Salir";
            this.btn_fSalir.UseVisualStyleBackColor = true;
            this.btn_fSalir.Click += new System.EventHandler(this.btn_fSalir_Click);
            // 
            // Pagos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 681);
            this.Controls.Add(this.btn_timbrar);
            this.Controls.Add(this.btn_fSalir);
            this.Controls.Add(this.btn_enviar);
            this.Controls.Add(this.imprimir_btn);
            this.Controls.Add(this.tb_folio);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.tb_serie);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cb_webservice);
            this.Controls.Add(this.lbl_emp);
            this.Controls.Add(this.cb_ClaveEmp);
            this.Controls.Add(this.Clave_lbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Pagos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recepción de Págos";
            this.Load += new System.EventHandler(this.Pagos_Load);
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_pagos)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_emp;
        public System.Windows.Forms.ComboBox cb_ClaveEmp;
        private System.Windows.Forms.Label Clave_lbl;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.TextBox tb_xml;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cb_webservice;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cb_moneda;
        private System.Windows.Forms.TextBox tb_tipoCambio;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tb_num_oper;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_monto;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_formapago;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tb_serie;
        private System.Windows.Forms.TextBox tb_folio;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox tb_monedaR;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox tb_folioR;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox tb_serieR;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox tb_uuid;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox tb_saldo_insoluto;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox tb_imp_pagado;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox tb_saldo_anterior;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox tb_num_parcialidad;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox tb_metodoPagoR;
        private System.Windows.Forms.TextBox tb_tipoCambioR;
        private System.Windows.Forms.Button btn_enviar;
        private System.Windows.Forms.Button imprimir_btn;
        private System.Windows.Forms.Button btn_timbrar;
        private System.Windows.Forms.Button btn_fSalir;
        private System.Windows.Forms.Button btn_agregar;
        private System.Windows.Forms.DataGridView dg_pagos;
        private System.Windows.Forms.Button btn_mas;
        private System.Windows.Forms.DateTimePicker dtp_fechaPago;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}