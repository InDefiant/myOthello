using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyOthello
{
    [TestClass]
    public class OthelloTest
    {
        [TestMethod]
        public void TestCheckForAdjacentTiles()
        {
            OthelloBoard othello = new OthelloBoard();
            Player player2 = new Player(2, "white");
            othello.ResetGameBoard();

            Assert.IsTrue(othello.CheckForAdjacentTiles(othello.gameBoard[5, 3], player2), "Bool Statement Error");
            Assert.IsTrue(othello.directionsToCheck.Count == 1, "Count is Wrong");
            //Assert.IsTrue(othello.directionsToCheck[0] == "Up", "Direction is wrong.");
        }
    }
}
