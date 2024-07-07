using System;

namespace TDAs.Set
{
    public class StaticSetTDA<T> : ISet<T>
    {
        private T[] values;
        private int index;
        private bool isInitialized = false;
        
        public void Init(int size)
        {
            values = new T[size];
            index = 0;
            isInitialized = true;
        }

        public bool IsEmpty()
        {
            CheckInit();
            return index == 0;
        }

        public void Add(int t)
        {
            throw new NotImplementedException();
        }

        public void Add(T t)
        {
            CheckInit();
            values[index] = t;
            index++;
        }


        public T Choose()
        {
            CheckInit();
            return values[index - 1];
        }

        public void Remove(int t)
        {
            CheckInit();
            
            for (int i = 0; i < index; i++)
            {
                if (t.Equals(values[i]))
                {
                    values[i] = values[index];
                    values[index] = default;
                    index--;
                    return;
                }
            }
        }

        public bool Contains(int t)
        {
            CheckInit();
            
            for (int i = 0; i < index; i++)
            {
                if (values[i].Equals(t))
                {
                    return true;
                }
            }

            return false;
        }
        private void CheckInit()
        {
            if (!isInitialized)
            {
                throw new Exception("Set not initialized");
            }
        }

    }
}
