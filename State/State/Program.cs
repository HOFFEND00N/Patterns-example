using System;

namespace State
{
    class Program
    {
        //this is a strategy pattern not State
        static void Main(string[] args)
        {
            Paint paint = new Paint();
            Circle circle = new Circle();
            Line line = new Line();
            Star star = new Star();

            paint.SetShape(line);
            paint.MouseButtonPressed();
            paint.MouseButtonReleased();
            paint.DrawShape();


            paint.SetShape(circle);
            paint.MouseButtonPressed();
            paint.MouseButtonReleased();
            paint.DrawShape();


            paint.SetShape(star);
            paint.MouseButtonPressed();
            paint.MouseButtonReleased();
            paint.DrawShape();
        }

        class Paint
        {
            IShape shape;

            public void SetShape(IShape shape)
            {
                this.shape = shape;
            }

            public void MouseButtonPressed()
            {
                shape.MouseButtonPressed();
            }

            public void MouseButtonReleased()
            {
                shape.MouseButtonReleased();
            }

            public void DrawShape()
            {
                shape.Draw();
            }
        }

        interface IShape
        {
            public void MouseButtonPressed();
            public void MouseButtonReleased();
            public void Draw();
        }

        class Line : IShape
        {
            private Point start, finish;

            public void MouseButtonPressed()
            {
                start = RandomPointGenerator.GeneratePoint();
            }

            public void MouseButtonReleased()
            {
                finish = RandomPointGenerator.GeneratePoint();
            }

            public void Draw()
            {
                Console.WriteLine("Line has been drawn");
                start.ConsolePrint();
                finish.ConsolePrint();
            }
        }

        class Circle : IShape
        {
            private Point start, finish;

            public void MouseButtonPressed()
            {
                start = RandomPointGenerator.GeneratePoint();
            }

            public void MouseButtonReleased()
            {
                finish = RandomPointGenerator.GeneratePoint();
            }

            public void Draw()
            {
                Console.WriteLine("Circle has been drawn");
                start.ConsolePrint();
                finish.ConsolePrint();
            }
        }

        class Star : IShape
        {
            private Point start, finish;

            public void MouseButtonPressed()
            {
                start = RandomPointGenerator.GeneratePoint();
            }

            public void MouseButtonReleased()
            {
                finish = RandomPointGenerator.GeneratePoint();
            }

            public void Draw()
            {
                Console.WriteLine("Star has been drawn");
                start.ConsolePrint();
                finish.ConsolePrint();
            }
        }

        class Point
        {
            public double x, y;

            public Point(double x, double y)
            {
                this.x = x;
                this.y = y;
            }

            public void ConsolePrint()
            {
                Console.WriteLine("X = {0}, Y = {1}", x, y);
            }
        }

        static class RandomPointGenerator
        {
            public static Point GeneratePoint()
            {
                Random random = new Random();
                return new Point(random.NextDouble() * 10, random.NextDouble() * 10);
            }
        }
    }
}
