using System;
using System.Collections.Generic;
namespace noghteKhat
{
    class Program
    {
        class player
        {
            string name;
            public int squareNumber;
            public player(string n)
            {
                squareNumber = 0;
                name = n;
            }
            public void getStatus()
            {
                Console.WriteLine("{0}:    {1}", name, squareNumber);
            }
        }
        class square
        {
            //int id;
            bool up, down, left, right;
            public bool isFull;//to check if a played won this square
            public square()
            {
                up = false;
                down = false;
                left = false;
                right = false;
                isFull = false;
            }
            public void markUp(){
                up=true;
            }
            public void markDown()
            {
                down = true;
            }
            public void markLeft()
            {
                left = true;
            }
            public void markRight()
            {
                right = true;
            }
            public bool isReady()//to check if two edges are marked 
            {
                if((up&&down&&!right&&!left)|| (up && !down && right && !left)|| (up && !down && !right && left))
                {
                    return true;
                }else if ((!up && down && right && !left)|| (!up && down && !right && left)|| (!up && !down && right && left))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            
        }
        class gameBoard
        {
            static int m = 2;
            static int k = 3;
            square[,] board = new square[m, k];
            bool[,] markedHedges = new bool[m + 1, k];
            bool[,] markedVedges = new bool[m, k + 1];
            int totalFullSquares;
            public void print()
            {
                for (int i = 0; i < m + 1; i++)
                {

                    for (int j = 0; j < k + 1; j++)
                    {

                        Console.Write(" {0} ", i * k + j);
                        if (j < k&&markedHedges[i,j])
                        {
                            Console.Write("---");
                        }
                        else
                        {
                            Console.Write("   ");
                        }
                    }
                    Console.WriteLine();
                    if (i < m)
                    {
                        for (int j = 0; j < k + 1; j++)
                        {
                            if (markedVedges[i, j])
                            {
                                Console.Write(" | ");
                            }
                            else
                            {
                                Console.Write("   ");
                            }
                            Console.Write("   ");
                        }
                    }
                }
            }
            public string isEdge(int start, int end)
            {
                if (start < end)
                {
                    if (end == start + 1)
                    {
                        return "horizontal";
                    }
                    else if (end == start + k)
                    {
                        return "vertical";
                    }
                    else
                    {
                        return "no";
                    }
                }
                else if (start > end)
                {
                    if (start == end + 1)
                    {
                        return "Rhorizontal";
                    }
                    else if (start == end + k)
                    {
                        return "Rvertical";
                    }
                    else
                    {
                        return "no";
                    }
                }
                else
                    return "no";
            }
            public (int, int) findEdgeIndex(int start, int end, string state)
            {
                if (state == "horizontal")
                {
                    return ((int)Math.Floor((double)start / k), start % k);
                }
                else if (state == "Rhorizontal")
                {
                    return ((int)Math.Floor((double)end / k), end % k );
                }
                else if (state == "vertical")
                {
                    return ((int)Math.Floor((double)start / k), start % k - 1);
                }
                else if (state == "Rvertical")
                {
                    return ((int)Math.Floor((double)end / k), end % k - 1);
                }
                else return (-1, -1);
            }
            public void MarkEdge(int start, int end, player currentPlayer)
            {
                string edge = isEdge(start, end);
                if (edge != "no")
                {
                    (int, int) edgeIndexes = findEdgeIndex(start, end, edge);
                    if (edge == "horizontal" || edge == "Rhorizontal")
                    {
                        if (!markedHedges[edgeIndexes.Item1, edgeIndexes.Item2])
                        {
                            markedHedges[edgeIndexes.Item1, edgeIndexes.Item2] = true;
                            if (edgeIndexes.Item1 - 1 > 0)
                            {

                                if (board[edgeIndexes.Item1 - 1, edgeIndexes.Item2].isReady())
                                {
                                    board[edgeIndexes.Item1 - 1, edgeIndexes.Item2].markDown();
                                    board[edgeIndexes.Item1 - 1, edgeIndexes.Item2].isFull = true;
                                    currentPlayer.squareNumber++;
                                }
                                else
                                {
                                    board[edgeIndexes.Item1 - 1, edgeIndexes.Item2].markDown();
                                }
                            }
                            if (edgeIndexes.Item1 < m)
                            {
                                if (board[edgeIndexes.Item1, edgeIndexes.Item2].isReady())
                                {
                                    board[edgeIndexes.Item1, edgeIndexes.Item2].markUp();
                                    board[edgeIndexes.Item1, edgeIndexes.Item2].isFull = true;
                                    currentPlayer.squareNumber++;
                                }
                                else
                                {
                                    board[edgeIndexes.Item1, edgeIndexes.Item2].markUp();
                                }
                            }
                            Console.WriteLine("edge was marked successfully.");
                        }
                        else
                        {
                            throw new Exception("this edge is already marked!");
                        }
                    }
                    if (edge == "vertical" || edge == "Rvertical")
                    {
                        if (!markedVedges[edgeIndexes.Item1, edgeIndexes.Item2])
                        {
                            markedVedges[edgeIndexes.Item1, edgeIndexes.Item2] = true;
                            if (edgeIndexes.Item2 - 1 > 0)
                            {

                                if (board[edgeIndexes.Item1, edgeIndexes.Item2 - 1].isReady())
                                {
                                    board[edgeIndexes.Item1, edgeIndexes.Item2 - 1].markRight();
                                    board[edgeIndexes.Item1, edgeIndexes.Item2 - 1].isFull = true;
                                    currentPlayer.squareNumber++;
                                }
                                else
                                {
                                    board[edgeIndexes.Item1, edgeIndexes.Item2 - 1].markRight();
                                }
                            }
                            if (edgeIndexes.Item2 < k)
                            {
                                if (board[edgeIndexes.Item1, edgeIndexes.Item2].isReady())
                                {
                                    board[edgeIndexes.Item1, edgeIndexes.Item2].markLeft();
                                    board[edgeIndexes.Item1, edgeIndexes.Item2].isFull = true;
                                    currentPlayer.squareNumber++;
                                }
                                else
                                {
                                    board[edgeIndexes.Item1, edgeIndexes.Item2].markLeft();
                                }
                            }
                            Console.WriteLine("edge was marked successfully.");
                        }
                        else
                        {
                            throw new Exception("this edge is already marked!");
                        }
                    }
                }
                else
                {
                    throw new Exception("this edge does not exist!");
                }
            }
            public bool end()
            {
                if (totalFullSquares < m * k)
                {
                    return false;
                }
                return true;
            }
            public gameBoard()
            {
                totalFullSquares = 0;
                
            }
        }
            static void Main(string[] args)
            {
            Console.WriteLine("start");
            Console.WriteLine("enter number of players");
            int n = int.Parse(Console.ReadLine());
            List < player> players= new List<player>();
            for(int i = 0; i < n;i++)
            {
                Console.WriteLine("enter player name");
                players.Add(new player(Console.ReadLine()));
            }
            gameBoard g = new gameBoard();
            int currentPlayerIndex = 0;
            while (!g.end())
            {
                bool valid = false;
                while (!valid)
                {
                try
                {
                        Console.WriteLine("enter edge start number:");
                        int start = int.Parse(Console.ReadLine());
                        Console.WriteLine("enter edge end number:");
                        int end = int.Parse(Console.ReadLine());
                        g.MarkEdge(start, end, players[currentPlayerIndex]);
                        valid = true;
                        g.print();
                }catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                }
                currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
            }
            if (g.end())
            {
                foreach (player p in players)
                {
                    p.getStatus();
                }
            }

            }
        
    }
}
