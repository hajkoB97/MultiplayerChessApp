using System;
using System.Linq;

namespace ChessAppLibrary.Utils
{
    public class TokenGenerator
    {
        public static string GenerateGameToken()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 10)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
