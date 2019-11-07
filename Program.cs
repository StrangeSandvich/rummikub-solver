using System;
using System.Collections.Generic;

namespace rummikub_solver
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Tile> inputTiles = new List<Tile>();
            Console.WriteLine("Welcome to Rummikub Solver. Input tiles.");
            while(true){
                string input = Console.ReadLine();
                if(input == "solve"){
                    break;
                } else {
                    try
                    {
                        inputTiles.Add(new Tile(input));
                    }
                    catch (System.ArgumentException)
                    {
                        Console.WriteLine("Could not recognize tile. Try again");
                    }
                }
            }
            Console.WriteLine("Solving...");
            try{
                Solution solution = RummikubSolver.Solve(inputTiles);
                Console.WriteLine("Solved:");
                Console.WriteLine(solution.ToString());
            } catch (UnsolveableException){
                Console.WriteLine("No solution found");
            }
            Console.WriteLine("Goodbye!");
        }

        static List<Tile> StringListToTileList(List<String> strings){
            List<Tile> result = new List<Tile>();
            foreach (string input in strings)
            {
                result.Add(new Tile(input));
            }
            return result;
        }
    }
}
