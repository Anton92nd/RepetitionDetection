using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using RepetitionDetection.CharGenerators;
using RepetitionDetection.Detection;
using RepetitionDetection.Logging;
using RepetitionDetection.TextGeneration;
using RepetitionDetection.TextGeneration.RemoveStrategies;

namespace GraphicalInterface
{
    /// <summary>
    ///     Interaction logic for RunWindow.xaml
    /// </summary>
    public partial class RunWindow : Window
    {
        private readonly ICharGenerator charGenerator;
        private readonly Detector detector;
        private readonly int length;
        private readonly IRemoveStrategy removeStrategy;
        private readonly int runsCount;
        private readonly SaveData saveData;
        private CancellationToken cancellationToken;
        private OutputLogger logger;
        private long ms;
        private volatile int run;
        private CancellationTokenSource tokenSource;
        private volatile int totalCharsGenerated;

        public RunWindow(Detector detector, IRemoveStrategy removeStrategy, ICharGenerator charGenerator,
            SaveData saveData, int length, int runsCount)
        {
            this.detector = detector;
            this.removeStrategy = removeStrategy;
            this.charGenerator = charGenerator;
            this.saveData = saveData;
            this.length = length;
            this.runsCount = runsCount;
            InitializeComponent();
        }

        private void GroupBoxParameters_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlockExponent.Text = string.Format("Exponent: ({0}){1}", detector.E,
                detector.DetectEqual ? string.Empty : "+");
            TextBlockCharGenerator.Text = string.Format("Char generator: {0}", charGenerator);
            TextBlockRemoveStrategy.Text = string.Format("Repetition removing strategy: {0}", removeStrategy);
            TextBlockLength.Text = string.Format("Length: {0}", length);
            TextBlockRunsCount.Text = string.Format("Runs count: {0}", runsCount);
            TextBlockAlphabetSize.Text = string.Format("Alphabet size: {0}", charGenerator.AlphabetSize);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tokenSource.Cancel();
        }

        private void RunWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            var statsOutput = saveData.SaveStats
                ? new StreamWriter(
                    File.Open(
                        Path.Combine(saveData.SavePath, "stats_" + DateTime.Now.ToString("yy-MM-dd_HH-mm") + ".txt"),
                        FileMode.Create))
                : null;
            var fullLogOutput = saveData.SaveFullLog
                ? new StreamWriter(
                    File.Open(
                        Path.Combine(saveData.SavePath, "full_" + DateTime.Now.ToString("yy-MM-dd_HH-mm") + ".txt"),
                        FileMode.Create))
                : null;
            logger = new OutputLogger(fullLogOutput);
            tokenSource = new CancellationTokenSource();
            cancellationToken = tokenSource.Token;
            if (statsOutput != null)
                statsOutput.WriteLine(
                    "Exponent: {0}\nDetect equal to exponent: {1}\nAlphabet size: {2}\nLength: {3}\nRuns count: {4}\nChar generator: {5}\nRemoving strategy: {6}",
                    detector.E, detector.DetectEqual, charGenerator.AlphabetSize, length, runsCount,
                    charGenerator.GetType().Name, removeStrategy.GetType().Name);
            var generateTask = Task.Run(() =>
            {
                totalCharsGenerated = 0;
                ms = 0;
                for (run = 0; run < runsCount; ++run)
                {
                    if (cancellationToken.IsCancellationRequested)
                        break;
                    detector.Reset();
                    if (statsOutput != null)
                        statsOutput.WriteLine("Run #{0}:", run + 1);
                    RandomWordGenerator.Generate(detector, length, removeStrategy, charGenerator, logger,
                        cancellationToken);
                    if (statsOutput != null)
                    {
                        statsOutput.WriteLine("Coef: {0:0.000000}, Time: {1:0.000}",
                            RandomWordGenerator.Statistics.CharsGenerated*1.0/length,
                            RandomWordGenerator.Statistics.Milliseconds / 1000.0);
                        statsOutput.WriteLine("Repetition periods:\n{0}",
                            string.Join("\n", RandomWordGenerator.Statistics.CountOfPeriods
                                .OrderBy(p => p.Key)
                                .Select(p => string.Format("{0}: {1}", p.Key, p.Value))));
                        statsOutput.WriteLine("-----");
                    }
                    totalCharsGenerated += RandomWordGenerator.Statistics.CharsGenerated;
                    ms += RandomWordGenerator.Statistics.Milliseconds;
                    if (statsOutput != null)
                        statsOutput.Flush();
                }
                Thread.Sleep(100);
                tokenSource.Cancel();
                if (statsOutput != null)
                    statsOutput.Close();
            }, cancellationToken);
            UpdateStatus += ended => Dispatcher.Invoke(() =>
            {
                TextBoxCurrentLength.Text = logger.TextLength.ToString();
                TextBoxRunNumber.Text = (run + 1 - (ended ? 1 : 0)).ToString();
                if (run > 0)
                {
                    TextBoxAverageCoef.Text = string.Format("{0:0.000000}", totalCharsGenerated*1.0/length/run);
                    TextBoxAverageTime.Text = string.Format("{0:0.000}", ms*1.0/run);
                }
            });
            var monitoringTask = Task.Run(() =>
            {
                while (true)
                {
                    if (UpdateStatus != null)
                        UpdateStatus(cancellationToken.IsCancellationRequested);
                    Thread.Sleep(50);
                }
            }, cancellationToken);
        }

        private event UpdateStatusEvent UpdateStatus;

        private void RunWindow_OnClosed(object sender, EventArgs e)
        {
            tokenSource.Cancel();
        }

        private delegate void UpdateStatusEvent(bool ended);
    }
}