using System;

namespace TemplateMethod2
{
    class Program
    {
        static void Main(string[] args)
        {
            DungeonBuilder SkyDungeon = new SkyDungeon();
            SkyDungeon.BuildDungeon();

            DungeonBuilder ForestDungeon = new SkyDungeon();
            ForestDungeon.BuildDungeon();
        }

        abstract class DungeonBuilder
        {
            public void BuildDungeon()
            {
                OpenDungeon();
                MakeMonsters();
                MakeReward();
                SetLifeTime();
            }

            private void SetLifeTime()
            {
                Random random = new Random();
                Console.WriteLine("Dungeon life time = {0}", random.Next(1,10));
            }

            private void OpenDungeon()
            {
                Console.WriteLine("Dungeon opened");
            }

            protected abstract void MakeReward();

            protected abstract void MakeMonsters();
        }

        class ForestDungeon : DungeonBuilder
        {
            protected override void MakeMonsters()
            {
                Console.WriteLine("Forest dungeon monsters made");
            }

            protected override void MakeReward()
            {
                Console.WriteLine("Forest dungeon reward made");
            }
        }

        class SkyDungeon : DungeonBuilder
        {
            protected override void MakeMonsters()
            {
                Console.WriteLine("Sky dungeon monsters made");
            }

            protected override void MakeReward()
            {
                Console.WriteLine("Sky dungeon reward made");
            }
        }
    }
}
