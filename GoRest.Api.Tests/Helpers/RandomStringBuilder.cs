using System;
using System.Text;

namespace API_Tests.Helpers
{
    public class RandomStringBuilder
    {
        public string GenerateRandomStringOfSpecifiedLength(int length)
        {
            const string source = "abcdefghijklmn opqrstuvwxyz012 3456789";
            var output = new StringBuilder();
            Random random = new Random();
            for (var i = 0; i < length; i++)
            {
                var c = source[random.Next(0, source.Length)];
                output.Append(c);
            }
            return output.ToString();   
        }
    }
}
