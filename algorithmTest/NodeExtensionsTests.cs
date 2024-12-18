using algorithmLib.Link;

namespace algorithmTest
{
   
    public class NodeExtensionsTests
    {
        [Fact]
        public void InsertNext_ShouldConnectNextNode()
        {
            var node = new Node { Val = 1 };
            var nextNode = node.InsertNext(2);

            Assert.Equal(2, nextNode.Val);
            Assert.Equal(node, nextNode.Prev);
            Assert.Equal(nextNode, node.Next);
        }


        [Fact]
        public void InsertPrev_ShouldConnectPreviousNode()
        {

            var root = new Node();
            var node1 = root.InsertNext(1);
            var node2 = node1.InsertNext(2);

            // Act
            var newNode = node2.InsertPrev(99);

            // Assert
            Assert.Equal(99, newNode.Val);
            Assert.Equal(node1, newNode.Prev);   // 新節點的 Prev 是 node1
            Assert.Equal(node2, newNode.Next);   // 新節點的 Next 是 node2
            Assert.Equal(newNode, node1.Next);   // node1 的 Next 指向新節點
            Assert.Equal(newNode, node2.Prev);   // node2 的 Prev 指向新節點
        }

        [Fact]
        public void RemovePrev_ShouldConnectPreviousNode()
        {

            var root = new Node();
            var node1 = root.InsertNext(1);
            var node2 = node1.InsertNext(2);

            // Act
            var newNode = node2.InsertPrev(99);

 
            // Assert
            Assert.Equal(99, newNode.Val);
            Assert.Equal(node1, newNode.Prev);   // 新節點的 Prev 是 node1
            Assert.Equal(node2, newNode.Next);   // 新節點的 Next 是 node2
            Assert.Equal(newNode, node1.Next);   // node1 的 Next 指向新節點
            Assert.Equal(newNode, node2.Prev);   // node2 的 Prev 指向新節點

            Assert.Equal<bool>(false, node1.RemovePrev());//前節點為root
            Assert.Equal<bool>(true, node2.RemovePrev());//移除前節點node1
            Assert.Equal(root.Next, node1);
            Assert.Equal(node1.Next, node2);

        }


        [Fact]
        public void RemoveNext_ShouldConnectPreviousNode()
        {

            var root = new Node();
            var node1 = root.InsertNext(1);
            var node2 = node1.InsertNext(2);

            // Act
            var newNode = node2.InsertPrev(99);


            // Assert
            Assert.Equal(99, newNode.Val);
            Assert.Equal(node1, newNode.Prev);   // 新節點的 Prev 是 node1
            Assert.Equal(node2, newNode.Next);   // 新節點的 Next 是 node2
            Assert.Equal(newNode, node1.Next);   // node1 的 Next 指向新節點
            Assert.Equal(newNode, node2.Prev);   // node2 的 Prev 指向新節點

            Assert.Equal<bool>(false, node2.RemoveNext());//未端沒節點
            Assert.Equal<bool>(true, node1.RemoveNext());
            Assert.Equal(node2, node1.Next);
        }

        [Fact]
        public void RemoveSelf_ShouldConnectPreviousNode()
        {

            var root = new Node();
            var node1 = root.InsertNext(1);
            var node2 = node1.InsertNext(2);

            // Act
            var newNode = node2.InsertPrev(99);


            // Assert
            Assert.Equal(99, newNode.Val);
            Assert.Equal(node1, newNode.Prev);   // 新節點的 Prev 是 node1
            Assert.Equal(node2, newNode.Next);   // 新節點的 Next 是 node2
            Assert.Equal(newNode, node1.Next);   // node1 的 Next 指向新節點
            Assert.Equal(newNode, node2.Prev);   // node2 的 Prev 指向新節點

            Assert.Equal<bool>(true, node2.RemoveSelf());
            Assert.Equal(newNode, node1.Next);
            Assert.Equal(newNode.Prev, node1);
            Assert.Equal(newNode.Next, null);

        }
    }
}
