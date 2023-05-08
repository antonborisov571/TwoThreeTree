using System.Xml.Linq;
using TaskTwoThreeTree;

namespace TestTwoThreeTree
{
    public class TestTwoThreeTree
    {
        private ITwoThreeTree<int> tree;

        public TestTwoThreeTree()
        {
            // Создаем новое дерево для каждого теста
            tree = new TwoThreeTree<int>();
        }

        [Fact]
        public void Add_AddsDataToTree()
        {
            tree.Add(5);
            tree.Add(3);
            tree.Add(7);

            Assert.True(tree.Contains(5));
            Assert.True(tree.Contains(3));
            Assert.True(tree.Contains(7));
        }

        [Fact]
        public void Find_ReturnsCorrectNode()
        {
            tree.Add(5);
            tree.Add(3);
            tree.Add(7);

            var node = tree.Find(3);

            Assert.Equal(3, node.DataLeft);
        }

        [Fact]
        public void Remove_RemovesDataFromTree()
        {
            tree.Add(5);
            tree.Add(3);
            tree.Add(7);

            tree.Remove(3);

            Assert.False(tree.Contains(3));
        }

        [Fact]
        public void Contains_ReturnsTrueForExistingData()
        {
            tree.Add(5);
            tree.Add(3);
            tree.Add(7);

            Assert.True(tree.Contains(5));
        }

        [Fact]
        public void Contains_ReturnsFalseForNonexistentData()
        {
            tree.Add(5);
            tree.Add(3);
            tree.Add(7);

            Assert.False(tree.Contains(10));
        }

        [Fact]
        public void Remove_SmallSheetRight()
        {
            tree.Add(5);
            tree.Add(3);
            tree.Add(7);

            tree.Remove(7);
            Assert.Equal(3, tree.RootNode.DataLeft);
            Assert.Equal(5, tree.RootNode.DataRight);
        }

        [Fact]
        public void Remove_SmallSheetLeft()
        {
            tree.Add(5);
            tree.Add(3);
            tree.Add(7);

            tree.Remove(3);
            Assert.Equal(5, tree.RootNode.DataLeft);
            Assert.Equal(7, tree.RootNode.DataRight);
        }

        [Fact]
        public void Remove_BigSheetRight()
        {
            int[] elements = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            foreach (int element in elements)
            {
                tree.Add(element);
            }
            tree.Remove(10);
            Assert.Equal(4, tree.RootNode.DataLeft);
            Assert.Equal(2, tree.RootNode.Left.DataLeft);
            Assert.Equal(1, tree.RootNode.Left.Left.DataLeft);
            Assert.Equal(3, tree.RootNode.Left.Right.DataLeft);
            Assert.Equal(6, tree.RootNode.Right.DataLeft);
            Assert.Equal(8, tree.RootNode.Right.DataRight);
            Assert.Equal(5, tree.RootNode.Right.Left.DataLeft);
            Assert.Equal(7, tree.RootNode.Right.MiddleRight.DataLeft);
            Assert.Equal(9, tree.RootNode.Right.Right.DataLeft);
        }

        [Fact]
        public void Remove_BigSheetLeft()
        {
            int[] elements = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            foreach (int element in elements)
            {
                tree.Add(element);
            }
            tree.Remove(1);
            Assert.Equal(6, tree.RootNode.DataLeft);
            Assert.Equal(4, tree.RootNode.Left.DataLeft);
            Assert.Equal(2, tree.RootNode.Left.Left.DataLeft);
            Assert.Equal(3, tree.RootNode.Left.Left.DataRight);
            Assert.Equal(5, tree.RootNode.Left.Right.DataLeft);
            Assert.Equal(8, tree.RootNode.Right.DataLeft);
            Assert.Equal(7, tree.RootNode.Right.Left.DataLeft);
            Assert.Equal(10, tree.RootNode.Right.Right.DataRight);
            Assert.Equal(9, tree.RootNode.Right.Right.DataLeft);
        }

