using System;
using System.Collections;
using System.Collections.Generic;

namespace Iterator
{
    class Program
    {
        static void Main(string[] args)
        {
            var goverment = new OfficialBranch();

            var dm = new OfficialBranch();
            goverment.AddOfficial(dm);

            dm.AddOfficial(new DeputyMinister());
            dm.AddOfficial(new DefaultOfficial());
            dm.AddOfficial(new DefaultOfficial());
            dm.AddOfficial(new DefaultOfficial());
            dm.AddOfficial(new DefaultOfficial());

            goverment.AddOfficial(new Minister());
            goverment.AddOfficial(new DeputyMinister());

            Console.WriteLine(goverment.Steal());
            dm.GetOfficialChildren();

            goverment.UseIterator(IteratorType.InWide);
            foreach (var i in goverment)
                Console.WriteLine(i);
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

        class OfficialBranch : IOfficial, IEnumerable
        {
            private List<IOfficial> officials;
            private IEnumerator enumerator;

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
                foreach (var i in officials)
                    allStolen += i.Steal();
                return allStolen;
            }

            public void UseIterator(IteratorType type)
            {
                switch (type)
                {
                    case IteratorType.InDepth:
                        enumerator = new InDepthIterator(this);
                        break;
                    case IteratorType.InWide:
                        enumerator = new InWideIterator(this);
                        break;
                    default:
                        throw new ArgumentException();
                }
            }

            public IEnumerator GetEnumerator()
            {
                return enumerator == null ? new InDepthIterator(this) : enumerator;
            }
        }

        enum IteratorType
        {
            InDepth,
            InWide
        }

        class InWideIterator : IEnumerator
        {
            List<Tuple<int, IOfficial>> officials = new List<Tuple<int, IOfficial>>();
            int position = -1;

            public InWideIterator(IOfficial official)
            {
                officials.Add(new Tuple<int, IOfficial>(1, official));
                ConvertTreeToList(official);
            }

            private void ConvertTreeToList(IOfficial official)
            {
                if (official is OfficialBranch)
                {
                    var tmpOfficial = official as OfficialBranch;
                    var children = tmpOfficial.GetOfficialChildren();
                    foreach (var i in children)
                        officials.Add(new Tuple<int, IOfficial>(0, i));
                }
                else
                    officials.Add(new Tuple<int, IOfficial>(0, official));

                for (int i = 0; i < officials.Count; i++)
                {
                    if (officials[i].Item1 == 0 && officials[i].Item2 is OfficialBranch)
                    {
                        officials[i] = new Tuple<int, IOfficial>(1, officials[i].Item2);
                        ConvertTreeToList(officials[i].Item2);
                    }
                }
            }

            public object Current
            {
                get
                {
                    if (position == -1 || position >= officials.Count)
                        throw new InvalidOperationException();
                    else
                        return officials[position];
                }
            }

            public bool MoveNext()
            {
                if (position < officials.Count - 1)
                {
                    position++;
                    return true;
                }
                else
                    return false;
            }

            public void Reset()
            {
                position = -1;
            }
        }

        class InDepthIterator : IEnumerator
        {
            List<Tuple<int, IOfficial>> officials = new List<Tuple<int, IOfficial>>();
            int position = -1;

            public InDepthIterator(IOfficial official)
            {
                officials.Add(new Tuple<int, IOfficial>(1, official));
                ConvertTreeToList(official);
            }

            private void ConvertTreeToList(IOfficial official)
            {
                if (official is OfficialBranch)
                {
                    var tmpOfficial = official as OfficialBranch;
                    var children = tmpOfficial.GetOfficialChildren();
                    foreach (var i in children)
                    {
                        officials.Add(new Tuple<int, IOfficial>(1, i));
                        if (i is OfficialBranch)
                            ConvertTreeToList(i);
                    }
                }
                else
                    officials.Add(new Tuple<int, IOfficial>(0, official));
            }

            public object Current
            {
                get
                {
                    if (position == -1 || position >= officials.Count)
                        throw new InvalidOperationException();
                    else
                        return officials[position];
                }
            }

            public bool MoveNext()
            {
                if (position < officials.Count - 1)
                {
                    position++;
                    return true;
                }
                else
                    return false;
            }

            public void Reset()
            {
                position = -1;
            }
        }
    }
}
