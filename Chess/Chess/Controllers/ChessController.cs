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
    class ChessController
    {
        private static Board board = new Board();
        private static string filePath;

        public static void Run()
        {
            Console.Write("Please enter the file path of your move file: ");

            SetMoveFilePath(Console.ReadLine());

            List<String> moves = LoadMoveFile();

            foreach (String move in moves)
            {
                int type = RecognizeMoveType(move);

                switch (type)
                {
                    case 1:

                        PlacePiece(move);

                        break;

                    case 2:

                        MoveOnePiece(move);

                        break;

                    case 3:

                        MoveTwoPieces(move);

                        break;

                    case 0:

                        Console.WriteLine("Invalid Move");

                        break;

                    default:

                        Console.WriteLine("Error in code");

                        break;
                }
            }
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
            String color = (pieceAcronym.Substring(1).Equals("l") ? "white " : (pieceAcronym.Substring(1).Equals("d") ? "black " : "invalid"));

            if (color.Equals("invalid"))
                return 1;

            String square = move.Substring(2);
            Int16 y;

            if (!Int16.TryParse(square.Substring(1), out y))
            {
                throw new FormatException("Error: Invalid square format");
            }

            Int16 x = Convert.ToInt16(Convert.ToChar(square.Substring(0, 1).ToUpper()) - 65);

            if (board.gameSpace[x, y] is EmptyPiece)
            {

                switch (pieceAcronym.Substring(0, 1))
                {
                    case "K":

                        board.gameSpace[x, y] = new King(color);

                        break;

                    case "Q":

                        board.gameSpace[x, y] = new Queen(color);

                        break;

                    case "B":

                        board.gameSpace[x, y] = new Bishop(color);

                        break;

                    case "N":

                        board.gameSpace[x, y] = new Knight(color);

                        break;

                    case "R":

                        board.gameSpace[x, y] = new Rook(color);

                        break;

                    case "P":

                        board.gameSpace[x, y] = new Pawn(color);

                        break;

                    default:

                        return 1;
                }

                return 0;
            } else
            {
                Console.WriteLine("Invalid position, a piece already exists on that square!");
            }
        }

        private static void MoveOnePiece(String move)
        {
            String[] moveSteps = move.Split(' ');

            Console.WriteLine($"Piece moved from {moveSteps[0]} to {moveSteps[1]}");
        }

        private static void MoveTwoPieces(String move)
        {
            String[] moves = move.Split(' ');
            String[] move1Steps = { moves[0], moves[1] };
            String[] move2Steps = { moves[2], moves[3] };

            Console.WriteLine($"Piece moved from {move1Steps[0]} to {move1Steps[1]}, and piece moved from {move2Steps[0]} to {move2Steps[1]}");
        }
    }
}
