using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Chess.Controllers;

namespace Chess.Models.Pieces
{
    public class Rook : Piece
    {
        private bool hasMoved = false;

        public override int Move(int[] positionXY, bool isActuallyMoving)
        {
            int distanceX = Math.Abs(XPosition - positionXY[0]);
            int distanceY = Math.Abs(YPosition - positionXY[1]);

            if ((positionXY[0] < 0 || positionXY[0] > 7) || (positionXY[1] < 0 || positionXY[1] > 7))
            {
                return 4;
            }

            if (distanceX != 0 && distanceY != 0)
            {
                return 1;
            }

            int valid;

            if (distanceX == 0 && distanceY != 0)
            {
                valid = ValidRookMove(distanceY, positionXY[1], false, this, false);
            }
            else if (distanceY == 0 && distanceX != 0)
            {
                valid = ValidRookMove(distanceX, positionXY[0], true, this, false);
            }
            else if (distanceX == 0 && distanceY == 0)
            {
                return 6;
            }
            else
            {
                throw new Exception("Error in code, unreachable state");
            }

            if (valid != 0)
            {
                return valid;
            }

            if (isActuallyMoving)
            {
            UpdateBoard(positionXY);
            hasMoved = true;
            }

            return 0;
        }

        public static int ValidRookMove(int absDist, int changedPos, bool isX, Object obj, bool isQueen)
        {
            Piece piece;

            if (isQueen)
            {
                piece = obj as Queen;
            }
            else
            {
                piece = obj as Rook;
            }

            bool taken = false;

            int value = 0;

            for (int i = 0; i < absDist; i++)
            {
                if (isX)
                {
                    if (changedPos - piece.XPosition > 0)
                    {
                        int type = (ChessController.Board.gameSpace[changedPos - i, piece.YPosition].GetType() == typeof(EmptyPiece) ? 0 :
                            (ChessController.Board.gameSpace[changedPos - i, piece.YPosition].GetType() == typeof(King) ? 1 : 2));

                        switch (type)
                        {
                            case 0:

                                break;

                            case 1:

                                if (Char.IsUpper(ChessController.Board.gameSpace[changedPos - i, piece.YPosition].GetSymbol()) ^ Char.IsUpper(piece.GetSymbol()))
                                {
                                    //Change later, king check
                                    value = 5;
                                    break;
                                }
                                else
                                {
                                    return 2;
                                }

                            case 2:

                                if (Char.IsUpper(ChessController.Board.gameSpace[changedPos - i, piece.YPosition].GetSymbol()) ^ Char.IsUpper(piece.GetSymbol()))
                                {
                                    if (i == 0)
                                    {
                                        taken = true;

                                        break;
                                    }
                                    else
                                    {
                                        return 3;
                                    }
                                }
                                else
                                {
                                    return 2;
                                }

                            default:

                                break;
                        }
                    }
                    else
                    {
                        int type = (ChessController.Board.gameSpace[changedPos + i, piece.YPosition].GetType() == typeof(EmptyPiece) ? 0 :
                            (ChessController.Board.gameSpace[changedPos + i, piece.YPosition].GetType() == typeof(King) ? 1 : 2));

                        switch (type)
                        {
                            case 0:

                                break;

                            case 1:

                                if (Char.IsUpper(ChessController.Board.gameSpace[changedPos + i, piece.YPosition].GetSymbol()) ^ Char.IsUpper(piece.GetSymbol()))
                                {
                                    value = 5;
                                    break;
                                }
                                else
                                {
                                    return 2;
                                }

                            case 2:

                                if (Char.IsUpper(ChessController.Board.gameSpace[changedPos + i, piece.YPosition].GetSymbol()) ^ Char.IsUpper(piece.GetSymbol()))
                                {
                                    if (i == 0)
                                    {

                                        taken = true;

                                        break;
                                    }
                                    else
                                    {
                                        return 3;
                                    }
                                }
                                else
                                {
                                    return 2;
                                }

                            default:

                                break;
                        }
                    }
                }
                else
                {
                    if (changedPos - piece.YPosition > 0)
                    {
                        int type = (ChessController.Board.gameSpace[piece.XPosition, changedPos - i].GetType() == typeof(EmptyPiece) ? 0 :
                            (ChessController.Board.gameSpace[piece.XPosition, changedPos - i].GetType() == typeof(King) ? 1 : 2));

                        switch (type)
                        {
                            case 0:

                                break;

                            case 1:

                                if (Char.IsUpper(ChessController.Board.gameSpace[piece.XPosition, changedPos - i].GetSymbol()) ^ Char.IsUpper(piece.GetSymbol()))
                                {
                                    value = 5;
                                    break;
                                }
                                else
                                {
                                    return 2;
                                }

                            case 2:

                                if (Char.IsUpper(ChessController.Board.gameSpace[piece.XPosition, changedPos - i].GetSymbol()) ^ Char.IsUpper(piece.GetSymbol()))
                                {
                                    if (i == 0)
                                    {
                                        taken = true;

                                        break;
                                    }
                                    else
                                    {
                                        return 3;
                                    }
                                }
                                else
                                {
                                    return 2;
                                }

                            default:

                                break;
                        }
                    }
                    else
                    {
                        int type = (ChessController.Board.gameSpace[piece.XPosition, changedPos + i].GetType() == typeof(EmptyPiece) ? 0 :
                            (ChessController.Board.gameSpace[piece.XPosition, changedPos + i].GetType() == typeof(King) ? 1 : 2));

                        switch (type)
                        {
                            case 0:

                                break;

                            case 1:

                                if (Char.IsUpper(ChessController.Board.gameSpace[piece.XPosition, changedPos + i].GetSymbol()) ^ Char.IsUpper(piece.GetSymbol()))
                                {
                                    value = 5;
                                    break;
                                }
                                else
                                {
                                    return 2;
                                }

                            case 2:

                                if (Char.IsUpper(ChessController.Board.gameSpace[piece.XPosition, changedPos + i].GetSymbol()) ^ Char.IsUpper(piece.GetSymbol()))
                                {
                                    if (i == 0)
                                    {
                                        taken = true;

                                        break;
                                    }
                                    else
                                    {
                                        return 3;
                                    }
                                }
                                else
                                {
                                    return 2;
                                }

                            default:

                                break;
                        }
                    }
                }
            }

            return value;
        }

