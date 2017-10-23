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
        private CancellationToken token;
        private OutputLogger logger;
        private long ms;
        private volatile int runsPerformed;
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
            StreamWriter statsOutput = null, fullLogOutput = null;
            if (saveData.SaveStats)
            {
                if (!Directory.Exists(saveData.SavePath))
                    Directory.CreateDirectory(saveData.SavePath);
                var path = Path.Combine(saveData.SavePath, "stats_" + DateTime.Now.ToString("yy-MM-dd_HH-mm-ss") + ".txt");
                statsOutput = new StreamWriter(File.Open(path, FileMode.Create));
            }
            if (saveData.SaveFullLog)
            {
                if (!Directory.Exists(saveData.SavePath))
                    Directory.CreateDirectory(saveData.SavePath);
                var path = Path.Combine(saveData.SavePath, "full_" + DateTime.Now.ToString("yy-MM-dd_HH-mm-ss") + ".txt");
                fullLogOutput = new StreamWriter(File.Open(path, FileMode.Create));
            }
            logger = new OutputLogger(fullLogOutput);
            tokenSource = new CancellationTokenSource();
            token = tokenSource.Token;
            if (statsOutput != null)
                statsOutput.WriteLine(
                    "Exponent: {0}\nDetect equal to exponent: {1}\nAlphabet size: {2}\nLength: {3}\nRuns count: {4}\nChar generator: {5}\nRemoving strategy: {6}",
                    detector.E, detector.DetectEqual, charGenerator.AlphabetSize, length, runsCount,
                    charGenerator.GetType().Name, removeStrategy);
            Task.Run(() =>
            {
                totalCharsGenerated = 0;
                ms = 0;
                runsPerformed = 0;
                while (runsPerformed < runsCount && !token.IsCancellationRequested)
                {
                    detector.Reset();
                    if (statsOutput != null)
                        statsOutput.WriteLine("Run #{0}:", runsPerformed + 1);

                    var text = RandomWordGenerator.Generate(detector, length, removeStrategy, charGenerator, logger, token);
                    if (!token.IsCancellationRequested)
                    {
                        runsPerformed++;
                        totalCharsGenerated += RandomWordGenerator.Statistics.CharsGenerated;
                        ms += RandomWordGenerator.Statistics.Milliseconds;

                        if (statsOutput != null)
                            statsOutput.Flush();
                        if (fullLogOutput != null)
                            fullLogOutput.WriteLine("Result: {0}\n", text);
                        if (statsOutput != null)
                        {
                            statsOutput.WriteLine("Coef: {0:0.000000}, Time: {1:0.000} ms",
                                RandomWordGenerator.Statistics.CharsGenerated * 1.0 / length,
                                RandomWordGenerator.Statistics.Milliseconds);
                            statsOutput.WriteLine("Repetition periods:\n{0}",
                                string.Join("\n", RandomWordGenerator.Statistics.CountOfPeriods
                                    .OrderBy(p => p.Key)
                                    .Select(p => string.Format("{0}: {1}", p.Key, p.Value))));
                            statsOutput.WriteLine("-----");
                        }
                    }
                }
                if (statsOutput != null)
                {
                    statsOutput.WriteLine("-----");
                    statsOutput.WriteLine("Runs performed: {0}\nAverage coef: {1:0.000000}\nAverage time: {2:0.000} ms",
                        runsPerformed,
                        totalCharsGenerated*1.0/length/runsPerformed,
                        ms*1.0/runsPerformed);
                }
                Thread.Sleep(500);
                tokenSource.Cancel();
                if (statsOutput != null)
                    statsOutput.Close();
                if (fullLogOutput != null)
                    fullLogOutput.Close();
            }, token);
            UpdateStatus += () => Dispatcher.Invoke(() =>
            {
                TextBoxCurrentLength.Text = logger.TextLength.ToString();
                TextBoxRunsCompleted.Text = runsPerformed.ToString();
                if (runsPerformed > 0)
                {
                    TextBoxAverageCoef.Text = string.Format("{0:0.000000}", totalCharsGenerated*1.0/length/runsPerformed);
                    TextBoxAverageTime.Text = string.Format("{0:0.000}", ms*1.0/runsPerformed);
                }
            });
            Task.Run(() =>
            {
                while (!token.IsCancellationRequested)
                {
                    // ReSharper disable once PossibleNullReferenceException
                    UpdateStatus();
                    Thread.Sleep(100);
                }
                // ReSharper disable once PossibleNullReferenceException
                UpdateStatus();
            }, token);
        }

        private event UpdateStatusEvent UpdateStatus;

        private void RunWindow_OnClosed(object sender, EventArgs e)
        {
            tokenSource.Cancel();
        }

        private delegate void UpdateStatusEvent();
    }
}