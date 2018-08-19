using Chess.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models.Pieces
{
    public class Pawn : Piece
    {
        private bool hasMoved = false;
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

            if ((distanceY == 0 && ((hasMoved) ? distanceX > 1 : distanceX > 2) ^ (distanceX == 1 && distanceY == 1)))
            {
                return 1;
            }

            if (ChessController.Board.gameSpace[positionXY[0], positionXY[1]].GetType() != typeof(EmptyPiece))
            {
                if (distanceX == 1 && distanceY == 1)
                {
                    if (IsSameColor(positionXY))
                    {
                        return 2;
                    }

                    UpdateBoard(positionXY);

                }
                else
                {
                    if (IsSameColor(positionXY))
                    {
                        return 2;
                    }
                    return 3;
                }
            }

            if (distanceY == 2)
            {
                for (int y = (YPosition - positionXY[1] > 0) ? 
                    positionXY[1] : distanceY; (YPosition - positionXY[1] > 0) ? y <= distanceY : 
                    y >= positionXY[1];)
                {

                    if (ChessController.Board.gameSpace[positionXY[0], y].GetType() != typeof(EmptyPiece))
                    {
                        if (IsSameColor(positionXY))
                        {
                            return 2;
                        }
                        return 3;
                    }

                    if (YPosition - positionXY[1] > 0)
                        y++;
                    else
                        y--;
                }
            } 


            ChessController.Board.gameSpace[positionXY[0], positionXY[1]] = this;
            ChessController.Board.gameSpace[XPosition, YPosition] = new EmptyPiece(XPosition, YPosition);

            XPosition = positionXY[0];
            YPosition = positionXY[1];

            hasMoved = true;

            return 0;
        }

        public Pawn(String color, int x, int y) : base(color.Equals("White") ? 'p' : 'P', x, y)
        {
        }
    }
}
