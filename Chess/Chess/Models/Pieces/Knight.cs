﻿using Chess.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models.Pieces
{
    public class Knight : Piece
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

            if (!(distanceX == 2 && distanceY == 1) || !(distanceX == 1 && distanceY == 2))
            {
                throw new ArgumentException("Invalid move for the specified piece");
            }

            if (ChessController.Board.gameSpace[positionXY[0], positionXY[1]].GetType() != typeof(EmptyPiece))
            {
                if (IsSameColor(positionXY))
                {
                    throw new Exception("You cannot take your own piece");
                }

                UpdateBoard(positionXY);

            }


            ChessController.Board.gameSpace[positionXY[0], positionXY[1]] = this;
            ChessController.Board.gameSpace[XPosition, YPosition] = new EmptyPiece(XPosition, YPosition);

            XPosition = positionXY[0];
            YPosition = positionXY[1];

            return 0;
        }

        public Knight(String color, int x, int y) : base(color.Equals("White") ? 'n' : 'N', x, y)
        {
        }
    }
}
