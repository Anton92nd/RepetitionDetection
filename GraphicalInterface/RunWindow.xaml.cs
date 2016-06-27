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
    /// Interaction logic for RunWindow.xaml
    /// </summary>
    public partial class RunWindow : Window
    {
        private readonly Detector detector;
        private readonly IRemoveStrategy removeStrategy;
        private readonly ICharGenerator charGenerator;
        private readonly SaveData saveData;
        private readonly int length;
        private readonly int runsCount;
        private Task generateTask, monitoringTask;
        private CancellationToken cancellationToken;
        private OutputLogger logger;
        private volatile int run;
        private CancellationTokenSource tokenSource;

        public RunWindow(Detector detector, IRemoveStrategy removeStrategy, ICharGenerator charGenerator, SaveData saveData, int length, int runsCount)
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
            TextBlockExponent.Text = string.Format("Exponent: ({0}){1}", detector.E, detector.DetectEqual ? string.Empty : "+");
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
            logger = new OutputLogger(null);
            tokenSource = new CancellationTokenSource();
            cancellationToken = tokenSource.Token;
            generateTask = Task.Run(() =>
            {
                for (run = 1; run <= runsCount; ++run)
                {
                    detector.Reset();
                    RandomWordGenerator.Generate(detector, length, removeStrategy, charGenerator, logger, cancellationToken);
                }
            }, cancellationToken);
            UpdateStatus += () => Dispatcher.Invoke(() =>
            {
                TextBoxCurrentLength.Text = logger.TextLength.ToString();
                TextBoxRunNumber.Text = run.ToString();
            });
            monitoringTask = Task.Run(() =>
            {
                while (true)
                {
                    if (cancellationToken.IsCancellationRequested)
                        return;
                    if (UpdateStatus != null)
                        UpdateStatus();
                    Thread.Sleep(50);
                }
            }, cancellationToken);
        }

        private delegate void UpdateStatusEvent();

        private event UpdateStatusEvent UpdateStatus;
    }
}
