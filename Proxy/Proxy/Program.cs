using System;
using System.Collections.Generic;

namespace Proxy
{
    class Program
    {
        static void Main(string[] args)
        {
            ICalculator c = new ConsoleLogCalculatorProxy(new DecartCalculator());
            var a = new ConsoleLogCalculatorProxy(new CacheCalculatorProxy(new DecartCalculator()));
            c.GetDistance(0, 0, 1, 1);
            a.GetDistance(0, 0, 1, 1);
            a.GetDistance(0, 0, 1, 1);
        }

        interface ICalculator
        {
            public double GetDistance(double x1, double y1, double x2, double y2);
        }

        class DecartCalculator : ICalculator
        {
            public double GetDistance(double x1, double y1, double x2, double y2)
            {
                return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
            }
        }

        class CacheCalculatorProxy : ICalculator
        {
            private ICalculator calculator;
            private Dictionary<Key, double> calculatorCache = new Dictionary<Key, double>();
            public CacheCalculatorProxy(ICalculator calculator)
            {
                this.calculator = calculator;
            }
            //what is interface, what problem solves, constraints on Key of Dictionary, Refactor
            public double GetDistance(double x1, double y1, double x2, double y2)
            {
                var hash = new Key(x1, y1, x2, y2);
                if (!calculatorCache.ContainsKey(hash))
                    calculatorCache.Add(hash, calculator.GetDistance(x1, y1, x2, y2));

                return calculatorCache[hash];
            }

            public void test<TKey, TValue>(ICollection<KeyValuePair<TKey, TValue>> collection, KeyValuePair<TKey, TValue> pair)
            {
                collection.Add(pair);
            }

        }

        class Key
        {
            double x1, y1, x2, y2;
            public Key(double x1, double y1, double x2, double y2)
            {
                this.x1 = x1;
                this.y1 = y1;
                this.x2 = x2;
                this.y2 = y2;
            }

            public override bool Equals(Object obj)
            {
                if (!(obj is Key)) return false;

                Key p = (Key)obj;
                return (x1 == p.x1 && y1 == p.y1 && x2 == p.x2 && y2 == p.y2)
                        || (x1 == p.x2 && y1 == p.y2 && x2 == p.x1 && y2 == p.y1);
            }

            public override int GetHashCode()
            {
                return x1.GetHashCode() + y1.GetHashCode() + x2.GetHashCode() + y2.GetHashCode();
            }
        }

        class ConsoleLogCalculatorProxy : ICalculator
        {
            private ICalculator calculator;
            public ConsoleLogCalculatorProxy(ICalculator calculator)
            {
                this.calculator = calculator;
            }

            public double GetDistance(double x1, double y1, double x2, double y2)
            {
                Console.WriteLine("Method - GetDistance, date & time = {0}", DateTime.Now);
                return calculator.GetDistance(x1, y1, x2, y2);
            }
        }
    }
}
