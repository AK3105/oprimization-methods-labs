using System;
using System.Collections.Generic;
using System.Text;


	class Edge<T> 
	{
		public Vertex<T> BeginVertex { get; set; }
		public Vertex<T> EndVertex { get; set; }
		public double flow=0;
		public double value;
		public T name;
		public Edge(T name,double value,Vertex<T> begin,Vertex<T> end)
		{
			BeginVertex = begin;
			EndVertex = end;
			this.value = value;
			this.name = name;
		}
		public void Separate()
        {
			Console.WriteLine("удаляем");

			BeginVertex.edges.Remove(this);
			EndVertex.edges.Remove(this);
			//foreach (var e in BeginVertex.edges)
			//	if (this == e)
			//		BeginVertex.edges.Remove(e);
			//foreach (var e in EndVertex.edges)
			//	if (this == e)
			//		BeginVertex.edges.Remove(e);
			//BeginVertex = null;
			//EndVertex = null;
		}
		public Edge<T> Copy()
		{
			Edge<T> e = new Edge<T>(this.name, value, this.BeginVertex.Copy(), this.EndVertex.Copy());
			return e;
		}
		public override string ToString()
		{
			return "flow="+flow+" Value="+value+" BeginVertex="+BeginVertex.name+" EndVertex="+EndVertex.name;
		}
	}

