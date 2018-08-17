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
                throw new ArgumentOutOfRangeException("The position specified is out of bounds!");
            }

            if ((distanceX != 0 && ((!hasMoved && distanceY > 2) || distanceY > 1)) ^ (distanceX != 1 && distanceY != 1))
            {
                throw new ArgumentException("Invalid move for the specified piece");
            }

            if (ChessController.Board.gameSpace[positionXY[0], positionXY[1]].GetType() != typeof(EmptyPiece))
            {
                if (distanceX == 1 && distanceY == 1)
                {
                    if (!(Char.IsLower(this.GetSymbol()) ?
                        Char.IsUpper(ChessController.Board.gameSpace[positionXY[0], positionXY[1]].GetSymbol()) :
                        Char.IsLower(ChessController.Board.gameSpace[positionXY[0], positionXY[1]].GetSymbol())))
                    {
                        throw new Exception("You cannot take your own piece");
                    }

                    UpdateBoard(positionXY);

                }
                else
                {
                    throw new Exception("Invalid move");
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
                        throw new Exception("Cannot move through pieces");
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

        public Pawn(String color) : base(color.Equals("White") ? 'p' : 'P')
        {
        }
    }
}
