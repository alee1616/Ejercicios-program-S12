// 1) Escribir el programa que tenga una función recursiva que encuentre el
// número de nodos de un árbol binario. Se debe imprimir el árbol en recorrido
// preorden y a continuación mostrar la cantidad de nodos del árbol.
// (Importante: se deben solicitar los datos como entrada para crear el árbol).

using System;
using System.Linq;

// La unidad básica del árbol. Cada nodo guarda un 'dato' y 
// referencias a sus hijos.
// Estructura básica de un nodo para nuestro árbol.
//El código define tres "planos" o "moldes" (clases) para funcionar:
public class Nodo
// clase que representa un nodo del árbol binario
//Cada Nodo es una pieza del árbol.
//Simplemente almacena un número (dato) y mantiene referencias (enlaces)
// a sus dos posibles hijos (izquierdo y derecho). Si un hijo es null,
// significa que no tiene hijo en esa dirección.
{
    public int dato;
    public Nodo izquierdo;
    public Nodo derecho;

    public Nodo(int item)
    {
        dato = item;
        izquierdo = derecho = null;
    }
}
//Esta clase "gestiona" todos los nodos y sabe dónde está el primero de todos.
//Su trabajo es guardar la raiz. Todas las operaciones 
//(contar, imprimir, insertar) comienzan desde esta raiz.
public class ArbolBinario
{
    public Nodo raiz;

    public ArbolBinario()
    {
        raiz = null;
    }

    // Función recursiva para contar el número total de nodos. ¡Esta es la función clave!
    public int ContarNodos(Nodo nodo)
    //Esta es la función recursiva que pedía el problema. 
    //¿Qué es la recursividad? Es una técnica donde una función 
    //se llama a sí misma para resolver una versión más pequeña del 
    // mismo problema, hasta que llega a un "caso base" simple.

    //Caso Base: Si el nodo que me pasas es null (es decir,
    //  llegué a una rama vacía, un callejón sin salida), no hay nada que contar. 
    // Devuelvo 0.
    {
        // Caso base: si es nulo, no suma nada.
        if (nodo == null)
        {
            return 0;
        }
        // Retorno: 1 (por este nodo) + el conteo de la izquierda + el conteo de la derecha.
        //Caso Recursivo: Si el nodo sí existe, la cuenta total es:
        //1 (para contarme a mí mismo, el nodo actual).
        //+ todo lo que cuente mi sub-árbol izquierdo (ContarNodos(nodo.izquierdo)).
        //+ todo lo que cuente mi sub-árbol derecho (ContarNodos(nodo.derecho)).
        return 1 + ContarNodos(nodo.izquierdo) + ContarNodos(nodo.derecho); 
    }

    // Función para el recorrido Preorden (Raíz -> Izquierda -> Derecha).
    public void RecorridoPreorden(Nodo nodo)
    //public void RecorridoPreorden(Nodo nodo)
    //Esta función imprime el árbol en un orden específico: Raíz -> Izquierda -> Derecha.
    //Console.Write(nodo.dato + " ");
    //Visita la Raíz: Primero, imprime el dato del nodo actual.
    //RecorridoPreorden(nodo.izquierdo);
    //Ve a la Izquierda: Llama a esta misma función para todo el sub-árbol izquierdo.
    //RecorridoPreorden(nodo.derecho);
    //Ve a la Derecha: Cuando la izquierda termine, llama a esta misma función para todo el sub-árbol derecho.
    {
        if (nodo != null)
        {
            Console.Write(nodo.dato + " "); // 1. Imprimo (Visito la Raíz)
            RecorridoPreorden(nodo.izquierdo); // 2. Voy a la Izquierda
            RecorridoPreorden(nodo.derecho); // 3. Voy a la Derecha
        }
    }

    // --- Lógica de Inserción (para construir el árbol) ---

    // Método público que llamas para añadir un dato.
    public void Insertar(int dato)
    {
        raiz = InsertarRecursivo(raiz, dato);
    }

    // El método auxiliar que cuenta y decide dónde poner el nuevo dato para que crezca.
    private Nodo InsertarRecursivo(Nodo actual, int dato)
    {
        if (actual == null)
        {
            return new Nodo(dato);
        }

        // Simplemente decidimos dónde insertar basado en qué lado tiene menos nodos.
        if (ContarNodos(actual.izquierdo) <= ContarNodos(actual.derecho))
        {
            actual.izquierdo = InsertarRecursivo(actual.izquierdo, dato);
        }
        else
        {
            actual.derecho = InsertarRecursivo(actual.derecho, dato);
        }

        return actual;
    }
}

public class Program
{
    // Esta función maneja la entrada de múltiples números del usuario.
    static ArbolBinario SolicitarYConstruirArbol()
    {
        ArbolBinario arbol = new ArbolBinario();
        Console.WriteLine("\n>>> Solicitud de Datos <<<");
        // Aquí es donde pedimos todos los números.
        Console.WriteLine("¡IMPORTANTE! Ingresa VARIOS números separados por comas, espacios o punto y coma (ej: 50, 20, 70, 10, 35):");
        string entrada = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(entrada))
        {
            Console.WriteLine("No se ingresaron datos. El árbol está vacío.");
            return arbol;
        }

        // Usamos Split para dividir la cadena de texto en un array de strings, usando varios delimitadores.
        string[] valores = entrada.Split(new char[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);

        Console.WriteLine($"Procesando {valores.Length} valores para construir el árbol...");

        // Usamos un bucle para insertar CADA número individualmente.
        foreach (var val in valores)
        {
            if (int.TryParse(val.Trim(), out int numero))
            {
                arbol.Insertar(numero); // Llama a la lógica recursiva de inserción por cada número.
            }
        }
        return arbol;
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("/// Problema 1: Conteo de Nodos con Función Recursiva ///");

        // 1. Solicitamos los datos y construimos el árbol.
        ArbolBinario miArbol = SolicitarYConstruirArbol();

        Console.WriteLine("\n>>> Resultado <<<");

        // 2. Imprimimos el árbol en recorrido Preorden.
        Console.Write("Recorrido Preorden del Árbol: ");
        miArbol.RecorridoPreorden(miArbol.raiz);

        // 3. Mostramos la cantidad de nodos.
        int totalNodos = miArbol.ContarNodos(miArbol.raiz);
        Console.WriteLine($"\nCantidad Total de Nodos: **{totalNodos}**");

        Console.WriteLine("\nPresiona cualquier tecla para finalizar...");
        Console.ReadKey();
    }
}
