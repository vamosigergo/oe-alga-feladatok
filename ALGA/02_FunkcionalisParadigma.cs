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
        public FeltetelesFeladatTarolo(int meret) : base(meret)
        {

        }

        public Func<T, bool> BejaroFeltetel { get; set; }

        public void FeltetelesVegrehajtas(Func<T,bool> feltetel)
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
            Func<T, bool> feltétel = BejaroFeltetel ?? (x => true);
            return new FeltetesFeladatTárolóBejáró<T>(tarolo, feltétel);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class FeltetesFeladatTárolóBejáró<T> : IEnumerator<T> where T : IVegrehajthato
    {
        private T[] tarolo;
        private Func<T, bool> bejaroFeltetel;
        private int aktuálisPozíció = -1;

       
        public FeltetesFeladatTárolóBejáró(T[] feladatok, Func<T, bool> feltétel)
        {
            this.tarolo = tarolo;
            this.bejaroFeltetel = bejaroFeltetel;
        }

        
        public bool MoveNext()
        {
            aktuálisPozíció++;
            while (aktuálisPozíció < tarolo.Length)
            {
                if (bejaroFeltetel(tarolo[aktuálisPozíció]))
                {
                    return true;
                }
                aktuálisPozíció++;
            }
            return false;
        }

        
        public void Reset()
        {
            aktuálisPozíció = -1;
        }

        
        public T Current
        {
            get
            {
                if (aktuálisPozíció < 0 || aktuálisPozíció >= tarolo.Length)
                {
                    throw new InvalidOperationException();
                }
                return tarolo[aktuálisPozíció];
            }
        }

        object System.Collections.IEnumerator.Current => Current;

       
        public void Dispose() { }
    }



}
