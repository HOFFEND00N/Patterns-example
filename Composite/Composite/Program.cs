using System;
using System.Collections.Generic;

namespace Composite
{
    class Program
    {
        static void Main(string[] args)
        {
            var goverment = new OfficialBranch();
            goverment.AddOfficial(new Minister());
            goverment.AddOfficial(new DeputyMinister());

            var dm = new OfficialBranch();
            goverment.AddOfficial(dm);

            dm.AddOfficial(new DeputyMinister());
            dm.AddOfficial(new DefaultOfficial());
            dm.AddOfficial(new DefaultOfficial());
            dm.AddOfficial(new DefaultOfficial());
            dm.AddOfficial(new DefaultOfficial());

            Console.WriteLine(goverment.Steal());
            dm.GetOfficialChildren();
        }

        interface IOfficial
        {
            public double Steal();
        }

        class Minister : IOfficial
        {
            public double Steal()
            {
                return 10;
            }
        }

        class DeputyMinister : IOfficial
        {
            public double Steal()
            {
                return 5;
            }
        }

        class DefaultOfficial : IOfficial
        {
            public double Steal()
            {
                return 1;
            }
        }

        class OfficialBranch : IOfficial
        {
            private List<IOfficial> officials;

            public OfficialBranch()
            {
                officials = new List<IOfficial>();
            }

            public void AddOfficial(IOfficial official)
            {
                officials.Add(official);
            }

            public List<IOfficial> GetOfficialChildren()
            {
                return officials;
            }

            public void RemoveOfficial(IOfficial official)
            {
                officials.Remove(official);
            }

            public double Steal()
            {
                double allStolen = 0;
                foreach(var i in officials)
                    allStolen += i.Steal();
                return allStolen;
            }
        }
    }
}
