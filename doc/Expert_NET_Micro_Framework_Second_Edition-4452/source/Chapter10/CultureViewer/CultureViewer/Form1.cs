using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.IO;

namespace CultureViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
            this.comboBox1.Items.AddRange(cultures);
            this.comboBox1.SelectedItem = CultureInfo.CurrentCulture;
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            DisplayCulture((CultureInfo)this.comboBox1.SelectedItem);
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                CultureInfo culture = (CultureInfo)this.comboBox1.SelectedItem;
                dlg.DefaultExt = ".resx";
                dlg.Filter = "Resource File (*.resx)|*.resx";
                dlg.FileName = culture.Name;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ExportCulture(culture, dlg.FileName);
                }
            }
        }

        private void DisplayCulture(CultureInfo culture)
        {
            this.SuspendLayout();

            this.nameLabel.Text = culture.Name;
            this.neutralCultureLabel.Text = culture.IsNeutralCulture ? "(neutral Culture)" : string.Empty;

            bool isVisible = !culture.IsNeutralCulture;

            this.numberDecimalSeparatorLabel.Visible = isVisible;
            this.numberGroupSeparatorLabel.Visible = isVisible;
            this.numberGroupSizesLabel.Visible = isVisible;
            this.negativeSignLabel.Visible = isVisible;
            this.positiveSignLabel.Visible = isVisible;

            this.dateSeparatorLabel.Visible = isVisible;
            this.longDatePatternLabel.Visible = isVisible;
            this.shortDatePatternLabel.Visible = isVisible;
            this.yearMonthPatternLabel.Visible = isVisible;
            this.monthDayPatternLabel.Visible = isVisible;
            this.monthNamesLabel.Visible = isVisible;
            this.abbreviatedMonthNamesLabel.Visible = isVisible;
            this.dayNamesLabel.Visible = isVisible;
            this.abbreviatedDayNamesLabel.Visible = isVisible;

            this.timeSeparatorLabel.Visible = isVisible;
            this.longTimePatternLabel.Visible = isVisible;
            this.shortTimePatternLabel.Visible = isVisible;
            this.amDesignatorLabel.Visible = isVisible;
            this.pmDesignatorLabel.Visible = isVisible;

            this.exportButton.Enabled = !culture.IsNeutralCulture;

            if (!culture.IsNeutralCulture)
            {
                NumberFormatInfo numberFormat = culture.NumberFormat;
                //number format
                this.numberDecimalSeparatorLabel.Text = numberFormat.NumberDecimalSeparator;
                this.numberGroupSeparatorLabel.Text = numberFormat.NumberGroupSeparator;
                string numberGroupSizes = string.Empty;
                foreach (int numberGroupSize in numberFormat.NumberGroupSizes)
                    numberGroupSizes += numberGroupSize.ToString(CultureInfo.InvariantCulture) + "|";
                this.numberGroupSizesLabel.Text = numberGroupSizes.TrimEnd('|');
                this.negativeSignLabel.Text = numberFormat.NegativeSign;
                this.positiveSignLabel.Text = numberFormat.PositiveSign;

                DateTimeFormatInfo dateTimeFormat = culture.DateTimeFormat;
                //date format
                this.dateSeparatorLabel.Text = dateTimeFormat.DateSeparator;
                this.longDatePatternLabel.Text = dateTimeFormat.LongDatePattern;
                this.shortDatePatternLabel.Text = dateTimeFormat.ShortDatePattern;
                this.yearMonthPatternLabel.Text = dateTimeFormat.YearMonthPattern;
                this.monthDayPatternLabel.Text = dateTimeFormat.MonthDayPattern;
                this.monthNamesLabel.Text = string.Join("|", dateTimeFormat.MonthNames).TrimEnd('|');
                this.abbreviatedMonthNamesLabel.Text = string.Join("|", dateTimeFormat.AbbreviatedMonthNames).TrimEnd('|');
                this.dayNamesLabel.Text = string.Join("|", dateTimeFormat.DayNames).TrimEnd('|');
                this.abbreviatedDayNamesLabel.Text = string.Join("|", dateTimeFormat.AbbreviatedDayNames).TrimEnd('|');
            
                //time format
                this.timeSeparatorLabel.Text = dateTimeFormat.TimeSeparator;
                this.longTimePatternLabel.Text = dateTimeFormat.LongTimePattern;
                this.shortTimePatternLabel.Text = dateTimeFormat.ShortTimePattern;
                this.amDesignatorLabel.Text = dateTimeFormat.AMDesignator;
                this.pmDesignatorLabel.Text = dateTimeFormat.PMDesignator;
            }
            this.ResumeLayout(true);
        }

        private void ExportCulture(CultureInfo culture, string path)
        {
            using (TextWriter w = new StreamWriter(path))
            {
                w.WriteLine(Properties.Resources.ResourceSkeleton);
           
                NumberFormatInfo numberFormat = culture.NumberFormat;
                //number format
                WriteData(w, "NumberDecimalSeparator", numberFormat.NumberDecimalSeparator);
                WriteData(w, "NumberGroupSeparator", numberFormat.NumberGroupSeparator);
                string numberGroupSizes = string.Empty;
                foreach (int numberGroupSize in numberFormat.NumberGroupSizes)
                    numberGroupSizes += numberGroupSize.ToString(CultureInfo.InvariantCulture) + "|";
                WriteData(w, "NumberGroupSizes", numberGroupSizes.TrimEnd('|'));
                WriteData(w, "NegativeSign", numberFormat.NegativeSign);
                WriteData(w, "PositiveSign", numberFormat.PositiveSign);

                DateTimeFormatInfo dateTimeFormat = culture.DateTimeFormat;
                //date format
                WriteData(w, "DateSeparator", dateTimeFormat.DateSeparator);
                WriteData(w, "LongDatePattern", dateTimeFormat.LongDatePattern);
                WriteData(w, "ShortDatePattern", dateTimeFormat.ShortDatePattern);
                WriteData(w, "YearMonthPattern", dateTimeFormat.YearMonthPattern);
                WriteData(w, "MonthDayPattern", dateTimeFormat.MonthDayPattern);
                WriteData(w, "MonthNames", string.Join("|", dateTimeFormat.MonthNames).TrimEnd('|'));
                WriteData(w, "AbbreviatedMonthNames", string.Join("|", dateTimeFormat.AbbreviatedMonthNames).TrimEnd('|'));
                WriteData(w, "DayNames", string.Join("|", dateTimeFormat.DayNames).TrimEnd('|'));
                WriteData(w, "AbbreviatedDayNames", string.Join("|", dateTimeFormat.AbbreviatedDayNames).TrimEnd('|'));

                //time format
                WriteData(w, "TimeSeparator", dateTimeFormat.TimeSeparator);
                WriteData(w, "LongTimePattern", dateTimeFormat.LongTimePattern);
                WriteData(w, "ShortTimePattern", dateTimeFormat.ShortTimePattern);
                WriteData(w, "AMDesignator", dateTimeFormat.AMDesignator);
                WriteData(w, "PMDesignator", dateTimeFormat.PMDesignator);

                w.WriteLine(" </root>");
            }
        }

        private static void WriteData(TextWriter w, string name, string value)
        {
            w.WriteLine("  <data name=\"{0}\" xml:space=\"preserve\">", name);
            w.WriteLine("    <value>{0}</value>", value);
            w.WriteLine("  </data>");
        }
    }
}