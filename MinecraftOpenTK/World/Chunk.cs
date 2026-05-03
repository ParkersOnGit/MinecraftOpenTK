using MinecraftOpenTK.GameObjects.Base;
using MinecraftOpenTK.GameObjects.Blocks;
using OpenTK.Mathematics;

namespace MinecraftOpenTK.World
{
    internal class Chunk
    {
        internal Block[,,] Data { get; set; } = new Block[16, 25, 16]; // Eventually replace with (16, 256, 16).

        internal Chunk()
        {
            // Very basic chunk for debugging.
            for (int x = 0; x < 16; x++)
            for (int y = 0; y < 3; y++)
            for (int z = 0; z < 16; z++)
            {
                Data[x, y, z] = y == 0 ? new Stone() : y == 1 ? new Dirt() : new Grass();
                Data[x, y, z].Position = new Vector3(x, y, z);
                Data[x, y, z].Initialize();
            }
        }

        internal void Render(Matrix4 viewMatrix, Matrix4 projectionMatrix)
        {
            foreach (Block block in Data)
            {
                if (block == null) continue;
                block.Render(viewMatrix, projectionMatrix);
            }
        }
    }
}
