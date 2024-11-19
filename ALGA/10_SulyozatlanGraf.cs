using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Adatszerkezetek
{
    public class EgeszGrafEl : GrafEl<int>, IComparable
    {
        public int Honnan { get; private set; }
        public int Hova { get; private set; }
        public EgeszGrafEl(int honnan, int hova)
        {
            Honnan = honnan;
            Hova = hova;
        }

        public virtual int CompareTo(object? obj)
        {
            if (obj != null && obj is EgeszGrafEl b)
            {
                if (Honnan != b.Honnan)
                {
                    return Honnan.CompareTo(b.Honnan);
                }
                else
                {
                    return Hova.CompareTo(b.Hova);
                }
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
    }
    public class CsucsmatrixSulyozatlanEgeszGraf : SulyozatlanGraf<int, EgeszGrafEl>
    {
        int n;
        bool[,] M;
        public CsucsmatrixSulyozatlanEgeszGraf(int n)
        {
            this.n = n;
            M = new bool[n, n];
        }

        public int CsucsokSzama => n;

        public int ElekSzama
        {
            get
            {
                int x = 0;
                for (int i = 0; i < M.GetLength(0); i++)
                {
                    for (int j = 0; j < M.GetLength(1); j++)
                    {
                        if (M[i, j])
                        {
                            x++;
                        }
                    }
                }
                return x;
            }
        }

        public Halmaz<int> Csucsok
        {
            get
            {
                Halmaz<int> csucsok = new FaHalmaz<int>();
                for (int i = 0; i < n; i++)
                {
                    csucsok.Beszur(i);
                }
                return csucsok;
            }
        }

        public Halmaz<EgeszGrafEl> Elek
        {
            get
            {
                Halmaz<EgeszGrafEl> elek = new FaHalmaz<EgeszGrafEl>();
                for (int i = 0; i < M.GetLength(0); i++)
                {
                    
                    for (int j = 0; j < M.GetLength(1); j++)
                    {
                        if (M[i, j])
                        {
                            EgeszGrafEl egyel = new EgeszGrafEl(i, j);
                            elek.Beszur(egyel);
                        }
                    }
                    
                }
                return elek;
            }
        }

        public Halmaz<int> Szomszedai(int csucs)
        {
            Halmaz<int> szomszed = new FaHalmaz<int>();
            for (int j = 0; j < n; j++)
            {
                if (M[csucs, j])
                    szomszed.Beszur(j);
            }
            return szomszed;
        }

        public void UjEl(int honnan, int hova)
        {
            M[honnan, hova] = true;
        }

        public bool VezetEl(int honnan, int hova)
        {
            return M[honnan, hova];
        }
    }
    public static class GrafBejarasok
    {

        public static Halmaz<V> SzelessegiBejaras<V, E>(Graf<V, E> g, V start, Action<V> muvelet) where V : IComparable
        {
            Sor<V> S = new LancoltSor<V>();
            Halmaz<V> F = new FaHalmaz<V>();
            S.Sorba(start);
            F.Beszur(start);

            while (!S.Ures)
            {
                V k = S.Sorbol();
                muvelet(k);

                var temp = g.Szomszedai(k);
                Action<V> ide = x =>
                {
                    if (!F.Eleme(x))
                    {
                        S.Sorba(x);
                        F.Beszur(x);
                    }
                };
                temp.Bejar(ide);
            }
            return F;
        }

        public static void MelysegiBejarasRekurziv<V, E>(Graf<V, E> g, V k, Halmaz<V> F, Action<V> muvelet)
        {
            F.Beszur(k);
            muvelet(k);

            var temp = g.Szomszedai(k);
            Action<V> ide = x =>
            {
                if (!F.Eleme(x))
                {
                    MelysegiBejarasRekurziv(g, x, F, muvelet);
                }
            };
            temp.Bejar(ide);



        }

        public static Halmaz<V> MelysegiBejaras<V, E>(Graf<V, E> g, V start, Action<V> muvelet) where V : IComparable
        {
            var F = new FaHalmaz<V>();
            MelysegiBejarasRekurziv(g, start, F, muvelet);
            return F;
        }
    }



}
