using System;
using Xunit;
using LinkedListCollection;
using Xunit.Sdk;

namespace LinkedListCollection.Tests
{
    public abstract class LoopSingleLinkListTests<T> where T: IEquatable<T>
    {
        protected abstract NodeWithLink<T> CreateSampleNode();
        protected abstract NodeWithLink<T>[] CreateSequenceOfFiveNodes();
        // AddFirst
        [Fact]
        public void AddFirst_NodeAddFirstInEmptyList_NodeAdded()
        {
            // Arrange
            LoopSingleLinkList<T> list = new LoopSingleLinkList<T>();
            NodeWithLink<T> first = CreateSampleNode();
            // Act
            list.AddFirst(first);
            // Assert
            Assert.Same(first, list.First);
            Assert.Same(first, list.Last);
        }
        [Fact]
        public void AddFirst_NodeAddFirstInNotEmptyList_NodeAdded()
        {
            // Arrange
            NodeWithLink<T>[] sequence = CreateSequenceOfFiveNodes();
            LoopSingleLinkList<T> list = new LoopSingleLinkList<T>(sequence);
            NodeWithLink<T> first = list.First;
            NodeWithLink<T> newFirst = CreateSampleNode();
            // Act
            list.AddFirst(newFirst);
            // Assert
            Assert.Same(newFirst, list.First);
            Assert.Same(newFirst, list.Last?.Next);
            Assert.Same(first, newFirst?.Next);
        }
        [Fact]
        public void AddFirst_NullNodeAddFirstInList_ArgumentNullExceptionReturned()
        {
            // Arrange
            LoopSingleLinkList<T> list = new LoopSingleLinkList<T>();
            NodeWithLink<T> first = null;
            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => list.AddFirst(first));
        }
        [Fact]
        public void AddFirst_NodeAddFirstInList_LengthUp1()
        {
            // Arrange
            LoopSingleLinkList<T> list = new LoopSingleLinkList<T>();
            NodeWithLink<T> first = CreateSampleNode();
            // Act
            list.AddFirst(first);
            // Assert
            Assert.Equal(1, list.Length);
        }

