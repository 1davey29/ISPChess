using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public abstract class Piece
    {
        private Int16 xPosition;
        private Int16 yPosition;

        public abstract void Move(String newPosition);
    }
}
