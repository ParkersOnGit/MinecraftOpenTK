using MinecraftOpenTK.Loaders;
using OpenTK.Mathematics;
using static MinecraftOpenTK.Enums;

namespace MinecraftOpenTK.GameObjects.Base
{
    internal abstract class Block
    {
        protected BlockType Type { get; set; }

        private Plane[] planes = new Plane[6];
        protected Texture[] textures = new Texture[6];

        internal Vector3 Position { get; set; } = Vector3.Zero;

        internal void Initialize()
        {
            for (int i = 0; i < planes.Length; i++)
            {
                planes[i] = new Plane();
                planes[i].VertexShaderPath = "../../../Assets/Shaders/Default.vert";
                planes[i].FragmentShaderPath = "../../../Assets/Shaders/Block.frag";
                planes[i].Texture = Texture.LoadFromFile($"../../../Assets/Textures/TextureAtlas.png", Type.ToTextureCoordinates()[i]);
                planes[i].Initialize();

                switch ((BlockFace)i)
                {
                    case BlockFace.Top:
                        planes[i].Position = new Vector3(0.0f, 0.5f, 0.0f);
                        break;
                    case BlockFace.Bottom:
                        planes[i].Position = new Vector3(0.0f, -0.5f, 0.0f);
                        break;
                    case BlockFace.Front:
                        planes[i].Position = new Vector3(0.0f, 0.0f, 0.5f);
                        planes[i].Rotation = new Vector3(90.0f, 0.0f, 0.0f);
                        break;
                    case BlockFace.Back:
                        planes[i].Position = new Vector3(0.0f, 0.0f, -0.5f);
                        planes[i].Rotation = new Vector3(-90.0f, 180.0f, 0.0f);
                        break;
                    case BlockFace.Left:
                        planes[i].Position = new Vector3(-0.5f, 0.0f, 0.0f);
                        planes[i].Rotation = new Vector3(0.0f, -90.0f, 90.0f);
                        break;
                    case BlockFace.Right:
                        planes[i].Position = new Vector3(0.5f, 0.0f, 0.0f);
                        planes[i].Rotation = new Vector3(0.0f, 90.0f, -90.0f);
                        break;
                }
                planes[i].Position += Position;
            }
        }

        internal void Render(Matrix4 viewMatrix, Matrix4 projectionMatrix)
        {
            for (int i = 0; i < planes.Length; i++)
            {
                planes[i].Render(viewMatrix, projectionMatrix);
            }
        }
    }
}
