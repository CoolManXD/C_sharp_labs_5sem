using System;
using Xunit;
using LinkedListCollection;
using Xunit.Sdk;

namespace LinkedListCollection.Tests
{
    public class LoopSingleLinkListTests
    {
        // AddFirst
        [Fact]
        public void AddFirst_NodeWithValueTypeElementAddFirstInEmptyList_NodeAdded()
        {
            // Arrange
            LoopSingleLinkList<int> list = new LoopSingleLinkList<int>();
            NodeWithLink<int> first = new NodeWithLink<int>(1);
            // Act
            list.AddFirst(first);
            // Assert
            Assert.Same(first, list.First);
            Assert.Same(first, list.Last);
        }
        [Fact]
        public void AddFirst_NodeWithValueTypeElementAddFirstInNotEmptyList_NodeAdded()
        {
            // Arrange
            LoopSingleLinkList<int> list = new LoopSingleLinkList<int>(1, 2);
            NodeWithLink<int> first = list.First;
            NodeWithLink<int> newFirst = new NodeWithLink<int>(3);
            // Act
            list.AddFirst(newFirst);
            // Assert
            Assert.Same(newFirst, list.First);
            Assert.Same(newFirst, list.Last?.Next);
            Assert.Same(first, newFirst?.Next);
        }
        [Fact]
        public void AddFirst_NodeWithReferenceTypeElementAddFirstInEmptyList_NodeAdded()
        {
            // Arrange
            LoopSingleLinkList<Person> list = new LoopSingleLinkList<Person>();
            NodeWithLink<Person> first = new NodeWithLink<Person>(new Person("s1","y1"));
            // Act
            list.AddFirst(first);
            // Assert
            Assert.Same(first, list.First);
            Assert.Same(first, list.Last);
        }
        [Fact]
        public void AddFirst_NodeWithReferenceTypeElementAddFirstInNotEmptyList_NodeAdded()
        {
            // Arrange
            LoopSingleLinkList<Person> list = new LoopSingleLinkList<Person>(new Person("s1", "y1"), new Person("s2", "y2"));
            NodeWithLink<Person> first = list.First;
            NodeWithLink<Person> newFirst = new NodeWithLink<Person>(new Person("s3", "y3"));
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
            LoopSingleLinkList<int> list = new LoopSingleLinkList<int>();
            NodeWithLink<int> first = null;
            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => list.AddFirst(first));
        }
        [Fact]
        public void AddFirst_NodeAddFirstInList_LengthUp1()
        {
            // Arrange
            LoopSingleLinkList<int> list = new LoopSingleLinkList<int>();
            NodeWithLink<int> first = new NodeWithLink<int>(1);
            // Act
            list.AddFirst(first);
            // Assert
            Assert.Equal(1, list.Length);
        }

