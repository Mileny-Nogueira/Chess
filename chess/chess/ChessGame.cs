using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Runtime.ConstrainedExecution;
using board;
using chess.chess;

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
            //Castle Kingside
            if (p is King && target.column == origin.column + 2)
            {
                Position originR = new Position(origin.row, origin.column + 3);
                Position targetR = new Position(origin.row, origin.column + 1);
                Piece R = board.deletePiece(originR);
                R.incrementMoveCounter();
                board.putPiece(R, targetR);
            }
            //Castle Queenside
            if (p is King && target.column == origin.column - 2)
            {
                Position originR = new Position(origin.row, origin.column - 4);
                Position targetR = new Position(origin.row, origin.column - 1);
                Piece R = board.deletePiece(originR);
                R.incrementMoveCounter();
                board.putPiece(R, targetR);
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

            if(isInCheck(currentPlayer))
            {
                unmakeMove(origin, target, capturedPiece);
                throw new BoardException("You can't put you in check!");
            }

            if(isInCheck(adversary(currentPlayer)))
            {
                check = true;
            }
            else
            {
                check = false;
            }

            if (testCheckmate(adversary(currentPlayer)))
            {
                finished = true;
            }
            else
            {
                turn++;
                changePlayer();
            }
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

        public bool testCheckmate(Color color)
        {
            if (!isInCheck(color))
            {
                return false;
            }
            foreach(Piece x in piecesInGame(color))
            {
                bool[,] mat = x.possibleMoves();
                for(int i = 0; i < board.rows; i++)
                {
                    for(int j = 0; j < board.columns; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = x.position;
                            Position target = new Position(i, j);
                            Piece capturedPiece = executeMove(x.position, target);
                            bool testCheck = isInCheck(color);
                            unmakeMove(origin, target, capturedPiece);
                            if (!testCheck)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void putNewPiece(char column, int row, Piece piece)
        {
            board.putPiece(piece, new ChessPosition(column, row).toPosition());
            pieces.Add(piece);
        }

        private void putPieces()
        {
            putNewPiece('a', 1, new Rook(board, Color.White));
            putNewPiece('b', 1, new Knight(board, Color.White));
            putNewPiece('c', 1, new Bishop(board, Color.White));
            putNewPiece('d', 1, new Queen(board, Color.White));
            putNewPiece('e', 1, new King(board, Color.White, this));
            putNewPiece('f', 1, new Bishop(board, Color.White));
            putNewPiece('g', 1, new Knight(board, Color.White));
            putNewPiece('h', 1, new Rook(board, Color.White));
            putNewPiece('a', 2, new Pawn(board, Color.White, this));
            putNewPiece('b', 2, new Pawn(board, Color.White, this));
            putNewPiece('c', 2, new Pawn(board, Color.White, this));
            putNewPiece('d', 2, new Pawn(board, Color.White, this));
            putNewPiece('e', 2, new Pawn(board, Color.White, this));
            putNewPiece('f', 2, new Pawn(board, Color.White, this));
            putNewPiece('g', 2, new Pawn(board, Color.White, this));
            putNewPiece('h', 2, new Pawn(board, Color.White, this));

            putNewPiece('a', 8, new Rook(board, Color.Yellow));
            putNewPiece('b', 8, new Knight(board, Color.Yellow));
            putNewPiece('c', 8, new Bishop(board, Color.Yellow));
            putNewPiece('d', 8, new Queen(board, Color.Yellow));
            putNewPiece('e', 8, new King(board, Color.Yellow, this));
            putNewPiece('f', 8, new Bishop(board, Color.Yellow));
            putNewPiece('g', 8, new Knight(board, Color.Yellow));
            putNewPiece('h', 8, new Rook(board, Color.Yellow));
            putNewPiece('a', 7, new Pawn(board, Color.Yellow, this));
            putNewPiece('b', 7, new Pawn(board, Color.Yellow, this));
            putNewPiece('c', 7, new Pawn(board, Color.Yellow, this));
            putNewPiece('d', 7, new Pawn(board, Color.Yellow, this));
            putNewPiece('e', 7, new Pawn(board, Color.Yellow, this));
            putNewPiece('f', 7, new Pawn(board, Color.Yellow, this));
            putNewPiece('g', 7, new Pawn(board, Color.Yellow, this));
            putNewPiece('h', 7, new Pawn(board, Color.Yellow, this));
        }
    }
}
