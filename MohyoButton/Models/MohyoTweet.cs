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
        private static MT19937 mt = new MT19937((uint)(DateTime.Now.ToFileTimeUtc() % uint.MaxValue));

        private static List<string> _MohyoStrings =
            new List<string> {
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
                "～(＞ω＜)～もひょ",
                "進捗もひょです",
                "Mohyo",
                "mohyo",
                "むいっ",
            };

        public static List<string> MohyoStrings{
            get {return _MohyoStrings; }
            private set { _MohyoStrings = value; }
        }

        public static Task<StatusResponse> Post(Tokens tokens)
        {
            try {
                var res = tokens.Statuses.UpdateAsync(status => MohyoStrings[GetRandomNumber()]);
                return res;
            }
            catch (TwitterException ex)
            {
                new WpfMessageBox(ex.Message).Show();
                return null;
            }
        }

        private static int GetRandomNumber()
        {
            return Math.Abs((int)mt.GetInt32() % MohyoStrings.Count);
        }
    }
}
