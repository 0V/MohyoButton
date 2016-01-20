using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace MohyoButton.Models
{
    public class KeyParser
    {
        public static KeyInfo ReadKey(string fileName)
        {
            var serializer = new XmlSerializer(typeof(KeyInfo));
            using (var sr = new StreamReader(
                fileName, new UTF8Encoding(false)))
            {
                var info = (KeyInfo)serializer.Deserialize(sr);
                return info;
            }
        }

        public static void WriteKey(string fileName, KeyInfo keyInfo)
        {
            var serializer = new XmlSerializer(typeof(KeyInfo));
            using (var streamWriter = new StreamWriter(
                fileName, false, new UTF8Encoding(false)))
            {
                serializer.Serialize(streamWriter, keyInfo);
            }
        }

        public static void WriteKey(string fileName, CoreTweet.Tokens tokens, string countMessage, bool postCountMessage, string userWordListName)
        {
            var keyInfo = TokensToKeyInfo(tokens, countMessage, postCountMessage, userWordListName);
            WriteKey(fileName, keyInfo);
        }

        public static KeyInfo TokensToKeyInfo(CoreTweet.Tokens tokens, string countMessage, bool postCountMessage, string userWordListName)
        {
            var info = new KeyInfo();
            info.AccessToken = tokens.AccessToken;
            info.AccessTokenSecret = tokens.AccessTokenSecret;
            info.ConsumerKey = tokens.ConsumerKey;
            info.ConsumerSecret = tokens.ConsumerSecret;
            info.CountMessage = countMessage;
            info.PostCountMessage = postCountMessage;
            info.UserWordListName = userWordListName;
            return info;
        }
    }
}