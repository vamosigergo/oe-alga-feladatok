using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Adatszerkezetek
{
    public class TombVerem<T> : Verem<T>
    {
        public T[] E;
        public int n = 0;
        public bool Ures
        {
            get
            {
                return n == 0;
            }
        }

        public TombVerem(int meret)
        {
            E = new T[meret];
        }

        public T Felso()
        {
            if (!Ures)
            {
                return E[n - 1];
            }
            else
            {
                throw new NincsElemKivetel();
            }
        }

        public void Verembe(T ertek)
        {
            if (n < E.Length)
            {
                E[n] = ertek;
                n++;
            }
            else
            {
                throw new NincsHelyKivetel();
            }
        }

        public T Verembol()
        {
            if (n > 0)
            {
                T ertek = E[n - 1];
                n--;
                return ertek;
            }
            else
            {
                throw new NincsElemKivetel();
            }
        }
    }

    public class TombSor<T> : Sor<T>
    {
        public T[] E;
        public int n; 
        public int e; 
        public int u; 

        public bool Ures
        {
            get
            {
                return n == 0;
            }
        }
        public TombSor(int meret)
        {
            E = new T[meret];
            n = e = u = 0;
        }

        public T Elso()
        {
            if (!Ures)
            {
                return E[e];
            }
            else
            {
                throw new NincsElemKivetel();
            }
        }

        public void Sorba(T ertek)
        {
            if (n < E.Length)
            {
                E[u] = ertek;
                u = (u + 1) % E.Length;
                n++;
            }
            else
            {
                throw new NincsHelyKivetel();
            }
        }

        public T Sorbol()
        {
            if (!Ures)
            {
                T ertek = E[e];
                e = (e + 1) % E.Length;
                n--;
                return ertek;
            }
            else
            {
                throw new NincsElemKivetel();
            }
        }
    }

    public class TombLista<T> : Lista<T>, IEnumerable<T>
    {
        public T[] E;
        public int n;

        public int Elemszam
        {
            get
            {
                return n;
            }
        }

        public TombLista(int meret = 2)
        {
            E = new T[meret];
            n = 0;
        }
        public void Novel()
        {
            T[] E2 = E;
            E = new T[E2.Length * 2];
            for (int i = 0; i < E2.Length; i++)
            {
                E[i] = E2[i];
            }
        }

        public void Bejar(Action<T> muvelet)
        {
            for (int i = 0; i < n; i++)
            {
                muvelet(E[i]);
            }
        }

        public void Beszur(int index, T ertek)
        {
            if (index <= E.Length && index >= 0)
            {
                if (n >= E.Length)
                {
                    Novel();
                }
                for (int i = n; i > index; i--)
                {
                    E[i] = E[i - 1];
                }
                E[index] = ertek;
                n++;
            }
            else
            {
                throw new HibasIndexKivetel();
            }
        }

        public void Hozzafuz(T ertek)
        {
            Beszur(n, ertek);
        }

        public T Kiolvas(int index)
        {
            if (index < n)
            {
                return E[index];
            }
            else
            {
                throw new HibasIndexKivetel();
            }
        }

        public void Modosit(int index, T ertek)
        {
            if (index < n)
            {
                E[index] = ertek;
            }
            else
            {
                throw new HibasIndexKivetel();
            }
        }

        public void Torol(T ertek)
        {
            int db = 0;
            for (int i = 0; i < n; i++)
            {
                if (E[i].Equals(ertek))
                {
                    db++;
                }
                else
                {
                    E[i - db] = E[i];
                }
            }
            n -= db;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new TombListaBejaro<T>(E, n);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    public class TombListaBejaro<T> : IEnumerator<T>
    {
        public T[] E;
        public int n;
        public int aktualisIndex = -1;
        public T aktualis;

        public TombListaBejaro(T[] E, int n)
        {
            this.E = E;
            this.n = n;
        }

        public T Current => E[aktualisIndex];

        object IEnumerator.Current => aktualis;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (aktualisIndex < n - 1)
            {
                aktualisIndex++;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Reset()
        {
            aktualisIndex = -1;
        }
    }
}
