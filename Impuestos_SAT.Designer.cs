namespace SiscomCFDI
{
    partial class Impuestos_SAT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Impuestos_SAT));
            this.rb_retenciones = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.rb_traslado = new System.Windows.Forms.RadioButton();
            this.gb_retencion = new System.Windows.Forms.GroupBox();
            this.cb_retTasa = new System.Windows.Forms.ComboBox();
            this.btn_addRetencion = new System.Windows.Forms.Button();
            this.tb_retImporte = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cb_retImpuesto = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_retBase = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gb_traslado = new System.Windows.Forms.GroupBox();
            this.cb_trasTasa = new System.Windows.Forms.ComboBox();
            this.btn_addTraslado = new System.Windows.Forms.Button();
            this.tb_trasImporte = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cb_trasImpuesto = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tb_trasBase = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dg_impuestos = new System.Windows.Forms.DataGridView();
            this.btn_aceptar = new System.Windows.Forms.Button();
            this.gb_retencion.SuspendLayout();
            this.gb_traslado.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_impuestos)).BeginInit();
            this.SuspendLayout();
            // 
            // rb_retenciones
            // 
            this.rb_retenciones.AutoSize = true;
            this.rb_retenciones.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_retenciones.Location = new System.Drawing.Point(3, 6);
            this.rb_retenciones.Name = "rb_retenciones";
            this.rb_retenciones.Size = new System.Drawing.Size(83, 22);
            this.rb_retenciones.TabIndex = 2;
            this.rb_retenciones.Text = "Retención";
            this.rb_retenciones.UseVisualStyleBackColor = true;
            this.rb_retenciones.CheckedChanged += new System.EventHandler(this.rb_tipo_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tipo";
            // 
            // rb_traslado
            // 
            this.rb_traslado.AutoSize = true;
            this.rb_traslado.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rb_traslado.Location = new System.Drawing.Point(92, 6);
            this.rb_traslado.Name = "rb_traslado";
            this.rb_traslado.Size = new System.Drawing.Size(71, 22);
            this.rb_traslado.TabIndex = 3;
            this.rb_traslado.Text = "Traslado";
            this.rb_traslado.UseVisualStyleBackColor = true;
            this.rb_traslado.CheckedChanged += new System.EventHandler(this.rb_tipo_CheckedChanged);
            // 
            // gb_retencion
            // 
            this.gb_retencion.BackColor = System.Drawing.Color.Transparent;
            this.gb_retencion.Controls.Add(this.cb_retTasa);
            this.gb_retencion.Controls.Add(this.btn_addRetencion);
            this.gb_retencion.Controls.Add(this.tb_retImporte);
            this.gb_retencion.Controls.Add(this.label5);
            this.gb_retencion.Controls.Add(this.label4);
            this.gb_retencion.Controls.Add(this.cb_retImpuesto);
            this.gb_retencion.Controls.Add(this.label3);
            this.gb_retencion.Controls.Add(this.tb_retBase);
            this.gb_retencion.Controls.Add(this.label2);
            this.gb_retencion.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb_retencion.Location = new System.Drawing.Point(12, 70);
            this.gb_retencion.Name = "gb_retencion";
            this.gb_retencion.Size = new System.Drawing.Size(496, 102);
            this.gb_retencion.TabIndex = 0;
            this.gb_retencion.TabStop = false;
            this.gb_retencion.Text = "Retención";
            this.gb_retencion.Visible = false;
            // 
            // cb_retTasa
            // 
            this.cb_retTasa.FormattingEnabled = true;
            this.cb_retTasa.Location = new System.Drawing.Point(49, 61);
            this.cb_retTasa.Name = "cb_retTasa";
            this.cb_retTasa.Size = new System.Drawing.Size(100, 26);
            this.cb_retTasa.TabIndex = 9;
            this.cb_retTasa.SelectedIndexChanged += new System.EventHandler(this.cb_retTasa_SelectedIndexChanged);
            // 
            // btn_addRetencion
            // 
            this.btn_addRetencion.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_addRetencion.Location = new System.Drawing.Point(415, 70);
            this.btn_addRetencion.Name = "btn_addRetencion";
            this.btn_addRetencion.Size = new System.Drawing.Size(75, 26);
            this.btn_addRetencion.TabIndex = 8;
            this.btn_addRetencion.Text = "Agregar";
            this.btn_addRetencion.UseVisualStyleBackColor = true;
            this.btn_addRetencion.Click += new System.EventHandler(this.btn_addRetencion_Click);
            // 
            // tb_retImporte
            // 
            this.tb_retImporte.Location = new System.Drawing.Point(235, 61);
            this.tb_retImporte.Name = "tb_retImporte";
            this.tb_retImporte.ReadOnly = true;
            this.tb_retImporte.Size = new System.Drawing.Size(100, 21);
            this.tb_retImporte.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(167, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 18);
            this.label5.TabIndex = 6;
            this.label5.Text = "Importe:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 18);
            this.label4.TabIndex = 4;
            this.label4.Text = "Tasa:";
            // 
            // cb_retImpuesto
            // 
            this.cb_retImpuesto.FormattingEnabled = true;
            this.cb_retImpuesto.Location = new System.Drawing.Point(235, 24);
            this.cb_retImpuesto.Name = "cb_retImpuesto";
            this.cb_retImpuesto.Size = new System.Drawing.Size(100, 26);
            this.cb_retImpuesto.TabIndex = 3;
            this.cb_retImpuesto.SelectedIndexChanged += new System.EventHandler(this.cb_retImpuesto_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(167, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "Impuesto:";
            // 
            // tb_retBase
            // 
            this.tb_retBase.Location = new System.Drawing.Point(49, 24);
            this.tb_retBase.Name = "tb_retBase";
            this.tb_retBase.Size = new System.Drawing.Size(100, 21);
            this.tb_retBase.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 18);
            this.label2.TabIndex = 0;
            this.label2.Text = "Base:";
            // 
            // gb_traslado
            // 
            this.gb_traslado.BackColor = System.Drawing.Color.Transparent;
            this.gb_traslado.Controls.Add(this.cb_trasTasa);
            this.gb_traslado.Controls.Add(this.btn_addTraslado);
            this.gb_traslado.Controls.Add(this.tb_trasImporte);
            this.gb_traslado.Controls.Add(this.label6);
            this.gb_traslado.Controls.Add(this.label7);
            this.gb_traslado.Controls.Add(this.cb_trasImpuesto);
            this.gb_traslado.Controls.Add(this.label8);
            this.gb_traslado.Controls.Add(this.tb_trasBase);
            this.gb_traslado.Controls.Add(this.label9);
            this.gb_traslado.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gb_traslado.Location = new System.Drawing.Point(12, 70);
            this.gb_traslado.Name = "gb_traslado";
            this.gb_traslado.Size = new System.Drawing.Size(496, 102);
            this.gb_traslado.TabIndex = 1;
            this.gb_traslado.TabStop = false;
            this.gb_traslado.Text = "Traslado";
            this.gb_traslado.Visible = false;
            // 
            // cb_trasTasa
            // 
            this.cb_trasTasa.FormattingEnabled = true;
            this.cb_trasTasa.Location = new System.Drawing.Point(49, 61);
            this.cb_trasTasa.Name = "cb_trasTasa";
            this.cb_trasTasa.Size = new System.Drawing.Size(100, 26);
            this.cb_trasTasa.TabIndex = 9;
            this.cb_trasTasa.SelectedIndexChanged += new System.EventHandler(this.cb_trasTasa_SelectedIndexChanged);
            // 
            // btn_addTraslado
            // 
            this.btn_addTraslado.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_addTraslado.Location = new System.Drawing.Point(415, 70);
            this.btn_addTraslado.Name = "btn_addTraslado";
            this.btn_addTraslado.Size = new System.Drawing.Size(75, 26);
            this.btn_addTraslado.TabIndex = 8;
            this.btn_addTraslado.Text = "Agregar";
            this.btn_addTraslado.UseVisualStyleBackColor = true;
            this.btn_addTraslado.Click += new System.EventHandler(this.btn_addTraslado_Click);
            // 
            // tb_trasImporte
            // 
            this.tb_trasImporte.Location = new System.Drawing.Point(235, 61);
            this.tb_trasImporte.Name = "tb_trasImporte";
            this.tb_trasImporte.ReadOnly = true;
            this.tb_trasImporte.Size = new System.Drawing.Size(100, 21);
            this.tb_trasImporte.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(167, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 18);
            this.label6.TabIndex = 6;
            this.label6.Text = "Importe:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 64);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 18);
            this.label7.TabIndex = 4;
            this.label7.Text = "Tasa:";
            // 
            // cb_trasImpuesto
            // 
            this.cb_trasImpuesto.FormattingEnabled = true;
            this.cb_trasImpuesto.Location = new System.Drawing.Point(235, 24);
            this.cb_trasImpuesto.Name = "cb_trasImpuesto";
            this.cb_trasImpuesto.Size = new System.Drawing.Size(100, 26);
            this.cb_trasImpuesto.TabIndex = 3;
            this.cb_trasImpuesto.Text = "Seleccione";
            this.cb_trasImpuesto.SelectedIndexChanged += new System.EventHandler(this.cb_trasImpuesto_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(167, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 18);
            this.label8.TabIndex = 2;
            this.label8.Text = "Impuesto:";
            // 
            // tb_trasBase
            // 
            this.tb_trasBase.Location = new System.Drawing.Point(49, 24);
            this.tb_trasBase.Name = "tb_trasBase";
            this.tb_trasBase.Size = new System.Drawing.Size(100, 21);
            this.tb_trasBase.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 27);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 18);
            this.label9.TabIndex = 0;
            this.label9.Text = "Base:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rb_traslado);
            this.panel1.Controls.Add(this.rb_retenciones);
            this.panel1.Location = new System.Drawing.Point(12, 32);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(175, 31);
            this.panel1.TabIndex = 4;
            // 
            // dg_impuestos
            // 
            this.dg_impuestos.AllowUserToAddRows = false;
            this.dg_impuestos.AllowUserToResizeRows = false;
            this.dg_impuestos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dg_impuestos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg_impuestos.Location = new System.Drawing.Point(12, 178);
            this.dg_impuestos.MultiSelect = false;
            this.dg_impuestos.Name = "dg_impuestos";
            this.dg_impuestos.ReadOnly = true;
            this.dg_impuestos.Size = new System.Drawing.Size(496, 150);
            this.dg_impuestos.TabIndex = 5;
            this.dg_impuestos.Visible = false;
            // 
            // btn_aceptar
            // 
            this.btn_aceptar.Enabled = false;
            this.btn_aceptar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_aceptar.Location = new System.Drawing.Point(427, 349);
            this.btn_aceptar.Name = "btn_aceptar";
            this.btn_aceptar.Size = new System.Drawing.Size(75, 28);
            this.btn_aceptar.TabIndex = 8;
            this.btn_aceptar.Text = "Aceptar";
            this.btn_aceptar.UseVisualStyleBackColor = true;
            this.btn_aceptar.Click += new System.EventHandler(this.btn_aceptar_Click);
            // 
            // Impuestos_SAT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(520, 389);
            this.Controls.Add(this.btn_aceptar);
            this.Controls.Add(this.gb_traslado);
            this.Controls.Add(this.dg_impuestos);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gb_retencion);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Impuestos_SAT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Impuestos";
            this.Load += new System.EventHandler(this.Impuestos_SAT_Load);
            this.gb_retencion.ResumeLayout(false);
            this.gb_retencion.PerformLayout();
            this.gb_traslado.ResumeLayout(false);
            this.gb_traslado.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg_impuestos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rb_retenciones;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rb_traslado;
        private System.Windows.Forms.GroupBox gb_retencion;
        private System.Windows.Forms.TextBox tb_retBase;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_addRetencion;
        private System.Windows.Forms.TextBox tb_retImporte;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cb_retImpuesto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gb_traslado;
        private System.Windows.Forms.ComboBox cb_trasTasa;
        private System.Windows.Forms.Button btn_addTraslado;
        private System.Windows.Forms.TextBox tb_trasImporte;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cb_trasImpuesto;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tb_trasBase;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cb_retTasa;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dg_impuestos;
        private System.Windows.Forms.Button btn_aceptar;
    }
}