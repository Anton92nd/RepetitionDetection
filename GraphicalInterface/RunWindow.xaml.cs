using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
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
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
            this.detector = detector;
            this.removeStrategy = removeStrategy;
            this.charGenerator = charGenerator;
            this.saveData = saveData;
            this.length = length;
            this.runsCount = runsCount;
            statistics = new Statistics();
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
                    $"Removing strategy: {removeStrategy}",
                    "-----------------------"));
            logger.Log(LogLevel.Stats, "Run #    Conversion    Time (ms)");
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
                    logger.Log(LogLevel.Full, $"Run #{runsPerformed + 1}:");

                    var text = RandomWordGenerator.Generate(detector, length, removeStrategy, charGenerator, statistics, logger, token);
                    if (!token.IsCancellationRequested)
                    {
                        runsPerformed++;
                        totalCharsGenerated += statistics.CharsGenerated;
                        ms += statistics.Milliseconds;
                    }

                    LogRun(text, token.IsCancellationRequested);
                }

                LogTotal();

                Thread.Sleep(500);
                tokenSource.Cancel();
                logger.Dispose();
            }, token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            UpdateStatus += () => Dispatcher.Invoke(() =>
            {
                TextBoxCurrentLength.Text = statistics.TextLength.ToString();
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

        private void LogRun(StringBuilder text, bool isCancellationRequested)
        {
            if (isCancellationRequested)
            {
                logger.Log(LogLevel.Stats, $"{runsPerformed + 1,5}    {"????",10}    {statistics.Milliseconds:0.0} [Canceled]");
            }
            else
            {
                logger.Log(LogLevel.Full, $"Result #{runsPerformed}: {text}\n");
                logger.Flush(LogLevel.Full);

                logger.Log(LogLevel.Stats, string.Format("{0,5}    {1,10:0.000}    {2,8:0.0}",
                    runsPerformed,
                    statistics.CharsGenerated * 1.0 / length,
                    statistics.Milliseconds));
            }

            logger.Flush(LogLevel.Stats);
        }

        private void LogTotal()
        {
            CalculateStats();
            logger.Log(LogLevel.Stats, "\n-------[Total]---------");
            logger.Log(LogLevel.Stats, string.Format("{0,5}    {1,10:0.000}    {2,8:0.0}", 
                runsPerformed, 
                totalCharsGenerated * 1.0 / length / runsPerformed,
                ms * 1.0 / runsPerformed));
            logger.Log(LogLevel.Stats, "-------[Total]---------\n");

            var totalRepetitions = statistics.TotalCountOfRuns.Sum(p => p.Value);
            logger.Log(LogLevel.Stats, string.Format("Repetition statistics:\nperiod    border         count    percent\n" +
                                                     string.Join("\n", statistics.TotalCountOfRuns
                                                         .OrderBy(p => p.Key)
                                                         .Select(p => $"{p.Key.Period,6}    {p.Key.Border,6}    {p.Value,10}    {p.Value * 100.0 / totalRepetitions,7:0.000}")))
            );
            logger.Log(LogLevel.Stats, string.Format("Other statistics:\nlength    visit count    advance coef.    movingAverage(advance coef., 100)    advance fraction    advance after advance fraction\n" +
                                                     string.Join("\n", statistics.TotalAdvances
                                                         .Select((x, i) => string.Format("{0,6}    {1,11}    {2,13:0.0000}    {3,33:0.0000}    {4,16:0.0000}    {5:0.0000}",
                                                             i,
                                                             x.VisitCount,
                                                             x.LengthDeltaSum * 1.0 / x.VisitCount,
                                                             x.MovingAverageAdvance,
                                                             x.AdvanceCount * 1.0 / x.VisitCount,
                                                             x.AdvanceAfterAdvanceCount * 1.0 / x.AfterAdvanceCount))))
            );
        }

        private void CalculateStats()
        {
            const int windowSize = 100;
            var array = statistics.TotalAdvances.Select(x => x.LengthDeltaSum * 1.0 / x.VisitCount).ToArray();
            for (var i = 0; i < array.Length; ++i)
            {
                double sum = 0;
                for (var j = Math.Max(0, i - windowSize + 1); j <= i; ++j)
                    sum += array[j];
                statistics.TotalAdvances[i].MovingAverageAdvance = sum / Math.Min(i + 1, windowSize);
            }
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
        private readonly Statistics statistics;

        private delegate void UpdateStatusEvent();
    }
}