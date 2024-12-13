using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Adatszerkezetek
{
    public class SulyozottEgeszGrafEl : EgeszGrafEl, SulyozottGrafEl<int>
    {
        public SulyozottEgeszGrafEl(int honnan, int hova, float suly) : base(honnan, hova)
        {
            this.Suly = suly;
        }

        public float Suly { get; }
    }

    public class CsucsmatrixSulyozottEgeszGraf : SulyozottGraf<int, SulyozottEgeszGrafEl>
    {
        int n;
        float[,] M;

        public CsucsmatrixSulyozottEgeszGraf(int n)
        {
            this.n = n;
            M = new float[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    M[i, j] = float.NaN;
                }
            }
        }

        public int CsucsokSzama
        {
            get { return n; }
        }


        public int ElekSzama
        {
            get
            {
                int count = 0;
                for (int i = 0; i < M.GetLength(0); i++)
                {
                    for (int k = 0; k < M.GetLength(1); k++)
                    {
                        if (!float.IsNaN(M[i, k]))
                        {
                            count++;
                        }
                    }
                }
                return count;
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
        public Halmaz<SulyozottEgeszGrafEl> Elek
        {
            get
            {
                Halmaz<SulyozottEgeszGrafEl> elek = new FaHalmaz<SulyozottEgeszGrafEl>();
                for (int i = 0; i < M.GetLength(0); i++)
                {
                    for (int j = 0; j < M.GetLength(1); j++)
                    {
                        if (VezetEl(i, j))
                        {
                            SulyozottEgeszGrafEl egyel = new SulyozottEgeszGrafEl(i, j, Suly(i, j));
                            elek.Beszur(egyel);
                        }
                    }
                }
                return elek;
            }
        }
        public float Suly(int honnan, int hova)
        {
            if (VezetEl(honnan, hova))
            {
                return M[honnan, hova];
            }
            throw new NincsElKivetel();

        }

        public Halmaz<int> Szomszedai(int csucs)
        {
            Halmaz<int> szomszed = new FaHalmaz<int>();
            for (int i = 0; i < n; i++)
            {
                if (VezetEl(csucs, i))
                {
                    szomszed.Beszur(i);
                }
            }
            return szomszed;
        }

        public void UjEl(int honnan, int hova, float suly)
        {
            M[honnan, hova] = suly;
        }

        public bool VezetEl(int honnan, int hova)
        {
            return !float.IsNaN(M[honnan, hova]);
        }
    }
    public class Utkereses
    {
        public static Szotar<V, float> Dijkstra<V, E>(SulyozottGraf<V, E> g, V start)
        {
            Szotar<V, float> L = new HasitoSzotarTulcsordulasiTerulettel<V, float>(g.CsucsokSzama);
            Szotar<V, V> P = new HasitoSzotarTulcsordulasiTerulettel<V, V>(g.CsucsokSzama);
            KupacPrioritasosSor<V> S = new KupacPrioritasosSor<V>(g.CsucsokSzama, (ez, ennel) => L.Kiolvas(ez) < L.Kiolvas(ennel));

            g.Csucsok.Bejar(x =>
            {
                L.Beir(x, float.MaxValue);
                S.Sorba(x);
            });
            L.Beir(start, 0);
            S.Frissit(start);
            while (!S.Ures)
            {
                V u = S.Sorbol();
                g.Szomszedai(u).Bejar(x =>
                {
                    float ujsuly = L.Kiolvas(u) + g.Suly(u, x);
                    if (ujsuly < L.Kiolvas(x))
                    {
                        L.Beir(x, ujsuly);
                        P.Beir(x, u);
                        S.Frissit(x);
                    }
                });
            }
            return L;
        }



    }
    public class FeszitofaKereses
    {
        public static Szotar<V, V> Prim<V, E>(SulyozottGraf<V, E> g, V start)
        {

            Szotar<V, float> K = new HasitoSzotarTulcsordulasiTerulettel<V, float>(g.CsucsokSzama);
            Szotar<V, V> P = new HasitoSzotarTulcsordulasiTerulettel<V, V>(g.CsucsokSzama);
            KupacPrioritasosSor<V> S = new KupacPrioritasosSor<V>(g.CsucsokSzama, (ez, ennel) => K.Kiolvas(ez) < K.Kiolvas(ennel));
            List<V> W = new List<V>();


            g.Csucsok.Bejar(x =>
            {
                K.Beir(x, float.MaxValue);
                P.Beir(x, default);
                S.Sorba(x);
                
                W.Add(x);
            });
            K.Beir(start, 0);
            S.Frissit(start);


            while (!S.Ures)
            {
                V u = S.Sorbol();
                               
                W.Remove(u);


                g.Szomszedai(u).Bejar(v =>
                {
                    if ( W.Contains(v) && g.Suly(u, v) < K.Kiolvas(v))
                    {
                        K.Beir(v, g.Suly(u, v));
                        P.Beir(v, u);
                        S = new KupacPrioritasosSor<V>(g.CsucsokSzama, (ez, ennel) => K.Kiolvas(ez) < K.Kiolvas(ennel));
                        g.Csucsok.Bejar(cs =>
                        {
                            if (W.Contains(cs))
                            {
                                S.Sorba(cs);
                            }
                        });

                        
                    }
                });
            }

            return P;

        }

        public static Halmaz<E> Kruskal<V, E>(SulyozottGraf<V, E> g) where E : SulyozottGrafEl<V>, IComparable
        {
    
            Halmaz<E> A = new FaHalmaz<E>();
            List<HashSet<V>> halmazok = new List<HashSet<V>>();
            g.Csucsok.Bejar(x =>
            {
                halmazok.Add(new HashSet<V> { x });
            });
            KupacPrioritasosSor<E> S = new KupacPrioritasosSor<E>(g.ElekSzama, (ez, ennel) => ez.Suly.CompareTo(ennel.Suly) < 0);
            g.Elek.Bejar(S.Sorba);
            while (!S.Ures)
            {
                E e = S.Sorbol();
                HashSet<V> halmaz1 = halmazok.FirstOrDefault(h => h.Contains(e.Honnan));
                HashSet<V> halmaz2 = halmazok.FirstOrDefault(h => h.Contains(e.Hova));

                if (halmaz1 != halmaz2)
                {
                    A.Beszur(e);
                    halmaz1.UnionWith(halmaz2);
                    halmazok.Remove(halmaz2);
                }
            }
            return A;
        }


    }
}
