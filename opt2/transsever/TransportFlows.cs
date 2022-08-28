using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Opt
{

    
    class TransportFlows<T>
    {
        
        public static List<Edge<T>> GetAFlow(Graph<T> graph,Vertex<T> s,Vertex<T> t, out double flowRes, out double outgoingFlow, out double incomingFlow)//главный метод, возвращающий заполненые ребра и по ссылке передает аргументы общего, исходящего и входящего потока
        {
            flowRes = 0;
            outgoingFlow = 0;
            incomingFlow = 0;

            double eps = double.Epsilon;
            List<Edge<T>> fullEdges = new List<Edge<T>>();
            List<Vertex<T>> path = new List<Vertex<T>>();
            do
            {
      
                List<Edge<T>> edges = new List<Edge<T>>();
                 path = graph.WideSearch( s, t.name);
               
                foreach (var v in path)
                    Console.WriteLine(v);
                for (int i = 0; i < path.Count - 1; i++)
                {
                    edges.Add(path[i].GetEdge(path[i + 1]));
                }
                foreach (var e in edges)
                    Console.WriteLine(e);
                double minValue = edges.Min(a => (a.value - a.flow));

                foreach (var e in edges)
                    e.flow += minValue;

                var fullEdge = from e in edges
                               where Math.Abs(e.value - e.flow) < eps
                               select e;
               
                
                fullEdges.AddRange(fullEdge);
                
                
            } while (path.Count != 0);
            foreach (var e in fullEdges)
            {
                    flowRes += e.value;
                    
            }
            foreach (var e in s.edges)
            {
                outgoingFlow+= e.flow;

            }
            foreach (var e in t.edges)
            {
                incomingFlow += e.flow;

            }

            return fullEdges;
        }
    }
}
