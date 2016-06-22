using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using RepetitionDetection.CharGenerators;
using RepetitionDetection.Commons;
using RepetitionDetection.Detection;
using RepetitionDetection.TextGeneration.RemoveStrategies;

namespace RepetitionDetection
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            listBoxCharGenerator.SelectedIndex = 0;
            listBoxStrategy.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                textBoxMessages.BackColor = DefaultBackColor;
                textBoxMessages.ForeColor = Color.Red;
                var detector = GetDetector();
                var charGenerator = GetCharGenerator(detector.Text);
                var removeBorderStrategy = GetRemoveBorderStrategy();
                int length, runsCount;
                if (!int.TryParse(textBoxLength.Text, out length))
                {
                    Raise("Length must be integer");
                }
                if (!int.TryParse(textBoxRunsCount.Text, out runsCount))
                {
                    Raise("Runs count must be integer");
                }
                textBoxMessages.ForeColor = Color.Green;
                textBoxMessages.Text = "Success!";
                var newForm = new RunForm(detector, charGenerator, removeBorderStrategy, length, runsCount);
                newForm.ShowDialog(this);
            }
            catch(Exception)
            {
            }
        }

        private IRemoveStrategy GetRemoveBorderStrategy()
        {
            var index = listBoxStrategy.SelectedIndex;
            if (index < 0 || index > 1)
            {
                Raise("Select repetition removing strategy");
            }
            if (index == 0)
                return new RemoveBorderStrategy();
            int periodsCount;
            if (!int.TryParse(textBoxPeriodsCount.Text, out periodsCount))
            {
                Raise("Periods count must be integer");
            }
            var e = new RationalNumber(int.Parse(textBoxNumerator.Text), int.Parse(textBoxDenominator.Text));
            if (new RationalNumber(periodsCount) >= e)
            {
                Raise("Periods count must be less than exponent");
            }
            return new RemovePeriodMultipleStrategy(periodsCount);
        }

        private ICharGenerator GetCharGenerator(StringBuilder text)
        {
            int alphabetSize;
            if (!int.TryParse(textBoxAlphabetSize.Text, out alphabetSize))
            {
                Raise("Alphabet size must be integer");
            }
            var index = listBoxCharGenerator.SelectedIndex;
            if (index < 0 || index > 2)
            {
                Raise("Char generator is not selected");
            }
            switch (index)
            {
                case 0:
                    return new RandomCharGenerator(alphabetSize);
                case 1:
                    return new BinaryCharGenerator(text, alphabetSize);
                default:
                    return new RandomNotLastCharGenerator(text, alphabetSize);
            }
        }

        private void Raise(string message)
        {
            textBoxMessages.Text = message;
            throw new Exception(message);
        }

        private Detector GetDetector()
        {
            var text = new StringBuilder();
            var exponent = GetExponent();
            var detectEqual = checkBoxDetectEqual.Checked;
            return new RepetitionDetector(text, exponent, detectEqual);
        }

        private RationalNumber GetExponent()
        {
            int numerator, denominator;
            if (!int.TryParse(textBoxNumerator.Text, out numerator))
            {
                Raise("Numerator must be integer");
            }
            if (!int.TryParse(textBoxDenominator.Text, out denominator))
            {
                Raise("Denominator must be integer");
            }
            if (denominator == 0)
            {
                Raise("Denominator can't be equal to 0");
            }
            var result = new RationalNumber(numerator, denominator);
            if (result <= new RationalNumber(1))
            {
                Raise("Exponent must be larger than 1");
            }
            return result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxOutput.Text = saveFileDialog1.FileName;
            }
        }

        private void listBoxStrategy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxStrategy.SelectedIndex == 1)
            {
                labelPeriodsCount.Visible = true;
                textBoxPeriodsCount.Visible = true;
            }
            else
            {
                labelPeriodsCount.Visible = false;
                textBoxPeriodsCount.Visible = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkedListBox1.Visible = checkBox1.Checked;
        }
    }
}
