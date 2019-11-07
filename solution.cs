using System;
using System.Collections.Generic;

namespace rummikub_solver
{
  public class NoTileException : System.Exception
  {
      public NoTileException() { }
      public NoTileException(string message) : base(message) { }
      public NoTileException(string message, System.Exception inner) : base(message, inner) { }
      protected NoTileException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
  }
  public class Solution
  {
    public List<Tile> allTiles { get; private set; }
    public List<Tile> unusedTiles { get; set; }
    public List<Tile> unprocessedTiles { get; set; }
    public List<Connection> connections { get; set; }
    public bool solved { get; private set; }

    public Solution(List<Tile> tiles)
    {
      allTiles = tiles;
      unprocessedTiles = new List<Tile>(tiles);
      unusedTiles = new List<Tile>();
      connections = new List<Connection>();
      solved = false;
    }

    //Terribly innefficient but yeah whatever
    public override string ToString(){
      string result = "Connections: ";
      foreach (Connection connection in connections)
      {
          result += connection.ToString();
          result += " ";
      }
      return result;
    }
    /*Take the smallest unprocessed tile and create a connection with just it to return*/
    public Connection NextUnprocessed(){
      unprocessedTiles.Sort();
      Tile nextTile = unprocessedTiles[0];
      unprocessedTiles.Remove(nextTile);
      Connection nextConnection = new Connection(new List<Tile>(){nextTile}, false);
      connections.Add(nextConnection);
      return nextConnection;
    }

    /*Take some bad connection, put the smallest tile in unused and return the rest to unprocessed */
    public void Shatter(Connection connection){
      connections.Remove(connection);
      connection.tiles.Sort();
      Tile front = connection.tiles[0];
      unusedTiles.Add(front);
      unprocessedTiles.AddRange(connection.tiles.GetRange(1, connection.tiles.Count-1));
    }

    public Tile GetFollowingTile(Tile former){
      foreach (Tile candidate in unprocessedTiles){
          if(candidate.color == former.color && candidate.number == (former.number + 1)){
            unprocessedTiles.Remove(candidate);
            return candidate;
          }
      }
      throw new NoTileException();
    }

    public Solution Copy(){
      Solution copy = new Solution(new List<Tile>());
      copy.allTiles = new List<Tile>(allTiles);
      copy.unprocessedTiles = new List<Tile>(unprocessedTiles);
      copy.unusedTiles = new List<Tile>(unusedTiles);
      copy.solved = solved;
      copy.connections = new List<Connection>(connections);
      return copy;
    }

    public bool Solved()
    {
      solved = NonMutateSolved();
      return solved;
    }

    private bool NonMutateSolved()
    {
      //Ensure all tiles are in a connection
      if (unusedTiles.Count != 0 || unprocessedTiles.Count != 0)
      {
        return false;
      }
      //Ensure all connections are valid
      foreach (Connection conn in connections)
      {
        if (conn.validate() == false)
        {
          return false;
        }
      }
      //Collect all tiles in the connections
      List<Tile> tilesInConn = new List<Tile>();
      foreach (Connection conn in connections)
      {
        tilesInConn.AddRange(conn.tiles);
      }
      //Ensure no mismatch between tiles in connections and tiles in solution
      foreach (Tile tile in allTiles)
      {
        //Ensure all tiles in solution are in connections
        if (tilesInConn.Contains(tile))
        {
          tilesInConn.Remove(tile);
        }
        else
        {
          return false;
        }
      }
      //Ensure no tiles in connections that aren't part of solution
      if (tilesInConn.Count != 0)
      {
        return false;
      }
      return true;
    }
  }
}