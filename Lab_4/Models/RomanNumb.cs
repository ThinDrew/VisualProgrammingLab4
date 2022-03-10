using System;
using RomanEx;


namespace RomanNumb
{
    public class RomanNumber : ICloneable, IComparable
    {
        //Конструктор получает число n, которое должен представлять объект класса

        ushort number;
        public ushort Number
        {
            set
            {
                number = value;
            }
            get => number;
        }

        public RomanNumber(ushort n)
        {
            if (n == 0 || n > 3999)
                throw new RomanNumberException("Ошибка: значение n <= 0", n);

            number = n;
        }

        //Сложение римских чисел
        public static RomanNumber operator +(RomanNumber? n1, RomanNumber? n2)
        {
           return Add(n1, n2);
        }

        public static RomanNumber Add(RomanNumber? n1, RomanNumber? n2)
        {
            if (n1 == null || n2 == null)
            {
                throw new ArgumentNullException(nameof(n1));
            }

            ushort tmp = checked((ushort)(n1.number + n2.number));

            if (tmp == 0)
                throw new RomanNumberException("Ошибка: значение n == 0", tmp);

            RomanNumber r = new RomanNumber(tmp);
            return r;
        }

        //Вычитание римских чисел
        public static RomanNumber operator -(RomanNumber? n1, RomanNumber? n2)
        {
            return Sub(n1, n2);
        }
        public static RomanNumber Sub(RomanNumber? n1, RomanNumber? n2)
        {
            if (n1 == null || n2 == null)
            {
                throw new ArgumentNullException(nameof(n1));
            }

            ushort tmp = checked((ushort)(n1.number - n2.number));

            if (tmp == 0)
                throw new RomanNumberException("Ошибка: значение n == 0", tmp);

            RomanNumber r = new RomanNumber(tmp);
            return r;
        }

        //Умножение римских чисел
        public static RomanNumber operator *(RomanNumber? n1, RomanNumber? n2)
        {
            return Mul(n1, n2);
        }

        public static RomanNumber Mul(RomanNumber? n1, RomanNumber? n2)
        {
            if (n1 == null || n2 == null)
            {
                throw new ArgumentNullException(nameof(n1));
            }

            ushort tmp = checked((ushort)(n1.number * n2.number));

            if (tmp <= 0)
                throw new RomanNumberException("Ошибка: значение n <= 0", tmp);

            RomanNumber r = new RomanNumber(tmp);
            return r;
        }

        //Целочисленное деление римских чисел
        public static RomanNumber operator /(RomanNumber? n1, RomanNumber? n2)
        {
            return Div(n1, n2);
        }
        public static RomanNumber Div(RomanNumber? n1, RomanNumber? n2)
        {
            if (n1 == null || n2 == null)
            {
                throw new ArgumentNullException(nameof(n1));
            }

            ushort tmp = checked((ushort)(n1.number / n2.number));

            if (tmp == 0)
                throw new RomanNumberException("Ошибка: значение меньше нуля", tmp);

            RomanNumber r = new RomanNumber(tmp);
            return r;
        }

        //Возвращает строковое представление римского числа
        public override string ToString()
        {
            if (number > 3999) throw new OverflowException("Ошибка: значение n > 3999");
            string symb = "IVXLCDM";
            int value = number, degrees = -1;
            while (value != 0)
            {
                degrees++;
                value /= 10;
            }

            string answer = "";
            value = number;
            int remainder;
            while (degrees >= 0)
            {
                remainder = value / (int)Math.Pow(10, degrees);

                if (remainder < 4)
                    for (int j = 0; j < remainder; j++)
                        answer += symb[degrees * 2];
                else if (remainder == 4)
                {
                    answer += symb[degrees * 2];
                    answer += symb[degrees * 2 + 1];
                }
                else if (remainder < 9)
                {
                    answer += symb[degrees * 2 + 1];
                    for (int j = 0; j < remainder - 5; j++)
                        answer += symb[degrees * 2];
                }
                else
                {
                    answer += symb[degrees * 2];
                    answer += symb[degrees * 2 + 2];
                }
                value %= (int)Math.Pow(10, degrees);
                degrees--;
            }
            return answer;
        }
        //Реализация интерфейса IClonable
        public object Clone()
        {

            return new RomanNumber(number);

        }
        //Реализация интерфейса IComparable
        public int CompareTo(object? obj)
        {
            if (obj is RomanNumber number) return this.number.CompareTo(number.Number);
            else throw new ArgumentException("Некорректное значение параметра");
        }

        public static bool operator >(RomanNumber operand1, RomanNumber operand2)
        {
            return operand1.CompareTo(operand2) > 0;
        }


        public static bool operator <(RomanNumber operand1, RomanNumber operand2)
        {   
            return operand1.CompareTo(operand2) < 0;
        }

        public static bool operator >=(RomanNumber operand1, RomanNumber operand2)
        {
            return operand1.CompareTo(operand2) >= 0;
        }

        public static bool operator <=(RomanNumber operand1, RomanNumber operand2)
        {
            return operand1.CompareTo(operand2) <= 0;
        }

        

    }

}