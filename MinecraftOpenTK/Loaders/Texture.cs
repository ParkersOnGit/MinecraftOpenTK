using OpenTK.Graphics.OpenGL4;
using System.Drawing;
using System.Drawing.Imaging;

namespace MinecraftOpenTK.Loaders
{
    internal class Texture
    {
        private int Handle;

        /// <summary>
        /// Loads a 2D texture from a specified file path and index.
        /// </summary>
        /// <param name="filePath">The file path of the texture.</param>
        /// <param name="index">The index of which is on the atlas (-1 for entire texture).</param>
        internal static Texture LoadFromFile(string filePath, int index = -1)
        {
            // Create temporary handle to load the texture.
            int tempHandle = GL.GenTexture();

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, tempHandle);

            using (var btmp = new Bitmap(filePath))
            {
                // Get the data from the bitmap by locking into memory.
                BitmapData data = btmp.LockBits(new Rectangle(0, 0, btmp.Width, btmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                btmp.UnlockBits(data);

                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, btmp.Width, btmp.Height, 0, OpenTK.Graphics.OpenGL4.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            }

            // Set the texture parameters.
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter , (int)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter , (int)TextureMagFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS , (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT , (int)TextureWrapMode.Repeat);

            // Generate mipmaps for the texture.
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            return new Texture(tempHandle);
        }
    
        internal Texture(int handle)
        {
            Handle = handle;
        }

        internal void Use(TextureUnit unit)
        {
            GL.ActiveTexture(unit);
            GL.BindTexture(TextureTarget.Texture2D, Handle);
        }
    }
}
