using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models.Pieces
{
    public class King : Piece
    {
        public override void Move(string newPosition)
        {
            throw new NotImplementedException();
        }

        public King(String color) : base(color.Equals("White") ? 'k' : 'K')
        {
        }
    }
}
