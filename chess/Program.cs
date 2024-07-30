
using board;

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
                    try
                    {
                        Console.Clear();
                        Screen.printBoard(game.board);
                        Console.WriteLine("\nTurn: " + game.turn);
                        Console.WriteLine("Waiting for player: " + game.currentPlayer);
                        Console.Write("\nOrigin: ");
                        Position origin = Screen.readChessPosition().toPosition();
                        game.validateOriginPosition(origin);

                        bool[,] possibleMoves = game.board.piece(origin).possibleMoves();

                        Console.Clear();
                        Screen.printBoard(game.board, possibleMoves);

                        Console.WriteLine();
                        Console.Write("Target: ");
                        Position target = Screen.readChessPosition().toPosition();
                        game.validateTargetPosition(origin, target);
                        game.makeMove(origin, target);
                    }
                    catch (BoardException e)
                    {
                        Console.Write(e.Message);
                        System.Threading.Thread.Sleep(2000);
                    }
                    
                }
                
            }
            catch (BoardException e)
            { 
                Console.WriteLine(e.Message);
            }
        }
    }
}