        // AddLast
        [Fact]
        public void AddLast_NodeAddLastInEmptyList_NodeAdded()
        {
            // Arrange
            LoopSingleLinkList<T> list = new LoopSingleLinkList<T>();
            NodeWithLink<T> last = CreateSampleNode();
            // Act
            list.AddLast(last);
            // Assert
            Assert.Same(last, list.First);
            Assert.Same(last, list.Last);
        }
        [Fact]
        public void AddLast_NodeAddLastInNotEmptyList_NodeAdded()
        {
            // Arrange
            NodeWithLink<T>[] sequence = CreateSequenceOfFiveNodes();
            LoopSingleLinkList<T> list = new LoopSingleLinkList<T>(sequence);
            NodeWithLink<T> last = list.Last;
            NodeWithLink<T> newLast = CreateSampleNode();
            // Act
            list.AddLast(newLast);
            // Assert
            Assert.Same(newLast, list.Last);
            Assert.Same(newLast, last.Next);
            Assert.Same(list.First, newLast?.Next);
        }
        [Fact]
        public void AddLast_NullNodeAddLastInList_ArgumentNullExceptionReturned()
        {
            // Arrange
            LoopSingleLinkList<T> list = new LoopSingleLinkList<T>();
            NodeWithLink<T> last = null;
            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => list.AddLast(last));
        }
        [Fact]
        public void AddLast_NodeAddLastInList_LengthUp1()
        {
            // Arrange
            LoopSingleLinkList<T> list = new LoopSingleLinkList<T>();
            NodeWithLink<T> first = CreateSampleNode();
            // Act
            list.AddLast(first);
            // Assert
            Assert.Equal(1, list.Length);
        }

        // AddAfter
        [Fact]
        public void AddAfter_NodeAddAfterSomeNodeInMiddleInList_NodeAdded()
        {
            // Arrange
            NodeWithLink<T>[] sequence = CreateSequenceOfFiveNodes();
            LoopSingleLinkList<T> list = new LoopSingleLinkList<T>(sequence);
            NodeWithLink<T> node1 = sequence[0];
            NodeWithLink<T> node2 = sequence[1];
            NodeWithLink<T> node0 = CreateSampleNode();
            // Act
            list.AddAfter(node1, node0);
            // Assert
            Assert.Same(node0, node1?.Next);
            Assert.Same(node2, node0?.Next);
        }
        [Fact]
        public void AddAfter_NodeAddAfterLastNodeInList_NodeAdded()
        {
            // Arrange
            NodeWithLink<T>[] sequence = CreateSequenceOfFiveNodes();
            LoopSingleLinkList<T> list = new LoopSingleLinkList<T>(sequence);
            NodeWithLink<T> node1 = sequence[0];
            NodeWithLink<T> node5 = sequence[sequence.Length - 1];
            NodeWithLink<T> node0 = CreateSampleNode();
            // Act
            list.AddAfter(node5, node0);
            // Assert
            Assert.Same(node0, list.Last);
            Assert.Same(node0, node5?.Next);
            Assert.Same(node1, node0?.Next);
        }
        [Fact]
        public void AddAfter_AddAfterWithAnyNullArgumentsNode_ArgumentNullExceptionReturned()
        {
            // Arrange
            NodeWithLink<T> node1 = CreateSampleNode();
            LoopSingleLinkList<T> list = new LoopSingleLinkList<T>(node1);
            NodeWithLink<T> node2 = null;
            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => list.AddAfter(node1, node2));
            Assert.Throws<ArgumentNullException>(() => list.AddAfter(node2, node1));
        }
        [Fact]
        public void AddAfter_NodeAddAfterInList_LengthUp1()
        {
            // Arrange
            NodeWithLink<T>[] sequence = CreateSequenceOfFiveNodes();
            LoopSingleLinkList<T> list = new LoopSingleLinkList<T>(sequence);
            NodeWithLink<T> node1 = sequence[0];
            NodeWithLink<T> node0 = CreateSampleNode();
            // Act
            list.AddAfter(node1, node0);
            // Assert
            Assert.Equal(sequence.Length + 1, list.Length);
        }

        // Find
        [Fact]
        public void Find_FindExistNodeWithSpecificValue_NotNullNodeReturned()
        {
            // Arrange
            NodeWithLink<T>[] sequence = CreateSequenceOfFiveNodes();
            LoopSingleLinkList<T> list = new LoopSingleLinkList<T>(sequence);
            T existValue = sequence[1].Value;
            // Act
            NodeWithLink<T> found = list.Find(existValue);
            // Assert
            Assert.NotNull(found);
        }
        [Fact]
        public void Find_FindNotExistNodeWithSpecificValue_NullNodeReturned()
        {
            // Arrange
            NodeWithLink<T>[] sequence = CreateSequenceOfFiveNodes();
            LoopSingleLinkList<T> list = new LoopSingleLinkList<T>(sequence);
            T notExistValue = CreateSampleNode().Value;
            // Act
            NodeWithLink<T> found = list.Find(notExistValue);
            // Assert
            Assert.Null(found);
        }

        // FindLast
        [Fact]
        public void FindLast_FindLastExistNodeWithSpecificValue_NotNullNodeReturned()
        {
            // Arrange
            NodeWithLink<T>[] sequence = CreateSequenceOfFiveNodes();
            NodeWithLink<T>[] newSequence = new NodeWithLink<T>[sequence.Length + 1];
            sequence.CopyTo(newSequence, 0);

            NodeWithLink<T> seekNode = sequence[0];
            newSequence[newSequence.Length - 1] = seekNode;

            LoopSingleLinkList<T> list = new LoopSingleLinkList<T>(newSequence);

            // Act
            NodeWithLink<T> found = list.FindLast(seekNode.Value);
            // Assert
            Assert.Same(seekNode, found);
        }
        [Fact]
        public void FindLast_FindLastNotExistNodeWithSpecificValue_NullNodeReturned()
        {
            // Arrange
            NodeWithLink<T>[] sequence = CreateSequenceOfFiveNodes();
            LoopSingleLinkList<T> list = new LoopSingleLinkList<T>(sequence);
            T notExistValue = CreateSampleNode().Value;
            // Act
            NodeWithLink<T> found = list.FindLast(notExistValue);
            // Assert
            Assert.Null(found);
        }

        // Contains
        [Fact]
        public void Contains_ContainsExistNodeWithSpecificValue_TrueReturned()
        {
            // Arrange
            // Arrange
            NodeWithLink<T>[] sequence = CreateSequenceOfFiveNodes();
            LoopSingleLinkList<T> list = new LoopSingleLinkList<T>(sequence);
            T existValue = sequence[1].Value;
            // Act
            bool isFound = list.Contains(existValue);
            // Assert
            Assert.True(isFound);
        }
        [Fact]
        public void Contains_ContainsNotExistNodeWithSpecificValue_TrueReturned()
        {
            // Arrange
            NodeWithLink<T>[] sequence = CreateSequenceOfFiveNodes();
            LoopSingleLinkList<T> list = new LoopSingleLinkList<T>(sequence);
            T notExistValue = CreateSampleNode().Value;
            // Act
            bool isFound = list.Contains(notExistValue);
            // Assert
            Assert.False(isFound);
        }

        // Remove
        [Fact]
        public void Remove_RemoveExistNodeFromListWithOneNode_TrueReturned()
        {
            // Arrange   
            NodeWithLink<T> first = CreateSampleNode();
            LoopSingleLinkList<T> list = new LoopSingleLinkList<T>(first);
            // Act
            bool isDeleted = list.Remove(first.Value);
            // Assert
            Assert.True(isDeleted);
            Assert.Null(list.First);
            Assert.Null(list.Last);
        }
        [Fact]
        public void Remove_RemoveExistMiddleNodeFromListValueType_TrueReturned()
        {
            // Arrange
            NodeWithLink<T>[] sequence = CreateSequenceOfFiveNodes();
            NodeWithLink<T> node1 = sequence[0];
            NodeWithLink<T> node2 = sequence[1];
            NodeWithLink<T> node3 = sequence[2];
            LoopSingleLinkList<T> list = new LoopSingleLinkList<T>(sequence);
            T removeValue = node2.Value;
            // Act
            bool isDeleted = list.Remove(removeValue);
            // Assert
            Assert.True(isDeleted);
            Assert.Same(node3, node1?.Next);
        }
        [Fact]
        public void Remove_RemoveFirstNodeFromList_TrueReturned()
        {
            // Arrange         
            NodeWithLink<T>[] sequence = CreateSequenceOfFiveNodes();
            NodeWithLink<T> node1 = sequence[0];
            NodeWithLink<T> node2 = sequence[1];
            LoopSingleLinkList<T> list = new LoopSingleLinkList<T>(sequence);
            T removeValue = node1.Value;
            // Act
            bool isDeleted = list.Remove(removeValue);
            // Assert
            Assert.True(isDeleted);
            Assert.Same(node2, list.First);
            Assert.Same(node2, list.Last?.Next);
        }
        [Fact]
        public void Remove_RemoveLastNodeFromList_TrueReturned()
        {
            // Arrange         
            NodeWithLink<T>[] sequence = CreateSequenceOfFiveNodes();
            NodeWithLink<T> node4 = sequence[sequence.Length - 2];
            NodeWithLink<T> node5 = sequence[sequence.Length - 1];
            LoopSingleLinkList<T> list = new LoopSingleLinkList<T>(sequence);
            T removeValue = node5.Value;
            // Act
            bool isDeleted = list.Remove(removeValue);
            // Assert
            Assert.True(isDeleted);
            Assert.Same(node4, list.Last);
            Assert.Same(list.First, node4?.Next);
        }
        [Fact]
        public void Remove_RemoveNotExistNodeFromList_FalseReturned()
        {
            // Arrange         
            NodeWithLink<T>[] sequence = CreateSequenceOfFiveNodes();
            LoopSingleLinkList<T> list = new LoopSingleLinkList<T>(sequence);
            T removeValue = CreateSampleNode().Value;
            // Act
            bool isDeleted = list.Remove(removeValue);
            // Assert
            Assert.False(isDeleted);
        }      
        [Fact]
        public void Remove_RemoveExistMiddleNodeFromList_LengthDown1()
        {
            // Arrange
            NodeWithLink<T>[] sequence = CreateSequenceOfFiveNodes();
            NodeWithLink<T> node1 = sequence[0];
            NodeWithLink<T> node2 = sequence[1];
            NodeWithLink<T> node3 = sequence[2];
            LoopSingleLinkList<T> list = new LoopSingleLinkList<T>(sequence);
            T removeValue = node2.Value;
            // Act
            list.Remove(removeValue);
            // Assert
            Assert.Equal(sequence.Length - 1, list.Length);
        }

        // Clear 
        [Fact]
        public void Clear_RemoveAllNodesFromList_NodesDeleted()
        {
            // Arrange         
            NodeWithLink<T>[] sequence = CreateSequenceOfFiveNodes();
            LoopSingleLinkList<T> list = new LoopSingleLinkList<T>(sequence);
            // Act
            list.Clear();
            // Assert
            Assert.Equal(0, list.Length);
            Assert.Null(list.First);
            Assert.Null(list.Last);
        }

        // GetEnumerator/foreach
        [Fact]
        public void GetEnumerator_ForeachWithCorrectSequence_CorrectIterations()
        {
            // Arrange
            NodeWithLink<T>[] sequence = CreateSequenceOfFiveNodes();
            LoopSingleLinkList<T> list = new LoopSingleLinkList<T>(sequence);
            int i = 0;
            // Act/Assert
            foreach (var temp in list)
            {
                Assert.Equal(sequence[i].Value, temp);
                i++;
            }
        }
        [Fact]
        public void GetEnumerator_ForeachSequenceWithNullNode_ArgumentNullExceptionReturned()
        {
            // Arrange
            NodeWithLink<T>[] sequence = CreateSequenceOfFiveNodes();
            LoopSingleLinkList<T> list = new LoopSingleLinkList<T>(sequence);
            list.First.Next = null;
            // Act/Assert
            Assert.Throws<ArgumentNullException>(() => {
                foreach (var temp in list) { }       
            });           
        }

    }
    
}
