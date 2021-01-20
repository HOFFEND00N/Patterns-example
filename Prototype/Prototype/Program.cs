using System;
using System.Collections.Generic;

namespace Prototype
{
    class Program
    {
        static void Main(string[] args)
        {
            var officialsFactory = new OfficialsFactory();

            var RuGoverment = officialsFactory.Get("Russian goverment");
            Console.WriteLine(RuGoverment.Steal());
        }

        interface IOfficial : ICloneable
        {
            public double Steal();
        }

        class Minister : IOfficial
        {
            public object Clone()
            {
                return new Minister();
            }

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

            public object Clone()
            {
                return new DeputyMinister();
            }
        }

        class DefaultOfficial : IOfficial
        {
            public double Steal()
            {
                return 1;
            }

            public object Clone()
            {
                return new DefaultOfficial();
            }
        }

        class OfficialBranch : IOfficial
        {
            private List<IOfficial> officials;

            public OfficialBranch()
            {
                officials = new List<IOfficial>();
            }

            public OfficialBranch(List<IOfficial> officials)
            {
                this.officials = officials;
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
                foreach (var i in officials)
                    allStolen += i.Steal();
                return allStolen;
            }

            public object Clone()
            {
                var clonedOfficials = new List<IOfficial>();
                foreach (var i in officials)
                {
                    clonedOfficials.Add((IOfficial)i.Clone());
                }
                return new OfficialBranch(clonedOfficials);
            }
        }

        class OfficialsFactory
        {
            private Dictionary<string, IOfficial> prototypeStorage = new Dictionary<string, IOfficial>();
            
            public OfficialsFactory()
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

                prototypeStorage.Add("Russian goverment", goverment);
            }

            public IOfficial Get(string key)
            {
                try
                {
                    return (IOfficial)prototypeStorage[key].Clone();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}
