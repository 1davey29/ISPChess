using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models.Pieces
{
    public class Queen : Piece
    {
        public override void Move(string newPosition)
        {
            throw new NotImplementedException();
        }

        public Queen(String color) : base(color.Equals("White") ? 'q' : 'Q')
        {
        }
    }
}
