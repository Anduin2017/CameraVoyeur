using System;
using System.Windows;

namespace temp
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            new App().Run(); ;
        }
    }

    internal class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var window = new MainWindow();
            window.Show();
        }
    }
}
