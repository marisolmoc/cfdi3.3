namespace SiscomCFDI
{
    partial class Folios
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Folios));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_cEmp = new System.Windows.Forms.ComboBox();
            this.empresaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.cfdiDataSet = new SiscomCFDI.cfdiDataSet();
            this.cfdiDataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.empresaTableAdapter = new SiscomCFDI.cfdiDataSetTableAdapters.empresaTableAdapter();
            this.btn_Eliminar = new System.Windows.Forms.Button();
            this.lbl_emp = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.empresaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cfdiDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cfdiDataSetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.CornflowerBlue;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Location = new System.Drawing.Point(6, 19);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(630, 175);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(642, 200);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Folios Activos";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(424, 262);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Agregar Folios";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(560, 262);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Cerrar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Clave Empresa:";
            // 
            // cb_cEmp
            // 
            this.cb_cEmp.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.empresaBindingSource, "Clave", true));
            this.cb_cEmp.DataSource = this.cfdiDataSetBindingSource;
            this.cb_cEmp.FormattingEnabled = true;
            this.cb_cEmp.Location = new System.Drawing.Point(107, 10);
            this.cb_cEmp.Name = "cb_cEmp";
            this.cb_cEmp.Size = new System.Drawing.Size(59, 21);
            this.cb_cEmp.TabIndex = 5;
            this.cb_cEmp.SelectedValueChanged += new System.EventHandler(this.cb_cEmp_SelectedValueChanged);
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
            // empresaTableAdapter
            // 
            this.empresaTableAdapter.ClearBeforeFill = true;
            // 
            // btn_Eliminar
            // 
            this.btn_Eliminar.Location = new System.Drawing.Point(18, 262);
            this.btn_Eliminar.Name = "btn_Eliminar";
            this.btn_Eliminar.Size = new System.Drawing.Size(75, 23);
            this.btn_Eliminar.TabIndex = 6;
            this.btn_Eliminar.Text = "Eliminar";
            this.btn_Eliminar.UseVisualStyleBackColor = true;
            this.btn_Eliminar.Click += new System.EventHandler(this.btn_Eliminar_Click);
            // 
            // lbl_emp
            // 
            this.lbl_emp.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_emp.Location = new System.Drawing.Point(172, 12);
            this.lbl_emp.Name = "lbl_emp";
            this.lbl_emp.Size = new System.Drawing.Size(476, 24);
            this.lbl_emp.TabIndex = 7;
            // 
            // Folios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666, 309);
            this.Controls.Add(this.lbl_emp);
            this.Controls.Add(this.btn_Eliminar);
            this.Controls.Add(this.cb_cEmp);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Folios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Folios";
            this.Load += new System.EventHandler(this.Folios_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.empresaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cfdiDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cfdiDataSetBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_cEmp;
        private cfdiDataSet cfdiDataSet;
        private System.Windows.Forms.BindingSource empresaBindingSource;
        private SiscomCFDI.cfdiDataSetTableAdapters.empresaTableAdapter empresaTableAdapter;
        private System.Windows.Forms.BindingSource cfdiDataSetBindingSource;
        private System.Windows.Forms.Button btn_Eliminar;
        private System.Windows.Forms.Label lbl_emp;
    }
}