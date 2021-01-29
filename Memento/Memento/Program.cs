using System;
using System.Collections.Generic;

namespace Memento
{
    class Program
    {
        static void Main(string[] args)
        {
            TextWindow textWindow = new TextWindow();
            TextEditor textEditor = new TextEditor(textWindow);

            textWindow.AddText("number 1\n");
            textEditor.Save();

            textWindow.AddText("number 2\n");
            textEditor.Save();

            textWindow.AddText("number 3\n");
            Console.WriteLine(textWindow.Print());

            textEditor.Undo();

            Console.WriteLine(textWindow.Print());

            textEditor.Undo();

            Console.WriteLine(textWindow.Print());
        }

        class TextEditor
        {
            private Stack<TextWindowMemento> savedTextWindows;
            TextWindow textWindow;

            public TextEditor(TextWindow textWindow)
            {
                savedTextWindows = new Stack<TextWindowMemento>();
                this.textWindow = textWindow;
            }

            public void Save()
            {
                savedTextWindows.Push(textWindow.GetMemento());
            }

            public void Undo()
            {
                if(savedTextWindows.Count > 0)
                    textWindow.Restore(savedTextWindows.Pop());
            }
        }

        class TextWindowMemento
        {
            public string Text { get; private set; }

            public TextWindowMemento(string text)
            {
                Text = text;
            }
        }

        class TextWindow
        {
            public string currentText;

            public void AddText(string text)
            {
                currentText += text;
            }

            public TextWindowMemento GetMemento()
            {
                return new TextWindowMemento(currentText);
            }

            public void Restore(TextWindowMemento memento)
            {
                currentText = memento.Text;
            }

            public string Print()
            {
                return currentText;
            }
        }
    }
}
