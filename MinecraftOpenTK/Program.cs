using OpenTK.Windowing.Common;
using OpenTK.Mathematics;

using OpenTK.Windowing.Desktop;

namespace MinecraftOpenTK
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            NativeWindowSettings nativeWindowSettings = new NativeWindowSettings()
            {
                Title = "Minecraft - OpenTK",
                ClientSize = new Vector2i(16 * 100, 9 * 100),
                Vsync = VSyncMode.On,
                StartVisible = false
            };

            using (Window window = new Window(GameWindowSettings.Default, nativeWindowSettings))
            {
                window.Run();
            }
        }
    }
}
