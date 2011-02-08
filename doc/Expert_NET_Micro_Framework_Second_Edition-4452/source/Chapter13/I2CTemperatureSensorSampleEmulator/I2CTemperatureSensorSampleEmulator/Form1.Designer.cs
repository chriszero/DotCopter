namespace I2CTemperatureSensorSampleEmulator
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
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label1;
            this.temperatureTrackBar = new System.Windows.Forms.TrackBar();
            this.temperatureLabel = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.temperatureTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(125, 44);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(70, 13);
            label2.TabIndex = 2;
            label2.Text = "Temperature:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(30, 44);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(38, 13);
            label3.TabIndex = 3;
            label3.Text = "+128 °";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(30, 255);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(35, 13);
            label4.TabIndex = 4;
            label4.Text = "-128 °";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 9);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(303, 13);
            label1.TabIndex = 5;
            label1.Text = "Emulation of a Texas Instruments TMP100 temperature sensor.";
            // 
            // temperatureTrackBar
            // 
            this.temperatureTrackBar.Location = new System.Drawing.Point(33, 60);
            this.temperatureTrackBar.Maximum = 127999;
            this.temperatureTrackBar.Minimum = -128000;
            this.temperatureTrackBar.Name = "temperatureTrackBar";
            this.temperatureTrackBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.temperatureTrackBar.Size = new System.Drawing.Size(45, 195);
            this.temperatureTrackBar.TabIndex = 0;
            this.temperatureTrackBar.TickFrequency = 128000;
            this.temperatureTrackBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.temperatureTrackBar.Scroll += new System.EventHandler(this.temperatureTrackBar_Scroll);
            // 
            // temperatureLabel
            // 
            this.temperatureLabel.AutoSize = true;
            this.temperatureLabel.Location = new System.Drawing.Point(125, 60);
            this.temperatureLabel.Name = "temperatureLabel";
            this.temperatureLabel.Size = new System.Drawing.Size(89, 13);
            this.temperatureLabel.TabIndex = 1;
            this.temperatureLabel.Text = "temperatureLabel";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 285);
            this.Controls.Add(label1);
            this.Controls.Add(label4);
            this.Controls.Add(label3);
            this.Controls.Add(label2);
            this.Controls.Add(this.temperatureLabel);
            this.Controls.Add(this.temperatureTrackBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "I2C Temperature Sensor Emulator";
            ((System.ComponentModel.ISupportInitialize)(this.temperatureTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar temperatureTrackBar;
        private System.Windows.Forms.Label temperatureLabel;
    }
}

