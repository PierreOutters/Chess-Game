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
        static void SetupPieces(char[,] board)
        {
            Pieces[] pieces = new Pieces[32];
            int k = 0;
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[j,k] == 'R')
                    {
                        pieces[k] = new Pieces(3, i, j, true);
                        k++;
                    }
                    else if (board[j, k] == 'k')
                    {
                        pieces[k] = new Pieces(2, i, j, true);
                        k++;
                    }
                    else if (board[j, k] == 'B')
                    {
                        pieces[k] = new Pieces(1, i, j, true);
                        k++;
                    }
                    else if (board[j, k] == 'Q')
                    {
                        pieces[k] = new Pieces(4, i, j, true);
                        k++;
                    }
                    else if (board[j, k] == 'K')
                    {
                        pieces[k] = new Pieces(5, i, j, true);
                        k++;
                    }
                    else if (board[j, k] == 'p')
                    {
                        pieces[k] = new Pieces(0, i, j, true);
                        k++;
                    }
                }
            }
            for (int i = 6; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[j, k] == 'R')
                    {
                        pieces[k] = new Pieces(3, i, j, false);
                        k++;
                    }
                    else if (board[j, k] == 'k')
                    {
                        pieces[k] = new Pieces(2, i, j, false);
                        k++;
                    }
                    else if (board[j, k] == 'B')
                    {
                        pieces[k] = new Pieces(1, i, j, false);
                        k++;
                    }
                    else if (board[j, k] == 'Q')
                    {
                        pieces[k] = new Pieces(4, i, j, false);
                        k++;
                    }
                    else if (board[j, k] == 'K')
                    {
                        pieces[k] = new Pieces(5, i, j, false);
                        k++;
                    }
                    else if (board[j, k] == 'p')
                    {
                        pieces[k] = new Pieces(0, i, j, false);
                        k++;
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            char[,] board = CreateBoard();
            SetupPieces(board);
        }
    }
    class Pieces
    {
        private int type; //0 for pawn, 1 for bisop, 2 for knight, 3 for rook, 4 for queen, 5 for king
        private int ycoord, xcoord;
        bool alive = true;
        bool colour; //false for white, true for black
        public Pieces(int intype, int inycoord, int inxcoord, bool incolour)
        {
            type = intype; ycoord = inycoord; xcoord = inxcoord; colour = incolour;
        }
        public bool IsAlive()
        {
            return alive;
        }
    }
}
