using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using Newtonsoft.Json;
using RepetitionDetection.CharGenerators;
using RepetitionDetection.Commons;
using RepetitionDetection.Detection;
using RepetitionDetection.Logging;
using RepetitionDetection.TextGeneration.RemoveStrategies;

namespace GraphicalInterface
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MyInitialize();
            Title = "Repetition-free word generator";
        }

        private void MyInitialize()
        {
            if (!TryLoadFromSettings(out initialSettings))
                initialSettings = new InitialSettings
                {
                    SaveData = new SaveData
                    {
                        SavePath = "C:\\runs\\",
                        SaveFullLog = false,
                        SaveStats = true
                    },
                    AlphabetSize = 3,
                    CharGeneratorIndex = 0,
                    DetectEqualToExponent = true,
                    Numerator = 2,
                    Denominator = 1,
                    GeneratedStringLength = 1000,
                    RepetitionRemovingStrategyIndex = 0,
                    RunsCount = 100,
                    RemoveStrategy = new RemoveBorderStrategy()
                };
        }

        private bool TryLoadFromSettings(out InitialSettings settings)
        {
            settings = null;
            if (!File.Exists(SettingsPath))
                return false;
            try
            {
                settings = JsonConvert.DeserializeObject<InitialSettings>(File.ReadAllText(SettingsPath),
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.Auto
                    });
                if (settings == null)
                    throw new Exception();
                return true;
            }
            catch
            {
            }
            return false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var detector = GetDetector();
                var charGenerator = GetCharGenerator(detector.Text, detector.E, detector.DetectEqual);
                var removeStrategy = GetRemoveStrategy(detector.E);
                if (!int.TryParse(TextBoxLength.Text, out var length) || length <= 0)
                    Raise("Length must be positive integer");
                if (!int.TryParse(TextBoxRunsCount.Text, out var runsCount) || runsCount <= 0)
                    Raise("Runscount must be positive integer");
                if (TextBoxRemovingUpperBound.Visibility == Visibility.Visible)
                    if (!int.TryParse(TextBoxRemovingUpperBound.Text, out var removingUpperBound))
                        Raise("Removing upper bound must be positive integer");
                    else
                        removeStrategy = new NoMoreThanStrategy(removeStrategy, removingUpperBound);
                TextBlockErrorMessage.Text = string.Empty;
                var saveData = new SaveData
                {
                    SavePath = TextBoxOutputPath.Text,
                    SaveStats = CheckBoxStatistics.IsChecked ?? false,
                    SaveFullLog = CheckBoxFullLog.IsChecked ?? false
                };
                TextBlockErrorMessage.Text = string.Empty;
                File.WriteAllText(SettingsPath, JsonConvert.SerializeObject(new InitialSettings
                {
                    AlphabetSize = charGenerator.AlphabetSize,
                    CharGeneratorIndex = ComboBoxCharGenerator.SelectedIndex,
                    SaveData = saveData,
                    RunsCount = runsCount,
                    DetectEqualToExponent = detector.DetectEqual,
                    GeneratedStringLength = length,
                    Numerator = detector.E.Num,
                    Denominator = detector.E.Denom,
                    RepetitionRemovingStrategyIndex = ComboBoxRemoveStrategy.SelectedIndex,
                    RemoveStrategy = removeStrategy
                }, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                }));
                var runWindow = new RunWindow(detector, removeStrategy, charGenerator, saveData, length, runsCount);
                runWindow.ShowDialog();
            }
            catch (InputDataException)
            {
            }
        }

        private IRemoveStrategy GetRemoveStrategy(RationalNumber e)
        {
            var index = ComboBoxRemoveStrategy.SelectedIndex;
            if (index < 0 || index > 3)
                Raise("Select char generator");
            if (index == 0)
                return new RemoveBorderStrategy();
            if (index == 2)
                return new CustomRemoveStrategy();
            if (!int.TryParse(TextBoxPeriodsCount.Text, out var periodsCount))
                Raise("Periods count must be integer");
            if (periodsCount <= 0)
                Raise("Periods count must be positive");
            if (new RationalNumber(periodsCount) >= e)
                Raise("Periods count must be less than exponent");
            return new RemovePeriodsStrategy(periodsCount);
        }

        private ICharGenerator GetCharGenerator(StringBuilder text, RationalNumber E, bool detectEqual)
        {
            if (!int.TryParse(TextBoxAlphabet.Text, out var alphabetSize))
                Raise("Alphabet size must be integer");
            var index = ComboBoxCharGenerator.SelectedIndex;
            if (index < 0 || index > 1)
                Raise("Select char generator");
            switch (index)
            {
                case 0:
                    return new RandomCharGenerator(alphabetSize);
                default:
                    return new CleverCharGenerator(text, alphabetSize, E, detectEqual);
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
            if (!int.TryParse(TextBoxNumerator.Text, out var numerator))
                Raise("Numerator must be integer");
            if (!int.TryParse(TextBoxDenominator.Text, out var denominator))
                Raise("Denominator must be integer");
            if (denominator == 0)
                Raise("Denominator can't be equal to 0");
            var result = new RationalNumber(numerator, denominator);
            if (result <= new RationalNumber(1))
                Raise("Exponent must be greater than 1");
            return result;
        }

        private void Raise(string message)
        {
            TextBlockErrorMessage.Text = message;
            throw new InputDataException(message);
        }

        private void CharGenerator_OnLoaded(object sender, RoutedEventArgs e)
        {
            ComboBoxCharGenerator.ItemsSource = new[] {"Random uniform", "Clever"};
            ComboBoxCharGenerator.SelectedIndex = initialSettings.CharGeneratorIndex;
        }

        private void RemoveStrategy_OnLoaded(object sender, RoutedEventArgs e)
        {
            ComboBoxRemoveStrategy.ItemsSource = new[] {"Remove border", "Remove period(s)", "Custom"};
            ComboBoxRemoveStrategy.SelectedIndex = initialSettings.RepetitionRemovingStrategyIndex;
            if (ComboBoxRemoveStrategy.SelectedIndex != 1)
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

        private void ComboBoxRemoveStrategy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxRemoveStrategy.SelectedIndex != 1)
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
            var folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                TextBoxOutputPath.Text = folderBrowserDialog.SelectedPath;
        }

        private void TextBoxNumerator_OnLoaded(object sender, RoutedEventArgs e)
        {
            TextBoxNumerator.Text = initialSettings.Numerator.ToString(CultureInfo.InvariantCulture);
        }

        private void TextBoxDenominator_OnLoaded(object sender, RoutedEventArgs e)
        {
            TextBoxDenominator.Text = initialSettings.Denominator.ToString(CultureInfo.InvariantCulture);
        }

        private void CheckBoxDetectEqual_OnLoaded(object sender, RoutedEventArgs e)
        {
            CheckBoxDetectEqual.IsChecked = initialSettings.DetectEqualToExponent;
        }

        private void TextBoxAlphabet_OnLoaded(object sender, RoutedEventArgs e)
        {
            TextBoxAlphabet.Text = initialSettings.AlphabetSize.ToString(CultureInfo.InvariantCulture);
        }

        private void TextBoxLength_OnLoaded(object sender, RoutedEventArgs e)
        {
            TextBoxLength.Text = initialSettings.GeneratedStringLength.ToString(CultureInfo.InvariantCulture);
        }

        private void TextBoxRunsCount_OnLoaded(object sender, RoutedEventArgs e)
        {
            TextBoxRunsCount.Text = initialSettings.RunsCount.ToString(CultureInfo.InvariantCulture);
        }

        private void TextBoxOutputPath_OnLoaded(object sender, RoutedEventArgs e)
        {
            TextBoxOutputPath.Text = initialSettings.SaveData.SavePath;
        }

        private void CheckBoxFullLog_OnLoaded(object sender, RoutedEventArgs e)
        {
            CheckBoxFullLog.IsChecked = initialSettings.SaveData.SaveFullLog;
        }

        private void CheckBoxStatistics_OnLoaded(object sender, RoutedEventArgs e)
        {
            CheckBoxStatistics.IsChecked = initialSettings.SaveData.SaveStats;
        }

        private void TextBoxPeriodsCount_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (initialSettings.RemoveStrategy is RemovePeriodsStrategy)
                TextBoxPeriodsCount.Text =
                    ((RemovePeriodsStrategy) initialSettings.RemoveStrategy).PeriodsCount.ToString(CultureInfo
                        .InvariantCulture);
            else
                TextBoxPeriodsCount.Text = "1";
        }

        private void CheckBoxRemovingUpperBound_OnClick(object sender, RoutedEventArgs e)
        {
            TextBoxRemovingUpperBound.Visibility = TextBoxRemovingUpperBound.Visibility == Visibility.Visible
                ? Visibility.Hidden
                : Visibility.Visible;
        }

        private void TextBoxRemovingUpperBound_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (initialSettings.RemoveStrategy is NoMoreThanStrategy)
            {
                var strategy = (NoMoreThanStrategy) initialSettings.RemoveStrategy;
                CheckBoxRemovingUpperBound.IsChecked = true;
                TextBoxRemovingUpperBound.Visibility = Visibility.Visible;
                TextBoxRemovingUpperBound.Text = strategy.MaxCharactersToRemove.ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                CheckBoxRemovingUpperBound.IsChecked = false;
                TextBoxRemovingUpperBound.Visibility = Visibility.Hidden;
            }
        }

        private static string SettingsPath => Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),
            InititalSettingsFileName);

        private InitialSettings initialSettings;

        private const string InititalSettingsFileName = "settings.ini";
    }
}