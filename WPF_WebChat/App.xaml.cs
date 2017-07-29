using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_WebChat
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private const string FEATURE_BROWSER_EMULATION = @"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION";
        private String exeName = null;
        App()
        {
            /// このPCにインストールされている最新バージョンのIEを使うよう Registryへ登録
            Microsoft.Win32.RegistryKey regkey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(FEATURE_BROWSER_EMULATION);
            exeName = System.IO.Path.GetFileName(Assembly.GetExecutingAssembly().Location);

            int IeVer = this.IEVersion;

            regkey.SetValue(exeName, IeVer * 1000, Microsoft.Win32.RegistryValueKind.DWord);
            regkey.Close();
            Console.WriteLine("App: Constructor called. Register key of WebBrowser Control for {0}: IEver= {1}", exeName, IeVer);
        }
        ~App()
        {
            Microsoft.Win32.RegistryKey regkey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(FEATURE_BROWSER_EMULATION);

            try { 
                regkey.DeleteValue(exeName, true);
                regkey.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
            }
            Console.WriteLine("App: Destructor called. Unregister key of WebBrowser Control.");
        }
        private int IEVersion
        {
            get
            {
                var r = new Regex(@"(\d{1,2})\.(\d{1,2})\.[\d]+.[\d]+");
                var m = r.Match(Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Internet Explorer").GetValue("Version").ToString());

                var ver1 = Convert.ToInt32(m.Groups[1].Value);
                var ver2 = Convert.ToInt32(m.Groups[2].Value);

                if (ver1 == 9 && ver2 > 9)
                {
                    return ver2;
                }

                return ver1;
            }
        }
    }
}
