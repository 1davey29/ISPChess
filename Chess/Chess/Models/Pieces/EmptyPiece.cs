using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models.Pieces
{
    public class EmptyPiece : Piece
    {
        public override int Move(string newPosition)
        {
            return 0;
        }

        public EmptyPiece(int xPosition, int yPosition) : base('-', xPosition, yPosition)
        {
        }
    }
}
