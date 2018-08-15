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

            if (distanceX != 0 && distanceY != 0)
            {
                throw new ArgumentException("Error: Invalid move");
            }

            if (distanceX == 0 && distanceY != 0)
            {
                ValidRookMove(distanceY, positionXY[1], false, this, false);
            }
            else if (distanceY == 0 && distanceX != 0)
            {
                ValidRookMove(distanceX, positionXY[0], true, this, false);
            }
            else
            {
                throw new Exception("Error in code, unreachable state");
            }

            ChessController.Board.gameSpace[positionXY[0], positionXY[1]] = this;
            ChessController.Board.gameSpace[XPosition, YPosition] = new EmptyPiece(XPosition, YPosition);

            XPosition = positionXY[0];
            YPosition = positionXY[1];

            return 0;
        }

        protected static int ValidRookMove(int absDist, int changedPos, bool isX, Object obj, bool isQueen)
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
                                    throw new InvalidOperationException("Error: Cannot take Enemy King, Enemy King in check");
                                }
                                else
                                {
                                    throw new InvalidOperationException("Error: Invalid Movement, Cannot move through your own piece");
                                }

                            case 2:

                                if (Char.IsUpper(ChessController.Board.gameSpace[changedPos - i, piece.YPosition].GetSymbol()) ^ Char.IsUpper(piece.GetSymbol()))
                                {
                                    if (i == 0)
                                    {
                                        return 1;
                                    }
                                    else
                                    {
                                        throw new InvalidOperationException("Error: Invalid Movement, Cannot move through enemy piece");
                                    }
                                }
                                else
                                {
                                    throw new InvalidOperationException("Error: Invalid Movement, Cannot move through your own piece");
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
                                    throw new InvalidOperationException("Error: Cannot take Enemy King, Enemy King in check");
                                }
                                else
                                {
                                    throw new InvalidOperationException("Error: Invalid Movement, Cannot move through your own piece");
                                }

                            case 2:

                                if (Char.IsUpper(ChessController.Board.gameSpace[changedPos + i, piece.YPosition].GetSymbol()) ^ Char.IsUpper(piece.GetSymbol()))
                                {
                                    if (i == 0)
                                    {
                                        return 1;
                                    }
                                    else
                                    {
                                        throw new InvalidOperationException("Error: Invalid Movement, Cannot move through enemy piece");
                                    }
                                }
                                else
                                {
                                    throw new InvalidOperationException("Error: Invalid Movement, Cannot move through your own piece");
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
                                    throw new InvalidOperationException("Error: Cannot take Enemy King, Enemy King in check");
                                }
                                else
                                {
                                    throw new InvalidOperationException("Error: Invalid Movement, Cannot move through your own piece");
                                }

                            case 2:

                                if (Char.IsUpper(ChessController.Board.gameSpace[piece.XPosition, changedPos - i].GetSymbol()) ^ Char.IsUpper(piece.GetSymbol()))
                                {
                                    if (i == 0)
                                    {
                                        return 1;
                                    }
                                    else
                                    {
                                        throw new InvalidOperationException("Error: Invalid Movement, Cannot move through enemy piece");
                                    }
                                }
                                else
                                {
                                    throw new InvalidOperationException("Error: Invalid Movement, Cannot move through your own piece");
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
                                    throw new InvalidOperationException("Error: Cannot take Enemy King, Enemy King in check");
                                }
                                else
                                {
                                    throw new InvalidOperationException("Error: Invalid Movement, Cannot move through your own piece");
                                }

                            case 2:

                                if (Char.IsUpper(ChessController.Board.gameSpace[piece.XPosition, changedPos + i].GetSymbol()) ^ Char.IsUpper(piece.GetSymbol()))
                                {
                                    if (i == 0)
                                    {
                                        return 1;
                                    }
                                    else
                                    {
                                        throw new InvalidOperationException("Error: Invalid Movement, Cannot move through enemy piece");
                                    }
                                }
                                else
                                {
                                    throw new InvalidOperationException("Error: Invalid Movement, Cannot move through your own piece");
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
