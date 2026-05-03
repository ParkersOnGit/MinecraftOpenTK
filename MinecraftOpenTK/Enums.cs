namespace MinecraftOpenTK
{
    internal static class Enums
    {
        internal enum BlockType
        {
            None = 0,
            Stone = 1,
            Dirt = 2,
            Grass = 3
        }

        internal static int[]? ToTextureCoordinates(this BlockType blockType)
        {
            switch (blockType)
            {
                case BlockType.None: return null;
                case BlockType.Stone: return new int[] { 0, 0, 0, 0, 0, 0 };
                case BlockType.Dirt: return new int[] { 1, 1, 1, 1, 1, 1 };
                case BlockType.Grass: return new int[] { 3, 1, 2, 2, 2, 2 };
                default: return null;
            }
        }

        internal enum BlockFace
        {
            Top, Bottom, Front, Back, Left, Right
        }
    }
}
