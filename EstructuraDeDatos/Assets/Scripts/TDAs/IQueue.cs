namespace TDAs
{
    public interface IQueue<T>
    {
        void Enqueue(T t);
        void Dequeue();
        T Peek();
        bool IsEmpty();
    }
}