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
        int connectionNumber = tiles[0].Getnumber();
        foreach(Tile tile in tiles.GetRange(1, tiles.Count-1)){
          if(tile.Getnumber() != connectionNumber){
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
        
        //Create list of just the numbers
        List<int> numberlist = new List<int>();
        foreach(Tile tile in tiles){
          numberlist.Add(tile.Getnumber());
        }

        //Check that all numbers are in a row. This also ensures no duplicates. 
        numberlist.Sort();
        for (int i = 0; i < numberlist.Count-1; i++)
        {
            if(numberlist[i] != numberlist[i+1]-1){
              return false;
            }
        }

        //Connection tiles are all the same color and come in a row
        return true;
      }
    }
  }
}