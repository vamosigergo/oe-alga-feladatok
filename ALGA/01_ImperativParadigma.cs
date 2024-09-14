using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Paradigmak
{
    public interface IVegrehajthato
    {
        void Vegrehajtas();
    }

    public interface IFuggo
    {
        bool FuggosegTeljesul { get; }
    }


    public class FeladatTarolo<T> : IEnumerable<T> where T : IVegrehajthato
    {
        public T[] tarolo;
        public int n = 0;
        public FeladatTarolo(int meret)
        {
            tarolo = new T[meret];
            n++;

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
            throw new NotImplementedException();
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
            TaroloBejaro<T> bejaro = new TaroloBejaro<T>(tarolo, n);
            return bejaro;
        }
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

    public class TaroloBejaro<T> : IEnumerator<T>
    {
        T[] E;
        int n;
        int AktualisIndex = -1;

        public TaroloBejaro(T[] E, int n)
        {
            this.E = E;
            this.n = n;
        }

        public T Current
        {

            get { return E[AktualisIndex]; } 
        }

        object IEnumerator.Current => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            if(AktualisIndex < n-1)
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
            AktualisIndex = -1;
        }
    }

    

    public class TaroloMegteltKivetel() : Exception
    {

    }






}
