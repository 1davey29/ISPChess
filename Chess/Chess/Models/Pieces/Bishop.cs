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

            bool IsXPositive = (piece.XPosition - positionXY[0] > 0);
            bool IsYPositive = (piece.YPosition - positionXY[1] > 0);
            for (int x = IsXPositive ? positionXY[0] : piece.XPosition; 
                IsXPositive ? x <= distanceX : x >= positionXY[0];)
            {
                for (int y = IsYPositive ? positionXY[1] : piece.YPosition; 
                    IsYPositive ? y <= distanceY : y >= positionXY[1];)
                {
                    if (Math.Abs(x) == Math.Abs(y))
                    {
                        if (ChessController.Board.gameSpace[x, y].GetType() != typeof(EmptyPiece))
                        {
                            if (piece.IsSameColor(positionXY))
                            {
                                return 2;
                            }

                            if (!((IsXPositive ? x == positionXY[0] : x == distanceX) && 
                                (IsYPositive ? y == positionXY[1] : y == distanceY)))
                            {
                                return 3;
                            }
                        }
                    }

                    if (IsXPositive)
                        x++;
                    else
                        x--;

                    if (IsYPositive)
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
