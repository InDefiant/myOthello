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
using System.Windows.Navigation;
using System.Windows.Shapes;

//Dylan Engle CSCI 3005 OOP, Dr.Dana, Final Project, December 5, 2019

namespace MyOthello
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OthelloSetUp setUp = new OthelloSetUp();
        public MainWindow()
        {
            InitializeComponent();
        }

        //Used To start a match with another human player
        private void human_Click(object sender, RoutedEventArgs e)
        {
            setUp.Show();
            this.Close();
        }

        //Going to add AI in next update 
        //Will be used to player alone
        private void computer_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("COMING SOON TM");
        }

        //Mostly a joke but thinking about using it to make an "impossible to beat AI"
        private void alien_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Please Find An Alien");
            MessageBox.Show("Actually Let's just play against another Human for now.");
            setUp.Show();
            this.Hide();
        }
    }
}
