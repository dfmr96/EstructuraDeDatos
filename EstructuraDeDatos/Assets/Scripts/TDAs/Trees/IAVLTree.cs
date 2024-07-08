using TDAs.Trees;

namespace Assets.Scripts.Estructura_de_Datos.Interfaces
{
    public interface IAVLTree
    {
        /// <summary>
        /// Inserta un nuevo nodo con el dato especificado en el árbol AVL.
        /// </summary>
        /// <param name="data">El dato del nuevo nodo a insertar.</param>
        void Insert(EventNode data);

        /// <summary>
        /// Elimina un nodo que contiene el dato especificado del árbol AVL.
        /// </summary>
        /// <param name="data">El dato del nodo a eliminar.</param>
        void Remove(EventNode data);

        /// <summary>
        /// Obtiene el nodo con el valor mínimo del árbol AVL.
        /// </summary>
        /// <returns>El nodo con el valor mínimo.</returns>
        AVLNode GetMin();
    }
}