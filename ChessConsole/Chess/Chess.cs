using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chess
{
    public class Chess
    {
        //Forsyth–Edwards Notation
        public string fen { get; private set; }
        static DateTime dt = DateTime.Now;
        Board board;
        Moves moves;
        List<FigureMoving> allMoves;
        public Chess(string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")
        {
            this.fen = fen;
            board = new Board(fen);
            moves = new Moves(board);
            FindAllMoves();
        }
        Chess(Board board)
        {
            this.board = board;
            this.fen = board.fen;
            moves = new Moves(board);

        }
        public Chess Move(string move)
        {
            FigureMoving fm = new FigureMoving(move);
            if (!moves.CanMove(fm))
                return this;
            Board nextBoard = board.Move(fm);
            Chess nextChess = new Chess(nextBoard);
            return nextChess;
        }
        //Положение фигуры на доске
        public char GetFigureAt(int x, int y)
        {
            TimerFigure();
            Square square = new Square(x, y);
            Figure f = board.GetFigureAt(square);
            return f == Figure.none ? '.' : (char)f;
        }
        void TimerFigure()
        {
            if (board.moveColor == Color.white)
            {
                TimerCallback timeCB = new TimerCallback(PrintTime);
                Timer time = new Timer(timeCB, null, 0, 1000);
            }
            else if(board.moveColor==Color.black)
            {
                TimerCallback timeCB = new TimerCallback(PrintTime);
                Timer time = new Timer(timeCB, null, 0, 1000);
            }
        }
        static void PrintTime(object state)
        {
            DateTime dateTime = DateTime.Now;
            var timeNow = dateTime - dt;
            if (timeNow.Seconds > 300)
            {
                Environment.Exit(0);
            }
        }
        void FindAllMoves()
        {
            allMoves = new List<FigureMoving>();
            foreach (FigureOnSquare fs in board.YieldFigures())
                foreach (Square to in Square.YieldSquares())
                {
                    FigureMoving fm = new FigureMoving(fs, to);
                    if (moves.CanMove(fm))
                        if (!board.IsCheskAfterMove(fm))
                            allMoves.Add(fm);
                }
        }
        public List<string> GetAllMoves()
        {
            FindAllMoves();
            List<string> list = new List<string>();
            foreach (FigureMoving fm in allMoves)
                list.Add(fm.ToString());
            return list;
        }
        public bool IsCheck()
        {
            return board.IsCheck();
        }
    }
}
