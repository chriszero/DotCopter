namespace SerialPortSampleEmulator
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
            this.appToCom1TextBox = new System.Windows.Forms.TextBox();
            this.com1ToAppTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.sendToAppButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // appToCom1TextBox
            // 
            this.appToCom1TextBox.Location = new System.Drawing.Point(12, 25);
            this.appToCom1TextBox.Multiline = true;
            this.appToCom1TextBox.Name = "appToCom1TextBox";
            this.appToCom1TextBox.ReadOnly = true;
            this.appToCom1TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.appToCom1TextBox.Size = new System.Drawing.Size(454, 121);
            this.appToCom1TextBox.TabIndex = 1;
            this.appToCom1TextBox.WordWrap = false;
            // 
            // com1ToAppTextBox
            // 
            this.com1ToAppTextBox.Location = new System.Drawing.Point(12, 185);
            this.com1ToAppTextBox.Multiline = true;
            this.com1ToAppTextBox.Name = "com1ToAppTextBox";
            this.com1ToAppTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.com1ToAppTextBox.Size = new System.Drawing.Size(454, 121);
            this.com1ToAppTextBox.TabIndex = 3;
            this.com1ToAppTextBox.WordWrap = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Application -> COM1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 169);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "COM1 -> Application";
            // 
            // sendToAppButton
            // 
            this.sendToAppButton.Location = new System.Drawing.Point(15, 312);
            this.sendToAppButton.Name = "sendToAppButton";
            this.sendToAppButton.Size = new System.Drawing.Size(159, 34);
            this.sendToAppButton.TabIndex = 4;
            this.sendToAppButton.Text = "Send to Application";
            this.sendToAppButton.UseVisualStyleBackColor = true;
            this.sendToAppButton.Click += new System.EventHandler(this.sendToAppButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 358);
            this.Controls.Add(this.sendToAppButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.com1ToAppTextBox);
            this.Controls.Add(this.appToCom1TextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Serial Port Emulator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox appToCom1TextBox;
        private System.Windows.Forms.TextBox com1ToAppTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button sendToAppButton;
    }
}

