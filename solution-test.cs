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

    Tile tileY1;
    Tile tileY2;
    Tile tileY3;
    Tile tileY3Double;
    Tile tileY4;
    Tile tileY6;
    Tile tileG6;
    Tile tileR6; 
    Tile tileB11;
    Tile tileB12;
    Tile tileB13;

    /// <summary>
    /// Initializes solution test objects
    /// </summary>
    /// 
    [SetUp]
    protected void SetUp()
    {
      tileY1 = new Tile(1, COLOR.Yellow);
      tileY2 = new Tile(2, COLOR.Yellow);
      tileY3 = new Tile(3, COLOR.Yellow);
      tileY3Double = new Tile(3, COLOR.Yellow);
      tileY4 = new Tile(4, COLOR.Yellow);
      tileY6 = new Tile(6, COLOR.Yellow);
      tileG6 = new Tile(6, COLOR.Green);
      tileR6 = new Tile(6, COLOR.Red);
      tileB11 = new Tile(11, COLOR.Blue);
      tileB12 = new Tile(12, COLOR.Blue);
      tileB13 = new Tile(13, COLOR.Blue);

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
      soluY1toY4.unprocessedTiles.Remove(tileY1);
      soluY1toY4.unprocessedTiles.Remove(tileY2);
      soluY1toY4.unprocessedTiles.Remove(tileY3);
      soluY1toY4.unprocessedTiles.Remove(tileY4);
      soluY1toY4.connections.Add(connY1toY4);

      soluWith3Conn = new Solution(new List<Tile>(new Tile[] { tileY1, tileY2, tileY3, tileY4, tileY6, tileG6, tileR6, tileB11, tileB12, tileB13 }));
      soluWith3Conn.unprocessedTiles.Remove(tileY1);
      soluWith3Conn.unprocessedTiles.Remove(tileY2);
      soluWith3Conn.unprocessedTiles.Remove(tileY3);
      soluWith3Conn.unprocessedTiles.Remove(tileY4);
      soluWith3Conn.connections.Add(connY1toY4);
      soluWith3Conn.unprocessedTiles.Remove(tileY6);
      soluWith3Conn.unprocessedTiles.Remove(tileG6);
      soluWith3Conn.unprocessedTiles.Remove(tileR6);
      soluWith3Conn.connections.Add(connY6G6R6);
      soluWith3Conn.unprocessedTiles.Remove(tileB11);
      soluWith3Conn.unprocessedTiles.Remove(tileB12);
      soluWith3Conn.unprocessedTiles.Remove(tileB13);
      soluWith3Conn.connections.Add(connB11toB13);

      //Invalid solutions
      soluWithUnusedTile = new Solution(new List<Tile>(new Tile[] { tileY1, tileY2, tileY3, tileY4, tileY6 }));
      soluWithUnusedTile.unprocessedTiles.Remove(tileY1);
      soluWithUnusedTile.unprocessedTiles.Remove(tileY2);
      soluWithUnusedTile.unprocessedTiles.Remove(tileY3);
      soluWithUnusedTile.unprocessedTiles.Remove(tileY4);
      soluWithUnusedTile.connections.Add(connY1toY4);

      soluWithInvalidConn = new Solution(new List<Tile>(new Tile[] { tileY1, tileY2, tileY3, tileY4, tileY6 }));
      soluWithInvalidConn.unprocessedTiles.Remove(tileY1);
      soluWithInvalidConn.unprocessedTiles.Remove(tileY2);
      soluWithInvalidConn.unprocessedTiles.Remove(tileY3);
      soluWithInvalidConn.unprocessedTiles.Remove(tileY4);
      soluWithInvalidConn.unprocessedTiles.Remove(tileY6);
      soluWithInvalidConn.connections.Add(connMissingY5);

      soluWithTileMismatchExtra = new Solution(new List<Tile>(new Tile[] { tileY1, tileY2, tileY3, tileY4, tileY6 }));
      soluWithTileMismatchExtra.unprocessedTiles.Remove(tileY1);
      soluWithTileMismatchExtra.unprocessedTiles.Remove(tileY2);
      soluWithTileMismatchExtra.unprocessedTiles.Remove(tileY3);
      soluWithTileMismatchExtra.unprocessedTiles.Remove(tileY4);
      soluWithTileMismatchExtra.unprocessedTiles.Remove(tileY6);
      soluWithTileMismatchExtra.connections.Add(connY1toY4);

      soluWithTileMismatchMissing = new Solution(new List<Tile>(new Tile[] { tileY1, tileY2, tileY3 }));
      soluWithTileMismatchMissing.unprocessedTiles.Remove(tileY1);
      soluWithTileMismatchMissing.unprocessedTiles.Remove(tileY2);
      soluWithTileMismatchMissing.unprocessedTiles.Remove(tileY3);
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

    [Test]
    public void TestShatter()
    {
      Assert.That(soluY1toY4.unprocessedTiles.Contains(tileY1), Is.False);
      soluY1toY4.Shatter(connY1toY4);
      Assert.That(soluY1toY4.unusedTiles.Contains(tileY1), Is.True);
      Assert.That(soluY1toY4.unprocessedTiles.Contains(tileY1), Is.False);
      Assert.That(soluY1toY4.unprocessedTiles.Contains(tileY2), Is.True);
      Assert.That(soluY1toY4.unprocessedTiles.Contains(tileY3), Is.True);
      Assert.That(soluY1toY4.unprocessedTiles.Contains(tileY4), Is.True);
      Assert.That(soluY1toY4.unusedTiles.Contains(tileY2), Is.False);
      Assert.That(soluY1toY4.unusedTiles.Contains(tileY3), Is.False);
      Assert.That(soluY1toY4.unusedTiles.Contains(tileY4), Is.False);

      Assert.That(soluY1toY4.connections.Count, Is.Zero);
    }
  }
}