namespace Laba_8
{
    partial class MainWindow
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.label1 = new System.Windows.Forms.Label();
            this.email = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.fileSize = new System.Windows.Forms.NumericUpDown();
            this.hideButton = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.saveConfig = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.fileSize)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Email:";
            // 
            // email
            // 
            this.email.Location = new System.Drawing.Point(126, 12);
            this.email.Name = "email";
            this.email.Size = new System.Drawing.Size(186, 22);
            this.email.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "Размер файлов:";
            // 
            // fileSize
            // 
            this.fileSize.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.fileSize.Location = new System.Drawing.Point(126, 42);
            this.fileSize.Maximum = new decimal(new int[] {
            1048576,
            0,
            0,
            0});
            this.fileSize.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.fileSize.Name = "fileSize";
            this.fileSize.Size = new System.Drawing.Size(86, 22);
            this.fileSize.TabIndex = 4;
            this.fileSize.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // hideButton
            // 
            this.hideButton.Location = new System.Drawing.Point(225, 38);
            this.hideButton.Name = "hideButton";
            this.hideButton.Size = new System.Drawing.Size(87, 27);
            this.hideButton.TabIndex = 22;
            this.hideButton.Text = "Скрыть";
            this.hideButton.UseVisualStyleBackColor = true;
            this.hideButton.Click += new System.EventHandler(this.HideProgramButtonClick);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(207, 44);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(0, 15);
            this.label13.TabIndex = 23;
            // 
            // saveConfig
            // 
            this.saveConfig.Location = new System.Drawing.Point(14, 74);
            this.saveConfig.Name = "saveConfig";
            this.saveConfig.Size = new System.Drawing.Size(87, 27);
            this.saveConfig.TabIndex = 24;
            this.saveConfig.Text = "Сохранить";
            this.saveConfig.UseVisualStyleBackColor = true;
            this.saveConfig.Click += new System.EventHandler(this.SaveSettingsButtonClick);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(324, 112);
            this.Controls.Add(this.saveConfig);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.hideButton);
            this.Controls.Add(this.fileSize);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.email);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.Text = "Hooks";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.Shown += new System.EventHandler(this.MainWindowShown);
            ((System.ComponentModel.ISupportInitialize)(this.fileSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox email;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown fileSize;
        private System.Windows.Forms.Button hideButton;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button saveConfig;
    }
}

