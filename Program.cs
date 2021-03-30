using System;
using System.IO;
using System.Linq;
using static System.Console;

namespace ConsoleApp_Skillbox_01_
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("data.txt");
            var questions = lines
                .Select(s => s.Split('|'))
                .Select(s => (s[0], s[1]))
                .ToList();

            var score = 0;
            var random = new Random();
            var counter = questions.Count;
            while (true)
            {
                if (counter < 1)
                {
                    counter = questions.Count;
                }
                var index = random.Next(counter -1);
                var question = questions[index];

                questions.RemoveAt(index);
                questions.Add(question);
                counter--;

                var openedLetters = 0;
                
                while (openedLetters < question.Item2.Length)
                {
                    var answer = question.Item2
                                        .Substring(0, openedLetters)
                                        .PadRight(question.Item2.Length, '_');
                    ForegroundColor = ConsoleColor.Yellow;
                    WriteLine($"{question.Item1}" + "\n" + $"({question.Item2.Length} букв)");
                    ForegroundColor = ConsoleColor.Green;
                    WriteLine(answer);
                    var tryAnswer = ReadLine();
                    if (tryAnswer.Trim().ToLowerInvariant() == question.Item2.Trim().ToLowerInvariant())
                    {
                        score++;
                        WriteLine($"Правильно! Это — {question.Item2}");
                        WriteLine($"У вас {score} баллов!");
                        openedLetters = -1;
                        break;
                    }
                    else
                    {
                        openedLetters++;
                    }
                }
                if (openedLetters != -1)
                {
                    ForegroundColor = ConsoleColor.Red;
                    WriteLine($"Никто не отгадал! Это — {question.Item2}");
                }
                
            }
        }
    }
}
