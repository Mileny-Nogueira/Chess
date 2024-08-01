using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace board
{
    abstract internal class Piece
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

        public void incrementMoveCounter()
        {
            moveCount++;
        }

        public void decrementMoveCounter()
        {
            moveCount--;
        }

        public bool existsPossibleMoves()
        {
            bool[,] mat = possibleMoves();
            for (int i = 0; i < board.rows; i++)
            {
                for (int j = 0; j < board.columns; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool canMoveTo(Position position)
        {
            return possibleMoves()[position.row, position.column];
        }

        public abstract bool[,] possibleMoves();
    }   
}
