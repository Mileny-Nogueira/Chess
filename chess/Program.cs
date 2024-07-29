using board;
using chess;

namespace chess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Board board = new Board(8, 8);

                board.putPiece(new Rook(board, Color.Yellow), new Position(0, 0));
                board.putPiece(new King(board, Color.Yellow), new Position(0, 9));
                board.putPiece(new Rook(board, Color.Yellow), new Position(1, 3));
                board.putPiece(new King(board, Color.Yellow), new Position(2, 4));

                Screen.printBoard(board);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            


        }
    }
}
