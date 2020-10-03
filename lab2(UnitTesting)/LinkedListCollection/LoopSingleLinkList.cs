using System;
using System.Collections;
using System.Collections.Generic;

namespace LinkedListCollection
{
    // кольцевой односвязный список
    public class LoopSingleLinkList<T> : IEnumerable<T>
    {
        public delegate void SaverHandler(object sender);
        public SaverHandler Save;
 
        public NodeWithLink<T> First { get; private set; }
        public NodeWithLink<T> Last { get; private set; }
        public int Length { get; private set; } = 0;
        public LoopSingleLinkList() { }
        public LoopSingleLinkList(params T[] values)
        {
            foreach(T temp in values )
            {
                AddLast(temp);
            }
            Length = values.Length;
        }
        public LoopSingleLinkList(params NodeWithLink<T>[] values)
        {
            foreach (NodeWithLink<T> temp in values)
            {
                AddLast(temp);
            }
            Length = values.Length;
        }
        public void AddFirst(NodeWithLink<T> node)
        {
            // узел не может быть null 
            if (node == null)
                throw new ArgumentNullException();
            
            if (First == null) // если список пуст
            {            
                First = node;
                Last = node;
                Last.Next = node; // начало = конец 
            }
            else
            {
                Last.Next = node;
                node.Next = First;
                First = node;
            }
            Length++;
        }
        public NodeWithLink<T> AddFirst(T value)
        {
            NodeWithLink<T> newNode = new NodeWithLink<T>(value);
            AddFirst(newNode);
            return newNode;
        }
        public void AddLast(NodeWithLink<T> node)
        {
            // узел не может быть null 
            if (node == null)
                throw new ArgumentNullException();
            
            if (First == null) // если список пуст 
            {
                AddFirst(node);
            }
            else
            {
                Last.Next = node;
                node.Next = First;
                Last = node;
                Length++;
            }         
        }
        public NodeWithLink<T> AddLast(T value)
        {
            NodeWithLink<T> newNode = new NodeWithLink<T>(value);
            AddLast(newNode);
            return newNode;
        }
        public void AddAfter(NodeWithLink<T> node, NodeWithLink<T> newNode)  // вставка узла newNode после индентичного узла node в списке
        {
            // узлы не могут быть null 
            if (node == null || newNode == null)
                throw new ArgumentNullException();

            newNode.Next = node.Next;
            node.Next = newNode;
            
            if (node == Last) // вставка после последнего узла
                Last = newNode;
            Length++;
        }
        public NodeWithLink<T> AddAfter(NodeWithLink<T> node, T value) // вставка узла с значением value после индентичного узла node в списке
        {
            NodeWithLink<T> newNode = new NodeWithLink<T>(value);
            AddAfter(node, newNode);
            return newNode;
        }
        public NodeWithLink<T> Find(T value) // поиск первого вхождения узла с значением value
        {
            if (First == null) // список пуст
                return null;
            NodeWithLink<T> currentNode = First;
            do
            {
                if (currentNode.Value == null) // значение в узле равно null
                {
                    if (value == null)
                        return currentNode;
                }
                else if (currentNode.Value.Equals(value)) // значение в узле равно не null
                    return currentNode;

                currentNode = currentNode.Next;

            } while (currentNode != First); // перебор циклического списка
            return null;
        } 
        public NodeWithLink<T> FindLast(T value) // поиск последнего вхождения узла с значением value
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
        public bool Remove(T value) // удаление первого вхождения узла с значением value
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
            Save?.Invoke(this);
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
