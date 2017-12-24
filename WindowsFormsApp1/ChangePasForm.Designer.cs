namespace WindowsFormsApp1
{
     //Тест гита с сайта и синхр
    partial class ChangePasForm
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
            this.lblPasRep = new System.Windows.Forms.Label();
            this.lblPas = new System.Windows.Forms.Label();
            this.txtPasOld = new System.Windows.Forms.TextBox();
            this.txtPasNew = new System.Windows.Forms.TextBox();
            this.btnReg = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPas = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblPasRep
            // 
            this.lblPasRep.AutoSize = true;
            this.lblPasRep.Location = new System.Drawing.Point(6, 70);
            this.lblPasRep.Name = "lblPasRep";
            this.lblPasRep.Size = new System.Drawing.Size(126, 13);
            this.lblPasRep.TabIndex = 1;
            this.lblPasRep.Text = "Введите новый пароль:";
            // 
            // lblPas
            // 
            this.lblPas.AutoSize = true;
            this.lblPas.Location = new System.Drawing.Point(6, 20);
            this.lblPas.Name = "lblPas";
            this.lblPas.Size = new System.Drawing.Size(131, 13);
            this.lblPas.TabIndex = 2;
            this.lblPas.Text = "Введите старый пароль:";
            // 
            // txtPasOld
            // 
            this.txtPasOld.Location = new System.Drawing.Point(9, 40);
            this.txtPasOld.Name = "txtPasOld";
            this.txtPasOld.PasswordChar = '*';
            this.txtPasOld.Size = new System.Drawing.Size(149, 20);
            this.txtPasOld.TabIndex = 4;
            // 
            // txtPasNew
            // 
            this.txtPasNew.Location = new System.Drawing.Point(9, 86);
            this.txtPasNew.Name = "txtPasNew";
            this.txtPasNew.PasswordChar = '*';
            this.txtPasNew.Size = new System.Drawing.Size(149, 20);
            this.txtPasNew.TabIndex = 5;
            this.txtPasNew.Click += new System.EventHandler(this.txtPasNew_Click);
            // 
            // btnReg
            // 
            this.btnReg.Location = new System.Drawing.Point(9, 128);
            this.btnReg.Name = "btnReg";
            this.btnReg.Size = new System.Drawing.Size(149, 23);
            this.btnReg.TabIndex = 6;
            this.btnReg.Text = "Сменить пароль";
            this.btnReg.UseVisualStyleBackColor = true;
            this.btnReg.Click += new System.EventHandler(this.btnReg_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblPas);
            this.groupBox1.Controls.Add(this.btnReg);
            this.groupBox1.Controls.Add(this.txtPasOld);
            this.groupBox1.Controls.Add(this.txtPasNew);
            this.groupBox1.Controls.Add(this.lblPasRep);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(170, 170);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Смена пароля:";
            // 
            // txtPas
            // 
            this.txtPas.Location = new System.Drawing.Point(21, 188);
            this.txtPas.Name = "txtPas";
            this.txtPas.Size = new System.Drawing.Size(149, 20);
            this.txtPas.TabIndex = 8;
            this.txtPas.Visible = false;
            // 
            // ChangePasForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(194, 218);
            this.Controls.Add(this.txtPas);
            this.Controls.Add(this.groupBox1);
            this.Name = "ChangePasForm";
            this.Text = "ChangeForm";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblPasRep;
        private System.Windows.Forms.Label lblPas;
        private System.Windows.Forms.TextBox txtPasOld;
        private System.Windows.Forms.TextBox txtPasNew;
        private System.Windows.Forms.Button btnReg;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtPas;
    }
}
