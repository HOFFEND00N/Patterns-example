using System;

namespace Mediator
{
    class Program
    {
        static void Main(string[] args)
        {
            Mediator mediator = new Mediator();
            LightBulb lightBulb = new LightBulb(mediator);
            Switcher switcher = new Switcher(mediator);
            
            mediator.SetLightBulb(lightBulb);
            mediator.SetSwitcher(switcher);

            switcher.On();

            switcher.Off();

            lightBulb.BurnedOut();
        }

        class Mediator
        {
            private Switcher switcher;
            private LightBulb lightBulb;

            public void SetSwitcher(Switcher switcher)
            {
                this.switcher = switcher;
            }

            public void SetLightBulb(LightBulb lightBulb)
            {
                this.lightBulb = lightBulb;
            }

            public void Notify(Command command)
            {
                switch (command)
                {
                    case Command.TurnOnLigth:
                        lightBulb.TurnOn();
                        break;
                    case Command.TurnOffLight:
                        lightBulb.TurnOff();
                        break;
                    case Command.LightBulbBurnedOut:
                        switcher.Off();
                        break;
                    default:
                        break;
                }
            }
        }

        enum Command
        {
            TurnOnLigth,
            TurnOffLight,
            LightBulbBurnedOut
        }

        class Switcher
        {
            private Mediator mediator;

            public Switcher(Mediator mediator)
            {
                this.mediator = mediator;
            }

            public void On()
            {
                Console.WriteLine("Switcher activated");
                mediator.Notify(Command.TurnOnLigth);
            }

            public void Off()
            {
                Console.WriteLine("Switcher deactivated");
                mediator.Notify(Command.TurnOffLight);
            }
        }

        class LightBulb
        {
            private Mediator mediator;
            public LightBulb(Mediator mediator)
            {
                this.mediator = mediator;
            }

            public void TurnOn()
            {
                Console.WriteLine("Light bulb is on");
            }

            public void TurnOff()
            {
                Console.WriteLine("Light bulb is off");
            }

            public void BurnedOut()
            {
                Console.WriteLine("Light bulb burned out");
                mediator.Notify(Command.LightBulbBurnedOut);
            }
        }
    }
}
