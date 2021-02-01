using System;

namespace ObserverWithEventsAndDelegates
{
    class Program
    {
        static void Main(string[] args)
        {
            WashingMachine washingMachine = new WashingMachine();
            Phone phone1 = new Phone("Apple");
            Phone phone2 = new Phone("Xiaomi");

            washingMachine.washingMachineMessage += phone1.WashingMachineRecieved;
            washingMachine.washingMachineMessage += phone2.WashingMachineRecieved;
            //phone1.Subscribe(washingMachine);
            //phone2.Subscribe(washingMachine);

            washingMachine.SendMessage(Message.Complete);

            washingMachine.SendMessage(Message.Error);

            //phone1.Unsubscribe();
            washingMachine.washingMachineMessage -= phone1.WashingMachineRecieved;

            washingMachine.SendMessage(Message.Complete);
        }

        class WashingMachine
        {
            public event Action<WashingMachine, Message> washingMachineMessage;

            protected virtual void OnWashingMachineMessageSent(Message message)
            {
                if (washingMachineMessage != null)
                    washingMachineMessage(this, message);
            }

            public void SendMessage(Message message)
            {
                if (message == Message.Complete)
                    OnWashingMachineMessageSent(message);
                else
                    Console.WriteLine("Error, there is no power supply");
            }
        }

        enum Message
        {
            Complete,
            Error
        }

        class Phone
        {
            public string Brand { get; set; }

            public Phone(string brand)
            {
                Brand = brand;
            }

            public void WashingMachineRecieved(object sender, Message message)
            {
                Console.WriteLine("Get this message = {0}, from {1}", message.ToString(), sender.ToString());
                Console.WriteLine("brand = {0}", Brand);
            }
        }
    }
}
