using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace algorithmLib.Link
{
    /// <summary>
    /// 本身只管瓄Node的操作 (原資料要碓實有root節點)
    /// </summary>
    public static class NodeExtensions
    {
        /// <summary>
        /// 向後加節點
        /// </summary>
        /// <param name="node">當下節點</param>
        /// <param name="value">向後新增值</param>
        /// <returns></returns>
        public static Node InsertNext(this Node node, int value)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (node.Next == null)
            {
                //原節點後端沒節點則尾端附加即可
                node.Next = new Node { Prev = node, Val = value };
                return node.Next;
            }
            else
            {
                Node newNode = new Node { Prev = node, Val = value, Next = node.Next };
                node.Next.Prev = newNode;
                node.Next = newNode;
                return newNode;
            }
        }

        /// <summary>
        /// 向前加節點
        /// </summary>
        /// <param name="node">當下節點</param>
        /// <param name="value">向前新增值</param>
        /// <returns>返回新增的節點</returns>
        public static Node InsertPrev(this Node node, int value)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (node.Prev == null) // 當前節點是 root，必須往後加
            {
                // 如果 root 的 Next 為 null，直接加到後面
                if (node.Next == null)
                {
                    return node.InsertNext(value);
                }

                // 否則插入 root 和 root.Next 之間
                Node newNode = new Node { Prev = node, Val = value, Next = node.Next };
                node.Next.Prev = newNode;
                node.Next = newNode;
                return newNode;
            }
            else
            {
                // 插入到當前節點的前方
                Node newNode = new Node { Prev = node.Prev, Val = value, Next = node };

                // 修正周圍的指向
                node.Prev.Next = newNode;
                node.Prev = newNode;

                return newNode;
            }
        }

        /// <summary>
        /// 刪除本身
        /// </summary>
        /// <param name="node">本身節點</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool RemoveSelf(this Node node) {
            if (node == null) {
                throw new ArgumentNullException(nameof(node));
            }
            if (node.Prev == null) //為root (所有非根節點都會有Prev)
            {
                return false; //根不能刪
            }

            if (node.Next != null)//要判斷本身是為尾端
            {
                node.Next.Prev = node.Prev;
                node.Prev.Next = node.Next;
                return true;
            }

            node.Prev.Next = null;
            return true;
        }

        /// <summary>
        /// 刪襏前節點
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool RemovePrev(this Node node) 
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (node.Prev == null)
            {
                return false;
            }

            return node.Prev.RemoveSelf();
        }


        /// <summary>
        /// 刪除後節點
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool RemoveNext(this Node node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            if (node.Next == null)//後面節點不存在
            { 
                return false;
            }
            return node.Next.RemoveSelf();
        }
    }

    /// <summary>
    /// 鏈結串列節點
    /// </summary>
    public class Node
    {
        public Node? Prev { get; set; } = null;
        public int Val { get; set; } = -1;
        public Node? Next { get; set; } = null;
    }

    /// <summary>
    /// 鍊結行為
    /// </summary>
    public class Linklist
    {
        private Node? rootNode;
        private Node firstNode;
        private Node lastNode;
        private long count;
        public Linklist()
        {
            rootNode = new();
            Clear();
        }

        /// <summary>
        /// 數量
        /// </summary>
        /// <returns></returns>
        public long Count()
        {
            return this.count;
        }

        public void Clear()
        {
            
            Node current = firstNode;
            while (current != null)
            {
                var nextNode = current.Next;
                current.Next = null;  // 解除對後繼節點的引用
                current.Prev = null;
                current = nextNode;
            }
            firstNode = rootNode;
            lastNode = rootNode;
            rootNode.Next = null;
            count = 0;
        }

        /// <summary>
        /// 設定資料
        /// </summary>
        /// <param name="data"></param>
        public void SetData(int[] data)
        {
            for (int i = 0; i < data.Length; i++)
            {
                var newNode = lastNode.InsertNext(data[i]);
                lastNode = newNode;
                if (i == 0)
                {
                    firstNode = newNode;
                }
                count++;
            }
        }
        
        public bool RemoveFirst() 
        {
            if (firstNode.Prev == null)
            {
                return false;
            }
            if (firstNode.RemoveSelf())
            {
                count--;
                return true;
            }
            return false;
        }

        public bool RemoveLast()
        {
            if (lastNode.Prev == null) 
            {
                return false; 
            }
            if (lastNode.RemoveSelf())
            {
                count--;
                return true;
            }
            return false;
        }

        /// <summary>
        /// 輸出
        /// </summary>
        /// <returns></returns>
        public string Print()
        {
            StringBuilder sb = new StringBuilder();
            Node current = rootNode.Next;
            while (current != null)
            {
                sb.Append(current.Val.ToString());
                if (current.Next != null)
                {
                    sb.Append("->");
                }
                current = current.Next;
            }

            return sb.ToString();

        }

        /// <summary>
        /// 交換二邊指標
        /// </summary>
        /// <param name="node"></param>
        public void Swap(Node node)
        {
            var temp = node.Prev;
            node.Prev = node.Next;
            node.Next = temp;
        }

        /// <summary>
        /// 反轉(頭尾指標要處理)
        /// </summary>
        public void Reverse()
        {
            //if (firstNode == null || firstNode == lastNode)
            //{
            //    // 空鏈表或只有一個節點的情況，不需處理
            //    return;
            //}

            //Node current = firstNode;
            //Node prev = null;

            //// 遍歷並反轉每個節點的 Next 和 Prev 指針
            //while (current != null)
            //{
            //    Swap(current);
            //    if (current.Next == rootNode) {
            //        current.Next = null;//因反轉後第一個元素變最後一個元素
            //        lastNode = current;//
            //    }
            //    prev = current;
            //    current = current.Prev;//這是原本的next 
            //}
            //if (prev != null)
            //{
            //    prev.Prev = rootNode;
            //    rootNode.Next = prev;
            //    firstNode = prev;
            //}

            if (firstNode == null) return; // 空列表不需反轉
            Node current = firstNode;
            Node prev = null;

            // 遍歷並反轉每個節點的連結方向
            while (current != null)
            {
                Node next = current.Next;   // 暫存下一個節點
                current.Next = prev;        // 反轉指向
                current.Prev = next;        // 同時調整前驅指向
                prev = current;             // 更新前驅為目前節點
                current = next;             // 移動到下一節點
            }

            // 更新首尾節點
            lastNode = firstNode;          // 原始的第一個節點變為最後一個節點
            firstNode = prev;              // 最後處理的節點成為新的第一個節點

            // 修正根節點的指向
            firstNode.Prev = rootNode;
            rootNode.Next = firstNode;

            lastNode.Next = null;          // 最後一個節點的 Next 為 null

        }

    }
}
