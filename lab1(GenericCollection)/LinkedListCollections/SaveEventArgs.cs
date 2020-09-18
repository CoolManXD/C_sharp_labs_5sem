using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedListCollection
{
    public class SaveEventArgs<T>
    {
        public NodeWithLink<T> Node { get; }
        public object Data { get; }
        public SaveEventArgs(NodeWithLink<T> node, object data)
        {
            Node = node;
            Data = data;
        }
    }
}
