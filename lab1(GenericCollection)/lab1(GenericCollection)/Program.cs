using System;
using System.Collections;
using System.Collections.Generic;

namespace lab1_GenericCollection_
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var s = new LoopSingleLinkList<int>();
            s.AddFirst(1);
            s.AddFirst(2);
            //s.AddFirst(3);
            foreach(var temp in s)
            {
                Console.WriteLine(temp);
            }
            Console.ReadKey();
        }
    }
    public class NodeWithLink<T>
    {
        public T Value { get; set; }
        public NodeWithLink<T> Next { get; set; }
        public NodeWithLink(T value)
        {
            Value = value;
        }
    }
    public class LoopSingleLinkList<T> : IEnumerable<T>
    {
        private NodeWithLink<T> head;
        private NodeWithLink<T> last;
        public int Length { get; private set; } = 0;
        public void AddFirst(T value)
        {
            NodeWithLink<T> node = new NodeWithLink<T>(value);
            if (head == null)
            {
                // зациклим один элемент
                head = node;
                last = node;
                last.Next = node;
            }
            else
            {
                last.Next = node;
                node.Next = head;
                head = node;
            }
            Length++;
        }
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new LoopSingleLinkListEnumerator<T>(head, last);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new LoopSingleLinkListEnumerator<T>(head, last);
        }
    }

    public class LoopSingleLinkListEnumerator<T> : IEnumerator<T>
    {
        private NodeWithLink<T> head;
        private NodeWithLink<T> position;
        int flag = 0;
        public LoopSingleLinkListEnumerator(NodeWithLink<T> head, NodeWithLink<T> last)
        {
            this.head = head;
            this.position = last;
        }
        T IEnumerator<T>.Current
        {
            get
            {
                if (position == null)
                    throw new NullReferenceException();
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
            if (head == null)
                return false;
            if (position.Next != head || flag++ == 0)
            {
                position = position.Next;
                return true;
            }
            return false;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
        public void Dispose()
        {
            Console.WriteLine("nothing");
        }
    }
}
