using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StateMachines
{
   public class DoubleLinkedList<T>
    {
        public DoubleLinkedListNode<T> CurrentNode { get; private set; }
        public DoubleLinkedListNode<T> Add (T node)
        {
            if(CurrentNode == null)
            {
                CurrentNode = new DoubleLinkedListNode<T>(node, null, null);
                First = CurrentNode;
            }
            DoubleLinkedListNode<T> newNode = new DoubleLinkedListNode<T>(node, this.CurrentNode, null);
            CurrentNode.NextNode = newNode;
            CurrentNode = newNode;
            return CurrentNode;
        }
        public DoubleLinkedListNode<T> GotoFirstNode()
        {
            this.CurrentNode = First;
            return CurrentNode;
        }

        public DoubleLinkedListNode<T> First { get; private set; }
        public DoubleLinkedListNode<T> Next
        {
            get
            {
                this.CurrentNode = CurrentNode.NextNode;
                return this.CurrentNode;
            }
        }
        public DoubleLinkedListNode<T> Previous
        {
            get
            {
                this.CurrentNode = CurrentNode.PreviousNode;
                return this.CurrentNode;
            }
        }
    }
}
