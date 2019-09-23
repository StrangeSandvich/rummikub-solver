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

  public class Tile
  {
    private int number;

    public int Getnumber()
    {
      return number;
    }

    public void Setnumber(int value)
    {
      number = value;
    }

    public COLOR color
    {
      get; set;
    }

    public Tile(int number, COLOR color)
    {
      this.Setnumber(number);
      this.color = color;
    }
  }
}