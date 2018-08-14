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
            int[] positionXY;

            positionXY = Chess.Controllers.ChessController.ConvertToXY(newPosition);

            int distanceX = Math.Abs(XPosition - positionXY[0]);
            int distanceY = Math.Abs(YPosition - positionXY[1]);

            //Check if within board next




            if (distanceX == distanceY)
            {
                
            }



            return 0;
        }

        public Bishop(String color) : base(color.Equals("White") ? 'b' : 'B')
        {
        }
    }
}
