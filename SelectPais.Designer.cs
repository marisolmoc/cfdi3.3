namespace SiscomCFDI
{
    partial class SelectPais
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectPais));
            this.label1 = new System.Windows.Forms.Label();
            this.tb_pais = new System.Windows.Forms.TextBox();
            this.btn_pais = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Introdusca el País Extranjero:";
            // 
            // tb_pais
            // 
            this.tb_pais.Location = new System.Drawing.Point(12, 29);
            this.tb_pais.Name = "tb_pais";
            this.tb_pais.Size = new System.Drawing.Size(146, 20);
            this.tb_pais.TabIndex = 1;
            // 
            // btn_pais
            // 
            this.btn_pais.Location = new System.Drawing.Point(83, 68);
            this.btn_pais.Name = "btn_pais";
            this.btn_pais.Size = new System.Drawing.Size(75, 23);
            this.btn_pais.TabIndex = 2;
            this.btn_pais.Text = "Aceptar";
            this.btn_pais.UseVisualStyleBackColor = true;
            this.btn_pais.Click += new System.EventHandler(this.btn_pais_Click);
            // 
            // SelectPais
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(176, 104);
            this.Controls.Add(this.btn_pais);
            this.Controls.Add(this.tb_pais);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SelectPais";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "País";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_pais;
        private System.Windows.Forms.Button btn_pais;
    }
}