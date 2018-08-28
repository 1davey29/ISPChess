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
                return 4;
            }

            if ((distanceX != 0 && distanceY != 0) && (distanceX != distanceY))
            {
                return 1;
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

            if (validationReturn == 0)
            {
                UpdateBoard(positionXY);
            }

            return validationReturn;
        }

        public override int Move(int[] positionXY)
        {
            int distanceX = Math.Abs(XPosition - positionXY[0]);
            int distanceY = Math.Abs(YPosition - positionXY[1]);

            if ((positionXY[0] < 0 || positionXY[0] > 7) || (positionXY[1] < 0 || positionXY[1] > 7))
            {
                return 4;
            }

            if ((distanceX != 0 && distanceY != 0) && (distanceX != distanceY))
            {
                return 1;
            }

            int validationReturn = 0;
            if (distanceX == distanceY)
            {
                validationReturn = Bishop.PieceInWay(this, positionXY, distanceX, distanceY, true);
            }
            else
            {
                validationReturn = Rook.ValidRookMove((distanceX == 0) ? distanceY : distanceX,
                    (distanceX == 0) ? positionXY[1] : positionXY[0], (distanceX != 0), this, true);
            }

            if (validationReturn == 0)
            {
                UpdateBoard(positionXY);
            }

            return validationReturn;
        }

        public Queen(String color, int x, int y) : base(color.Equals("White") ? 'q' : 'Q', x, y)
        {
        }
    }
}
