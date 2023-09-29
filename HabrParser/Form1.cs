using HabrParser.Core;
using HabrParser.Core.Habra;
using System;
using System.Windows.Forms;

namespace HabrParser
{
    public partial class Form1 : Form
    {
        ParserWorker<string[]> parser;

        public Form1()
        {
            InitializeComponent();

            parser = new ParserWorker<string[]>(
                    new HabraParser()
                );

            parser.OnNewData += ParserOnNewData;
            parser.OnCompleted += ParserOnCompleted;

        }

        private void ParserOnNewData(object sender, string[] arg1)
        {
            foreach (var item in arg1)
            {
                ListTitles.Items.Add(item);
                ListTitles.Items.Add("===============================================");
            }
        }


        private void OnNumericValueChange(object sender, EventArgs args)
        {
            NumericEnd.Value = NumericStart.Value > NumericEnd.Value ? NumericStart.Value : NumericEnd.Value;
        }

        private void ParserOnCompleted(object sender)
        {
            MessageBox.Show("Done");
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            ListTitles.Items.Clear();
            parser.Settings = new HabraSettings((int)NumericStart.Value, (int)NumericEnd.Value);
            parser.Start();
        }

        private void EndButton_Click(object sender, EventArgs e)
        {
            parser.Abort();
        }
    }
}
