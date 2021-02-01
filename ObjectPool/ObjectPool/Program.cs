using System;
using System.Collections.Generic;

namespace ObjectPool
{
    class Program
    {
        static void Main(string[] args)
        {
            var pc = PCPool.GetPC();
            Console.WriteLine("PC: age = {0}, Id = {1}", pc.Age, pc.Id);
            PCPool.ReturnPC(pc);
        }

        static class PCPool
        {
            private static List<PC> avialavlePCs = new List<PC>();
            private static List<PC> usedPCs = new List<PC>();

            public static PC GetPC()
            {
                PC pc;
                if(avialavlePCs.Count > 0)
                {
                    pc = avialavlePCs[0];
                    avialavlePCs.Remove(pc);
                }
                else
                    pc = new PC();
                
                usedPCs.Add(pc);
                return pc;
            }

            public static void ReturnPC(PC pc)
            {
                usedPCs.Remove(pc);
                avialavlePCs.Add(pc);
            }
        }

        class PC
        {
            public string Id { get; set; }
            public int Age { get; set; }

            public PC()
            {
                Age = 0;
                Id = "Get id from external source";
            }

            public PC(string id, int age)
            {
                Id = id;
                Age = age;
            }
        }

    }
}
