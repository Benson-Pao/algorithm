using algorithmLib.Link; 
namespace algorithmTest
{
    public class LinklistTest
    {
       
        [Fact]
        public void PrintTest()
        {
            int[] data = { 3, 1, 2, 4, 5 };

            Linklist list = new Linklist();
            list.SetData(data);
            string ret = list.Print();
            Assert.Equal("3->1->2->4->5", ret);
        }



        [Fact]
        public void Reverse()
        {
            int[] data = { 1,2, 3, 4, 5 };

            Linklist list = new Linklist();
            list.SetData(data);
            list.Reverse();
            string ret = list.Print();
            Assert.Equal("5->4->3->2->1", ret);
        }

        [Fact]
        public void Reverse_ShouldHandleEmptyList()
        {
            // Arrange
            Linklist list = new Linklist();

            // Act
            list.Reverse();

            // Assert
            string ret = list.Print();
            Assert.Equal("", ret); // 空列表，反轉後仍為空
        }

        // 測試 RemoveFirst
        [Fact]
        public void RemoveFirst_ShouldRemoveFirstNode()
        {
            // Arrange
            int[] data = { 3, 1, 2, 4, 5 };
            Linklist list = new Linklist();
            list.SetData(data);

            // Act
            list.RemoveFirst();

            // Assert
            string ret = list.Print();
            Assert.Equal("1->2->4->5", ret);
        }

        // 測試 RemoveLast
        [Fact]
        public void RemoveLast_ShouldRemoveLastNode()
        {
            // Arrange
            int[] data = { 3, 1, 2, 4, 5 };
            Linklist list = new Linklist();
            list.SetData(data);

            // Act
            list.RemoveLast();

            // Assert
            string ret = list.Print();
            Assert.Equal("3->1->2->4", ret);
        }

        // 測試 RemoveFirst 當列表只有一個元素時
        [Fact]
        public void RemoveFirst_ShouldRemoveOnlyNode()
        {
            // Arrange
            int[] data = { 1 };
            Linklist list = new Linklist();
            list.SetData(data);

            // Act
            list.RemoveFirst();

            // Assert
            string ret = list.Print();
            Assert.Equal("", ret); // 空列表
        }

        // 測試 RemoveLast 當列表只有一個元素時
        [Fact]
        public void RemoveLast_ShouldRemoveOnlyNode()
        {
            // Arrange
            int[] data = { 1 };
            Linklist list = new Linklist();
            list.SetData(data);

            // Act
            list.RemoveLast();

            // Assert
            string ret = list.Print();
            Assert.Equal("", ret); // 空列表
        }
    }
}