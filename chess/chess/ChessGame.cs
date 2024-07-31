using System;
using System.Collections.Generic;
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
        private HashSet<Piece> pieces;
        private HashSet<Piece> captureds;

        public ChessGame()
        {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            pieces = new HashSet<Piece>();
            captureds = new HashSet<Piece>();
            putPieces();
            finished = false;
        }

        public void executeMove(Position origin, Position target)
        {
            Piece p = board.deletePiece(origin);
            p.incrementMoveCounter();
            Piece capturedPiece = board.deletePiece(target);
            board.putPiece(p, target);
            if(capturedPiece != null)
            {
                captureds.Add(capturedPiece);
            }

        }

        public void makeMove(Position origin, Position target)
        {
            executeMove(origin, target);
            turn++;
            changePlayer();
        }

        public void validateOriginPosition(Position position)
        {
            if(board.piece(position) == null)
            {
                throw new BoardException("There is no piece in this position!");
            }
            if(currentPlayer != board.piece(position).color)
            {
                throw new BoardException("This is not your piece, please choose a piece with the " + currentPlayer + " color!");
            }
            if(!board.piece(position).existsPossibleMoves())
            {
                throw new BoardException("There are no possible moves for this piece!");
            }
        }

        public void validateTargetPosition(Position origin, Position target)
        {
            if(!board.piece(origin).canMoveTo(target))
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

        public HashSet<Piece> piecesInGame(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach(Piece x in pieces)
            {
                if(x.color == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(capturedPieces(color));
            return aux;
        }

        public HashSet<Piece> capturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in captureds)
            {
                if(x.color == color)
                {
                    aux.Add(x);
                }
            }
            return aux;
        } 

        public void putNewPiece(char column, int row, Piece piece)
        {
            board.putPiece(piece, new ChessPosition(column, row).toPosition());
            pieces.Add(piece);
        }

        private void putPieces()
        {
            putNewPiece('c', 1, new Rook(board, Color.White));
            putNewPiece('c', 2, new Rook(board, Color.White));
            putNewPiece('d', 2, new Rook(board, Color.White));
            putNewPiece('e', 2, new Rook(board, Color.White));
            putNewPiece('e', 1, new Rook(board, Color.White));
            putNewPiece('d', 1, new King(board, Color.White));

            putNewPiece('c', 7, new Rook(board, Color.Yellow));
            putNewPiece('c', 8, new Rook(board, Color.Yellow));
            putNewPiece('d', 7, new Rook(board, Color.Yellow));
            putNewPiece('e', 7, new Rook(board, Color.Yellow));
            putNewPiece('e', 8, new Rook(board, Color.Yellow));
            putNewPiece('d', 8, new King(board, Color.Yellow));
        }
    }
}
