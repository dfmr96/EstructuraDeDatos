using Assets.Scripts.Estructura_de_Datos;

namespace TDAs.Trees
{
    public class AVLNode
    {
        public EventNode Data;
        public AVLNode Left;
        public AVLNode Right;
        public int Height;

        public AVLNode(EventNode data)
        {
            Data = data;
            Left = null;
            Right = null;
            Height = 1;  // Los nodos se inicializan con altura 1
        }
    }

}