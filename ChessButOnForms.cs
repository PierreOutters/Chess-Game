using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess_With_Forms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static char[,] CreateBoard()
        {
            char[,] board =
            {
                { 'R', 'k', 'B', 'Q', 'K', 'B', 'k', 'R'},
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
        static List<Pieces> SetupPieces(char[,] board)
        {
            List<Pieces> pieces = new List<Pieces>
            {
                new Pawn(1000, 1000, false)
            };
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
        }
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            char[,] board = CreateBoard();
            List<Pieces> pieces = SetupPieces(board);
            Application.Run(new Form1(board));
        }
    }
    abstract class Pieces // Finished
    {
        protected int ycoord, xcoord;
        protected bool alive = true;
        protected bool colour; //false for white, true for black
        protected bool wcheck, bcheck;
        protected bool checkmate, stalemate;
        protected bool fwcheck, fbcheck;
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
        public void ChangeToCoord(int y, int x)
        {
            ycoord = y; xcoord = x;
        }
        public void Check(bool colour)
        {
            if (colour)
            {
                bcheck = true;
                fbcheck = true;
            }
            else
            {
                wcheck = true;
                fwcheck = true;
            }
        }
        public void Stalemate()
        {
            Console.WriteLine("The game ended on a draw");
            Console.WriteLine("Press enter to play another game");
            Console.ReadKey(true);
            stalemate = true;
        }
        public void CheckMate(bool colour)
        {
            if (colour)
            {
                Console.WriteLine("Black has won the game");
            }
            else
            {
                Console.WriteLine("White has won the game");
            }
            Console.WriteLine("Press enter to play another game");
            checkmate = true;
            Console.ReadKey(true);
        }
        public bool ReturnStalemate()
        {
            return stalemate;
        }
        public bool ReturnCheckMate()
        {
            return checkmate;
        }
        public bool ReturnFCheck(bool colour)
        {
            if (colour)
            {
                return fbcheck;
            }
            else
            {
                return fwcheck;
            }
        }
        public bool ReturnCheck(bool colour)
        {
            if (colour)
            {
                return bcheck;
            }
            else
            {
                return wcheck;
            }
        }
        public void ResetFCheck(bool colour)
        {
            if (colour)
            {
                fbcheck = false;
            }
            else
            {
                fwcheck = false;
            }
        }
        public void ResetCheck()
        {
            wcheck = false; bcheck = false;
        }
        public abstract List<int[]> AvailablePlaces(char[,] board, List<Pieces> pieces);
        public abstract char ReturnType();
        public virtual void Moved()
        {
        }
        public virtual void UnMoved()
        {
        }
        public virtual void Promote(List<Pieces> pieces, char[,] board, List<List<string>> takeBacks)
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
        public override void Promote(List<Pieces> pieces, char[,] board, List<List<string>> takeBacks)
        {
            if (ycoord == 7 || ycoord == 0)
            {
                List<string> list = takeBacks[takeBacks.Count - 1];
                Console.WriteLine("Your pawn can now promote");
                Console.ReadKey(true);
                Console.WriteLine("Enter (Q) for a Queen \nEnter (R) for a Rook \nEnter (B) for a Bishop \nEnter (K) for a Knight");
                string promotion = Console.ReadLine().ToUpper();
                while (promotion != "Q" && promotion != "R" && promotion != "B" && promotion != "K")
                {
                    Console.WriteLine("Invalid input");
                    Console.WriteLine("Enter (Q) for a Queen \nEnter (R) for a Rook \nEnter (B) for a Bishop \nEnter (K) for a Knight");
                    promotion = Console.ReadLine().ToUpper();
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
                string str = pieces[pieces.Count - 1].ReturnType().ToString() + ycoord.ToString() + xcoord.ToString() + colour.ToString()[0].ToString();
                list.Add(str);
            }
        }
        public override List<int[]> AvailablePlaces(char[,] board, List<Pieces> pieces)
        {
            List<int[]> list = new List<int[]>();
            if (ycoord == 1000)
            {
                return list;
            }
            if (colour == false && ycoord - 1 >= 0)
            {
                if (board[ycoord - 1, xcoord] == '_')
                {
                    list.Add(new int[] { ycoord - 1, xcoord });
                    if (!hasmoved)
                    {
                        if (colour == false && ycoord - 1 > 0)
                        {
                            if (board[ycoord - 2, xcoord] == '_')
                            {
                                list.Add(new int[] { ycoord - 2, xcoord });
                            }
                        }
                    }
                }
            }
            if (colour == true && ycoord + 1 < 8)
            {
                if (board[ycoord + 1, xcoord] == '_')
                {
                    list.Add(new int[] { ycoord + 1, xcoord });
                    if (!hasmoved)
                    {
                        if (colour == true && ycoord + 1 < 7)
                        {
                            if (board[ycoord + 2, xcoord] == '_')
                            {
                                list.Add(new int[] { ycoord + 2, xcoord });
                            }
                        }
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
                        if (p.ReturnType() == 'K' && p.ReturnColour() != colour)
                        {
                            pieces[0].Check(!colour);
                        }
                    }
                    else if (p.ReturnCoords()[0] == ycoord + 1 && p.ReturnCoords()[1] == xcoord - 1)
                    {
                        list.Add(new int[] { ycoord + 1, xcoord - 1 });
                        if (p.ReturnType() == 'K' && p.ReturnColour() != colour)
                        {
                            pieces[0].Check(!colour);
                        }
                    }
                }
                else if (p.ReturnColour() != colour && colour == false)
                {
                    if (p.ReturnCoords()[0] == ycoord - 1 && p.ReturnCoords()[1] == xcoord + 1)
                    {
                        list.Add(new int[] { ycoord - 1, xcoord + 1 });
                        if (p.ReturnType() == 'K' && p.ReturnColour() != colour)
                        {
                            pieces[0].Check(!colour);
                        }
                    }
                    else if (p.ReturnCoords()[0] == ycoord - 1 && p.ReturnCoords()[1] == xcoord - 1)
                    {
                        list.Add(new int[] { ycoord - 1, xcoord - 1 });
                        if (p.ReturnType() == 'K' && p.ReturnColour() != colour)
                        {
                            pieces[0].Check(!colour);
                        }
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
                            if (p.ReturnType() == 'K' && p.ReturnColour() != colour)
                            {
                                pieces[0].Check(!colour);
                            }
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
                            if (p.ReturnType() == 'K' && p.ReturnColour() != colour)
                            {
                                pieces[0].Check(!colour);
                            }
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
                            if (p.ReturnType() == 'K' && p.ReturnColour() != colour)
                            {
                                pieces[0].Check(!colour);
                            }
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
                            if (p.ReturnType() == 'K' && p.ReturnColour() != colour)
                            {
                                pieces[0].Check(!colour);
                            }
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
                            if (p.ReturnType() == 'K' && p.ReturnColour() != colour)
                            {
                                pieces[0].Check(!colour);
                            }
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
                            if (p.ReturnType() == 'K' && p.ReturnColour() != colour)
                            {
                                pieces[0].Check(!colour);
                            }
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
                            if (p.ReturnType() == 'K' && p.ReturnColour() != colour)
                            {
                                pieces[0].Check(!colour);
                            }
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
                            if (p.ReturnType() == 'K' && p.ReturnColour() != colour)
                            {
                                pieces[0].Check(!colour);
                            }
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
                            if (p.ReturnType() == 'K' && p.ReturnColour() != colour)
                            {
                                pieces[0].Check(!colour);
                            }
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
                            if (p.ReturnType() == 'K' && p.ReturnColour() != colour)
                            {
                                pieces[0].Check(!colour);
                            }
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
                            if (p.ReturnType() == 'K' && p.ReturnColour() != colour)
                            {
                                pieces[0].Check(!colour);
                            }
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
                            if (p.ReturnType() == 'K' && p.ReturnColour() != colour)
                            {
                                pieces[0].Check(!colour);
                            }
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
                            if (p.ReturnType() == 'K' && p.ReturnColour() != colour)
                            {
                                pieces[0].Check(!colour);
                            }
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
                            if (p.ReturnType() == 'K' && p.ReturnColour() != colour)
                            {
                                pieces[0].Check(!colour);
                            }
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
                            if (p.ReturnType() == 'K' && p.ReturnColour() != colour)
                            {
                                pieces[0].Check(!colour);
                            }
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
                            if (p.ReturnType() == 'K' && p.ReturnColour() != colour)
                            {
                                pieces[0].Check(!colour);
                            }
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
    class King : Pieces // Finished
    {
        private bool hasmoved = false;
        public King(int inycoord, int inxcoord, bool incolour) : base(inycoord, inxcoord, incolour)
        {

        }
        public override void Moved()
        {
            hasmoved = true;
        }
        public override void UnMoved()
        {
            hasmoved = false;
        }
        public override char ReturnType()
        {
            return 'K';
        }
        static bool CheckCheck(char[,] board, List<Pieces> pieces, bool colour)
        {
            foreach (Pieces p in pieces)
            {
                if (p.ReturnType() != 'K')
                {
                    p.AvailablePlaces(board, pieces);
                }
            }
            return pieces[0].ReturnCheck(colour);
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
                        if (p.ReturnCoords()[0] == ycoord + 1 && p.ReturnCoords()[1] == xcoord - 1 && colour != p.ReturnColour())
                        {
                            list.Add(new int[] { ycoord + 1, xcoord - 1 });
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
            bool castle = true, resetFCheck = false;
            if (!hasmoved && !pieces[0].ReturnFCheck(colour))
            {
                for (int i = 0; i < xcoord; i++)
                {
                    if (board[ycoord, i] == 'R')
                    {
                        for (int j = 0; j < pieces.Count; j++)
                        {
                            if (pieces[j].ReturnCoords()[0] == ycoord && pieces[j].ReturnCoords()[1] == i)
                            {
                                if (!pieces[j].ReturnMoved())
                                {
                                    for (int k = 3; k < xcoord; k++)
                                    {
                                        if (board[ycoord, k] != '_')
                                        {
                                            castle = false;
                                        }
                                        if (!pieces[0].ReturnFCheck(colour))
                                        {
                                            resetFCheck = true;
                                        }
                                        int x = xcoord;
                                        board[ycoord, xcoord] = '_';
                                        char temp = board[ycoord, k];
                                        board[ycoord, k] = 'K';
                                        ChangeToCoord(ycoord, k);
                                        if (CheckCheck(board, pieces, colour))
                                        {
                                            pieces[0].ResetCheck();
                                            castle = false;
                                        }
                                        pieces[0].ResetCheck();
                                        board[ycoord, k] = temp;
                                        board[ycoord, x] = 'K';
                                        ChangeToCoord(ycoord, x);
                                    }
                                    if (castle)
                                    {
                                        list.Add(new int[] { ycoord, 2 });
                                    }
                                    if (resetFCheck)
                                    {
                                        pieces[0].ResetFCheck(colour);
                                    }
                                }
                            }
                        }
                    }
                }
                castle = true; resetFCheck = false;
                for (int i = xcoord + 1; i < 8; i++)
                {
                    if (board[ycoord, i] == 'R')
                    {
                        for (int j = 0; j < pieces.Count; j++)
                        {
                            if (pieces[j].ReturnCoords()[0] == ycoord && pieces[j].ReturnCoords()[1] == i)
                            {
                                if (!pieces[j].ReturnMoved())
                                {
                                    for (int k = xcoord + 1; k < 6; k++)
                                    {
                                        if (board[ycoord, k] != '_')
                                        {
                                            castle = false;
                                        }
                                        if (!pieces[0].ReturnFCheck(colour))
                                        {
                                            resetFCheck = true;
                                        }
                                        int x = xcoord;
                                        board[ycoord, xcoord] = '_';
                                        char temp = board[ycoord, k];
                                        board[ycoord, k] = 'K';
                                        ChangeToCoord(ycoord, k);
                                        if (CheckCheck(board, pieces, colour))
                                        {
                                            pieces[0].ResetCheck();
                                            castle = false;
                                        }
                                        board[ycoord, k] = temp;
                                        board[ycoord, x] = 'K';
                                        ChangeToCoord(ycoord, x);
                                    }
                                    if (castle)
                                    {
                                        list.Add(new int[] { ycoord, 6 });
                                    }
                                    if (resetFCheck)
                                    {
                                        pieces[0].ResetFCheck(colour);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            CheckCheck(board, pieces, !colour);
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
                            if (p.ReturnType() == 'K' && p.ReturnColour() != colour)
                            {
                                pieces[0].Check(!colour);
                            }
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
                            if (p.ReturnType() == 'K' && p.ReturnColour() != colour)
                            {
                                pieces[0].Check(!colour);
                            }
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
                            if (p.ReturnType() == 'K' && p.ReturnColour() != colour)
                            {
                                pieces[0].Check(!colour);
                            }
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
                            if (p.ReturnType() == 'K' && p.ReturnColour() != colour)
                            {
                                pieces[0].Check(!colour);
                            }
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
                            if (p.ReturnType() == 'K' && p.ReturnColour() != colour)
                            {
                                pieces[0].Check(!colour);
                            }
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
                            if (p.ReturnType() == 'K' && p.ReturnColour() != colour)
                            {
                                pieces[0].Check(!colour);
                            }
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
                            if (p.ReturnType() == 'K' && p.ReturnColour() != colour)
                            {
                                pieces[0].Check(!colour);
                            }
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
                            if (p.ReturnType() == 'K' && p.ReturnColour() != colour)
                            {
                                pieces[0].Check(!colour);
                            }
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
