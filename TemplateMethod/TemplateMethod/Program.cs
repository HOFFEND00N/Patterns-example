using System;

namespace TemplateMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            Spamer TelegramSpamer = new TelegramSpamer();
            TelegramSpamer.StartSpaming("Some Telegram spam");

            Spamer EmailSpamer = new EmailSpamer();
            EmailSpamer.StartSpaming("Some Email spam");
        }

        //why strategy pattern is more popular than Template method pattern *
        //setMessage looks wierd
        //Example dont suit the Template method
        abstract class Spamer
        {
            protected string message;
            public void StartSpaming(string message)
            {
                SetMessage(message);
                SendMessage();
            }

            protected void SetMessage(string message)
            {
                this.message = message;
            }

            protected abstract void SendMessage();
        }

        class TelegramSpamer : Spamer
        {
            protected override void SendMessage()
            {
                Console.WriteLine("Send message to Telegram");
            }
        }

        class EmailSpamer: Spamer
        {
            protected override void SendMessage()
            {
                Console.WriteLine("Send message to Email");
            }
        }
    }
}
