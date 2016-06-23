using System;
using System.Globalization;
using System.Threading;

namespace RepetitionDetection
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        }
    }
}
