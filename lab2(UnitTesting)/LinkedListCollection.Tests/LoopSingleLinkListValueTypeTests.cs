using System;
using System.Collections.Generic;
using System.Text;

namespace LinkedListCollection.Tests
{
    public class LoopSingleLinkListValueTypeTests : LoopSingleLinkListTests<int>
    {
        protected override NodeWithLink<int> CreateSampleNode()
        {
            return new NodeWithLink<int>(0);
        }

        protected override NodeWithLink<int>[] CreateSequenceOfFiveNodes()
        {
            NodeWithLink<int> node1 = new NodeWithLink<int>(1);
            NodeWithLink<int> node2 = new NodeWithLink<int>(2);
            NodeWithLink<int> node3 = new NodeWithLink<int>(3);
            NodeWithLink<int> node4 = new NodeWithLink<int>(4);
            NodeWithLink<int> node5 = new NodeWithLink<int>(5);
            return new NodeWithLink<int>[5] { node1, node2, node3, node4, node5 };
        }
    }
}
