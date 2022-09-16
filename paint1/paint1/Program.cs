using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace paint1
{
    public class Game : GameWindow
    {
        int VertexBufferObject;
        Shader shader;
        int VertexArrayObject;


        float[] vertices = {
            -0.5f, -0.5f, 0.0f, //Bottom-left vertex
             0.5f, -0.5f, 0.0f, //Bottom-right vertex
             0.0f,  0.5f, 0.0f  //Top vertex
        };

        public Game(int width, int height, string title) : base(width, height, GraphicsMode.Default, title)
        {

        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            KeyboardState input = Keyboard.GetState();

            if (input.IsKeyDown(Key.Escape))
            {
                Exit();
            }

            base.OnUpdateFrame(e);
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            //Code goes here
            VertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            shader = new Shader("shader.vert", "shader.frag");
            VertexArrayObject = GL.GenVertexArray();

            // ..:: Initialization code (done once (unless your object frequently changes)) :: ..
            // 1. bind Vertex Array Object
            GL.BindVertexArray(VertexArrayObject);
            // 2. copy our vertices array in a buffer for OpenGL to use
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);
            // 3. then set our vertex attributes pointers
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);



            base.OnLoad(e);
        }

        protected override void OnUnload(EventArgs e)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(VertexBufferObject);
            base.OnUnload(e);
            shader.Dispose();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            //Code goes here.
            shader.Use();
            GL.BindVertexArray(VertexArrayObject);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            Context.SwapBuffers();
            base.OnRenderFrame(e);
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            base.OnResize(e);

        }



        static void Main(string[] args)
        {
            using (Game game = new Game(800, 600, "LearnOpenTK"))
            {
                game.Run(60.0);
            }
        }
    }

    public class Shader
    {
        int Handle;
        private int VertexShader;
        private int FragmentShader;
        private int shader;
        private int program;

        public Shader(string vertexPath, string fragmentPath)
        {
            string VertexShaderSource;

            using (StreamReader reader = new StreamReader(vertexPath, Encoding.UTF8))
            {
                VertexShaderSource = reader.ReadToEnd();
            }

            string FragmentShaderSource;

            using (StreamReader reader = new StreamReader(fragmentPath, Encoding.UTF8))
            {
                FragmentShaderSource = reader.ReadToEnd();
            }

            VertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(VertexShader, VertexShaderSource);

            FragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(FragmentShader, FragmentShaderSource);

            GL.CompileShader(VertexShader);

            GL.GetShader(shader, ShaderParameter.CompileStatus, out int success);
            if (success == 0)
            {
                string infoLog = GL.GetShaderInfoLog(VertexShader);
                System.Console.WriteLine(infoLog);
            }

            GL.CompileShader(FragmentShader);

            GL.GetShader(shader, ShaderParameter.CompileStatus, out int sucess);
            if (sucess == 0)
            {
                string infoLog = GL.GetShaderInfoLog(FragmentShader);
                System.Console.WriteLine(infoLog);
            }

            Handle = GL.CreateProgram();

            GL.AttachShader(Handle, VertexShader);
            GL.AttachShader(Handle, FragmentShader);

            GL.LinkProgram(Handle);

            GL.GetProgram(program, GetProgramParameterName.LinkStatus, out int succes);
            if (succes == 0)
            {
                string infoLog = GL.GetProgramInfoLog(program);
                Console.WriteLine(infoLog);
            }

            GL.DetachShader(Handle, VertexShader);
            GL.DetachShader(Handle, FragmentShader);
            GL.DeleteShader(FragmentShader);
            GL.DeleteShader(VertexShader);

        }

        public void Use()
        {
            GL.UseProgram(Handle);
        }

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                GL.DeleteProgram(Handle);

                disposedValue = true;
            }
        }

        ~Shader()
        {
            GL.DeleteProgram(Handle);
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }



    }
}
