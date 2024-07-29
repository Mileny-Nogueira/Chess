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
                ChessGame game = new ChessGame();
                while (!game.finished)
                {
                    Console.Clear();
                    Screen.printBoard(game.board);
                    Console.Write("\nOrigin: ");
                    Position origin = Screen.readChessPosition().toPosition();
                    Console.Write("Target: ");
                    Position target = Screen.readChessPosition().toPosition();
                    game.executeMove(origin, target);
                }
                
            }
            catch (BoardException e)
            { 
                Console.WriteLine(e.Message);
            }
        }
    }
}
