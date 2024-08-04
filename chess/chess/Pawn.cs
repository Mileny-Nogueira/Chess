using board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chess.chess
{
    internal class Pawn : Piece
    {
        private ChessGame game;
        public Pawn(Board board, Color color, ChessGame game) : base(board, color)
        {
            this.game = game;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool existEnemy(Position pos)
        {
            Piece p = board.piece(pos);
            return p != null && p.color != color;
        }

        private bool free(Position pos)
        {
            return board.piece(pos) == null;
        }

        public override bool[,] possibleMoves()
        {
            bool[,] mat = new bool[board.rows, board.columns];
            Position pos = new Position(0, 0);

            if(color == Color.White)
            {
                pos.defineValues(position.row - 1, position.column);
                if(board.isValidPosition(pos) && free(pos))
                {
                    mat[pos.row, pos.column] = true;
                }
                pos.defineValues(position.row - 2, position.column);
                Position p2 = new Position(position.row - 1, position.column);
                if(board.isValidPosition(p2) && free(p2) && board.isValidPosition(pos) && free(pos) && moveCount == 0)
                {
                    mat[pos.row, pos.column] = true;
                }
                pos.defineValues(position.row - 1, position.column - 1);
                if(board.isValidPosition(pos) && existEnemy(pos))
                {
                    mat[pos.row, pos.column] = true;
                }
                pos.defineValues(position.row - 1, position.column + 1);
                if (board.isValidPosition(pos) && existEnemy(pos))
                {
                    mat[pos.row, pos.column] = true;
                }
            }
            else
            {
                pos.defineValues(position.row + 1, position.column);
                if(board.isValidPosition(pos) && free(pos))
                {
                    mat[pos.row, pos.column] = true;
                }
                pos.defineValues(position.row + 2, position.column);
                Position p3 = new Position(position.row + 1, position.column);
                if(board.isValidPosition(p3) && free(p3) && board.isValidPosition(pos) && free(pos) && moveCount == 0)
                {
                    mat[pos.row, pos.column] = true;
                }
                pos.defineValues(position.row + 1, position.column - 1);
                if(board.isValidPosition(pos) && existEnemy(pos))
                {
                    mat[pos.row, pos.column] = true;
                }
                pos.defineValues(position.row + 1, position.column + 1);
                if(board.isValidPosition(pos) && existEnemy(pos))
                {
                    mat[pos.row, pos.column] = true;
                }
            }
            return mat;
        }
    }
}
