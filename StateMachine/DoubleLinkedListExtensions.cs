using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachines
{
    public static class DoubleLinkedListExtensions
    {
        public static DoubleLinkedListNode<T> GetLastNode<T>(this DoubleLinkedList<T> list)
        {
            DoubleLinkedListNode<T> cur = list.First;
            do
            {
                cur = cur.NextNode;
            } while (cur.NextNode != null);
            return cur;
        }

        public static DoubleLinkedListNode<T> GetNodeAt<T>(this DoubleLinkedList<T> list, int position, DoubleLinkedListNode<T> from)
        {
            if (position > 0)
            {
                from = from.NextNode;
                return GetNodeAt(list, position - 1, from);
            }
            else
            {
                return from;
            }
        }

    }
}
