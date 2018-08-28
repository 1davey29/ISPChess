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
            else
            {
                throw new Exception("Error in code, unreachable state");
            }

            if (valid != 0)
            {
                return valid;
            }

            UpdateBoard(positionXY);

            hasMoved = true;

            return 0;
        }

        public override int Move(int[] positionXY)
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

            UpdateBoard(positionXY);

            hasMoved = true;

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
                                    return 5;
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
                                    return 5;
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
                                    return 5;
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
                                    return 5;
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

            return 0;
        }

        public Rook(String color, int xPosition, int yPosition) : base(color.Equals("White") ? 'r' : 'R', xPosition, yPosition)
        {
        }
    }
}
