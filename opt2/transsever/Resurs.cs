using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Opt
{
    class Resurs
    {
        int n; //предприятий      
        int Sv;
        int m; //кол-во частей
        int dv; //Минимальная доля инвестиций
        public int[,] z; //Функции эффективности
        int[] v; //v
        public int[,] f; //опт знач
        int mass, mass2; //доп 
        int cur;
        int cur2 = 0;
        int curdv;
        int svcop;
        int num = 0;
        public int[,] dopm;
        public int[] endV;


        public Resurs(int n, int Sv, int m)
        {
            this.n = n;
            this.Sv = Sv;
            this.m = m;
            z = new int[m + 1, n];
            v = new int[m + 1];
            f = new int[m + 1, n];
            dopm = new int[m + 1, n];
            endV = new int[n];
        }

        public void init(int[,] z)
        {
            this.z = z;

        }


        public void RaspredResources()
        {
            dv = Sv / m;

            if ((Sv % m) > 0)
            {
                Console.WriteLine("проверка делимости без остатка не пройдена");
                Environment.Exit(1);
            }

            for (int i = 0; i < f.GetLength(0)-1; i++)
            {
                f[i, 0] = z[i, 0];
            }


            v[0] = 0;
            dopm[0, 0] = 0;
            dopm[0, 1] = 0;
            dopm[0, 2] = 0;
            for (int i = 1; i < v.Length; i++)
            {
                v[i] = v[i - 1] + dv;
                dopm[i, 0] = dopm[i - 1, 0] + dv;
            }

            mass = 0;
            mass2 = dv;
            curdv = mass2;
            for (int i = 1; i < z.GetLength(1); i++)
            {
                while ((mass + mass2) <= Sv)
                {
                    num += 1;
                    while (mass2 >= 0)
                    {
                        if (mass == 0 || mass2 == 0)
                        {

                            if (z[Array.IndexOf(v, mass), i] > f[Array.IndexOf(v, mass2), i-1])
                            {
                                cur = z[Array.IndexOf(v, mass), i];
                                if (cur > cur2)
                                {
                                    cur2 = cur;
                                    dopm[num, i] = mass;
                                }
                            }
                            else
                            {
                                cur = f[Array.IndexOf(v, mass2), i - 1];
                                if (cur > cur2)
                                {
                                    cur2 = cur;
                                    dopm[num, i] = mass;
                                }
                            }
                        }
                        else //mass mass2 не 0
                        {
                            cur = z[Array.IndexOf(v, mass), i] + f[Array.IndexOf(v, mass2), i - 1];
                            if (cur > cur2)
                            {
                                cur2 = cur;
                                dopm[num, i] = mass;
                            }
                        }
                        mass2 -= dv;
                        mass += dv;
                    }
                    f[Array.IndexOf(v, mass - dv), i] = cur2;
                    curdv += dv;
                    mass = 0;
                    mass2 = curdv;

                }
                mass = 0;
                mass2 = dv;
                cur2 = 0;
                curdv = dv;
                num = 0;
            }


            endV[n - 1] = dopm[m, n - 1];
            svcop = Sv - endV[n - 1];
            for (int i = n - 2; i >= 0; i--)
            {
                if (i == 0)
                {
                    endV[i] = svcop;
                    break;
                }

                endV[i] = dopm[Array.IndexOf(v, svcop), i];
                svcop = svcop - endV[i];
                Console.WriteLine("\nОптимальные вложения:");
                for (int ik = 0; ik < endV.Length; ik++)
                {
                    Console.Write("V" + (ik + 1) + "=" + endV[ik] + '\t');
                }
            }
             
                

            
        }
    }
}
