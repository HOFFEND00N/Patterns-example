using System;
using System.Collections.Generic;

namespace Observer
{
    class Program
    {
        //do the same with events using

        static void Main(string[] args)
        {
            WashingMachine washingMachine = new WashingMachine();
            Phone phone1 = new Phone("Apple");
            Phone phone2 = new Phone("Xiaomi");

            phone1.Subscribe(washingMachine);
            phone2.Subscribe(washingMachine);

            washingMachine.SendMessage(Message.Complete);

            washingMachine.SendMessage(Message.Error);

            phone1.Unsubscribe();

            washingMachine.SendMessage(Message.Complete);
        }

        class WashingMachine : IObservable<string>
        {
            List<IObserver<string>> observers = new List<IObserver<string>>();

            public IDisposable Subscribe(IObserver<string> observer)
            {
                if (!observers.Contains(observer))
                    observers.Add(observer);
                return new Unsubscriber(observers, observer);
            }

            public void SendMessage(Message message)
            {
                foreach (var i in observers)
                    if (message == Message.Complete)
                        i.OnNext(message.ToString());
                    else
                        i.OnError(new Exception("Error, there is no power supply"));
            }
        }

        enum Message
        {
            Complete,
            Error
        }

        class Phone : IObserver<string>
        {
            private IDisposable cancellation;
            public string Brand { get; set; }

            public Phone(string brand)
            {
                Brand = brand;
            }

            public void Subscribe(WashingMachine provider)
            {
                cancellation = provider.Subscribe(this);
            }

            public void Unsubscribe()
            {
                cancellation.Dispose();
            }

            public void OnCompleted()
            {
                Console.WriteLine("All messages recieved");
            }

            public void OnError(Exception error)
            {
                Console.WriteLine(error.Message);
            }

            public void OnNext(string value)
            {
                Console.WriteLine("Phone {0} recieved notification - {1}", Brand, value);
            }
        }

        class Unsubscriber : IDisposable
        {
            private List<IObserver<string>> observers;
            private IObserver<string> observer;

            internal Unsubscriber(List<IObserver<string>> observers, IObserver<string> observer)
            {
                this.observers = observers;
                this.observer = observer;
            }

            public void Dispose()
            {
                if (observers.Contains(observer))
                    observers.Remove(observer);
            }
        }
    }
}
