using System;
using System.Collections;
using System.Collections.Generic;

namespace Iterator
{
    class Program
    {
        // move logic to MoveNext()
        // Current() in Iterator returns Tuple, instead of expected object(Official)
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
            IOfficial currentOfficial;
            Queue<IOfficial> officials = new Queue<IOfficial>();

            public InWideIterator(IOfficial official)
            {
                officials.Enqueue(official);
            }

            public object Current
            {
                get
                {
                    if (currentOfficial == null)
                        throw new InvalidOperationException();
                    else
                        return currentOfficial;
                }
            }

            public bool MoveNext()
            {
                if (officials.Count != 0)
                    currentOfficial = officials.Dequeue();
                else
                    currentOfficial = null;
                if (currentOfficial is OfficialBranch)
                    foreach (var i in (currentOfficial as OfficialBranch).GetOfficialChildren())
                    {
                        officials.Enqueue(i);
                    }
                return currentOfficial == null ? false : true;
            }

            public void Reset()
            {
                officials = new Queue<IOfficial>();
            }
        }

        class InDepthIterator : IEnumerator
        {
            IOfficial currentOfficial;
            Stack<IOfficial> officials = new Stack<IOfficial>();
            HashSet<IOfficial> visited = new HashSet<IOfficial>();

            public InDepthIterator(IOfficial official)
            {
                officials.Push(official);
            }

            public object Current
            {
                get
                {
                    if (currentOfficial == null)
                        throw new InvalidOperationException();
                    else
                        return currentOfficial;
                }
            }

            public bool MoveNext()
            {
                IOfficial node;
                if (officials.Count != 0)
                    node = officials.Pop();
                else
                    node = null;
                currentOfficial = node;
                if (!visited.Contains(node) && node != null)
                {
                    visited.Add(node);
                }
                if (node is OfficialBranch)
                {
                    var children = (node as OfficialBranch).GetOfficialChildren();
                    for (int i = children.Count - 1; i >= 0; i--)
                        officials.Push(children[i]);
                }
                return currentOfficial == null ? false : true;
            }

            public void Reset()
            {
                officials = new Stack<IOfficial>();
                visited = new HashSet<IOfficial>();
            }
        }
    }
}
