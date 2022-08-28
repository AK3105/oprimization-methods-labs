using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Opt
{
    class Tuples
    {
        public double Strategy1Player { get; set; }
        public double Strategy2Player { get; set; }
        public (double,double) ValueOfGame { get; set; }
        public Tuples(double x,double y)
        {
            Strategy1Player = x;
            Strategy2Player = y;
        }
        public Tuples((double, double) valueOfGame)
        {
            ValueOfGame = valueOfGame;
        }
    }
    class MixedStrategy
    {
        public double P1 { get; set; }
        public double P2 { get; set; }
        public List<double> Q { get; set; }

        public double V { get; set; }
        public MixedStrategy(double p1, double p2, List<double> q,double v)
        {
            P1 = p1;
            P2 = p2;
            Q = q;
            V = v;
        }
        public override string ToString()
        {
            string s ="v="+V+" " +
                "p1= " + P1 + " p2= " + P2+" q: ";
            foreach (var q in Q)
                s += q + ", ";
            return s;
        }
       
    }

    class GameTheory
    {
        public static Tuples ClearStrategy(Matrix mtr)// основной метод, возвращающий стратегии 1 и 2 игрока
        {
            Tuples res = null;
            List<double> alpha = new List<double>();
           
            for (int row = 0; row < mtr.Rows; row++)
            {
                double min = mtr[row, 0];
                for (int col = 0; col < mtr.Cols; col++)
                {
                    if (min > mtr[row, col])
                    {
                        min = mtr[row, col];
                    }
                }
                alpha.Add(min);
            }
            List<double> beta = new List<double>();
            for (int col = 0; col < mtr.Cols; col++)
            {
                double max = mtr[0, col];
               
                for (int row = 0; row < mtr.Rows; row++)
                {
                    if (max < mtr[row, col])
                    {
                        max = mtr[row, col];
                    }
                   
                }
                beta.Add(max);
            }

            double alphaMax = alpha.Max(i => i);
            double betaMin = beta.Min(i => i);

            if (alphaMax == betaMin)
            {
                for (int row = 0; row < mtr.Rows; row++)
                {
                    for (int col = 0; col < mtr.Cols; col++)
                    {
                        if (betaMin == mtr[row, col])
                            res = new Tuples(row, col);
                    }
                }
            }
            else
            {
                res = new Tuples((alphaMax, betaMin));
            }
            return res;

        } 

        public static MixedStrategy MixedStrategy2xN(Matrix mtr)//основной метод для вычисления вероятностей смешанной стратегии
        {
            MixedStrategy res = null;
            List<Vector> listOfVec = new List<Vector>();
            List<List<(int,double)>> v = new List<List<(int,double)>>();
            for (int col=0; col < mtr.Cols ; col++)
            {
                Vector vector = mtr.GetColoumn(col);
                vector[1] = vector[1] - vector[0];
                listOfVec.Add(vector);
            }

            foreach (var c in listOfVec)
                Console.WriteLine(c);

            for (int i = 0; i < mtr.Cols; i++)
            {
                List<(int, double)> pairs = new List<(int, double)>();
                for (int j=i+1; j < mtr.Cols; j++)
                {
                    Vector point = new Vector(2);
                    point[0] = listOfVec[i][0] + (-1 * listOfVec[j][0]);
                    point[1] = -1 * listOfVec[i][1] + listOfVec[j][1];
                    double x = point[0] / point[1];
                    if(!double.IsInfinity(x))
                        pairs.Add((j, x));

                }
                v.Add(pairs);
            }
            for (int i = 0; i < v.Count; i++)
            {
                for (int j = 0; j < v[i].Count; j++)
                {

                    v[i][j] = (v[i][j].Item1, v[i][j].Item2 * listOfVec[i][1] + listOfVec[i][0]);
                }
            }
            (int, int, double) maximalMinoranta = (0, v[0][0].Item1, v[0][0].Item2);
            for (int i = 0; i < v.Count - 1; i++)
            {
                for (int j = 0; j < v[i].Count; j++)
                {

                    if (maximalMinoranta.Item3 > v[i][j].Item2)
                        maximalMinoranta = (i, j+1, v[i][j].Item2);
                }
            }
            Console.WriteLine(maximalMinoranta);
            Matrix matrix = new Matrix(2, 2);
            matrix[0, 0] = mtr[0, maximalMinoranta.Item1];
            matrix[0, 1] = mtr[0, maximalMinoranta.Item2];
            matrix[1, 0] = mtr[1, maximalMinoranta.Item1];
            matrix[1, 1] = mtr[1, maximalMinoranta.Item2];
            Console.WriteLine(matrix);
            double p1 = (matrix[1, 1] - matrix[1, 0]) / (matrix[0, 0] - matrix[0, 1] - matrix[1, 0] + matrix[1, 1]);
            double p2 = (matrix[0, 0] - matrix[0, 1]) / (matrix[0, 0] - matrix[0, 1] - matrix[1, 0] + matrix[1, 1]);
            double q1 = (matrix[1, 1] - matrix[0, 1]) / (matrix[0, 0] - matrix[0, 1] - matrix[1, 0] + matrix[1, 1]);
            double q2= (matrix[0, 0] - matrix[1, 0]) / (matrix[0, 0] - matrix[0, 1] - matrix[1, 0] + matrix[1, 1]);
            List<double> Q = new List<double>();
            for(int i = 0; i < mtr.Cols; i++)
            {
                if (i == maximalMinoranta.Item1)
                    Q.Add(q1);
                else if (i == maximalMinoranta.Item2)
                    Q.Add(q2);
                else Q.Add(0);
            }
            return new MixedStrategy(p1,p2,Q,maximalMinoranta.Item3);
        }
        
    }
}
