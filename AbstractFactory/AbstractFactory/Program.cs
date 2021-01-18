using System;

namespace AbstractFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            var rand = new Random().Next(0, 1);
            DungeonAbstractFactory factory = null;
            if (rand > 0.5)
                factory = new ForestDungeonFactory();
            else
                factory = new OceanDungeonFactory();
            var boss = factory.MakeBoss();
            var reward = factory.MakeReward();
            Console.WriteLine(boss.GetBossLevel());
            Console.WriteLine(boss.GetBossLevel());

            Console.WriteLine(boss.GetBossLevel());
            Console.WriteLine(boss.Hit());

            Console.WriteLine(reward.GeItemPrice());
            Console.WriteLine(reward.GetRewardType());
        }

        interface DungeonAbstractFactory
        {
            public ILoot MakeReward();
            public IBoss MakeBoss();
        }

        class ForestDungeonFactory : DungeonAbstractFactory
        {
            public ILoot MakeReward()
            {
                return new Forestcrystal();
            }

            public IBoss MakeBoss()
            {
                return new ForestBoss();
            }
        }


        class OceanDungeonFactory : DungeonAbstractFactory
        {
            public ILoot MakeReward()
            {
                return new OceanCrystal();
            }

            public IBoss MakeBoss()
            {
                return new OceanBoss();
            }
        }

        public interface ILoot
        {
            public double GeItemPrice();
            public string GetRewardType();
        }

        class OceanCrystal : ILoot
        {
            public double GeItemPrice()
            {
                var cost = new Random();
                return cost.NextDouble() * 100;
            }

            public string GetRewardType()
            {
                return "Ocean crystal reward!";
            }
        }

        class Forestcrystal : ILoot
        {
            public double GeItemPrice()
            {
                var cost = new Random();
                return cost.NextDouble() * 50;
            }

            public string GetRewardType()
            {
                return "Forest crystal reward!";
            }
        }

        enum RewardType
        {
            ForestCrystal,
            OceanCrystal
        }

        public interface IBoss
        {
            public double Hit();
            public double GetBossLevel();
        }

        class ForestBoss : IBoss
        {
            public double GetBossLevel()
            {
                var lvl = new Random();
                var forestSizeCoefficent = 1.5;
                return lvl.NextDouble() * 5 * forestSizeCoefficent;
            }

            public double Hit()
            {
                var dmg = new Random();
                var forestSizeCoefficent = 1.5;
                return dmg.NextDouble() * 100 * forestSizeCoefficent;
            }
        }

        class OceanBoss : IBoss
        {
            public double GetBossLevel()
            {
                var lvl = new Random();
                var OceanSizeCoefficent = 4;
                return lvl.NextDouble() * 5 * OceanSizeCoefficent;
            }

            public double Hit()
            {
                var dmg = new Random();
                var OceanSizeCoefficent = 4;
                return dmg.NextDouble() * 200 * OceanSizeCoefficent;
            }
        }

        enum BossdType
        {
            Forest,
            Ocean
        }
    }
}
