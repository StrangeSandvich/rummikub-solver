namespace rummikub_solver
{
  using NUnit.Framework;
  using System.Collections.Generic;
  /// <summary>
  /// Tests connection
  /// </summary>
  /// 
  [TestFixture]
  public class ConnectionTest
  {
    private Connection connY1toY4;
    private Connection connY6G6R6;
    private Connection connB11toB13;

    private Connection connG6R6;
    private Connection connY3doubled;
    private Connection connMissingY5;
    private Connection connWrongDir;

    /// <summary>
    /// Initializes connection test objects
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

      connY1toY4 = new Connection(new List<Tile>(new Tile[] { tileY1, tileY2, tileY3, tileY4 }), false);
      connY6G6R6 = new Connection(new List<Tile>(new Tile[] { tileY6, tileG6, tileR6 }), true);
      connB11toB13 = new Connection(new List<Tile>(new Tile[] { tileB11, tileB12, tileB13 }), false);
      connG6R6 = new Connection(new List<Tile>(new Tile[] { tileG6, tileR6 }), true);
      connMissingY5 = new Connection(new List<Tile>(new Tile[] { tileY1, tileY2, tileY3, tileY4, tileY6 }), false);
      connY3doubled = new Connection(new List<Tile>(new Tile[] { tileY1, tileY2, tileY3, tileY3Double, tileY4 }), false);
      connWrongDir = new Connection(new List<Tile>(new Tile[] { tileB11, tileB12, tileB13 }), true);

    }

    /// <summary>
    /// Assert these connections are valid
    /// </summary>
    /// 
    [Test]
    public void CommonValidConnections()
    {
      Assert.That(connY1toY4.validate(), Is.True);
      Assert.That(connY6G6R6.validate(), Is.True);
      Assert.That(connB11toB13.validate(), Is.True);
    }

    [Test]
    public void TwoTilesInvalid()
    {
      Assert.That(connG6R6.validate(), Is.False);
    }

    [Test]
    public void NoStreakInvalid()
    {
      Assert.That(connMissingY5.validate(), Is.False);
    }

    public void DoubleTileInvalid()
    {
      Assert.That(connY3doubled.validate(), Is.False);
    }

    public void DirectionInvalid()
    {
      Assert.That(connWrongDir.validate(), Is.False);
    }
    
  }
}