using System;
using System.Collections.Generic;
using System.Text;


	class Graph<T>
	{
        public List<Vertex<T>> Vertices { get ; set; }

 
        public Graph()
        {
            Vertices = new List<Vertex<T>>();
        }
        public Graph<T> Copy()
        {
            Graph<T> graph = new Graph<T>();
            foreach (var v in Vertices)
                graph.Vertices.Add(v.Copy());
            return graph;
        }
        
        public void AddVertex(T name,double marker, Color color)
        {
            Vertices.Add(new Vertex<T>(name,color));
        }
        public void AddVertex(Vertex<T> t)
        {
            Vertices.Add(t);
        }

        public Vertex<T> FindVertex(T name)
        {
            foreach (var v in Vertices)
            {
                if (v.name.Equals(name))
                {
                    return v;
                }
            }

            return null;
        }

       
        public void AddEdge(Vertex<T> b, Vertex<T> e, Edge<T> edge)
        {
            
            if (b != null && e != null)
            {
                b.AddEdge(edge);
                e.AddEdge(edge);
                edge.EndVertex = e;
                edge.BeginVertex = b;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startVertex"> вершина от которой ищем</param>
        /// <param name="name">имя котрое ищемем </param>
        /// <returns></returns>
        // метод-обертка для поиска в ширину
        public List<Vertex<T>> WideSearch(Vertex<T> startVertex,T name)
        {
            List<Vertex<T>> path = new List<Vertex<T>>();
            List<Vertex<T>> free = new List<Vertex<T>>();
            int index= -1;
            Vertex<T> result = Rec(startVertex, name, free, index,path);
            return path;
        }
        //рекурсивный метод алгоритма поиска в ширину
        private Vertex<T> Rec(Vertex<T> current, T name, List<Vertex<T>> free, int index, List<Vertex<T>> path )
        {
            
            if (current.name.Equals(name))
                return current;
            else
            {
                if (current.edges.Count != 0)
                {
                    foreach (var edge in current.edges)
                    {
                        if (edge.EndVertex != current)
                        {
                            free.Add(edge.EndVertex);
                            path.Add(edge.EndVertex);
                        }
                           
                    }

                }
               
            }
            index++;
            return Rec(free[index], name, free, index,path);

        }


        public List<Vertex<T>> Deikstra( Vertex<T> begin, Vertex<T> end)
        {
            //подготавка графа к алгоритму:
           
           
            // устанавливаем маркеры бесконечности(100000000)
            foreach (var v in Vertices)
                v.marker = Int64.MaxValue;
            //у начальной вершины маркер=0
            begin.marker = 0;
            // очередь куда будут записываться еще снепосещенные вершины
            Queue<Vertex<T>> free = new Queue<Vertex<T>>();
            //добавляем все вершины в непосещенные
            foreach (var v in Vertices)
                free.Enqueue(v);
            //список, где будут вершины, через которые идет мин путь
            List<Vertex<T>> way = new List<Vertex<T>>();
            way.Add(end);
            // список, где бдет пара: вершина, у которой изменили маркер(child) и вершина которая изменила маркер(parent)
            List<RelativeAndChild<T>> couples = new List<RelativeAndChild<T>>();
            //получаем список пар
            List<RelativeAndChild<T>> res = RecDeikstra(begin,end,free,way,couples);
            //возвращаем  список вершнин пути
            return RestoreTheWay(res,end,end,way);
        }
     
        //метод восстанавливающий путь до конечной точки
        //начиная с конечной точки поиска мы ищем вершину, которая ее изменила, затем заносим в ее список и повторяем алгоритм уже для найденной вершины
        private List<Vertex<T>> RestoreTheWay(List<RelativeAndChild<T>> couples,Vertex<T> finishVer, Vertex<T> current,List<Vertex<T>> way)
        {
            //итерируем с конца, так как в конце списка актуальная информация( у одной и той же вершины может изменяться маркер несколько раз)
            for(int i=couples.Count-1;i>=0;i--)
            {
                if (couples[i].child == current)
                {
                    way.Add(couples[i].relative);
                    return RestoreTheWay(couples,finishVer, couples[i].relative, way);
                }
            }
           
            return way;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="current"> текущая вершина </param>
        /// <param name="end"> искомая вершина </param>
        /// <param name="free"> список вершин мин. пути до искомой вершины </param>
        /// <returns></returns>
        private List<RelativeAndChild<T>> RecDeikstra(Vertex<T> current,Vertex<T> end,Queue<Vertex<T>> free,List<Vertex<T>> way, List<RelativeAndChild<T>> couples)
        {
            //Console.WriteLine(current.marker);
            //Console.WriteLine(current+"cur");
            //проверка на конечную вершину
            if (current != end)
            {
                //сортировка списка граней текущей вершины по возрастанию весов
                Edge<T> temp;
                for (int i = 0; i < current.edges.Count - 1; i++)
                {
                    for (int j = i + 1; j < current.edges.Count; j++)
                    {
                        if (current.edges[i].value > current.edges[j].value)
                        {
                            temp = current.edges[i];
                            current.edges[i] = current.edges[j];
                            current.edges[j] = temp;
                        }
                    }
                }
                //присваеваем соседним вершины маркер = маркер текущей вершины + вес ребра
                foreach (var edge in current.edges)
                {
                    
                    if(edge.EndVertex == current)
                    {
                        //проверка на то что начальная вершина ребра не посещена(красный = посещенный)
                        if (edge.BeginVertex.color != Color.красный)
                        {
                            free.Enqueue(edge.BeginVertex);
                            if(edge.BeginVertex.marker> current.marker + edge.value)
                            {
                                edge.BeginVertex.marker = current.marker + edge.value;
                                RelativeAndChild<T> couple = new RelativeAndChild<T>(current, edge.BeginVertex);
                                couples.Add(couple);
                            }
                            
                        }
                                
                             
                        
                    }
                     

                    if (edge.BeginVertex == current)
                    {
                        //проверка на то что конечная вершина ребра не посещена(красный = посещенный)
                        if (edge.EndVertex.color != Color.красный)
                        {
                            free.Enqueue(edge.EndVertex);
                            if (edge.EndVertex.marker > current.marker + edge.value)
                            {
                                edge.EndVertex.marker = current.marker + edge.value;

                                RelativeAndChild<T> couple = new RelativeAndChild<T>(current, edge.EndVertex);
                                couples.Add(couple);
                            }
                           
                                

                        }
                            
                    }
                        
                }
                //отмечаем текущую вершину как посещенную
                current.color = Color.красный;
                
               
                return RecDeikstra(free.Dequeue(), end, free,way,couples);
                
  
            }
            else
            {
                return couples;
            }
        }
    }

