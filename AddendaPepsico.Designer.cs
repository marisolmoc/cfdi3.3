namespace SiscomCFDI
{
    partial class AddendaPepsico
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddendaPepsico));
            this.btnContinueP = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.recep_red = new System.Windows.Forms.Label();
            this.prov_red = new System.Windows.Forms.Label();
            this.pedido_red = new System.Windows.Forms.Label();
            this.tb_NumRecepcion = new System.Windows.Forms.TextBox();
            this.tb_NumProveedor = new System.Windows.Forms.TextBox();
            this.tb_NumPedido = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnContinueP
            // 
            this.btnContinueP.Location = new System.Drawing.Point(226, 162);
            this.btnContinueP.Name = "btnContinueP";
            this.btnContinueP.Size = new System.Drawing.Size(77, 34);
            this.btnContinueP.TabIndex = 3;
            this.btnContinueP.Text = "Continuar";
            this.btnContinueP.UseVisualStyleBackColor = true;
            this.btnContinueP.Click += new System.EventHandler(this.btnContinueP_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.recep_red);
            this.groupBox1.Controls.Add(this.prov_red);
            this.groupBox1.Controls.Add(this.pedido_red);
            this.groupBox1.Controls.Add(this.tb_NumRecepcion);
            this.groupBox1.Controls.Add(this.tb_NumProveedor);
            this.groupBox1.Controls.Add(this.tb_NumPedido);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(291, 138);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Captura de datos";
            // 
            // recep_red
            // 
            this.recep_red.AutoSize = true;
            this.recep_red.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.recep_red.ForeColor = System.Drawing.Color.Red;
            this.recep_red.Location = new System.Drawing.Point(262, 93);
            this.recep_red.Name = "recep_red";
            this.recep_red.Size = new System.Drawing.Size(16, 22);
            this.recep_red.TabIndex = 10;
            this.recep_red.Text = "*";
            this.recep_red.Visible = false;
            // 
            // prov_red
            // 
            this.prov_red.AutoSize = true;
            this.prov_red.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prov_red.ForeColor = System.Drawing.Color.Red;
            this.prov_red.Location = new System.Drawing.Point(262, 60);
            this.prov_red.Name = "prov_red";
            this.prov_red.Size = new System.Drawing.Size(16, 22);
            this.prov_red.TabIndex = 9;
            this.prov_red.Text = "*";
            this.prov_red.Visible = false;
            // 
            // pedido_red
            // 
            this.pedido_red.AutoSize = true;
            this.pedido_red.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pedido_red.ForeColor = System.Drawing.Color.Red;
            this.pedido_red.Location = new System.Drawing.Point(262, 25);
            this.pedido_red.Name = "pedido_red";
            this.pedido_red.Size = new System.Drawing.Size(16, 22);
            this.pedido_red.TabIndex = 8;
            this.pedido_red.Text = "*";
            this.pedido_red.Visible = false;
            // 
            // tb_NumRecepcion
            // 
            this.tb_NumRecepcion.Location = new System.Drawing.Point(163, 96);
            this.tb_NumRecepcion.Name = "tb_NumRecepcion";
            this.tb_NumRecepcion.Size = new System.Drawing.Size(100, 20);
            this.tb_NumRecepcion.TabIndex = 5;
            // 
            // tb_NumProveedor
            // 
            this.tb_NumProveedor.Location = new System.Drawing.Point(163, 63);
            this.tb_NumProveedor.Name = "tb_NumProveedor";
            this.tb_NumProveedor.Size = new System.Drawing.Size(100, 20);
            this.tb_NumProveedor.TabIndex = 4;
            // 
            // tb_NumPedido
            // 
            this.tb_NumPedido.Location = new System.Drawing.Point(163, 28);
            this.tb_NumPedido.Name = "tb_NumPedido";
            this.tb_NumPedido.Size = new System.Drawing.Size(100, 20);
            this.tb_NumPedido.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Número de Recepción:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(131, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Número de Proveedor:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Número de pedido:";
            // 
            // AddendaPepsico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(315, 209);
            this.Controls.Add(this.btnContinueP);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddendaPepsico";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Addenda Pepsico";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnContinueP;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label recep_red;
        private System.Windows.Forms.Label prov_red;
        private System.Windows.Forms.Label pedido_red;
        private System.Windows.Forms.TextBox tb_NumRecepcion;
        private System.Windows.Forms.TextBox tb_NumProveedor;
        private System.Windows.Forms.TextBox tb_NumPedido;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}