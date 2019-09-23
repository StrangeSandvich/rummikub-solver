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
    private int number1;

    public int Getnumber()
    {
      return number1;
    }

    public void Setnumber(int value)
    {
      number1 = value;
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