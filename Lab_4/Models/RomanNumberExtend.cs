using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RomanNumb;
using RomanEx;

namespace RomanNumbExt
{
    public class RomanNumberExtend : RomanNumber
    {
        public RomanNumberExtend(string num) : base(2)
        {
            Number = 0;
            var symb = new Dictionary<char, int>()
            {
                { 'I', 1 },
                { 'V', 5 },
                { 'X', 10 },
                { 'L', 50 },
                { 'C', 100 },
                { 'D', 500 },
                { 'M', 1000 }
            };
            if (!symb.ContainsKey(num[0]))
            {
                throw new RomanNumberException("Неверный символ в римском числе");
            }
            for (int i = 0; i < num.Length - 1; i++)
            {
                if (!symb.ContainsKey(num[i + 1]))
                {
                    throw new RomanNumberException("Неверный символ в римском числе");
                }

                if (symb[num[i]] >= symb[num[i + 1]])
                    Number += (ushort)symb[num[i]];
                else
                    Number -= (ushort)symb[num[i]];
            }
            Number += (ushort)symb[num[num.Length - 1]];
        }

    }
}