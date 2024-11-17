using OE.ALGA.Optimalizalas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Optimalizalas
{
    public class DinamikusHatizsakPakolas
    {
        HatizsakProblema problema;
        public int LepesSzam { get; }
        public DinamikusHatizsakPakolas(HatizsakProblema problema)
        {
            this.problema = problema;
        }
        public float[,] TablazatFeltoltes()
        {
            float[,] F = new float[problema.n + 1, problema.Wmax + 1];
            for (int t = 0; t < problema.n + 1; t++)
            {
                F[t, 0] = 0;
            }
            for (int h = 0; h < problema.Wmax + 1; h++)
            {
                F[0, h] = 0;
            }
            for (int t = 1; t < problema.n + 1; t++)
            {
                for (int h = 1; h < problema.Wmax + 1; h++)
                {
                    if (h >= problema.w[t - 1])
                    {
                        F[t, h] = Math.Max(F[t - 1, h], F[t - 1, h - problema.w[t - 1]] + problema.p[t - 1]);
                    }
                    else
                    {
                        F[t, h] = F[t - 1, h];
                    }
                }
            }
            return F;
        }
        public float OptimalisErtek()
        {
            float[,] F = TablazatFeltoltes();
            return F[problema.n, problema.Wmax];
        }
        public bool[] OptimalisMegoldas()
        {
            float[,] F = TablazatFeltoltes();
            bool[] O = new bool[problema.n];
            int h = problema.Wmax;
            for (int t = problema.n; t > 0; t--)
            {
                if (F[t, h] != F[t - 1, h])
                {
                    O[t - 1] = true;
                    h -= problema.w[t - 1];
                }
                else
                {
                    O[t - 1] = false;
                }
            }

            return O;




        }
    }
}
