namespace IOX.UserControls
{
    partial class ucComSettings
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucComSettings));
            this.cbPortName = new System.Windows.Forms.ComboBox();
            this.cbBaudRate = new System.Windows.Forms.ComboBox();
            this.nudDataBits = new System.Windows.Forms.NumericUpDown();
            this.cbStopBits = new System.Windows.Forms.ComboBox();
            this.cbParity = new System.Windows.Forms.ComboBox();
            this.btnComPortRefresh = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.CheckBox();
            this.gb = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudDataBits)).BeginInit();
            this.gb.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbPortName
            // 
            this.cbPortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPortName.FormattingEnabled = true;
            this.cbPortName.Location = new System.Drawing.Point(6, 19);
            this.cbPortName.Name = "cbPortName";
            this.cbPortName.Size = new System.Drawing.Size(60, 21);
            this.cbPortName.TabIndex = 0;
            // 
            // cbBaudRate
            // 
            this.cbBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBaudRate.FormattingEnabled = true;
            this.cbBaudRate.Items.AddRange(new object[] {
            "230400"});
            this.cbBaudRate.Location = new System.Drawing.Point(99, 19);
            this.cbBaudRate.Name = "cbBaudRate";
            this.cbBaudRate.Size = new System.Drawing.Size(70, 21);
            this.cbBaudRate.TabIndex = 1;
            // 
            // nudDataBits
            // 
            this.nudDataBits.Location = new System.Drawing.Point(175, 20);
            this.nudDataBits.Name = "nudDataBits";
            this.nudDataBits.Size = new System.Drawing.Size(30, 20);
            this.nudDataBits.TabIndex = 3;
            // 
            // cbStopBits
            // 
            this.cbStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStopBits.FormattingEnabled = true;
            this.cbStopBits.Location = new System.Drawing.Point(277, 19);
            this.cbStopBits.Name = "cbStopBits";
            this.cbStopBits.Size = new System.Drawing.Size(90, 21);
            this.cbStopBits.TabIndex = 4;
            // 
            // cbParity
            // 
            this.cbParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbParity.FormattingEnabled = true;
            this.cbParity.Location = new System.Drawing.Point(211, 19);
            this.cbParity.Name = "cbParity";
            this.cbParity.Size = new System.Drawing.Size(60, 21);
            this.cbParity.TabIndex = 5;
            // 
            // btnComPortRefresh
            // 
            this.btnComPortRefresh.BackColor = System.Drawing.SystemColors.Control;
            this.btnComPortRefresh.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnComPortRefresh.BackgroundImage")));
            this.btnComPortRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnComPortRefresh.Location = new System.Drawing.Point(71, 18);
            this.btnComPortRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.btnComPortRefresh.Name = "btnComPortRefresh";
            this.btnComPortRefresh.Size = new System.Drawing.Size(23, 23);
            this.btnComPortRefresh.TabIndex = 2;
            this.btnComPortRefresh.UseVisualStyleBackColor = false;
            this.btnComPortRefresh.Click += new System.EventHandler(this.btnComPortRefresh_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Appearance = System.Windows.Forms.Appearance.Button;
            this.btnConnect.AutoCheck = false;
            this.btnConnect.Location = new System.Drawing.Point(372, 18);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(2);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(70, 23);
            this.btnConnect.TabIndex = 6;
            this.btnConnect.Text = "Disconnect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // gb
            // 
            this.gb.Controls.Add(this.btnConnect);
            this.gb.Controls.Add(this.cbParity);
            this.gb.Controls.Add(this.cbStopBits);
            this.gb.Controls.Add(this.nudDataBits);
            this.gb.Controls.Add(this.btnComPortRefresh);
            this.gb.Controls.Add(this.cbBaudRate);
            this.gb.Controls.Add(this.cbPortName);
            this.gb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gb.Location = new System.Drawing.Point(0, 0);
            this.gb.Name = "gb";
            this.gb.Size = new System.Drawing.Size(447, 46);
            this.gb.TabIndex = 7;
            this.gb.TabStop = false;
            this.gb.Text = "Serial port settings";
            // 
            // ucComSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gb);
            this.Name = "ucComSettings";
            this.Size = new System.Drawing.Size(447, 46);
            ((System.ComponentModel.ISupportInitialize)(this.nudDataBits)).EndInit();
            this.gb.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbPortName;
        private System.Windows.Forms.ComboBox cbBaudRate;
        private System.Windows.Forms.Button btnComPortRefresh;
        private System.Windows.Forms.NumericUpDown nudDataBits;
        private System.Windows.Forms.ComboBox cbStopBits;
        private System.Windows.Forms.ComboBox cbParity;
        private System.Windows.Forms.CheckBox btnConnect;
        private System.Windows.Forms.GroupBox gb;
    }
}
