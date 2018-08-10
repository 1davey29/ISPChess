using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Models
{
    public abstract class Piece
    {
        public Int16 XPosition { get; set; }
        public Int16 YPosition { get; set; }

        private readonly Char symbol;

        public abstract void Move(String newPosition);

        public Piece()
        {
            symbol = '-';
        }
    }
}
