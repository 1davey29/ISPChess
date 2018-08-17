using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Chess.Controllers;

namespace Chess.Models.Pieces
{
    public class Queen : Piece
    {
        public override int Move(string newPosition)
        {
            int[] positionXY;

            positionXY = ChessController.ConvertToXY(newPosition);

            int distanceX = Math.Abs(XPosition - positionXY[0]);
            int distanceY = Math.Abs(YPosition - positionXY[1]);

            if ((positionXY[0] < 0 || positionXY[0] > 7) || (positionXY[1] < 0 || positionXY[1] > 7))
            {
                throw new ArgumentOutOfRangeException("The position specified is out of bounds!");
            }

            if ((distanceX != 0 && distanceY != 0) && (distanceX != distanceY))
            {
                throw new ArgumentException("Invalid move for the specified piece");
            }

            int validationReturn = 0;
            if (distanceX == distanceY)
            {
                validationReturn = Bishop.PieceInWay(this, positionXY, distanceX, distanceY, true);
            } else
            {
                validationReturn = Rook.ValidRookMove((distanceX == 0) ? distanceY : distanceX, 
                    (distanceX == 0) ? positionXY[1] : positionXY[0], (distanceX != 0), this, true);
            }

            switch (validationReturn)
            {
                case 0:
                    UpdateBoard(positionXY);
                    break;
            }

            return 0;
        }

        public Queen(String color, int x, int y) : base(color.Equals("White") ? 'q' : 'Q', x, y)
        {
        }
    }
}
