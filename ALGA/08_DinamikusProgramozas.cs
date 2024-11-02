using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace OE.ALGA.Optimalizalas
{
    public class DinamikusHátizsákPakolas
    {
        HatizsakProblemak problema;
        public int Lepesszam { get; private set; }

        public DinamikusHátizsákPakolas(HatizsakProblemak problema)
        {
            this.problema = problema;
            Lepesszam = 0;
        }

        public int TablazatFeltoltes()
        {
            int n = problema.n;
            int W = problema.Wmax;
            int[,] F = new int[n + 1, W + 1];

            for (int t = 0; t < n; t++)
            {
                F[t, 0] = 0;
            }
            for (int h = 0; h < W; h++)
            {
                F[0, h] = 0;
            }
            for (int t = 0; t < n; t++)
            {
                for (int h = 0; h < W; h++)
                {
                    int wt = problema.w[t - 1];
                    int pt = problema.p[t-1];

                    if (h >= wt)
                    {
                        F[t, h] = Math.Max(F[t - 1, h], F[t - 1, h - wt] + pt);
                    }
                    else
                    {
                        F[t, h] = F[t - 1, h];
                    }
                }
            }

            return F[n, W];
        }

        public int OptimalisErtek()
        {
            return TablazatFeltoltes();
        }

        public bool[] OptimalisMegoldas(int[,] F)
        {
            int n = problema.n;
            int Wmax = problema.Wmax;

            bool[] O = new bool[n];

            int t = n;
            int h = Wmax;

            while (t > 0 && h > 0)
            {
                
                if (F[t, h] != F[t - 1, h])
                {
                    O[t] = true;
                    h = h - Wmax;
                }
                t--; 
            }

            return O; 
        }
    }
}
