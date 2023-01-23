namespace SiscomCFDI
{
    partial class Correo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Correo));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tb_asunto = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_pdf = new System.Windows.Forms.Label();
            this.lbl_xml = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rtb_coment = new System.Windows.Forms.RichTextBox();
            this.tb_para = new System.Windows.Forms.TextBox();
            this.tb_de = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_send = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.tb_cco = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_cc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tb_cco);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.tb_cc);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tb_asunto);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lbl_pdf);
            this.groupBox1.Controls.Add(this.lbl_xml);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.tb_para);
            this.groupBox1.Controls.Add(this.tb_de);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(372, 396);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Envío de Correo";
            // 
            // tb_asunto
            // 
            this.tb_asunto.Location = new System.Drawing.Point(77, 141);
            this.tb_asunto.Name = "tb_asunto";
            this.tb_asunto.Size = new System.Drawing.Size(225, 20);
            this.tb_asunto.TabIndex = 6;
            this.tb_asunto.Text = "Envio de CFDI  y PDF.";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 144);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Asunto:";
            // 
            // lbl_pdf
            // 
            this.lbl_pdf.AutoSize = true;
            this.lbl_pdf.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_pdf.Location = new System.Drawing.Point(110, 198);
            this.lbl_pdf.Name = "lbl_pdf";
            this.lbl_pdf.Size = new System.Drawing.Size(0, 13);
            this.lbl_pdf.TabIndex = 7;
            // 
            // lbl_xml
            // 
            this.lbl_xml.AutoSize = true;
            this.lbl_xml.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_xml.Location = new System.Drawing.Point(110, 183);
            this.lbl_xml.Name = "lbl_xml";
            this.lbl_xml.Size = new System.Drawing.Size(0, 13);
            this.lbl_xml.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 182);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Datos Adjuntos:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rtb_coment);
            this.groupBox2.Location = new System.Drawing.Point(6, 225);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(360, 160);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Comentario";
            // 
            // rtb_coment
            // 
            this.rtb_coment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_coment.Location = new System.Drawing.Point(3, 16);
            this.rtb_coment.Name = "rtb_coment";
            this.rtb_coment.Size = new System.Drawing.Size(354, 141);
            this.rtb_coment.TabIndex = 7;
            this.rtb_coment.Text = "Notificación de envio de CFDI.  \nSe anexan los archivos XML y PDF del CFDI.\n\nGrac" +
                "ias. ";
            // 
            // tb_para
            // 
            this.tb_para.Location = new System.Drawing.Point(77, 58);
            this.tb_para.Name = "tb_para";
            this.tb_para.Size = new System.Drawing.Size(225, 20);
            this.tb_para.TabIndex = 3;
            this.tb_para.Leave += new System.EventHandler(this.tb_para_Leave);
            // 
            // tb_de
            // 
            this.tb_de.Location = new System.Drawing.Point(77, 30);
            this.tb_de.Name = "tb_de";
            this.tb_de.Size = new System.Drawing.Size(225, 20);
            this.tb_de.TabIndex = 2;
            this.tb_de.Leave += new System.EventHandler(this.tb_de_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "De:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Para:";
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(186, 425);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(75, 23);
            this.btn_send.TabIndex = 8;
            this.btn_send.Text = "Enviar";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Location = new System.Drawing.Point(291, 425);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 2;
            this.btn_cancel.Text = "Cerrar";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // tb_cco
            // 
            this.tb_cco.Location = new System.Drawing.Point(77, 114);
            this.tb_cco.Name = "tb_cco";
            this.tb_cco.Size = new System.Drawing.Size(225, 20);
            this.tb_cco.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(28, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "CCO:";
            // 
            // tb_cc
            // 
            this.tb_cc.Location = new System.Drawing.Point(77, 87);
            this.tb_cc.Name = "tb_cc";
            this.tb_cc.Size = new System.Drawing.Size(225, 20);
            this.tb_cc.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(24, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "CC:";
            // 
            // Correo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(397, 462);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Correo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Envío de Factura";
            this.Load += new System.EventHandler(this.Correo_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_pdf;
        private System.Windows.Forms.Label lbl_xml;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox rtb_coment;
        private System.Windows.Forms.TextBox tb_para;
        private System.Windows.Forms.TextBox tb_de;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.TextBox tb_asunto;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_cco;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_cc;
        private System.Windows.Forms.Label label4;
    }
}