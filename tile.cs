using System;

namespace rummikub_solver
{

  public enum COLOR
  {
    Blue,
    Red,
    Green,
    Yellow
  }

  public class Tile : IComparable<Tile>
  {
    public int number{
      get; set;
    }

    public COLOR color
    {
      get; set;
    }

    public Tile(int number, COLOR color)
    {
      this.number = number;
      this.color = color;
    }
    public Tile(string identify)
    { 
      int number;
      string colorIdentify;
      try
      {
        colorIdentify = identify.Substring(0, 1);
        string numberIdentify = identify.Substring(1);
        number = int.Parse(numberIdentify);
      }
      catch (System.Exception)
      {
        throw new ArgumentException();
      }

      if(colorIdentify == "r"){
        this.color = COLOR.Red;
      } else if(colorIdentify == "g"){
        this.color = COLOR.Green;
      } else if(colorIdentify == "y"){
        this.color = COLOR.Yellow;
      } else if(colorIdentify == "b"){
        this.color = COLOR.Blue;
      } else {
        throw new ArgumentException();
      }
      if(number < 1 || number > 13){
        throw new ArgumentException();
      } else {
        this.number = number;
      }
    }

    public override string ToString(){
      string result;
      if(color == COLOR.Red){
        result = "r";
      } else if(color == COLOR.Blue){
        result = "b";
      } else if(color == COLOR.Yellow){
        result = "y";
      } else { //green
        result = "g";
      }
      string numberPart = number.ToString();
      return result + numberPart;
    }

    public int CompareTo(Tile other)
    {
      if(other == null){
        return 1;
      }
      if(other.color == color){
        return number.CompareTo(other.number);
      }
      return color.CompareTo(other.color);
    }
  }
}