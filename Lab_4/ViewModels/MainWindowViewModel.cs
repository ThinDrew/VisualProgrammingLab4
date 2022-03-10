using System;
using System.Collections.Generic;
using System.Text;
using System.Reactive;
using ReactiveUI;
using RomanNumbExt;
using RomanNumb;
using RomanEx;

namespace lab4.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        string textField;
        static bool isExceptionOnScreen;
        public string TextField
        {
            set => this.RaiseAndSetIfChanged(ref textField, value);
            get => textField;
        }

        public MainWindowViewModel()
        {
            isExceptionOnScreen = false;
            OnClickCommand = ReactiveCommand.Create<string, string>((str) => ClearExceptionAndAddSymbolIfPressed(str));
            OnClickOperation = ReactiveCommand.Create<string, string>((str) => CheckOperation(str));
            OnClickDelete = ReactiveCommand.Create(() => Delete());
            OnClickClear = ReactiveCommand.Create(() => Clear());

            OnClickEqual = ReactiveCommand.Create(() => TextField = Calculate(TextField));
        }
        public ReactiveCommand<string, string> OnClickOperation { get; }

        public string ClearExceptionAndAddSymbolIfPressed(string str)
        {
            if (isExceptionOnScreen)
            {
                Clear();
                isExceptionOnScreen = false;
            }
            return TextField += str;
        }

        public string Clear ()
        {
            TextField = TextField.Remove(0, TextField.Length);
            return TextField;
        }
        public string CheckOperation(string? str)
        {
            if (str == null || TextField == null) throw new RomanNumberException("Для алгебрарической операции необходимы операнды");

            if (TextField.Length == 0) return TextField;
            else if (TextField[^1] == str[0]) return TextField;

            switch (TextField[^1])
            {
                case '+':
                case '-':
                case '/':
                case '*': return TextField = TextField.Remove(TextField.Length - 1, 1) + str;
            }
            return TextField += str;
        }

        public string Delete ()
        {
            TextField = TextField.Remove(TextField.Length - 1, 1);
            return TextField;
        }

        private delegate RomanNumber Operation(RomanNumber? rn1, RomanNumber? rn2);

        public static string Calculate(string str)
        {
            char[] symbSeparator = { '+', '-', '*', '/' };
            string[] data = str.Split(symbSeparator);
            List<RomanNumberExtend> numbers = new List<RomanNumberExtend>();

            try
            {
                //Заполняем список чисел
                for (int i = 0; i < data.Length; i++)
                {
                    if (data[i] != "")
                    {
                        RomanNumberExtend newMember = new RomanNumberExtend(data[i]);
                        numbers.Add(newMember);
                    }
                }

                List<char> symbOperationInStr = new List<char>();
                string tempString = (string)str.Clone();
                //Заполняем список операций
                while (true)
                {
                    int currentIndex = tempString.IndexOfAny(symbSeparator);
                    if (currentIndex == -1) break;
                    symbOperationInStr.Add(tempString[currentIndex]);
                    currentIndex += 1;
                    tempString = tempString.Substring(currentIndex);
                }

                char currentCharOperation = '*';
                Operation oper = RomanNumber.Mul;

                for (int i = 0; i < 4 && symbOperationInStr.Count != 0; i++)
                {
                    switch (i)
                    {
                        case 0:
                            currentCharOperation = '*';
                            oper = RomanNumber.Mul;
                            break;
                        case 1:
                            currentCharOperation = '/';
                            oper = RomanNumber.Div;
                            break;
                        case 2:
                            currentCharOperation = '+';
                            oper = RomanNumber.Add;
                            break;
                        case 3:
                            currentCharOperation = '-';
                            oper = RomanNumber.Sub;
                            break;
                    }

                    int currenOperationIndex = 0;

                    while (currenOperationIndex < symbOperationInStr.Count)
                    {
                        if (symbOperationInStr[currenOperationIndex] == currentCharOperation)
                        {
                            CalculationOperation(currenOperationIndex, oper);
                        }
                        else
                        {
                            currenOperationIndex++;
                        }

                    }

                }

                void CalculationOperation(int index, Operation oper)
                {
                    RomanNumberExtend tempObject = new RomanNumberExtend(oper(numbers[index], numbers[index + 1]).ToString());
                    numbers.Insert(index + 1, tempObject);
                    numbers.RemoveAt(index);
                    numbers.RemoveAt(index + 1);
                    symbOperationInStr.RemoveAt(index);
                }

                return numbers[0].ToString();
            }
            catch (RomanNumberException Ex)
            {
                isExceptionOnScreen = true;
                return Ex.Message;
   
            }
        }

        public ReactiveCommand<string, string> OnClickCommand { get; }
        public ReactiveCommand<Unit, string> OnClickDelete { get; }
        public ReactiveCommand<Unit, string> OnClickEqual { get; }
        public ReactiveCommand<Unit, string> OnClickClear { get; }
    }
   
}
