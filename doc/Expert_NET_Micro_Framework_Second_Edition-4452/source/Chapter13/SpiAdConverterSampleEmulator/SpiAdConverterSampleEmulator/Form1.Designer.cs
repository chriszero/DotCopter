namespace SpiAdConverterSampleEmulator
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
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label1;
            this.maxVoltageLabel = new System.Windows.Forms.Label();
            this.voltageTrackBar = new System.Windows.Forms.TrackBar();
            this.voltageLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.voltageTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(125, 76);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(46, 13);
            label2.TabIndex = 2;
            label2.Text = "Voltage:";
            // 
            // maxVoltageLabel
            // 
            this.maxVoltageLabel.AutoSize = true;
            this.maxVoltageLabel.Location = new System.Drawing.Point(30, 76);
            this.maxVoltageLabel.Name = "maxVoltageLabel";
            this.maxVoltageLabel.Size = new System.Drawing.Size(88, 13);
            this.maxVoltageLabel.TabIndex = 3;
            this.maxVoltageLabel.Text = "maxVoltageLabel";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(30, 287);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(34, 13);
            label4.TabIndex = 4;
            label4.Text = "0 Volt";
            // 
            // label1
            // 
            label1.Location = new System.Drawing.Point(12, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(306, 35);
            label1.TabIndex = 5;
            label1.Text = "Emulation of a the ADC124S101, a 4 channel 12-bit analog digital converter from N" +
                "ational Semiconductor.";
            // 
            // voltageTrackBar
            // 
            this.voltageTrackBar.Location = new System.Drawing.Point(33, 92);
            this.voltageTrackBar.Name = "voltageTrackBar";
            this.voltageTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.voltageTrackBar.Size = new System.Drawing.Size(45, 195);
            this.voltageTrackBar.TabIndex = 0;
            this.voltageTrackBar.TickFrequency = 1000;
            this.voltageTrackBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.voltageTrackBar.Scroll += new System.EventHandler(this.voltageTrackBar_Scroll);
            // 
            // voltageLabel
            // 
            this.voltageLabel.AutoSize = true;
            this.voltageLabel.Location = new System.Drawing.Point(125, 92);
            this.voltageLabel.Name = "voltageLabel";
            this.voltageLabel.Size = new System.Drawing.Size(68, 13);
            this.voltageLabel.TabIndex = 1;
            this.voltageLabel.Text = "voltageLabel";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "First channel:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 315);
            this.Controls.Add(this.label3);
            this.Controls.Add(label1);
            this.Controls.Add(label4);
            this.Controls.Add(this.maxVoltageLabel);
            this.Controls.Add(label2);
            this.Controls.Add(this.voltageLabel);
            this.Controls.Add(this.voltageTrackBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SPI Analog Digital Converter Emulator";
            ((System.ComponentModel.ISupportInitialize)(this.voltageTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar voltageTrackBar;
        private System.Windows.Forms.Label voltageLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label maxVoltageLabel;
    }
}

