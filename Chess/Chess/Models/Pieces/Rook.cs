using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models.Pieces
{
    public class Rook : Piece
    {
        public override int Move(string newPosition)
        {
            throw new NotImplementedException();
        }

        public Rook(String color) : base(color.Equals("White") ? 'r' : 'R')
        {
        }
    }
}
