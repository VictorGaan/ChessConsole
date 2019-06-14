using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Chess;


namespace ChessApp
{
    class Program
    {

        private static void Main()
        {
            Console.WriteLine("\t\t\t\t\t\t\tConsole Chess");
            AIChess();
        }
        private static void AIChess()
        {

            Random random = new Random();
            Chess.Chess chess = new Chess.Chess("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
            List<string> list;
            while (true)
            {
                list = chess.GetAllMoves();
                Console.WriteLine(chess.fen);
                Print(ChessToAscii(chess));
                Console.WriteLine(chess.IsCheck() ? "Шах" : "");
                foreach (string moves in chess.GetAllMoves())
                    Console.Write(moves + "\n");
                Console.WriteLine();
                Console.Write("< ");
                string move = Console.ReadLine();
                if (move == "q") break;
                try
                { if (move == "") move = list[random.Next(list.Count)]; }
                catch (Exception) { }
                chess = chess.Move(move);
            }
        }

        static string ChessToAscii(Chess.Chess chess)
        {
            Console.Clear();
            string text = "    a b c d e f g h\n";
            text += "  +-----------------+\n";
            for (int y = 7; y >= 0; y--)
            {
                text += y + 1;
                text += " | ";
                for (int x = 0; x < 8; x++)
                {
                    text += chess.GetFigureAt(x, y) + " ";
                }
                text += "|\n";
            }
            text += "  +-----------------+\n";
            text += "    a b c d e f g h\n";
            return text;

        }
        private static void Print(string text)
        {
            ConsoleColor color = Console.ForegroundColor;
            foreach (char x in text)
            {
                if (x >= 'a' && x <= 'z')
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                else if (x >= 'A' && x <= 'Z')
                    Console.ForegroundColor = ConsoleColor.White;
                else
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write(x);
            }
            Console.ForegroundColor = color;
        }

    }
}
