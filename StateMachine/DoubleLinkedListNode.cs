using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachines
{
   public class DoubleLinkedListNode<T>
    {
        public DoubleLinkedListNode(T listNode, DoubleLinkedListNode<T> previousNode, DoubleLinkedListNode<T> nextNode)
        {
            this.ListNode = listNode;
            this.PreviousNode = previousNode;
            this.NextNode = nextNode;
        }
        public T ListNode { get; private set; }
        public DoubleLinkedListNode<T> PreviousNode { get;  set; }
        public DoubleLinkedListNode<T> NextNode { get;  set; }
    }
}
