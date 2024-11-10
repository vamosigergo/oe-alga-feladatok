using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OE.ALGA.Optimalizalas;

namespace OE.ALGA.Adatszerkezetek
{
    public class VisszalepesesOptimalizacio<T>
    {
        public int n;
        public int[] M;
        public T[,] R;
        public int Lepesszam { get; }

        public Func<int, T, bool> ft;
        public Func<int, T, T[], bool> fk;
        public Func<T[], int> josag;

        public VisszalepesesOptimalizacio(int n, int[] M, T[,] R, Func<int, T, bool> ft, Func<int, T, T[], bool> fk, Func<T[], int> josag)
        {
            this.n = n;
            this.M = M;
            this.R = R;
            this.ft = ft;
            this.fk = fk;
            this.josag = josag;
        }

        public T[] EgyMegoldas()
        {
            bool van = false;
            T[] E = new T[n];
            BackTrack(0, ref E, ref van);
            if (van)
                return E;
            else
                throw new Exception("Nincs megoldása");
        }
        
        void BackTrack(int szint, ref T[] E, ref bool van)
        {
            int i = -1;
            while(!van && i < M[szint] - 1)
            {
                i++;
                if (ft(szint, R[szint, i]))
                {
                    if(fk(szint, R[szint, i], E))
                    {
                        E[szint] = R[szint, i];
                        if (szint == n - 1)
                            van = true;
                        else
                            BackTrack(szint + 1, ref E, ref van);
                    }
                }
            }
        }
    }

    public class VisszalepesesHatizsakPakolas
    {
        HatizsakProblemak problema;
        private HatizsakProblemak probléma;

        public int Lepesszam { get; }

        public VisszalepesesHatizsakPakolas(HatizsakProblemak problema, int lepesszam)
        {
            this.problema = problema;
            Lepesszam = 0;
        }

        public VisszalepesesHatizsakPakolas(HatizsakProblemak probléma)
        {
            this.probléma = probléma;
        }

        public bool[] OptimalisMegoldas()
        {
            bool[,] R = new bool[problema.n, 2];
            int[] M = new int[2];

            for (int i = 0; i < problema.n; i++)
            {
                R[i, 0] = true;
                R[i, 1] = false;
            }

            /*var optimalizacio = new VisszalepesesOptimalizacio<bool>

                bool[] eredmény = optimalizacio.EgyMegoldas();
            Lepesszam = optimalizacio.Lepesszam;
            return eredmény;*/
            



        }
        public int OptimalisErtek()
        {
            bool[] optimálisMegoldás = OptimalisMegoldas();
            int optimálisÉrték = problema.OsszErtek(optimálisMegoldás);
            return optimálisÉrték;
        }




        
        
        
        
        
    }
    public class SzétválasztásÉsKorlátozásOptimalizáció<T> : VisszalepesesOptimalizacio<T>
    {

        private Func<int, T[], int> fb;

        public SzétválasztásÉsKorlátozásOptimalizáció(
            int n,
            int[] M,
            T[,] R,
            Func<int, T, bool> ft,
            Func<int, T, T[], bool> fk,
            Func<T[], int> josag,
            Func<int, T[], int> fb
        ) : base(n, M, R, ft, fk, josag)
        {
            this.fb = fb;
        }


        public T[] OptimalisMegoldasSzétválasztással()
        {
            bool van = false;
            T[] E = new T[n];
            BackTrackSzétválasztás(0, ref E, ref van);
            if (van)
                return E;
            else
                throw new Exception("Nincs optimális megoldás a szétválasztás és korlátozás módszerrel");
        }


        void BackTrackSzétválasztás(int szint, ref T[] E, ref bool van)
        {
            int i = -1;
            while (!van && i < M[szint] - 1)
            {
                i++;
                if (ft(szint, R[szint, i]))
                {
                    if (fk(szint, R[szint, i], E))
                    {
                        E[szint] = R[szint, i];
                        
                        int becsles = fb(szint, E);
                        if (becsles >= josag(E)) 
                        {
                            if (szint == n - 1)
                                van = true;
                            else
                                BackTrackSzétválasztás(szint + 1, ref E, ref van);
                        }
                    }
                }
            }
        }
    }

    public class SzétválasztásÉsKorlátozásHátizsákPakolás : VisszalepesesHatizsakPakolas
    {
        /*public SzétválasztásÉsKorlátozásHátizsákPakolás(HatizsakProblemak probléma)
            : base(probléma) 
        {
        }

        public override bool[] OptimálisMegoldás()
        {
            int[] M = new int[probléma.TárgyakSzáma];
            for (int i = 0; i < M.Length; i++)
            {
                M[i] = 2;
            }

            bool[,] R = new bool[probléma.TárgyakSzáma, 2];
            for (int i = 0; i < probléma.TárgyakSzáma; i++)
            {
                R[i, 0] = true;
                R[i, 1] = false;
            }

            var optimalizacio = new SzétválasztásÉsKorlátozásOptimalizáció<bool>(
                problma.TárgyakSzáma,
                M,
                R,
                (szint, érték) => probléma.Ellenőriz(érték),
                (szint, érték, megoldás) => probléma.Korlát(szint, érték, megoldás),
                megoldás => problema.Jóság(megoldás),
                (szint, részMegoldás) => probléma.FelsőBecslés(szint, részMegoldás)
            );

            bool[] optimalisMegoldas = optimalizacio.EgyMegoldas();
            LepesSzam = optimalizacio.Lepesszam;

            return optimalisMegoldas;
        }*/
    }






}
