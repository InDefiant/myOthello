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
    /// Interaction logic for OthelloGameOver.xaml
    /// </summary>
    public partial class OthelloGameOver : Window
    {
        

        public string winnerName;
        public string loserName;
        public int winnerScore;
        public int loserScore;
        public OthelloGameOver()
        {
            InitializeComponent();

            
        }
        //Handles which player is the Winner and Loser to Announce it via Window
        public OthelloGameOver(string winnerName,string loserName,int winnerScore,int loserScore)
        {
            InitializeComponent();
            winnerNameTxt.Text = winnerName;
            winnerScoreTxt.Text = winnerScore.ToString();

            this.winnerName = winnerName;
            this.loserName = loserName;
            this.winnerScore = winnerScore;
            this.loserScore = loserScore;

        }

        //Allows for another game of Othello to be player with the winner being Player 1(Black)
        private void playAgain_Click(object sender, RoutedEventArgs e)
        {

            Othello othello2 = new Othello(winnerName,loserName);
            othello2.Show();
            this.Close();
        }
    }
}
