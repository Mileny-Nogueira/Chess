using board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess.chess
{
    internal class Knight : Piece
    {
        public Knight(Board board, Color color) : base(board, color)
        {

        }

        public override string ToString()
        {
            return "N";
        }

        private bool canMove(Position pos)
        {
            Piece p = board.piece(pos);
            return p == null || p.color != color;
        }

        public override bool[,] possibleMoves()
        {
            bool[,] mat = new bool[board.rows, board.columns];
            Position pos = new Position(0, 0);

            pos.defineValues(position.row - 1, position.column - 2);
            if(board.isValidPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }
            pos.defineValues(position.row - 2, position.column - 1);
            if (board.isValidPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }
            pos.defineValues(position.row - 2, position.column + 1);
            if (board.isValidPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }
            pos.defineValues(position.row - 1, position.column + 2);
            if (board.isValidPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }
            pos.defineValues(position.row + 1, position.column + 2);
            if (board.isValidPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }
            pos.defineValues(position.row + 2, position.column + 1);
            if (board.isValidPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }
            pos.defineValues(position.row + 2, position.column - 1);
            if (board.isValidPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }
            pos.defineValues(position.row + 1, position.column - 2);
            if (board.isValidPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }

            return mat;
        }
    }
}