        [Fact]
        public void Remove_BigSheetLeft_Left()
        {
            int[] elements = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            foreach (int element in elements)
            {
                tree.Add(element);
            }
            tree.Remove(1);
            tree.Remove(8);
            Assert.Equal(6, tree.RootNode.DataLeft);
            Assert.Equal(4, tree.RootNode.Left.DataLeft);
            Assert.Equal(2, tree.RootNode.Left.Left.DataLeft);
            Assert.Equal(3, tree.RootNode.Left.Left.DataRight);
            Assert.Equal(5, tree.RootNode.Left.Right.DataLeft);
            Assert.Equal(9, tree.RootNode.Right.DataLeft);
            Assert.Equal(7, tree.RootNode.Right.Left.DataLeft);
            Assert.Equal(10, tree.RootNode.Right.Right.DataLeft);
            //Assert.Equal(9, tree.RootNode.Right.Right.DataLeft);
        }

        [Fact]
        public void Check_LeftRightFlipping()
        {
            int[] elements = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            foreach (int element in elements)
            {
                tree.Add(element);
            }
            tree.Remove(10);
            Assert.Equal(4, tree.RootNode.DataLeft);
            Assert.Equal(8, tree.RootNode.DataRight);
            Assert.Equal(14, tree.RootNode.Right.DataRight);
            Assert.Equal(7, tree.RootNode.MiddleRight.Right.DataLeft);
        }

        [Fact]
        public void Remove_DataRight()
        {
            int[] elements = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            foreach (int element in elements)
            {
                tree.Add(element);
            }
            tree.Remove(10);
            tree.Remove(14);
            Assert.Equal(4, tree.RootNode.DataLeft);
            Assert.Equal(8, tree.RootNode.DataRight);
            Assert.Equal(12, tree.RootNode.Right.DataLeft);
            Assert.Equal(9, tree.RootNode.Right.Left.DataLeft);
        }

        [Fact]
        public void Remove_DataLeft()
        {
            int[] elements = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            foreach (int element in elements)
            {
                tree.Add(element);
            }
            tree.Remove(10);
            tree.Remove(12);
            Assert.Equal(4, tree.RootNode.DataLeft);
            Assert.Equal(8, tree.RootNode.DataRight);
            Assert.Equal(11, tree.RootNode.Right.DataLeft);
            Assert.Equal(15, tree.RootNode.Right.Right.DataLeft);
        }

        [Fact]
        public void Remove_RootNode()
        {
            int[] elements = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            foreach (int element in elements)
            {
                tree.Add(element);
            }
            tree.Remove(8);
            Assert.Equal(7, tree.RootNode.DataLeft);
            Assert.Equal(12, tree.RootNode.DataRight);
            Assert.Equal(14, tree.RootNode.Right.DataLeft);
            Assert.Equal(15, tree.RootNode.Right.Right.DataLeft);
        }

        [Fact]
        public void Remove_Left_MiddleRight()
        {
            int[] elements = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            foreach (int element in elements)
            {
                tree.Add(element);
            }
            tree.Remove(8);
            tree.Remove(9);
            Assert.Equal(4, tree.RootNode.DataLeft);
            Assert.Equal(12, tree.RootNode.DataRight);
            Assert.Equal(7, tree.RootNode.MiddleRight.DataLeft);
            Assert.Equal(5, tree.RootNode.MiddleRight.Left.DataLeft);
            Assert.Equal(6, tree.RootNode.MiddleRight.Left.DataRight);
            Assert.Equal(10, tree.RootNode.MiddleRight.Right.DataLeft);
            Assert.Equal(11, tree.RootNode.MiddleRight.Right.DataRight);
        }

        [Fact]
        public void Remove_LeftRight()
        {
            int[] elements = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            foreach (int element in elements)
            {
                tree.Add(element);
            }
            tree.Remove(8);
            tree.Remove(9);
            tree.Remove(3);
            Assert.Equal(12, tree.RootNode.DataLeft);
            Assert.Equal(7, tree.RootNode.Left.DataRight);
            Assert.Equal(11, tree.RootNode.Left.Right.DataRight);
        }

