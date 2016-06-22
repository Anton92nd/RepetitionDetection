using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using RepetitionDetection.CharGenerators;
using RepetitionDetection.Detection;
using RepetitionDetection.Logging;
using RepetitionDetection.TextGeneration;
using RepetitionDetection.TextGeneration.RemoveStrategies;

namespace RepetitionDetection
{
    public partial class RunForm : Form
    {
        private readonly Detector detector;
        private readonly ICharGenerator charGenerator;
        private readonly IRemoveStrategy removeStrategy;
        private Task generateTask, updateTask;
        private volatile int run;
        private CancellationTokenSource tokenSource;

        public delegate void UpdateEvent();

        public event UpdateEvent OnUpdate;

        public RunForm(Detector detector, ICharGenerator charGenerator, IRemoveStrategy removeStrategy, int length, int runsCount)
        {
            this.detector = detector;
            this.charGenerator = charGenerator;
            this.removeStrategy = removeStrategy;
            InitializeComponent();
            labelExponent.Text = string.Format("Exponent: ({0}){1}", detector.E, detector.DetectEqual ? "" : "+");
            labelLength.Text = string.Format("Length: {0}", length);
            labelAlphabet.Text = string.Format("Alphabet size: {0}", charGenerator.AlphabetSize);
            labelCharGenerator.Text = string.Format("Char generator: {0}", charGenerator);
            labelRunsCount.Text = string.Format("Runs count: {0}", runsCount);
            labelStrategy.Text = string.Format("Remove strategy: {0}", removeStrategy);
        }
    }
}
