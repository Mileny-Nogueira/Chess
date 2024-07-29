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
        public Board board { get; protected set; }
        public Color color { get; protected set; }
        public int moveCount { get; protected set; }
        

        public Piece(Board board, Color color)
        {
            this.position = null;
            this.board = board;
            this.color = color;
            this.moveCount = 0;
        }

        public void incrementMoveCount()
        {
            moveCount++;
        }
    }   
}
