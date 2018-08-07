using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Controllers
{
    class ChessController
    {
        private static string filePath;

        public static void Run()
        {

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
            String square = move.Substring(2);
            String piece = (pieceAcronym.Substring(1, 2).Equals("l") ? "white " : (pieceAcronym.Substring(1, 2).Equals("d") ? "black " : "invalid"));

            if (piece.Equals("invalid"))
                return 1;

            switch (pieceAcronym.Substring(0, 1))
            {
                case "K":

                    piece += "king";

                    break;

                case "Q":

                    piece += "queen";

                    break;

                case "B":

                    piece += "bishop";

                    break;

                case "N":

                    piece += "knight";

                    break;

                case "R":

                    piece += "rook";

                    break;

                case "P":

                    piece += "pawn";

                    break;

                default:

                    return 1;

                    break;
            }

            Console.WriteLine($"Place {piece} on {square}");

            return 0;
        }

        private static void MoveOnePiece(String move)
        {

        }

        private static void MoveTwoPieces(String move)
        {

        }
    }
}
