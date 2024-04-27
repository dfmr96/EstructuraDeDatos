using System;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Serialization;

namespace TDAs
{
    public class NestedStack<T> : IStack<T>
    {
        public ListNode<T> head;
        public List<ListNode<T>> debugList;

        [ContextMenu("Push")]
        public void Push(T t)
        {
            ListNode<T> newNode = new ListNode<T>(t);
            if (!IsEmpty())
            {

                ListNode<T> auxNode = GetLastNode();

                auxNode.next = newNode;
            }
            else
            {
                head = newNode;
            }
            
            debugList.Add(newNode);
            PrintStack();
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

        [ContextMenu("Pop")]
        public void Pop()
        {
            if (IsEmpty()) return;
            
            ListNode<T> auxNode = head;
            ListNode<T> previusNode = null;
            while (auxNode.next != null)
            {
                previusNode = auxNode;
                auxNode = auxNode.next;
            }

            if (previusNode != null) previusNode.next = null;
            debugList.RemoveAt(debugList.Count - 1);
            PrintStack();
        }

        [ContextMenu("Peek")]
        public T Peek()
        {
            ListNode<T> auxNode = IterateFromHead();

            Debug.Log($"{auxNode.value}");
            return auxNode.value;
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