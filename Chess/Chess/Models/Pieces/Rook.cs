﻿using System;
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

            ChessController.Board.gameSpace[positionXY[0], positionXY[1]] = this;
            ChessController.Board.gameSpace[XPosition, YPosition] = new EmptyPiece(XPosition, YPosition);

            XPosition = positionXY[0];
            YPosition = positionXY[1];

            return 0;
        }

        public Rook(String color, int xPosition, int yPosition) : base(color.Equals("White") ? 'r' : 'R', xPosition, yPosition)
        {
        }
    }
}
