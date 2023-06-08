using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace small_word_final
{

    class Program
    {

        static void Main(string[] args)
        {

            HashSet<string> vertHash = new HashSet<string>();
            List<KeyValuePair<string, KeyValuePair<string, string>>> edges = new List<KeyValuePair<string, KeyValuePair<string, string>>>();

            int t;

            Console.Write("Enter 0 for exit, 1 for run the code: \n");
            t = Convert.ToInt32(Console.ReadLine());

            string moviepath = @"C:\Users\Ahmed M. Farid\Desktop\Algo project\Testcases\sample\movies1.txt";
            string querypath = @"C:\Users\Ahmed M. Farid\Desktop\Algo project\Testcases\sample\queries1.txt";
            if (t != 0)
            {

                var watch = new System.Diagnostics.Stopwatch();
                watch.Start();
                string[] lines = File.ReadAllLines(moviepath);
                foreach (string line in lines)
                {
                    string[] arr = line.Split('/');
                    for (int i = 1; i < arr.Length; i++)
                    {
                        vertHash.Add(arr[i]);
                        for (int j = i + 1; j < arr.Length; j++)
                        {
                            edges.Add(new KeyValuePair<string, KeyValuePair<string, string>>(arr[0], new KeyValuePair<string, string>(arr[i], arr[j])));

                        }
                    }
                }

                Graph graph = new Graph(new List<string>(vertHash), edges);
                ///////////////////////////////////////////////////////////////////
                string[] querylines = File.ReadAllLines(querypath);
                Console.Write("Deg. \t Rel. \t Chain\n");
                foreach (string queryline in querylines)
                {
                    string[] actors = queryline.Split('/');
                    graph.es(new List<string>(vertHash));
                    graph.Dijkstra(actors[0], actors[1]);
                    Console.WriteLine();

                }

                Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");


            }

        }
    }
}
