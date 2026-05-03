using MinecraftOpenTK.GameObjects.Base;
using MinecraftOpenTK.GameObjects.Blocks;
using OpenTK.Mathematics;

namespace MinecraftOpenTK.World
{
    internal class Chunk
    {
        internal Block[,,] Data { get; set; } = new Block[16, 64, 16];
        private Vector2i position;

        internal Chunk(Vector2i chunkPosition)
        {
            position = chunkPosition;

            // Very basic chunk for debugging.
            for (int x = 0; x < 16; x++)
            for (int y = 0; y < 64; y++)
            for (int z = 0; z < 16; z++)
            {
                Data[x, y, z] = y < 12 ? new Stone() : y < 15 ? new Dirt() : y < 16 ? new Grass() : null;
                if (Data[x, y, z] == null) continue;
                Data[x, y, z].Position = new Vector3i(x, y, z);
                Data[x, y, z].Position += new Vector3i(position.X * 16, 0, position.Y * 16);
                Data[x, y, z].Initialize();
            }
        }

        internal void Render(Matrix4 viewMatrix, Matrix4 projectionMatrix)
        {
            foreach (Block block in Data)
            {
                if (block == null) continue;

                block.Position -= new Vector3i(position.X * 16, 0, position.Y * 16);

                bool[] visibleFaces = new bool[6] 
                {
                    (block.Position.Y + 1 >= 64) || (Data[block.Position.X, block.Position.Y + 1, block.Position.Z] == null), // Top
                    (block.Position.Y - 1 < 0) || (Data[block.Position.X, block.Position.Y - 1, block.Position.Z] == null), // Bottom
                    (block.Position.Z + 1 >= 16) || (Data[block.Position.X, block.Position.Y, block.Position.Z + 1] == null), // Front
                    (block.Position.Z - 1 < 0) || (Data[block.Position.X, block.Position.Y, block.Position.Z - 1] == null), // Back
                    (block.Position.X - 1 < 0) || (Data[block.Position.X - 1, block.Position.Y, block.Position.Z] == null), // Left
                    (block.Position.X + 1 >= 16) || (Data[block.Position.X + 1, block.Position.Y, block.Position.Z] == null) // Right
                };

                block.Position += new Vector3i(position.X * 16, 0, position.Y * 16);

                block.Render(viewMatrix, projectionMatrix, visibleFaces);
            }
        }
    }
}
