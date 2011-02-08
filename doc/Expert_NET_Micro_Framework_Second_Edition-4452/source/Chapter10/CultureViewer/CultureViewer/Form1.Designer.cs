namespace CultureViewer
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.numberDecimalSeparatorLabel = new System.Windows.Forms.Label();
            this.numberGroupSeparatorLabel = new System.Windows.Forms.Label();
            this.negativeSignLabel = new System.Windows.Forms.Label();
            this.positiveSignLabel = new System.Windows.Forms.Label();
            this.neutralCultureLabel = new System.Windows.Forms.Label();
            this.dateSeparatorLabel = new System.Windows.Forms.Label();
            this.longDatePatternLabel = new System.Windows.Forms.Label();
            this.shortDatePatternLabel = new System.Windows.Forms.Label();
            this.yearMonthPatternLabel = new System.Windows.Forms.Label();
            this.monthDayPatternLabel = new System.Windows.Forms.Label();
            this.monthNamesLabel = new System.Windows.Forms.Label();
            this.abbreviatedMonthNamesLabel = new System.Windows.Forms.Label();
            this.dayNamesLabel = new System.Windows.Forms.Label();
            this.abbreviatedDayNamesLabel = new System.Windows.Forms.Label();
            this.numberGroupSizesLabel = new System.Windows.Forms.Label();
            this.timeSeparatorLabel = new System.Windows.Forms.Label();
            this.longTimePatternLabel = new System.Windows.Forms.Label();
            this.shortTimePatternLabel = new System.Windows.Forms.Label();
            this.amDesignatorLabel = new System.Windows.Forms.Label();
            this.pmDesignatorLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.exportButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(158, 12);
            this.comboBox1.MaxDropDownItems = 15;
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(516, 21);
            this.comboBox1.Sorted = true;
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedValueChanged += new System.EventHandler(this.comboBox1_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Name:";
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLabel.Location = new System.Drawing.Point(155, 62);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(68, 13);
            this.nameLabel.TabIndex = 3;
            this.nameLabel.Text = "nameLabel";
            // 
            // numberDecimalSeparatorLabel
            // 
            this.numberDecimalSeparatorLabel.AutoSize = true;
            this.numberDecimalSeparatorLabel.Location = new System.Drawing.Point(155, 103);
            this.numberDecimalSeparatorLabel.Name = "numberDecimalSeparatorLabel";
            this.numberDecimalSeparatorLabel.Size = new System.Drawing.Size(152, 13);
            this.numberDecimalSeparatorLabel.TabIndex = 4;
            this.numberDecimalSeparatorLabel.Text = "numberDecimalSeparatorLabel";
            // 
            // numberGroupSeparatorLabel
            // 
            this.numberGroupSeparatorLabel.AutoSize = true;
            this.numberGroupSeparatorLabel.Location = new System.Drawing.Point(155, 128);
            this.numberGroupSeparatorLabel.Name = "numberGroupSeparatorLabel";
            this.numberGroupSeparatorLabel.Size = new System.Drawing.Size(143, 13);
            this.numberGroupSeparatorLabel.TabIndex = 5;
            this.numberGroupSeparatorLabel.Text = "numberGroupSeparatorLabel";
            // 
            // negativeSignLabel
            // 
            this.negativeSignLabel.AutoSize = true;
            this.negativeSignLabel.Location = new System.Drawing.Point(155, 173);
            this.negativeSignLabel.Name = "negativeSignLabel";
            this.negativeSignLabel.Size = new System.Drawing.Size(95, 13);
            this.negativeSignLabel.TabIndex = 6;
            this.negativeSignLabel.Text = "negativeSignLabel";
            // 
            // positiveSignLabel
            // 
            this.positiveSignLabel.AutoSize = true;
            this.positiveSignLabel.Location = new System.Drawing.Point(155, 196);
            this.positiveSignLabel.Name = "positiveSignLabel";
            this.positiveSignLabel.Size = new System.Drawing.Size(90, 13);
            this.positiveSignLabel.TabIndex = 7;
            this.positiveSignLabel.Text = "positiveSignLabel";
            // 
            // neutralCultureLabel
            // 
            this.neutralCultureLabel.AutoSize = true;
            this.neutralCultureLabel.Location = new System.Drawing.Point(241, 62);
            this.neutralCultureLabel.Name = "neutralCultureLabel";
            this.neutralCultureLabel.Size = new System.Drawing.Size(98, 13);
            this.neutralCultureLabel.TabIndex = 8;
            this.neutralCultureLabel.Text = "neutralCultureLabel";
            // 
            // dateSeparatorLabel
            // 
            this.dateSeparatorLabel.AutoSize = true;
            this.dateSeparatorLabel.Location = new System.Drawing.Point(155, 242);
            this.dateSeparatorLabel.Name = "dateSeparatorLabel";
            this.dateSeparatorLabel.Size = new System.Drawing.Size(100, 13);
            this.dateSeparatorLabel.TabIndex = 9;
            this.dateSeparatorLabel.Text = "dateSeparatorLabel";
            // 
            // longDatePatternLabel
            // 
            this.longDatePatternLabel.AutoSize = true;
            this.longDatePatternLabel.Location = new System.Drawing.Point(155, 265);
            this.longDatePatternLabel.Name = "longDatePatternLabel";
            this.longDatePatternLabel.Size = new System.Drawing.Size(110, 13);
            this.longDatePatternLabel.TabIndex = 10;
            this.longDatePatternLabel.Text = "longDatePatternLabel";
            // 
            // shortDatePatternLabel
            // 
            this.shortDatePatternLabel.AutoSize = true;
            this.shortDatePatternLabel.Location = new System.Drawing.Point(155, 287);
            this.shortDatePatternLabel.Name = "shortDatePatternLabel";
            this.shortDatePatternLabel.Size = new System.Drawing.Size(113, 13);
            this.shortDatePatternLabel.TabIndex = 11;
            this.shortDatePatternLabel.Text = "shortDatePatternLabel";
            // 
            // yearMonthPatternLabel
            // 
            this.yearMonthPatternLabel.AutoSize = true;
            this.yearMonthPatternLabel.Location = new System.Drawing.Point(155, 311);
            this.yearMonthPatternLabel.Name = "yearMonthPatternLabel";
            this.yearMonthPatternLabel.Size = new System.Drawing.Size(117, 13);
            this.yearMonthPatternLabel.TabIndex = 12;
            this.yearMonthPatternLabel.Text = "yearMonthPatternLabel";
            // 
            // monthDayPatternLabel
            // 
            this.monthDayPatternLabel.AutoSize = true;
            this.monthDayPatternLabel.Location = new System.Drawing.Point(155, 334);
            this.monthDayPatternLabel.Name = "monthDayPatternLabel";
            this.monthDayPatternLabel.Size = new System.Drawing.Size(115, 13);
            this.monthDayPatternLabel.TabIndex = 13;
            this.monthDayPatternLabel.Text = "monthDayPatternLabel";
            // 
            // monthNamesLabel
            // 
            this.monthNamesLabel.AutoSize = true;
            this.monthNamesLabel.Location = new System.Drawing.Point(155, 357);
            this.monthNamesLabel.Name = "monthNamesLabel";
            this.monthNamesLabel.Size = new System.Drawing.Size(95, 13);
            this.monthNamesLabel.TabIndex = 14;
            this.monthNamesLabel.Text = "monthNamesLabel";
            // 
            // abbreviatedMonthNamesLabel
            // 
            this.abbreviatedMonthNamesLabel.AutoSize = true;
            this.abbreviatedMonthNamesLabel.Location = new System.Drawing.Point(155, 380);
            this.abbreviatedMonthNamesLabel.Name = "abbreviatedMonthNamesLabel";
            this.abbreviatedMonthNamesLabel.Size = new System.Drawing.Size(152, 13);
            this.abbreviatedMonthNamesLabel.TabIndex = 15;
            this.abbreviatedMonthNamesLabel.Text = "abbreviatedMonthNamesLabel";
            // 
            // dayNamesLabel
            // 
            this.dayNamesLabel.AutoSize = true;
            this.dayNamesLabel.Location = new System.Drawing.Point(155, 403);
            this.dayNamesLabel.Name = "dayNamesLabel";
            this.dayNamesLabel.Size = new System.Drawing.Size(83, 13);
            this.dayNamesLabel.TabIndex = 16;
            this.dayNamesLabel.Text = "dayNamesLabel";
            // 
            // abbreviatedDayNamesLabel
            // 
            this.abbreviatedDayNamesLabel.AutoSize = true;
            this.abbreviatedDayNamesLabel.Location = new System.Drawing.Point(155, 426);
            this.abbreviatedDayNamesLabel.Name = "abbreviatedDayNamesLabel";
            this.abbreviatedDayNamesLabel.Size = new System.Drawing.Size(141, 13);
            this.abbreviatedDayNamesLabel.TabIndex = 17;
            this.abbreviatedDayNamesLabel.Text = "abbreviatedDayNamesLabel";
            // 
            // numberGroupSizesLabel
            // 
            this.numberGroupSizesLabel.AutoSize = true;
            this.numberGroupSizesLabel.Location = new System.Drawing.Point(155, 150);
            this.numberGroupSizesLabel.Name = "numberGroupSizesLabel";
            this.numberGroupSizesLabel.Size = new System.Drawing.Size(122, 13);
            this.numberGroupSizesLabel.TabIndex = 18;
            this.numberGroupSizesLabel.Text = "numberGroupSizesLabel";
            // 
            // timeSeparatorLabel
            // 
            this.timeSeparatorLabel.AutoSize = true;
            this.timeSeparatorLabel.Location = new System.Drawing.Point(155, 471);
            this.timeSeparatorLabel.Name = "timeSeparatorLabel";
            this.timeSeparatorLabel.Size = new System.Drawing.Size(98, 13);
            this.timeSeparatorLabel.TabIndex = 19;
            this.timeSeparatorLabel.Text = "timeSeparatorLabel";
            // 
            // longTimePatternLabel
            // 
            this.longTimePatternLabel.AutoSize = true;
            this.longTimePatternLabel.Location = new System.Drawing.Point(155, 494);
            this.longTimePatternLabel.Name = "longTimePatternLabel";
            this.longTimePatternLabel.Size = new System.Drawing.Size(110, 13);
            this.longTimePatternLabel.TabIndex = 20;
            this.longTimePatternLabel.Text = "longTimePatternLabel";
            // 
            // shortTimePatternLabel
            // 
            this.shortTimePatternLabel.AutoSize = true;
            this.shortTimePatternLabel.Location = new System.Drawing.Point(155, 516);
            this.shortTimePatternLabel.Name = "shortTimePatternLabel";
            this.shortTimePatternLabel.Size = new System.Drawing.Size(113, 13);
            this.shortTimePatternLabel.TabIndex = 21;
            this.shortTimePatternLabel.Text = "shortTimePatternLabel";
            // 
            // amDesignatorLabel
            // 
            this.amDesignatorLabel.AutoSize = true;
            this.amDesignatorLabel.Location = new System.Drawing.Point(155, 539);
            this.amDesignatorLabel.Name = "amDesignatorLabel";
            this.amDesignatorLabel.Size = new System.Drawing.Size(98, 13);
            this.amDesignatorLabel.TabIndex = 22;
            this.amDesignatorLabel.Text = "amDesignatorLabel";
            // 
            // pmDesignatorLabel
            // 
            this.pmDesignatorLabel.AutoSize = true;
            this.pmDesignatorLabel.Location = new System.Drawing.Point(155, 561);
            this.pmDesignatorLabel.Name = "pmDesignatorLabel";
            this.pmDesignatorLabel.Size = new System.Drawing.Size(98, 13);
            this.pmDesignatorLabel.TabIndex = 23;
            this.pmDesignatorLabel.Text = "pmDesignatorLabel";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Culture:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 13);
            this.label3.TabIndex = 25;
            this.label3.Text = "NumberDecimalSeparator";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "NumberGroupSeparator";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 150);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "NumberGroupSizes";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 173);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 13);
            this.label6.TabIndex = 28;
            this.label6.Text = "NegativeSign";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 196);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 29;
            this.label7.Text = "PositiveSign";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 242);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 13);
            this.label8.TabIndex = 30;
            this.label8.Text = "DateSeparator";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 265);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 13);
            this.label9.TabIndex = 31;
            this.label9.Text = "LongDatePattern";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 287);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 13);
            this.label10.TabIndex = 32;
            this.label10.Text = "ShortDatePattern";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 311);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(93, 13);
            this.label11.TabIndex = 33;
            this.label11.Text = "YearMonthPattern";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 380);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(127, 13);
            this.label12.TabIndex = 34;
            this.label12.Text = "AbbreviatedMonthNames";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(12, 334);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(90, 13);
            this.label13.TabIndex = 35;
            this.label13.Text = "MonthDayPattern";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(12, 357);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(70, 13);
            this.label14.TabIndex = 36;
            this.label14.Text = "MonthNames";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(12, 403);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(59, 13);
            this.label15.TabIndex = 37;
            this.label15.Text = "DayNames";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(12, 426);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(116, 13);
            this.label16.TabIndex = 38;
            this.label16.Text = "AbbreviatedDayNames";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(14, 471);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(76, 13);
            this.label17.TabIndex = 39;
            this.label17.Text = "TimeSeparator";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(14, 494);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(88, 13);
            this.label18.TabIndex = 40;
            this.label18.Text = "LongTimePattern";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(14, 516);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(89, 13);
            this.label19.TabIndex = 41;
            this.label19.Text = "ShortTimePattern";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(14, 539);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(74, 13);
            this.label20.TabIndex = 42;
            this.label20.Text = "AMDesignator";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(14, 561);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(74, 13);
            this.label21.TabIndex = 43;
            this.label21.Text = "PMDesignator";
            // 
            // exportButton
            // 
            this.exportButton.Location = new System.Drawing.Point(593, 532);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(127, 42);
            this.exportButton.TabIndex = 44;
            this.exportButton.Text = "Export as resx...";
            this.exportButton.UseVisualStyleBackColor = true;
            this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(732, 594);
            this.Controls.Add(this.exportButton);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pmDesignatorLabel);
            this.Controls.Add(this.amDesignatorLabel);
            this.Controls.Add(this.shortTimePatternLabel);
            this.Controls.Add(this.longTimePatternLabel);
            this.Controls.Add(this.timeSeparatorLabel);
            this.Controls.Add(this.numberGroupSizesLabel);
            this.Controls.Add(this.abbreviatedDayNamesLabel);
            this.Controls.Add(this.dayNamesLabel);
            this.Controls.Add(this.abbreviatedMonthNamesLabel);
            this.Controls.Add(this.monthNamesLabel);
            this.Controls.Add(this.monthDayPatternLabel);
            this.Controls.Add(this.yearMonthPatternLabel);
            this.Controls.Add(this.shortDatePatternLabel);
            this.Controls.Add(this.longDatePatternLabel);
            this.Controls.Add(this.dateSeparatorLabel);
            this.Controls.Add(this.neutralCultureLabel);
            this.Controls.Add(this.positiveSignLabel);
            this.Controls.Add(this.negativeSignLabel);
            this.Controls.Add(this.numberGroupSeparatorLabel);
            this.Controls.Add(this.numberDecimalSeparatorLabel);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Culture Viewer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label numberDecimalSeparatorLabel;
        private System.Windows.Forms.Label numberGroupSeparatorLabel;
        private System.Windows.Forms.Label negativeSignLabel;
        private System.Windows.Forms.Label positiveSignLabel;
        private System.Windows.Forms.Label neutralCultureLabel;
        private System.Windows.Forms.Label dateSeparatorLabel;
        private System.Windows.Forms.Label longDatePatternLabel;
        private System.Windows.Forms.Label shortDatePatternLabel;
        private System.Windows.Forms.Label yearMonthPatternLabel;
        private System.Windows.Forms.Label monthDayPatternLabel;
        private System.Windows.Forms.Label monthNamesLabel;
        private System.Windows.Forms.Label abbreviatedMonthNamesLabel;
        private System.Windows.Forms.Label dayNamesLabel;
        private System.Windows.Forms.Label abbreviatedDayNamesLabel;
        private System.Windows.Forms.Label numberGroupSizesLabel;
        private System.Windows.Forms.Label timeSeparatorLabel;
        private System.Windows.Forms.Label longTimePatternLabel;
        private System.Windows.Forms.Label shortTimePatternLabel;
        private System.Windows.Forms.Label amDesignatorLabel;
        private System.Windows.Forms.Label pmDesignatorLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Button exportButton;
    }
}

