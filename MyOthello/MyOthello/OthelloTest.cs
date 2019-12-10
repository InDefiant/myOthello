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
        OthelloBoard othello = new OthelloBoard();
        Player player1 = new Player(2, "black");
        

        [TestMethod]
        public void TestCheckForAdjacentTiles()
        {
            othello.ResetGameBoard();

            Assert.IsTrue(othello.CheckForAdjacentTiles(othello.gameBoard[5, 3], player1), "Method erroneously returning False");
            Assert.IsTrue(othello.directionsToCheck.Count == 1, "Count is Wrong");
            Assert.IsTrue(othello.directionsToCheck[0] == "Up", othello.directionsToCheck[0]);
        }

        [TestMethod]
        public void TestSearchAndFlip()
        {
            othello.ResetGameBoard();
            othello.CheckForAdjacentTiles(othello.gameBoard[5, 3], player1);
            Assert.IsTrue(othello.NewCheckTilesToFlip(othello.gameBoard[5, 3]), "Method erroneously returning False");
            Assert.IsTrue(othello.tilesToFlip.Count == 1, string.Format("Count should be 1 instead it is Count:{0}", othello.tilesToFlip.Count));
            Assert.IsTrue(othello.tilesToFlip[0] == othello.gameBoard[4, 3], string.Format("Tile to flip should be [4,3] instead it is [{0},{1}]", othello.tilesToFlip[0].rowPosition, othello.tilesToFlip[0].columnPosition));

        }
    }
}
