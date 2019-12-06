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

            gameBoard[3, 3].color = "black";
            gameBoard[4, 4].color = "black";
            gameBoard[3, 4].color = "white";
            gameBoard[4, 3].color = "white";

            this.playerTurn = 1;
        }

        //Used to add a piece to the game board and change a Tile's parameters
        public void AddToBoard(int row, int col, Player player)
        {
            gameBoard[row, col].active = true;
            gameBoard[row, col].color = player.playerColor;
        }

        //Removes a piece from the gameboard
        public void RemoveFromBoard(int row, int col)
        {
            gameBoard[row, col].active = false;
            gameBoard[row, col].color = "";
        }
        //Handles the player's move
        //Adds the Tile to the board
        //Flips any tiles handled from the ValidMove function
        //Updates the Current Score
        public void PlayerMove(int row, int col, Player player)
        {
            AddToBoard(row, col, player);
            FlipTiles();
            UpdateScore();
        }

        //Checks to see if there are Adjacent Opposing Tiles
        //Also checks to make sure that there are Tiles to flip
        public bool ValidMove(int row, int col, Player player)
        {
            if (CheckForAdjacentTiles(row, col, player) && CheckTilesToFlip(row, col, player)) return true;
            else return false;
        }
        //Checks for Adjacent opposing tiles in each direction
        //Then gives each direction that passes the test into the DirectionsToCheck List for later use.
        //If there are any directions that pass the test the function is considered true and returned otherwise it is false.
        public bool CheckForAdjacentTiles(int row, int col, Player player)
        {
            directionsToCheck.Clear();

            string otherTile;
            Tile startTile = gameBoard[row, col];
            Tile checkTile;

            if (player.playerColor == "black") otherTile = "white";
            else otherTile = "black";

            //Up
            checkTile = Up(row, col);
            if (checkTile.color == otherTile && checkTile != startTile)
            {
                directionsToCheck.Add("Up");
            }

            //Down
            checkTile = Down(row, col);
            if (checkTile.color == otherTile && checkTile != startTile)
            {
                directionsToCheck.Add("Down");
            }
            //Left
            checkTile = Left(row, col);
            if (checkTile.color == otherTile && checkTile != startTile)
            {
                directionsToCheck.Add("Left");
            }
            //Right
            checkTile = Right(row, col);
            if (checkTile.color == otherTile && checkTile != startTile)
            {
                directionsToCheck.Add("Right");
            }
            //UpLeft
            checkTile = UpLeft(row, col);
            if (checkTile.color == otherTile && checkTile != startTile)
            {
                directionsToCheck.Add("UpLeft");
            }
            //UpRight
            checkTile = UpRight(row, col);
            if (checkTile.color == otherTile && checkTile != startTile)
            {

                directionsToCheck.Add("UpRight");
            }
            //DownLeft
            checkTile = DownLeft(row, col);
            if (checkTile.color == otherTile && checkTile != startTile)
            {
                directionsToCheck.Add("DownLeft");
            }
            //DownRight
            checkTile = DownRight(row, col);
            if (checkTile.color == otherTile && checkTile != startTile)
            {

                directionsToCheck.Add("DownRight");
            }
            if (directionsToCheck.Count == 0) return false;
            else
            {
                return true;
            }
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
        public bool CheckTilesToFlip(int row, int col, Player player)
        {
            tilesToFlip.Clear();

            Tile startTile = gameBoard[row, col];
            Tile checkTile = gameBoard[row, col];

            foreach (string direction in directionsToCheck)
            {
                if (direction == "Up")
                {
                    checkTile = Up(startTile.rowPosition, startTile.columnPosition);
                    while (Up(checkTile.rowPosition, checkTile.columnPosition) != startTile )
                    {
                        if (checkTile.color == player.playerColor)
                        {
                            while (Down(checkTile.rowPosition, checkTile.columnPosition) != startTile )
                            {
                                checkTile = Down(checkTile.rowPosition, checkTile.columnPosition);
                                if (checkTile == startTile) break;
                                tilesToFlip.Add(checkTile);

                            }
                            break;
                        }
                        checkTile = Up(checkTile.rowPosition, checkTile.columnPosition);
                        if (CheckForBorder(checkTile)) break;
                    }
                }

                if (direction == "Down")
                {
                    checkTile = Down(startTile.rowPosition, startTile.columnPosition);
                    while (Down(checkTile.rowPosition, checkTile.columnPosition) != startTile )
                    {
                        if (checkTile.color == player.playerColor)
                        {
                            while (Up(checkTile.rowPosition, checkTile.columnPosition) != startTile )
                            {
                                checkTile = Up(checkTile.rowPosition, checkTile.columnPosition);
                                if (checkTile == startTile) break;
                                tilesToFlip.Add(checkTile);

                            }
                            break;
                        }
                        checkTile = Down(checkTile.rowPosition, checkTile.columnPosition);
                        if (CheckForBorder(checkTile)) break;
                    }
                }

                if (direction == "Left")
                {
                    checkTile = Left(startTile.rowPosition, startTile.columnPosition);
                    while (Left(checkTile.rowPosition, checkTile.columnPosition) != startTile )
                    {
                        if (checkTile.color == player.playerColor)
                        {
                            while (Right(checkTile.rowPosition, checkTile.columnPosition) != startTile )
                            {
                                checkTile = Right(checkTile.rowPosition, checkTile.columnPosition);
                                if (checkTile == startTile) break;
                                tilesToFlip.Add(checkTile);

                            }
                            break;
                        }
                        checkTile = Left(checkTile.rowPosition, checkTile.columnPosition);
                        if (CheckForBorder(checkTile)) break;
                    }
                }

                if (direction == "Right")
                {
                    checkTile = Right(startTile.rowPosition, startTile.columnPosition);
                    while (Right(checkTile.rowPosition, checkTile.columnPosition) != startTile )
                    {
                        if (checkTile.color == player.playerColor)
                        {
                            while (Left(checkTile.rowPosition, checkTile.columnPosition) != startTile)
                            {
                                checkTile = Left(checkTile.rowPosition, checkTile.columnPosition);
                                if (checkTile == startTile) break;
                                tilesToFlip.Add(checkTile);

                            }
                            break;
                        }
                        checkTile = Right(checkTile.rowPosition, checkTile.columnPosition);
                        if (CheckForBorder(checkTile)) break;
                    }
                }

                if (direction == "UpLeft")
                {
                    checkTile = UpLeft(startTile.rowPosition, startTile.columnPosition);
                    while (UpLeft(checkTile.rowPosition, checkTile.columnPosition) != startTile)
                    {
                        if (checkTile.color == player.playerColor)
                        {
                            while (DownRight(checkTile.rowPosition, checkTile.columnPosition) != startTile )
                            {
                                checkTile = DownRight(checkTile.rowPosition, checkTile.columnPosition);
                                if (checkTile == startTile) break;
                                tilesToFlip.Add(checkTile);

                            }
                            break;
                        }
                        checkTile = UpLeft(checkTile.rowPosition, checkTile.columnPosition);
                        if (CheckForBorder(checkTile)) break;
                    }
                }

                if (direction == "UpRight")
                {
                    checkTile = UpRight(startTile.rowPosition, startTile.columnPosition);
                    while (UpRight(checkTile.rowPosition, checkTile.columnPosition) != startTile )
                    {
                        if (checkTile.color == player.playerColor)
                        {
                            while (DownLeft(checkTile.rowPosition, checkTile.columnPosition) != startTile)
                            {
                                checkTile = DownLeft(checkTile.rowPosition, checkTile.columnPosition);
                                if (checkTile == startTile) break;
                                tilesToFlip.Add(checkTile);

                            }
                            break;
                        }
                        checkTile = UpRight(checkTile.rowPosition, checkTile.columnPosition);
                        if (CheckForBorder(checkTile)) break;
                    }
                }

                if (direction == "DownLeft")
                {
                    checkTile = DownLeft(startTile.rowPosition, startTile.columnPosition);
                    while (DownLeft(checkTile.rowPosition, checkTile.columnPosition) != startTile )
                    {
                        if (checkTile.color == player.playerColor)
                        {
                            while (UpRight(checkTile.rowPosition, checkTile.columnPosition) != startTile )
                            {
                                checkTile = UpRight(checkTile.rowPosition, checkTile.columnPosition);
                                if (checkTile == startTile) break;
                                tilesToFlip.Add(checkTile);

                            }
                            break;
                        }
                        checkTile = DownLeft(checkTile.rowPosition, checkTile.columnPosition);
                        if (CheckForBorder(checkTile)) break;
                    }
                }

                if (direction == "DownRight")
                {
                    checkTile = DownRight(startTile.rowPosition, startTile.columnPosition);
                    while (DownRight(checkTile.rowPosition, checkTile.columnPosition) != startTile)
                    {
                        if (checkTile.color == player.playerColor)
                        {
                            while (UpLeft(checkTile.rowPosition, checkTile.columnPosition) != startTile )
                            {
                                checkTile = UpLeft(checkTile.rowPosition, checkTile.columnPosition);
                                if (checkTile == startTile) break;
                                tilesToFlip.Add(checkTile);

                            }
                            break;
                        }
                        checkTile = DownRight(checkTile.rowPosition, checkTile.columnPosition);
                        if (CheckForBorder(checkTile)) break;
                    }
                }

            }
            if (tilesToFlip.Count == 0) return false;
            else return true;

        }

        //Each of these are used to traverse the gameboard cells in a 2d array in the stated directions.
        public Tile Up(int startRow, int startCol)
        {
            if (startRow - 1 >= 0) return gameBoard[startRow - 1, startCol];
            else return gameBoard[startRow, startCol];
        }

        public Tile Down(int startRow, int startCol)
        {
            if (startRow + 1 <= 7) return gameBoard[startRow + 1, startCol];
            else return gameBoard[startRow, startCol];
        }

        public Tile Left(int startRow, int startCol)
        {
            if (startCol - 1 >= 0) return gameBoard[startRow, startCol - 1];
            else return gameBoard[startRow, startCol];
        }

        public Tile Right(int startRow, int startCol)
        {
            if (startCol + 1 <= 7) return gameBoard[startRow, startCol + 1];
            else return gameBoard[startRow, startCol];
        }

        public Tile UpLeft(int startRow, int startCol)
        {
            if (startRow - 1 >= 0 && startCol - 1 >= 0) return gameBoard[startRow - 1, startCol - 1];
            else return gameBoard[startRow, startCol];
        }

        public Tile UpRight(int startRow, int startCol)
        {
            if (startRow - 1 >= 0 && startCol + 1 <= 7) return gameBoard[startRow - 1, startCol + 1];
            else return gameBoard[startRow, startCol];
        }

        public Tile DownLeft(int startRow, int startCol)
        {
            if (startRow + 1 <= 7 && startCol - 1 >= 0) return gameBoard[startRow + 1, startCol - 1];
            else return gameBoard[startRow, startCol];
        }

        public Tile DownRight(int startRow, int startCol)
        {
            if (startRow + 1 <= 7 && startCol + 1 <= 7) return gameBoard[startRow + 1, startCol + 1];
            else return gameBoard[startRow, startCol];
        }

        //Checks to make sure there are viable moves in order to either skip a players turn or end the game if neither player has a viable move
        public bool AnyViableMoves(Player player)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (ValidMove(i, j, player)) return true;
                }
            }

            return false;
        }

        //Uses the tilesToFlip list to flip the tiles from the Valid move in order to gain points
        void FlipTiles()
        {
            foreach (Tile tile in tilesToFlip)
            {
                if (gameBoard[tile.rowPosition, tile.columnPosition].color == "black")
                {
                    gameBoard[tile.rowPosition, tile.columnPosition].color = "white";
                }
                else
                {
                    gameBoard[tile.rowPosition, tile.columnPosition].color = "black";
                }
            }
        }

        //Changes the turn between the two players
        public void NextTurn()
        {
            if (this.playerTurn == 1) this.playerTurn = 2;
            else this.playerTurn = 1;
        }

        //Updates the score by the number of tiles that matches each players color
        public void UpdateScore()
        {
            int p1Count = 0;
            int p2Count = 0;
            foreach (Tile tile in gameBoard)
            {
                if (tile.color == "black") p1Count++;
                else if (tile.color == "white") p2Count++;
            }
            this.player1Score = p1Count;
            this.player2Score = p2Count;
        }
    }
}
