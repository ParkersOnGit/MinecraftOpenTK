using OpenTK.Mathematics;

namespace MinecraftOpenTK.GameObjects.Base
{
    internal abstract class Entity
    {
        internal Vector3 Position { get; set; }
        internal Vector3 Rotation { get; set; }
        private Vector3 scale;
        internal Vector3 Scale
        {
            get => scale;
            set => scale = new Vector3(MathF.Max(value.X, 0.0f), MathF.Max(value.Y, 0.0f), MathF.Max(value.Z, 0.0f));
        }

        protected Entity()
        {
            Position = Vector3.Zero;
            Rotation = Vector3.Zero;
            Scale = Vector3.One;
        }
    }
}
