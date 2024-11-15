using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Paradigmak
{
    public class FeltetelesFeladatTarolo<T> : FeladatTarolo<T>, IEnumerable<T> where T : IVegrehajthato
    {
        public Func<T, bool> BejaroFeltetel { get; set; }
        public FeltetelesFeladatTarolo(int meret) : base(meret)
        {
        }
        public void FeltetelesVegrehajtas(Func<T, bool> feltetel)
        {
            for (int i = 0; i < n; i++)
            {
                if (feltetel(tarolo[i]))
                {
                    tarolo[i].Vegrehajtas();
                }
            }
        }
        public IEnumerator<T> GetEnumerator()
        {
            if (BejaroFeltetel != null)
            {
                return new FeltetelesFeladatTaroloBejaro<T>(tarolo, n, BejaroFeltetel);
            }
            else
            {
                return new FeltetelesFeladatTaroloBejaro<T>(tarolo, n, x => true);

            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class FeltetelesFeladatTaroloBejaro<T> : IEnumerator<T>
    {
        T[] tarolo;
        int n;
        int aktualisindex = -1;
        Func<T, bool> BejaroFeltetel;

        public FeltetelesFeladatTaroloBejaro(T[] tarolo, int n, Func<T, bool> bejaroFeltetel)
        {
            this.tarolo = tarolo;
            this.n = n;
            this.BejaroFeltetel = bejaroFeltetel;
        }

        public T Current => tarolo[aktualisindex];

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            
            while (aktualisindex < n - 1)
            {
                aktualisindex++;
                if (aktualisindex < tarolo.Length && BejaroFeltetel(tarolo[aktualisindex]))
                {
                    return true;
                }
            }
            return false;
        }

        public void Reset()
        {
            aktualisindex = 1;
        }
    }
}


