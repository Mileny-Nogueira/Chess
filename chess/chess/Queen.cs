using board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace chess.chess
{
    internal class Queen : Piece
    {
        public Queen(Board board, Color color) : base(board, color)
        {

        }

        public override string ToString()
        {
            return "Q";
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

            //North
            pos.defineValues(position.row - 1, position.column);
            while (board.isValidPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.defineValues(pos.row - 1, pos.column);
            }

            //Northest
            pos.defineValues(position.row - 1, position.column + 1);
            while (board.isValidPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.defineValues(pos.row - 1, pos.column + 1);
            }

            //East
            pos.defineValues(position.row, position.column + 1);
            while (board.isValidPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.defineValues(pos.row, pos.column + 1);
            }

            //Southeast
            pos.defineValues(position.row + 1, position.column + 1);
            while (board.isValidPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.defineValues(pos.row + 1, pos.column + 1);
            }

            //South
            pos.defineValues(position.row + 1, position.column);
            while (board.isValidPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.defineValues(pos.row + 1, pos.column);
            }

            //Southwest
            pos.defineValues(position.row + 1, position.column - 1);
            while (board.isValidPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.defineValues(pos.row + 1, pos.column - 1);
            }

            //West
            pos.defineValues(position.row, position.column - 1);
            while (board.isValidPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.defineValues(pos.row, pos.column - 1);
            }

            //Northwest
            pos.defineValues(position.row - 1, position.column - 1);
            while (board.isValidPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.defineValues(pos.row - 1, pos.column - 1);
            }
            return mat;
        }
    }
}
