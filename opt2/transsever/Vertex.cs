using System;
using System.Collections.Generic;
using System.Text;

	enum Color
	{
		красный,синий,беcцветный
	}
	class Vertex<T>
	{
		public T name;
		public   List<Edge<T>> edges;
		public  Color color;
		public double marker;

		public Vertex(T name,Color color)
		{
			this.name = name;
			this.color = color;
			edges = new List<Edge<T>>();
		}

		public void AddEdge(Edge<T> edge)
		{
			edges.Add(edge);
		}
		public Vertex<T> Copy()
		{
			Vertex<T> v = new Vertex<T>(name,color);
			foreach (var e in edges)
				v.edges.Add(e.Copy());
			return v;
		}
		public Edge<T> GetEdge(Vertex<T> end)
			{
				Edge<T> res = null;
				foreach (var e in edges)
				{
					if (e.EndVertex == this)
					{
						if (e.BeginVertex == end)
							res = e;
					}
					else if(e.BeginVertex==this)
					{
						if (e.EndVertex == end)
							res = e;
					}
				}
					
				
				return res;
			}
			public override string ToString()
			{
				string s = "Name= " + name+" Edges: ";
				if (edges.Count != 0)
				{
					foreach (var ed in edges)
						s += ed.name+" ";
				}
				return s;
			}
	}

