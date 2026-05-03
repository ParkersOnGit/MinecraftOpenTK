using MinecraftOpenTK.GameObjects.Base;
using OpenTK.Mathematics;

namespace MinecraftOpenTK.GameObjects
{
    internal class Camera : Entity
    {
        private Vector3 rotation;
        internal Vector3 Rotation
        {
            get => rotation;
            set => rotation = new Vector3(MathF.Min(MathF.Max(-90.0f, value.X), 90.0f), value.Y, value.Z);
        }

        internal float FOV { get; private set; } = 90.0f;

        internal Matrix4 ViewMatrix { get; private set; }
        internal void Update()
        {
            // Reset the view matrix to an identity matrix.
            ViewMatrix = Matrix4.Identity;

            // Apply camera translation.
            ViewMatrix *= Matrix4.CreateTranslation(-Position);

            // Apply camera rotation.
            ViewMatrix *= Matrix4.CreateRotationY(MathHelper.DegreesToRadians(-Rotation.Y)) *
                Matrix4.CreateRotationX(MathHelper.DegreesToRadians(-Rotation.X)) *
                Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(-Rotation.Z));
        }
    }
}
