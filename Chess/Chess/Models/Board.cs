using Chess.Models.Piece;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public class Board
    {
        public Piece[,] gameSpace { get; set; }

        public Board()
        {
            gameSpace = new Piece[8, 8];
            for (int x = 0; x < gameSpace.Length; x++)
            {
                for (int y = 0; y < gameSpace.Length; y++)
                {
                    gameSpace[x, y] = new Piece();
                }
            }
        }
    }
}
