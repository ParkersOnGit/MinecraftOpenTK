using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace MinecraftOpenTK.Loaders
{
    internal class Shader
    {
        private int Handle;
        private Dictionary<string, int> uniformLocations = new Dictionary<string, int>();
        private static Dictionary<(string, string), Shader> shaderCache = new Dictionary<(string, string), Shader>();

        internal void Use() => GL.UseProgram(Handle);

        internal Shader(string vertexPath, string fragmentPath)
        {
            if (shaderCache.ContainsKey((vertexPath, fragmentPath)))
            {
                Handle = shaderCache[(vertexPath, fragmentPath)].Handle;
                uniformLocations = shaderCache[(vertexPath, fragmentPath)].uniformLocations;
                return;
            }

            // Read the shader source code.
            string vertexShaderSource = File.ReadAllText(vertexPath);
            string fragmentShaderSource = File.ReadAllText(fragmentPath);

            // Generate shaders and bind source code to them.
            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertexShaderSource);

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragmentShaderSource);

            // Compile and check for errors.
            GL.CompileShader(vertexShader);
            GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out int compileStatus);
            if (compileStatus == 0)
            {
                string infoLog = GL.GetShaderInfoLog(vertexShader);
                Console.WriteLine(infoLog);
            }

            GL.CompileShader(fragmentShader);
            GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out compileStatus);
            if (compileStatus == 0)
            {
                string infoLog = GL.GetShaderInfoLog(fragmentShader);
                Console.WriteLine(infoLog);
            }

            // Now link the shaders into a program.
            Handle = GL.CreateProgram();

            GL.AttachShader(Handle, vertexShader);
            GL.AttachShader(Handle, fragmentShader);

            // Check for linking errors.
            GL.LinkProgram(Handle);
            GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out int linkStatus);
            if (linkStatus == 0)
            {
                string infoLog = GL.GetProgramInfoLog(Handle);
                Console.WriteLine(infoLog);
            }

            // Detach and delete unwanted shaders.
            GL.DetachShader(Handle, vertexShader);
            GL.DetachShader(Handle, fragmentShader);
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

            // Get and store all uniform locations into a dictionary.
            GL.GetProgram(Handle, GetProgramParameterName.ActiveUniforms, out int numberOfUniforms);
            for (int i = 0; i < numberOfUniforms; i++)
            {
                string name = GL.GetActiveUniform(Handle, i, out _, out _);
                int location = GL.GetUniformLocation(Handle, name);
                uniformLocations.Add(name, location);
            }

            // Cache the shader for later use.
            shaderCache.Add((vertexPath, fragmentPath), this);
        }

        internal void SetMatrix4(string name, Matrix4 data)
        {
            GL.UseProgram(Handle);
            GL.UniformMatrix4(uniformLocations[name], true, ref data);
        }
    }
}
