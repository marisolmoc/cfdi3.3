namespace SiscomCFDI
{
    partial class RptMensual
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RptMensual));
            this.dp_fecha = new System.Windows.Forms.DateTimePicker();
            this.cb_ClaveEmp = new System.Windows.Forms.ComboBox();
            this.Clave_lbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_crear = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_emp = new System.Windows.Forms.Label();
            this.btn_preview = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dp_fecha
            // 
            this.dp_fecha.CustomFormat = "MM/yyyy";
            this.dp_fecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dp_fecha.Location = new System.Drawing.Point(114, 61);
            this.dp_fecha.Name = "dp_fecha";
            this.dp_fecha.Size = new System.Drawing.Size(87, 20);
            this.dp_fecha.TabIndex = 1;
            // 
            // cb_ClaveEmp
            // 
            this.cb_ClaveEmp.FormattingEnabled = true;
            this.cb_ClaveEmp.Location = new System.Drawing.Point(114, 29);
            this.cb_ClaveEmp.Name = "cb_ClaveEmp";
            this.cb_ClaveEmp.Size = new System.Drawing.Size(50, 21);
            this.cb_ClaveEmp.TabIndex = 24;
            this.cb_ClaveEmp.TextChanged += new System.EventHandler(this.cb_ClaveEmp_TextChanged);
            // 
            // Clave_lbl
            // 
            this.Clave_lbl.AutoSize = true;
            this.Clave_lbl.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Clave_lbl.Location = new System.Drawing.Point(22, 31);
            this.Clave_lbl.Name = "Clave_lbl";
            this.Clave_lbl.Size = new System.Drawing.Size(89, 16);
            this.Clave_lbl.TabIndex = 23;
            this.Clave_lbl.Text = "Clave Empresa:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(63, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 16);
            this.label1.TabIndex = 25;
            this.label1.Text = "Fecha:";
            // 
            // btn_crear
            // 
            this.btn_crear.Location = new System.Drawing.Point(159, 99);
            this.btn_crear.Name = "btn_crear";
            this.btn_crear.Size = new System.Drawing.Size(75, 23);
            this.btn_crear.TabIndex = 27;
            this.btn_crear.Text = "Crear";
            this.btn_crear.UseVisualStyleBackColor = true;
            this.btn_crear.Click += new System.EventHandler(this.btn_crear_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(236, 35);
            this.label2.TabIndex = 29;
            this.label2.Text = "Seleccione la empresa y la fecha para generar el reporte mensual.";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btn_preview);
            this.panel1.Controls.Add(this.lbl_emp);
            this.panel1.Controls.Add(this.Clave_lbl);
            this.panel1.Controls.Add(this.dp_fecha);
            this.panel1.Controls.Add(this.cb_ClaveEmp);
            this.panel1.Controls.Add(this.btn_crear);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(-2, 47);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 138);
            this.panel1.TabIndex = 30;
            // 
            // lbl_emp
            // 
            this.lbl_emp.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_emp.Location = new System.Drawing.Point(3, 8);
            this.lbl_emp.Name = "lbl_emp";
            this.lbl_emp.Size = new System.Drawing.Size(242, 16);
            this.lbl_emp.TabIndex = 29;
            this.lbl_emp.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btn_preview
            // 
            this.btn_preview.Location = new System.Drawing.Point(13, 99);
            this.btn_preview.Name = "btn_preview";
            this.btn_preview.Size = new System.Drawing.Size(75, 23);
            this.btn_preview.TabIndex = 30;
            this.btn_preview.Text = "Vista Previa";
            this.btn_preview.UseVisualStyleBackColor = true;
            this.btn_preview.Click += new System.EventHandler(this.btn_preview_Click);
            // 
            // RptMensual
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(245, 182);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "RptMensual";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reporte Mensual CFD";
            this.Load += new System.EventHandler(this.RptMensual_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dp_fecha;
        public System.Windows.Forms.ComboBox cb_ClaveEmp;
        private System.Windows.Forms.Label Clave_lbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_crear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_emp;
        private System.Windows.Forms.Button btn_preview;
    }
}