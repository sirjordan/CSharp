            /// <summary>
            /// 7. Find and print shortest path by BFS in graph
            /// </summary>
            /// <param name="g">Graph represented by matrix of succesors.</param>
            /// <param name="startEdge">Edge to start BFS</param>
            /// <param name="endEdge">Edge to be searched.</param>
            private static void FindPathBFS(Graph g, int startEdge, int endEdge)
            {
                Queue<int> edges = new Queue<int>();
                List<int> visitedEdges = new List<int>(startEdge);
                edges.Enqueue(startEdge);
                bool founded = false;
     
                while (edges.Count > 0 && !founded)
                {
                    int currentEdge = edges.Dequeue();
                    visitedEdges.Add(currentEdge);
                    if (endEdge == currentEdge)
                    {
                        Console.Write("Found: ");
                        founded = true;
                    }
     
                    IList<int> sucsesorsOfCurrentEdge = g.GetSuccessors(currentEdge);
                    foreach (int sucsesor in sucsesorsOfCurrentEdge)
                    {
                        // Avoid the infite loop in oriented cycle graph
                        if (!visitedEdges.Contains(sucsesor))
                        {
                            edges.Enqueue(sucsesor);
                        }
                    }
                }
     
                if (founded)
                {
                    // Remove the last and mark it as the last one
                    int lastElement = visitedEdges[visitedEdges.Count - 1];
                    visitedEdges.RemoveAt(visitedEdges.Count - 1);
     
                    // Delete that nodes that has not direct connection to the last in the order of visit.
                    for (int i = visitedEdges.Count - 1; i > 0; i--)
                    {
                        // If no relation to last
                        if (!(g.GetSuccessors(visitedEdges[i]).Contains(lastElement)))
                        {
                            visitedEdges.RemoveAt(i);
                        }
                        else
                        {
                            lastElement = visitedEdges[i];
                        }
                    }
     
                    // Put back the founded just to show on the console;
                    visitedEdges.Add(endEdge);
                    foreach (var el in visitedEdges)
                    {
                        Console.Write(el + " ");
                    }
                }
                else
                {
                    Console.WriteLine("Not founded!");
                }
            }

