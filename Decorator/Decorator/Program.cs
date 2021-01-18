using System;

namespace Decorator
{
    class Program
    {
        static void Main(string[] args)
        {
            var sword = new Sword();
            Console.WriteLine(sword.Hit());

            WeaponDecorator polishedSword = new SwordPolishingDecorator(sword);
            Console.WriteLine(polishedSword.Hit());

            WeaponDecorator sharpenedSword2 = new SwordSharpeningDecorator(polishedSword);
            Console.WriteLine(sharpenedSword2.Hit());

            WeaponDecorator sharpenedSword1 = new SwordSharpeningDecorator(new Sword());
            Console.WriteLine(sharpenedSword1.Hit());

        }

        interface IWeapon
        {
            public double Hit();
        }

        class Sword : IWeapon
        {
            public virtual double Hit()
            {
                return 10;
            }
        }

        abstract class WeaponDecorator : IWeapon
        {
            protected IWeapon weapon;

            public WeaponDecorator(IWeapon weapon)
            {
                this.weapon = weapon;
            }

            public virtual double Hit()
            {
                return weapon.Hit();
            }
        }

        class SwordPolishingDecorator : WeaponDecorator
        {
            public SwordPolishingDecorator(IWeapon sword) : base(sword)
            {
            }

            public override double Hit()
            {
                return weapon.Hit() * 2;
            }
        }

        class SwordSharpeningDecorator : WeaponDecorator
        {
            public SwordSharpeningDecorator(IWeapon sword) : base(sword)
            {
            }

            public override double Hit()
            {
                return weapon.Hit() * 3;
            }
        }
    }
}
