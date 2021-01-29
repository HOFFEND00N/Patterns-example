using System;

namespace State2
{
    class Program
    {
        static void Main(string[] args)
        {
            TurnStileManager manager = new TurnStileManager();
            LockedTurnstile lockedTurnstile = new LockedTurnstile(manager);
            UnlockedTurnstile unlockedTurnstile = new UnlockedTurnstile(manager);

            manager.SetTurnstileState(lockedTurnstile);
            manager.Push();
            manager.Pay();
            manager.Pay();
            manager.Push();
            manager.Push();

        }

        class TurnStileManager
        {
            ITurnstile turnstile;

            public void SetTurnstileState(ITurnstile turnstile)
            {
                this.turnstile = turnstile;
            }

            public void Pay()
            {
                turnstile.Pay();
            }

            public void Push()
            {
                turnstile.Push();
            }
        }

        interface ITurnstile
        {
            public void Pay();
            public void Push();
        }

        class LockedTurnstile : ITurnstile
        {
            private TurnStileManager manager;

            public LockedTurnstile(TurnStileManager manager)
            {
                this.manager = manager;
            }

            public void Pay()
            {
                Console.WriteLine("Pay operation was succeful");
                manager.SetTurnstileState(new UnlockedTurnstile(manager));
            }

            public void Push()
            {
                Console.WriteLine("Pay first, then go");
            }
        }

        class UnlockedTurnstile : ITurnstile
        {
            private TurnStileManager manager;

            public UnlockedTurnstile(TurnStileManager manager)
            {
                this.manager = manager;
            }

            public void Pay()
            {
                Console.WriteLine("You have already paid");
            }

            public void Push()
            {
                Console.WriteLine("You have succefully went through");
                manager.SetTurnstileState(new LockedTurnstile(manager));
            }
        }
    }
}
