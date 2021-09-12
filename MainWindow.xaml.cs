using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Trick
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
            if (this.Progress.Value > 60 && this.Progress.Value < 100)
            {
                MessageBox.Show("网络连接已建立，正在运行黑入计划……暂时无法退出。请稍后重试。", "摄像头黑入器", MessageBoxButton.OK, MessageBoxImage.Error);
                e.Cancel = true;
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!this.ValidateIPv4(IP.Text))
            {
                MessageBox.Show("IP地址并不是一个正确的远程IP！", "摄像头黑入器", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var continueHack = MessageBox.Show($"即将开始黑入远程主机{IP.Text}！\r\n\r\n警告：利用系统漏洞黑入对方摄像头是违法行为，可能会被追踪法律责任！\r\n由此产生的一切责任与开发者无关！", "摄像头黑入器", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (continueHack == MessageBoxResult.Cancel)
            {
                return;
            }

            IP.IsEnabled = false;
            Status.IsEnabled = false;
            Status.Content = "正在黑入...";
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var ran = new Random();
            while (true)
            {
                await Task.Delay(ran.Next(0, 3000));
                var x = stopWatch.Elapsed.TotalSeconds;
                var k = 5;
                var y = 100 * (x - k * Math.Sqrt(x)) / (x - k * k);
                if (y >= 100)
                {
                    y = 100;
                }

                this.Progress.Value = y;
                Text.Content = $"{y:N2}%";
                this.Status.Content = $"{GetDetails(y)}...\r\n请不要退出。";

                if (y == 100)
                {
                    this.Progress.Value = 100;
                    this.Status.Content = $"黑入成功";

                    new CameraWindow().Show();
                    return;
                }
            }
        }

        private string GetDetails(double progress)
        {
            // Hi。当你看到这儿，我估计你已经发现：自己被骗啦~
            // 只要你真的下载了……你就输了……
            
            // 是的，这是一个钓鱼贴，它有两个目的：
            
            // * 挖掘一下有多少"坏"人会试图解锁别人的摄像头
            // * 挖掘一下骗别人运行一个exe有多容易
            // * 挖掘一下有多少人真的会相信黑入别人的摄像头会如此容易
                        
            // 在Readme中已经给出了大量线索，例如 "Rick Astley于27 July 1987发表的论文" 指的是 "never gonna give you up" ， 甚至还附带了一大段歌词 :D 这个Repo的Tags里也注明了它是恶作剧。
            // 项目的二进制叫 Trick.exe ，意思是：逗你玩.exe
            // 当然这个项目，也稍微使用了一些小技巧。例如：进度条会永远往前走，而永远不会走到头。
            
            // 祝你玩得愉快！可以把它编译好的结果分享给基友看他会干什么噢~ 有反馈记得来issue里聊
            
            // （另外，运行它确实是无害的。那份二进制也确实是这份代码编译出来的，它也确实代码只有150行……）
            
            if (progress >= 100)
                return "黑入成功";
            else if (progress >= 99.101)
                return "正在加载视频预览";
            else if (progress >= 98.012)
                return "正在开启对方的摄像头";
            else if (progress >= 97.321)
                return "正在开启远程主机的权限";
            else if (progress >= 91.987)
                return "正在尝试登入系统并安装远程操控驱动";
            else if (progress >= 75.129)
                return "正在暴力破解对方的密码，这可能会花费一段时间";
            else if (progress >= 70.583)
                return "正在搜索合适的密码破解算法";
            else if (progress >= 64.257)
                return "正在扫描对方的漏洞";
            else if (progress >= 61.234)
                return "正在验证系统版本";
            else if (progress >= 55.123)
                return "正在建立网络连接";
            else
                return "正在初始化";
        }

        public bool ValidateIPv4(string ipString)
        {
            if (String.IsNullOrWhiteSpace(ipString))
            {
                return false;
            }

            string[] splitValues = ipString.Split('.');
            if (splitValues.Length != 4)
            {
                return false;
            }

            if (splitValues[0].Trim() == "127" || splitValues[0].Trim() == "0")
            {
                return false;
            }

            byte tempForParsing;

            return splitValues.All(r => byte.TryParse(r, out tempForParsing));
        }
    }
}

// 额外

// 如果你已经读到这儿了，我还是建议你跑跑它试试。

// 进度条经过调教，其非常利用人类的心理。
// 因为它看起来真的好像马上就要完成了。
// 而且剩余的进度，前一半用户会永远觉得：明明刚才挺快的
// 用户真的已经花了很久付出了前一半，而进度条确实也在走
// 他会真的觉得马上就要成功了

// 所以，不知情的人第一次运行，一定会被进度条欺骗，而非常激动。

// 同样，这个项目也有它的价值：
// 你可以将它的文字修改修改，例如：“我的机密日记查看器”，按钮改成“开始解密日记”，从而让妈妈永远都看不到你的日记。。。
// 或是改成 “婚礼倒计时”，然后告诉女盆友，这个进度条到头了，就可以结婚了，从而……
