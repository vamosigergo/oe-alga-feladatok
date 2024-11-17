using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Adatszerkezetek
{
    public class SzotarElem<K, T>
    {
        public K kulcs;
        public T tart;
        public SzotarElem(K kulcs, T tart)
        {
            this.kulcs = kulcs;
            this.tart = tart;
        }
    }

    public class HasitoSzotarTulcsordulasiTerulettel<K, T> : Szotar<K, T>
    {
        SzotarElem<K, T>[] E;
        Func<K, int> h;
        Lista<SzotarElem<K, T>> U = new LancoltLista<SzotarElem<K, T>>();

        public HasitoSzotarTulcsordulasiTerulettel(int meret, Func<K, int> hasitofuggveny)
        {
            E = new SzotarElem<K, T>[meret];
            h = (x => Math.Abs(hasitofuggveny(x)) % E.Length);
        }

        public HasitoSzotarTulcsordulasiTerulettel(int meret) : this(meret, x => x.GetHashCode())
        {
        }

        public void Beir(K kulcs, T ertek)
        {
            SzotarElem<K, T> meglevo = KulcsKeres(kulcs);
            if (meglevo != null)
            {
                meglevo.tart = ertek;
            }
            else
            {
                SzotarElem<K, T> uj = new SzotarElem<K, T>(kulcs, ertek);
                if (E[h(kulcs)] == null)
                {
                    E[h(kulcs)] = uj;
                }
                else
                {
                    U.Hozzafuz(uj);
                }
            }

        }

        public T Kiolvas(K kulcs)
        {
            SzotarElem<K, T> meglevo = KulcsKeres(kulcs);
            if (meglevo != null)
            {
                return meglevo.tart;
            }
            else
            {
                throw new HibasKulcsKivetel();
            }
        }

        public void Torol(K kulcs)
        {
            int index = h(kulcs);

            if (E[index] != null && E[index].kulcs.Equals(kulcs))
            {
                E[index] = null;
            }
            else
            {
                SzotarElem<K, T> keresettElem = null;
                U.Bejar(x =>
                {
                    if (x.kulcs.Equals(kulcs))
                    {
                        keresettElem = x;
                    }
                });
                if (keresettElem != null)
                {
                    U.Torol(keresettElem);
                }
                else
                {
                    throw new HibasKulcsKivetel();
                }
            }
        }

        public SzotarElem<K, T> KulcsKeres(K kulcs)
        {
            int index = h(kulcs);
            if (E[index] != null && E[index].kulcs.Equals(kulcs))
            {
                return E[index];
            }
            else
            {
                SzotarElem<K, T> keresettElem = null;
                U.Bejar(x =>
                {
                    if (x.kulcs.Equals(kulcs))
                    {
                        keresettElem = x;
                    }
                });
                
                return keresettElem;
            }
        }
    }
}
