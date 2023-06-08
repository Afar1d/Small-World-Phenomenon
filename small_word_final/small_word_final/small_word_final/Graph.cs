using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace small_word_final
{
    class Graph
    {
        Dictionary<string, int> dist, vis, dist2;
        Dictionary<string, string> par, mov;
        Dictionary<string, int> strength;
        Dictionary<string, List<KeyValuePair<string, string>>> friend;
        Dictionary<Tuple<string, string>, int> act_movie;
        int id = 0;
        int numOfMovies = 0;


        public Graph(List<string> vertices, List<KeyValuePair<string, KeyValuePair<string, string>>> edges)
        {
            //objects
            dist = new Dictionary<string, int>();
            dist2 = new Dictionary<string, int>();
            par = new Dictionary<string, string>();
            mov = new Dictionary<string, string>();
            vis = new Dictionary<string, int>();
            strength = new Dictionary<string, int>();
            friend = new Dictionary<string, List<KeyValuePair<string, string>>>();
            act_movie = new Dictionary<Tuple<string, string>, int>();

            for (int i = 0; i < vertices.Count; i++)
            {
                friend.Add(vertices[i], new List<KeyValuePair<string, string>>());
                dist.Add(vertices[i], 0);
                dist2.Add(vertices[i], 0);
                par.Add(vertices[i], "");
                mov.Add(vertices[i], "");
                vis.Add(vertices[i], 0);
                strength.Add(vertices[i], 0);

            }
            //add edge between 2 actors
            foreach (var edge in edges)
            {
                string movie = edge.Key;
                string actor1 = edge.Value.Key;
                string actor2 = edge.Value.Value;
                // undirected gragh
                friend[actor1].Add(new KeyValuePair<string, string>(actor2, movie));
                friend[actor2].Add(new KeyValuePair<string, string>(actor1, movie));
                Tuple<string, string> act2_moive = new Tuple<string, string>(actor2, actor1);
                Tuple<string, string> act3_moive = new Tuple<string, string>(actor1, actor2);
                if (act_movie.ContainsKey(act2_moive))
                {
                    act_movie[act2_moive]++;
                }
                else if (act_movie.ContainsKey(act3_moive))
                {
                    act_movie[act3_moive]++;
                }
                else
                {
                    act_movie.Add(act2_moive, 1);
                }

            }
        }
        // to make the value of dictionaries equal 0 to relation 
        public void es(List<string> vertices)
        {
            dist = new Dictionary<string, int>();
            dist2 = new Dictionary<string, int>();
            par = new Dictionary<string, string>();
            mov = new Dictionary<string, string>();
            vis = new Dictionary<string, int>();
            strength = new Dictionary<string, int>();


            for (int i = 0; i < vertices.Count; i++)
            {

                dist.Add(vertices[i], 0);
                dist2.Add(vertices[i], 0);
                par.Add(vertices[i], "");
                mov.Add(vertices[i], "");
                vis.Add(vertices[i], 0);
                strength.Add(vertices[i], 0);

            }
        }

        //Dijkstra 
        public void Dijkstra(string actor1, string actor2)
        {
            id++;
            vis[actor1] = id;
            dist[actor1] = 0;

            // first = distance , second = actor
            SortedSet<KeyValuePair<int, string>> set = new SortedSet<KeyValuePair<int, string>>(new MyComparer());
            set.Add(new KeyValuePair<int, string>(0, actor1));
            // loop untill set become empty
            while (set.Count > 0)
            {
                int d = 0;
                string node = "";
                // loop for one time to get the fisrt element in set  to start from it
                foreach (var elem in set)
                {
                    d = elem.Key;
                    node = elem.Value;
                    set.Remove(elem);
                    break;
                }
                //if the value that sorted in node equal actore , then we calulate the shortest path betwwn them
                if (node.Equals(actor2))
                    break;
                // to limit time only
                if (d != dist[node])
                    continue;
                //loop for all niebour of node 
                foreach (var niebour in friend[node])
                {
                    // to update the shortest path -- the distance that updated put it in set 
                    if (par[niebour.Key] == "")
                    {
                        dist2[niebour.Key] = dist2[node] + 1;
                    }
                    if (dist[niebour.Key] > d + 1 || vis[niebour.Key] != id)
                    {
                        dist[niebour.Key] = d + 1;
                        par[niebour.Key] = node;
                        mov[niebour.Key] = niebour.Value; // to store the name of movie
                        vis[niebour.Key] = id;
                        set.Add(new KeyValuePair<int, string>(d + 1, niebour.Key));
                    }
                    // to updata relation
                    Tuple<string, string> ecc = new Tuple<string, string>(node, niebour.Key);
                    Tuple<string, string> ecc2 = new Tuple<string, string>(niebour.Key, node);
                    if (act_movie.ContainsKey(ecc))
                    {
                        numOfMovies = act_movie[ecc] + strength[node];
                        if (numOfMovies > strength[niebour.Key] && dist2[node] < dist2[niebour.Key])
                            strength[niebour.Key] = numOfMovies;
                    }
                    else
                    {
                        numOfMovies = act_movie[ecc2] + strength[node];
                        if (numOfMovies > strength[niebour.Key] && dist2[node] < dist2[niebour.Key])
                            strength[niebour.Key] = numOfMovies;
                    }
                }
            }


            //output-- deg.

            if (vis[actor2] == id)
                Console.Write(dist[actor2]+"\t");

            //rel
            Console.Write(" " + strength[actor2]);
            // chain
            List<string> shortestPath = new List<string>();
            string current = actor2;
            while (current != actor1)
            {
                shortestPath.Add(mov[current]);
                current = par[current];
            }

            //output--chain
            shortestPath.Reverse();

            Console.Write("\t" + shortestPath[0]);
            for (int i = 1; i < shortestPath.Count; i++)
                Console.Write(" => " + shortestPath[i]);



        }
    }
}

