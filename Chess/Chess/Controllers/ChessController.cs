using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Chess.Models;
using Chess.Models.Pieces;

namespace Chess.Controllers
{
    public static class ChessController
    {
        private static string filePath;

        public static Board Board { get; set; } = new Board("No Pawns");

        public static void Run()
        {
            Board.DisplayBoard();

            Console.Write("Please enter the file path of your move file: ");

            SetMoveFilePath(Console.ReadLine());

            List<String> moves = LoadMoveFile();
            int iter = 1;

            foreach (String move in moves)
            {
                int type = RecognizeMoveType(move);

                switch (type)
                {
                    case 1:

                        //PlacePiece(move);

                        break;

                    case 2:

                        int[] piece = ConvertToXY(move.Substring(0,2));

                        int movementResult = Board.gameSpace[piece[0], piece[1]].Move(move.Substring(3));

                        switch (movementResult)
                        {
                            case 0:
                                break;
                            case 1:
                                Console.WriteLine($"Invalid movement for a {Board.gameSpace[piece[0], piece[1]].GetType().ToString()}!");
                                break;
                            case 2:
                                Console.WriteLine("You cannot move through your own pieces!");
                                break;
                            case 3:
                                Console.WriteLine("You cannot move through enemy pieces!");
                                break;
                            case 4:
                                Console.WriteLine("Invalid movement, out of bounds!");
                                break;
                        }
                        if (movementResult == 0)
                        {
                            Board.DisplayBoard();
                        }

                        break;

                    case 3:

                        int[] piece1 = ConvertToXY(move.Substring(0, 2));

                        if (Board.gameSpace[piece1[0], piece1[1]].Move(move.Substring(3, 2)) == 0)
                        {
                            Board.DisplayBoard();
                        }

                        int[] piece2 = ConvertToXY(move.Substring(6, 2));

                        if (Board.gameSpace[piece2[0], piece2[2]].Move(move.Substring(9)) == 0)
                        {
                            Board.DisplayBoard();
                        }

                        break;

                    case 0:

                        Console.WriteLine($"Move {iter} is an invalid move");

                        break;

                    default:

                        Console.WriteLine("Error in code");

                        break;
                }
                iter++;
            }

            Board.DisplayBoard();
        }

        public static void SetMoveFilePath(string loadPath)
        {
            filePath = loadPath;
        }

        public static List<String> LoadMoveFile()
        {
            List<String> moves = new List<string>();
            StreamReader reader = new StreamReader(filePath);

            try
            {
                do
                {
                    moves.Add(reader.ReadLine());
                }
                while (reader.Peek() != -1);
            }

            catch
            {
                moves.Add("File is empty");
            }

            reader.Close();

            return moves;
        }

        private static int RecognizeMoveType(String move)
        {
            switch (move.Length)
            {
                case 4:

                    //Place a piece
                    return 1;

                case 5:

                    //Move one piece
                    return 2;

                case 11:

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
            Int32 x;

            if (!Int32.TryParse(square.Substring(1), out x))
                return 1;

            x = 8 - x;

            Int32 y = (Convert.ToInt32(Convert.ToChar(square.Substring(0, 1).ToUpper()) - 65));

            if (Board.gameSpace[x, y] is EmptyPiece)
            {

                switch (pieceAcronym.Substring(0, 1))
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
            Int32 x;

            Int32.TryParse(position.Substring(1), out x);

            x = 8 - x;

            Int32 y = (Convert.ToInt32(Convert.ToChar(position.Substring(0, 1).ToUpper()) - 65));

            int[] positionXY = { x, y };
            return positionXY;
        }
    }
}