        public List<string> GetAvailableMoves(bool isQueen)
        {
            List<string> movablePositions = new List<string>();

            for (int i = 0; i < 8; i++)
            {
                if (i != XPosition)
                {
                    int[] pos = new int[2] { i, YPosition };

                    if (Move(pos, false) == 0 || Move(pos, false) == 6)
                    {
                        movablePositions.Add($"{ Convert.ToString(Convert.ToChar(pos[0] + 97)) }{ Math.Abs(pos[1] - 8) }");
                    }
                }
                else
                {
                    for (int j = 0; j < 8; j++)
                    {
                        int[] pos = new int[2] { XPosition, j };

                        if (Move(pos, false) == 0 || Move(pos, false) == 6)
                        {
                            movablePositions.Add($"{ Convert.ToString(Convert.ToChar(pos[0] + 97)) }{ Math.Abs(pos[1] - 8) }");
                        }
                    }
                }
            }

            bool inCheck = ChessController.Board.IsKingInCheck(ChessController.IsWhite)[2] == 1;

            if (inCheck && !isQueen)
            {
                List<string> remove = new List<string>();
                int xPos = XPosition;
                int yPos = YPosition;

                foreach (string move in movablePositions)
                {
                    Piece tempPiece = ChessController.Board.GetPieceAt(move);
                    Piece currentPieceClone = new Rook((char.IsLower(GetSymbol()) ? "White" : "Black"), xPos, yPos);

                    //BUG: Pawn promotes if can block or take to last row
                    XPosition = xPos;
                    YPosition = yPos;
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
                    movablePositions.Remove(move);
                }

                XPosition = xPos;
                YPosition = yPos;
            }

            if (!movablePositions.Contains($"{ Convert.ToString(Convert.ToChar(XPosition + 97)) }{ Math.Abs(YPosition - 8) }"))
            {
                movablePositions.Add($"{ Convert.ToString(Convert.ToChar(XPosition + 97)) }{ Math.Abs(YPosition - 8) }");
            }

            return movablePositions;
        }

        public override List<string> GetAvailableMoves()
        {
            return GetAvailableMoves(false);
        }

        public Rook(String color, int xPosition, int yPosition) : base(color.Equals("White") ? 'r' : 'R', xPosition, yPosition)
        {
        }
    }
}
