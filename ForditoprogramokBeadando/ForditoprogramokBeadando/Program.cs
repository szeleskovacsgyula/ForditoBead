using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace forditoProgram
{
    public class Program
    {
        public static string[,] rules = new string[12, 7];
        public static string ruleNumber = "";
        public static string input;
        public static Stack stack;
        public static string sAct;
        
        static void Main(string[] args)
        {
            stack = new Stack();
            stack.Push("#");
            stack.Push("E");

            ReadFile();

            Console.Clear();

            input = "(i+i)*i#";

            string patternNumber = @"([0-9]+)";

            input = Regex.Replace(input, patternNumber, "i");

            do
            {
                for (int i = 0; i < rules.GetLength(1); i++)
                {
                    if (input[0].ToString() == rules[0, i])
                    {
                        sAct = stack.Pop().ToString();
                        for (int j = 0; j < rules.GetLength(0); j++)
                        {
                            if (sAct == rules[j, 0])
                            {
                                Console.WriteLine("({0}, \t{1}, \t{2})", input, StackList(stack), ruleNumber);
                                Evaluate(rules[j, i]);
                                break;
                            }
                        }
                    }
                }
            } while (input != "#" || stack.Count > 0);

            Console.ReadKey();
        }

        public static void ReadFile()
        {
            StreamReader sr = new StreamReader("rule.txt");
            string row = "";
            string[] rowArray = new string[7];
            int i = 0;
            while (!sr.EndOfStream)
            {
                row += sr.ReadLine();
                for (int j = 0; j < rowArray.Length; j++)
                {
                    rowArray[j] = row.Split('|')[j];
                }
                for (int k = 0; k < rowArray.Length; k++)
                {
                    rules[i, k] = rowArray[k];
                }
                row = "";
                i++;
            }
        }
        
        public static string StackList(Stack stack)
        {
            string elements = "";
            string[] stackArray = new string[stack.Count];
            stack.CopyTo(stackArray, 0);

            for (int i = 0; i < stackArray.Length; i++)
            {
                elements += stackArray[i];
            }

            if (stack.Count == 0)
            {
                return "#";
            }
            else
            {
                return elements;
            }
        }

        public static void Evaluate(string data)
        {
            if (data.Trim().Length == 0)
            {
                Console.Write("Az automata elutasító állapotban állt meg");
            }

            if (data.Trim() == "elfogad")
            {
                Console.Write("Az automata elfogadó állapotban állt meg");
            }

            if (data.Trim() == "pop")
            {
                //stack.Pop(); empty
                input = input.Substring(1);
            }

            if (data.Contains('(') && data.Contains(')'))
            {
                string datas = data.Substring(1).Split(',')[0];
                for (int j = datas.Length - 1; j >= 0; j--)
                {
                    stack.Push(datas[j].ToString());
                }
                ruleNumber += data.Substring(0, data.Length - 1).Split(',')[1];
            }
        }
    }
}
