using CoreTweet;
using System.Windows;
using System.Windows.Controls;
namespace MohyoButton.ViewModels
{
    public class LoginViewModel
    {
        private RelayCommand _PinRunCommand;
        public RelayCommand PinRunCommand
        {
            get { return _PinRunCommand ?? (_PinRunCommand = new RelayCommand(PinRun)); }
            set { _PinRunCommand = value; }
        }

        private void PinRun(object pinBox)
        {
            var text = (TextBox)pinBox;

            if (string.IsNullOrWhiteSpace(text.Text))
            {
                new WpfMessageBox("PIN を入力してください", MessageBoxButton.OK).ShowDialog();
                return;
            }

            try
            {
                var pin = text.Text;
                var token = OAuth.GetTokensAsync(App.Session, pin);
                App.Token = token.Result;
                new WpfMessageBox("認証に成功しました", MessageBoxButton.OK).ShowDialog();
                Models.KeyParser.WriteKey("keyinfo.xml",App.Token,App.CountMessage,App.PostCountMessage);
                Window.GetWindow(text).Close();
            }
            catch
            {
                new WpfMessageBox("認証に失敗しました\n\n再度認証してください",MessageBoxButton.OK).ShowDialog();
                Window.GetWindow(text).Close();
            }

        }

    }
}