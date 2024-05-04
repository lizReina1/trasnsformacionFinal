using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Proyecto1_01;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1
{
    public class Game : GameWindow
    {
        Escenario escena2 = new Escenario();
        Guion guion = new Guion();

        private Camera camera;
        //-----------------------------------------------------------------------------------------------------------------
        public Game(int width, int height, string title) : base(width, height, GraphicsMode.Default, title) {}
        //-----------------------------------------------------------------------------------------------------------------
        
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            camera.Update(e);
            base.OnUpdateFrame(e);
        }
        //-----------------------------------------------------------------------------------------------------------------
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(Color4.LightCyan);
            //Utilidades.Guardar<Escenario>(escena2, "escenafinal.json");
            escena2 = new Escenario(Utilidades.Cargar<Escenario>("escenafinal.json"));
            CargarCamara();
            SuscribirEventosDeTeclado();
            CargarGuion();
        }
        
        //-----------------------------------------------------------------------------------------------------------------
        protected override void OnUnload(EventArgs e)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            base.OnUnload(e);
        }
        //-----------------------------------------------------------------------------------------------------------------
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            //GL.DepthMask(true);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);
            GL.LoadIdentity();

            Camara();
            Ejes();
            escena2.Dibujar();

            Context.SwapBuffers();
            base.OnRenderFrame(e);
        }
        //-----------------------------------------------------------------------------------------------------------------
        protected override void OnResize(EventArgs e)
        {
            float d = 50;//80
            GL.Viewport(0, 0, Width, Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-d, d, -50, 50, -d, d);//16:9
          //  GL.Frustum(-80, 80, -80, 80, 4, 100);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            base.OnResize(e);
        }
        private void Ejes()
        {
            // Dibuja los ejes de coordenadas
            GL.LineWidth(2.0f); // Cambia 2.0f al grosor deseado
            GL.Begin(PrimitiveType.Lines);

            // Eje X (rojo)
            GL.Color3(Color.Red); // Rojo
            GL.Vertex3(0.0f, 0.0f, 0.0f); // Origen
            GL.Vertex3(80f, 0.0f, 0.0f); // Punto en X positivo

            // Eje Y (verde)
            GL.Color3(Color.Green); // Verde
            GL.Vertex3(0.0f, 0.0f, 0.0f); // Origen
            GL.Vertex3(0.0f, 80f, 0.0f); // Punto en Y positivo

            // Eje Z (azul)
            GL.Color3(Color.Purple); // Azul
            GL.Vertex3(0.0f, 0.0f, 0.0f); // Origen
            GL.Vertex3(0.0f, 0.0f, 80f); // Punto en Z positivo

            GL.End();
        }
        private void SuscribirEventosDeTeclado()
        {
            var keyboardInputManager = new KeyboardInputManager(escena2, guion);
            keyboardInputManager.OnEscapePressed += () =>
            {
                Exit();
            };
            keyboardInputManager.SubscribeToKeyboardEvents(this);
        }
        private void Camara()
        {
            // Configura la matriz de vista (View Matrix) para cambiar la vista
            Matrix4 viewMatrix = camera.GetViewMatrix();
            // Configura la matriz de vista
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref viewMatrix);
        }
        private void CargarCamara()
        {
            // Configura la cámara con la posición inicial y velocidad
            Vector3 initialPosition = new Vector3(0, 0, 3); // Posición inicial
            Vector3 initialFront = new Vector3(0, 0, -1);  // Dirección hacia donde mira
            Vector3 initialUp = Vector3.UnitY; // Vector "arriba"
            float cameraSpeed = 0.08f; // Velocidad de movimiento
            camera = new Camera(initialPosition, initialFront, initialUp, cameraSpeed);
        }
        private void CargarGuion()
        {
            Punto centroTele = new Punto(escena2.objetos["tele"].origin);
            Punto centroTeleDelante = new Punto(centroTele.x + 16, centroTele.y, centroTele.z);
            Punto centroTeleAtras = new Punto(centroTele.x - 16, centroTele.y, centroTele.z);



            Action adelante = () => escena2.objetos["tele"].Trasladar(1, 0, 0);
            Action adelante_lento = () => escena2.objetos["tele"].Trasladar(0.5f, 0, 0);
            Action atras = () => escena2.objetos["tele"].Trasladar(-1, 0, 0);
            Action arriba = () => escena2.objetos["tele"].Trasladar(0, 1, 0);
            Action abajo = () => escena2.objetos["tele"].Trasladar(0, -1, 0);

            Action rotar_pos = () => escena2.objetos["tele"].Rotar(-5, 'z', centroTeleDelante);
            Action rotar_neg = () => escena2.objetos["tele"].Rotar(5, 'z', centroTeleAtras);


            Action tele_x = () => escena2.objetos["tele"].partes["tele"].Rotar(-5, 'x');
            Action florero_x = () => escena2.objetos["florero"].partes["florero"].Rotar(-5, 'x');
            Action parlante_x = () => escena2.objetos["parlante"].partes["parlante"].Rotar(-5, 'x');

            Action tele_y = () => escena2.objetos["tele"].partes["tele"].Rotar(-5, 'y');
            Action florero_y = () => escena2.objetos["florero"].partes["florero"].Rotar(-5, 'y');
            Action parlante_y = () => escena2.objetos["parlante"].partes["parlante"].Rotar(-5, 'y');

            Action tele_z = () => escena2.objetos["tele"].partes["tele"].Rotar(-5, 'z');
            Action florero_z = () => escena2.objetos["florero"].partes["florero"].Rotar(-5, 'z');
            Action parlante_z = () => escena2.objetos["parlante"].partes["parlante"].Rotar(-5, 'z');


            Action tele_x_neg = () => escena2.objetos["tele"].partes["tele"].Rotar(5, 'x');
            Action florero_x_neg = () => escena2.objetos["tele"].partes["florero"].Rotar(5, 'x');
            Action parlante_x_neg = () => escena2.objetos["tele"].partes["parlante"].Rotar(5, 'x');

            Action tele_y_neg = () => escena2.objetos["tele"].partes["tele"].Rotar(5, 'y');
            Action florero_y_neg = () => escena2.objetos["tele"].partes["florero"].Rotar(5, 'y');
            Action parlante_y_neg = () => escena2.objetos["tele"].partes["parlante"].Rotar(5, 'y');

            Action tele_z_neg = () => escena2.objetos["tele"].partes["tele"].Rotar(5, 'z');
            Action florero_z_neg = () => escena2.objetos["tele"].partes["florero"].Rotar(5, 'z');
            Action parlante_z_neg = () => escena2.objetos["tele"].partes["parlante"].Rotar(5, 'z');


            // tele
            Action tele_mover_x = () => escena2.objetos["tele"].partes["tele"].Trasladar(1, 0, 0);
            Action tele_mover_x_neg = () => escena2.objetos["tele"].partes["tele"].Trasladar(-1, 0, 0);

            Action tele_mover_z = () => escena2.objetos["tele"].partes["tele"].Trasladar(0, 0, 1);
            Action tele_mover_z_neg = () => escena2.objetos["tele"].partes["tele"].Trasladar(0, 0, -1f);

            Action tele_mover_y = () => escena2.objetos["tele"].partes["tele"].Trasladar(0, 1, 0);
            Action tele_mover_y_neg = () => escena2.objetos["tele"].partes["tele"].Trasladar(0, -1f, 0);

            // parlante
            Action parlante_mover_x = () => escena2.objetos["tele"].partes["parlante"].Trasladar(1, 0, 0);
            Action parlante_mover_x_neg = () => escena2.objetos["tele"].partes["parlante"].Trasladar(-1, 0, 0);

            Action parlante_mover_z = () => escena2.objetos["tele"].partes["parlante"].Trasladar(0, 0, 1);
            Action parlante_mover_z_neg = () => escena2.objetos["tele"].partes["parlante"].Trasladar(0, 0, -1f);

            Action parlante_mover_y = () => escena2.objetos["tele"].partes["parlante"].Trasladar(0, 1, 0);
            Action parlante_mover_y_neg = () => escena2.objetos["tele"].partes["parlante"].Trasladar(0, -1f, 0);

            //florero
            Action florero_mover_x = () => escena2.objetos["tele"].partes["florero"].Trasladar(1, 0, 0);
            Action florero_mover_x_neg = () => escena2.objetos["tele"].partes["florero"].Trasladar(-1, 0, 0);

            Action florero_mover_z = () => escena2.objetos["tele"].partes["florero"].Trasladar(0, 0, 1);
            Action florero_mover_z_neg = () => escena2.objetos["tele"].partes["florero"].Trasladar(0, 0, -1f);

            Action florero_mover_y = () => escena2.objetos["tele"].partes["florero"].Trasladar(0, 1, 0);
            Action florero_mover_y_neg = () => escena2.objetos["tele"].partes["florero"].Trasladar(0, -1f, 0);

          
            Accion acto1 = new Accion();
            acto1.objeto.Add(adelante);

            Accion acto2 = new Accion();
            acto2.objeto.Add(rotar_pos);
            acto2.objeto.Add(adelante_lento);
            acto2.objeto.Add(abajo);

            Accion acto3 = new Accion();
            acto3.objeto.Add(rotar_neg);
         
            Accion acto4 = new Accion();
            acto4.objeto.Add(tele_mover_z);
            acto4.objeto.Add(tele_mover_y_neg);
            acto4.objeto.Add(tele_z_neg);

            acto4.objeto.Add(parlante_mover_z);
            acto4.objeto.Add(parlante_mover_y_neg);
            acto4.objeto.Add(parlante_z_neg);

            acto4.objeto.Add(florero_mover_z);
            acto4.objeto.Add(florero_mover_y_neg);
            acto4.objeto.Add(florero_z_neg);
            

            Accion acto5 = new Accion();
            acto5.objeto.Add(tele_mover_x_neg);
            acto5.objeto.Add(parlante_mover_x_neg);
            acto5.objeto.Add(florero_mover_x_neg);

            acto5.objeto.Add(tele_mover_z);
            acto5.objeto.Add(parlante_mover_z);
            acto5.objeto.Add(florero_mover_z_neg);

            acto5.objeto.Add(tele_z_neg);
            acto5.objeto.Add(parlante_z_neg);
            acto5.objeto.Add(florero_z_neg);

            //Cargar Actos
            guion.AgregarAccion(acto1.objeto, 50);//Adelante
            guion.AgregarAccion(acto2.objeto, 50);//caida
            guion.AgregarAccion(acto3.objeto, 15);//rectificacion
            guion.AgregarAccion(acto4.objeto, 10);
            guion.AgregarAccion(acto5.objeto, 40);
            

        }
    }
}
