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
                { 'R', 'k', 'B', 'Q', 'K', 'B', 'k', 'R'}, // These are the char that are needed to be typed
                { 'p', 'p', 'p', 'p', 'p', 'p', 'p', 'p'}, // To do a movement command when playing the game
                { '_', '_', '_', '_', '_', '_', '_', '_'},
                { '_', '_', '_', '_', '_', '_', '_', '_'},
                { '_', '_', '_', '_', '_', '_', '_', '_'},
                { '_', '_', '_', '_', '_', '_', '_', '_'},
                { 'p', 'p', 'p', 'p', 'p', 'p', 'p', 'p'},
                { 'R', 'k', 'B', 'Q', 'K', 'B', 'k', 'R'},
            };
            return board;
        } // Finished
        static List<Pieces> SetupPieces(char[,] board)
        {
            List<Pieces> pieces = new List<Pieces>();
            int k = 0;
            for (int y = 0; y < 2; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (board[y, x] == 'R')
                    {
                        pieces.Add(new Rook(y, x, true));
                    }
                    else if (board[y, x] == 'k')
                    {
                        pieces.Add(new Knight(y, x, true));
                    }
                    else if (board[y, x] == 'B')
                    {
                        pieces.Add(new Bishop(y, x, true));
                    }
                    else if (board[y, x] == 'Q')
                    {
                        pieces.Add(new Queen(y, x, true));
                    }
                    else if (board[y, x] == 'K')
                    {
                        pieces.Add(new King(y, x, true));
                    }
                    else if (board[y, x] == 'p')
                    {
                        pieces.Add(new Pawn(y, x, true));
                    }
                }
            }
            for (int y = 6; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (board[y, x] == 'R')
                    {
                        pieces.Add(new Rook(y, x, false));
                        k++;
                    }
                    else if (board[y, x] == 'k')
                    {
                        pieces.Add(new Knight(y, x, false));
                    }
                    else if (board[y, x] == 'B')
                    {
                        pieces.Add(new Bishop(y, x, false));
                    }
                    else if (board[y, x] == 'Q')
                    {
                        pieces.Add(new Queen(y, x, false));
                    }
                    else if (board[y, x] == 'K')
                    {
                        pieces.Add(new King(y, x, false));
                    }
                    else if (board[y, x] == 'p')
                    {
                        pieces.Add(new Pawn(y, x, false));
                    }
                }
            }
            return pieces;
        } // Finished
        static void PrintBoard(char[,] board, List<Pieces> pieces)
        {
            for (int y = 0; y < 8; y++)
            {
                Console.Write((8 - y) + " |");
                for (int x = 0; x < 8; x++)
                {
                    if (board[y, x] == '_')
                    {
                        Console.Write("   ");
                    }
                    else
                    {
                        foreach (Pieces p in pieces)
                        {
                            if (p.ReturnCoords()[0] == y && p.ReturnCoords()[1] == x && p.ReturnColour() == true)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                            }
                        }
                        Console.Write(" " + board[y, x] + " ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.Write("|");
                }
                Console.WriteLine("\n  ---------------------------------");
            }
            Console.WriteLine("    A   B   C   D   E   F   G   H\n");
        } // Finished
        static void PrintFlippedBoard(char[,] board, List<Pieces> pieces)
        {
            for (int y = 7; y >= 0; y--)
            {
                Console.Write((8 - y) + " |");
                for (int x = 7; x >= 0; x--)
                {
                    if (board[y, x] == '_')
                    {
                        Console.Write("   ");
                    }
                    else
                    {
                        foreach (Pieces p in pieces)
                        {
                            if (p.ReturnCoords()[0] == y && p.ReturnCoords()[1] == x && p.ReturnColour() == true)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                            }
                        }
                        Console.Write(" " + board[y, x] + " ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.Write("|");
                }
                Console.WriteLine("\n  ---------------------------------");
            }
            Console.WriteLine("    H   G   F   E   D   C   B   A\n");
        } // Finished
        static int[] ProcessMove(char xaxis, char yaxis)
        {
            return new int[] { 8 - int.Parse(yaxis.ToString()), (int)xaxis - 65 };
        }  // Finished
        /*static char[] BackwardsProcessMove(int yaxis, int xaxis)
        {
            return new char[] { (char)(xaxis + 65), char.Parse((8-yaxis).ToString())};
        } */ // May help me
        static bool EnterMove(char[,] board, List<Pieces> pieces, bool colour)
        {
            if (colour)
            {
                Console.WriteLine("Black to move");
            }
            else
            {
                Console.WriteLine("White to move");
            }
            Console.WriteLine("Please enter your move");
            string move = Console.ReadLine();
            if (move.Length != 3)
            {
                return false;
            }
            else if (!int.TryParse(move[2].ToString(), out int x))
            {
                return false;
            }
            int[] coords = ProcessMove(char.Parse(move[1].ToString().ToUpper()), move[2]);
            if (move[0] == 'p' || move[0] == 'R' || move[0] == 'k' || move[0] == 'B' || move[0] == 'K' || move[0] == 'Q')
            {
                return MakeMove(board, pieces, coords, move[0], colour);
            }
            return false;
        } // Finished
        static bool MakeMove(char[,] board, List<Pieces> pieces, int[] move, char piece, bool colour)
        {
            for (int i = 0; i < pieces.Count; i++)
            {
                if (pieces[i].IsAlive() && pieces[i].ReturnColour() == colour && pieces[i].ReturnType() == piece)
                {
                    for (int j = 0; j < pieces[i].AvailablePlaces(board, pieces).Count; j++)
                    {
                        if (pieces[i].AvailablePlaces(board, pieces)[j][0] == move[0] && pieces[i].AvailablePlaces(board, pieces)[j][1] == move[1])
                        {
                            int y = pieces[i].AvailablePlaces(board, pieces)[j][0], x = pieces[i].AvailablePlaces(board, pieces)[j][1];
                            if (pieces[i].AvailablePlaces(board, pieces)[j].Length == 3)
                            {
                                int l = pieces[i].AvailablePlaces(board, pieces)[j][2];
                                for (int k = 0; k < pieces.Count; k++)
                                {
                                    if (pieces[k].ReturnCoords()[0] == l && pieces[k].ReturnCoords()[1] == x)
                                    {
                                        board[pieces[k].ReturnCoords()[0], pieces[k].ReturnCoords()[1]] = '_';
                                        if (k < i)
                                        {
                                            i--;
                                        }
                                        pieces.RemoveAt(k);
                                    }
                                }
                            }
                            board[pieces[i].ReturnCoords()[0], pieces[i].ReturnCoords()[1]] = '_';
                            board[y, x] = piece;
                            if (pieces[i].ReturnType() == 'K' && pieces[i].ReturnCoords()[1] - x == -2)
                            {
                                foreach (Pieces p in pieces)
                                {
                                    if (p.ReturnCoords()[1] == 7 && p.ReturnCoords()[0] == pieces[i].ReturnCoords()[0])
                                    {
                                        board[p.ReturnCoords()[0], p.ReturnCoords()[1]] = '_';
                                        board[p.ReturnCoords()[0], 5] = p.ReturnType();
                                        pieces[i].ChangeToCoord(p.ReturnCoords()[0], 5);
                                        p.Moved();
                                    }
                                }
                            }
                            if (pieces[i].ReturnType() == 'K' && pieces[i].ReturnCoords()[1] - x == 2)
                            {
                                foreach (Pieces p in pieces)
                                {
                                    if (p.ReturnCoords()[1] == 0 && p.ReturnCoords()[0] == pieces[i].ReturnCoords()[0])
                                    {
                                        board[p.ReturnCoords()[0], p.ReturnCoords()[1]] = '_';
                                        board[p.ReturnCoords()[0], 3] = p.ReturnType();
                                        pieces[i].ChangeToCoord(p.ReturnCoords()[0], 3);
                                        p.Moved();
                                    }
                                }
                            }
                            if (pieces[i].ReturnType() == 'p' && !pieces[i].ReturnMoved() && y == 3 || pieces[i].ReturnType() == 'p' && !pieces[i].ReturnMoved() && y == 4)
                            {
                                pieces[i].DoubleMoved();
                            }
                            else
                            {
                                foreach (Pieces p in pieces)
                                {
                                    pieces[i].NoDoubleMoved();
                                }
                            }
                            pieces[i].ChangeToCoord(y, x);
                            pieces[i].Moved();
                            pieces[i].Promote(pieces, board);
                            for (int k = 0; k < pieces.Count; k++)
                            {
                                if (pieces[k].ReturnCoords()[0] == pieces[i].ReturnCoords()[0] && pieces[k].ReturnCoords()[1] == pieces[i].ReturnCoords()[1] && pieces[k].ReturnColour() != pieces[i].ReturnColour())
                                {
                                    pieces.RemoveAt(k);
                                    i--; k--;
                                }
                            }
                            return true;
                        }
                    }
                }
            }
            return false;
        } // Finished
        static bool CheckWin(List<Pieces> pieces, bool colour)
        {
            foreach (Pieces p in pieces)
            {
                if (p.ReturnColour() != colour && p.ReturnType() == 'K')
                {
                    return false;
                }
            }
            return true;
        } // Finished
        static void EndGame(bool colour)
        {
            if (colour)
            {
                Console.WriteLine("Black has won the game");
                Console.ReadKey(true); Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("White has won the game");
                Console.ReadKey(true); Environment.Exit(0);
            }
        } // Finished
        static void Main(string[] args)
        {
            char[,] board = CreateBoard();
            List<Pieces> pieces = SetupPieces(board);
            bool success;
            PrintBoard(board, pieces);
            while (true)
            {
                success = EnterMove(board, pieces, false);
                while (!success)
                {
                    Console.Clear();
                    PrintBoard(board, pieces);
                    Console.WriteLine("Invalid move try again");
                    success = EnterMove(board, pieces, false);
                }
                Console.Clear();
                PrintBoard(board, pieces);
                Console.ReadKey(true);
                if (CheckWin(pieces, false))
                {
                    EndGame(false);
                }
                Console.Clear();
                PrintFlippedBoard(board, pieces);
                success = EnterMove(board, pieces, true);
                while (!success)
                {
                    Console.Clear();
                    PrintFlippedBoard(board, pieces);
                    Console.WriteLine("Invalid move try again");
                    success = EnterMove(board, pieces, true);
                }
                Console.Clear();
                PrintFlippedBoard(board, pieces);
                Console.ReadKey(true);
                if (CheckWin(pieces, true))
                {
                    EndGame(true);
                }
                Console.Clear();
                PrintBoard(board, pieces);
            }
        } // Main
    }
    abstract class Pieces // Finished
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
        public abstract List<int[]> AvailablePlaces(char[,] board, List<Pieces> pieces);
        public abstract char ReturnType();
        public virtual void Moved()
        {
        }
        public virtual void Promote(List<Pieces> pieces, char[,] board)
        {
        }
        public virtual bool ReturnMoved()
        {
            return true;
        }
        public virtual void DoubleMoved()
        {
        }
        public virtual void NoDoubleMoved()
        {
        }
        public virtual bool ReturnDoubleMoved()
        {
            return false;
        }
    }
    class Pawn : Pieces // Finished
    {
        private bool hasmoved;
        private bool doublemoved;
        public Pawn(int inycoord, int inxcoord, bool incolour) : base(inycoord, inxcoord, incolour)
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
        public override bool ReturnMoved()
        {
            return hasmoved;
        }
        public override void DoubleMoved()
        {
            doublemoved = true;
        }
        public override void NoDoubleMoved()
        {
            doublemoved = false;
        }
        public override bool ReturnDoubleMoved()
        {
            return doublemoved;
        }
        public override void Promote(List<Pieces> pieces, char[,] board)
        {
            if (ycoord == 7 || ycoord == 0)
            {
                Console.WriteLine("Your pawn can now promote");
                Console.ReadKey(true);
                Console.WriteLine("Enter (Q) for a Queen \nEnter (R) for a Rook \nEnter (B) for a Bishop \nEnter (K) for a Knight");
                string promotion = Console.ReadLine().ToUpper();
                while (promotion != "Q" && promotion != "R" && promotion != "B" && promotion != "K")
                {
                    Console.WriteLine("Invalid input");
                    Console.WriteLine("Enter (Q) for a Queen \nEnter (R) for a Rook \nEnter (B) for a Bishop \nEnter (K) for a Knight");
                    promotion = Console.ReadLine().ToLower();
                }
                for (int i = 0; i < pieces.Count; i++)
                {
                    if (pieces[i].ReturnCoords() == ReturnCoords())
                    {
                        pieces.RemoveAt(i);
                    }
                }
                if (promotion == "R")
                {
                    pieces.Add(new Rook(ycoord, xcoord, colour));
                    Console.WriteLine("A new Rook has replaced the pawn");
                    board[ycoord, xcoord] = 'R'; Console.ReadKey(true);
                }
                else if (promotion == "B")
                {
                    pieces.Add(new Bishop(ycoord, xcoord, colour));
                    Console.WriteLine("A new Bishop has replaced the pawn");
                    board[ycoord, xcoord] = 'B'; Console.ReadKey(true);
                }
                else if (promotion == "K")
                {
                    pieces.Add(new Knight(ycoord, xcoord, colour));
                    Console.WriteLine("A new Knight has replaced the pawn");
                    board[ycoord, xcoord] = 'k'; Console.ReadKey(true);
                }
                else if (promotion == "Q")
                {
                    pieces.Add(new Queen(ycoord, xcoord, colour));
                    Console.WriteLine("A new Queen has replaced the pawn");
                    board[ycoord, xcoord] = 'Q'; Console.ReadKey(true);
                }
            }
        }
        public override List<int[]> AvailablePlaces(char[,] board, List<Pieces> pieces)
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
            foreach (Pieces p in pieces)
            {
                if (p.ReturnColour() != colour && colour == true)
                {
                    if (p.ReturnCoords()[0] == ycoord && p.ReturnCoords()[1] == xcoord + 1 && p.ReturnDoubleMoved())
                    {
                        list.Add(new int[] { ycoord + 1, xcoord + 1, ycoord });
                    }
                    else if (p.ReturnCoords()[0] == ycoord && p.ReturnCoords()[1] == xcoord - 1 && p.ReturnDoubleMoved())
                    {
                        list.Add(new int[] { ycoord + 1, xcoord - 1, ycoord });
                    }
                }
                else if (p.ReturnColour() != colour && colour == false)
                {
                    if (p.ReturnCoords()[0] == ycoord && p.ReturnCoords()[1] == xcoord + 1 && p.ReturnDoubleMoved())
                    {
                        list.Add(new int[] { ycoord - 1, xcoord + 1, ycoord });
                    }
                    else if (p.ReturnCoords()[0] == ycoord && p.ReturnCoords()[1] == xcoord - 1 && p.ReturnDoubleMoved())
                    {
                        list.Add(new int[] { ycoord - 1, xcoord - 1, ycoord });
                    }
                }
            }
            return list;
        }
    }
    class Rook : Pieces // Finished
    {
        private bool hasmoved = false;
        public Rook(int inycoord, int inxcoord, bool incolour) : base(inycoord, inxcoord, incolour)
        {

        }
        public override void Moved()
        {
            hasmoved = true;
        }
        public override bool ReturnMoved()
        {
            return hasmoved;
        }
        public override char ReturnType()
        {
            return 'R';
        }
        public override List<int[]> AvailablePlaces(char[,] board, List<Pieces> pieces)
        {
            List<int[]> list = new List<int[]>();
            for (int y = ycoord + 1; y < 8; y++)
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
    class Knight : Pieces // Finished
    {
        public Knight(int inycoord, int inxcoord, bool incolour) : base(inycoord, inxcoord, incolour)
        {

        }
        public override char ReturnType()
        {
            return 'k';
        }
        public override List<int[]> AvailablePlaces(char[,] board, List<Pieces> pieces)
        {
            List<int[]> list = new List<int[]>();
            if (ycoord - 2 >= 0 && xcoord - 1 >= 0)
            {
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
            }
            if (ycoord - 2 >= 0 && xcoord + 1 < 8)
            {
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
            }
            if (ycoord - 1 >= 0 && xcoord - 2 >= 0)
            {
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
            }
            if (ycoord - 1 >= 0 && xcoord + 2 < 8)
            {
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
            }
            if (ycoord + 2 < 8 && xcoord - 1 >= 0)
            {
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
            }
            if (ycoord + 2 < 8 && xcoord + 1 < 8)
            {
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
            }
            if (ycoord + 1 < 8 && xcoord - 2 >= 0)
            {
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
            }
            if (ycoord + 1 < 8 && xcoord + 2 < 8)
            {
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
            }
            return list;
        }
    }
    class Bishop : Pieces // Finished
    {
        public Bishop(int inycoord, int inxcoord, bool incolour) : base(inycoord, inxcoord, incolour)
        {

        }
        public override char ReturnType()
        {
            return 'B';
        }
        public override List<int[]> AvailablePlaces(char[,] board, List<Pieces> pieces)
        {
            List<int[]> list = new List<int[]>();
            int y = ycoord + 1;
            for (int x = xcoord + 1; x < 8; x++)
            {
                if (y > 7)
                {
                    break;
                }
                if (board[y, x] != '_')
                {
                    foreach (Pieces p in pieces)
                    {
                        if (p.ReturnCoords()[0] == y && p.ReturnCoords()[1] == x && colour != p.ReturnColour())
                        {
                            list.Add(new int[] { y, x });
                        }
                    }
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
                    foreach (Pieces p in pieces)
                    {
                        if (p.ReturnCoords()[0] == y && p.ReturnCoords()[1] == x && colour != p.ReturnColour())
                        {
                            list.Add(new int[] { y, x });
                        }
                    }
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
                    foreach (Pieces p in pieces)
                    {
                        if (p.ReturnCoords()[0] == y && p.ReturnCoords()[1] == x && colour != p.ReturnColour())
                        {
                            list.Add(new int[] { y, x });
                        }
                    }
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
                    foreach (Pieces p in pieces)
                    {
                        if (p.ReturnCoords()[0] == y && p.ReturnCoords()[1] == x && colour != p.ReturnColour())
                        {
                            list.Add(new int[] { y, x });
                        }
                    }
                    break;
                }
                list.Add(new int[] { y, x });
                y--;
            }
            return list;
        }
    }
    class King : Pieces // Check, Checkmate
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
        public override List<int[]> AvailablePlaces(char[,] board, List<Pieces> pieces)
        {
            List<int[]> list = new List<int[]>();
            if (ycoord + 1 < 8)
            {
                if (board[ycoord + 1, xcoord] == '_')
                {
                    list.Add(new int[] { ycoord + 1, xcoord });
                }
                else
                {
                    foreach (Pieces p in pieces)
                    {
                        if (p.ReturnCoords()[0] == ycoord + 1 && p.ReturnCoords()[1] == xcoord && colour != p.ReturnColour())
                        {
                            list.Add(new int[] { ycoord + 1, xcoord });
                        }
                    }
                }
            }
            if (ycoord + 1 < 8 && xcoord + 1 < 8)
            {
                if (board[ycoord + 1, xcoord + 1] == '_')
                {
                    list.Add(new int[] { ycoord + 1, xcoord + 1 });
                }
                else
                {
                    foreach (Pieces p in pieces)
                    {
                        if (p.ReturnCoords()[0] == ycoord + 1 && p.ReturnCoords()[1] == xcoord + 1 && colour != p.ReturnColour())
                        {
                            list.Add(new int[] { ycoord + 1, xcoord + 1 });
                        }
                    }
                }
            }
            if (xcoord + 1 < 8)
            {
                if (board[ycoord, xcoord + 1] == '_')
                {
                    list.Add(new int[] { ycoord, xcoord + 1 });
                }
                else
                {
                    foreach (Pieces p in pieces)
                    {
                        if (p.ReturnCoords()[0] == ycoord && p.ReturnCoords()[1] == xcoord + 1 && colour != p.ReturnColour())
                        {
                            list.Add(new int[] { ycoord, xcoord + 1 });
                        }
                    }
                }
            }
            if (ycoord - 1 >= 0 && xcoord + 1 < 8)
            {
                if (board[ycoord - 1, xcoord + 1] == '_')
                {
                    list.Add(new int[] { ycoord - 1, xcoord + 1 });
                }
                else
                {
                    foreach (Pieces p in pieces)
                    {
                        if (p.ReturnCoords()[0] == ycoord - 1 && p.ReturnCoords()[1] == xcoord + 1 && colour != p.ReturnColour())
                        {
                            list.Add(new int[] { ycoord - 1, xcoord + 1 });
                        }
                    }
                }
            }
            if (ycoord - 1 >= 0)
            {
                if (board[ycoord - 1, xcoord] == '_')
                {
                    list.Add(new int[] { ycoord - 1, xcoord });
                }
                else
                {
                    foreach (Pieces p in pieces)
                    {
                        if (p.ReturnCoords()[0] == ycoord - 1 && p.ReturnCoords()[1] == xcoord && colour != p.ReturnColour())
                        {
                            list.Add(new int[] { ycoord - 1, xcoord });
                        }
                    }
                }
            }
            if (ycoord + 1 < 8 && xcoord - 1 >= 0)
            {
                if (board[ycoord + 1, xcoord - 1] == '_')
                {
                    list.Add(new int[] { ycoord + 1, xcoord - 1 });
                }
                else
                {
                    foreach (Pieces p in pieces)
                    {
                        if (p.ReturnCoords()[0] == ycoord - 1 && p.ReturnCoords()[1] == xcoord - 1 && colour != p.ReturnColour())
                        {
                            list.Add(new int[] { ycoord - 1, xcoord - 1 });
                        }
                    }
                }
            }
            if (xcoord - 1 >= 0)
            {
                if (board[ycoord, xcoord - 1] == '_')
                {
                    list.Add(new int[] { ycoord, xcoord - 1 });
                }
                else
                {
                    foreach (Pieces p in pieces)
                    {
                        if (p.ReturnCoords()[0] == ycoord && p.ReturnCoords()[1] == xcoord - 1 && colour != p.ReturnColour())
                        {
                            list.Add(new int[] { ycoord, xcoord - 1 });
                        }
                    }
                }
            }
            if (ycoord - 1 >= 0 && xcoord - 1 >= 0)
            {
                if (board[ycoord - 1, xcoord - 1] == '_')
                {
                    list.Add(new int[] { ycoord - 1, xcoord - 1 });
                }
                else
                {
                    foreach (Pieces p in pieces)
                    {
                        if (p.ReturnCoords()[0] == ycoord - 1 && p.ReturnCoords()[1] == xcoord - 1 && colour != p.ReturnColour())
                        {
                            list.Add(new int[] { ycoord - 1, xcoord - 1 });
                        }
                    }
                }
            }
            if (!hasmoved)
            {
                if (board[ycoord, xcoord + 1] == '_' && board[ycoord, xcoord + 2] == '_')
                {
                    foreach (Pieces p in pieces)
                    {
                        if (p.ReturnCoords()[0] == ycoord && p.ReturnCoords()[1] == xcoord + 3 && p.ReturnType() == 'R' && !p.ReturnMoved())
                        {
                            list.Add(new int[] { ycoord, xcoord + 2 });
                        }
                    }
                }
                else if (board[ycoord, xcoord - 1] == '_' && board[ycoord, xcoord - 2] == '_' && board[ycoord, xcoord - 3] == '_')
                {
                    foreach (Pieces p in pieces)
                    {
                        if (p.ReturnCoords()[0] == ycoord && p.ReturnCoords()[1] == xcoord - 4 && p.ReturnType() == 'R' && !p.ReturnMoved())
                        {
                            list.Add(new int[] { ycoord, xcoord - 2 });
                        }
                    }
                }
            }
            return list;
        }
    }
    class Queen : Pieces // Finished
    {
        public Queen(int inycoord, int inxcoord, bool incolour) : base(inycoord, inxcoord, incolour)
        {

        }
        public override char ReturnType()
        {
            return 'Q';
        }
        public override List<int[]> AvailablePlaces(char[,] board, List<Pieces> pieces)
        {
            List<int[]> list = new List<int[]>();
            for (int i = ycoord + 1; i < 8; i++)
            {
                if (board[i, xcoord] != '_')
                {
                    foreach (Pieces p in pieces)
                    {
                        if (p.ReturnCoords()[0] == i && p.ReturnCoords()[1] == xcoord && colour != p.ReturnColour())
                        {
                            list.Add(new int[] { i, xcoord });
                        }
                    }
                    break;
                }
                list.Add(new int[] { i, xcoord });
            }
            for (int i = ycoord - 1; i >= 0; i--)
            {
                if (board[i, xcoord] != '_')
                {
                    foreach (Pieces p in pieces)
                    {
                        if (p.ReturnCoords()[0] == i && p.ReturnCoords()[1] == xcoord && colour != p.ReturnColour())
                        {
                            list.Add(new int[] { i, xcoord });
                        }
                    }
                    break;
                }
                list.Add(new int[] { i, xcoord });
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
            int y = ycoord + 1;
            for (int x = xcoord + 1; x < 8; x++)
            {
                if (y > 7)
                {
                    break;
                }
                if (board[y, x] != '_')
                {
                    foreach (Pieces p in pieces)
                    {
                        if (p.ReturnCoords()[0] == y && p.ReturnCoords()[1] == x && colour != p.ReturnColour())
                        {
                            list.Add(new int[] { y, x });
                        }
                    }
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
                    foreach (Pieces p in pieces)
                    {
                        if (p.ReturnCoords()[0] == y && p.ReturnCoords()[1] == x && colour != p.ReturnColour())
                        {
                            list.Add(new int[] { y, x });
                        }
                    }
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
                    foreach (Pieces p in pieces)
                    {
                        if (p.ReturnCoords()[0] == y && p.ReturnCoords()[1] == x && colour != p.ReturnColour())
                        {
                            list.Add(new int[] { y, x });
                        }
                    }
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
                    foreach (Pieces p in pieces)
                    {
                        if (p.ReturnCoords()[0] == y && p.ReturnCoords()[1] == x && colour != p.ReturnColour())
                        {
                            list.Add(new int[] { y, x });
                        }
                    }
                    break;
                }
                list.Add(new int[] { y, x });
                y--;
            }
            return list;
        }
    }
}
