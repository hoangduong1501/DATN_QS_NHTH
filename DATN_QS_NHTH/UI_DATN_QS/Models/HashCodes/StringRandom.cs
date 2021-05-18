using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UI_DATN_QS.Models.HashCodes
{
    public class StringRandom
    {
        public static string GeneratePassword(bool useUppercase = true, bool useNumbers = true, int passwordSize = 10)
        {
            const string UPPER_CASE = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string NUMBERS = "123456789";

            char[] _password = new char[passwordSize];
            string charSet = "";
            System.Random _random = new Random();
            int counter;


            if (useUppercase) charSet += UPPER_CASE;

            if (useNumbers) charSet += NUMBERS;

            for (counter = 0; counter < passwordSize; counter++)
            {
                _password[counter] = charSet[_random.Next(charSet.Length - 1)];
            }

            return string.Join(null, _password);
        }
    }
}