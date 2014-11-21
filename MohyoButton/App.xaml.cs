using CoreTweet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MohyoButton
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {

        private static LoginWindow _LoginWindow = new LoginWindow();
        public static LoginWindow LoginWindowInstance { get { return _LoginWindow; } set { _LoginWindow = value; } }

        public const string ConsumerKey = "yQLCCq8XjKUes6kzptst06LYD";
        public const string ConsumerSecret = "l2OEujoHJMgdME7uwLSbNzhQ73ImKKVjpSI7uDSr7tZPXaEJSp";

        public static Tokens Token { get; set; }
        public static OAuth.OAuthSession Session { get; set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += ((_sender, _e) =>
                ShowErrorMessage((Exception)_e.ExceptionObject));

        }

        /// <summary>
        /// †ガバガバ例外処理†
        /// </summary>
        /// <param name="ex"></param>
        public static void ShowErrorMessage(Exception ex)
        {
            var twitterException = ex as TwitterException;
            if (twitterException != null)
            {
                if (twitterException.Message != null)
                    MessageBox.Show("Twitter のエラーが発生しました。プログラムを終了します。\n"+ twitterException.Message, "†ガバガバ例外処理†", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                    MessageBox.Show("Twitter のエラーが発生しました。プログラムを終了します。", "†ガバガバ例外処理†", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("エラーが発生しました。プログラムを終了します。\n" + ex.Message, "†ガバガバ例外処理†", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            App.Current.Shutdown(-1);
            Environment.Exit(-1);
        }
    }
}
