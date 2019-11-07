namespace rummikub_solver
{
  using NUnit.Framework;
  using System.Collections.Generic;
  /// <summary>
  /// Tests solution
  /// </summary>
  /// 
  [TestFixture]
  public class SolverTest
  {
    Tile tileY1;
    Tile tileY2;
    Tile tileY3;
    Tile tileY3Double;
    Tile tileY4;
    Tile tileY5;
    Tile tileY6;
    Tile tileG6;
    Tile tileR6;
    Tile tileB6; 
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
      tileY5 = new Tile(5, COLOR.Yellow);
      tileY6 = new Tile(6, COLOR.Yellow);
      tileG6 = new Tile(6, COLOR.Green);
      tileR6 = new Tile(6, COLOR.Red);
      tileB6 = new Tile(6, COLOR.Blue);
      tileB11 = new Tile(11, COLOR.Blue);
      tileB12 = new Tile(12, COLOR.Blue);
      tileB13 = new Tile(13, COLOR.Blue);
    }

    /// <summary>
    /// 
    /// </summary>
    /// 
    [Test]
    public void TestNumberSolver()
    {
      Solution result = RummikubSolver.Solve(new List<Tile>(){tileY6, tileG6, tileR6});
      Assert.That(result.Solved(), Is.True);
    }

    [Test]
    public void TestNumberSolverWith4()
    {
      Solution result = RummikubSolver.Solve(new List<Tile>(){tileY6, tileG6, tileR6, tileB6});
      Assert.That(result.Solved(), Is.True);
    }

    [Test]
    public void TestColorSolverWith4()
    {
      Solution result = RummikubSolver.Solve(new List<Tile>(){tileY3, tileY4, tileY5, tileY6});
      Assert.That(result.Solved(), Is.True);
    }

    [Test]
    public void TestCross()
    {
      Solution result = RummikubSolver.Solve(new List<Tile>(){tileY4, tileY5, tileY6, tileG6, tileR6, tileB6});
      Assert.That(result.Solved(), Is.True);
    }
  }
}