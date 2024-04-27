using System.Collections;
using System.Collections.Generic;

namespace TDAs
{
    public class QueueTDA<T>: IQueue<T>
    {
        private T[] values;
        private int index;
        private bool isInitialized;
        public int Count => index;

        public void Enqueue(T t)
        {
            if (!isInitialized) return;
            values[index] = t;
            index++;
        }

        public void Dequeue(T t)
        {
            if (!isInitialized) return;
            index--;
        }

        public T Peek()
        {
            return values[index - 1];
        }

        public bool IsEmpty()
        {
            if (index == 0 && isInitialized) return true;
            return false;
        }

        public void Initialize(int i)
        {
            values = new T[i];
            index = 0;
            isInitialized = true;
        }
    }
}