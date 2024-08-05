using board;

namespace chess
{
    internal class King : Piece
    {
        private ChessGame game;
        public King(Board board, Color color, ChessGame game) : base(board, color)
        {
            this.game = game;
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

        private bool testRookForCastling(Position pos)
        {
            Piece p = board.piece(pos);
            return p != null && p is Rook && p.color == color && p.moveCount == 0;
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
            //Castle
            if (moveCount == 0 && !game.check)
            {
                //Castle Kingside
                Position posR1 = new Position(position.row, position.column + 3);
                if(testRookForCastling(posR1))
                {
                    Position p1 = new Position(position.row, position.column + 1);
                    Position p2 = new Position(position.row, position.column + 2);
                    if (board.piece(p1) == null && board.piece(p2) == null)
                    {
                        mat[position.row, position.column + 2] = true;
                    }
                }
                //Castle Queenside
                Position posR2 = new Position(position.row, position.column - 4);
                if (testRookForCastling(posR2))
                {
                    Position p1 = new Position(position.row, position.column - 1);
                    Position p2 = new Position(position.row, position.column - 2);
                    Position p3 = new Position(position.row, position.column - 3);
                    if(board.piece(p1) == null && board.piece(p2) == null && board.piece(p3) == null)
                    {
                        mat[position.row, position.column - 2] = true;
                    }
                }
            }
            return mat;
        }
    }
}
