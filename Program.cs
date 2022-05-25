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
        static bool EnterMove(char[,] board, Pieces[] pieces)
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
                    for (int j = 0; j < pieces[i].AvailablePlaces(board, pieces).Count; j++)
                    {
                        if (pieces[i].AvailablePlaces(board, pieces)[j][0] == move[0] && pieces[i].AvailablePlaces(board, pieces)[j][1] == move[1])
                        {
                            /*
                            if (board[pieces[i].AvailablePlaces(board, pieces)[j][0], pieces[i].AvailablePlaces(board, pieces)[j][1]] != '_')
                            {
                                return false;
                            }
                            */
                            board[pieces[i].ReturnCoords()[0], pieces[i].ReturnCoords()[1]] = '_';
                            int y = pieces[i].AvailablePlaces(board, pieces)[j][0], x = pieces[i].AvailablePlaces(board, pieces)[j][1];
                            board[pieces[i].AvailablePlaces(board, pieces)[j][0], pieces[i].AvailablePlaces(board, pieces)[j][1]] = piece;
                            pieces[i].ChangeToCoord(y, x);
                            pieces[i].Moved();
                            foreach (Pieces p in pieces)
                            {
                                if (p.ReturnCoords() == pieces[i].ReturnCoords())
                                {
                                    p.Kill();
                                }
                            }
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
                success = EnterMove(board, pieces);
                while (!success)
                {
                    Console.WriteLine("Invalid move try again");
                    success = EnterMove(board, pieces);
                }
                Console.Clear();
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
        public void Kill()
        {
            alive = false;
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
        public void ChangeToCoord(int y, int x)
        {
            ycoord = y; xcoord = x;
        }
        public abstract List<int[]> AvailablePlaces(char[,] board, Pieces[] pieces);
        public abstract char ReturnType();
        public virtual void Moved()
        {
        }
    }
    class Pawn : Pieces
    {
        private bool hasmoved = false;
        public Pawn(int inycoord, int inxcoord, bool incolour) : base (inycoord, inxcoord, incolour)
        {

        }
        public override char ReturnType()
        {
            return 'p';
        }
        public override void Moved()
        {
            hasmoved = true;
        }
        public override List<int[]> AvailablePlaces(char[,] board, Pieces[] pieces)
        {
            List<int[]> list = new List<int[]>();
            if (colour == false && ycoord - 1 > 0)
            {
                if (board[ycoord - 1, xcoord] == '_')
                {
                    list.Add(new int[] { ycoord - 1, xcoord });
                    if (!hasmoved)
                    {
                        list.Add(new int[] { ycoord - 2, xcoord });
                    }
                }
            }
            if (colour == true && ycoord + 1 < 7)
            {
                if (board[ycoord + 1, xcoord] == '_')
                {
                    list.Add(new int[] { ycoord + 1, xcoord });
                    if (!hasmoved)
                    {
                        list.Add(new int[] { ycoord + 2, xcoord });
                    }
                }
            }
            foreach (Pieces p in pieces)
            {
                if (p.ReturnColour() != colour && colour == true)
                {
                    if (p.ReturnCoords()[0] == ycoord + 1 && p.ReturnCoords()[1] == xcoord + 1)
                    {
                        list.Add(new int[] { ycoord + 1, xcoord + 1 });
                    }
                    else if (p.ReturnCoords()[0] == ycoord + 1 && p.ReturnCoords()[1] == xcoord - 1)
                    {
                        list.Add(new int[] { ycoord + 1, xcoord - 1 });
                    }
                }
                else if (p.ReturnColour() != colour && colour == false)
                {
                    if (p.ReturnCoords()[0] == ycoord - 1 && p.ReturnCoords()[1] == xcoord + 1)
                    {
                        list.Add(new int[] { ycoord - 1, xcoord + 1 });
                    }
                    else if (p.ReturnCoords()[0] == ycoord - 1 && p.ReturnCoords()[1] == xcoord - 1)
                    {
                        list.Add(new int[] { ycoord - 1, xcoord - 1 });
                    }
                }
            }
            return list;
        }
    }
    class Rook : Pieces
    {
        private bool hasmoved = false;
        public Rook(int inycoord, int inxcoord, bool incolour) : base(inycoord, inxcoord, incolour)
        {

        }
        public override void Moved()
        {
            hasmoved = true;
        }
        public override char ReturnType()
        {
            return 'R';
        }
        public override List<int[]> AvailablePlaces(char[,] board, Pieces[] pieces)
        {
            List<int[]> list = new List<int[]>();
            for (int y = ycoord+1; y < 8; y++)
            {
                if (board[y,xcoord] != '_')
                {
                    foreach (Pieces p in pieces)
                    {
                        if (p.ReturnCoords()[0] == y && p.ReturnCoords()[1] == xcoord && colour != p.ReturnColour())
                        {
                            list.Add(new int[] { y, xcoord });
                        }
                    }
                    break;
                }
                list.Add(new int[] { y, xcoord });
            }
            for (int y = ycoord - 1; y >= 0; y--)
            {
                if (board[y, xcoord] != '_')
                {
                    foreach (Pieces p in pieces)
                    {
                        if (p.ReturnCoords()[0] == y && p.ReturnCoords()[1] == xcoord && colour != p.ReturnColour())
                        {
                            list.Add(new int[] { y, xcoord });
                        }
                    }
                    break;
                }
                list.Add(new int[] { y, xcoord });
            }
            for (int x = xcoord + 1; x < 8; x++)
            {
                if (board[ycoord, x] != '_')
                {
                    foreach (Pieces p in pieces)
                    {
                        if (p.ReturnCoords()[0] == ycoord && p.ReturnCoords()[1] == x && colour != p.ReturnColour())
                        {
                            list.Add(new int[] { ycoord, x });
                        }
                    }
                    break;
                }
                list.Add(new int[] { ycoord, x });
            }
            for (int x = xcoord - 1; x >= 0; x--)
            {
                if (board[ycoord, x] != '_')
                {
                    foreach (Pieces p in pieces)
                    {
                        if (p.ReturnCoords()[0] == ycoord && p.ReturnCoords()[1] == x && colour != p.ReturnColour())
                        {
                            list.Add(new int[] { ycoord, x });
                        }
                    }
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
        public override List<int[]> AvailablePlaces(char[,] board, Pieces[] pieces)
        {
            List<int[]> list = new List<int[]>();
            if (board[ycoord - 2, xcoord - 1] == '_')
            {
                list.Add(new int[] { ycoord - 2, xcoord - 1 });
            }
            else
            {
                foreach (Pieces p in pieces)
                {
                    if (p.ReturnCoords()[0] == ycoord - 2 && p.ReturnCoords()[1] == xcoord - 1 && colour != p.ReturnColour())
                    {
                        list.Add(new int[] { ycoord - 2, xcoord - 1 });
                    }
                }
            }
            if (board[ycoord - 2, xcoord + 1] == '_')
            {
                list.Add(new int[] { ycoord - 2, xcoord + 1 });
            }
            else
            {
                foreach (Pieces p in pieces)
                {
                    if (p.ReturnCoords()[0] == ycoord - 2 && p.ReturnCoords()[1] == xcoord + 1 && colour != p.ReturnColour())
                    {
                        list.Add(new int[] { ycoord - 2, xcoord + 1 });
                    }
                }
            }
            if (board[ycoord - 1, xcoord - 2] == '_')
            {
                list.Add(new int[] { ycoord - 1, xcoord - 2 });
            }
            else
            {
                foreach (Pieces p in pieces)
                {
                    if (p.ReturnCoords()[0] == ycoord - 1 && p.ReturnCoords()[1] == xcoord - 2 && colour != p.ReturnColour())
                    {
                        list.Add(new int[] { ycoord - 1, xcoord - 2 });
                    }
                }
            }
            if (board[ycoord - 1, xcoord + 2] == '_')
            {
                list.Add(new int[] { ycoord - 1, xcoord + 2 });
            }
            else
            {
                foreach (Pieces p in pieces)
                {
                    if (p.ReturnCoords()[0] == ycoord - 1 && p.ReturnCoords()[1] == xcoord + 2 && colour != p.ReturnColour())
                    {
                        list.Add(new int[] { ycoord - 1, xcoord + 2 });
                    }
                }
            }
            if (board[ycoord + 2, xcoord - 1] == '_')
            {
                list.Add(new int[] { ycoord + 2, xcoord - 1 });
            }
            else
            {
                foreach (Pieces p in pieces)
                {
                    if (p.ReturnCoords()[0] == ycoord + 2 && p.ReturnCoords()[1] == xcoord - 1 && colour != p.ReturnColour())
                    {
                        list.Add(new int[] { ycoord + 2, xcoord - 1 });
                    }
                }
            }
            if (board[ycoord + 2, xcoord + 1] == '_')
            {
                list.Add(new int[] { ycoord + 2, xcoord + 1 });
            }
            else
            {
                foreach (Pieces p in pieces)
                {
                    if (p.ReturnCoords()[0] == ycoord + 2 && p.ReturnCoords()[1] == xcoord + 1 && colour != p.ReturnColour())
                    {
                        list.Add(new int[] { ycoord + 2, xcoord + 1 });
                    }
                }
            }
            if (board[ycoord + 1, xcoord - 2] == '_')
            {
                list.Add(new int[] { ycoord + 1, xcoord - 2 });
            }
            else
            {
                foreach (Pieces p in pieces)
                {
                    if (p.ReturnCoords()[0] == ycoord + 1 && p.ReturnCoords()[1] == xcoord - 2 && colour != p.ReturnColour())
                    {
                        list.Add(new int[] { ycoord + 1, xcoord - 2 });
                    }
                }
            }
            if (board[ycoord + 1, xcoord + 2] == '_')
            {
                list.Add(new int[] { ycoord + 1, xcoord + 2 });
            }
            else
            {
                foreach (Pieces p in pieces)
                {
                    if (p.ReturnCoords()[0] == ycoord + 1 && p.ReturnCoords()[1] == xcoord + 2 && colour != p.ReturnColour())
                    {
                        list.Add(new int[] { ycoord + 1, xcoord + 2 });
                    }
                }
            }
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
        public override List<int[]> AvailablePlaces(char[,] board, Pieces[] pieces)
        {
            List<int[]> list = new List<int[]>();
            int y = ycoord + 1;
            for (int x = xcoord+1; x < 8; x++)
            {
                if (y > 7)
                {
                    break;
                }
                if (board[y, x] != '_')
                {
                    break;
                }
                list.Add(new int[] { y, x });
                y++;
            }
            y = ycoord - 1;
            for (int x = xcoord + 1; x < 8; x++)
            {
                if (y < 0)
                {
                    break;
                }
                if (board[y, x] != '_')
                {
                    break;
                }
                list.Add(new int[] { y, x });
                y--;
            }
            y = ycoord + 1;
            for (int x = xcoord - 1; x >= 0; x--)
            {
                if (y > 7)
                {
                    break;
                }
                if (board[y, x] != '_')
                {
                    break;
                }
                list.Add(new int[] { y, x });
                y++;
            }
            y = ycoord - 1;
            for (int x = xcoord - 1; x >= 0; x--)
            {
                if (y < 0)
                {
                    break;
                }
                if (board[y, x] != '_')
                {
                    break;
                }
                list.Add(new int[] { y, x });
                y--;
            }
            return list;
        }
    }
    class King : Pieces
    {
        private bool hasmoved = false;
        public King(int inycoord, int inxcoord, bool incolour) : base(inycoord, inxcoord, incolour)
        {

        }
        public override void Moved()
        {
            hasmoved = true;
        }
        public override char ReturnType()
        {
            return 'K';
        }
        public override List<int[]> AvailablePlaces(char[,] board, Pieces[] pieces)
        {
            List<int[]> list = new List<int[]>();
            list.Add(new int[] { ycoord + 1, xcoord });
            list.Add(new int[] { ycoord + 1, xcoord + 1 });
            list.Add(new int[] { ycoord, xcoord + 1 });
            list.Add(new int[] { ycoord - 1, xcoord + 1 });
            list.Add(new int[] { ycoord - 1, xcoord });
            list.Add(new int[] { ycoord - 1, xcoord - 1 });
            list.Add(new int[] { ycoord, xcoord - 1 });
            list.Add(new int[] { ycoord - 1, xcoord - 1 });
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
        public override List<int[]> AvailablePlaces(char[,] board, Pieces[] pieces)
        {
            List<int[]> list = new List<int[]>();
            for (int i = ycoord + 1; i < 8; i++)
            {
                if (board[i, xcoord] != '_')
                {
                    break;
                }
                list.Add(new int[] { i, xcoord });
            }
            for (int i = ycoord - 1; i >= 0; i--)
            {
                if (board[i, xcoord] != '_')
                {
                    break;
                }
                list.Add(new int[] { i, xcoord });
            }
            for (int x = xcoord + 1; x < 8; x++)
            {
                if (board[ycoord, x] != '_')
                {
                    break;
                }
                list.Add(new int[] { ycoord, x });
            }
            for (int x = xcoord - 1; x >= 0; x--)
            {
                if (board[ycoord, x] != '_')
                {
                    break;
                }
                list.Add(new int[] { ycoord, x });
            }
            int y = ycoord + 1;
            for (int x = xcoord + 1; x < 8; x++)
            {
                if (y > 7)
                {
                    break;
                }
                if (board[y, x] != '_')
                {
                    break;
                }
                list.Add(new int[] { y, x });
                y++;
            }
            y = ycoord - 1;
            for (int x = xcoord + 1; x < 8; x++)
            {
                if (y < 0)
                {
                    break;
                }
                if (board[y, x] != '_')
                {
                    break;
                }
                list.Add(new int[] { y, x });
                y--;
            }
            y = ycoord + 1;
            for (int x = xcoord - 1; x >= 0; x--)
            {
                if (y > 7)
                {
                    break;
                }
                if (board[y, x] != '_')
                {
                    break;
                }
                list.Add(new int[] { y, x });
                y++;
            }
            y = ycoord - 1;
            for (int x = xcoord - 1; x >= 0; x--)
            {
                if (y < 0)
                {
                    break;
                }
                if (board[y, x] != '_')
                {
                    break;
                }
                list.Add(new int[] { y, x });
                y--;
            }
            return list;
        }
    }
}
