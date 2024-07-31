using board;
using chess;
using System.Collections.Generic;

namespace chess
{
    internal class Screen
    {
        public static void printChessGame(ChessGame game)
        {
            printBoard(game.board);
            printCapturedPieces(game);
            Console.WriteLine("\nTurn: " + game.turn);
            Console.WriteLine("Waiting for player: " + game.currentPlayer);
        }

        public static void printCapturedPieces(ChessGame game)
        {
            Console.WriteLine();
            Console.WriteLine("Captured pieces: ");
            Console.Write("White: ");
            printSet(game.capturedPieces(Color.White));
            Console.Write("Yellow: ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            printSet(game.capturedPieces(Color.Yellow));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }

        public static void printSet(HashSet<Piece> set)
        {
            Console.Write("[");
            foreach(Piece x in set)
            {
                Console.Write(x + " ");
            }
            Console.Write("]\n");
        }

        public static void printBoard(Board board)
        {
            for (int i = 0; i < board.rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.columns; j++)
                {
                    printPiece(board.piece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void printBoard(Board board, bool[,] possibleMoves)
        {
            ConsoleColor initialBackground = Console.BackgroundColor;
            ConsoleColor modifiedBackground = ConsoleColor.DarkGray;
            for (int i = 0; i < board.rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.columns; j++)
                {
                    if (possibleMoves[i, j])
                    {
                        Console.BackgroundColor = modifiedBackground;
                    }
                    else
                    {
                        Console.BackgroundColor = initialBackground;
                    }
                    printPiece(board.piece(i, j));
                    Console.BackgroundColor = initialBackground;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = initialBackground;
        }

        public static ChessPosition readChessPosition()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int row = int.Parse(s[1] + "");

            return new ChessPosition(column, row);
        }

        public static void printPiece(Piece piece)
        {
            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (piece.color == Color.White)
                {
                    Console.Write(piece);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }
    }
}
