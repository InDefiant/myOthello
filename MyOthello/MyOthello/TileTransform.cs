using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyOthello
{
    class TileTransform
    {
        public delegate int ModifyPositionInGrid(int position);
        public delegate string FlipColor(string color);

        public static Tile ModifyRow(Tile tile, ModifyPositionInGrid op)
        {
            if (op(tile.rowPosition) < 0 || op(tile.rowPosition) > 7) return tile;
            Tile newTile = tile;
            newTile.rowPosition = op(tile.rowPosition);
            return newTile;
        }

        public static Tile ModifyColumn(Tile tile, ModifyPositionInGrid op)
        {
            if (op(tile.columnPosition) < 0 || op(tile.columnPosition) > 7) return tile;
            Tile newTile = tile;
            newTile.columnPosition = op(tile.columnPosition);
            return newTile;
        }

        public static Tile ModifyDiagonal(Tile tile, ModifyPositionInGrid op1, ModifyPositionInGrid op2)
        {
            if (op1(tile.rowPosition) < 0 || op1(tile.rowPosition) > 7) return tile;
            if (op2(tile.columnPosition) < 0 || op2(tile.columnPosition) > 7) return tile;
            Tile newTile = tile;
            newTile.rowPosition = op1(tile.rowPosition);
            newTile.columnPosition = op2(tile.columnPosition);
            return newTile;
        }


    }
}
