namespace SiscomCFDI
{
    partial class Impresion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Impresion));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_xml = new System.Windows.Forms.TextBox();
            this.btn_xml = new System.Windows.Forms.Button();
            this.btn_pdf = new System.Windows.Forms.Button();
            this.btn_cerrar = new System.Windows.Forms.Button();
            this.cb_ClaveEmp = new System.Windows.Forms.ComboBox();
            this.empresaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cfdiDataSet = new SiscomCFDI.cfdiDataSet();
            this.cfdiDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Clave_lbl = new System.Windows.Forms.Label();
            this.empresaTableAdapter = new SiscomCFDI.cfdiDataSetTableAdapters.empresaTableAdapter();
            this.btn_enviar = new System.Windows.Forms.Button();
            this.lbl_emp = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.empresaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cfdiDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cfdiDataSetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(227, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Agrega el Archivo que deseas imprimir";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Archivo XML:";
            // 
            // tb_xml
            // 
            this.tb_xml.Location = new System.Drawing.Point(92, 84);
            this.tb_xml.Name = "tb_xml";
            this.tb_xml.Size = new System.Drawing.Size(264, 20);
            this.tb_xml.TabIndex = 2;
            // 
            // btn_xml
            // 
            this.btn_xml.Location = new System.Drawing.Point(362, 82);
            this.btn_xml.Name = "btn_xml";
            this.btn_xml.Size = new System.Drawing.Size(75, 23);
            this.btn_xml.TabIndex = 3;
            this.btn_xml.Text = "Buscar";
            this.btn_xml.UseVisualStyleBackColor = true;
            this.btn_xml.Click += new System.EventHandler(this.btn_xml_Click);
            // 
            // btn_pdf
            // 
            this.btn_pdf.Location = new System.Drawing.Point(258, 149);
            this.btn_pdf.Name = "btn_pdf";
            this.btn_pdf.Size = new System.Drawing.Size(75, 30);
            this.btn_pdf.TabIndex = 5;
            this.btn_pdf.Text = "Crear PDF";
            this.btn_pdf.UseVisualStyleBackColor = true;
            this.btn_pdf.Click += new System.EventHandler(this.btn_pdf_Click);
            // 
            // btn_cerrar
            // 
            this.btn_cerrar.Location = new System.Drawing.Point(362, 149);
            this.btn_cerrar.Name = "btn_cerrar";
            this.btn_cerrar.Size = new System.Drawing.Size(75, 30);
            this.btn_cerrar.TabIndex = 6;
            this.btn_cerrar.Text = "Cerrar";
            this.btn_cerrar.UseVisualStyleBackColor = true;
            this.btn_cerrar.Click += new System.EventHandler(this.btn_cerrar_Click);
            // 
            // cb_ClaveEmp
            // 
            this.cb_ClaveEmp.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.empresaBindingSource, "Clave", true));
            this.cb_ClaveEmp.DataSource = this.cfdiDataSetBindingSource;
            this.cb_ClaveEmp.FormattingEnabled = true;
            this.cb_ClaveEmp.Location = new System.Drawing.Point(110, 42);
            this.cb_ClaveEmp.Name = "cb_ClaveEmp";
            this.cb_ClaveEmp.Size = new System.Drawing.Size(47, 21);
            this.cb_ClaveEmp.TabIndex = 22;
            this.cb_ClaveEmp.TextChanged += new System.EventHandler(this.cb_ClaveEmp_TextChanged);
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
            // Clave_lbl
            // 
            this.Clave_lbl.AutoSize = true;
            this.Clave_lbl.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Clave_lbl.Location = new System.Drawing.Point(15, 44);
            this.Clave_lbl.Name = "Clave_lbl";
            this.Clave_lbl.Size = new System.Drawing.Size(89, 16);
            this.Clave_lbl.TabIndex = 21;
            this.Clave_lbl.Text = "Clave Empresa:";
            // 
            // empresaTableAdapter
            // 
            this.empresaTableAdapter.ClearBeforeFill = true;
            // 
            // btn_enviar
            // 
            this.btn_enviar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_enviar.Enabled = false;
            this.btn_enviar.Location = new System.Drawing.Point(15, 149);
            this.btn_enviar.Name = "btn_enviar";
            this.btn_enviar.Size = new System.Drawing.Size(85, 30);
            this.btn_enviar.TabIndex = 34;
            this.btn_enviar.Text = "Enviar Correo";
            this.btn_enviar.UseVisualStyleBackColor = true;
            this.btn_enviar.Click += new System.EventHandler(this.btn_enviar_Click);
            // 
            // lbl_emp
            // 
            this.lbl_emp.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_emp.Location = new System.Drawing.Point(163, 42);
            this.lbl_emp.Name = "lbl_emp";
            this.lbl_emp.Size = new System.Drawing.Size(274, 21);
            this.lbl_emp.TabIndex = 35;
            // 
            // Impresion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 197);
            this.Controls.Add(this.lbl_emp);
            this.Controls.Add(this.btn_enviar);
            this.Controls.Add(this.cb_ClaveEmp);
            this.Controls.Add(this.Clave_lbl);
            this.Controls.Add(this.btn_cerrar);
            this.Controls.Add(this.btn_pdf);
            this.Controls.Add(this.btn_xml);
            this.Controls.Add(this.tb_xml);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Impresion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Impresión";
            this.Load += new System.EventHandler(this.Impresion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.empresaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cfdiDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cfdiDataSetBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_xml;
        private System.Windows.Forms.Button btn_xml;
        private System.Windows.Forms.Button btn_pdf;
        private System.Windows.Forms.Button btn_cerrar;
        public System.Windows.Forms.ComboBox cb_ClaveEmp;
        private System.Windows.Forms.Label Clave_lbl;
        private cfdiDataSet cfdiDataSet;
        private System.Windows.Forms.BindingSource empresaBindingSource;
        private SiscomCFDI.cfdiDataSetTableAdapters.empresaTableAdapter empresaTableAdapter;
        private System.Windows.Forms.BindingSource cfdiDataSetBindingSource;
        private System.Windows.Forms.Button btn_enviar;
        private System.Windows.Forms.Label lbl_emp;
    }
}