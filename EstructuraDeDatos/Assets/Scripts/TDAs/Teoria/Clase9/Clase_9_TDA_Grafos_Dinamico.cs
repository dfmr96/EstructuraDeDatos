/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8_Grafos
{

    public interface GrafoTDA
    {
        void InicializarGrafo();
        void AgregarVertice(int v);
        void EliminarVertice(int v);
        ConjuntoTDA Vertices();
        void AgregarArista(int v1, int v2, int peso);
        void EliminarArista(int v1, int v2);
        bool ExisteArista(int v1, int v2);
        int PesoArista(int v1, int v2);
    }

    public class GrafoMA : GrafoTDA
    {
        static int n = 100;
        public int[,] MAdy;
        public int[] Etiqs;
        public int cantNodos;

        public void InicializarGrafo()
        {
            MAdy = new int[n, n];
            Etiqs = new int[n];
            cantNodos = 0;
        }

        public void AgregarVertice(int v)
        {
            Etiqs[cantNodos] = v;
            for (int i = 0; i <= cantNodos; i++)
            {
                MAdy[cantNodos, i] = 0;
                MAdy[i, cantNodos] = 0;
            }
            cantNodos++;
        }

        public void EliminarVertice(int v)
        {
            int ind = Vert2Indice(v);

            for (int k = 0; k < cantNodos; k++)
            {
                MAdy[k, ind] = MAdy[k, cantNodos - 1];
            }

            for (int k = 0; k < cantNodos; k++)
            {
                MAdy[ind, k] = MAdy[cantNodos - 1, k];
            }

            Etiqs[ind] = Etiqs[cantNodos - 1];
            cantNodos--;
        }

        private int Vert2Indice(int v)
        {
            int i = cantNodos - 1;
            while (i >= 0 && Etiqs[i] != v)
            {
                i--;
            }

            return i;
        }

        public ConjuntoTDA Vertices()
        {
            ConjuntoTDA Vert = new ConjuntoLD();
            Vert.InicializarConjunto();
            for (int i = 0; i < cantNodos; i++)
            {
                Vert.Agregar(Etiqs[i]);
            }
            return Vert;
        }

        public void AgregarArista(int v1, int v2, int peso)
        {
            int o = Vert2Indice(v1);
            int d = Vert2Indice(v2);
            MAdy[o, d] = peso;
        }

        public void EliminarArista(int v1, int v2)
        {
            int o = Vert2Indice(v1);
            int d = Vert2Indice(v2);
            MAdy[o, d] = 0;
        }

        public bool ExisteArista(int v1, int v2)
        {
            int o = Vert2Indice(v1);
            int d = Vert2Indice(v2);
            return MAdy[o, d] != 0;
        }

        public int PesoArista(int v1, int v2)
        {
            int o = Vert2Indice(v1);
            int d = Vert2Indice(v2);
            return MAdy[o, d];
        }
    }

    public class NodoGrafo
    {
        public int valorNodo;
        public NodoArista arista;
        public NodoGrafo sigNodo;
    }

    public class NodoArista
    {
        public int pesoArista;
        public NodoGrafo nodoDestino;
        public NodoArista sigArista;
    }

    public class GrafoLA : GrafoTDA
    {
        NodoGrafo origen;

        public void InicializarGrafo()
        {
            origen = null;
        }

        public void AgregarVertice( int v)
        {
            //El vertice se inserta al inicio de la lista de nodos
            NodoGrafo aux = new NodoGrafo();
            aux.valorNodo = v;
            aux.arista = null;
            aux.sigNodo = origen;
            origen = aux;
        }

        /*
        * Para agregar una nueva arista al grafo , primero se deben
        * buscar los nodos entre los cuales se va agregar la arista ,
        * y luego se inserta sobre la lista de adyacentes del nodo
        * origen (en este caso nombrado como v1)
        #1#

        public void AgregarArista(int v1, int v2, int peso)
        {
            NodoGrafo n1 = Vert2Nodo(v1);
            NodoGrafo n2 = Vert2Nodo(v2);
            //La nueva arista se inserta al inicio de la lista
            //de nodos adyacentes del nodo origen
            NodoArista aux = new NodoArista();
            aux.pesoArista = peso;
            aux.nodoDestino = n2;
            aux.sigArista = n1.arista;
            n1.arista = aux;
        }

        private NodoGrafo Vert2Nodo(int v)
        {
            NodoGrafo aux = origen;
            while (aux != null && aux.valorNodo != v)
            {
                aux = aux.sigNodo;
            }
            return aux;
        }

        public void EliminarVertice(int v)
        {
            //Se recorre la lista de v´ertices para remover el nodo v
            //y las aristas con este v´ertice.
            // Distingue el caso que sea el primer nodo
            if (origen.valorNodo == v)
            {
                origen = origen.sigNodo;
            }
            NodoGrafo aux = origen;
            while (aux != null)
            {
                // remueve de aux todas las aristas hacia v
                this.EliminarAristaNodo(aux, v);
                if (aux.sigNodo!= null && aux.sigNodo.valorNodo == v)
                {
                    //Si el siguiente nodo de aux es v, lo elimina
                    aux.sigNodo = aux.sigNodo.sigNodo;
                }
                aux = aux.sigNodo;
            }
        }

        /*
        * Si en las aristas del nodo existe
        * una arista hacia v, la elimina
        #1#
        private void EliminarAristaNodo(NodoGrafo nodo, int v)
        {
            NodoArista aux = nodo.arista;
            if (aux != null)
            {
                //Si la arista a eliminar es la primera en
                //la lista de nodos adyacentes
                if (aux.nodoDestino.valorNodo == v)
                {
                    nodo.arista = aux.sigArista;
                }
                else
                {
                    while (aux.sigArista!= null && aux.sigArista.nodoDestino.valorNodo != v)
                    {
                        aux = aux.sigArista;
                    }
                    if (aux.sigArista!= null)
                    {
                        // Quita la referencia a la arista hacia v
                        aux.sigArista = aux.sigArista.sigArista;
                    }
                }
            }
        }

        public ConjuntoTDA Vertices()
        {
            ConjuntoTDA c = new ConjuntoLD();
            c.InicializarConjunto();
            NodoGrafo aux = origen;
            while (aux != null)
            {
                c.Agregar(aux.valorNodo);
                aux = aux.sigNodo;
            }
            return c;
        }

        /*
        * Se elimina la arista que tiene como origen al v´ertice v1
        * y destino al v´ertice v2
        #1#
        public void EliminarArista(int v1, int v2)
        {
            NodoGrafo n1 = Vert2Nodo(v1);
            EliminarAristaNodo(n1, v2);
        }

        public bool ExisteArista(int v1, int v2)
        {
            NodoGrafo n1 = Vert2Nodo(v1);
            NodoArista aux = n1.arista;
            while (aux != null && aux.nodoDestino.valorNodo != v2)
            {
                aux = aux.sigArista;
            }
            //Solo si se encontro la arista buscada , aux no es null
            return aux != null;
        }

        public int PesoArista(int v1, int v2)
        {
            NodoGrafo n1 = Vert2Nodo(v1);
            NodoArista aux = n1.arista;
            while (aux.nodoDestino.valorNodo != v2)
            {
                aux = aux.sigArista;
            }
            //Se encontr´o la arista entre los dos nodos
            return aux.pesoArista;
        }
    }
 }
 */
