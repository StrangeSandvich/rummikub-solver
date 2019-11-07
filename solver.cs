using System;
using System.Collections.Generic;
namespace rummikub_solver
{
  public class UnsolveableException : System.Exception
  {
    public UnsolveableException() { }
    public UnsolveableException(string message) : base(message) { }
    public UnsolveableException(string message, System.Exception inner) : base(message, inner) { }
    protected UnsolveableException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
  }
  public class RummikubSolver
  {

    public static Solution Solve(List<Tile> tiles)
    {
      Solution solution = new Solution(tiles);
      return RecursiveSolve(solution);
    }

    public static Solution RecursiveSolve(Solution solution)
    {
      while (solution.unprocessedTiles.Count > 0)
      {
        Connection nextConnect = solution.NextUnprocessed();
        Tile currentTile = nextConnect.tiles[0];
        try
        {
          while (true)
          {
            currentTile = solution.GetFollowingTile(currentTile);
            nextConnect.tiles.Add(currentTile);
            if (nextConnect.validate())
            {
              try
              {
                return RecursiveSolve(solution.Copy());
              }
              catch (UnsolveableException)
              {
                //Console.WriteLine("Recursive solve failed when passing on connection " + nextConnect.ToString());
                //Continue by adding another tile
              } //End catch
            } //End validate if
          } //End while
        } // End NoTileException try
        catch (NoTileException)
        {
          //Can't find any solution with the given tile as the first in a color combo. Put it in unused and return other tiles in connection to unproccessed
          //Console.WriteLine("Ran out of following tiles, shattering connection " + nextConnect.ToString());
          solution.Shatter(nextConnect);
        }
        //Then get the next tile
      } //End while
      //All unprocessed tiles put in unused or connections
      //Try to match up all the remaning tiles in number connections
      return SolveByNumber(solution);
    }

    //Take a solution with all tiles processed and try to fit all the unused tiles into number connections
    public static Solution SolveByNumber(Solution solution)
    {
      if(solution.unusedTiles.Count == 0){
        return solution;
      }
      List<List<Tile>> numberSeperated = new List<List<Tile>>();
      for (int i = 1; i <= 13; i++)
      {
          numberSeperated.Add(new List<Tile>());
      }
      foreach (Tile tile in solution.unusedTiles)
      {
          numberSeperated[tile.number-1].Add(tile);
      }
      foreach (List<Tile> seperation in numberSeperated)
      {
          solution.connections.AddRange(CreateNumberConnections(seperation));
      }
      solution.unusedTiles.Clear();
      //Double check it
      if(solution.Solved()){
        return solution;
      } else {
        throw new UnsolveableException();
      }
    }

    //Assumes all tiles have the same number
    public static List<Connection> CreateNumberConnections(List<Tile> tiles)
    {
      int number = tiles.Count;
      if (number == 5)
      {
        //No possible solution with 5 tiles. 
        throw new UnsolveableException();
      }
      else if (number == 0){
        //Nothing to do with this
        return new List<Connection>();
      }
      else if (number < 3){
        //Takes 3 to tango
        throw new UnsolveableException("Got less than 3 tiles for number " + tiles[0].number);
      }
      else if (number < 5)
      {
        //If less than 5, toss all in a connection and use the inbuilt validate to see if they all work
        Connection single = new Connection(tiles, true);
        if (single.validate())
        {
          return new List<Connection>() { single };
        }
        else
        {
          throw new UnsolveableException("Got less than 5 tiles but connection " + single.ToString() + " did not validate");
        }
      }
      else
      {
        Connection doubleFirst = new Connection(new List<Tile>(), true);
        Connection doubleSecond = new Connection(new List<Tile>(), true);
        foreach (Tile tile in tiles)
        {
          if(doubleFirst.containsTwin(tile)){
            doubleSecond.tiles.Add(tile);
          } 
          else if(doubleSecond.containsTwin(tile)){
            doubleFirst.tiles.Add(tile);
          } else if(doubleFirst.tiles.Count > doubleSecond.tiles.Count){
            doubleSecond.tiles.Add(tile);
          } else {
            doubleFirst.tiles.Add(tile);
          }
        }
        if(doubleFirst.validate() && doubleSecond.validate()){
          return new List<Connection>(){doubleFirst, doubleSecond};
        } else {
          throw new UnsolveableException();
        }
      }
    }
  }

  /* Old solution attempt
  List<Tile> greens = new List<Tile>();
    List<Tile> reds = new List<Tile>();
    List<Tile> blues = new List<Tile>();
    List<Tile> yellows = new List<Tile>();
    foreach (Tile tile in tiles)
    {
        if(tile.color == COLOR.Blue){
          blues.Add(tile);
        } else if(tile.color == COLOR.Green){
          greens.Add(tile);
        } else if(tile.color == COLOR.Red){
          reds.Add(tile);
        } else { //Tile is yellow
          yellows.Add(tile);
        }
    }
    var bluereturn = CreateConnectionByColor(blues);
    var greenreturn = CreateConnectionByColor(greens);
    var redreturn = CreateConnectionByColor(reds);
    var yellowreturn = CreateConnectionByColor(yellows);
    start.connections.AddRange(bluereturn.Item1);
    start.connections.AddRange(redreturn.Item1);
    start.connections.AddRange(greenreturn.Item1);
    start.connections.AddRange(yellowreturn.Item1);
    start.unusedTiles = bluereturn.Item2;
    start.unusedTiles.AddRange(redreturn.Item2);
    start.unusedTiles.AddRange(greenreturn.Item2);
    start.unusedTiles.AddRange(yellowreturn.Item2);

    //TODO fix up the unused tiles by number connections. 
    //TODO and then somehow fix the rest of the tiles in there. 
    return false;

  //Simple implementation. There can be multiple vaild groups of connections for a list of tiles. 
  public Tuple<List<Connection>, List<Tile>> CreateConnectionByColor(List<Tile> tiles){
    List<Tile> leftover = new List<Tile>();
    List<Connection> connections = new List<Connection>();

    tiles.Sort();
    while(tiles.Count > 0){
      Tile headTile = tiles[0];
      tiles.Remove(headTile);
      Connection connectionAttempt = new Connection(new List<Tile>(){headTile}, false);
      foreach (Tile tile in tiles){
          //Tile follows and is now new head tile. Remove from list. 
          if(tile.number - 1 == headTile.number){
          connectionAttempt.tiles.Add(tile);
          headTile = tile;
          tiles.Remove(headTile);
        } else if(tile.number == headTile.number){
          //Skip tile
          continue;
        } else {
          //No new tile will fit in
          break;
        }
      }
      //Test if the connection made is valid
      if(connectionAttempt.validate()){
        //Connection is good, add it to returns
        connections.Add(connectionAttempt);
      } else {
        //none of the tiles in the failed connection will be of use, add to leftovers
        leftover.AddRange(connectionAttempt.tiles);
      }
    } //while ends
    //All tiles have been assigned a connection or failed to and is left in leftovers
    return new Tuple<List<Connection>, List<Tile>>(connections, leftover);
  }
  */
}
