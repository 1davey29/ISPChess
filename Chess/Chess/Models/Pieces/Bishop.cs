using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models.Pieces
{
    public class Bishop : Piece
    {
        public override int Move(string newPosition)
        {
            Int32 x;
            if (!Int32.TryParse(newPosition.Substring(1), out x))
            {
                return 1;
            }

            x = 8 - x;

            Int32 y = (Convert.ToInt32(Convert.ToChar(newPosition.Substring(0, 1).ToUpper()) - 65));

            

            return 0;
        }

        public Bishop(String color) : base(color.Equals("White") ? 'b' : 'B')
        {
        }
    }
}
