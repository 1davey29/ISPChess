using Chess.Controllers;
using Chess.Models.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public abstract class Piece
    {
        public int XPosition { get; set; }
        public int YPosition { get; set; }

        protected readonly Char symbol;

        public abstract int Move(String newPosition);
        public abstract int Move(int[] positionXY);

        public Char GetSymbol()
        {
            return symbol;
        }

        public void UpdateBoard(int[] positionXY)
        {
            ChessController.Board.gameSpace[positionXY[0], positionXY[1]] = this;
            ChessController.Board.gameSpace[XPosition, YPosition] = new EmptyPiece(XPosition, YPosition);

            XPosition = positionXY[0];
            YPosition = positionXY[1];
        }

        public bool IsSameColor(int[] positionXY)
        {
            return !(Char.IsUpper(ChessController.Board.gameSpace[positionXY[0], positionXY[1]].GetSymbol()) ^ Char.IsUpper(GetSymbol()));
        }

        public bool IsMovable()
        {
            List<string> moves = GetAvailableMoves();

            int iter = 0;

            foreach (string move in moves)
            {
                iter++;
            }

            if (iter > 0)
            {
                return true;
            }

            return false;
        }

        public List<string> GetAvailableMoves()
        {
            return new List<string>();
        }

        public Piece(Char symbol, int xPosition, int yPosition)
        {
            this.symbol = symbol;
            XPosition = xPosition;
            YPosition = yPosition;
        }
    }
}