        [Fact]
        public void Remove_RightLeft()
        {
            int[] elements = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            foreach (int element in elements)
            {
                tree.Add(element);
            }
            tree.Remove(8);
            tree.Remove(9);
            tree.Remove(3);
            tree.Remove(13);
            Assert.Equal(7, tree.RootNode.DataLeft);
            Assert.Equal(4, tree.RootNode.Left.DataLeft);
            Assert.Equal(6, tree.RootNode.Left.Right.DataRight);
        }

        [Fact]
        public void Remove_SheetLeftLeft()
        {
            int[] elements = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            foreach (int element in elements)
            {
                tree.Add(element);
            }
            tree.Remove(8);
            tree.Remove(9);
            tree.Remove(3);
            tree.Remove(13);
            tree.Remove(1);
            Assert.Equal(7, tree.RootNode.DataLeft);
            Assert.Equal(4, tree.RootNode.Left.DataLeft);
            Assert.Equal(6, tree.RootNode.Left.Right.DataRight);
        }

        [Fact]
        public void Remove_SheetFull()
        {
            int[] elements = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            foreach (int element in elements)
            {
                tree.Add(element);
            }
            tree.Remove(8);
            tree.Remove(9);
            tree.Remove(3);
            tree.Remove(13);
            tree.Remove(1);
            tree.Remove(2);
            Assert.Equal(7, tree.RootNode.DataLeft);
            Assert.Equal(5, tree.RootNode.Left.DataLeft);
            Assert.Equal(6, tree.RootNode.Left.Right.DataLeft);
        }

        [Fact]
        public void Remove_SheetFullFull()
        {
            int[] elements = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            foreach (int element in elements)
            {
                tree.Add(element);
            }
            tree.Remove(8);
            tree.Remove(9);
            tree.Remove(3);
            tree.Remove(13);
            tree.Remove(1);
            tree.Remove(2);
            tree.Remove(4);
            Assert.Equal(7, tree.RootNode.DataLeft);
            Assert.Equal(5, tree.RootNode.Left.DataLeft);
            Assert.Equal(11, tree.RootNode.MiddleRight.DataRight);
            Assert.Equal(15, tree.RootNode.Right.DataRight);
        }

        [Fact]
        public void Add_Remove_SingleElement_Success()
        {
            int element = 5;
            tree.Add(element);
            tree.Remove(element);
            Assert.Equal(0, tree.RootNode.DataLeft);
        }

        [Fact]
        public void Add_Remove_MultipleElements_Success()
        {
            int[] elements = { 5, 8, 2, 9, 1, 10, 4, 6, 3, 7 };
            foreach (int element in elements)
            {
                tree.Add(element);
            }
            foreach (int element in elements)
            {
                tree.Remove(element);
            }
            Assert.Equal(0, tree.RootNode.DataLeft);
        }

        [Fact]
        public void Remove_NotElement()
        {
            int[] elements = { 5, 8, 2, 9, 1, 10, 4, 6, 3, 7 };
            foreach (int element in elements)
            {
                tree.Add(element);
            }
            tree.Remove(100);
            Assert.False(tree.Remove(100));
        }

        [Fact]
        public void Contains_MiddleRight()
        {
            int[] elements = { 1, 2, 3, 4, 5 };
            foreach (int element in elements)
            {
                tree.Add(element);
            }
            Assert.True(tree.Contains(3));
        }

        [Fact]
        public void Add_MiddleRight()
        {
            int[] elements = { 10, 20, 30, 40, 50, 25 };
            foreach (int element in elements)
            {
                tree.Add(element);
            }
            Assert.True(tree.Contains(25));
        }

        [Fact]
        public void Add_MiddleRight_MiddleRight()
        {
            int[] elements = { 10, 20, 30, 40, 50, 25, 27 };
            foreach (int element in elements)
            {
                tree.Add(element);
            }
            Assert.Equal(27, tree.RootNode.DataLeft);
        }

        [Fact]
        public void Add_Default()
        {
            int element = 0;
            Assert.False(tree.Add(element));
        }

