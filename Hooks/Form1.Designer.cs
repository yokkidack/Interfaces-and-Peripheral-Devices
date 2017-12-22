namespace Global_Hooker
{
    partial class Form1
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
            this.EmailAddresserT = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.EmailAddresseeT = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.EmailPasswordT = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.FileSizeT = new System.Windows.Forms.TextBox();
            this.HiddenModeC = new System.Windows.Forms.CheckBox();
            this.SaveB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // EmailAddresserT
            // 
            this.EmailAddresserT.Location = new System.Drawing.Point(113, 46);
            this.EmailAddresserT.Name = "EmailAddresserT";
            this.EmailAddresserT.Size = new System.Drawing.Size(181, 20);
            this.EmailAddresserT.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Email Addresser";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Email Addressee";
            // 
            // EmailAddresseeT
            // 
            this.EmailAddresseeT.Location = new System.Drawing.Point(113, 116);
            this.EmailAddresseeT.Name = "EmailAddresseeT";
            this.EmailAddresseeT.Size = new System.Drawing.Size(181, 20);
            this.EmailAddresseeT.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Email Password";
            // 
            // EmailPasswordT
            // 
            this.EmailPasswordT.Location = new System.Drawing.Point(113, 79);
            this.EmailPasswordT.Name = "EmailPasswordT";
            this.EmailPasswordT.Size = new System.Drawing.Size(181, 20);
            this.EmailPasswordT.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "File Size";
            // 
            // FileSizeT
            // 
            this.FileSizeT.Location = new System.Drawing.Point(113, 155);
            this.FileSizeT.Name = "FileSizeT";
            this.FileSizeT.Size = new System.Drawing.Size(181, 20);
            this.FileSizeT.TabIndex = 6;
            // 
            // HiddenModeC
            // 
            this.HiddenModeC.AutoSize = true;
            this.HiddenModeC.Location = new System.Drawing.Point(29, 203);
            this.HiddenModeC.Name = "HiddenModeC";
            this.HiddenModeC.Size = new System.Drawing.Size(90, 17);
            this.HiddenModeC.TabIndex = 11;
            this.HiddenModeC.Text = "Hidden Mode";
            this.HiddenModeC.UseVisualStyleBackColor = true;
            // 
            // SaveB
            // 
            this.SaveB.Location = new System.Drawing.Point(165, 197);
            this.SaveB.Name = "SaveB";
            this.SaveB.Size = new System.Drawing.Size(75, 23);
            this.SaveB.TabIndex = 12;
            this.SaveB.Text = "Save";
            this.SaveB.UseVisualStyleBackColor = true;
            this.SaveB.Click += new System.EventHandler(this.SaveB_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(315, 272);
            this.Controls.Add(this.SaveB);
            this.Controls.Add(this.HiddenModeC);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.FileSizeT);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.EmailPasswordT);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.EmailAddresseeT);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.EmailAddresserT);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox EmailAddresserT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox EmailAddresseeT;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox EmailPasswordT;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox FileSizeT;
        private System.Windows.Forms.CheckBox HiddenModeC;
        private System.Windows.Forms.Button SaveB;
    }
}

