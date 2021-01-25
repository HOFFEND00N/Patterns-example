using System;
using System.IO;

namespace Strategy
{
    class Program
    {
        static void Main(string[] args)
        {
            Loger loger = new Loger(new FileLoggingStrategy());
            loger.DoLogging("Some logging");
        }

        class Loger
        {
            ILoggingStrategy strategy;

            public Loger(ILoggingStrategy strategy)
            {
                this.strategy = strategy;
            }

            public void DoLogging(string text)
            {
                strategy.LogData(text);
            }
        }

        interface ILoggingStrategy
        {
            public void LogData(string text);
        }

        class ConsoleLoggingStrategy : ILoggingStrategy
        {
            public void LogData(string text)
            {
                Console.WriteLine(text);
            }
        }

        class FileLoggingStrategy : ILoggingStrategy
        {
            public void LogData(string text)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "output.txt");

                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.Write(text);
                }
            }
        }
    }
}
