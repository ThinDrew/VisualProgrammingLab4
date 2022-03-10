using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RomanEx
{
    public class RomanNumberException : Exception
    {
        ushort number;
        public ushort Value
        {
            get => number;
        }

        public RomanNumberException() { }
        public RomanNumberException(string? message, ushort value) : base(message)
        {
            number = value;
        }

        
        

        public RomanNumberException(string? message) : base(message)
        {

        }
    }
}
