namespace TDAs.Set
{
    public interface ISet<T>

    {
    void Init(int size);
    bool IsEmpty();
    void Add(T t);
    T Choose();
    void Remove(int t);
    bool Contains(int t);
    }
}
