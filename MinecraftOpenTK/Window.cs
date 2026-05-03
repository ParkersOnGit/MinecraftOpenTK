using MinecraftOpenTK.GameObjects;
using MinecraftOpenTK.GameObjects.Base;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace MinecraftOpenTK
{
    internal class Window : GameWindow
    {
        internal Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings) { }
        
        internal Matrix4 ProjectionMatrix { get; private set; } = Matrix4.Identity;
        internal Camera Camera { get; private set; } = new Camera();

        public Plane testPlane = new Plane(); // debug remove later
        public Block testBlock = new Block(); // debug remove later

        override protected void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(0.59f, 0.84f, 1.00f, 1.00f);

            // Set initial camera position.
            Camera.Position = new Vector3(0.0f, 0.5f, 0.0f);

            testPlane.VertexShaderPath = "../../../Assets/Shaders/Default.vert";
            testPlane.FragmentShaderPath = "../../../Assets/Shaders/Default.frag";
            testPlane.Initialize();

            testBlock.Initialize();

            // Once done loading, show the window.
            UpdateFrameViewport();
            IsVisible = true;
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit);
            Camera.Update();

            //testPlane.Render(Camera.ViewMatrix, ProjectionMatrix);

            testBlock.Render(Camera.ViewMatrix, ProjectionMatrix);

            Title = Camera.Position.ToString();
            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            double DT = e.Time;
            KeyboardState KB = KeyboardState;
            float playerSpeed = 5.0f;

            if (KB.IsKeyDown(Keys.W))
                Camera.Position += new Vector3(-(float)MathHelper.Sin(MathHelper.DegreesToRadians(Camera.Rotation.Y)),
                    0.0f, 
                    -(float)MathHelper.Cos(MathHelper.DegreesToRadians(Camera.Rotation.Y))) * (float)DT * playerSpeed;
            if (KB.IsKeyDown(Keys.A))
                Camera.Position += new Vector3(-(float)MathHelper.Cos(MathHelper.DegreesToRadians(Camera.Rotation.Y)), 
                    0.0f, 
                    (float)MathHelper.Sin(MathHelper.DegreesToRadians(Camera.Rotation.Y))) * (float)DT * playerSpeed;
            if (KB.IsKeyDown(Keys.S))
                Camera.Position += new Vector3((float)MathHelper.Sin(MathHelper.DegreesToRadians(Camera.Rotation.Y)), 
                    0.0f, 
                    (float)MathHelper.Cos(MathHelper.DegreesToRadians(Camera.Rotation.Y))) * (float)DT * playerSpeed;
            if (KB.IsKeyDown(Keys.D))
                Camera.Position += new Vector3((float)MathHelper.Cos(MathHelper.DegreesToRadians(Camera.Rotation.Y)), 
                    0.0f, 
                    -(float)MathHelper.Sin(MathHelper.DegreesToRadians(Camera.Rotation.Y))) * (float)DT * playerSpeed;
            if (KB.IsKeyDown(Keys.Space))
                Camera.Position += new Vector3(0.0f, 1.0f, 0.0f) * (float)DT * playerSpeed;
            if (KB.IsKeyDown(Keys.LeftShift))
                Camera.Position += new Vector3(0.0f, -1.0f, 0.0f) * (float)DT * playerSpeed;
        }

        protected override void OnFramebufferResize(FramebufferResizeEventArgs e) => UpdateFrameViewport();
        private void UpdateFrameViewport()
        {
            GL.Viewport(0, 0, ClientSize.X, ClientSize.Y);
            ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(Camera.FOV), ClientSize.X / (float)ClientSize.Y, 0.1f, 100f);
        }
    }
}
