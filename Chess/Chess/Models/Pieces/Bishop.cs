using Chess.Controllers;
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

            positionXY = ChessController.ConvertToXY(newPosition);

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

            switch (PieceInWay(this, positionXY, distanceX, distanceY, false))
            {
                case 0:
                    UpdateBoard(positionXY);

                    break;
            } 

            
            return 0;
        }

        public static int PieceInWay(Object obj, int[] positionXY, int distanceX, int distanceY, bool isQueen)
        {
            Piece piece;

            if (isQueen)
            {
                piece = obj as Queen;
            } else
            {
                piece = obj as Bishop;
            }

            for (int x = (piece.XPosition - positionXY[0] > 0) ? positionXY[0] : distanceX; (piece.XPosition - positionXY[0] > 0) ? x <= distanceX : x >= positionXY[0];)
            {
                for (int y = (piece.YPosition - positionXY[1] > 0) ? positionXY[1] : distanceY; (piece.YPosition - positionXY[1] > 0) ? y <= distanceY : y >= positionXY[1];)
                {
                    if (Math.Abs(x) == Math.Abs(y))
                    {

                        if (ChessController.Board.gameSpace[x, y].GetType() != typeof(EmptyPiece))
                        {
                            if (((piece.XPosition - positionXY[0] > 0) ? x == positionXY[0] : x == distanceX) && ((piece.YPosition - positionXY[1] > 0) ? y == positionXY[1] : y == distanceY))
                            {
                                if (!(Char.IsLower(piece.GetSymbol()) ? Char.IsUpper(ChessController.Board.gameSpace[x, y].GetSymbol()) : Char.IsLower(ChessController.Board.gameSpace[x, y].GetSymbol())))
                                {
                                    throw new Exception("You cannot take your own piece");
                                }
                            }
                            else
                            {
                                throw new Exception("Cannot move through other pieces");
                            }

                        }
                    }

                    if (piece.XPosition - positionXY[0] > 0)
                        x++;
                    else
                        x--;

                    if (piece.YPosition - positionXY[1] > 0)
                        y++;
                    else
                        y--;
                }
            }

            return 0;
        }

        public Bishop(String color, int x, int y) : base(color.Equals("White") ? 'b' : 'B', x, y)
        {
        }
    }
}
