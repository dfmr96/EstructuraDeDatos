using System.Collections.Generic;
using UnityEngine;

namespace TDAs
{
    public class NestedQueueTDA<T> : IQueue<T>
    {
        public ListNode<T> head;
        public List<ListNode<T>> debugList;
        
        public void Enqueue(T t)
        {
            ListNode<T> newNode = new ListNode<T>(t);

            ListNode<T> auxNode = head;
            head = newNode;
            head.next = auxNode;
            
            debugList.Insert(0,newNode);
        }

        public void Dequeue()
        {
            head = head.next;
            debugList.RemoveAt(0);
        }

        public T Peek()
        {
            return head.value;
        }

        public bool IsEmpty()
        {
            if (head == null) return true;
            return false;
        }

        public void Initialize(ListNode<T> t)
        {
            head = t;
        }
    }
}