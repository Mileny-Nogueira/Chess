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
        public  bool check {  get; private set; }

        public ChessGame()
        {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            pieces = new HashSet<Piece>();
            captureds = new HashSet<Piece>();
            putPieces();
            finished = false;
            check = false;
        }

        public Piece executeMove(Position origin, Position target)
        {
            Piece p = board.deletePiece(origin);
            p.incrementMoveCounter();
            Piece capturedPiece = board.deletePiece(target);
            board.putPiece(p, target);
            if(capturedPiece != null)
            {
                captureds.Add(capturedPiece);
            }
            return capturedPiece;

        }

        public void unmakeMove(Position origin, Position target, Piece capturedPiece)
        {
            Piece p = board.deletePiece(target);
            p.decrementMoveCounter();
            if(capturedPiece != null)
            {
                board.putPiece(capturedPiece, target);
                captureds.Remove(capturedPiece);
            }
            board.putPiece(p, origin);
        }

        public void makeMove(Position origin, Position target)
        {
            Piece capturedPiece = executeMove(origin, target);
            if (isInCheck(currentPlayer))
            {
                unmakeMove(origin, target, capturedPiece);
                throw new BoardException("You can't put you in check!");
            }

            if (isInCheck(adversary(currentPlayer)))
            {
                check = true;
            }
            else
            {
                check = false;
            }

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
        
        private Color adversary(Color color)
        {
            if(color == Color.White)
            {
                return Color.Yellow;
            }
            else
            {
                return Color.White;
            }
        }

        private Piece king(Color color)
        {
            foreach (Piece x in piecesInGame(color))
            {
                if (x is King)
                {
                    return x;
                }
            }
            return null;
        }

        public bool isInCheck(Color color)
        {
            Piece K = king(color);
            if(K == null)
            {
                throw new BoardException("There is no " + color + " King in board.");
            }
            foreach(Piece x in piecesInGame(adversary(color)))
            {
                bool[,] mat = x.possibleMoves();
                if(mat[K.position.row, K.position.column])
                {
                    return true;
                }
            }
            return false;
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
