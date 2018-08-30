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
        public override int Move(int[] positionXY, bool isActuallyMoving)
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
                if (isActuallyMoving)
                {
                UpdateBoard(positionXY);
                }
            }

            return validationReturn;
        }

        public override List<string> GetAvailableMoves()
        {
            Rook tempRook = new Rook(char.IsLower(GetSymbol()) ? "White" : "Black", XPosition, YPosition);
            List<string> rookMoves = tempRook.GetAvailableMoves(true);
            Bishop tempBishop = new Bishop(char.IsLower(GetSymbol()) ? "White" : "Black", XPosition, YPosition);
            List<string> bishopMoves = tempBishop.GetAvailableMoves(true);
            List<string> moves = new List<string>();

            foreach (string move in rookMoves)
            {
                moves.Add(move);
            }

            foreach (string move in bishopMoves)
            {
                if (!moves.Contains(move))
                {
                moves.Add(move);
                }
            }

            return moves;
        }

        public Queen(String color, int x, int y) : base(color.Equals("White") ? 'q' : 'Q', x, y)
        {
        }
    }
}
