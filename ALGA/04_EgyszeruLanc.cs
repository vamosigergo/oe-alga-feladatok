﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OE.ALGA.Adatszerkezetek
{
    internal class LancElem<T>
    {
        public T tart;
        public LancElem<T>? kov;
        public LancElem(T tart, LancElem<T>? kov)
        {
            this.tart = tart;
            this.kov = kov;
        }


    }

    public class LancoltVerem<T> : Verem<T>
    {
        LancElem<T>? fej;
        public LancoltVerem()
        {
            fej = null;
        }
        public bool Ures
        {
            get
            {
                return fej == null;
            }
        }

        public T Felso()
        {
           if(fej != null)
            {
                return fej.tart;
            }
            else
            {
                throw new NincsElemKivetel();
            }
        }

        public void Verembe(T ertek)
        {
            LancElem<T> uj = new LancElem<T>(ertek, fej);
            fej = uj;
        }

        public T Verembol()
        {
            if(fej != null)
            {
                T ertek = fej.tart;
                fej = fej.kov;
                return ertek;
            }
            else
            {
                throw new NincsElemKivetel();
            }
        }
    }

    public class LancoltSor<T> : Sor<T>
    {
        LancElem<T>? fej;
        LancElem<T>? vege;

        public bool Ures
        {
            get
            {
                return fej == null;
               
            }
        }

        public T Elso()
        {
           if(fej != null)
           {
                return fej.tart;
           }
            else
            {
                throw new NincsElemKivetel();
            }
        }

        public void Sorba(T ertek)
        {
            LancElem<T> uj = new LancElem<T>(ertek, null);
            if(vege != null)
            {
                vege.kov = uj;
            }
            else
            {
                fej = uj;
            }

            vege = uj;
        }

        public T Sorbol()
        {
           if(fej != null)
           {
                T ertek = fej.tart;
                LancElem<T>? q;
                q = fej;
                fej = fej.kov;
                if(fej == null)
                {
                    vege = null;
                }
                return ertek;
           }

           else
           {
                throw new NincsElemKivetel();
           }
        }
    }

    public class LancoltLista<T> : Lista<T>, IEnumerable<T>
    {
        LancElem<T>? fej;
        LancElem<T>? p;
        LancElem<T>? e;
        LancElem<T>? q;

        public int Elemszam
        {
            get
            {
                int count = 0;
                var current = fej;
                while(current != null)
                {
                    count++;
                    current = current.kov;
                }
                return count;
            }
        }

        public void Bejar(Action<T> muvelet)
        {
            p = fej;
            while(p != null)
            {
                muvelet(p.tart);
                p = p.kov;
            }
        }

        public void Beszur(int index, T ertek)
        {
           
            if (fej == null || index == 0)
            {
                LancElem<T> uj = new LancElem<T>(ertek, fej);
                fej = uj;
            }
            else
            {
                p = fej;
                int i = 2;
                while (p.kov != null && i < index) 
                {
                    p = p.kov;
                    i++;
                }
                if(i <= index)
                {
                    LancElem<T> uj = new LancElem<T>(ertek, p.kov);
                    p.kov = uj;
                }
                else
                {
                    throw new HibasIndexKivetel();
                }
            }
           
            
        }

        

        public void Hozzafuz(T ertek)
        {
            LancElem<T> uj = new LancElem<T>(ertek, null);
            if(fej == null)
            {
                fej = uj;
            }
            else
            {
                p = fej;
                while(p.kov != null)
                {
                    p = p.kov;
                }
                p.kov = uj;
            }
        }

        public T Kiolvas(int index)
        {
            p = fej;
            int i = 1;
            while(p != null && i < index)
            {
                p = p.kov;
                i++;
            }
            if(p != null)
            {
                return p.tart;
            }
            else
            {
                throw new HibasIndexKivetel();
            }
        }

        public void Modosit(int index, T ertek)
        {
            p = fej;
            int i = 1;
            while (p != null && i < index)
            {
                p = p.kov;
                i++;
            }
            if (p != null)
            {
                 p.tart = ertek;
            }
            else
            {
                throw new HibasIndexKivetel();
            }
        }

        public void Torol(T ertek)
        {
            p = fej;
            e = null;

            do
            {
                while (p != null && !EqualityComparer<T>.Default.Equals(p.tart, ertek))
                {
                    e = p;
                    p = p.kov;
                }
                if(p != null)
                {
                    q = p.kov;
                    if (e == null)
                    {
                        fej = q;
                    }
                    else
                    {
                        e.kov = q;
                    }
                    p = q;
                }
                
            }
            while (p != null);
  
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        public IEnumerator<T> GetEnumerator()
        {
            var current = fej;
            while (current != null)
            {
                yield return current.tart;  
                current = current.kov;      
            }
        }
    }

    public class LancoltListaBejaro<T> : IEnumerator<T>
    {

        private LancElem<T> currentElem;
        private LancElem<T> fej;
        public T Current => currentElem.tart;

        object IEnumerator.Current => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool MoveNext()
        {
            if (currentElem == null)
            {
                currentElem = fej; 
            }
            else
            {
                currentElem = currentElem.kov; 
            }

            return currentElem != null; 
        }

        public void Reset()
        {
            currentElem = null;
        }
    }
}
