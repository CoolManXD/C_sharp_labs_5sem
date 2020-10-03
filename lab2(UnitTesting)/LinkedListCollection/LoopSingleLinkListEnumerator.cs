using System;
using System.Collections;
using System.Collections.Generic;

namespace LinkedListCollection
{
    public class LoopSingleLinkListEnumerator<T> : IEnumerator<T>
    {
        private NodeWithLink<T> first;
        private NodeWithLink<T> last;
        private NodeWithLink<T> position;
        int flag = 0; // флаг для начала первой итерации перебора списка
        public LoopSingleLinkListEnumerator(NodeWithLink<T> head, NodeWithLink<T> last)
        {
            this.first = head;
            this.last = last;
            this.position = last;
        }
        T IEnumerator<T>.Current
        {
            get
            {
                if (position == null)
                {
                    throw new Exception("Узел списка равен null");
                    //throw new ArgumentNullException();
                }    
                    
                return position.Value;
            }
        }
        object IEnumerator.Current
        {
            get
            {
                if (position == null)
                    throw new NullReferenceException();
                return position.Value;
            }
        }
        public bool MoveNext()
        {
            if (first == null) // список пуст 
                return false;
            if (position.Next != first || flag++ == 0)
            {
                position = position.Next;
                return true;
            }
            return false;
        }
        public void Reset()
        {
            position = last;
        }
        public void Dispose()
        {
        }
    }
}
