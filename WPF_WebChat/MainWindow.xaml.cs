using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_WebChat
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();

            wbWebChat.Navigate(new Uri(@"file:///C:/researchS/15MicrosoftBotFramework/BotFramework-WebChat/samples/fullwindow/index.html?domain=http://localhost:3000/directline&webSocket=false&username=WPF-User"));
            // レスポンスを早くするには index.html で pollingInterval をpiで指定できるようにしたうえで，Navigate内で &pi=100 を追加する

        }

    }
}
