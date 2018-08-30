using Chess.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models.Pieces
{
    public class King : Piece
    {
        private bool hasMoved = false;

        public override int Move(int[] positionXY, bool isMoving)
        {
            int distanceX = Math.Abs(XPosition - positionXY[0]);
            int distanceY = Math.Abs(YPosition - positionXY[1]);

            if (distanceX == 0 && distanceY == 0)
            {
                return 6;
            }

            if ((positionXY[0] < 0 || positionXY[0] > 7) || (positionXY[1] < 0 || positionXY[1] > 7))
            {
                return 4;
            }

            if (distanceX > 1 || distanceY > 1)
            {
                return 1;
            }


            if (ChessController.Board.gameSpace[positionXY[0], positionXY[1]].GetType() != typeof(EmptyPiece))
            {
                if (IsSameColor(positionXY))
                {
                    return 2;
                }

                if (ChessController.Board.gameSpace[positionXY[0], positionXY[1]].GetType() == typeof(King))
                {
                    return 5;
                }
            }

            if (isMoving)
            {
                UpdateBoard(positionXY);

                hasMoved = true;
            }

            return 0;
        }

        public override List<string> GetAvailableMoves()
        {
            return new List<string>();
        }

        public King(String color, int x, int y) : base(color.Equals("White") ? 'k' : 'K', x, y)
        {
        }
    }
}
