﻿using System;
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


        public abstract void Move(String newPosition);
    }
}
