using OpenTK.Input;
using Proyecto1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto1_01
{
    public class KeyboardInputManager
    {
        private Escenario escena;
        private Guion guion;
        public event Action OnEscapePressed;
        public KeyboardInputManager(Escenario escena, Guion guion)
        {
            this.escena = escena;
            this.guion = guion;
        }

        public void SubscribeToKeyboardEvents(Game game)
        {
            game.KeyDown += HandleKeyDown;
        }

        private void HandleKeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                OnEscapePressed?.Invoke();//invoke llama a todos los manejadores del evento OnEscapePressed, ? verifica que no es null
            }
            if (e.Key == Key.Number1)//left
            {
                escena.objetos["tele"].partes["tele"].Trasladar(-1, 0, 0);
            }
            if (e.Key == Key.Number2)//right
            {
                escena.objetos["tele"].partes["tele"].Trasladar(1, 0, 0);
            }
            if (e.Key == Key.Number3)//up
            {
                escena.objetos["tele"].partes["tele"].Trasladar(0, 1, 0);
            }
            if (e.Key == Key.Number4)//down
            {
                escena.objetos["tele"].partes["tele"].Trasladar(0, -1, 0);
            }
            if (e.Key == Key.Number5)//z positivo
            {
                escena.objetos["tele"].partes["tele"].Trasladar(0, 0, 1);
            }
            if (e.Key == Key.Number6)//z negativo
            {
                escena.objetos["tele"].partes["tele"].Trasladar(0, 0, -1);
            }
            if (e.Key == Key.Number7)//escalar a mas
            {
                escena.objetos["tele"].partes["tele"].Escalar(1.1f);
            }
            if (e.Key == Key.Number8)//escalar a menos
            {
                escena.objetos["tele"].partes["tele"].Escalar(0.9f);
            }
            if (e.Key == Key.Number9)//rotar en z
            {
                escena.objetos["florero"].partes["florero"].Rotar(-5, 'z');
               // escena.objetos["tele"].partes["parlante"].Rotar(-5, 'z');
               // escena.objetos["tele"].partes["tele"].Rotar(-5, 'z');
            }
            if (e.Key == Key.Number0)
            {
                escena.objetos["florero"].partes["florero"].Escalar(1.1f);
               // escena.objetos["parlante"].partes["parlante"].Escalar(1.1f);
               // escena.objetos["tele"].partes["tele"].Escalar(1.1f);
            }
            // -----------------------------------------
            // tele
            if (e.Key == Key.Keypad4)//Left
            {
                escena.objetos["tele"].Trasladar(-1, 0, 0);
            }
            if (e.Key == Key.Keypad6)//Right
            {
                escena.objetos["tele"].Trasladar(1, 0, 0);
            }

            if (e.Key == Key.Keypad2)//Up
            {
                escena.objetos["tele"].Trasladar(0, 1, 0);
            }
            if (e.Key == Key.Keypad8)//Down
            {
                escena.objetos["tele"].Trasladar(0, -1, 0);
            }
            if (e.Key == Key.KeypadPlus)//Escalar a mas
            {
                escena.objetos["tele"].Escalar(1.1f);
            }
            if (e.Key == Key.KeypadMinus)//Escalar a menos
            {
                escena.objetos["tele"].Escalar(0.9f);
            }
            if (e.Key == Key.KeypadMultiply)//Escalar a menos
            {
                escena.objetos["tele"].Rotar(-5, 'z');
            }

            if (e.Key == Key.G)//Left
            {
                escena.objetos["tele"].Rotar(-5, 'z');
            }
            if (e.Key == Key.F)//Left
            {
                escena.objetos["tele"].Rotar(-5, 'y');
            }
            if (e.Key == Key.H)//Left
            {
                escena.objetos["tele"].Rotar(5, 'x');
            }
            if (e.Key == Key.T)//Left
            {
                escena.objetos["tele"].Rotar(5, 'x');
            }
            //---------------------------------------------------
            //Escenario
            if (e.Key == Key.A)//Left
            {
                escena.Trasladar(-1, 0, 0);
            }
            if (e.Key == Key.D)//Right
            {
                escena.Trasladar(1, 0, 0);
            }
            if (e.Key == Key.W)//Up
            {
                escena.Trasladar(0, 1, 0);
            }
            if (e.Key == Key.S)//Down
            {
                escena.Trasladar(0, -1, 0);
            }
            if (e.Key == Key.Q)//Z positivo
            {
                escena.Trasladar(0, 0, 1);
            }
            if (e.Key == Key.E)//Z negativo
            {
                escena.Trasladar(0, 0, -1);
            }
            if (e.Key == Key.M)//Escalar a mas
            {
                escena.Escalar(1.1f);
            }
            if (e.Key == Key.N)//Escalar a menos
            {
                escena.Escalar(0.9f);
            }
            if (e.Key == Key.X)//Rotar
            {
                escena.Rotar(5, 'x');
            }
            if (e.Key == Key.Y)//Z positivo
            {
                escena.Rotar(5, 'y');
            }
            if (e.Key == Key.Z)//Z positivo
            {
                escena.Rotar(5, 'z');
            }
            if (e.Key == Key.P)
            {
                guion.Iniciar();
            }
        }
    }
}
