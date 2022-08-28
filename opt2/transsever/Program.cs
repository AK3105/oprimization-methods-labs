using System;

namespace Opt
{

    class Program
    {
        static void Main(string[] args)
        {
            Transport tr = new Transport(4, 4);
            double[] a = { 90, 80, 100, 30 };
            double[] b = { 70, 60, 110, 60 };
            double[,] c = { { 5, 7, 1, 2 }, { 3, 4, 2, 5 }, { 5, 2, 3, 1 }, { 2, 1, 3, 4 } };
            tr.SevZapMethod(a, b, c);
            double[,] mtr = new double[7, 7]
            {
                {Double.PositiveInfinity,12,22,28,32,40,46 },
                {12,Double.PositiveInfinity,10,40,20,28,34 },
                {22,10,Double.PositiveInfinity,50,10,18,24 },
                {28,27,17,Double.PositiveInfinity,27,35,41 },
                {32,20,10,60,Double.PositiveInfinity,8,14 },
                {46,34,24,74,14,Double.PositiveInfinity,6 },
                {52,40,30,80,20,6,Double.PositiveInfinity },
            };
            Matrix matrix = new Matrix(7, 7, mtr);
            var rest = LittleAlgorithm.LittlesAlgorithm(matrix);
            foreach (var s in rest)
                     {
            	Console.WriteLine(s);
                     }
           
            Console.WriteLine("распределение ресурсов");
            double[] j = { 10, 14, 16, 25 };
            Resurs res = new Resurs(4, 100, 5);
            int[,] test = new int[5, 4] { {15,14,17,13},{28,30,33,35},{60,55,58,57},{75,73,73,76},{90,85,92,86} };
            res.init(test);
            res.RaspredResources();
            
            Console.WriteLine();
            ///потоки
            Graph<double> graph = new Graph<double>();

            //Vertex<double> ver0 = new Vertex<double>(0, Color.беcцветный);
            //Vertex<double> ver1 = new Vertex<double>(1, Color.беcцветный);
            //Vertex<double> ver2 = new Vertex<double>(2, Color.беcцветный);
            //Vertex<double> ver3 = new Vertex<double>(3, Color.беcцветный);
            //Vertex<double> ver4 = new Vertex<double>(4, Color.беcцветный);
            //Vertex<double> ver5 = new Vertex<double>(5, Color.беcцветный);
            //Vertex<double> ver6 = new Vertex<double>(6, Color.беcцветный);
            //Vertex<double> ver7 = new Vertex<double>(7, Color.беcцветный);


            //Edge<double> e01 = new Edge<double>(01, 39, ver0, ver1);
            //Edge<double> e02 = new Edge<double>(02, 10, ver0, ver2);
            //Edge<double> e03 = new Edge<double>(03, 23, ver0, ver3);
            //Edge<double> e14 = new Edge<double>(14, 25, ver1, ver4);
            //Edge<double> e21 = new Edge<double>(21, 81, ver2, ver1);
            //Edge<double> e25 = new Edge<double>(25, 61, ver2, ver5);
            //Edge<double> e26 = new Edge<double>(26, 15, ver2, ver6);
            //Edge<double> e32 = new Edge<double>(32, 20, ver3, ver2);
            //Edge<double> e42 = new Edge<double>(42, 18, ver4, ver2);
            //Edge<double> e46 = new Edge<double>(46, 33, ver4, ver6);
            //Edge<double> e54 = new Edge<double>(54, 16, ver5, ver4);
            //Edge<double> e57 = new Edge<double>(57, 53, ver5, ver7);
            //Edge<double> e65 = new Edge<double>(65, 71, ver6, ver5);
            //Edge<double> e67 = new Edge<double>(67, 95, ver6, ver7);

            //graph.AddVertex(ver0);
            //graph.AddVertex(ver1);
            //graph.AddVertex(ver2);
            //graph.AddVertex(ver3);
            //graph.AddVertex(ver4);
            //graph.AddVertex(ver5);
            //graph.AddVertex(ver6);
            //graph.AddVertex(ver7);

            //graph.AddEdge(ver0, ver1, e01);
            //graph.AddEdge(ver0, ver2, e02);
            //graph.AddEdge(ver0, ver3, e03);
            //graph.AddEdge(ver1, ver4, e14);
            //graph.AddEdge(ver2, ver1, e21);
            //graph.AddEdge(ver2, ver5, e25);
            //graph.AddEdge(ver2, ver6, e26);
            //graph.AddEdge(ver3, ver2, e32);
            //graph.AddEdge(ver4, ver2, e42);
            //graph.AddEdge(ver4, ver6, e46);
            //graph.AddEdge(ver5, ver4, e54);
            //graph.AddEdge(ver5, ver7, e57);
            //graph.AddEdge(ver6, ver5, e65);
            //graph.AddEdge(ver6, ver7, e67);

            //var res = TransportFlows<double>.GetAFlow(graph, ver0, ver7);
            //Console.WriteLine(res);

            
            //double[,] m = new double[4, 4]
            //{
            //    { 3, 9,2,1 },
            //    { 7, 8, 5,6},
            //    { 4, 7,3, 5 },
            //    {5,6,1,7 }
            //};

            //Matrix mtr = new Matrix(4, 4, m);
            //Tuples coord = GameTheory.ClearStrategy(mtr);
            //Console.WriteLine(coord.A + " " + coord.B);

            double[,] m = new double[2, 4]
            {
                {4,2,3,-1 },
                {-4,0,-2,2 }
            };
            Matrix mtr = new Matrix(2, 4, m);
            MixedStrategy mixed = GameTheory.MixedStrategy2xN(mtr);
            Console.WriteLine(mixed);

        }
    }
}
