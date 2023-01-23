namespace SiscomCFDI
{
    partial class Acceso
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Acceso));
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_usuario = new System.Windows.Forms.TextBox();
            this.tb_passw = new System.Windows.Forms.TextBox();
            this.btn_entrar = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 18);
            this.label2.TabIndex = 1;
            this.label2.Text = "Usuario:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Trebuchet MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(13, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 18);
            this.label3.TabIndex = 2;
            this.label3.Text = "Contraseña:";
            // 
            // tb_usuario
            // 
            this.tb_usuario.Location = new System.Drawing.Point(16, 38);
            this.tb_usuario.Name = "tb_usuario";
            this.tb_usuario.Size = new System.Drawing.Size(182, 20);
            this.tb_usuario.TabIndex = 3;
            // 
            // tb_passw
            // 
            this.tb_passw.Location = new System.Drawing.Point(16, 96);
            this.tb_passw.Name = "tb_passw";
            this.tb_passw.Size = new System.Drawing.Size(182, 20);
            this.tb_passw.TabIndex = 4;
            this.tb_passw.UseSystemPasswordChar = true;
            this.tb_passw.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tb_passw_KeyUp);
            // 
            // btn_entrar
            // 
            this.btn_entrar.Location = new System.Drawing.Point(68, 134);
            this.btn_entrar.Name = "btn_entrar";
            this.btn_entrar.Size = new System.Drawing.Size(75, 23);
            this.btn_entrar.TabIndex = 5;
            this.btn_entrar.Text = "Aceptar";
            this.btn_entrar.UseVisualStyleBackColor = true;
            this.btn_entrar.Click += new System.EventHandler(this.btn_entrar_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(151, 134);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 6;
            this.btn_cancel.Text = "Cancelar";
            this.btn_cancel.UseVisualStyleBackColor = true;
            // 
            // Acceso
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(237, 168);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_entrar);
            this.Controls.Add(this.tb_passw);
            this.Controls.Add(this.tb_usuario);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Acceso";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Acceso";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_usuario;
        private System.Windows.Forms.TextBox tb_passw;
        private System.Windows.Forms.Button btn_entrar;
        private System.Windows.Forms.Button btn_cancel;
    }
}