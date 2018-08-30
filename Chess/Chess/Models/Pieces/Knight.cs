using Chess.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models.Pieces
{
    public class Knight : Piece
    {
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

            if (!(distanceX == 2 && distanceY == 1) && !(distanceX == 1 && distanceY == 2))
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

                if (isMoving)
                {
                    UpdateBoard(positionXY);
                }
            }

            if (isMoving)
            {
                ChessController.Board.gameSpace[positionXY[0], positionXY[1]] = this;
                ChessController.Board.gameSpace[XPosition, YPosition] = new EmptyPiece(XPosition, YPosition);

                XPosition = positionXY[0];
                YPosition = positionXY[1];
            }

            return 0;
        }

        public override List<string> GetAvailableMoves()
        {
            List<string> availibleMoves = new List<string>();
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    int movementReturn = Move(new int[] { x, y }, false);
                    if (movementReturn == 0 || movementReturn == 6)
                    {
                        availibleMoves.Add($"{ Convert.ToString(Convert.ToChar(x + 97)) }{ Math.Abs(y - 8) }");
                    }
                }
            }

            bool inCheck = ChessController.Board.IsKingInCheck(ChessController.IsWhite)[2] == 1;

            if (inCheck)
            {
                foreach (string move in availibleMoves)
                {
                    Piece tempPiece = ChessController.Board.GetPieceAt(move);
                    int xPos = XPosition;
                    int yPos = YPosition;
                    Piece currentPieceClone = new Knight((char.IsLower(GetSymbol()) ? "White" : "Black"), XPosition, YPosition);

                    //BUG: Pawn promotes if can block or take to last row
                    Move(ChessController.ConvertToXY(move), true);

                    if (ChessController.Board.IsKingInCheck(ChessController.IsWhite)[2] == 1)
                    {
                        availibleMoves.Remove(move);
                    }

                    ChessController.Board.gameSpace[xPos, yPos] = currentPieceClone;
                    ChessController.Board.gameSpace[ChessController.ConvertToXY(move)[0], ChessController.ConvertToXY(move)[1]] = tempPiece;
                }
            }

            return availibleMoves;
        }

        public Knight(String color, int x, int y) : base(color.Equals("White") ? 'n' : 'N', x, y)
        {
        }
    }
}
