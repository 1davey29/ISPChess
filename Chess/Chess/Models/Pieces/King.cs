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
            List<string> availibleMoves = new List<string>();
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    int movementReturn = Move(new int[] { (x + XPosition), (y + YPosition) }, false);
                    if (movementReturn == 0 || movementReturn == 6)
                    {
                        availibleMoves.Add($"{ Convert.ToString(Convert.ToChar((x + XPosition) + 97)) }{ Math.Abs((y + YPosition) - 8) }");
                    }
                }
            }

            bool inCheck = ChessController.Board.IsKingInCheck(ChessController.IsWhite)[2] == 1;

            if (inCheck)
            {
                List<string> remove = new List<string>();
                int xPos = XPosition;
                int yPos = YPosition;

                foreach (string move in availibleMoves)
                {
                    Piece tempPiece = ChessController.Board.GetPieceAt(move);
                    Piece currentPieceClone = new King((char.IsLower(GetSymbol()) ? "White" : "Black"), xPos, yPos);

                    //BUG: Pawn promotes if can block or take to last row
                    Move(ChessController.ConvertToXY(move), true);

                    if (ChessController.Board.IsKingInCheck(ChessController.IsWhite)[2] == 1)
                    {
                        remove.Add(move);
                    }

                    ChessController.Board.gameSpace[xPos, yPos] = currentPieceClone;
                    ChessController.Board.gameSpace[ChessController.ConvertToXY(move)[0], ChessController.ConvertToXY(move)[1]] = tempPiece;
                }

                foreach (string move in remove)
                {
                    availibleMoves.Remove(move);
                }
            }

            if (!availibleMoves.Contains($"{ Convert.ToString(Convert.ToChar(XPosition + 97)) }{ Math.Abs(YPosition - 8) }"))
            {
                availibleMoves.Add($"{ Convert.ToString(Convert.ToChar(XPosition + 97)) }{ Math.Abs(YPosition - 8) }");
            }

            return availibleMoves;
        }

        public King(String color, int x, int y) : base(color.Equals("White") ? 'k' : 'K', x, y)
        {
        }
    }
}
