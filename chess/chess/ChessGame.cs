using System;
using System.Reflection.PortableExecutable;
using board;

namespace chess
{
    internal class ChessGame
    {
        public Board board {  get; private set; }
        private int turn;
        private Color currentPlayer;
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
            p.incrementMoveCount();
            Piece capturedPiece = board.deletePiece(target);
            board.putPiece(p, target);

        }

        private void putPieces()
        {
            board.putPiece(new Rook(board, Color.Yellow), new ChessPosition('c', 1).toPosition());
            board.putPiece(new Rook(board, Color.Yellow), new ChessPosition('c', 2).toPosition());
            board.putPiece(new Rook(board, Color.Yellow), new ChessPosition('d', 2).toPosition());
            board.putPiece(new Rook(board, Color.Yellow), new ChessPosition('e', 2).toPosition());
            board.putPiece(new Rook(board, Color.Yellow), new ChessPosition('e', 1).toPosition());
            board.putPiece(new King(board, Color.Yellow), new ChessPosition('d', 1).toPosition());

            board.putPiece(new Rook(board, Color.White), new ChessPosition('c', 7).toPosition());
            board.putPiece(new Rook(board, Color.White), new ChessPosition('c', 8).toPosition());
            board.putPiece(new Rook(board, Color.White), new ChessPosition('d', 7).toPosition());
            board.putPiece(new Rook(board, Color.White), new ChessPosition('e', 7).toPosition());
            board.putPiece(new Rook(board, Color.White), new ChessPosition('e', 8).toPosition());
            board.putPiece(new King(board, Color.White), new ChessPosition('d', 8).toPosition());
        }
    }
}
