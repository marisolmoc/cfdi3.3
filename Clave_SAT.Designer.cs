namespace SiscomCFDI
{
    partial class Clave_SAT
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Clave_SAT));
            this.tb_desc = new System.Windows.Forms.TextBox();
            this.buscar_codigoSat = new System.Windows.Forms.Button();
            this.dataGrid_codigoSat = new System.Windows.Forms.DataGridView();
            this.codigoSat_grp = new System.Windows.Forms.GroupBox();
            this.claveUnidad_grp = new System.Windows.Forms.GroupBox();
            this.tb_claveUnidad = new System.Windows.Forms.TextBox();
            this.dataGrid_claveUnidad = new System.Windows.Forms.DataGridView();
            this.buscar_claveUnidad = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_codigoSat)).BeginInit();
            this.codigoSat_grp.SuspendLayout();
            this.claveUnidad_grp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_claveUnidad)).BeginInit();
            this.SuspendLayout();
            // 
            // tb_desc
            // 
            this.tb_desc.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_desc.Location = new System.Drawing.Point(20, 34);
            this.tb_desc.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tb_desc.Name = "tb_desc";
            this.tb_desc.Size = new System.Drawing.Size(445, 23);
            this.tb_desc.TabIndex = 1;
            this.tb_desc.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tb_desc_KeyUp);
            // 
            // buscar_codigoSat
            // 
            this.buscar_codigoSat.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buscar_codigoSat.Location = new System.Drawing.Point(489, 32);
            this.buscar_codigoSat.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buscar_codigoSat.Name = "buscar_codigoSat";
            this.buscar_codigoSat.Size = new System.Drawing.Size(100, 28);
            this.buscar_codigoSat.TabIndex = 2;
            this.buscar_codigoSat.Text = "Buscar";
            this.buscar_codigoSat.UseVisualStyleBackColor = true;
            this.buscar_codigoSat.Click += new System.EventHandler(this.buscar_codigoSat_Click);
            // 
            // dataGrid_codigoSat
            // 
            this.dataGrid_codigoSat.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGrid_codigoSat.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGrid_codigoSat.Location = new System.Drawing.Point(20, 84);
            this.dataGrid_codigoSat.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGrid_codigoSat.MultiSelect = false;
            this.dataGrid_codigoSat.Name = "dataGrid_codigoSat";
            this.dataGrid_codigoSat.ReadOnly = true;
            this.dataGrid_codigoSat.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGrid_codigoSat.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGrid_codigoSat.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid_codigoSat.Size = new System.Drawing.Size(569, 206);
            this.dataGrid_codigoSat.TabIndex = 3;
            this.dataGrid_codigoSat.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_codigoSat_CellClick);
            // 
            // codigoSat_grp
            // 
            this.codigoSat_grp.Controls.Add(this.tb_desc);
            this.codigoSat_grp.Controls.Add(this.dataGrid_codigoSat);
            this.codigoSat_grp.Controls.Add(this.buscar_codigoSat);
            this.codigoSat_grp.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.codigoSat_grp.Location = new System.Drawing.Point(16, 16);
            this.codigoSat_grp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.codigoSat_grp.Name = "codigoSat_grp";
            this.codigoSat_grp.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.codigoSat_grp.Size = new System.Drawing.Size(612, 308);
            this.codigoSat_grp.TabIndex = 4;
            this.codigoSat_grp.TabStop = false;
            this.codigoSat_grp.Text = "Seleccione Codigo SAT de Producto/Servicio ";
            this.codigoSat_grp.Visible = false;
            // 
            // claveUnidad_grp
            // 
            this.claveUnidad_grp.Controls.Add(this.tb_claveUnidad);
            this.claveUnidad_grp.Controls.Add(this.dataGrid_claveUnidad);
            this.claveUnidad_grp.Controls.Add(this.buscar_claveUnidad);
            this.claveUnidad_grp.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.claveUnidad_grp.Location = new System.Drawing.Point(16, 18);
            this.claveUnidad_grp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.claveUnidad_grp.Name = "claveUnidad_grp";
            this.claveUnidad_grp.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.claveUnidad_grp.Size = new System.Drawing.Size(612, 306);
            this.claveUnidad_grp.TabIndex = 5;
            this.claveUnidad_grp.TabStop = false;
            this.claveUnidad_grp.Text = "Selecciones Codigo Unicad SAT";
            this.claveUnidad_grp.Visible = false;
            // 
            // tb_claveUnidad
            // 
            this.tb_claveUnidad.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_claveUnidad.Location = new System.Drawing.Point(20, 36);
            this.tb_claveUnidad.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tb_claveUnidad.Name = "tb_claveUnidad";
            this.tb_claveUnidad.Size = new System.Drawing.Size(445, 23);
            this.tb_claveUnidad.TabIndex = 4;
            this.tb_claveUnidad.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tb_claveUnidad_KeyUp);
            // 
            // dataGrid_claveUnidad
            // 
            this.dataGrid_claveUnidad.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGrid_claveUnidad.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGrid_claveUnidad.Location = new System.Drawing.Point(20, 83);
            this.dataGrid_claveUnidad.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dataGrid_claveUnidad.MultiSelect = false;
            this.dataGrid_claveUnidad.Name = "dataGrid_claveUnidad";
            this.dataGrid_claveUnidad.ReadOnly = true;
            this.dataGrid_claveUnidad.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGrid_claveUnidad.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGrid_claveUnidad.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGrid_claveUnidad.Size = new System.Drawing.Size(569, 206);
            this.dataGrid_claveUnidad.TabIndex = 6;
            this.dataGrid_claveUnidad.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGrid_claveUnidad_CellClick);
            // 
            // buscar_claveUnidad
            // 
            this.buscar_claveUnidad.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buscar_claveUnidad.Location = new System.Drawing.Point(489, 33);
            this.buscar_claveUnidad.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buscar_claveUnidad.Name = "buscar_claveUnidad";
            this.buscar_claveUnidad.Size = new System.Drawing.Size(100, 28);
            this.buscar_claveUnidad.TabIndex = 5;
            this.buscar_claveUnidad.Text = "Buscar";
            this.buscar_claveUnidad.UseVisualStyleBackColor = true;
            this.buscar_claveUnidad.Click += new System.EventHandler(this.buscar_claveUnidad_Click);
            // 
            // Clave_SAT
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 340);
            this.Controls.Add(this.claveUnidad_grp);
            this.Controls.Add(this.codigoSat_grp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Clave_SAT";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Clave SAT";
            this.Load += new System.EventHandler(this.Clave_SAT_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_codigoSat)).EndInit();
            this.codigoSat_grp.ResumeLayout(false);
            this.codigoSat_grp.PerformLayout();
            this.claveUnidad_grp.ResumeLayout(false);
            this.claveUnidad_grp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid_claveUnidad)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tb_desc;
        private System.Windows.Forms.Button buscar_codigoSat;
        private System.Windows.Forms.DataGridView dataGrid_codigoSat;
        private System.Windows.Forms.GroupBox codigoSat_grp;
        private System.Windows.Forms.GroupBox claveUnidad_grp;
        private System.Windows.Forms.TextBox tb_claveUnidad;
        private System.Windows.Forms.DataGridView dataGrid_claveUnidad;
        private System.Windows.Forms.Button buscar_claveUnidad;
    }
}