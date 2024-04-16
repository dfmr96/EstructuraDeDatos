using System;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

namespace TDAs
{
    public class NestedStack : MonoBehaviour, IStack
    {
        public ListNode head;
        public List<ListNode> list;

        [ContextMenu("Push")]
        public void Push()
        {
            int i = UnityEngine.Random.Range(0, 5);

            ListNode newNode = new ListNode(i);
            if (!isEmpty())
            {

                ListNode auxNode = GetLastNode();

                auxNode.next = newNode;
            }
            else
            {
                head = newNode;
            }
            
            list.Add(newNode);
            PrintStack();
        }

        private ListNode IterateFromHead()
        {
            ListNode auxNode = head;
            while (auxNode.next != null)
            {
                auxNode = auxNode.next;
            }

            return auxNode;
        }

        private ListNode GetLastNode()
        {
            return IterateFromHead();
        }

        [ContextMenu("Pop")]
        public void Pop()
        {
            if (isEmpty()) return;
            
            ListNode auxNode = head;
            ListNode previusNode = null;
            while (auxNode.next != null)
            {
                previusNode = auxNode;
                auxNode = auxNode.next;
            }

            if (previusNode != null) previusNode.next = null;
            list.RemoveAt(list.Count - 1);
            PrintStack();
        }

        [ContextMenu("Peek")]
        public int Peek()
        {
            ListNode auxNode = IterateFromHead();

            Debug.Log($"{auxNode.value}");
            return auxNode.value;
        }

        public bool isEmpty()
        {
            return head == null;
        }

        public void PrintStack()
        {
            ListNode auxNode = head;

            while (auxNode.next != null)
            {
                Debug.Log($"{auxNode.value}");
                auxNode = auxNode.next;
            }
        }
    }
}