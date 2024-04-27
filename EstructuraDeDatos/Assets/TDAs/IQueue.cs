namespace TDAs
{
    public interface IQueue<T>
    {
        void Enqueue(T t);
        void Dequeue(T t);
        T Peek();
        bool IsEmpty();
    }
}