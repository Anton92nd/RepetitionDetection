using System.Globalization;
using System.Threading;

namespace RepetitionDetection
{
    public static class Program
    {
        public static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            var mainForm = new MainForm();
            mainForm.ShowDialog();
        }
    }
}
