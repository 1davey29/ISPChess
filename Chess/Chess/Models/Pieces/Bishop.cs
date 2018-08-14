using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models.Pieces
{
    public class Bishop : Piece
    {
        public override int Move(string newPosition)
        {
            int[] positionXY;

            positionXY = Controllers.ChessController.ConvertToXY(newPosition);

            int distanceX = Math.Abs(XPosition - positionXY[0]);
            int distanceY = Math.Abs(YPosition - positionXY[1]);

            if ((positionXY[0] < 0 || positionXY[0] > 7) || (positionXY[1] < 0 || positionXY[1] > 7))
            {
                throw new ArgumentOutOfRangeException("The position specified is out of bounds!");
            }

            if (distanceX != distanceY)
            {
                throw new ArgumentException("Invalid move for the specified piece");
            }

            XPosition = positionXY[0];
            YPosition = positionXY[1];

            return 0;
        }

        public Bishop(String color) : base(color.Equals("White") ? 'b' : 'B')
        {
        }
    }
}
