namespace OE.ALGA.Optimalizalas
{
    public class VisszalepesesOptimalizacio<T>
    {
        protected int n;
        protected int[] M;
        protected T[,] R;

        protected Func<int, T, bool> ft;
        protected Func<int, T, T[], bool> fk;
        protected Func<T[], float> josag;
        public int LepesSzam { get; protected set; }

        public VisszalepesesOptimalizacio(int n, int[] M, T[,] R, Func<int, T, bool> ft, Func<int, T, T[], bool> fk, Func<T[], float> josag)
        {
            this.n = n;
            this.M = M;
            this.R = R;
            this.ft = ft;
            this.fk = fk;
            this.josag = josag;
        }
        protected virtual void Backtrack(int szint, ref T[] E, ref bool van, ref T[] O)
        {

            int i = -1;
            while (i < M[szint] - 1)
            {
                ++LepesSzam;
                i++;
                if (ft(szint, R[szint, i]))
                {
                    if (fk(szint, R[szint, i], E))
                    {
                        E[szint] = R[szint, i];
                        if (szint == n - 1)
                        {
                            if (!van || josag(E) > josag(O))
                            {
                                Array.Copy(E, O, E.Length);
                            }
                            van = true;
                        }
                        else Backtrack(szint + 1, ref E, ref van, ref O);
                    }
                }
            }
        }
        public T[] OptimalisMegoldas()
        {
            LepesSzam = 0;
            T[] O = new T[n];
            T[] E = new T[n];
            bool van = false;
            Backtrack(0, ref E, ref van, ref O);
            return O;
        }

    }

    public class VisszalepesesHatizsakPakolas
    {
        protected HatizsakProblema problema;
        public int LepesSzam { get; protected set; }

        public VisszalepesesHatizsakPakolas(HatizsakProblema problema)
        {
            this.problema = problema;
        }

        public virtual bool[] OptimalisMegoldas()
        {
            int n = problema.n;
            int[] M = new int[n];
            bool[,] R = new bool[n, 2];
            for (int i = 0; i < n; i++)
            {
                M[i] = 2;
                R[i, 0] = true;
                R[i, 1] = false;
            }


            VisszalepesesOptimalizacio<bool> optimalizacio = new VisszalepesesOptimalizacio<bool>(
            n, M, R,
            (szint, elem) => true,
            fk,
            josag
            );

            bool[] optimalisMegoldas = optimalizacio.OptimalisMegoldas();
            this.LepesSzam = optimalizacio.LepesSzam;
            return optimalisMegoldas;
        }

        protected bool fk(int szint, bool R, bool[] E)
        {
            E[szint] = R;
            return problema.OsszSuly(E) <= problema.Wmax;
        }
        protected float josag(bool[] pakolas)
        {
            return problema.OsszErtek(pakolas);
        }

        public float OptimalisErtek()
        {
            bool[] optimalisMegoldas = OptimalisMegoldas();
            return problema.OsszErtek(optimalisMegoldas);
        }
    }

    public class SzetvalasztasEsKorlatozasHatizsakPakolas<T> : VisszalepesesOptimalizacio<T>
    {
        protected Func<int, T[], float> fb;
        public SzetvalasztasEsKorlatozasHatizsakPakolas(int n, int[] M, T[,] R, Func<int, T, bool> ft, Func<int, T, T[], bool> fk, Func<T[], float> josag, Func<int, T[], float> fb) : base(n, M, R, ft, fk, josag)
        {
            this.fb = fb;
        }

        protected override void Backtrack(int szint, ref T[] E, ref bool van, ref T[] O)
        {
            int i = -1;
            while (i < M[szint] - 1)
            {
                ++LepesSzam;
                i++;
                if (ft(szint, R[szint, i]))
                {
                    if (fk(szint, R[szint, i], E))
                    {
                        E[szint] = R[szint, i];
                        if (szint == n - 1)
                        {
                            if (!van || josag(E) > josag(O))
                            {
                                Array.Copy(E, O, E.Length);
                            }
                            van = true;
                        }
                        else if (josag(E) + fb(szint, E) > josag(O))
                        {
                            Backtrack(szint + 1, ref E, ref van, ref O);
                        }
                    }
                }
            }
        }
    }

    public class SzetvalasztasEsKorlatozasHatizsakPakolas : VisszalepesesHatizsakPakolas
    {
        public SzetvalasztasEsKorlatozasHatizsakPakolas(HatizsakProblema problema) : base(problema)
        {
        }

        public override bool[] OptimalisMegoldas()
        {
            int n = problema.n;
            int[] M = new int[n];
            bool[,] R = new bool[n, 2];
            for (int i = 0; i < n; i++)
            {
                M[i] = 2;
                R[i, 0] = true;
                R[i, 1] = false;
            }


            SzetvalasztasEsKorlatozasHatizsakPakolas<bool> optimalizacio = new SzetvalasztasEsKorlatozasHatizsakPakolas<bool>(
            n, M, R,
            (szint, elem) => true,
            fk,
            josag,
            fb
            );

            bool[] optimalisMegoldas = optimalizacio.OptimalisMegoldas();
            this.LepesSzam = optimalizacio.LepesSzam;
            return optimalisMegoldas;
        }

        protected float fb(int szint, bool[] E)
        {
            float sum = 0;
            for (int i = szint + 1; i < problema.n; i++)
            {
                if (problema.OsszSuly(E) + problema.w[i] <= problema.Wmax)
                {
                    sum += problema.p[i];
                }
            }
            return sum;
        }
    }

}