using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Adatszerkezetek
{
    public class TömbVerem<T> : Verem<T>
    {
         T[] E;
         int n;

        public bool Ures
        {
            get
            {
                return n == 0;
            }
        }
        public TömbVerem(int meret)
        {
            E = new T[meret];
             
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
               T ertek = E[n-1];
                n--;
                return ertek;
            }
            else
            {
                throw new NincsElemKivetel();
            }
            
        }

        public T Felso()
        {
            if (!Ures)
            {
                return E[n-1];
            }
            else
            {
                throw new NincsElemKivetel();
            }
        }
    }

    public class TombSor<T> : Sor<T>
    {
        T[] E;
        int n;
        int e;
        int u;

        public TombSor(int meret)
        {
            E = new T[meret];
        }
        
        public bool Ures
        {
            get
            {
                return n == 0;
            }
        }

        public T Elso()
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

        public void Sorba(T ertek)
        {
            if(n < E.Length)
            {
                n++;
                u = u % E.Length + 1;
                E[u] = ertek;
            }
            else
            {
                throw new NincsHelyKivetel();
            }
        }

        public T Sorbol()
        {
           if (n > 0)
            {
                n--;
                e = e % E.Length + 1;
                T ertek = E[e];
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
        T[] E;
        int n;
        
        public int Elemszam
        {
            get
            {
                return n;
            }
        }

        public TombLista(int meret)
        {
            E = new T[meret];
        }

        public void MeretNovel()
        {
            T[] É = E;
            E = new T[E.Length * 2];

            for (int i = 0; i < n; i++)
            {
                E[i] = É[i];
            }
            É = null;
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
            if(index <= n + 1)
            {
                n++;
                for (int i = 0; i < index; i++)
                {
                    E[i] = E[i - 1];
                }
                E[index] = ertek;
            }
            else if (n == E.Length)
            {
                MeretNovel();
            }
            else
            {
                throw new HibasIndexKivetel();
            }
        }

        public void Hozzafuz(T ertek)
        {
            Beszur(n + 1, ertek);
        }

        public T Kiolvas(int index)
        {
            if(index <= n)
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
            if(index <= n)
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
            n = n - db;
        }

        public IEnumerator<T> GetEnumerator()
        {
            TombListaBejaro<T> bejaro = new TombListaBejaro<T>(E, n);
            return bejaro;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    public class TombListaBejaro<T> : IEnumerator<T>
    {
        T[] E;
        int n;
        int AktualisIndex = -1;

        public TombListaBejaro(T[] E, int n)
        {
            this.E = E;
            this.n = n;
        }
        
        public T Current
        {
            get
            {
                return E[AktualisIndex];
            }
        }

        object IEnumerator.Current => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            if(AktualisIndex < n)
            {
                AktualisIndex++;
                return true;
            }
            else
            {
                return false;   
            }
        }

        public void Reset()
        {
            AktualisIndex--;
        }
    }
}
