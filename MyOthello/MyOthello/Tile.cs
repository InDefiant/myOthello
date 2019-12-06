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
        public int rowPosition { get; set; }
        public int columnPosition { get; set; }

        public Tile()
        {
            this.active = false;
            this.color = "";
        }
    }
}
