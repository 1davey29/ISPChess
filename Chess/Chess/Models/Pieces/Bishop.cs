using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models.Pieces
{
    public class Bishop : Piece
    {
        public override void Move(string newPosition)
        {
            
        }

        public Bishop(String color) : base(color.Equals("White") ? 'b' : 'B')
        {
        }
    }
}
