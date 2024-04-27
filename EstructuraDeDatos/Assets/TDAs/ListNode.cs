namespace TDAs
{
    [System.Serializable]
    public class ListNode<T>
    {
        public T value;
        public ListNode<T> next;

        public ListNode(T value)
        {
            this.value = value;
        }
    }
}