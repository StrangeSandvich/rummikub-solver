namespace rummikub_solver
{
  using NUnit.Framework;
  using System.Collections.Generic;
  /// <summary>
  /// Tests solution
  /// </summary>
  /// 
  [TestFixture]
  public class SolutionTest
  {
    //Valid connections
    private Connection connY1toY4;
    private Connection connY6G6R6;
    private Connection connB11toB13;

    //Invalid connections
    private Connection connG6R6;
    private Connection connY3doubled;
    private Connection connMissingY5;
    private Connection connWrongDir;

    //Valid solutions
    private Solution soluY1toY4;
    private Solution soluWith3Conn;

    //Invalid solutions
    private Solution soluWithUnusedTile;
    private Solution soluWithInvalidConn;
    //Tiles provided in constructor doesn't match tiles in connections
    private Solution soluWithTileMismatchExtra;
    private Solution soluWithTileMismatchMissing;

    /// <summary>
    /// Initializes solution test objects
    /// </summary>
    /// 
    [SetUp]
    protected void SetUp()
    {
      Tile tileY1 = new Tile(1, COLOR.Yellow);
      Tile tileY2 = new Tile(2, COLOR.Yellow);
      Tile tileY3 = new Tile(3, COLOR.Yellow);
      Tile tileY3Double = new Tile(3, COLOR.Yellow);
      Tile tileY4 = new Tile(4, COLOR.Yellow);
      Tile tileY6 = new Tile(6, COLOR.Yellow);
      Tile tileG6 = new Tile(6, COLOR.Green);
      Tile tileR6 = new Tile(6, COLOR.Red);
      Tile tileB11 = new Tile(11, COLOR.Blue);
      Tile tileB12 = new Tile(12, COLOR.Blue);
      Tile tileB13 = new Tile(13, COLOR.Blue);

      //Valid connections
      connY1toY4 = new Connection(new List<Tile>(new Tile[] { tileY1, tileY2, tileY3, tileY4 }), false);
      connY6G6R6 = new Connection(new List<Tile>(new Tile[] { tileY6, tileG6, tileR6 }), true);
      connB11toB13 = new Connection(new List<Tile>(new Tile[] { tileB11, tileB12, tileB13 }), false);
      //Invalid connections
      connG6R6 = new Connection(new List<Tile>(new Tile[] { tileG6, tileR6 }), true);
      connMissingY5 = new Connection(new List<Tile>(new Tile[] { tileY1, tileY2, tileY3, tileY4, tileY6 }), false);
      connY3doubled = new Connection(new List<Tile>(new Tile[] { tileY1, tileY2, tileY3, tileY3Double, tileY4 }), false);
      connWrongDir = new Connection(new List<Tile>(new Tile[] { tileB11, tileB12, tileB13 }), true);

      //Valid Solutions
      soluY1toY4 = new Solution(new List<Tile>(new Tile[] { tileY1, tileY2, tileY3, tileY4 }));
      soluY1toY4.unusedTiles.Remove(tileY1);
      soluY1toY4.unusedTiles.Remove(tileY2);
      soluY1toY4.unusedTiles.Remove(tileY3);
      soluY1toY4.unusedTiles.Remove(tileY4);
      soluY1toY4.connections.Add(connY1toY4);

      soluWith3Conn = new Solution(new List<Tile>(new Tile[] { tileY1, tileY2, tileY3, tileY4, tileY6, tileG6, tileR6, tileB11, tileB12, tileB13}));
      soluWith3Conn.unusedTiles.Remove(tileY1);
      soluWith3Conn.unusedTiles.Remove(tileY2);
      soluWith3Conn.unusedTiles.Remove(tileY3);
      soluWith3Conn.unusedTiles.Remove(tileY4);
      soluWith3Conn.connections.Add(connY1toY4);
      soluWith3Conn.unusedTiles.Remove(tileY6);
      soluWith3Conn.unusedTiles.Remove(tileG6);
      soluWith3Conn.unusedTiles.Remove(tileR6);
      soluWith3Conn.connections.Add(connY6G6R6);
      soluWith3Conn.unusedTiles.Remove(tileB11);
      soluWith3Conn.unusedTiles.Remove(tileB12);
      soluWith3Conn.unusedTiles.Remove(tileB13);
      soluWith3Conn.connections.Add(connB11toB13);

      //Invalid solutions
      soluWithUnusedTile = new Solution(new List<Tile>(new Tile[] { tileY1, tileY2, tileY3, tileY4, tileY6}));
      soluWithUnusedTile.unusedTiles.Remove(tileY1);
      soluWithUnusedTile.unusedTiles.Remove(tileY2);
      soluWithUnusedTile.unusedTiles.Remove(tileY3);
      soluWithUnusedTile.unusedTiles.Remove(tileY4);
      soluWithUnusedTile.connections.Add(connY1toY4);

      soluWithInvalidConn = new Solution(new List<Tile>(new Tile[] { tileY1, tileY2, tileY3, tileY4, tileY6}));
      soluWithInvalidConn.unusedTiles.Remove(tileY1);
      soluWithInvalidConn.unusedTiles.Remove(tileY2);
      soluWithInvalidConn.unusedTiles.Remove(tileY3);
      soluWithInvalidConn.unusedTiles.Remove(tileY4);
      soluWithInvalidConn.unusedTiles.Remove(tileY6);
      soluWithInvalidConn.connections.Add(connMissingY5);

      soluWithTileMismatchExtra = new Solution(new List<Tile>(new Tile[] { tileY1, tileY2, tileY3, tileY4, tileY6 }));
      soluWithTileMismatchExtra.unusedTiles.Remove(tileY1);
      soluWithTileMismatchExtra.unusedTiles.Remove(tileY2);
      soluWithTileMismatchExtra.unusedTiles.Remove(tileY3);
      soluWithTileMismatchExtra.unusedTiles.Remove(tileY4);
      soluWithTileMismatchExtra.unusedTiles.Remove(tileY6);
      soluWithTileMismatchExtra.connections.Add(connY1toY4);

      soluWithTileMismatchMissing = new Solution(new List<Tile>(new Tile[] { tileY1, tileY2, tileY3 }));
      soluWithTileMismatchMissing.unusedTiles.Remove(tileY1);
      soluWithTileMismatchMissing.unusedTiles.Remove(tileY2);
      soluWithTileMismatchMissing.unusedTiles.Remove(tileY3);
      soluWithTileMismatchMissing.connections.Add(connY1toY4);
    }

    /// <summary>
    /// Assert these solutions are valid
    /// </summary>
    /// 
    [Test]
    public void TestValidSolutions()
    {
      Assert.That(soluY1toY4.Solved(), Is.True);
      Assert.That(soluWith3Conn.Solved(), Is.True);
    }
    
    [Test]
    public void TestUnusedTileSolution()
    {
      Assert.That(soluWithUnusedTile.Solved(), Is.False);
    }

    [Test]
    public void TestInvalidConnSolution()
    {
      Assert.That(soluWithInvalidConn.Solved(), Is.False);
    }

    [Test]
    public void TestTileMismatchSolution()
    {
      Assert.That(soluWithTileMismatchExtra.Solved(), Is.False);
      Assert.That(soluWithTileMismatchMissing.Solved(), Is.False);
    }
  }
}