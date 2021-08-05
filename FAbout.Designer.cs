namespace PersonalNet
{
    partial class FAbout
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
        	System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FAbout));
        	this.panel1 = new System.Windows.Forms.Panel();
        	this.okButton = new System.Windows.Forms.Button();
        	this.cancelButton = new System.Windows.Forms.Button();
        	this.panel2 = new System.Windows.Forms.Panel();
        	this.label7 = new System.Windows.Forms.Label();
        	this.panel4 = new System.Windows.Forms.Panel();
        	this.label8 = new System.Windows.Forms.Label();
        	this.label1 = new System.Windows.Forms.Label();
        	this.label2 = new System.Windows.Forms.Label();
        	this.panel3 = new System.Windows.Forms.Panel();
        	this.label4 = new System.Windows.Forms.Label();
        	this.panel1.SuspendLayout();
        	this.panel2.SuspendLayout();
        	this.panel4.SuspendLayout();
        	this.SuspendLayout();
        	// 
        	// panel1
        	// 
        	this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        	this.panel1.Controls.Add(this.okButton);
        	this.panel1.Controls.Add(this.cancelButton);
        	this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
        	this.panel1.Location = new System.Drawing.Point(0, 222);
        	this.panel1.Margin = new System.Windows.Forms.Padding(2);
        	this.panel1.Name = "panel1";
        	this.panel1.Size = new System.Drawing.Size(517, 31);
        	this.panel1.TabIndex = 1;
        	// 
        	// okButton
        	// 
        	this.okButton.BackColor = System.Drawing.Color.Cyan;
        	this.okButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        	this.okButton.Location = new System.Drawing.Point(199, 3);
        	this.okButton.Margin = new System.Windows.Forms.Padding(2);
        	this.okButton.Name = "okButton";
        	this.okButton.Size = new System.Drawing.Size(117, 25);
        	this.okButton.TabIndex = 2;
        	this.okButton.Text = "OK";
        	this.okButton.UseVisualStyleBackColor = false;
        	this.okButton.Click += new System.EventHandler(this.okButton_Click);
        	// 
        	// cancelButton
        	// 
        	this.cancelButton.Location = new System.Drawing.Point(2, 3);
        	this.cancelButton.Margin = new System.Windows.Forms.Padding(2);
        	this.cancelButton.Name = "cancelButton";
        	this.cancelButton.Size = new System.Drawing.Size(50, 25);
        	this.cancelButton.TabIndex = 3;
        	this.cancelButton.Text = "Cancel";
        	this.cancelButton.UseVisualStyleBackColor = true;
        	this.cancelButton.Visible = false;
        	this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
        	// 
        	// panel2
        	// 
        	this.panel2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel2.BackgroundImage")));
        	this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        	this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        	this.panel2.Controls.Add(this.label7);
        	this.panel2.Controls.Add(this.panel4);
        	this.panel2.Controls.Add(this.panel3);
        	this.panel2.Controls.Add(this.label4);
        	this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
        	this.panel2.Location = new System.Drawing.Point(0, 0);
        	this.panel2.Margin = new System.Windows.Forms.Padding(2);
        	this.panel2.Name = "panel2";
        	this.panel2.Size = new System.Drawing.Size(517, 222);
        	this.panel2.TabIndex = 0;
        	this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel2Paint);
        	// 
        	// label7
        	// 
        	this.label7.BackColor = System.Drawing.Color.OrangeRed;
        	this.label7.Location = new System.Drawing.Point(10, 96);
        	this.label7.Name = "label7";
        	this.label7.Size = new System.Drawing.Size(487, 4);
        	this.label7.TabIndex = 9;
        	// 
        	// panel4
        	// 
        	this.panel4.BackColor = System.Drawing.Color.PaleGreen;
        	this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        	this.panel4.Controls.Add(this.label8);
        	this.panel4.Controls.Add(this.label1);
        	this.panel4.Controls.Add(this.label2);
        	this.panel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        	this.panel4.Location = new System.Drawing.Point(161, 10);
        	this.panel4.Name = "panel4";
        	this.panel4.Size = new System.Drawing.Size(334, 83);
        	this.panel4.TabIndex = 8;
        	// 
        	// label8
        	// 
        	this.label8.AutoSize = true;
        	this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        	this.label8.ForeColor = System.Drawing.Color.Black;
        	this.label8.Location = new System.Drawing.Point(108, 58);
        	this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        	this.label8.Name = "label8";
        	this.label8.Size = new System.Drawing.Size(91, 17);
        	this.label8.TabIndex = 11;
        	this.label8.Text = "Version 2.0";
        	// 
        	// label1
        	// 
        	this.label1.AutoSize = true;
        	this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        	this.label1.ForeColor = System.Drawing.Color.Red;
        	this.label1.Location = new System.Drawing.Point(78, 0);
        	this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        	this.label1.Name = "label1";
        	this.label1.Size = new System.Drawing.Size(149, 13);
        	this.label1.TabIndex = 0;
        	this.label1.Text = "CNP-Check Net Personal";
        	this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        	// 
        	// label2
        	// 
        	this.label2.AutoSize = true;
        	this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        	this.label2.ForeColor = System.Drawing.Color.Blue;
        	this.label2.Location = new System.Drawing.Point(62, 15);
        	this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        	this.label2.Name = "label2";
        	this.label2.Size = new System.Drawing.Size(195, 13);
        	this.label2.TabIndex = 2;
        	this.label2.Text = "Corporacion \"Open Net Systems\"";
        	// 
        	// panel3
        	// 
        	this.panel3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel3.BackgroundImage")));
        	this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
        	this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        	this.panel3.Location = new System.Drawing.Point(10, 10);
        	this.panel3.Name = "panel3";
        	this.panel3.Size = new System.Drawing.Size(145, 83);
        	this.panel3.TabIndex = 7;
        	// 
        	// label4
        	// 
        	this.label4.AutoSize = true;
        	this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        	this.label4.ForeColor = System.Drawing.Color.Blue;
        	this.label4.Location = new System.Drawing.Point(80, 124);
        	this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        	this.label4.Name = "label4";
        	this.label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
        	this.label4.Size = new System.Drawing.Size(0, 17);
        	this.label4.TabIndex = 3;
        	// 
        	// FAbout
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
        	this.ClientSize = new System.Drawing.Size(517, 253);
        	this.ControlBox = false;
        	this.Controls.Add(this.panel2);
        	this.Controls.Add(this.panel1);
        	this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        	this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
        	this.Margin = new System.Windows.Forms.Padding(2);
        	this.Name = "FAbout";
        	this.RightToLeft = System.Windows.Forms.RightToLeft.No;
        	this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        	this.Text = "Reloj Checador Personal";
        	this.TopMost = true;
        	this.panel1.ResumeLayout(false);
        	this.panel2.ResumeLayout(false);
        	this.panel2.PerformLayout();
        	this.panel4.ResumeLayout(false);
        	this.panel4.PerformLayout();
        	this.ResumeLayout(false);
        }
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label4;

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}