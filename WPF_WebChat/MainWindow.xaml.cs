using System;
using System.Windows;

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
			//var uri = @"file:///C:/researchS/15MicrosoftBotFramework/BotFramework-WebChat/samples/fullwindow/index.html?domain=http://localhost:3000/directline&webSocket=false&username=WPF-User";
			var uri = @"file:///C:/Users/sawa/Source/Repos/BotFramework-WebChat-Test/samples/backchannel/index.html?domain=http://localhost:3000/directline&webSocket=false&pi=100";

            wbWebChat.Navigate(new Uri(uri));
            // レスポンスを早くするには index.html で pollingInterval をpiで指定できるようにしたうえで，Navigate内で &pi=100 を追加する

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            wbWebChat.InvokeScript("changeBackgroundColor", textBox.Text);
        }
    }
}
