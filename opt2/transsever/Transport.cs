using System;
using System.Collections.Generic;
using System.Text;

namespace Opt
{
    class Transport
    {
        int n, m;//размер
        double[] a;/// <summary>
                   /// массив ресурсов производителей
                   /// </summary>
        double[] b;/// <summary>
                   /// массив потребностей потребителей
                   /// </summary>
        double[,] c;/// <summary>
                    /// стоимость перевозок
                    /// </summary>
        double[,] x;/// <summary>
                    /// объем перевозок
                    /// </summary>
        double[] u;/// <summary>
                   /// потенциалы
                   /// </summary>
        double[] v;
        double[,] d;/// <summary>
                    /// матрица невязок
                    /// </summary>
        double krn;/// <summary>
                   /// критерий уменьш
                   /// </summary>
        bool optimum;/// <summary>
                     /// оптимальность
                     /// </summary>
        int imin, jmin;

        public Transport(int n, int m)
        {
            this.n = n;
            this.m = m;
            a = new double[n];
            b = new double[m];
            c = new double[n, m];
            x = new double[n, m];
            u = new double[n];
            v = new double[m];
            d = new double[n, m];
            optimum = false;
            krn = Double.MaxValue;
        }
        public void Init()
        {
            if (a.Length != n || b.Length != m||c.GetLength(0)!=n||c.GetLength(1)!=m)
                Console.WriteLine("ERROR");
            double suma = 0;
            double sumb = 0;
            for (int i = 0; i < n; i++)
                suma += a[i];
            for (int i = 0; i < m; i++)
                sumb += b[i];
            if (suma > sumb)
            {
                m++;
                b[m] = suma - sumb;

            }
            if (suma < sumb)
            {
                n++;
                a[n] = sumb-suma;

            }

           
          
        }
        private void Nevyaz()
        {
            this.Pot();
            for (int i = 0; i < n; i++)
            {

                for (int j = 0; j < m; j++)
                {
                    d[i, j] = c[i, j] - u[i] - v[j];
                    Console.WriteLine(d[i, j]);
                }
            }
        }
        private void Pot()
        {
            for (int i = 0; i < n; i++)
            {

                for (int j = 0; j < m; j++)
                {
                    u[i] = double.NaN;
                    v[j] = double.NaN;

                }
            }
            int sum = 0;

            for (int i = 0; i < n; i++)
            {

                for (int j = 0; j < m; j++)
                {

                    if (x[i, j] > 0)
                    {
                        sum++;
                    }

                }
            }

            if (sum != n + m - 1)
                Console.WriteLine("error");

            u[0] = 0;

            for (int k = 0; k <n+m-1; k++)
            {
                
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        if (x[i, j] > 0)
                        {
                            if (v[j] == double.NaN & u[i] != double.NaN)
                                v[j] = c[i, j] - u[i];
                            else if (u[i] == double.NaN & v[j] != double.NaN)
                                u[i] = c[i, j] - v[j];
                        }
                    }
                }
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Console.WriteLine(u[i].ToString(), v[j].ToString());
                }
            }
        }
        public void SevZapMethod( double[] a, double[] b, double[,] c)
        {
            
                double aa = 0;
                double bb = 0;
           
                    for (int i = 0; i < n; i++)
                {
                    aa = a[i];
                    for (int j = 0; j < m; j++)
                    {
                        bb = b[j];
                        if (aa > bb)
                        {
                            x[i, j] = bb;

                        }else 
                        x[i, j] = aa;

                        aa -= x[i, j];
                        bb -= x[i, j];


                     

                }
                }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Console.WriteLine(x[i, j]);
                }
            }
                    this.Nevyaz();
        }
    }
}

