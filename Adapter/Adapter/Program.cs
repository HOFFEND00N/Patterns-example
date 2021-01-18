using System;

namespace Adapter
{
    class Program
    {
        static void Main(string[] args)
        {
            // Избавиться от 2 типов точек либо от 2 калькуляторов?
            // слишком много сущностей, нужен минимальный набор классов
            var calculator = new PolarCalculator();
            DecartCalculator target = new PolarCalculatorAdapter(calculator);

            Console.WriteLine(target.CalculateDistance(0, 0, 1, 1));
        }
    }

    class DecartCalculator
    {
        public virtual double CalculateDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }
    }

    class PolarCalculator
    {
        public double GetDistance(double r1, double phi1, double r2, double phi2)
        {
            return Math.Sqrt(r1 * r1 + r2 * r2 - 2 * r1 * r2 * Math.Cos(phi2 - phi1));
        }
    }

    class PolarCalculatorAdapter : DecartCalculator
    {
        private PolarCalculator calculator;
        public PolarCalculatorAdapter(PolarCalculator calculator)
        {
            this.calculator = calculator;
        }

        public override double CalculateDistance(double x1, double y1, double x2, double y2)
        {
            var p1 = ConvertToPolarPoint(x1, y1);
            var p2 = ConvertToPolarPoint(x2, y2);
            return calculator.GetDistance(p1.Item1, p1.Item2, p2.Item1, p2.Item2);
        }

        private Tuple<double, double> ConvertToPolarPoint(double x, double y)
        {
            double r = Math.Sqrt(x * x + y * y);
            double phi = 0;
            if (x > 0 && y >= 0)
                phi = Math.Atan(y / x);
            if (x > 0 && y < 0)
                phi = Math.Atan(y / x) + Math.PI * 2;
            if (x < 0)
                phi = Math.Atan(y / x) + Math.PI;
            if (x == 0 && y > 0)
                phi = Math.PI / 2;
            if (x == 0 && y < 0)
                phi = 3 * Math.PI / 2;
            return new Tuple<double, double>(r, phi);
        }
    }
}
