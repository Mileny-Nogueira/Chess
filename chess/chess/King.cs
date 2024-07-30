using board;

namespace chess
{
    internal class King : Piece
    {
        public King(Board board, Color color) : base(board, color)
        {

        }

        public override string ToString()
        {
            return "K";
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
            if (board.isValidPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }

            //Northwest
            pos.defineValues(position.row - 1, position.column + 1);
            if (board.isValidPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }

            //East
            pos.defineValues(position.row, position.column + 1);
            if (board.isValidPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }

            //Southeast
            pos.defineValues(position.row + 1, position.column + 1);
            if (board.isValidPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }

            //South
            pos.defineValues(position.row + 1, position.column);
            if (board.isValidPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }

            //Southwest
            pos.defineValues(position.row + 1, position.column - 1);
            if (board.isValidPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }

            //West
            pos.defineValues(position.row, position.column - 1);
            if (board.isValidPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }

            //Northwest
            pos.defineValues(position.row - 1, position.column - 1);
            if (board.isValidPosition(pos) && canMove(pos))
            {
                mat[pos.row, pos.column] = true;
            }
            return mat;
        }
    }
}
