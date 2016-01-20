using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MohyoButton.Models
{
    public class WordFileLoad
    {

        private static IEnumerable<string> ReadFile(string filePath)
        {
            string result = null;

            try
            {
                using (var sr = new StreamReader(filePath, Encoding.GetEncoding("utf-8")))
                    result = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw new FileLoadException("ファイルを読み込めませんでした",ex);
            }

            using (var sr = new StringReader(result)) {
                while (sr.Peek() != -1)
                    yield return sr.ReadLine();
            }
        }

        public static IEnumerable<string> Load(string filePath)
        {
            List<string> wordList = null;

            try
            {
                wordList = ReadFile(filePath).ToList();
            }
            catch (Exception e)
            {
                throw new FileLoadException("ファイルを読み込めませんでした", e);
            }

            return wordList;
        }
    }
}