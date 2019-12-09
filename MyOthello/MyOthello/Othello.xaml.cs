using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

//Dylan Engle CSCI 3005 OOP, Dr.Dana, Final Project, December 5, 2019

namespace MyOthello
{
    /// <summary>
    /// Interaction logic for Othello.xaml
    /// </summary>
    public partial class Othello : Window
    {

        //Make new instances of the gameboard/logic and of the player class
        public OthelloBoard othelloBoard = new OthelloBoard();
        public Player player1 = new Player(1, "black");
        public Player player2 = new Player(2, "white");


        public Othello()
        {

        }
        //Uses the info passed from the SetUp Window to Set up the GameBoard to Default Settings
        public Othello(string player1Name,string player2Name)
        {
            InitializeComponent();

            player1.name = player1Name;
            player2.name = player2Name;

            player1txtBox.Text = string.Format("{0}: ", player1.name);
            player2txtBox.Text = string.Format("{0}: ", player2.name);

            othelloBoard.ResetGameBoard();
            UpdateBoard();
        }

        //Updates the Gameboard pieces for each player
        //Updates the Current Turn via Arrow and Score
        //Also Checks to see if there are no moves remaining for each player and ends the game
        private void UpdateBoard()
        {
            foreach (Tile tile in othelloBoard.gameBoard)
            {
                var img = new Image { };

                if (tile.active == true && tile.color == "black")
                {
                    var blackPiece = new BitmapImage(new Uri(@"pack://application:,,,/Images/blackpiece.png"));
                    img.Source = blackPiece;
                }
                if (tile.active == true && tile.color == "white")
                {
                    var whitePiece = new BitmapImage(new Uri(@"pack://application:,,,/Images/whitepiece.png"));
                    img.Source = whitePiece;
                }

                img.SetValue(Grid.RowProperty, tile.rowPosition);
                img.SetValue(Grid.ColumnProperty, tile.columnPosition);

                myGrid.Children.Add(img);
            }

            othelloBoard.UpdateScore(player1,player2);

            player1scoreBox.Text = player1.score.ToString();
            player2scoreBox.Text = player2.score.ToString();



            if (othelloBoard.playerTurn == 1)
            {
                player1Active.Visibility = Visibility.Visible;
                player2Active.Visibility = Visibility.Collapsed;
            }

            if (othelloBoard.playerTurn == 2)
            {
                player1Active.Visibility = Visibility.Collapsed;
                player2Active.Visibility = Visibility.Visible;
            }



            //if (!othelloBoard.AnyViableMoves(player1) && !othelloBoard.AnyViableMoves(player2))
            //{
            //    GameOver();
            //}

        }

        //Handles the Game ending and Changes the Window to the GameOver Window
        //Which States the Winner and Allows players to PlayAgain
        private void GameOver()
        {


            if(player1.score > player2.score)
            {
                OthelloGameOver gameOver = new OthelloGameOver(player1.name,player2.name,player1.score,player2.score);
                gameOver.Show();
                this.Hide();
            }
            else
            {
                OthelloGameOver gameOver = new OthelloGameOver(player2.name,player1.name,player2.score,player1.score);
                gameOver.Show();
                this.Hide();
            }

        }

        //Handles the players move and turn by clicking on a space in the grid
        private void MyGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2) // for double-click
            {

                var point = Mouse.GetPosition(myGrid);

                int row = 0;
                int col = 0;
                double accumulatedHeight = 0.0;
                double accumulatedWidth = 0.0;

                // calc row mouse was over
                foreach (var rowDefinition in myGrid.RowDefinitions)
                {
                    accumulatedHeight += rowDefinition.ActualHeight;
                    if (accumulatedHeight >= point.Y)
                        break;
                    row++;
                }

                // calc col mouse was over
                foreach (var columnDefinition in myGrid.ColumnDefinitions)
                {
                    accumulatedWidth += columnDefinition.ActualWidth;
                    if (accumulatedWidth >= point.X)
                        break;
                    col++;
                }

                //Used to Debug where you are clicking in Graph
                MessageBox.Show(string.Format("Grid clicked at row {0}, col {1}", row, col));

                Tile newTile = new Tile();
                newTile = othelloBoard.gameBoard[row, col];
                //Depending on the Player's turn it Checks to make sure the move is Valid
                //Then it Executes the Player's Move and Changes the turn along with updating the board

                if (othelloBoard.playerTurn == 1)
                {
                    if (othelloBoard.ValidMove(newTile, player1))
                    {
                        othelloBoard.PlayerMove(newTile, player1);
                        othelloBoard.NextTurn();
                        UpdateBoard();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("INVALID MOVE: TRY AGAIN");
                        if(!othelloBoard.AnyViableMoves(player1))
                        {
                            othelloBoard.NextTurn();
                            return;
                        }
                    }
                }

                if (othelloBoard.playerTurn == 2)
                {
                    
                    if (othelloBoard.ValidMove(newTile, player2))
                    {
                        othelloBoard.PlayerMove(newTile, player2);
                        othelloBoard.NextTurn();
                        UpdateBoard();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("INVALID MOVE: TRY AGAIN");
                        if (!othelloBoard.AnyViableMoves(player2))
                        {
                            othelloBoard.NextTurn();
                            return;
                        }
                    }
                }
            }
        }

        //Ends Game Early
        private void endGameBtn_Click(object sender, RoutedEventArgs e)
        {
            GameOver();
        }
    }
}
