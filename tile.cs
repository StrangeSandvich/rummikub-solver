using System;

public enum COLOR {
  Blue,
  Red,
  Green,
  Yellow
}

public class Tile {

  public int number {
    get; set;
  }
  public COLOR color {
    get; set;
  }

  public void Tile(int number, COLOR color){
    this.number = number;
    this.color = color;
  }
}