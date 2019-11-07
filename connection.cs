using System;
using System.Collections.Generic;

namespace rummikub_solver{
  public class Connection {
    public List<Tile> tiles {get; set;}

    public bool connectionByNumber; //If not connection by number, connection is by color
    public bool valid {get; private set;}
 
    public Connection(List<Tile> tiles, bool connectionByNumber){
      this.tiles = tiles;
      this.connectionByNumber = connectionByNumber;
      validate();
    }

    public bool containsTwin(Tile twin){
      foreach (Tile tile in tiles)
      {
          if(tile.color == twin.color && tile.number == twin.number){
            return true;
          }
      }
      return false;
    }

    public override string ToString(){
      string result = "[";
      tiles.Sort();
      foreach (Tile tile in tiles)
      {
        result += tile.ToString();
        result += " ";
      }
      return result + "]";
    }

    public bool validate(){
      valid = nonMutateValidate();
      return valid;
    }

    private bool nonMutateValidate(){
      //If there isn't at least 3 tiles, connection is not valid
      if(tiles.Count < 3){
        return false;
      }
      if(connectionByNumber){
        //If not all tiles share the same number, connection is not valid
        int connectionNumber = tiles[0].number;
        foreach(Tile tile in tiles.GetRange(1, tiles.Count-1)){
          if(tile.number != connectionNumber){
            return false;
          }
        }

        //If not all tiles have different colors, connection is not valid
        List<COLOR> seenColors = new List<COLOR>();
        foreach(Tile tile in tiles){
          if(seenColors.Contains(tile.color)){
            return false;
          }
          seenColors.Add(tile.color);
        }

        //implicit, if there are 5 or more tiles, one color has to appear twice.
        return true;


      } else { //Connection is color based
        //If not all tiles share color, connection is not valid
        COLOR connectionColor = tiles[0].color;
        foreach(Tile tile in tiles.GetRange(1, tiles.Count-1)){
          if(tile.color != connectionColor){
            return false;
          }
        }
        

        tiles.Sort();
        //Check that all numbers are in a row. This also ensures no duplicates. 
        for (int i = 0; i < tiles.Count-1; i++)
        {
            if(tiles[i].number != tiles[i+1].number-1){
              return false;
            }
        }

        //Connection tiles are all the same color and come in a row
        return true;
      }
    }
  }
}