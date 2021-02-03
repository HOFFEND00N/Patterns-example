using System;
using System.Collections.Generic;

namespace Visitor
{
    class Program
    {
        static void Main(string[] args)
        {
            Rectangle rectangle = new Rectangle(1, 2);
            Circle circle = new Circle(1, 1, 4);

            IShape shape = new Circle(1,2,3);
            ConsoleLogger.Log(shape);

            ConsoleLogVisitor consoleLogVisitor = new ConsoleLogVisitor();

            rectangle.AcceptVisitor(consoleLogVisitor);
            ConsoleLogger.Log(rectangle);

            circle.AcceptVisitor(consoleLogVisitor);
            ConsoleLogger.Log(circle);
        }

        interface IVisitor
        {
            public void Accept(Rectangle rectangle);
            public void Accept(Circle circle);
        }

        interface IShape
        {
            public void AcceptVisitor(IVisitor visitor);
        }

        class ConsoleLogVisitor : IVisitor
        {
            public void Accept(Rectangle rectangle)
            {
                Console.WriteLine("Rectangle. Height = {0}, Width = {1}", rectangle.Height, rectangle.Width);
            }

            public void Accept(Circle circle)
            {
                Console.WriteLine("Circle. X = {0}, Y = {1}, R = {2}", circle.X, circle.Y, circle.R);
            }
        }

        static class ConsoleLogger 
        {
            //visitor better because avoid if statements
            public static void Log(IShape shape)
            {
                if (shape is Circle)
                    Log(shape as Circle);
                else
                    Log(shape as Rectangle);
            }

            public static void Log(Rectangle rectangle)
            {
                Console.WriteLine("Rectangle. Height = {0}, Width = {1}", rectangle.Height, rectangle.Width);
            }

            public static void Log(Circle circle)
            {
                Console.WriteLine("Circle. X = {0}, Y = {1}, R = {2}", circle.X, circle.Y, circle.R);
            }
        }

        class Rectangle : IShape
        {
            public double Width { get; set; }
            public double Height { get; set; }

            public Rectangle(double width, double height)
            {
                Width = width;
                Height = height;
            }

            public void AcceptVisitor(IVisitor visitor)
            {
                visitor.Accept(this);
            }
        }

        class Circle : IShape
        {
            public double X { get; set; }
            public double Y { get; set; }
            public double R { get; set; }

            public Circle(double x, Double y, double r)
            {
                X = x;
                Y = y;
                R = r;
            }

            public void AcceptVisitor(IVisitor visitor)
            {
                visitor.Accept(this);
            }
        }
    }
}
