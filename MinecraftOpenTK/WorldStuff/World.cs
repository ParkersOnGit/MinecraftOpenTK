using OpenTK.Mathematics;

namespace MinecraftOpenTK.WorldStuff
{
    internal class World
    {
        internal Dictionary<Vector2i, Chunk> AllChunks { get; set; } = new Dictionary<Vector2i, Chunk>();
        internal Dictionary<Vector2i, Chunk> LoadedChunks { get; set; } = new Dictionary<Vector2i, Chunk>();

        internal void LoadChunk(Vector2i chunkPosition)
        {
            // Check if chunk is already loaded. If so skip.
            if (LoadedChunks.ContainsKey(chunkPosition)) return;

            // Check if chunk is already generated. If so load it, otherwise generate a new one.
            if (AllChunks.ContainsKey(chunkPosition))
            {
                LoadedChunks.Add(chunkPosition, AllChunks[chunkPosition]);
            }
            else
            {
                Chunk newChunk = new Chunk(chunkPosition);
                AllChunks.Add(chunkPosition, newChunk);
                LoadedChunks.Add(chunkPosition, newChunk);
            }
        }

        internal void UnloadChunk(Vector2i chunkPosition)
        {
            // Check if chunk is loaded. If not skip.
            if (!LoadedChunks.ContainsKey(chunkPosition)) return;

            // Unload the chunk.
            LoadedChunks.Remove(chunkPosition);
        }

        internal void RenderLoadedChunks(Matrix4 viewMatrix, Matrix4 projectionMatrix)
        {
            foreach (Chunk chunk in LoadedChunks.Values)
            {
                chunk.Render(viewMatrix, projectionMatrix);
            }
        }
    }
}
