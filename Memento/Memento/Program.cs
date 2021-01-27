using System;
using System.Collections.Generic;

namespace Memento
{
    class Program
    {
        //compare methods from mt example vs RefactoringGuru example
        //memento what problem solves
        static void Main(string[] args)
        {
            TextEditor textEditor = new TextEditor(new TextWindow());
            textEditor.Write("number 1\n");
            textEditor.Save();

            textEditor.Write("number 2\n");
            textEditor.Save();

            textEditor.Write("number 3\n");
            Console.WriteLine(textEditor.Print());

            textEditor.Undo();

            Console.WriteLine(textEditor.Print());

            textEditor.Undo();

            Console.WriteLine(textEditor.Print());
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

            public string Print()
            {
                return textWindow.currentText;
            }

            public void Save()
            {
                savedTextWindows.Push(textWindow.Save());
            }

            public void Undo()
            {
                textWindow.Restore(savedTextWindows.Pop());
            }

            public void Write(string text)
            {
                textWindow.addText(text);
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

            public void addText(string text)
            {
                currentText += text;
            }

            public TextWindowMemento Save()
            {
                return new TextWindowMemento(currentText);
            }

            public void Restore(TextWindowMemento memento)
            {
                currentText = memento.Text;
            }
        }
    }
}
