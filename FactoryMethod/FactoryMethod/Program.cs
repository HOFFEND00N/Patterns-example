using System;

namespace FactoryMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            RewardFactory factory = new RewardFactory();

            Console.WriteLine("Type Reward type: Money = 1 / Item = 2 ");
            var rewardType = Console.ReadLine();

            IReward desiredReward = null;
            if (rewardType == "1")
            {
                desiredReward = new MoneyReward();
            }
            else if(rewardType == "2")
            {
                desiredReward = new ItemReward();
            }
            Console.WriteLine(desiredReward.GetPrice());

            var enumType = (RewardType)42;
            factory.GetReward(enumType);

            var money = factory.GetReward(RewardType.Money);
            Console.WriteLine(money.GetPrice());

            var item = factory.GetReward(RewardType.Item);
            Console.WriteLine(item.GetPrice());
        }

        class RewardFactory
        {
            public IReward GetReward(RewardType type)
            {
                switch (type)
                {
                    case RewardType.Money:
                        return new ItemReward();
                    case RewardType.Item:
                        return new MoneyReward();
                    default:
                        throw new Exception("There is no such reward");
                }
            }
        }

        interface IReward
        {
            public double GetPrice();
            public string GetRewardType();
        }

        class MoneyReward : IReward
        {
            public double GetPrice()
            {
                var cost = new Random();
                return cost.NextDouble() * 100;
            }

            public string GetRewardType()
            {
                return "Money Reward!";
            }
        }

        class ItemReward : IReward
        {
            public double GetPrice()
            {
                var itemQuality = new Random();
                return (itemQuality.NextDouble() * 5) * 50;
            }

            public string GetRewardType()
            {
                return "Item Reward!";
            }
        }

        enum RewardType
        {
            Money,
            Item 
        }
    }
}
