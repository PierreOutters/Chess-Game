using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Program
    {
        static char[,] CreateBoard()
        {
            char[,] board =
            {
                { 'R', 'k', 'B', 'Q', 'K', 'B', 'k', 'R'}, // y-axis is y, x-axis is x
                { 'p', 'p', 'p', 'p', 'p', 'p', 'p', 'p'},
                { '_', '_', '_', '_', '_', '_', '_', '_'},
                { '_', '_', '_', '_', '_', '_', '_', '_'},
                { '_', '_', '_', '_', '_', '_', '_', '_'},
                { '_', '_', '_', '_', '_', '_', '_', '_'},
                { 'p', 'p', 'p', 'p', 'p', 'p', 'p', 'p'},
                { 'R', 'k', 'B', 'Q', 'K', 'B', 'k', 'R'},
            };
            return board;
        }
        static Pieces[] SetupPieces(char[,] board)
        {
            Pieces[] pieces = new Pieces[32];
            int k = 0;
            for (int y = 0; y < 2; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (board[y, x] == 'R')
                    {
                        pieces[k] = new Rook(y, x, true);
                        k++;
                    }
                    else if (board[y, x] == 'k')
                    {
                        pieces[k] = new Knight(y, x, true);
                        k++;
                    }
                    else if (board[y, x] == 'B')
                    {
                        pieces[k] = new Bishop(y, x, true);
                        k++;
                    }
                    else if (board[y, x] == 'Q')
                    {
                        pieces[k] = new Queen(y, x, true);
                        k++;
                    }
                    else if (board[y, x] == 'K')
                    {
                        pieces[k] = new King(y, x, true);
                        k++;
                    }
                    else if (board[y, x] == 'p')
                    {
                        pieces[k] = new Pawn(y, x, true);
                        k++;
                    }
                }
            }
            for (int y = 6; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (board[y, x] == 'R')
                    {
                        pieces[k] = new Rook(y, x, false);
                        k++;
                    }
                    else if (board[y, x] == 'k')
                    {
                        pieces[k] = new Knight(y, x, false);
                        k++;
                    }
                    else if (board[y, x] == 'B')
                    {
                        pieces[k] = new Bishop(y, x, false);
                        k++;
                    }
                    else if (board[y, x] == 'Q')
                    {
                        pieces[k] = new Queen(y, x, false);
                        k++;
                    }
                    else if (board[y, x] == 'K')
                    {
                        pieces[k] = new King(y, x, false);
                        k++;
                    }
                    else if (board[y, x] == 'p')
                    {
                        pieces[k] = new Pawn(y, x, false);
                        k++;
                    }
                }
            }
            return pieces;
        }
        static void PrintBoard(char[,] board)
        {
            for (int y = 0; y < 8; y++)
            {
                Console.Write((8-y) + " |");
                for (int x = 0; x < 8; x++)
                {
                    if (board[y, x] == '_')
                    {
                        Console.Write("   ");
                    }
                    else
                    {
                        Console.Write(" " + board[y, x] + " ");
                    }
                    Console.Write("|");
                }
                Console.WriteLine("\n  ---------------------------------");
            }
            Console.WriteLine("    A   B   C   D   E   F   G   H\n");
        }
        static void RemoveDeadPieces(char[,] board, Pieces[] pieces)
        {
            for (int i = 0; i < pieces.Length; i++)
            {
                if (!pieces[i].IsAlive())
                {
                    board[pieces[i].ReturnCoords()[0], pieces[i].ReturnCoords()[1]] = '_';
                }
            }
        }
        static int[] ProcessMove(char xaxis, char yaxis)
        {
            return new int[] { 8-int.Parse(yaxis.ToString()), (int)xaxis - 65 };
        } 
        static char[] BackwardsProcessMove(int yaxis, int xaxis)
        {
            return new char[] { (char)(xaxis + 65), char.Parse((8-yaxis).ToString())};
        }
        static bool MakeMove(char[,] board, Pieces[] pieces)
        {
            Console.WriteLine("Please enter your move");
            string move = Console.ReadLine();
            int[] coords = ProcessMove(char.Parse(move[1].ToString().ToUpper()), move[2]);
            if (move[0] == 'p' || move[0] == 'R' || move[0] == 'k' || move[0] == 'B' || move[0] == 'K' || move[0] == 'Q')
            {
                return MakeWhiteMove(board, pieces, coords, move[0]);
            }
            return false;
        }
        static bool MakeWhiteMove(char[,] board, Pieces[] pieces, int[] move, char piece)
        {
            for (int i = 0; i < pieces.Length; i++)
            {
                if (pieces[i].IsAlive() && pieces[i].ReturnColour() == false && pieces[i].ReturnType() == piece)
                {
                    for (int j = 0; j < pieces[i].AvailablePlaces(board).Count; j++)
                    {

                        if (pieces[i].AvailablePlaces(board)[j][0] == move[0] && pieces[i].AvailablePlaces(board)[j][1] == move[1])
                        {
                            if (board[pieces[i].AvailablePlaces(board)[j][0], pieces[i].AvailablePlaces(board)[j][1]] != '_')
                            {
                                return false;
                            }
                            board[pieces[i].ReturnCoords()[0], pieces[i].ReturnCoords()[1]] = '_';
                            Console.WriteLine("Available Places j 0: " + pieces[i].AvailablePlaces(board)[j][0]);
                            board[pieces[i].AvailablePlaces(board)[j][0], pieces[i].AvailablePlaces(board)[j][1]] = piece;
                            Console.WriteLine("Available Places j 0: " + pieces[i].AvailablePlaces(board)[j][0]);
                            Console.WriteLine("Coords 0: " + pieces[i].ReturnCoords()[0]);
                            Console.WriteLine("Available Places j 1: " + pieces[i].AvailablePlaces(board)[j][1]);
                            Console.WriteLine("Coords 1: " + pieces[i].ReturnCoords()[1]);
                            Console.ReadKey();
                            pieces[i].ChangeCoord(pieces[i].AvailablePlaces(board)[j][0], pieces[i].AvailablePlaces(board)[j][1]);
                            pieces[i].Moved();
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        static void Main(string[] args)
        {
            char[,] board = CreateBoard();
            Pieces[] pieces = SetupPieces(board);
            bool success = false;
            PrintBoard(board);
            while (true)
            {
                success = MakeMove(board, pieces);
                while (!success)
                {
                    Console.WriteLine("Invalid move try again");
                    success = MakeMove(board, pieces);
                }
                PrintBoard(board);
            }
        }
    }
    abstract class Pieces
    {
        protected int ycoord, xcoord;
        protected bool alive = true;
        protected bool colour; //false for white, true for black
        public Pieces(int inycoord, int inxcoord, bool incolour)
        {
            ycoord = inycoord; xcoord = inxcoord; colour = incolour;
        }
        public bool IsAlive()
        {
            return alive;
        }
        public bool ReturnColour()
        {
            return colour;
        }
        public int[] ReturnCoords()
        {
            int[] coords = { ycoord, xcoord };
            return coords;
        }
        public void ChangeCoord(int y, int x)
        {
            ycoord = y; xcoord = x;
        }
        public abstract List<int[]> AvailablePlaces(char[,] board);
        public abstract char ReturnType();
        public virtual void Moved()
        {
        }
    }
    class Pawn : Pieces
    {
        public Pawn(int inycoord, int inxcoord, bool incolour) : base (inycoord, inxcoord, incolour)
        {

        }
        public override char ReturnType()
        {
            return 'p';
        }
        public bool hasmoved = false;
        public override void Moved()
        {
            hasmoved = true;
        }
        public override List<int[]> AvailablePlaces(char[,] board)
        {
            List<int[]> list = new List<int[]>();
            if (colour == false)
            {
                list.Add(new int[] { ycoord - 1, xcoord });
                if (!hasmoved)
                {
                    list.Add (new int[] { ycoord - 2, xcoord });
                }
            }
            else
            {
                list.Add(new int[] { ycoord + 1, xcoord });
                if (!hasmoved)
                {
                    list.Add(new int[] { ycoord + 2, xcoord });
                }
            }
            return list;
        }
    }
    class Rook : Pieces
    {
        public Rook(int inycoord, int inxcoord, bool incolour) : base(inycoord, inxcoord, incolour)
        {

        }
        public override char ReturnType()
        {
            return 'R';
        }
        public override List<int[]> AvailablePlaces(char[,] board)
        {
            List<int[]> list = new List<int[]>();
            for (int y = ycoord+1; y < 8; y++)
            {
                if (board[y,xcoord] != '_')
                {
                    break;
                }
                list.Add(new int[] { y, xcoord });
            }
            for (int y = ycoord - 1; y > 0; y--)
            {
                if (board[y, xcoord] != '_')
                {
                    break;
                }
                list.Add(new int[] { y, xcoord });
            }
            for (int x = xcoord + 1; x < 8; x++)
            {
                if (board[ycoord, x] != '_')
                {
                    break;
                }
                list.Add(new int[] { ycoord, x });
            }
            for (int x = xcoord - 1; x > 0; x--)
            {
                if (board[ycoord, x] != '_')
                {
                    break;
                }
                list.Add(new int[] { ycoord, x });
            }
            return list;
        }
    }
    class Knight : Pieces
    {
        public Knight(int inycoord, int inxcoord, bool incolour) : base(inycoord, inxcoord, incolour)
        {

        }
        public override char ReturnType()
        {
            return 'k';
        }
        public override List<int[]> AvailablePlaces(char[,] board)
        {
            List<int[]> list = new List<int[]>();
            return list;
        }
    }
    class Bishop : Pieces
    {
        public Bishop(int inycoord, int inxcoord, bool incolour) : base(inycoord, inxcoord, incolour)
        {

        }
        public override char ReturnType()
        {
            return 'B';
        }
        public override List<int[]> AvailablePlaces(char[,] board)
        {
            List<int[]> list = new List<int[]>();
            return list;
        }
    }
    class King : Pieces
    {
        public King(int inycoord, int inxcoord, bool incolour) : base(inycoord, inxcoord, incolour)
        {

        }
        public override char ReturnType()
        {
            return 'K';
        }
        public override List<int[]> AvailablePlaces(char[,] board)
        {
            List<int[]> list = new List<int[]>();
            return list;
        }
    }
    class Queen : Pieces
    {
        public Queen(int inycoord, int inxcoord, bool incolour) : base(inycoord, inxcoord, incolour)
        {

        }
        public override char ReturnType()
        {
            return 'Q';
        }
        public override List<int[]> AvailablePlaces(char[,] board)
        {
            List<int[]> list = new List<int[]>();
            return list;
        }
    }
}