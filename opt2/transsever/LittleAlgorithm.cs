using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Opt
{
    class LittleAlgorithm
    {
       
            public static List<(double, double)> LittlesAlgorithm(Matrix mtr)
            {
                Matrix matrix = mtr.Copy();
                List<(double, double)> path = new List<(double, double)>();
                if (CheckMatrix(matrix))
                {
                    double lowBorder = 0;
                    for (int rank = matrix.GetRank(); rank >= 2; rank--)
                    {

                        for (int i = 0; i < matrix.GetRank(); i++)
                        {

                            Vector row = matrix.GetRow(i);
                            double min = FindMin(row);

                            if (min != Double.MaxValue)
                            {
                                lowBorder += min;
                                for (int j = 0; j < matrix.GetRank(); j++)
                                {
                                    if (!matrix[i, j].Equals(Double.NaN))
                                        matrix[i, j] -= min;
                                }
                            }

                        }
                        for (int j = 0; j < matrix.GetRank(); j++)
                        {
                            Vector coloumn = matrix.GetColoumn(j);
                            double min = FindMin(coloumn);
                            if (min != Double.MaxValue)
                            {
                                lowBorder += min;
                                for (int i = 0; i < matrix.GetRank(); i++)
                                {
                                    if (!matrix[i, j].Equals(Double.NaN))
                                        matrix[i, j] -= min;
                                }
                            }
                        }

                        (int, int) tuple = GetCoordinatesOfMaxG(matrix);
                        DeleteRowAndCol(matrix, tuple);
                        path.Add(tuple);
                        matrix[tuple.Item2, tuple.Item1] = Double.PositiveInfinity;


                    }


                }
                return path;
            }
            private static void DeleteRowAndCol(Matrix mtr, (int, int) coord)
            {
                for (int j = 0; j < mtr.GetRank(); j++)
                {
                    mtr[coord.Item1, j] = Double.NaN;
                }
                for (int i = 0; i < mtr.GetRank(); i++)
                {
                    mtr[i, coord.Item2] = Double.NaN;
                }
            }
            private static bool CheckMatrix(Matrix matrix)
            {
                bool checking = false;
                for (int i = 0; i < matrix.GetRank(); i++)
                {
                    for (int j = 0; j < matrix.GetRank(); j++)
                    {
                        if (i == j)
                        {
                            if (Double.IsInfinity(matrix[i, j]))
                            {
                                checking = true;
                            }
                            else
                                checking = false;
                        }
                    }
                }

                return checking;
            }
            private static (int, int) GetCoordinatesOfMaxG(Matrix matrix)
            {
                List<(int, int)> coordinates = new List<(int, int)>();
                Dictionary<(int, int), double> g = new Dictionary<(int, int), double>();

                for (int i = 0; i < matrix.GetRank(); i++)
                {
                    for (int j = 0; j < matrix.GetRank(); j++)
                    {
                        if (matrix[i, j] == 0)
                        {
                            coordinates.Add((i, j));

                        }
                    }
                }

                foreach (var s in coordinates)
                {
                    double minInRow = double.MaxValue;
                    double minInColoumn = double.MaxValue;


                    for (int j = 0; j < matrix.GetRank(); j++)
                    {
                        if (j != s.Item2)
                        {
                            if (minInRow >= matrix[s.Item1, j] && !matrix[s.Item1, j].Equals(Double.NaN))
                            {
                                minInRow = matrix[s.Item1, j];
                            }
                        }

                    }
                    for (int i = 0; i < matrix.GetRank(); i++)
                    {
                        if (i != s.Item1)
                        {
                            if (minInColoumn >= matrix[i, s.Item2] && !matrix[i, s.Item2].Equals(Double.NaN))
                                minInColoumn = matrix[i, s.Item2];
                        }

                    }
                    if (minInColoumn == Double.MaxValue)
                    {
                        minInColoumn = 0;
                    }
                    if (minInRow == Double.MaxValue)
                    {
                        minInColoumn = 0;
                    }
                    g.Add(s, minInRow + minInColoumn);

                }

                double max = g.Values.Max();
                var tuple = g.First(tuple => tuple.Value == max).Key;
                return tuple;
            }
            private static double FindMin(Vector vec)
            {

                double min = Double.MaxValue;
                for (int i = 0; i < vec.GetSize(); i++)
                {
                    if (vec[i] <= min && !vec[i].Equals(Double.NaN))
                    {
                        min = vec[i];
                    }
                }

                return min;
            }
        }

    
}