        // AddLast
        [Fact]
        public void AddLast_NodeWithValueTypeElementAddLastInEmptyList_NodeAdded()
        {
            // Arrange
            LoopSingleLinkList<int> list = new LoopSingleLinkList<int>();
            NodeWithLink<int> last = new NodeWithLink<int>(1);
            // Act
            list.AddLast(last);
            // Assert
            Assert.Same(last, list.First);
            Assert.Same(last, list.Last);
        }
        [Fact]
        public void AddLast_NodeWithValueTypeElementAddLastInNotEmptyList_NodeAdded()
        {
            // Arrange
            LoopSingleLinkList<int> list = new LoopSingleLinkList<int>(1, 2);
            NodeWithLink<int> last = list.Last;
            NodeWithLink<int> newLast = new NodeWithLink<int>(3);
            // Act
            list.AddLast(newLast);
            // Assert
            Assert.Same(newLast, list.Last);
            Assert.Same(newLast, last.Next);
            Assert.Same(list.First, newLast?.Next);
        }
        [Fact]
        public void AddLast_NodeWithReferenceTypeElementAddLastInEmptyList_NodeAdded()
        {
            // Arrange
            LoopSingleLinkList<Person> list = new LoopSingleLinkList<Person>();
            NodeWithLink<Person> last = new NodeWithLink<Person>(new Person("s1", "y1"));
            // Act
            list.AddLast(last);
            // Assert
            Assert.Same(last, list.First);
            Assert.Same(last, list.Last);
        }
        [Fact]
        public void AddLast_NodeWithReferenceTypeElementAddLastInNotEmptyList_NodeAdded()
        {
            // Arrange
            LoopSingleLinkList<Person> list = new LoopSingleLinkList<Person>(new Person("s1", "y1"), new Person("s2", "y2"));
            NodeWithLink<Person> last = list.Last;
            NodeWithLink<Person> newLast = new NodeWithLink<Person>(new Person("s3", "y3"));
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
            LoopSingleLinkList<int> list = new LoopSingleLinkList<int>();
            NodeWithLink<int> last = null;
            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => list.AddLast(last));
        }
        [Fact]
        public void AddLast_NodeAddLastInList_LengthUp1()
        {
            // Arrange
            LoopSingleLinkList<int> list = new LoopSingleLinkList<int>();
            NodeWithLink<int> first = new NodeWithLink<int>(1);
            // Act
            list.AddLast(first);
            // Assert
            Assert.Equal(1, list.Length);
        }

        // AddAfter
        [Fact]
        public void AddAfter_NodeWithValueTypeElementAddAfterSomeNodeInList_NodeAdded()
        {
            // Arrange
            NodeWithLink<int> node1 = new NodeWithLink<int>(1);
            NodeWithLink<int> node2 = new NodeWithLink<int>(2);
            NodeWithLink<int> node3 = new NodeWithLink<int>(3);
            LoopSingleLinkList<int> list = new LoopSingleLinkList<int>(node1, node2, node3);
            NodeWithLink<int> node4 = new NodeWithLink<int>(4);
            // Act
            list.AddAfter(node1, node4);
            // Assert
            Assert.Same(node4, node1?.Next);
            Assert.Same(node2, node4?.Next);
        }
        [Fact]
        public void AddAfter_NodeWithReferenceTypeElementAddAfterSomeNodeInList_NodeAdded()
        {
            // Arrange
            NodeWithLink<Person> node1 = new NodeWithLink<Person>(new Person("s1","y1"));
            NodeWithLink<Person> node2 = new NodeWithLink<Person>(new Person("s2", "y2"));
            NodeWithLink<Person> node3 = new NodeWithLink<Person>(new Person("s3", "y3"));
            LoopSingleLinkList<Person> list = new LoopSingleLinkList<Person>(node1, node2, node3);
            NodeWithLink<Person> node4 = new NodeWithLink<Person>(new Person("s4", "y4"));
            // Act
            list.AddAfter(node1, node4);
            // Assert
            Assert.Same(node4, node1?.Next);
            Assert.Same(node2, node4?.Next);
        }
        [Fact]
        public void AddAfter_NodeAddAfterLastNodeInList_NodeAdded()
        {
            // Arrange
            NodeWithLink<int> node1 = new NodeWithLink<int>(1);
            NodeWithLink<int> node2 = new NodeWithLink<int>(2);
            NodeWithLink<int> node3 = new NodeWithLink<int>(3);
            LoopSingleLinkList<int> list = new LoopSingleLinkList<int>(node1, node2, node3);
            NodeWithLink<int> node4 = new NodeWithLink<int>(4);
            // Act
            list.AddAfter(node3, node4);
            // Assert
            Assert.Same(node4, list.Last);
            Assert.Same(node4, node3?.Next);
            Assert.Same(node1, node4?.Next);
        }
        [Fact]
        public void AddAfter_AddAfterWithAnyNullArgumentsNode_ArgumentNullExceptionReturned()
        {
            // Arrange
            NodeWithLink<int> node1 = new NodeWithLink<int>(1);
            LoopSingleLinkList<int> list = new LoopSingleLinkList<int>(node1);
            NodeWithLink<int> node2 = null;
            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => list.AddAfter(node1, node2));
            Assert.Throws<ArgumentNullException>(() => list.AddAfter(node2, node1));
        }
        [Fact]
        public void AddAfter_NodeAddFirstInList_LengthUp1()
        {
            // Arrange
            NodeWithLink<int> node1 = new NodeWithLink<int>(1);
            NodeWithLink<int> node2 = new NodeWithLink<int>(2);
            NodeWithLink<int> node3 = new NodeWithLink<int>(3);
            LoopSingleLinkList<int> list = new LoopSingleLinkList<int>(node1, node2, node3);
            NodeWithLink<int> node4 = new NodeWithLink<int>(4);
            // Act
            list.AddAfter(node1, node4);
            // Assert
            Assert.Equal(4, list.Length);
        }

        // Find
        [Fact]
        public void Find_FindExistNodeWithSpecificValueType_NotNullNodeReturned()
        {
            // Arrange
            LoopSingleLinkList<int> list = new LoopSingleLinkList<int>(1, 2, 3, 4, 5);
            // Act
            NodeWithLink<int> found = list.Find(4);
            // Assert
            Assert.NotNull(found);
        }
        [Fact]
        public void Find_FindNotExistNodeWithSpecificValueType_NullNodeReturned()
        {
            // Arrange
            LoopSingleLinkList<int> list = new LoopSingleLinkList<int>(1, 2, 3, 4, 5);
            // Act
            NodeWithLink<int> found = list.Find(0);
            // Assert
            Assert.Null(found);
        }
        [Fact]
        public void Find_FindExistNodeWithSpecificReferenceType_NotNullNodeReturned()
        {
            // Arrange
            NodeWithLink<Person> node1 = new NodeWithLink<Person>(new Person("s1", "y1"));
            NodeWithLink<Person> node2 = new NodeWithLink<Person>(new Person("s2", "y2"));
            NodeWithLink<Person> node3 = new NodeWithLink<Person>(new Person("s3", "y3"));
            LoopSingleLinkList<Person> list = new LoopSingleLinkList<Person>(node1, node2, node3);
            Person temp = new Person("s2", "y2");
            // Act
            NodeWithLink<Person> found = list.Find(temp);
            // Assert
            Assert.NotNull(found);
        }
        [Fact]
        public void Find_FindNotExistNodeWithSpecificReferenceType_NullNodeReturned()
        {
            // Arrange
            NodeWithLink<Person> node1 = new NodeWithLink<Person>(new Person("s1", "y1"));
            NodeWithLink<Person> node2 = new NodeWithLink<Person>(new Person("s2", "y2"));
            NodeWithLink<Person> node3 = new NodeWithLink<Person>(new Person("s3", "y3"));
            LoopSingleLinkList<Person> list = new LoopSingleLinkList<Person>(node1, node2, node3);
            Person temp = new Person("s4", "y4");
            // Act
            NodeWithLink<Person> found = list.Find(temp);
            // Assert
            Assert.Null(found);
        }
        [Fact]
        public void Find_FindExistNodeWithNullValue_NotNullNodeReturned()
        {
            // Arrange
            NodeWithLink<Person> node1 = new NodeWithLink<Person>(new Person("s1", "y1"));
            NodeWithLink<Person> node2 = new NodeWithLink<Person>(null);
            NodeWithLink<Person> node3 = new NodeWithLink<Person>(new Person("s3", "y3"));
            LoopSingleLinkList<Person> list = new LoopSingleLinkList<Person>(node1, node2, node3);
            // Act
            NodeWithLink<Person> found = list.Find(null);
            // Assert
            Assert.NotNull(found);
        }

        // FindLast
        [Fact]
        public void FindLast_FindLastExistNodeWithSpecificValueType_NotNullNodeReturned()
        {
            // Arrange
            NodeWithLink<int> node1 = new NodeWithLink<int>(1);
            NodeWithLink<int> node2 = new NodeWithLink<int>(2);
            NodeWithLink<int> node3 = new NodeWithLink<int>(3);
            NodeWithLink<int> node4 = new NodeWithLink<int>(3);
            NodeWithLink<int> node5 = new NodeWithLink<int>(4);
            LoopSingleLinkList<int> list = new LoopSingleLinkList<int>(node1, node2, node3, node4, node5);
            // Act
            NodeWithLink<int> found = list.FindLast(3);
            // Assert
            Assert.Same(node4, found);
        }
        [Fact]
        public void FindLast_FindLastNotExistNodeWithSpecificValueType_NullNodeReturned()
        {
            // Arrange
            LoopSingleLinkList<int> list = new LoopSingleLinkList<int>(1, 2, 3, 4, 5);
            // Act
            NodeWithLink<int> found = list.FindLast(0);
            // Assert
            Assert.Null(found);
        }
        [Fact]
        public void FindLast_FindLastExistNodeWithSpecificReferenceType_NotNullNodeReturned()
        {
            // Arrange
            NodeWithLink<Person> node1 = new NodeWithLink<Person>(new Person("s1", "y1"));
            NodeWithLink<Person> node2 = new NodeWithLink<Person>(new Person("s2", "y2"));
            NodeWithLink<Person> node3 = new NodeWithLink<Person>(new Person("s3", "y3"));
            NodeWithLink<Person> node4 = new NodeWithLink<Person>(new Person("s3", "y3"));
            NodeWithLink<Person> node5 = new NodeWithLink<Person>(new Person("s4", "y4"));
            LoopSingleLinkList<Person> list = new LoopSingleLinkList<Person>(node1, node2, node3, node4, node5);
            Person temp = new Person("s3", "y3");
            // Act
            NodeWithLink<Person> found = list.FindLast(temp);
            // Assert
            Assert.Same(node4, found);
        }
        [Fact]
        public void FindLast_FindLastNotExistNodeWithSpecificReferenceType_NullNodeReturned()
        {
            // Arrange
            NodeWithLink<Person> node1 = new NodeWithLink<Person>(new Person("s1", "y1"));
            NodeWithLink<Person> node2 = new NodeWithLink<Person>(new Person("s2", "y2"));
            NodeWithLink<Person> node3 = new NodeWithLink<Person>(new Person("s3", "y3"));
            LoopSingleLinkList<Person> list = new LoopSingleLinkList<Person>(node1, node2, node3);
            Person temp = new Person("s4", "y4");
            // Act
            NodeWithLink<Person> found = list.FindLast(temp);
            // Assert
            Assert.Null(found);
        }
        [Fact]
        public void FindLast_FindLastExistNodeWithNullValue_NotNullNodeReturned()
        {
            // Arrange
            NodeWithLink<Person> node1 = new NodeWithLink<Person>(new Person("s1", "y1"));
            NodeWithLink<Person> node2 = new NodeWithLink<Person>(null);
            NodeWithLink<Person> node3 = new NodeWithLink<Person>(null);
            NodeWithLink<Person> node4 = new NodeWithLink<Person>(new Person("s3", "y3"));
            LoopSingleLinkList<Person> list = new LoopSingleLinkList<Person>(node1, node2, node3, node4);
            // Act
            NodeWithLink<Person> found = list.FindLast(null);
            // Assert
            Assert.Same(node3, found);
        }

        // Contains
        [Fact]
        public void Contains_ContainsExistNodeWithSpecificValue_TrueReturned()
        {
            // Arrange
            LoopSingleLinkList<int> list = new LoopSingleLinkList<int>(1, 2, 3, 4, 5);
            // Act
            bool isFound = list.Contains(3);
            // Assert
            Assert.True(isFound);
        }
        [Fact]
        public void Contains_ContainsNotExistNodeWithSpecificValue_TrueReturned()
        {
            // Arrange
            LoopSingleLinkList<int> list = new LoopSingleLinkList<int>(1, 2, 3, 4, 5);
            // Act
            bool isFound = list.Contains(0);
            // Assert
            Assert.False(isFound);
        }

        // Remove
        [Fact]
        public void Remove_RemoveExistNodeFromListWithOneNode_TrueReturned()
        {
            // Arrange         
            LoopSingleLinkList<int> list = new LoopSingleLinkList<int>(1);
            // Act
            bool isDeleted = list.Remove(1);
            // Assert
            Assert.True(isDeleted);
            Assert.Null(list.First);
            Assert.Null(list.Last);
        }
        [Fact]
        public void Remove_RemoveExistMiddleNodeFromList_TrueReturned()
        {
            // Arrange
            NodeWithLink<int> node1 = new NodeWithLink<int>(1);
            NodeWithLink<int> node2 = new NodeWithLink<int>(2);
            NodeWithLink<int> node3 = new NodeWithLink<int>(3);
            NodeWithLink<int> node4 = new NodeWithLink<int>(4);
            LoopSingleLinkList<int> list = new LoopSingleLinkList<int>(node1, node2, node3, node4);
            // Act
            bool isDeleted = list.Remove(3);
            // Assert
            Assert.True(isDeleted);
            Assert.Same(node4, node2?.Next);
        }
        [Fact]
        public void Remove_RemoveFirstNodeFromList_TrueReturned()
        {
            // Arrange         
            NodeWithLink<int> node1 = new NodeWithLink<int>(1);
            NodeWithLink<int> node2 = new NodeWithLink<int>(2);
            NodeWithLink<int> node3 = new NodeWithLink<int>(3);
            NodeWithLink<int> node4 = new NodeWithLink<int>(4);
            LoopSingleLinkList<int> list = new LoopSingleLinkList<int>(node1, node2, node3, node4);
            // Act
            bool isDeleted = list.Remove(1);
            // Assert
            Assert.True(isDeleted);
            Assert.Same(node2, list.First);
            Assert.Same(node2, list.Last?.Next);
        }
        [Fact]
        public void Remove_RemoveLastNodeFromList_TrueReturned()
        {
            // Arrange         
            NodeWithLink<int> node1 = new NodeWithLink<int>(1);
            NodeWithLink<int> node2 = new NodeWithLink<int>(2);
            NodeWithLink<int> node3 = new NodeWithLink<int>(3);
            NodeWithLink<int> node4 = new NodeWithLink<int>(4);
            LoopSingleLinkList<int> list = new LoopSingleLinkList<int>(node1, node2, node3, node4);
            // Act
            bool isDeleted = list.Remove(4);
            // Assert
            Assert.True(isDeleted);
            Assert.Same(node3, list.Last);
            Assert.Same(node1, node3?.Next);
        }
        [Fact]
        public void Remove_RemoveNotExistNodeFromList_TrueReturned()
        {
            // Arrange         
            LoopSingleLinkList<int> list = new LoopSingleLinkList<int>(1, 2, 3, 4);
            // Act
            bool isDeleted = list.Remove(0);
            // Assert
            Assert.False(isDeleted);
        }
        [Fact]
        public void Remove_RemoveExistMiddleNodeFromList_LengthDown1()
        {
            // Arrange
            NodeWithLink<int> node1 = new NodeWithLink<int>(1);
            NodeWithLink<int> node2 = new NodeWithLink<int>(2);
            NodeWithLink<int> node3 = new NodeWithLink<int>(3);
            NodeWithLink<int> node4 = new NodeWithLink<int>(4);
            LoopSingleLinkList<int> list = new LoopSingleLinkList<int>(node1, node2, node3, node4);
            // Act
            list.Remove(3);
            // Assert
            Assert.Equal(3, list.Length);
        }

        // Clear 
        [Fact]
        public void Clear_RemoveAllNodesFromList_NodesDeleted()
        {
            // Arrange         
            LoopSingleLinkList<int> list = new LoopSingleLinkList<int>(1, 2, 3, 4, 5);
            // Act
            list.Clear();
            // Assert
            Assert.Equal(0, list.Length);
            Assert.Null(list.First);
            Assert.Null(list.Last);
        }
    }
}
