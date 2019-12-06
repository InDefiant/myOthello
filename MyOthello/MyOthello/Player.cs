using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Dylan Engle CSCI 3005 OOP, Dr.Dana, Final Project, December 5, 2019

namespace MyOthello
{
    public class Player
    {
        
        public string name { get; set; }
        //Used to determine player order
        public int playerNum { get; private set; }
        //Matches with tiles in order to give ownership to the moves and the score.
        public string playerColor { get; private set; }
        public int score { get; set; }

        Player() { }

        public Player(int playerNum,string playerColor)
        {
            this.playerNum = playerNum;
            this.playerColor = playerColor;
        }
        public Player(string name, int playerNum, string playerColor)
        {
            this.name = name;
            this.playerNum = playerNum;
            this.playerColor = playerColor;
        }
    }
}
