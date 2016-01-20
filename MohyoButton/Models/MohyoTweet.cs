using CoreTweet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MohyoButton.Models
{
    public class MohyoTweet
    {
        public MohyoTweet()
        {
            InitMohyoStrings();
            PostFromUserList = false;
        }

        public MohyoTweet(IEnumerable<string> wordList)
        {
            if (!wordList.Any(x => !string.IsNullOrWhiteSpace(x)))
                throw new ArgumentNullException();

            _MohyoStrings = wordList.ToList();
            PostFromUserList = true;
        }

        private void InitMohyoStrings()
        {
            _MohyoStrings = new List<string> {
                "もひょ",
                "もひょっ",
                "もひょぉ",
                "もひょもひょ",
                "もひょもひょっ",
                "もひょもひょぉ",
                "もひょもひょもひょもひょ",
                "＞ω＜もひょ",
                "(~´ω`)~もひょ",
                "~(´ω`~)もひょ",
                "(～＞ω＜)～もひょ",
                "～(＞ω＜～)もひょ",
                "～(・ω・～)もひょ",
                "(～・ω・)～もひょ",
                "進捗もひょです",
                "mohyo",
                "むいっ",
                "むきゅーっ",
            };
        }

        private MT19937 mt = new MT19937((uint)(DateTime.Now.ToFileTimeUtc() % uint.MaxValue));

        public bool PostFromUserList { get; private set; }

        private List<string> _MohyoStrings;
        public List<string> MohyoStrings
        {
            get { return _MohyoStrings; }
            private set { _MohyoStrings = value; }
        }

        public Task<StatusResponse> Post(Tokens tokens)
        {
            try
            {
                if (!PostFromUserList)
                {
                    App.MohyoCount++;
                }
                var res = tokens.Statuses.UpdateAsync(status => MohyoStrings[GetRandomNumber()]);
                return res;
            }
            catch (TwitterException ex)
            {
//                App.MohyoCount--;
//                new WpfMessageBox(ex.Message).Show();
                return null;
            }
        }

        public Task<StatusResponse> Post(Tokens tokens, string addMessage)
        {
            try
            {
                if (!PostFromUserList)
                {
                    App.MohyoCount++;
                    addMessage = addMessage.Replace("[COUNT]", App.MohyoCount.ToString());
                    var res = tokens.Statuses.UpdateAsync(status => MohyoStrings[GetRandomNumber()] + addMessage);
                    return res;
                }
                else
                {
                    var res = tokens.Statuses.UpdateAsync(status => MohyoStrings[GetRandomNumber()]);
                    return res;
                }
            }
            catch (TwitterException ex)
            {
//                App.MohyoCount--;
//                new WpfMessageBox(ex.Message).Show();
                return null;
            }
        }

        private int GetRandomNumber()
        {
            return Math.Abs((int)mt.GetInt32() % MohyoStrings.Count);
        }
    }
}
