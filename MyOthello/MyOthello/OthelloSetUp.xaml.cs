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
    /// Interaction logic for OthelloSetUp.xaml
    /// </summary>
    public partial class OthelloSetUp : Window
    {

        public OthelloSetUp()
        {
            InitializeComponent();
        }

        //Handles the Name selection for each player
        private void start_Click(object sender, RoutedEventArgs e)
        {
            Othello othello = new Othello(p1Name.Text, p2Name.Text);
            othello.Show();
            this.Hide();
        }
    }
}
