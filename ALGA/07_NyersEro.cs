using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Optimalizalas
{
    public class HatizsakProblema
    {
        public int n { get; }
        public int Wmax { get; }
        public int[] w { get; }
        public float[] p { get; }
        public HatizsakProblema(int n, int Wmax, int[] w, float[] p)
        {
            if (n < 1)
            {
                throw new Exception();
            }
            if (Wmax < 1)
            {
                throw new Exception();
            }
            this.n = n;
            this.Wmax = Wmax;
            this.w = w;
            this.p = p;
        }
        public int OsszSuly(bool[] pakolas)
        {
            int sum = 0;
            for (int i = 0; i < pakolas.Length; i++)
            {
                if (pakolas[i])
                {
                    sum += w[i];
                }
            }
            return sum;
        }
        public float OsszErtek(bool[] pakolas)
        {
            float sum = 0;
            for (int i = 0; i < pakolas.Length; i++)
            {
                if (pakolas[i])
                {
                    sum += p[i];
                }
            }
            return sum;
        }
        public bool Ervenyes(bool[] pakolas)
        {
            if (OsszSuly(pakolas) <= Wmax)
            {
                return true;
            }
            return false;
        }
    }
    public class NyersEro<T>
    {
        int m;
        Func<int, T> generator;
        Func<T, float> josag;
        public int LepesSzam { get; private set; }
        public NyersEro(int m, Func<int, T> generator, Func<T, float> josag)
        {
            this.m = m;
            this.generator = generator;
            this.josag = josag;
        }
        public T OptimalisMegoldas()
        {
            T o = generator(1);
            for (int i = 1; i < m; i++)
            {
                T x = generator(i);
                LepesSzam++;
                if (josag(x) > josag(o))
                {
                    o = x;
                }
            }
            return o;
        }

    }
    public class NyersEroHatizsakPakolas
    {
        HatizsakProblema problema;
        float optimalisertek;
        public int LepesSzam { get; private set; }
        public NyersEroHatizsakPakolas(HatizsakProblema problema)
        {
            this.problema = problema;
        }
        public bool[] Generator(int i)
        {
            bool[] K = new bool[problema.n];
            int szam = i - 1;
            for (int j = 0; j < problema.n; j++)
            {
                K[j] = ((szam / (int)Math.Pow(2, j)) % 2) == 1;
            }
            return K;
        }
        public float Josag(bool[] pakolas)
        {
            if (!problema.Ervenyes(pakolas))
            {
                return -1;
            }
            return problema.OsszErtek(pakolas);
        }
        public bool[] OptimalisMegoldas()
        {
            NyersEro<bool[]> o = new NyersEro<bool[]>((int)Math.Pow(2, problema.n), generator: Generator, josag: Josag);
            bool[] optimalispakolas = o.OptimalisMegoldas();
            LepesSzam = o.LepesSzam;
            optimalisertek = problema.OsszErtek(optimalispakolas);
            return optimalispakolas;
        }
        public float OptimalisErtek()
        {
            if (optimalisertek == 0)
            {
                OptimalisMegoldas();
            }
            return optimalisertek;
        }
    }
}
