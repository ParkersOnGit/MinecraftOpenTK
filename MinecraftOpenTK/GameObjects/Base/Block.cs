using static MinecraftOpenTK.Enums;

namespace MinecraftOpenTK.GameObjects.Base
{
    internal abstract class Block
    {
        internal BlockType Type { get; private set; }

        internal Block(BlockType type)
        {
            Type = type;

        }
    }
}
