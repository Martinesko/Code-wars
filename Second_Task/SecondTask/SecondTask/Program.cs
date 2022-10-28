using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Channels;

namespace SecondTask
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> listOfRiddles = new List<string>();
            ListFiller(listOfRiddles);
            while (true)
            { 
                Guide();
                string inputLine = Console.ReadLine();
                if (inputLine.Length == 1 && !char.IsDigit(inputLine[0]))
                {
                    Console.WriteLine("There is no such command");
                    continue;
                }

                int command = int.Parse(inputLine);
                switch (command)
                {
                    case 1:
                        AddRiddleLogic(listOfRiddles);
                        break;
                    case 2:
                        RandomRiddleLogic(listOfRiddles);
                        break;
                    case 3:
                        CountOfList(listOfRiddles);
                        break;
                    case 4:
                        ClearFile(listOfRiddles);
                        break;
                    case 5:
                        Console.WriteLine("Program exited");
                        Thread.Sleep(2000);
                        return;
                    default:
                        Console.WriteLine("There is no such command");
                        Thread.Sleep(2000);
                        Console.Clear();
                        break;
                }
            }
        }
        static void ListFiller(List<string> listOfRiddles)
        {
            var Reader = new StreamReader(@"..\..\..\Files\riddles.txt");
            using (Reader)
            {
                string[] riddles = Reader.ReadToEnd().Trim().Split('\n');
                if (!string.IsNullOrWhiteSpace(riddles[0]))
                {
                    foreach (var riddle in riddles)
                    {
                        listOfRiddles.Add(riddle+'\n');
                    }
                }
            }
        }
        static void WriteRiddle(string riddle,string answer,List<string> listOfRiddles)
        {
            string riddleAndAnswer = riddle + "|" + answer;
            listOfRiddles.Add(riddleAndAnswer);
            File.AppendAllText(@"..\..\..\Files\riddles.txt",riddleAndAnswer + "\n");

        }
        static string RandomRiddle(List<string> listOfRiddles)
        {
            if (listOfRiddles.Any())
            {
                Random rd = new Random();
                int randomRiddleIndex = rd.Next(0, listOfRiddles.Count);
                return listOfRiddles[randomRiddleIndex];
            }
            else
            {
                return "There is no riddles in the List";
            }
        }
        static void RandomRiddleLogic(List<string> listOfRiddles)
        {
            string riddleAndAnswer = (RandomRiddle(listOfRiddles));
            if (riddleAndAnswer == "There is no riddles in the List")
            {
                Console.WriteLine("There is no riddles in the List");
                return;
            }
            string randomRiddle = riddleAndAnswer.Split('|')[0];
            string rightAnswer = riddleAndAnswer.Split('|')[1].TrimEnd();
            Console.WriteLine($"Your riddle is {randomRiddle}");
            int counter = 5;
            while (true)
            {
                counter--;
                Console.Write("Enter your answer:");
                string enteredAnswer = Console.ReadLine();
                if (enteredAnswer == rightAnswer)
                {
                    Console.WriteLine("Correct answer!");
                    Thread.Sleep(1000);
                    Console.Clear();
                    break;
                }
                else if (counter == 0)
                {
                    Console.WriteLine("Wrong answer!");
                    Console.WriteLine($"You have no more tries");
                    Console.WriteLine($"The answer was {rightAnswer}");
                    Thread.Sleep(1300);
                    Console.Clear();
                    break;
                }
                else
                {
                    Console.WriteLine("Wrong answer!");
                    Console.WriteLine($"You have {counter} more tries!");
                }

            }
        }
        static void AddRiddleLogic(List<string> listOfRiddles)
        {
            Console.Write("Enter riddle: ");
            string riddle = Console.ReadLine();
            Console.Write("Enter answer: ");
            string answer = Console.ReadLine();
            WriteRiddle(riddle, answer, listOfRiddles);
            Console.WriteLine("You successfully added a riddle");
            Thread.Sleep(750);
            Console.Clear();
        }
        static void CountOfList(List<string> listOfRiddles)
        {
            Console.WriteLine($"There is {listOfRiddles.Count} in the list");
            Thread.Sleep(3000);
            Console.Clear();
        }
        static void ClearFile(List<string> listOfRiddles)
        {
            string riddlesFIlePath = @"..\..\..\Files\riddles.txt";
            var riddleCleaner = new StreamWriter(riddlesFIlePath);
            using (riddleCleaner)
            {
                riddleCleaner.Write("");
            }
            listOfRiddles.Clear();
            Console.WriteLine("All riddles Cleared");
            Thread.Sleep(1000);
            Console.Clear();
        }
       
        static void Guide()
        {
            Console.WriteLine("#########################" +
                              "\n[1] new riddle" +
                              "\n[2] random riddle" +
                              "\n[3] count of riddle" +
                              "\n[4] clear all riddles" +
                              "\n[5] exit program" +
                              "\n#########################");
            Console.Write("Enter number: ");
        }
    }
}