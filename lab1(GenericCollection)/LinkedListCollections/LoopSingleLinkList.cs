using System;
using System.Collections;
using System.Collections.Generic;

namespace LinkedListCollection
{
    public class LoopSingleLinkList<T> : IEnumerable<T>
    {
        public NodeWithLink<T> First { get; private set; }
        public NodeWithLink<T> Last { get; private set; }
        public int Length { get; private set; } = 0;
        public NodeWithLink<T> AddFirst(NodeWithLink<T> node)
        {
            if (node == null)
                throw new ArgumentNullException();
            if (First == null)
            {
                // зациклим один элемент
                First = node;
                Last = node;
                Last.Next = node;
            }
            else
            {
                Last.Next = node;
                node.Next = First;
                First = node;
            }
            Length++;
            return First;
        }
        public NodeWithLink<T> AddFirst(T value)
        {
            return AddFirst(new NodeWithLink<T>(value));
        }
        public NodeWithLink<T> AddLast(NodeWithLink<T> node)
        {
            if (node == null)
                throw new ArgumentNullException();
            if (First == null)
            {
                AddFirst(node);
            }
            else
            {
                Last.Next = node;
                node.Next = First;
                Last = node;
            }
            Length++;
            return Last;
        }
        public NodeWithLink<T> AddLast(T value)
        {
            return AddLast(new NodeWithLink<T>(value));
        }
        public void AddAfter(NodeWithLink<T> node, NodeWithLink<T> newNode)
        {
            if (node == null || newNode == null)
                throw new ArgumentNullException();
            newNode.Next = node.Next;
            node.Next = newNode;
            if (node == Last)
                Last = newNode;
            Length++;
        }
        public NodeWithLink<T> AddAfter(NodeWithLink<T> node, T value)
        {
            NodeWithLink<T> newNode = new NodeWithLink<T>(value);
            AddAfter(node, newNode);
            return newNode;
        }
        public NodeWithLink<T> Find(T value)
        {
            if (First == null)
                return null;
            NodeWithLink<T> currentNode = First;
            do
            {
                if (currentNode.Value == null)
                {
                    if (value == null)
                        return currentNode;
                }
                else if (currentNode.Value.Equals(value))
                    return currentNode;

                currentNode = currentNode.Next;

            } while (currentNode != First);
            return null;
        }
        public NodeWithLink<T> FindLast(T value)
        {
            if (First == null)
                return null;
            NodeWithLink<T> currentNode = First;
            NodeWithLink<T> foundNode = null;
            do
            {
                if (currentNode.Value == null)
                {
                    if (value == null)
                        foundNode = currentNode;
                }
                else if (currentNode.Value.Equals(value))
                    foundNode = currentNode;

                currentNode = currentNode.Next;

            } while (currentNode != First);
            return foundNode;
        }
        public bool Contains(T value)
        {
            if (Find(value) != null)
                return true;
            return false;
        }
        public bool Remove(T value)
        {
            if (First == null)
                return false;

            NodeWithLink<T> currentNode = First;
            NodeWithLink<T> previousNode = null;

            do
            {
                if (currentNode.Value == null)
                {
                    if (value == null)
                    {
                        // Если узел в середине или в конце
                        if (previousNode != null)
                        {
                            // убираем узел current, теперь previous ссылается не на current, а на current.Next
                            previousNode.Next = currentNode.Next;

                            // Если узел последний,
                            // изменяем переменную tail
                            if (currentNode == Last)
                                Last = previousNode;
                        }
                        else // если удаляется первый элемент
                        {
                            // если в списке всего один элемент
                            if (Length == 1)
                            {
                                First = null;
                                Last = null;
                            }
                            else
                            {
                                First = currentNode.Next;
                                Last.Next = First;
                            }
                        }
                        Length--;
                        return true;
                    }
                }
                else if (currentNode.Value.Equals(value))
                {
                    // Если узел в середине или в конце
                    if (previousNode != null)
                    {
                        // убираем узел current, теперь previous ссылается не на current, а на current.Next
                        previousNode.Next = currentNode.Next;

                        // Если узел последний,
                        // изменяем переменную tail
                        if (currentNode == Last)
                            Last = previousNode;
                    }
                    else // если удаляется первый элемент
                    {
                        // если в списке всего один элемент
                        if (Length == 1)
                        {
                            First = null;
                            Last = null;
                        }
                        else
                        {
                            First = currentNode.Next;
                            Last.Next = First;
                        }
                    }
                    Length--;
                    return true;
                }
                previousNode = currentNode;
                currentNode = currentNode.Next;

            } while (currentNode != First);

            return false;
        }
        public void Clear()
        {
            First = null;
            Last = null;
            Length = 0;
        }
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new LoopSingleLinkListEnumerator<T>(First, Last);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new LoopSingleLinkListEnumerator<T>(First, Last);
        }
    }
}
