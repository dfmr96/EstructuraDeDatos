/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8_Grafos
{
    /*class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Programa Iniciado\n");

            // creo la estructura de grafos (estatica)
            GrafoMA grafoEst = new GrafoMA();

            // inicializo TDA
            grafoEst.InicializarGrafo();

            // vector de vértices
            int [] vertices = { 1, 2, 3, 4, 5, 6, 7, 8, 10 };
            // agrego los vértices
            for (int i = 0; i < vertices.Length; i++)
            {
                grafoEst.AgregarVertice(vertices[i]);
            }

            // vector de aristas - vertices origen
            int[] aristas_origen = { 1, 2, 1, 3, 3, 5, 6, 4, 8 };
            // vector de aristas - vertices destino
            int[] aristas_destino = { 2, 1, 3, 5, 4, 6, 5, 6, 10 };
            // vector de aristas - pesos
            int[] aristas_pesos = { 12, 10, 21, 9, 32, 12, 87, 10, 10 };
            // agrego las aristas
            for (int i = 0; i < aristas_pesos.Length; i++)
            {
                grafoEst.AgregarArista(aristas_origen[i], aristas_destino[i], aristas_pesos[i]);
            }

            Console.WriteLine("\nListado de Etiquetas de los nodos");
            for (int i = 0; i < grafoEst.Etiqs.Length; i++)
            {
                if (grafoEst.Etiqs[i] != 0)
                {
                    Console.WriteLine("Nodo: " + grafoEst.Etiqs[i].ToString());
                }
            }

            Console.WriteLine("\nListado de Aristas (Inicio, Fin, Peso)");
            for (int i = 0; i < grafoEst.cantNodos; i++)
            {
                for (int j = 0; j < grafoEst.cantNodos; j++)
                {
                    if (grafoEst.MAdy[i, j] != 0)
                    {
                        // obtengo la etiqueta del nodo origen, que está en las filas (i)
                        int nodoIni = grafoEst.Etiqs[i];
                        // obtengo la etiqueta del nodo destino, que está en las columnas (j)
                        int nodoFin = grafoEst.Etiqs[j];
                        Console.WriteLine(nodoIni.ToString() + ", " + nodoFin.ToString() + ", " + grafoEst.MAdy[i, j].ToString());
                    }
                }
            }

            Console.ReadKey();
        }
    }#1#
}
*/
