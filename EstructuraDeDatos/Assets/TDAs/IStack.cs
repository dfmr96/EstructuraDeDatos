namespace TDAs
{
    public interface IStack<T>
    {
        public void Push(T t);
        public void Pop();
        public T Peek();
        public bool IsEmpty();
    }
}