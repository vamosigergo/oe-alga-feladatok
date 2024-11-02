using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Optimalizalas
{
   public class HatizsakProblemak
    {
        internal int n { get; }
        public int Wmax { get; }
        public int[] w { get; }
        public int[] p { get; }

        public HatizsakProblemak(int n, int wmax, int[] weights, int[] values)
        {
            n =this.n;
            Wmax = wmax;
            w = new int[n];
            for (int i = 0; i < n; i++)
            {
                w[i] = weights[i];
            }

            p = new int[n];
            for (int i = 0; i < n; i++)
            {
                p[i] = values[i];
            }
        }

        public int OsszSuly(bool[] X)
        {
            int s = 0;

            for (int i = 0; i < n; i++)
            {
                if (X[i] == true)
                {
                    s = s + w[i];
                }
            }

            return s;
        }

        public int OsszErtek(bool[] X)
        {
            int s = 0;

            for (int i = 0; i < n; i++)
            {
                if (X[i] == true)
                {
                    s = s + p[i];
                }
            }

            return s;
        }

        public bool Ervenyes(bool[] n)
        {
            int totalWeight = OsszSuly(n);

            if (totalWeight <= Wmax)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

    public class NyersEro<T>
    {
        public int m;
        private Func<int, T> generator;
        private Func<T, double> josag;
        private int lepesszam;
        public int LépésSzám => lepesszam;

        public NyersEro(int m, Func<int, T> generator, Func<T, double> josag)
        {

            this.m = m;
            this.generator = generator;
            this.josag = josag;
            this.lepesszam = 0;

        }

        public bool[] Generator(int index)
        {

            int n = 0;
            int szám = index - 1; 
            bool[] K = new bool[n]; 

            for (int j = 0; j < n; j++) 
            {
                K[j] = (szám / Math.Pow(2, j - 1) % 2 == 1); 
            }

            return K; 
        }

        public double Josag(bool[] megoldas)
        {
            double osszErtek = 0;  
            double osszSuly = 0;   

            
            for (int i = 0; i < megoldas.Length; i++)
            {
                if (megoldas[i]) 
                {
                    // osszErtek += p[i];  
                    // osszSuly += w[i];   
                }
            }

            
            return osszErtek; 
        }

        public T OptimálisMegoldás()
        {

            bool[] o = Generator(1);

            for (int i = 2; i < m; i++)
            {
                
                bool[] x = Generator(i);

                
                if (Josag(x) > Josag(o))
                {
                    
                    o = x;
                }
                
            }


            
            //return o;
        }
    }

    public class NyersEroHatizsakPakolas
    {
        private int lepesszam;
        public int LépésSzám => lepesszam;
        private HatizsakProblemak hátizsákProbléma;
        public bool[] OptimalisPakolas { get; private set; }
        public double OptimalisErtek { get; private set; }

        public NyersEroHatizsakPakolas(HatizsakProblemak hátizsákProbléma)
        {
            this.hátizsákProbléma = hátizsákProbléma;


        }
        public bool[] Generator(int index)
        {

            int n = 0;
            int szám = index - 1;
            bool[] K = new bool[n];

            for (int j = 0; j < n; j++)
            {
                K[j] = (szám / Math.Pow(2, j - 1) % 2 == 1);
            }

            return K;


        }

        public double Josag(bool[] megoldas)
        {
            double osszErtek = 0;
            double osszSuly = 0;


            for (int i = 0; i < megoldas.Length; i++)
            {
                if (megoldas[i])
                {
                    // osszErtek += p[i];  
                    // osszSuly += w[i];   
                }
            }


            return osszErtek;
        }

        public bool[] OptimálisMegoldás()
        {
            // i. Hozz létre egy új NyersErő objektumot
            NyersEro<bool[]> nyersErő = new NyersEro<bool[]>(
                (int)Math.Pow(2, hátizsákProbléma.n), // 2^n lehetséges megoldás
                Generator, // Generátor függvény
                Josag // Jóság függvény
            );

            // ii. Hívd meg az OptimálisMegoldás metódusát
            OptimalisPakolas = nyersErő.OptimálisMegoldás();

            // iii. A lépésszám tárolása
            lepesszam = nyersErő.LépésSzám;

            OptimalisErtek = Josag(OptimalisPakolas);

            // iv. Visszaadja az optimális pakolást
            return OptimalisPakolas;
        }


    }
}
