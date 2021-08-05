namespace PersonalNet
{
    partial class LoginForm
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
        	this.panel1 = new System.Windows.Forms.Panel();
        	this.lblBandera = new System.Windows.Forms.Label();
        	this.okButton = new System.Windows.Forms.Button();
        	this.cancelButton = new System.Windows.Forms.Button();
        	this.panel2 = new System.Windows.Forms.Panel();
        	this.password = new System.Windows.Forms.TextBox();
        	this.label2 = new System.Windows.Forms.Label();
        	this.userID = new System.Windows.Forms.TextBox();
        	this.label1 = new System.Windows.Forms.Label();
        	this.panel1.SuspendLayout();
        	this.panel2.SuspendLayout();
        	this.SuspendLayout();
        	// 
        	// panel1
        	// 
        	this.panel1.Controls.Add(this.lblBandera);
        	this.panel1.Controls.Add(this.okButton);
        	this.panel1.Controls.Add(this.cancelButton);
        	this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
        	this.panel1.Location = new System.Drawing.Point(0, 132);
        	this.panel1.Margin = new System.Windows.Forms.Padding(2);
        	this.panel1.Name = "panel1";
        	this.panel1.Size = new System.Drawing.Size(210, 31);
        	this.panel1.TabIndex = 1;
        	// 
        	// lblBandera
        	// 
        	this.lblBandera.Location = new System.Drawing.Point(190, 4);
        	this.lblBandera.Name = "lblBandera";
        	this.lblBandera.Size = new System.Drawing.Size(17, 18);
        	this.lblBandera.TabIndex = 4;
        	this.lblBandera.Text = "0";
        	this.lblBandera.Visible = false;
        	// 
        	// okButton
        	// 
        	this.okButton.Location = new System.Drawing.Point(28, 4);
        	this.okButton.Margin = new System.Windows.Forms.Padding(2);
        	this.okButton.Name = "okButton";
        	this.okButton.Size = new System.Drawing.Size(50, 25);
        	this.okButton.TabIndex = 2;
        	this.okButton.Text = "OK";
        	this.okButton.UseVisualStyleBackColor = true;
        	this.okButton.Click += new System.EventHandler(this.okButton_Click);
        	// 
        	// cancelButton
        	// 
        	this.cancelButton.Location = new System.Drawing.Point(125, 4);
        	this.cancelButton.Margin = new System.Windows.Forms.Padding(2);
        	this.cancelButton.Name = "cancelButton";
        	this.cancelButton.Size = new System.Drawing.Size(50, 25);
        	this.cancelButton.TabIndex = 3;
        	this.cancelButton.Text = "Cancel";
        	this.cancelButton.UseVisualStyleBackColor = true;
        	this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
        	// 
        	// panel2
        	// 
        	this.panel2.Controls.Add(this.password);
        	this.panel2.Controls.Add(this.label2);
        	this.panel2.Controls.Add(this.userID);
        	this.panel2.Controls.Add(this.label1);
        	this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.panel2.Location = new System.Drawing.Point(0, 0);
        	this.panel2.Margin = new System.Windows.Forms.Padding(2);
        	this.panel2.Name = "panel2";
        	this.panel2.Size = new System.Drawing.Size(210, 132);
        	this.panel2.TabIndex = 0;
        	// 
        	// password
        	// 
        	this.password.Location = new System.Drawing.Point(86, 71);
        	this.password.Margin = new System.Windows.Forms.Padding(2);
        	this.password.Name = "password";
        	this.password.PasswordChar = '*';
        	this.password.Size = new System.Drawing.Size(66, 20);
        	this.password.TabIndex = 1;
        	this.password.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        	// 
        	// label2
        	// 
        	this.label2.AutoSize = true;
        	this.label2.Location = new System.Drawing.Point(28, 74);
        	this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        	this.label2.Name = "label2";
        	this.label2.Size = new System.Drawing.Size(53, 13);
        	this.label2.TabIndex = 2;
        	this.label2.Text = "Password";
        	// 
        	// userID
        	// 
        	this.userID.Location = new System.Drawing.Point(86, 31);
        	this.userID.Margin = new System.Windows.Forms.Padding(2);
        	this.userID.Name = "userID";
        	this.userID.Size = new System.Drawing.Size(66, 20);
        	this.userID.TabIndex = 0;
        	this.userID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        	// 
        	// label1
        	// 
        	this.label1.AutoSize = true;
        	this.label1.Location = new System.Drawing.Point(28, 34);
        	this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        	this.label1.Name = "label1";
        	this.label1.Size = new System.Drawing.Size(57, 13);
        	this.label1.TabIndex = 0;
        	this.label1.Text = "Usuario ID";
        	// 
        	// LoginForm
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
        	this.ClientSize = new System.Drawing.Size(210, 163);
        	this.ControlBox = false;
        	this.Controls.Add(this.panel2);
        	this.Controls.Add(this.panel1);
        	this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        	this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
        	this.Margin = new System.Windows.Forms.Padding(2);
        	this.Name = "LoginForm";
        	this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        	this.Text = "                 Acceso Usuario";
        	this.TopMost = true;
        	this.panel1.ResumeLayout(false);
        	this.panel2.ResumeLayout(false);
        	this.panel2.PerformLayout();
        	this.ResumeLayout(false);
        }
        public System.Windows.Forms.Label lblBandera;

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox userID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox password;
        private System.Windows.Forms.Label label2;
    }
}