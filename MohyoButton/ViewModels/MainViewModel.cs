using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreTweet;
using System.Xml;
using System.IO;

namespace MohyoButton.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private Models.MohyoTweet mohyoTweet;

        public MainViewModel()
        {
            LoadToken();

            string wordsFileName = App.UserWordListName;

            try
            {
                var list = Models.WordFileLoad.Load(wordsFileName);
                if (list.Any(x => !string.IsNullOrWhiteSpace(x)))
                {
                    new WpfMessageBox(wordsFileName + " から単語を読み込みました。").Show();
                    mohyoTweet = new Models.MohyoTweet(list);
                }
                else
                {
                    mohyoTweet = new Models.MohyoTweet();
                }
            }
            catch (FileLoadException fe)
            {
                mohyoTweet = new Models.MohyoTweet();
            }
        }
        
        private RelayCommand _CloseCommand;
        public RelayCommand CloseCommand
        {
            get { return _CloseCommand ?? (_CloseCommand = new RelayCommand(Close)); }
            set { _CloseCommand = value; }
        }

        public void Close(object obj)
        {
            App.Current.Shutdown();
        }

        private RelayCommand _MohyoCommand;
        public RelayCommand MohyoCommand
        {
            get { return _MohyoCommand ?? (_MohyoCommand = new RelayCommand(TweetMohyo)); }
            set { _MohyoCommand = value; }
        }

        private void LoadToken()
        {
            try
            {
                var keyInfo = Models.KeyParser.ReadKey("keyinfo.xml");

                if (!string.IsNullOrWhiteSpace(keyInfo.CountMessage))
                    App.CountMessage = keyInfo.CountMessage;

                if (!string.IsNullOrWhiteSpace(keyInfo.UserWordListName))
                    App.UserWordListName = keyInfo.UserWordListName;

                App.PostCountMessage = keyInfo.PostCountMessage;
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

        private void TweetMohyo(object obj)
        {
            if (App.Token == null)
            {
                LoadToken();
            }
            else
            {
                Task<StatusResponse> res;

                if (App.PostCountMessage)
                    res = mohyoTweet.Post(App.Token, App.CountMessage);
                else
                    res = mohyoTweet.Post(App.Token);

                res.ContinueWith((r) =>
                {
                    try
                    {
                        if (r.Result == null)
                        {
                            App.Current.Dispatcher.InvokeAsync(() => new WpfMessageBox("もひょれませんでした").Show());
                        }
                        else
                        {
                            App.Current.Dispatcher.InvokeAsync(() => new WpfMessageBox("もひょりました").Show());
                        }
                    }
                    catch
                    {
                        App.Current.Dispatcher.InvokeAsync(() => new WpfMessageBox("もひょれませんでした").Show());
                    }
                });
            }
        }
    }
}
