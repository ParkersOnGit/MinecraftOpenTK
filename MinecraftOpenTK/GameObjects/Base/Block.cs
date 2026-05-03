using OpenTK.Mathematics;
using static MinecraftOpenTK.Enums;

namespace MinecraftOpenTK.GameObjects.Base
{
    internal class Block // RETURN THIS TO AN ABSTRACT CLASS LATER
    {
        protected BlockType Type { get; private set; }

        private Plane[] planes = new Plane[6];

        internal void Initialize()
        {
            for (int i = 0; i < planes.Length; i++)
            {
                planes[i] = new Plane();
                planes[i].VertexShaderPath = "../../../Assets/Shaders/Default.vert";
                planes[i].FragmentShaderPath = "../../../Assets/Shaders/Default.frag";
                planes[i].Initialize();

                switch ((BlockFaces)i)
                {
                    case BlockFaces.Top:
                        planes[i].Position = new Vector3(0.0f, 0.5f, 0.0f);
                        break;
                    case BlockFaces.Bottom:
                        planes[i].Position = new Vector3(0.0f, -0.5f, 0.0f);
                        break;
                    case BlockFaces.Front:
                        planes[i].Position = new Vector3(0.0f, 0.0f, 0.5f);
                        planes[i].Rotation = new Vector3(90.0f, 0.0f, 0.0f);
                        break;
                    case BlockFaces.Back:
                        planes[i].Position = new Vector3(0.0f, 0.0f, -0.5f);
                        planes[i].Rotation = new Vector3(-90.0f, 0.0f, 0.0f);
                        break;
                    case BlockFaces.Left:
                        planes[i].Position = new Vector3(-0.5f, 0.0f, 0.0f);
                        planes[i].Rotation = new Vector3(0.0f, 0.0f, 90.0f);
                        break;
                    case BlockFaces.Right:
                        planes[i].Position = new Vector3(0.5f, 0.0f, 0.0f);
                        planes[i].Rotation = new Vector3(0.0f, 0.0f, -90.0f);
                        break;
                }
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
