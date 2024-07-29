using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace board
{
    internal class Piece
    {
        public Position position { get; set; }
        public Color color { get; protected set; }
        public int moveCount { get; protected set; }
        public Board board { get; protected set; }

        public Piece (Position position, Color color, Board board)
        {
            this.position = position;
            this.color = color;
            this.board = board;
            this.moveCount = 0;
        }
    }   
}
