namespace AOC2023Backend.Days
{
    public class DayTwentyTwo : Day
    {
        public override string PartOne(string input)
        {
            throw new NotImplementedException();
        }

        public override string PartTwo(string input)
        {
            throw new NotImplementedException();
        }
    }

    public class Matrix3D<T> where T : class
    {
        public List<List<List<T?>>> Values { get; set; } = new();


        public T? GetValue(int x, int y, int z)
        {
            return Values[x][y][z];
        }

        public void SetValue(int x, int y, int z, T? input)
        {
            while (Values.Count - 1 < x)
            {
                Values.Add(new());
            }
            while (Values[x].Count - 1 < y)
            {
                Values[x].Add(new());
            }
            while (Values[x][y].Count - 1 < z)
            {
                Values[x][y].Add(null);
            }
            Values[x][y][z] = input;
        }


    }

    public class Block
    {
        public enum Axis
        {
            X, Y, Z
        }
        public List<BlockNode> Nodes { get; set; } = new List<BlockNode>();

        public List<Block> SittingOn { get; set; } = new();

        public Axis AlongAxis { get; set; }

        public Matrix3D<BlockNode> ParentMatrix { get; set; }

        public static Block BlockFactory(string input, Matrix3D<BlockNode> parent)
        {
            var coords = input.Split('~');

            var xYZs = coords.Select(x => x.Split(',')).Select(x => x.Select(y => int.Parse(y)).ToList()).ToList();

            return new Block(xYZs[0][0], xYZs[0][1], xYZs[0][2], xYZs[1][0], xYZs[1][1], xYZs[1][2], parent);
        }

        public Block(int x1, int y1, int z1, int x2, int y2, int z2, Matrix3D<BlockNode> parent)
        {
            ParentMatrix = parent;
            if (x1 != x2)
            {
                AlongAxis = Axis.X;

                if (x1 > x2)
                {
                    (x2, x1) = (x1, x2);
                }
                for (int i = x1; i <= x2; i++)
                {
                    Nodes.Add(new BlockNode(i, y1, z1, this));
                }
            }
            else if (y1 != y2)
            {
                AlongAxis = Axis.Y;

                if (y1 > y2)
                {
                    (y2, y1) = (y1, y2);
                }
                for (int i = y1; i <= y2; i++)
                {
                    Nodes.Add(new BlockNode(x1, i, z1, this));
                }
            }
            else if (z1 != z2)
            {
                AlongAxis = Axis.Z;

                if (z1 > z2)
                {
                    (z2, z1) = (z1, z2);
                }
                for (int i = z1; i <= z2; i++)
                {
                    Nodes.Add(new BlockNode(x1, y1, i, this));
                }
            }

            foreach (var node in Nodes)
            {
                ParentMatrix.SetValue(node.XIndex, node.YIndex, node.ZIndex, node);
            }

            ParentMatrix = parent;
        }
    }

    public class BlockNode
    {
        public Block ParentBlock { get; set; }
        public int XIndex { get; set; }
        public int YIndex { get; set; }
        public int ZIndex { get; set; }

        public BlockNode(int x, int y, int z, Block parent)
        {
            ParentBlock = parent;
            XIndex = x; YIndex = y; ZIndex = z;
        }

        public Block? CheckBelow()
        {
            var below = ParentBlock.ParentMatrix.GetValue(XIndex, YIndex, ZIndex - 1);
            if (below != null)
            {
                return below.ParentBlock;
            }
            return null;
        }

    }


}
