

namespace LinkedListCollection
{
    public class NodeWithLink<T>
    {
        public T Value { get; set; }
        public NodeWithLink<T> Next { get; set; }
        public NodeWithLink(T value)
        {
            Value = value;
        }
    }
}
