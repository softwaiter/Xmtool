using System;

namespace CodeM.Common.Tools
{
    public class RandomTool
    {
        private static RandomTool sRTool = new RandomTool();

        private RandomTool()
        { 
        }

        internal static RandomTool New()
        {
            return sRTool;
        }

        private static char[] sCaptchaChars = {'2', '3', '4', '5', '6', '7', '8', '9',
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N',
            'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b',
            'c', 'd', 'e', 'f', 'g', 'h', 'j', 'k', 'm', 'n', 'p', 'q', 'r',
            's', 't', 'u', 'v', 'w', 'x', 'y', 'z'};

        public string RandomCaptcha(int len, bool onlyNumber = false)
        {
            if (onlyNumber)
            {
                string result = string.Empty;
                int step = (int)DateTime.Now.Ticks % int.MaxValue;
                Random r = new Random(step);
                for (int i = 0; i < len; i++)
                {
                    int num = r.Next(0, int.MaxValue) % 10;
                    result += num;
                }
                return result;
            }
            else
            {
                string result = string.Empty;
                int step = (int)DateTime.Now.Ticks % int.MaxValue;
                Random r = new Random(step);
                for (int i = 0; i < len; i++)
                {
                    int index = r.Next(0, int.MaxValue) % sCaptchaChars.Length;
                    result += sCaptchaChars[index];
                }
                return result;
            }
        }
    }
}
