﻿using System;
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
        private static Board board = new Board();
        private static string filePath;

        public static void Run()
        {
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

                        PlacePiece(move);

                        break;

                    case 2:

                        MovePiece(move);

                        break;

                    case 3:

                        MoveTwoPieces(move);

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

            board.DisplayBoard();
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

            }
            else
            {
                Console.WriteLine("Invalid position, a piece already exists on that square!");

                return 2;
            }
        }

        private static int MovePiece(String move)
        {
            String square1 = move.Substring(0, 2);
            int x1;

            if (!int.TryParse(square1.Substring(1), out x1))
                return 1;

            x1 = 8 - x1;

            int y1 = Convert.ToInt32(Convert.ToChar(square1.Substring(0, 1).ToUpper()) - 65);

            String square2 = move.Substring(3);
            int x2;

            if (!int.TryParse(square2.Substring(1), out x2))
                return 1;

            x2 = 8 - x2;

            int y2 = Convert.ToInt32(Convert.ToChar(square2.Substring(0, 1).ToUpper()) - 65);

            board.gameSpace[x2, y2] = board.gameSpace[x1, y1];
            board.gameSpace[x1, y1] = new EmptyPiece();

            return 0;
        }

        private static int MoveTwoPieces(String move)
        {
            MovePiece(move.Substring(0, 5));

            MovePiece(move.Substring(6));

            return 0;
        }
    }
}
