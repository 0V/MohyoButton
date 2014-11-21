using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreTweet;
using System.Xml;
namespace MohyoButton.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private RelayCommand _MohyoCommand;
        public RelayCommand MohyoCommand
        {
            get { return _MohyoCommand ?? (_MohyoCommand = new RelayCommand(TweetMohyo)); }
            set { _MohyoCommand = value; }
        }

        private void TweetMohyo(object obj)
        {
            if (App.Token == null)
            {
                try
                {
                    var keyInfo = Models.KeyParser.ReadKey("keyinfo.xml");
                    App.Token = Tokens.Create(keyInfo.ConsumerKey, keyInfo.ConsumerSecret, keyInfo.AccessToken, keyInfo.AccessTokenSecret);
                }
                catch (Exception ex)
                {


                    if (!App.LoginWindowInstance.IsLoaded)
                    {
                        new WpfMessageBox("ファイルからKey情報を正しく読み取れませんでした。Twitter の認証画面を開きます。").ShowDialog();
                        App.LoginWindowInstance = new LoginWindow();
                        App.Session = OAuth.Authorize(App.ConsumerKey, App.ConsumerSecret);
                        var uri = App.Session.AuthorizeUri;
                        System.Diagnostics.Process.Start(uri.AbsoluteUri);
                        App.LoginWindowInstance.Show();
                    }
                    else
                    {
                        new WpfMessageBox("Twitter の認証をしてください。").ShowDialog();
                    }
                }
            }
            else
            {
                var res = Models.MohyoTweet.Post(App.Token);
                res.ContinueWith((r) => {
                    try {
                        if (r.Result == null)
                        {
                            App.Current.Dispatcher.InvokeAsync(() => new WpfMessageBox("もひょれませんでした").Show());
                        }
                        else
                        {
                            App.Current.Dispatcher.InvokeAsync(() => new WpfMessageBox("もひょりました").Show());
                        }
                    }
                    catch {
                        App.Current.Dispatcher.InvokeAsync(() => new WpfMessageBox("もひょれませんでした").Show());
                    }
                });
            }
        }
    }
}