        [Fact]
        public void Remove_MiddleRight_RightOneElement()
        {
            int[] elements = { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110 };
            foreach (int element in elements)
            {
                tree.Add(element);
            }
            tree.Remove(70);
            Assert.Equal(80, tree.RootNode.DataLeft);
            Assert.Equal(60, tree.RootNode.Left.Right.DataRight);
        }

        [Fact]
        public void Remove_SheetRootNode()
        {
            int[] elements = { 10 };
            foreach (int element in elements)
            {
                tree.Add(element);
            }
            tree.Remove(10);
            Assert.Equal(0, tree.RootNode.DataLeft);
        }

        [Fact]
        public void FlippingLeft_ParentDataRight_MiddleRightDataRight()
        {
            int[] elements = { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 55, 57 };
            foreach (int element in elements)
            {
                tree.Add(element);
            }
            tree.Remove(30);
            Assert.Equal(50, tree.RootNode.Left.Right.DataLeft);
            Assert.Equal(40, tree.RootNode.Left.Right.Parent.DataLeft);
        }

        [Fact]
        public void FlippingRight_ParentDataRight_MiddleRightDataRight()
        {
            int[] elements = { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 55, 57 };
            foreach (int element in elements)
            {
                tree.Add(element);
            }
            tree.Remove(90);
            Assert.Equal(100, tree.RootNode.Right.Right.DataLeft);
            Assert.Equal(80, tree.RootNode.Right.Right.Parent.DataLeft);
        }

        [Fact]
        public void FlippingRight_ParentDataRight()
        {
            int[] elements = { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 55 };
            foreach (int element in elements)
            {
                tree.Add(element);
            }
            tree.Remove(90);
            Assert.Equal(100, tree.RootNode.Right.Right.DataLeft);
            Assert.Equal(80, tree.RootNode.Right.Right.Parent.DataRight);
        }

        [Fact]
        public void Remove_DataRightNotSheet()
        {
            int[] elements = { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 55 };
            foreach (int element in elements)
            {
                tree.Add(element);
            }
            tree.Remove(80);
            Assert.Equal(100, tree.RootNode.Right.Right.DataLeft);
            Assert.Equal(110, tree.RootNode.Right.Right.DataRight);
        }

        [Fact]
        public void Remove_BigDataRightNotSheet()
        {
            int[] elements = { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 130, 140, 150, 160, 170, 180, 190, 200 };
            foreach (int element in elements)
            {
                tree.Add(element);
            }
            tree.Remove(160);
            Assert.Equal(190, tree.RootNode.Right.Right.DataLeft);
            Assert.Equal(200, tree.RootNode.Right.Right.Right.DataLeft);
        }

        [Fact]
        public void Check_Coverage1()
        {
            int[] elements = { 5, 8, 12, 25, 33, 41, 56, 67, 71, 92, 101 };
            foreach (int element in elements)
            {
                tree.Add(element);
            }
            tree.Remove(56);
            tree.Remove(12);
            tree.Remove(71);
            tree.Remove(101);
            tree.Remove(33);
            tree.Remove(67);
        }

        [Fact]
        public void Check_Coverage2()
        {
            int[] elements = { 1, 4, 7, 11, 15, 22, 34, 45, 57, 68, 76, 82, 91, 105 };
            foreach (int element in elements)
            {
                tree.Add(element);
            }
            tree.Remove(34);
            tree.Remove(45);
            tree.Remove(91);
            tree.Remove(105);
            tree.Remove(82);
        }

        [Fact]
        public void Check_Coverage3()
        {
            int[] elements = { 3, 6, 10, 17, 23, 27, 38, 49, 55, 62, 78, 84, 97, 103, 111 };
            foreach (int element in elements)
            {
                tree.Add(element);
            }
            tree.Remove(3);
            tree.Remove(111);
            tree.Remove(6);
            tree.Remove(103);
            tree.Remove(10);
            tree.Remove(55);
            tree.Remove(78);
        }

