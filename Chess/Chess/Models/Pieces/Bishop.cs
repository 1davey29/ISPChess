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
                return 4;
            }
            
            if (distanceX != distanceY)
            {
                return 1;
            }

            int validationReturn = PieceInWay(this, positionXY, distanceX, distanceY, false);
            if (validationReturn == 0)
            {
                UpdateBoard(positionXY);
            }
            
            return validationReturn;
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

            bool IsXPositive = (piece.XPosition - positionXY[0] > 0);
            bool IsYPositive = (piece.YPosition - positionXY[1] > 0);

            int xIter = 0;
            int yIter = 0;
            for (int x = piece.XPosition; (IsXPositive) ? x > positionXY[0] : x < positionXY[0];)
            {
                xIter++;
                yIter = 0;
                for (int y = piece.YPosition; (IsYPositive) ? y > positionXY[1] : y < positionXY[1];)
                {
                    yIter++;
                    if (xIter != 1 && (xIter == yIter))
                    {
                        if (ChessController.Board.gameSpace[x, y].GetType() != typeof(EmptyPiece))
                        {
                            int[] testedPositon = { x, y };
                            if (piece.IsSameColor(testedPositon))
                            {
                                return 2;
                            }

                            if (!((x == positionXY[0]) && (y == positionXY[1])))
                            {
                                if (ChessController.Board.gameSpace[x, y].GetType() == typeof(King))
                                {
                                    return 5;
                                }

                                return 3;
                            }
                        }
                    }


                    if (IsYPositive)
                        y--;
                    else
                        y++;
                }
                    if (IsXPositive)
                        x--;
                    else
                        x++;
            }

            return 0;
        }

        public Bishop(String color, int x, int y) : base(color.Equals("White") ? 'b' : 'B', x, y)
        {
        }
    }
}
