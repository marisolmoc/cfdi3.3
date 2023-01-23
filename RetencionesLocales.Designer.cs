namespace SiscomCFDI
{
    partial class RetencionesLocales
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RetencionesLocales));
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.cancel_btn = new System.Windows.Forms.Button();
            this.aceptar_btn = new System.Windows.Forms.Button();
            this.descr_retLocal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.importe_retLocal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tasa_retLocal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.dgv);
            this.panel1.Location = new System.Drawing.Point(-6, -3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(483, 206);
            this.panel1.TabIndex = 0;
            // 
            // dgv
            // 
            this.dgv.AllowUserToDeleteRows = false;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.descr_retLocal,
            this.importe_retLocal,
            this.tasa_retLocal});
            this.dgv.Location = new System.Drawing.Point(17, 14);
            this.dgv.Name = "dgv";
            this.dgv.Size = new System.Drawing.Size(444, 171);
            this.dgv.TabIndex = 0;
            this.dgv.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dgv_UserAddedRow);
            // 
            // cancel_btn
            // 
            this.cancel_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancel_btn.Location = new System.Drawing.Point(382, 209);
            this.cancel_btn.Name = "cancel_btn";
            this.cancel_btn.Size = new System.Drawing.Size(75, 30);
            this.cancel_btn.TabIndex = 1;
            this.cancel_btn.Text = "Cancelar";
            this.cancel_btn.UseVisualStyleBackColor = true;
            this.cancel_btn.Click += new System.EventHandler(this.cancel_btn_Click);
            // 
            // aceptar_btn
            // 
            this.aceptar_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aceptar_btn.Location = new System.Drawing.Point(277, 209);
            this.aceptar_btn.Name = "aceptar_btn";
            this.aceptar_btn.Size = new System.Drawing.Size(75, 30);
            this.aceptar_btn.TabIndex = 2;
            this.aceptar_btn.Text = "Aceptar";
            this.aceptar_btn.UseVisualStyleBackColor = true;
            this.aceptar_btn.Click += new System.EventHandler(this.aceptar_btn_Click);
            // 
            // descr_retLocal
            // 
            this.descr_retLocal.HeaderText = "Descripción";
            this.descr_retLocal.Name = "descr_retLocal";
            this.descr_retLocal.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.descr_retLocal.Width = 200;
            // 
            // importe_retLocal
            // 
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = "0";
            this.importe_retLocal.DefaultCellStyle = dataGridViewCellStyle1;
            this.importe_retLocal.HeaderText = "Importe";
            this.importe_retLocal.Name = "importe_retLocal";
            // 
            // tasa_retLocal
            // 
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = "0";
            this.tasa_retLocal.DefaultCellStyle = dataGridViewCellStyle2;
            this.tasa_retLocal.HeaderText = "Tasa de Retencion %";
            this.tasa_retLocal.Name = "tasa_retLocal";
            this.tasa_retLocal.ToolTipText = "Porcentaje de tasa de retencion ";
            // 
            // RetencionesLocales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(471, 251);
            this.Controls.Add(this.aceptar_btn);
            this.Controls.Add(this.cancel_btn);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RetencionesLocales";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Retenciones Locales";
            this.Load += new System.EventHandler(this.RetencionesLocales_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button cancel_btn;
        private System.Windows.Forms.Button aceptar_btn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descr_retLocal;
        private System.Windows.Forms.DataGridViewTextBoxColumn importe_retLocal;
        private System.Windows.Forms.DataGridViewTextBoxColumn tasa_retLocal;

    }
}