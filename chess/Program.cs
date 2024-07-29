using board;
using chess;

namespace chess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8,8);

            board.putPiece(new Rook(board, Color.Yellow), new Position(0, 0));
            board.putPiece(new King(board, Color.Yellow), new Position(1, 3));
            board.putPiece(new Rook(board, Color.White), new Position(3, 5));
            board.putPiece(new Rook(board, Color.White), new Position(4, 5));
            Screen.printBoard(board);
        }
    }
}
