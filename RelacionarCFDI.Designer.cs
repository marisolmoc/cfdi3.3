namespace SiscomCFDI
{
    partial class RelacionarCFDI
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RelacionarCFDI));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cb_tipoRelacion = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_agregar = new System.Windows.Forms.Button();
            this.dg_lista_facturas = new System.Windows.Forms.DataGridView();
            this.btn_buscardoc = new System.Windows.Forms.Button();
            this.dg_docRelacionados = new System.Windows.Forms.DataGridView();
            this.btn_aceptar = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_lista_facturas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg_docRelacionados)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cb_tipoRelacion);
            this.groupBox2.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupBox2.Location = new System.Drawing.Point(13, 13);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(832, 74);
            this.groupBox2.TabIndex = 36;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tipo Relación";
            // 
            // cb_tipoRelacion
            // 
            this.cb_tipoRelacion.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_tipoRelacion.FormattingEnabled = true;
            this.cb_tipoRelacion.Location = new System.Drawing.Point(8, 27);
            this.cb_tipoRelacion.Margin = new System.Windows.Forms.Padding(4);
            this.cb_tipoRelacion.Name = "cb_tipoRelacion";
            this.cb_tipoRelacion.Size = new System.Drawing.Size(432, 26);
            this.cb_tipoRelacion.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_agregar);
            this.groupBox1.Controls.Add(this.dg_lista_facturas);
            this.groupBox1.Controls.Add(this.btn_buscardoc);
            this.groupBox1.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold);
            this.groupBox1.Location = new System.Drawing.Point(13, 95);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(832, 324);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Documentos Relacionados";
            // 
            // btn_agregar
            // 
            this.btn_agregar.Enabled = false;
            this.btn_agregar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_agregar.Font = new System.Drawing.Font("Trebuchet MS", 8F);
            this.btn_agregar.Location = new System.Drawing.Point(730, 278);
            this.btn_agregar.Margin = new System.Windows.Forms.Padding(4);
            this.btn_agregar.Name = "btn_agregar";
            this.btn_agregar.Size = new System.Drawing.Size(94, 38);
            this.btn_agregar.TabIndex = 75;
            this.btn_agregar.Text = "Agregar";
            this.btn_agregar.UseVisualStyleBackColor = true;
            this.btn_agregar.Click += new System.EventHandler(this.btn_agregar_Click);
            // 
            // dg_lista_facturas
            // 
            this.dg_lista_facturas.AllowUserToAddRows = false;
            this.dg_lista_facturas.AllowUserToDeleteRows = false;
            this.dg_lista_facturas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dg_lista_facturas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Trebuchet MS", 8F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dg_lista_facturas.DefaultCellStyle = dataGridViewCellStyle1;
            this.dg_lista_facturas.Location = new System.Drawing.Point(8, 68);
            this.dg_lista_facturas.Name = "dg_lista_facturas";
            this.dg_lista_facturas.ReadOnly = true;
            this.dg_lista_facturas.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dg_lista_facturas.RowTemplate.Height = 24;
            this.dg_lista_facturas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_lista_facturas.Size = new System.Drawing.Size(817, 188);
            this.dg_lista_facturas.TabIndex = 40;
            // 
            // btn_buscardoc
            // 
            this.btn_buscardoc.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_buscardoc.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_buscardoc.Location = new System.Drawing.Point(8, 27);
            this.btn_buscardoc.Margin = new System.Windows.Forms.Padding(4);
            this.btn_buscardoc.Name = "btn_buscardoc";
            this.btn_buscardoc.Size = new System.Drawing.Size(151, 28);
            this.btn_buscardoc.TabIndex = 39;
            this.btn_buscardoc.Text = "Buscar Documento";
            this.btn_buscardoc.UseVisualStyleBackColor = true;
            this.btn_buscardoc.Click += new System.EventHandler(this.btn_buscardoc_Click);
            // 
            // dg_docRelacionados
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dg_docRelacionados.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dg_docRelacionados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Trebuchet MS", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dg_docRelacionados.DefaultCellStyle = dataGridViewCellStyle3;
            this.dg_docRelacionados.Location = new System.Drawing.Point(13, 440);
            this.dg_docRelacionados.MultiSelect = false;
            this.dg_docRelacionados.Name = "dg_docRelacionados";
            this.dg_docRelacionados.ReadOnly = true;
            this.dg_docRelacionados.RowTemplate.Height = 24;
            this.dg_docRelacionados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dg_docRelacionados.ShowEditingIcon = false;
            this.dg_docRelacionados.Size = new System.Drawing.Size(832, 188);
            this.dg_docRelacionados.TabIndex = 38;
            // 
            // btn_aceptar
            // 
            this.btn_aceptar.Enabled = false;
            this.btn_aceptar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_aceptar.Location = new System.Drawing.Point(744, 651);
            this.btn_aceptar.Margin = new System.Windows.Forms.Padding(4);
            this.btn_aceptar.Name = "btn_aceptar";
            this.btn_aceptar.Size = new System.Drawing.Size(94, 38);
            this.btn_aceptar.TabIndex = 74;
            this.btn_aceptar.Text = "Aceptar";
            this.btn_aceptar.UseVisualStyleBackColor = true;
            this.btn_aceptar.Click += new System.EventHandler(this.btn_aceptar_Click);
            // 
            // RelacionarCFDI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 702);
            this.Controls.Add(this.btn_aceptar);
            this.Controls.Add(this.dg_docRelacionados);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RelacionarCFDI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relacionar CFDI";
            this.Load += new System.EventHandler(this.RelacionarCFDI_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dg_lista_facturas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg_docRelacionados)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cb_tipoRelacion;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dg_docRelacionados;
        private System.Windows.Forms.Button btn_aceptar;
        private System.Windows.Forms.DataGridView dg_lista_facturas;
        private System.Windows.Forms.Button btn_buscardoc;
        private System.Windows.Forms.Button btn_agregar;
    }
}