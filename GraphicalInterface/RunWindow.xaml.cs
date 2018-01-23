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
            TextBlockExponent.Text = $"Exponent: ({detector.E}){(detector.DetectEqual ? string.Empty : "+")}";
            TextBlockCharGenerator.Text = $"Char generator: {charGenerator}";
            TextBlockRemoveStrategy.Text = $"Repetition removing strategy: {removeStrategy}";
            TextBlockLength.Text = $"Length: {length}";
            TextBlockRunsCount.Text = $"Runs count: {runsCount}";
            TextBlockAlphabetSize.Text = $"Alphabet size: {charGenerator.AlphabetSize}";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            tokenSource.Cancel();
        }

        private void RunWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            StreamWriter statsOutput = null, fullLogOutput = null;
            if (saveData.SaveStats)
            {
                if (!Directory.Exists(saveData.SavePath))
                    Directory.CreateDirectory(saveData.SavePath);
                var path = Path.Combine(saveData.SavePath,
                    "stats_" + DateTime.Now.ToString("yy-MM-dd_HH-mm-ss") + ".txt");
                statsOutput = new StreamWriter(File.Open(path, FileMode.Create));
            }
            if (saveData.SaveFullLog)
            {
                if (!Directory.Exists(saveData.SavePath))
                    Directory.CreateDirectory(saveData.SavePath);
                var path = Path.Combine(saveData.SavePath,
                    "full_" + DateTime.Now.ToString("yy-MM-dd_HH-mm-ss") + ".txt");
                fullLogOutput = new StreamWriter(File.Open(path, FileMode.Create));
            }
            logger = new OutputLogger(fullLogOutput);
            tokenSource = new CancellationTokenSource();
            token = tokenSource.Token;
            statsOutput?.WriteLine(string.Join("\n",
                $"Exponent: {detector.E}",
                $"Detect equal to exponent: {detector.DetectEqual}",
                $"Alphabet size: {charGenerator.AlphabetSize}",
                $"Length: {length}",
                $"Runs count: {runsCount}",
                $"Char generator: {charGenerator.GetType().Name}",
                $"Removing strategy: {removeStrategy}"));
            Task.Factory.StartNew(() =>
            {
                totalCharsGenerated = 0;
                ms = 0;
                runsPerformed = 0;
                while (runsPerformed < runsCount && !token.IsCancellationRequested)
                {
                    detector.Reset();
                    statsOutput?.WriteLine("Run #{0}:", runsPerformed + 1);

                    var text = RandomWordGenerator.Generate(detector, length, removeStrategy, charGenerator, logger,
                        token);
                    if (!token.IsCancellationRequested)
                    {
                        runsPerformed++;
                        totalCharsGenerated += RandomWordGenerator.Statistics.CharsGenerated;
                        ms += RandomWordGenerator.Statistics.Milliseconds;

                        statsOutput?.Flush();
                        fullLogOutput?.WriteLine($"Result: {text}\n");
                        if (statsOutput != null)
                        {
                            statsOutput.WriteLine("Coef: {0:0.000000}, Time: {1:0.000} ms",
                                RandomWordGenerator.Statistics.CharsGenerated * 1.0 / length,
                                RandomWordGenerator.Statistics.Milliseconds);
                            statsOutput.WriteLine("Repetition periods:\n{0}",
                                string.Join("\n", RandomWordGenerator.Statistics.CountOfPeriods
                                    .OrderBy(p => p.Key)
                                    .Select(p => $"{p.Key}: {p.Value}")));
                            statsOutput.WriteLine("-----");
                        }
                    }
                }
                if (statsOutput != null)
                {
                    statsOutput.WriteLine("-----");
                    statsOutput.WriteLine("Runs performed: {0}\nAverage coef: {1:0.000000}\nAverage time: {2:0.000} ms",
                        runsPerformed,
                        totalCharsGenerated * 1.0 / length / runsPerformed,
                        ms * 1.0 / runsPerformed);
                }
                Thread.Sleep(500);
                tokenSource.Cancel();
                statsOutput?.Close();
                fullLogOutput?.Close();
            }, token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            UpdateStatus += () => Dispatcher.Invoke(() =>
            {
                TextBoxCurrentLength.Text = logger.TextLength.ToString();
                TextBoxRunsCompleted.Text = runsPerformed.ToString();
                if (runsPerformed > 0)
                {
                    TextBoxAverageCoef.Text = $"{totalCharsGenerated * 1.0 / length / runsPerformed:0.000000}";
                    TextBoxAverageTime.Text = $"{ms * 1.0 / runsPerformed:0.000}";
                }
            });
            Task.Factory.StartNew(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    // ReSharper disable once PossibleNullReferenceException
                    UpdateStatus();
                    Thread.Sleep(100);
                }
                // ReSharper disable once PossibleNullReferenceException
                UpdateStatus();
            }, token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        private event UpdateStatusEvent UpdateStatus;

        private void RunWindow_OnClosed(object sender, EventArgs e)
        {
            tokenSource.Cancel();
        }

        private readonly ICharGenerator charGenerator;
        private readonly Detector detector;
        private readonly int length;
        private readonly IRemoveStrategy removeStrategy;
        private readonly int runsCount;
        private readonly SaveData saveData;
        private OutputLogger logger;
        private long ms;
        private volatile int runsPerformed;
        private CancellationToken token;
        private CancellationTokenSource tokenSource;
        private volatile int totalCharsGenerated;

        private delegate void UpdateStatusEvent();
    }
}