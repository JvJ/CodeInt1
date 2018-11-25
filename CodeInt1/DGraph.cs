using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeInt1
{
    // A class representing a directed graph
    // node
    public class DGraph<T>
    {
        private List<DGraph<T>> m_links;

        public T Data { get; private set; }

        public Guid ID { get; private set; }
        
        public IEnumerable<DGraph<T>> Links
        {
            get
            {
                return m_links.AsEnumerable();
            }
        }

        public DGraph(T data)
        {
            Data = data;
            m_links = new List<DGraph<T>>();
            ID = Guid.NewGuid();
        }

        public void Attach(DGraph<T> other)
        {
            // I know this is an O(n) operation,
            // but whatever
            if (!m_links.Contains(other))
            {
                m_links.Add(other);
            }
        }

        // Problem 4.2 - Is there a route between any 2 given nodes
        public bool RouteTo(DGraph<T> other)
        {
            // To avoid collisions
            var visited = new HashSet<DGraph<T>>();

            return RouteTo(other, visited);
        }

        private bool RouteTo(DGraph<T> other, HashSet<DGraph<T>> visited)
        {
            
            visited.Add(this);

            foreach (var g in m_links)
            {
                if (g == other)
                {
                    return true;
                }
                else if (!visited.Contains(g) &&
                    g.RouteTo(other, visited))
                {
                    return true;
                }
            }

            return false;
        }


        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

    }
}
