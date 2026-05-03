using MinecraftOpenTK.Loaders;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace MinecraftOpenTK.GameObjects.Base
{
    internal sealed class Plane
    {
        private int vertexBufferObject;
        private int elementBufferObject;
        private int vertexArrayObject;

        internal Texture Texture { get; set; }
        private Shader shader;

        private Matrix4 modelMatrix;

        internal Vector3 Position { get; set; } = Vector3.Zero;
        internal Vector3 Rotation { get; set; } = Vector3.Zero;
        internal Vector3 Scale { get; set; } = Vector3.One;

        internal string VertexShaderPath { get; set; }
        internal string FragmentShaderPath { get; set; }

        private float[] vertices = new float[]
        {
            // Position             // Texture      //Normal
            -0.5f, 0.0f, -0.5f,     0.0f, 0.0f,     0.0f, 1.0f, 0.0f,
             0.5f, 0.0f, -0.5f,     1.0f, 0.0f,     0.0f, 1.0f, 0.0f,
             0.5f, 0.0f,  0.5f,     1.0f, 1.0f,     0.0f, 1.0f, 0.0f,
            -0.5f, 0.0f,  0.5f,     0.0f, 1.0f,     0.0f, 1.0f, 0.0f
        };

        private uint[] indices = new uint[]
        {
            0, 1, 2,
            2, 3, 0
        };

        /// <summary>
        /// Call this method only once before you want to render (Usually in the OnLoad method).
        /// </summary>
        internal void Initialize()
        {
            // Generate and bind the VAO.
            vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayObject);

            // Generate and bind the VBO.
            vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            // Set vertex attribute pointers.
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);
            GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 5 * sizeof(float));
            GL.EnableVertexAttribArray(2);

            // Generate and bind the EBO.
            elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

            // Load the shader.
            shader = new Shader(VertexShaderPath, FragmentShaderPath);
            shader.Use();
        }

        /// <summary>
        /// Renders the plane.
        /// </summary>
        /// <param name="viewMatrix">The cameras view matrix.</param>
        /// <param name="projectionMatrix">The windows projection matrix.</param>
        internal void Render(Matrix4 viewMatrix, Matrix4 projectionMatrix)
        {
            Update();

            GL.BindVertexArray(vertexArrayObject);
            Texture.Use(TextureUnit.Texture0);

            shader.SetMatrix4("model", modelMatrix);
            shader.SetMatrix4("view", viewMatrix);
            shader.SetMatrix4("projection", projectionMatrix);
            shader.Use();

            GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
        }

        private void Update()
        {
            // Reset the model matrix.
            modelMatrix = Matrix4.Identity;

            // Apply scaling.
            modelMatrix *= Matrix4.CreateScale(Scale);

            // Apply rotation.
            modelMatrix *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(Rotation.Y)) *
                Matrix4.CreateRotationX(MathHelper.DegreesToRadians(Rotation.X)) *
                Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(Rotation.Z));

            // Apply translation.
            modelMatrix *= Matrix4.CreateTranslation(Position);
        }
    }
}
