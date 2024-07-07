namespace TDAs
{
    public class StackTDA<T> : IStack<T>
    {
        private T[] values;
        private int index;
        
        public void Push(T t)
        {
            values[index] = t;
            index++;
        }

        public void Pop()
        {
            index--;
        }

        public T Peek()
        {
            return values[index];
        }

        public bool IsEmpty()
        {
            if (index == 0) return true;
            return false;
        }
    }
}