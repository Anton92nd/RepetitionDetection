using System;
using System.Collections.Generic;
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
            logger = GetLogger();
            logger.Log(LogLevel.Stats,
                string.Join("\n",
                    $"Exponent: {detector.E}",
                    $"Detect equal to exponent: {detector.DetectEqual}",
                    $"Alphabet size: {charGenerator.AlphabetSize}",
                    $"Length: {length}",
                    $"Runs count: {runsCount}",
                    $"Char generator: {charGenerator.GetType().Name}",
                    $"Removing strategy: {removeStrategy}"));

            tokenSource = new CancellationTokenSource();
            token = tokenSource.Token;
            Task.Factory.StartNew(() =>
            {
                totalCharsGenerated = 0;
                ms = 0;
                runsPerformed = 0;
                while (runsPerformed < runsCount && !token.IsCancellationRequested)
                {
                    detector.Reset();
                    logger.Log(LogLevel.Stats, $"Run #{runsPerformed + 1}:");
                    logger.Log(LogLevel.Full, $"Run #{runsPerformed + 1}:");

                    var text = RandomWordGenerator.Generate(detector, length, removeStrategy, charGenerator, logger,
                        token);
                    if (!token.IsCancellationRequested)
                    {
                        runsPerformed++;
                        totalCharsGenerated += RandomWordGenerator.Statistics.CharsGenerated;
                        ms += RandomWordGenerator.Statistics.Milliseconds;

                        logger.Log(LogLevel.Full, $"Result #{runsPerformed}: {text}\n");
                        logger.Flush(LogLevel.Full);

                        logger.Log(LogLevel.Stats, string.Format("Coef: {0:0.000000}, Time: {1:0.000} ms",
                            RandomWordGenerator.Statistics.CharsGenerated * 1.0 / length,
                            RandomWordGenerator.Statistics.Milliseconds));
                        logger.Log(LogLevel.Stats, string.Format("Repetitions (period, border):\n{0}",
                            string.Join("\n", RandomWordGenerator.Statistics.CountOfRuns
                                .OrderBy(p => p.Key)
                                .Select(p => $"{p.Key}: {p.Value}"))));
                        logger.Log(LogLevel.Stats, "-----");
                        logger.Flush(LogLevel.Stats);
                    }
                    else
                    {
                        logger.Log(LogLevel.Stats, "[Canceled]");
                        logger.Log(LogLevel.Stats, $"Time: {RandomWordGenerator.Statistics.Milliseconds:0.000} ms");
                        logger.Log(LogLevel.Stats, string.Format("Repetitions (period, border):\n{0}",
                            string.Join("\n", RandomWordGenerator.Statistics.CountOfRuns
                                .OrderBy(p => p.Key)
                                .Select(p => $"{p.Key}: {p.Value}"))));
                        logger.Log(LogLevel.Stats, "-----");
                        logger.Flush(LogLevel.Stats);
                    }
                }

                logger.Log(LogLevel.Stats, "-----");
                logger.Log(LogLevel.Stats, string.Format("Runs performed: {0}\nAverage coef: {1:0.000000}\nAverage time: {2:0.000} ms",
                    runsPerformed,
                    totalCharsGenerated * 1.0 / length / runsPerformed,
                    ms * 1.0 / runsPerformed));
                
                Thread.Sleep(500);
                tokenSource.Cancel();
                logger.Dispose();
            }, token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            UpdateStatus += () => Dispatcher.Invoke(() =>
            {
                TextBoxCurrentLength.Text = RandomWordGenerator.Statistics.TextLength.ToString();
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

        private GenerationLogger GetLogger()
        {
            var loggers = new List<(LogLevel LogLevel, StreamWriter Writer)>();
            if (saveData.SaveStats)
            {
                if (!Directory.Exists(saveData.SavePath))
                    Directory.CreateDirectory(saveData.SavePath);
                var path = Path.Combine(saveData.SavePath,
                    "stats_" + DateTime.Now.ToString("yy-MM-dd_HH-mm-ss") + ".txt");
                loggers.Add((LogLevel.Stats, new StreamWriter(File.Open(path, FileMode.Create))));
            }
            if (saveData.SaveFullLog)
            {
                if (!Directory.Exists(saveData.SavePath))
                    Directory.CreateDirectory(saveData.SavePath);
                var path = Path.Combine(saveData.SavePath,
                    "full_" + DateTime.Now.ToString("yy-MM-dd_HH-mm-ss") + ".txt");
                loggers.Add((LogLevel.Full, new StreamWriter(File.Open(path, FileMode.Create))));
            }
            return new GenerationLogger(loggers);
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
        private GenerationLogger logger;
        private long ms;
        private volatile int runsPerformed;
        private CancellationToken token;
        private CancellationTokenSource tokenSource;
        private volatile int totalCharsGenerated;

        private delegate void UpdateStatusEvent();
    }
}