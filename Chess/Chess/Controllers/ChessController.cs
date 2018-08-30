using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Enums;
using Chess.Models;
using Chess.Models.Pieces;

namespace Chess.Controllers
{
    public static class ChessController
    {
        public static Board Board { get; set; } = new Board(LaunchState.NoPawns);
        public static bool IsWhite { get; set; } = true;

        public static void Run()
        {
            bool isGameOver = false;

            do
            {
                Board.DisplayBoard();
                isGameOver = TakeTurn();
            } while (!isGameOver);

            Board.DisplayBoard();

            Console.WriteLine($"Game over, {(IsWhite ? "White" : "Black")} wins!");
        }

        public static bool TakeTurn()
        {
            Console.WriteLine("Turn: " + (IsWhite ? "White" : "Black"));

            bool inCheck = Board.IsKingInCheck(IsWhite)[2] == 1;

            pickNewPiece:

            if (inCheck)
            {
                Console.WriteLine("Your King is in check!\n");
            }

            Console.WriteLine("Pieces that can be moved:");

            List<string> pieces = Board.GetMovablePieces();

            if (inCheck)
            {
            }

            Console.Write("\nWhich piece would you like to move? (type the alphanumeric position): ");

            string pieceString = Console.ReadLine().ToLower();

            while (!pieces.Contains(pieceString))
            {
                Console.WriteLine("\n\nError, Invalid Piece");
                Console.Write("\nWhich piece would you like to move? (type the alphanumeric position): ");
                pieceString = Console.ReadLine().ToLower();
            }

            Piece piece = Board.GetPieceAt(pieceString);

            List<string> moves = piece.GetAvailableMoves();

            Console.Write("\nWhere would you like to move your piece? (type the alphanumberic position, type your piece's position to cancel): ");

            foreach (string move1 in moves)
            {
                Console.WriteLine(move1);
            }

            string move = Console.ReadLine().ToLower();

            while (!moves.Contains(move))
            {
                Console.WriteLine("Error, Invalid Move");
                Console.Write("\nWhere would you like to move your piece? (type the alphanumberic position, type your piece's position to cancel): ");
                move = Console.ReadLine().ToLower();
            }

            int type = RecognizeMoveType(move);

            switch (type)
            {
                case 2:

                    if (IsWhite == Char.IsLower(piece.GetSymbol()))
                    {

                        int movementResult = piece.Move(ConvertToXY(move), true);

                        switch (movementResult)
                        {
                            case 0:
                                Console.WriteLine("---------------");
                                break;
                            case 1:
                                Console.WriteLine($"Invalid movement for a {piece.GetType().Name}!");
                                break;
                            case 2:
                                Console.WriteLine("You cannot move through your own pieces!");
                                break;
                            case 3:
                            case 5:
                                Console.WriteLine("You cannot move through enemy pieces!");
                                break;
                            case 4:
                                Console.WriteLine("Invalid movement, out of bounds!");
                                break;
                            case 6:
                                goto pickNewPiece;
                        }

                        Board.DisplayBoard();

                        if (Board.IsKingInCheckmate(!IsWhite))
                        {
                            return true;
                        }

                        IsWhite = !IsWhite;
                    }

                    break;

                case 3:

                    //bool lastValid = false;
                    //int[] piece1 = ConvertToXY(move.Substring(0, 2));

                    //if (isWhite == Char.IsLower(Board.gameSpace[piece1[0], piece1[1]].GetSymbol()))
                    //{

                    //    if (Board.gameSpace[piece1[0], piece1[1]].Move(move.Substring(3, 2)) == 0)
                    //    {
                    //        lastValid = true;
                    //    }
                    //    else
                    //    {
                    //        lastValid = false;
                    //    }

                    //}
                    //else if (Board.gameSpace[piece1[0], piece1[1]].GetSymbol().Equals('-'))
                    //{
                    //    Console.WriteLine("There is no piece at that location to move");
                    //}
                    //else
                    //{
                    //    Console.WriteLine("You cannot move the opponents pieces");
                    //}

                    //int[] piece2 = ConvertToXY(move.Substring(6, 2));

                    //if (isWhite == Char.IsLower(Board.gameSpace[piece2[0], piece2[1]].GetSymbol()) && lastValid)
                    //{

                    //    if (Board.gameSpace[piece2[0], piece2[2]].Move(move.Substring(9)) == 0)
                    //    {
                    //        Console.WriteLine("---------------");
                    //        Board.DisplayBoard();

                    //        isWhite = !isWhite;
                    //    }

                    //}
                    //else if (Board.gameSpace[piece2[0], piece2[1]].GetSymbol().Equals('-') && lastValid)
                    //{
                    //    Console.WriteLine("There is no piece at that location to move");
                    //}
                    //else if (lastValid)
                    //{
                    //    Console.WriteLine("You cannot move the opponents pieces");
                    //}

                    break;

                case 0:

                    //Console.WriteLine("---------------");
                    //Console.WriteLine($"Invalid move");

                    break;

                default:

                    Console.WriteLine("Error in code");

                    break;
            }

            return false;
        }

        private static int RecognizeMoveType(String move)
        {
            switch (move.Length)
            {
                case 2:

                    //Move one piece
                    return 2;

                case 5:

                    //Move two pieces
                    return 3;

                default:

                    //Invalid move
                    return 0;
            }
        }

        private static int PlacePiece(String move)
        {
            String pieceAcronym = move.Substring(0, 2);
            String color = (pieceAcronym.Substring(1).Equals("l") ? "White" : (pieceAcronym.Substring(1).Equals("d") ? "Black" : "invalid"));

            if (color.Equals("invalid"))
                return 1;

            String square = move.Substring(2);
            Int32 y;

            if (!Int32.TryParse(square.Substring(1), out y))
                return 1;

            y = 8 - y;

            Int32 x = (Convert.ToInt32(Convert.ToChar(square.Substring(0, 1).ToUpper()) - 65));

            if (Board.gameSpace[x, y] is EmptyPiece)
            {

                switch (pieceAcronym.Substring(0, 1).ToUpper())
                {
                    case "K":

                        Board.gameSpace[x, y] = new King(color, x, y);

                        break;

                    case "Q":

                        Board.gameSpace[x, y] = new Queen(color, x, y);

                        break;

                    case "B":

                        Board.gameSpace[x, y] = new Bishop(color, x, y);

                        break;

                    case "N":

                        Board.gameSpace[x, y] = new Knight(color, x, y);

                        break;

                    case "R":

                        Board.gameSpace[x, y] = new Rook(color, x, y);

                        break;

                    case "P":

                        Board.gameSpace[x, y] = new Pawn(color, x, y);

                        break;

                    default:

                        return 1;
                }

                return 0;

            }
            else
            {
                Console.WriteLine("Invalid position, a piece already exists on that square!");

                return 2;
            }
        }

        public static int[] ConvertToXY(string position)
        {
            Int32 y;

            Int32.TryParse(position.Substring(1), out y);

            y = 8 - y;

            Int32 x = (Convert.ToInt32(Convert.ToChar(position.Substring(0, 1).ToUpper()) - 65));

            int[] positionXY = { x, y };
            return positionXY;
        }
    }
}
