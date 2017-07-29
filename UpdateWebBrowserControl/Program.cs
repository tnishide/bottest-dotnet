using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

// http://qiita.com/hbsnow/items/e732621b223e2a5d5df8 参考に
// 本来は実際に実行ファイルを作成するコードにこれらを埋め込む

namespace UpdateWebBrowserControl
{
    class Program
    {
        static private int IEVersion
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
        static void Main(string[] args)
        {
            const string FEATURE_BROWSER_EMULATION = @"Software\Microsoft\Internet Explorer\Main\FeatureControl\FEATURE_BROWSER_EMULATION";
        
            Microsoft.Win32.RegistryKey regkey = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(FEATURE_BROWSER_EMULATION);
            //String exeName = System.IO.Path.GetFileName(Assembly.GetExecutingAssembly().Location);
            String exeName = "WPF_WebChat.exe";

            int IeVer;
            IeVer = IEVersion;

            regkey.SetValue(exeName, IeVer * 1000, Microsoft.Win32.RegistryValueKind.DWord);
            regkey.Close();
        }
    }
}
