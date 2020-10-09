using System;
using Xunit;
using LinkedListCollection;
using Xunit.Sdk;

namespace LinkedListCollection.Tests
{
    public class LoopSingleLinkListReferenceTypeTests: LoopSingleLinkListTests<LoopSingleLinkListReferenceTypeTests.EquatableMock>
    {
        // Find
        [Fact]
        public void Find_FindExistNodeWithNullValue_NotNullNodeReturned()
        {
            // Arrange
            NodeWithLink<EquatableMock>[] sequence = CreateSequenceOfFiveNodes();
            EquatableMock existValue = null;
            LoopSingleLinkList<EquatableMock> list = new LoopSingleLinkList<EquatableMock>(sequence);
            list.AddLast(existValue);           
            // Act
            NodeWithLink<EquatableMock> found = list.Find(existValue);
            // Assert
            Assert.NotNull(found);
        }
        // FindLast
        [Fact]
        public void FindLast_FindLastExistNodeWithNullValue_NotNullNodeReturned()
        {
            // Arrange
            NodeWithLink<EquatableMock>[] sequence = CreateSequenceOfFiveNodes();
            NodeWithLink<EquatableMock> existValue = new NodeWithLink<EquatableMock>(null);
            LoopSingleLinkList<EquatableMock> list = new LoopSingleLinkList<EquatableMock>(sequence);

            list.AddFirst(existValue.Value);
            list.AddLast(existValue);

            // Act
            NodeWithLink<EquatableMock> found = list.FindLast(existValue.Value);
            // Assert
            Assert.Same(existValue, found);
        }
        // Contains
        [Fact]
        public void Contains_ContainsExistNodeWithNullValue_TrueReturned()
        {
            // Arrange
            NodeWithLink<EquatableMock>[] sequence = CreateSequenceOfFiveNodes();
            EquatableMock existValue = null;
            LoopSingleLinkList<EquatableMock> list = new LoopSingleLinkList<EquatableMock>(sequence);
            list.AddLast(existValue);
            // Act
            bool isFound = list.Contains(existValue);
            // Assert
            Assert.True(isFound);
        }
        // Remove
        [Fact]
        public void Remove_RemoveExistMiddleNodeWithNullValueFromList_TrueReturned()
        {
            // Arrange
            NodeWithLink<EquatableMock>[] sequence = CreateSequenceOfFiveNodes();
            NodeWithLink<EquatableMock> node1 = sequence[0];
            NodeWithLink<EquatableMock> node3 = sequence[2];
            NodeWithLink<EquatableMock> node2 = new NodeWithLink<EquatableMock>(null);
            sequence[1] = node2;
            LoopSingleLinkList<EquatableMock> list = new LoopSingleLinkList<EquatableMock>(sequence);
            EquatableMock removeValue = node2.Value;
            // Act
            bool isDeleted = list.Remove(removeValue);
            // Assert
            Assert.True(isDeleted);
            Assert.Same(node3, node1?.Next);
        }
        [Fact]
        public void Remove_RemoveExistNodeWithNullValueFromListWithOneNode_TrueReturned()
        {
            // Arrange
            NodeWithLink<EquatableMock> node0 = new NodeWithLink<EquatableMock>(null);
            LoopSingleLinkList<EquatableMock> list = new LoopSingleLinkList<EquatableMock>(node0);
            EquatableMock removeValue = null;
            // Act
            bool isDeleted = list.Remove(removeValue);
            // Assert
            Assert.True(isDeleted);
            Assert.Null(list.First);
            Assert.Null(list.Last);
        }

        protected override NodeWithLink<EquatableMock> CreateSampleNode()
        {
            return new NodeWithLink<EquatableMock>(new EquatableMock(0));
        }

        protected override NodeWithLink<EquatableMock>[] CreateSequenceOfFiveNodes()
        {
            NodeWithLink<EquatableMock> node1 = new NodeWithLink<EquatableMock>(new EquatableMock(1));
            NodeWithLink<EquatableMock> node2 = new NodeWithLink<EquatableMock>(new EquatableMock(2));
            NodeWithLink<EquatableMock> node3 = new NodeWithLink<EquatableMock>(new EquatableMock(3));
            NodeWithLink<EquatableMock> node4 = new NodeWithLink<EquatableMock>(new EquatableMock(4));
            NodeWithLink<EquatableMock> node5 = new NodeWithLink<EquatableMock>(new EquatableMock(5));
            return new NodeWithLink<EquatableMock>[5] { node1, node2, node3, node4, node5 };
        }
        public class EquatableMock
        {
            public int Id { get; }
            public EquatableMock(int id)
            {
                Id = id;
            }
            public override bool Equals(object obj)
            {
                if (obj == null)
                    return false;
                EquatableMock temp = obj as EquatableMock;
                if (temp == null)
                    return false;
                return this.Id == temp.Id;
            }
            public override int GetHashCode()
            {
                return Id;
            }
        }
    }
}
