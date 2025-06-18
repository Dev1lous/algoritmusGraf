using System;
using System.Collections.Generic;

class Program
{
    static int V = 6; // počet vrcholů
    static List<(int dest, int weight)>[] graph = new List<(int, int)>[V];

    static void Main()
    {
        // Inicializace seznamu sousedů
        for (int i = 0; i < V; i++)
            graph[i] = new List<(int, int)>();

        // Naplnění grafu podle obrázku
        graph[0].Add((1, 10));
        graph[0].Add((3, 3));
        graph[1].Add((2, 2));
        graph[1].Add((4, 8));
        graph[2].Add((5, 4));
        graph[3].Add((5, 3));
        graph[4].Add((5, 1));
        graph[5].Add((4, 4));

        Console.Write("Zadej počáteční vrchol (0 - 5): ");
        int start = int.Parse(Console.ReadLine());

        Dijkstra(start);
    }

    static void Dijkstra(int start)
    {
        int[] distance = new int[V];
        int[] previous = new int[V];
        bool[] visited = new bool[V];

        for (int i = 0; i < V; i++)
        {
            distance[i] = int.MaxValue;
            previous[i] = -1; // -1 znamená žádného předchůdce
        }

        distance[start] = 0;
        var pq = new SortedSet<(int dist, int vertex)>();
        pq.Add((0, start));

        while (pq.Count > 0)
        {
            var (distU, u) = pq.Min;
            pq.Remove(pq.Min);

            if (visited[u]) continue;
            visited[u] = true;

            foreach (var (v, weight) in graph[u])
            {
                if (distance[v] > distance[u] + weight)
                {
                    distance[v] = distance[u] + weight;
                    previous[v] = u;
                    pq.Add((distance[v], v));
                }
            }
        }

        // Výpis výsledků
        Console.WriteLine($"\nNejkratší vzdálenosti a cesty z vrcholu {start}:");
        for (int i = 0; i < V; i++)
        {
            if (distance[i] == int.MaxValue)
            {
                Console.WriteLine($"Do vrcholu {i} = nedosažitelný");
            }
            else
            {
                Console.Write($"Do vrcholu {i} = {distance[i]}, cesta: ");
                PrintPath(i, previous);
                Console.WriteLine();
            }
        }
    }

    static void PrintPath(int v, int[] previous)
    {
        Stack<int> path = new Stack<int>();
        while (v != -1)
        {
            path.Push(v);
            v = previous[v];
        }

        Console.Write(string.Join(" → ", path));
    }
}
// dodělat body ale už chápu to, že se jedná o Dijkstrův algoritmus