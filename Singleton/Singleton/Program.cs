using System;
using System.Collections.Generic;

namespace Singleton
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = DecartCalculatorSingleton.GetInstance();
            Console.WriteLine(a.GetDistance(1, 1, 2, 2));
        }

        class DecartCalculatorSingleton
        {
            private Dictionary<Key, double> calculatorCache = new Dictionary<Key, double>();
            static private DecartCalculatorSingleton calculator;
            private DecartCalculatorSingleton()
            {
            }

            public static DecartCalculatorSingleton GetInstance()
            {
                if(calculator == null)
                    calculator = new DecartCalculatorSingleton();
                return calculator;
            }

            public double GetDistance(double x1, double y1, double x2, double y2)
            {
                var hash = new Key(x1, y1, x2, y2);
                if (!calculatorCache.ContainsKey(hash))
                    calculatorCache.Add(hash, calculator.GetDistance(x1, y1, x2, y2));

                return calculatorCache[hash];
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
    }
}
