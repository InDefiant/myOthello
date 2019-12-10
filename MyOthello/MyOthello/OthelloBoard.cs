using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Dylan Engle CSCI 3005 OOP, Dr.Dana, Final Project, December 5, 2019

namespace MyOthello
{
    public class OthelloBoard
    {
        public Tile[,] gameBoard;
        public List<Tile> tilesToFlip;
        public List<string> directionsToCheck;
        public int playerTurn;
        public int player1Score;
        public int player2Score;

        //Intializes the Gameboard,Scores, and lists used for Valid Move Checking
        public OthelloBoard()
        {
            this.player1Score = 0;
            this.player2Score = 0;

            this.gameBoard = new Tile[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Tile newTile = new Tile();
                    newTile.rowPosition = i;
                    newTile.columnPosition = j;
                    this.gameBoard[i, j] = newTile;
                }
            }

            this.tilesToFlip = new List<Tile>();
            this.directionsToCheck = new List<string>();
        }

        //Resets the Gameboard to the first turn 
        public void ResetGameBoard()
        {
            foreach (Tile tile in this.gameBoard)
            {
                tile.active = false;
                tile.color = "";
            }

            gameBoard[3, 3].active = true;
            gameBoard[4, 4].active = true;
            gameBoard[3, 4].active = true;
            gameBoard[4, 3].active = true;

            gameBoard[3, 3].setTileColor("black");
            gameBoard[4, 4].setTileColor("black");
            gameBoard[3, 4].setTileColor("white");
            gameBoard[4, 3].setTileColor("white");

            this.playerTurn = 1;
        }

        //Used to add a piece to the game board and change a Tile's parameters
        public void AddToBoard(Tile tile, Player player)
        {
            tile.active = true;
            tile.setTileColor(player.playerColor);
        }

        //Removes a piece from the gameboard
        public void RemoveFromBoard(Tile tile)
        {
           tile.active = false;
           tile.color = "";
        }
        //Handles the player's move
        //Adds the Tile to the board
        //Flips any tiles handled from the ValidMove function
        //Updates the Current Score
        public void PlayerMove(Tile tile, Player player)
        {
            AddToBoard(tile, player);
            FlipTiles();
        }

        //Checks to see if there are Adjacent Opposing Tiles
        //Also checks to make sure that there are Tiles to flip
        public bool ValidMove(Tile tile, Player player)
        {
            if (CheckForAdjacentTiles(tile,player) && NewCheckTilesToFlip(tile)) return true;
            else return false;
        }
        //Checks for Adjacent opposing tiles in each direction
        //Then gives each direction that passes the test into the DirectionsToCheck List for later use.
        //If there are any directions that pass the test the function is considered true and returned otherwise it is false.
        public bool CheckForAdjacentTiles(Tile startTile,Player player)
        {
            directionsToCheck.Clear();

            Tile checkTile = new Tile();
            checkTile = ResetTile(checkTile, startTile);

            AddToBoard(startTile, player);

            
            checkTile = AddDirection(ToGameBoard(checkTile = TileTransform.ModifyRow(checkTile, row => row - 1)), startTile, "Up");
            checkTile = AddDirection(ToGameBoard(checkTile = TileTransform.ModifyRow(checkTile, row => row + 1)), startTile, "Down");
            checkTile = AddDirection(ToGameBoard(checkTile = TileTransform.ModifyColumn(checkTile, col => col - 1)), startTile, "Left");
            checkTile = AddDirection(ToGameBoard(checkTile = TileTransform.ModifyColumn(checkTile, col => col + 1)), startTile, "Right");
            checkTile = AddDirection(ToGameBoard(checkTile = TileTransform.ModifyDiagonal(checkTile, row => row - 1, col => col - 1)), startTile, "UpLeft");
            checkTile = AddDirection(ToGameBoard(checkTile = TileTransform.ModifyDiagonal(checkTile, row => row - 1, col => col + 1)), startTile, "UpRight");
            checkTile = AddDirection(ToGameBoard(checkTile = TileTransform.ModifyDiagonal(checkTile, row => row + 1, col => col - 1)), startTile, "DownLeft");
            checkTile = AddDirection(ToGameBoard(checkTile = TileTransform.ModifyDiagonal(checkTile, row => row + 1, col => col + 1)), startTile, "DownRight");

            if (directionsToCheck.Count == 0) return false;

            return true;
        }

        private Tile ResetTile(Tile checkTile,Tile startTile)
        {
            checkTile.rowPosition = startTile.rowPosition;
            checkTile.columnPosition = startTile.columnPosition;

            return checkTile;

        }

        private Tile AddDirection(Tile checkedTile,Tile startTile,string direction)
        {
            if(checkedTile.color == startTile.opposingColor && checkedTile != startTile)
            {
                directionsToCheck.Add(direction);
            }
            return ResetTile(checkedTile, startTile);

        }

