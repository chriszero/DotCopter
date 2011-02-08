namespace GpioPortSampleEmulator
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
            this.gpioOut0CheckBox = new System.Windows.Forms.CheckBox();
            this.gpioOut1CheckBox = new System.Windows.Forms.CheckBox();
            this.gpioIn0CheckBox = new System.Windows.Forms.CheckBox();
            this.gpioIn1Button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // gpioOut0CheckBox
            // 
            this.gpioOut0CheckBox.AutoSize = true;
            this.gpioOut0CheckBox.Enabled = false;
            this.gpioOut0CheckBox.Location = new System.Drawing.Point(12, 12);
            this.gpioOut0CheckBox.Name = "gpioOut0CheckBox";
            this.gpioOut0CheckBox.Size = new System.Drawing.Size(118, 17);
            this.gpioOut0CheckBox.TabIndex = 0;
            this.gpioOut0CheckBox.Text = "gpioOut0CheckBox";
            this.gpioOut0CheckBox.UseVisualStyleBackColor = true;
            // 
            // gpioOut1CheckBox
            // 
            this.gpioOut1CheckBox.AutoSize = true;
            this.gpioOut1CheckBox.Enabled = false;
            this.gpioOut1CheckBox.Location = new System.Drawing.Point(12, 35);
            this.gpioOut1CheckBox.Name = "gpioOut1CheckBox";
            this.gpioOut1CheckBox.Size = new System.Drawing.Size(118, 17);
            this.gpioOut1CheckBox.TabIndex = 1;
            this.gpioOut1CheckBox.Text = "gpioOut1CheckBox";
            this.gpioOut1CheckBox.UseVisualStyleBackColor = true;
            // 
            // gpioIn0CheckBox
            // 
            this.gpioIn0CheckBox.AutoSize = true;
            this.gpioIn0CheckBox.Location = new System.Drawing.Point(12, 80);
            this.gpioIn0CheckBox.Name = "gpioIn0CheckBox";
            this.gpioIn0CheckBox.Size = new System.Drawing.Size(110, 17);
            this.gpioIn0CheckBox.TabIndex = 2;
            this.gpioIn0CheckBox.Text = "gpioIn0CheckBox";
            this.gpioIn0CheckBox.UseVisualStyleBackColor = true;
            this.gpioIn0CheckBox.CheckedChanged += new System.EventHandler(this.gpioIn0CheckBox_CheckedChanged);
            // 
            // gpioIn1Button
            // 
            this.gpioIn1Button.Location = new System.Drawing.Point(12, 103);
            this.gpioIn1Button.Name = "gpioIn1Button";
            this.gpioIn1Button.Size = new System.Drawing.Size(153, 27);
            this.gpioIn1Button.TabIndex = 3;
            this.gpioIn1Button.Text = "gpioIn1Button";
            this.gpioIn1Button.UseVisualStyleBackColor = true;
            this.gpioIn1Button.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gpioIn1Button_MouseDown);
            this.gpioIn1Button.MouseUp += new System.Windows.Forms.MouseEventHandler(this.gpioIn1Button_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(223, 143);
            this.Controls.Add(this.gpioIn1Button);
            this.Controls.Add(this.gpioIn0CheckBox);
            this.Controls.Add(this.gpioOut1CheckBox);
            this.Controls.Add(this.gpioOut0CheckBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GPIO Port Emulator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox gpioOut0CheckBox;
        private System.Windows.Forms.CheckBox gpioOut1CheckBox;
        private System.Windows.Forms.CheckBox gpioIn0CheckBox;
        private System.Windows.Forms.Button gpioIn1Button;
    }
}

