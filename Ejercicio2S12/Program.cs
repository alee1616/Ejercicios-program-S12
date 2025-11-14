// Escriba programa que tenga un método que dado un árbol binario devuelva
    // un "espejo" del mismo. El resultado final lo debe imprimir en sus tres
    // recorridos: inorden, preorden y postorden.
    // Nota: Convertir un árbol dado en su imagen espejo, significa intercambiar la
    // posición de sus nodos izquierdo y derecho:

    using System;
    using System.Linq;

    // Estructura básica de un nodo para nuestro árbol.
    public class Nodo
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

    public class ArbolBinario
    {
        public Nodo raiz;

        public ArbolBinario()
        {
            raiz = null;
        }

        // Función auxiliar para contar nodos (ayuda a balancear la inserción).
        public int ContarNodos(Nodo nodo)
        {
            if (nodo == null) return 0;
            return 1 + ContarNodos(nodo.izquierdo) + ContarNodos(nodo.derecho);
        }

        // Lógica de inserción para construir el árbol.
        public void Insertar(int dato)
        {
            raiz = InsertarRecursivo(raiz, dato);
        }

        private Nodo InsertarRecursivo(Nodo actual, int dato)
        {
            if (actual == null) return new Nodo(dato);

            // Intento de inserción simple para construir el árbol.
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

        //  * Método para el Problema 2: Obtener Espejo *

        // Función recursiva que transforma el árbol en su imagen espejo.
        public Nodo ObtenerEspejo(Nodo nodo)
        {
            if (nodo == null)
            {
                return nodo; // Caso base: si el nodo es nulo, no hay nada que invertir.
            }

            // 1. Llamada recursiva: pido que se inviertan primero los subárboles (de abajo hacia arriba).
            Nodo espejoIzquierdo = ObtenerEspejo(nodo.izquierdo);
            Nodo espejoDerecho = ObtenerEspejo(nodo.derecho);

            // 2. Intercambio: ¡Hago el switch en el nodo actual!
            nodo.izquierdo = espejoDerecho; // El nuevo izquierdo es el viejo derecho invertido.
            nodo.derecho = espejoIzquierdo;  // El nuevo derecho es el viejo izquierdo invertido.

            return nodo; // Devuelvo el nodo ya modificado.
        }

        
        //  * Métodos de Recorrido para la Verificación *
        
        // 1. Recorrido Inorden (Izquierda -> Raíz -> Derecha).
        public void RecorridoInorden(Nodo nodo)
        {
            if (nodo != null)
            {
                RecorridoInorden(nodo.izquierdo);
                Console.Write(nodo.dato + " ");
                RecorridoInorden(nodo.derecho);
            }
        }

        // 2. Recorrido Preorden (Raíz -> Izquierda -> Derecha).
        public void RecorridoPreorden(Nodo nodo)
        {
            if (nodo != null)
            {
                Console.Write(nodo.dato + " "); // ¡La Raíz primero!
                RecorridoPreorden(nodo.izquierdo);
                RecorridoPreorden(nodo.derecho);
            }
        }

        // 3. Recorrido Postorden (Izquierda -> Derecha -> Raíz).
        public void RecorridoPostorden(Nodo nodo)
        {
            if (nodo != null)
            {
                RecorridoPostorden(nodo.izquierdo);
                RecorridoPostorden(nodo.derecho);
                Console.Write(nodo.dato + " "); // ¡La Raíz al final!
            }
        }
    }

public class Program
{
    // Función para solicitar datos de entrada y construir el árbol.
    static ArbolBinario SolicitarYConstruirArbol()
    {
        ArbolBinario arbol = new ArbolBinario();
        Console.WriteLine("\n/// Solicitud de Datos ///");
        Console.WriteLine("Ingresa VARIOS números separados por comas, espacios o punto y coma (ej: 1, 2, 3, 4, 5, 6, 7):");
        string entrada = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(entrada))
        {
            Console.WriteLine("No se ingresaron datos. Creando un árbol vacío.");
            return arbol;
        }

        // Separo los números de la entrada.
        string[] valores = entrada.Split(new char[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);

        // Inserto cada número para construir el árbol.
        foreach (var val in valores)
        {
            if (int.TryParse(val.Trim(), out int numero))
            {
                arbol.Insertar(numero);
            }
        }
        return arbol;
    }

    public static void Main(string[] args)
    {
        Console.WriteLine("/// Creación de la Imagen Espejo (C#) ///");

        // 1. Solicitamos los datos y construimos el árbol original.
        ArbolBinario miArbol = SolicitarYConstruirArbol();

        Console.WriteLine("\n>>> Árbol Original <<<");
        Console.Write("Original (Inorden): ");
        miArbol.RecorridoInorden(miArbol.raiz);

        // 2. ¡Llamamos al método que lo convierte en espejo!
        // Al modificar miArbol.raiz, el árbol completo se transforma.
        miArbol.raiz = miArbol.ObtenerEspejo(miArbol.raiz);

        Console.WriteLine("\n\n/// Resultado: Árbol Espejo ///");
        Console.WriteLine("¡El árbol ha sido convertido a su imagen espejo!");

        // 3. Imprimimos el resultado final en los tres recorridos solicitados.
        Console.Write("\n1. Recorrido **Inorden**: ");
        miArbol.RecorridoInorden(miArbol.raiz);

        Console.Write("\n2. Recorrido **Preorden**: ");
        miArbol.RecorridoPreorden(miArbol.raiz);

        Console.Write("\n3. Recorrido **Postorden**: ");
        miArbol.RecorridoPostorden(miArbol.raiz);

        Console.WriteLine("\n\n¡Proceso finalizado! Presiona cualquier tecla para salir.");
        Console.ReadKey();
    }
}