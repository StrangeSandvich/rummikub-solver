using System;
using System.Collections.Generic;

namespace rummikub_solver
{
  public class Solution
  {
    public List<Tile> allTiles { get; private set; }
    public List<Tile> unusedTiles { get; set; }
    public List<Connection> connections { get; set; }
    public bool solved { get; private set; }

    public Solution(List<Tile> tiles)
    {
      allTiles = tiles;
      unusedTiles = new List<Tile>(tiles);
      connections = new List<Connection>();
      solved = false;
    }

    public bool Solved()
    {
      solved = NonMutateSolved();
      return solved;
    }

    private bool NonMutateSolved()
    {
      //Ensure all tiles are in a connection
      if (unusedTiles.Count != 0)
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