        //Checks to make sure that pieces are on the border
        public bool CheckForBorder(Tile tile)
        {
            if (tile.rowPosition == 7 || tile.columnPosition == 7 || tile.rowPosition == 0 || tile.columnPosition == 0) return true;
            else return false;

        }
        //Uses the directionstoCheck List to find if there are any matching pieces in the valid directions
        //If a piece matching the players color is found it adds each tile that would be flipped into the tilesToFlip list
        //If the tilesToFlip list is populated it means that the move is Valid which then returns the function as true, otherwise false

        public bool NewCheckTilesToFlip(Tile startTile)
        {
            tilesToFlip.Clear();
            foreach (string direction in directionsToCheck)
            {
                if (direction == "Up") SearchAndFlip(startTile, "row", -1, 0);
                if (direction == "Down") SearchAndFlip(startTile, "row", 1, 0);
                if (direction == "Left") SearchAndFlip(startTile, "col", 0, -1);
                if (direction == "Right") SearchAndFlip(startTile, "col", 0, 1);
                if (direction == "UpLeft") SearchAndFlip(startTile, "both", -1, -1);
                if (direction == "UpRight") SearchAndFlip(startTile, "both", -1, 1);
                if (direction == "DownLeft") SearchAndFlip(startTile, "both", 1, -1);
                if (direction == "DownRight") SearchAndFlip(startTile, "both", 1, 1);
            }

            RemoveFromBoard(startTile);
            if (tilesToFlip.Count == 0) return false;
            else return true;

        }

        public void SearchAndFlip(Tile startTile,string type, int rowMult, int colMult)
        {
            Tile checkTile = new Tile();
            checkTile = ResetTile(checkTile, startTile);

            while(true)
            {
                if (type == "row") checkTile = TileTransform.ModifyRow(checkTile, row => row + (1 * rowMult));
                if (type == "col") checkTile = TileTransform.ModifyColumn(checkTile, col => col + (1 * colMult));
                if (type == "both") checkTile = TileTransform.ModifyDiagonal(checkTile, row => row + (1 * rowMult), col => col + (1 * colMult));

                checkTile = ToGameBoard(checkTile);

                if(checkTile.color == startTile.color)
                {
                    while (true)
                    {
                        if (type == "row") checkTile = TileTransform.ModifyRow(checkTile, row => row + (1 * rowMult * -1));
                        if (type == "col") checkTile = TileTransform.ModifyColumn(checkTile, col => col + (1 * colMult * -1));
                        if (type == "both") checkTile = TileTransform.ModifyDiagonal(checkTile, row => row + (1 * rowMult * -1), col => col + (1 * colMult * -1));

                        if (type == "row" && checkTile.rowPosition == 0 || checkTile.rowPosition == 7) break;
                        if (type == "col" && checkTile.columnPosition == 0 || checkTile.columnPosition == 7) break;
                        if (type == "both" && checkTile.rowPosition == 0 || checkTile.rowPosition == 7 || checkTile.columnPosition == 0 || checkTile.columnPosition == 7) break;
                        if (checkTile.rowPosition == startTile.rowPosition && checkTile.columnPosition == startTile.columnPosition) break;
                        tilesToFlip.Add(gameBoard[checkTile.rowPosition,checkTile.columnPosition]);
                    }
                    break;
                }

                if (type == "row" && checkTile.rowPosition == 0 || checkTile.rowPosition == 7) break;
                if (type == "col" && checkTile.columnPosition == 0 || checkTile.columnPosition == 7) break;
                if (type == "both" && checkTile.rowPosition == 0 || checkTile.rowPosition == 7 || checkTile.columnPosition == 0 || checkTile.columnPosition == 7) break;
            }
        }

        private Tile ToGameBoard(Tile tile)
        {
            Tile newTile = new Tile();
            newTile = tile;
            newTile.color = gameBoard[tile.rowPosition, tile.columnPosition].color;
            newTile.opposingColor = gameBoard[tile.rowPosition, tile.columnPosition].opposingColor;

            return newTile;
        }

        //Checks to make sure there are viable moves in order to either skip a players turn or end the game if neither player has a viable move
        public bool AnyViableMoves(Player player)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (ValidMove(gameBoard[i,j], player)) return true;
                }
            }
            return false;
        }

        //Uses the tilesToFlip list to flip the tiles from the Valid move in order to gain points
        void FlipTiles()
        {
            foreach (Tile tile in tilesToFlip)
            {
                if (tile.color == "black") gameBoard[tile.rowPosition, tile.columnPosition].color = "white";
                else gameBoard[tile.rowPosition, tile.columnPosition].color = "black";
            }
        }

        //Changes the turn between the two players
        public void NextTurn()
        {
            if (this.playerTurn == 1) this.playerTurn = 2;
            else this.playerTurn = 1;
        }

        //Updates the score by the number of tiles that matches each players color
        public void UpdateScore(Player player1, Player player2)
        {
            int p1Count = 0;
            int p2Count = 0;
            foreach (Tile tile in gameBoard)
            {
                if (tile.color == "black") p1Count++;
                else if (tile.color == "white") p2Count++;
            }
            player1.score = p1Count;
            player2.score = p2Count;
        }
    }
}
