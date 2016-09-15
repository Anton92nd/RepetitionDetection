using System.Text;
using System.Windows;
using Microsoft.Win32;
using RepetitionDetection.CharGenerators;
using RepetitionDetection.Commons;
using RepetitionDetection.Detection;
using RepetitionDetection.Logging;
using RepetitionDetection.TextGeneration.RemoveStrategies;

namespace GraphicalInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Title = "Repetition-free word generator";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var detector = GetDetector();
                var charGenerator = GetCharGenerator(detector.Text);
                var removeStrategy = GetRemoveStrategy(detector.E);
                int length, runsCount;
                if (!int.TryParse(TextBoxLength.Text, out length) || length <= 0)
                    Raise("Length must be positive integer");
                if (!int.TryParse(TextBoxRunsCount.Text, out runsCount) || runsCount <= 0)
                    Raise("Runscount must be positive integer");
                TextBlockErrorMessage.Text = string.Empty;
                var save = CheckBoxSave.IsChecked ?? false;
                var saveData = new SaveData
                {
                    SavePath = TextBoxOutputPath.Text,
                    SaveReps = (CheckBoxReps.IsChecked ?? false) && save,
                    SaveTime = (CheckBoxTime.IsChecked ?? false) && save,
                };
                TextBlockErrorMessage.Text = string.Empty;
                var runWindow = new RunWindow(detector, removeStrategy, charGenerator, saveData, length, runsCount);
                runWindow.ShowDialog();
            }
            catch(InputDataException)
            {
            }
        }

        private IRemoveStrategy GetRemoveStrategy(RationalNumber e)
        {
            var index = ComboBoxRemoveStrategy.SelectedIndex;
            if (index < 0 || index > 2)
                Raise("Select char generator");
            if (index == 0)
                return new RemoveBorderStrategy();
            int periodsCount;
            if (!int.TryParse(TextBoxPeriodsCount.Text, out periodsCount))
                Raise("Periods count must be integer");
            if (periodsCount <= 0)
                Raise("Periods count must be positive");
            if (new RationalNumber(periodsCount) >= e)
                Raise("Periods count must be less than exponent");
            return new RemovePeriodMultipleStrategy(periodsCount);
        }

        private ICharGenerator GetCharGenerator(StringBuilder text)
        {
            int alphabetSize;
            if (!int.TryParse(TextBoxAlphabet.Text, out alphabetSize))
            {
                Raise("Alphabet size must be integer");
            }
            var index = ComboBoxCharGenerator.SelectedIndex;
            if (index < 0 || index > 2)
                Raise("Select char generator");
            switch (index)
            {
                case 0:
                    return new RandomCharGenerator(alphabetSize);
                case 1:
                    return new RandomNotLastCharGenerator(text, alphabetSize);
                default:
                    return new BinaryCharGenerator(text, alphabetSize);
            }
        }

        private Detector GetDetector()
        {
            var exponent = GetExponent();
            var detectEqual = CheckBoxDetectEqual.IsChecked ?? false;
            return new RepetitionDetector(new StringBuilder(), exponent, detectEqual);
        }

        private RationalNumber GetExponent()
        {
            int numerator, denominator;
            if (!int.TryParse(TextBoxNumerator.Text, out numerator))
            {
                Raise("Numerator must be integer");
            }
            if (!int.TryParse(TextBoxDenominator.Text, out denominator))
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
                Raise("Exponent must be greater than 1");
            }
            return result;
        }

        private void Raise(string message)
        {
            TextBlockErrorMessage.Text = message;
            throw new InputDataException(message);
        }

        private void CharGenerator_OnLoaded(object sender, RoutedEventArgs e)
        {
            ComboBoxCharGenerator.ItemsSource = new[] {"Random uniform", "Random uniform, except last symbol", "Binary"};
            ComboBoxCharGenerator.SelectedIndex = 0;
        }

        private void RemoveStrategy_OnLoaded(object sender, RoutedEventArgs e)
        {
            ComboBoxRemoveStrategy.ItemsSource = new[] {"Remove border", "Remove period(s)"};
            ComboBoxRemoveStrategy.SelectedIndex = 0;
        }

        private void ComboBoxRemoveStrategy_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ComboBoxRemoveStrategy.SelectedIndex == 0)
            {
                TextBlockPeriodsCount.Visibility = Visibility.Hidden;
                TextBoxPeriodsCount.Visibility = Visibility.Hidden;
            }
            else
            {
                TextBlockPeriodsCount.Visibility = Visibility.Visible;
                TextBoxPeriodsCount.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                TextBoxOutputPath.Text = saveFileDialog.FileName;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ButtonChangePath.Visibility = Visibility.Visible;
            TextBoxOutputPath.Visibility = Visibility.Visible;
            CheckBoxReps.Visibility = Visibility.Visible;
            CheckBoxTime.Visibility = Visibility.Visible;
        }

        private void CheckBoxSave_OnUnchecked(object sender, RoutedEventArgs e)
        {
            ButtonChangePath.Visibility = Visibility.Hidden;
            TextBoxOutputPath.Visibility = Visibility.Hidden;
            CheckBoxReps.Visibility = Visibility.Hidden;
            CheckBoxTime.Visibility = Visibility.Hidden;
        }
    }
}
