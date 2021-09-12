using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace temp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (this.Progress.Value > 60)
            {
                MessageBox.Show("网络连接已建立，正在运行黑入计划……暂时无法退出。请稍后重试。", "摄像头黑入器", MessageBoxButton.OK, MessageBoxImage.Error);
                e.Cancel = true;
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var continueHack = MessageBox.Show("利用系统漏洞黑入对方摄像头是违法行为！\r\n由此产生的一切责任与开发者无关！", "摄像头黑入器", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (continueHack == MessageBoxResult.Cancel)
            {
                return;
            }

            Status.IsEnabled = false;
            Status.Content = "正在黑入...";
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            while (true)
            {
                await Task.Delay(1);
                var x = stopWatch.Elapsed.TotalSeconds;
                this.Progress.Value = 100 * (x - Math.Sqrt(x)) / (x - 1);
                Text.Content = $"{this.Progress.Value:N2}%";
                this.Status.Content = $"{GetDetails(this.Progress.Value)}...\r\n请不要退出。";
            }
        }

        private string GetDetails(double progress)
        {
            if (progress >= 99.101)
                return "正在加载视频预览";
            else if (progress >= 98.012)
                return "正在开启对方的摄像头";
            else if (progress >= 97.321)
                return "正在登录对方的系统";
            else if (progress >= 91.987)
                return "正在验证得到的密码并安装远程操控驱动";
            else if (progress >= 75.129)
                return "正在暴力破解对方的密码，这可能会花费一段时间";
            else if (progress >= 70.583)
                return "正在搜索合适的密码破解算法";
            else if (progress >= 65.257)
                return "正在验证系统版本";
            else if (progress >= 60.234)
                return "正在扫描对方的漏洞";
            else if (progress >= 55.123)
                return "正在建立网络连接";
            else
                return "正在初始化";
        }
    }
}
