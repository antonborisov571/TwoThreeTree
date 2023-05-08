using TaskTwoThreeTree;

TwoThreeTree<int> tree = new TwoThreeTree<int>();
for (int i = 0; i < 100000; i++)
{
    tree.Add(i);
}
for (int i = 0; i < 100000; i++)
{
    tree.Remove(i);
}
Console.WriteLine(tree.Contains(2));
int v = 0;