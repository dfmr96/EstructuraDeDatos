using System;
using System.Collections.Generic;
using UnityEngine;

namespace TDAs
{
    [Serializable]
    public class NestedStack<T> : IStack<T>
    {
        public ListNode<T> head;
        public List<ListNode<T>> debugList = new List<ListNode<T>>();
        
        public void Push(T t)
        {
            ListNode<T> newNode = new ListNode<T>(t);
            if (!IsEmpty())
            {

                ListNode<T> auxNode = head;
                head = newNode;
                head.next = auxNode;
            }
            else
            {
                head = newNode;
            }
            debugList.Insert(0,newNode);
            //PrintStack();
        }

        private ListNode<T> IterateFromHead()
        {
            ListNode<T> auxNode = head;
            while (auxNode.next != null)
            {
                auxNode = auxNode.next;
            }

            return auxNode;
        }

        private ListNode<T> GetLastNode()
        {
            return IterateFromHead();
        }
        
        public void Pop()
        {
            head = head.next;
            if (debugList.Count > 0) debugList.RemoveAt(0);
            //PrintStack();
        }

        public T Peek()
        {
            return head.value;
        }

        public bool IsEmpty()
        {
            return head == null;
        }

        public void PrintStack()
        {
            ListNode<T> auxNode = head;

            while (auxNode.next != null)
            {
                Debug.Log($"{auxNode.value}");
                auxNode = auxNode.next;
            }
        }
    }
}