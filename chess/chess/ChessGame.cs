using System;
using System.Reflection.PortableExecutable;
using board;

namespace chess
{
    internal class ChessGame
    {
        public Board board {  get; private set; }
        public int turn { get; private set; }
        public Color currentPlayer { get; private set; }
        public bool finished { get; private set; }

        public ChessGame()
        {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            putPieces();
            finished = false;
        }

        public void executeMove(Position origin, Position target)
        {
            Piece p = board.deletePiece(origin);
            p.incrementMoveCounter();
            Piece capturedPiece = board.deletePiece(target);
            board.putPiece(p, target);

        }

        public void makeMove(Position origin, Position target)
        {
            executeMove(origin, target);
            turn++;
            changePlayer();
        }

        public void validateOriginPosition(Position position)
        {
            if (board.piece(position) == null)
            {
                throw new BoardException("There is no piece in this position!");
            }
            if (currentPlayer != board.piece(position).color)
            {
                throw new BoardException("This is not your piece, please choose a piece with the " + currentPlayer + " color!");
            }
            if (!board.piece(position).existsPossibleMoves())
            {
                throw new BoardException("There are no possible moves for this piece!");
            }
        }

        public void validateTargetPosition(Position origin, Position target)
        {
            if (!board.piece(origin).canMoveTo(target))
            {
                throw new BoardException("Target position is invalid!");
            }
        }

        private void changePlayer()
        {
            if(currentPlayer == Color.White)
            {
                currentPlayer = Color.Yellow;
            }
            else
            {
                currentPlayer = Color.White;
            }
        }

        private void putPieces()
        {
            board.putPiece(new Rook(board, Color.White), new ChessPosition('c', 1).toPosition());
            board.putPiece(new Rook(board, Color.White), new ChessPosition('c', 2).toPosition());
            board.putPiece(new Rook(board, Color.White), new ChessPosition('d', 2).toPosition());
            board.putPiece(new Rook(board, Color.White), new ChessPosition('e', 2).toPosition());
            board.putPiece(new Rook(board, Color.White), new ChessPosition('e', 1).toPosition());
            board.putPiece(new King(board, Color.White), new ChessPosition('d', 1).toPosition());

            board.putPiece(new Rook(board, Color.Yellow), new ChessPosition('c', 7).toPosition());
            board.putPiece(new Rook(board, Color.Yellow), new ChessPosition('c', 8).toPosition());
            board.putPiece(new Rook(board, Color.Yellow), new ChessPosition('d', 7).toPosition());
            board.putPiece(new Rook(board, Color.Yellow), new ChessPosition('e', 7).toPosition());
            board.putPiece(new Rook(board, Color.Yellow), new ChessPosition('e', 8).toPosition());
            board.putPiece(new King(board, Color.Yellow), new ChessPosition('d', 8).toPosition());
        }
    }
}