        [Fact]
        public void Check_Coverage4()
        {
            int[] elements = { 2, 9, 14, 18, 21, 31, 43, 52, 61, 73, 81, 85, 95, 108, 113, 126, 137, 149, 157, 169 };
            foreach (int element in elements)
            {
                tree.Add(element);
            }
            tree.Remove(81);
            tree.Remove(73);
            tree.Remove(31);
            tree.Remove(169);
            tree.Remove(113);
            tree.Remove(95);
            tree.Remove(81);
        }

        [Fact]
        public void Check_Coverage5()
        {
            int[] elements = { 13, 24, 35, 46, 58, 63, 75, 88, 99, 114, 121, 134, 146, 159, 167, 178, 189, 197, 205, 210 };
            foreach (int element in elements)
            {
                tree.Add(element);
            }
            tree.Remove(114);
            tree.Remove(134);
            tree.Remove(189);
            tree.Remove(35);
            tree.Remove(75);
            tree.Remove(121);
            tree.Remove(88);
        }

        [Fact]
        public void Check_Coverage6()
        {
            TwoThreeTree<int> tree = new TwoThreeTree<int>();
            for (int i = 0; i < 100000; i++)
            {
                tree.Add(i);
            }
            for (int i = 0; i < 100000; i++)
            {
                tree.Remove(i);
            }
        }

        [Fact]
        public void Check_Coverage7()
        {
            var random = new Random();
            var list = new List<int>();
            TwoThreeTree<int> tree = new TwoThreeTree<int>();
            for (int i = 0; i < 100000; i++)
            {
                tree.Add(i);
            }
            for (int i = 0; i < 1000000; i++)
            {
                list.Add(random.Next(1, 100000));
            }
            foreach (int i in list)
            {
                tree.Remove(i);
            }
            for (int i = 1; i < 100000; i++)
            {
                if (!list.Contains(i))
                {
                    Assert.True(tree.Contains(i));
                }
            }
            foreach (int i in list)
            {
                Assert.False(tree.Contains(i));
            }
        }

        [Fact]
        public void Check_Coverage8()
        {
            var random = new Random();
            var list = new List<int>();
            TwoThreeTree<int> tree = new TwoThreeTree<int>();
            for (int i = 0; i < 10000; i++)
            {
                tree.Add(i);
            }
            for (int i = 0; i < 10000; i++)
            {
                list.Add(random.Next(1, 10000));
            }
            foreach (int i in list)
            {
                tree.Remove(i);
            }
            for (int i = 1; i < 10000; i++)
            {
                if (!list.Contains(i))
                {
                    Assert.True(tree.Contains(i));
                }
            }
            foreach (int i in list)
            {
                Assert.False(tree.Contains(i));
            }
        }

        [Fact]
        public void Check_Coverage9()
        {
            var random = new Random();
            var list = new List<int>();
            TwoThreeTree<int> tree = new TwoThreeTree<int>();
            for (int i = 0; i < 100000; i++)
            {
                tree.Add(i);
            }
            for (int i = 0; i < 1000000; i++)
            {
                list.Add(random.Next(1, 100000));
            }
            foreach (int i in list)
            {
                tree.Remove(i);
            }
            for (int i = 1; i < 100000; i++)
            {
                if (!list.Contains(i))
                {
                    Assert.True(tree.Contains(i));
                }
            }
            foreach (int i in list)
            {
                Assert.False(tree.Contains(i));
            }
        }

        [Fact]
        public void Check_Coverage10()
        {
            var random = new Random();
            var list = new List<int>();
            TwoThreeTree<int> tree = new TwoThreeTree<int>();
            for (int i = 0; i < 100000; i++)
            {
                tree.Add(i);
            }
            for (int i = 0; i < 1000000; i++)
            {
                list.Add(random.Next(1, 100000));
            }
            foreach (int i in list)
            {
                tree.Remove(i);
            }
            for (int i = 1; i < 100000; i++)
            {
                if (!list.Contains(i))
                {
                    Assert.True(tree.Contains(i));
                }
            }
            foreach (int i in list)
            {
                Assert.False(tree.Contains(i));
            }
        }
    }
}