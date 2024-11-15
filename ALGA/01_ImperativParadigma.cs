using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Paradigmak
{
    public interface IFuggo
    {
        public bool FuggosegTeljesul { get; }
    }
    public interface IVegrehajthato
    {
        void Vegrehajtas();
    }

    public class FuggoFeladatTarolo<T> : FeladatTarolo<T> where T : IVegrehajthato, IFuggo
    {
        public FuggoFeladatTarolo(int meret) : base(meret)
        {
        }
        public override void MindentVegrehajt()
        {
            for (int i = 0; i < n; i++)
            {
                if (tarolo[i].FuggosegTeljesul)
                {
                    tarolo[i].Vegrehajtas();
                }
            }
        }

    }

    public class FeladatTaroloBejaro<T> : IEnumerator<T>
    {
        T[] tarolo;
        int n;
        int aktualisindex = -1;
        public FeladatTaroloBejaro(T[] tarolo, int n)
        {
            this.tarolo = tarolo;
            this.n = n;
        }

        public T Current
        {
            get
            {
                return tarolo[aktualisindex];
            }
        }

        object IEnumerator.Current => Current;

        public void Dispose()
        {

        }

        public bool MoveNext()
        {
            if (aktualisindex < n - 1)
            {
                aktualisindex++;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Reset()
        {
            aktualisindex = -1;
        }
    }

    public class FeladatTarolo<T> : IEnumerable<T> where T : IVegrehajthato
    {
        public T[] tarolo;
        public int n = 0;
        public FeladatTarolo(int meret)
        {
            tarolo = new T[meret];
        }
        public void Felvesz(T elem)
        {
            if (n < tarolo.Length)
            {
                tarolo[n] = elem;
                n++;
            }
            else
            {
                throw new TaroloMegteltKivetel();

            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new FeladatTaroloBejaro<T>(tarolo, n);
        }

        public virtual void MindentVegrehajt()
        {
            for (int i = 0; i < n; i++)
            {
                tarolo[i].Vegrehajtas();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    public class TaroloMegteltKivetel : Exception
    {

    }
}

