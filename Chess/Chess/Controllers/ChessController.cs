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

        }

        private static void PlacePiece(String move)
        {

        }

        private static void MoveOnePiece(String move)
        {

        }

        private static void MoveTwoPieces(String move)
        {

        }
    }
}
