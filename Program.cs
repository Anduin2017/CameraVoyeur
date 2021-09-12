using System;
using System.Windows;

namespace Trick
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args) => new App().Run();
    }

    internal class App : Application
    {
        protected override void OnStartup(StartupEventArgs e) => new MainWindow().Show();
    }
}
