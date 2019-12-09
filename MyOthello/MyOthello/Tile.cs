using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Dylan Engle CSCI 3005 OOP, Dr.Dana, Final Project, December 5, 2019

namespace MyOthello
{
    public class Tile
    {
        public bool active { get; set; }
        public string color { get; set; }
        public string opposingColor;
        public int rowPosition { get; set; }
        public int columnPosition { get; set; }

        public Tile()
        {
            this.active = false;
            this.color = "";
        }

        public void setTileColor(string color)
        {
            this.color = color;

            if (this.color == "white") this.opposingColor = "black";
            else if (this.color == "black") this.opposingColor = "white";
        }

        public void flipTileColors(string oldColor, string newColor)
        {
            if(this.color != "")
            {
                this.color = "";
                this.opposingColor = "";
            }
        }
    }
}
