using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    class Program // Need Documentation
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
        } // Finished
        static char[,] CreateChess960Board()
        {
            int k = 2, R = 2, K = 1, B = 2, Q = 1;
            List<char> chars = new List<char>();
            Random random = new Random();
            while (k > 0 || R > 0 || K > 0 || B > 0 || Q > 0)
            {
                int rnd = random.Next(0, 5);
                if (rnd == 0 && k > 0)
                {
                    k--; chars.Add('k');
                }
                else if (rnd == 1 && R == 1 && K == 0 || rnd == 1 && R == 2)
                {
                    R--; chars.Add('R');
                }
                else if (rnd == 2 && K == 1 && R == 1)
                {
                    K--; chars.Add('K');
                }
                else if (rnd == 3 && B > 0)
                {
                    B--; chars.Add('B');
                }
                else if (rnd == 4 && Q > 0)
                {
                    Q--; chars.Add('Q');
                }
            }
            char[,] board =
            {
                { chars[0], chars[1], chars[2], chars[3], chars[4], chars[5], chars[6], chars[7]},
                { 'p', 'p', 'p', 'p', 'p', 'p', 'p', 'p'},
                { '_', '_', '_', '_', '_', '_', '_', '_'},
                { '_', '_', '_', '_', '_', '_', '_', '_'},
                { '_', '_', '_', '_', '_', '_', '_', '_'},
                { '_', '_', '_', '_', '_', '_', '_', '_'},
                { 'p', 'p', 'p', 'p', 'p', 'p', 'p', 'p'},
                { chars[0], chars[1], chars[2], chars[3], chars[4], chars[5], chars[6], chars[7]}
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
        static bool TakeBacks(char[,] board, List<Pieces> pieces, List<List<string>> takeBacks)
        {
            if (takeBacks.Count == 0)
            {
                return false;
            }
            int i = takeBacks.Count - 1;
            if (takeBacks[i].Count == 2)
            {
                if (takeBacks[i][1].Length == 3)
                {
                    board[int.Parse(takeBacks[i][1][1].ToString()), int.Parse(takeBacks[i][1][2].ToString())] = takeBacks[i][1][0];
                    foreach (Pieces p in pieces)
                    {
                        if (p.ReturnCoords()[0] == int.Parse(takeBacks[i][1][1].ToString()) && p.ReturnCoords()[1] == int.Parse(takeBacks[i][1][2].ToString()))
                        {
                            p.ChangeToCoord(int.Parse(takeBacks[i][0][1].ToString()), int.Parse(takeBacks[i][0][2].ToString()));
                            board[p.ReturnCoords()[0], p.ReturnCoords()[1]] = p.ReturnType();
                        }
                    }
                }
                else
                {
                    board[int.Parse(takeBacks[i][1][1].ToString()), int.Parse(takeBacks[i][1][2].ToString())] = takeBacks[i][1][0];
                    foreach (Pieces p in pieces)
                    {
                        if (p.ReturnCoords()[0] == int.Parse(takeBacks[i][1][1].ToString()) && p.ReturnCoords()[1] == int.Parse(takeBacks[i][1][2].ToString()))
                        {
                            p.ChangeToCoord(int.Parse(takeBacks[i][0][1].ToString()), int.Parse(takeBacks[i][0][2].ToString()));
                            board[p.ReturnCoords()[0], p.ReturnCoords()[1]] = p.ReturnType();
                        }
                    }
                    bool col;
                    if (takeBacks[i][1][3] == 'F')
                    {
                        col = false;
                    }
                    else
                    {
                        col = true;
                    }
                    if (takeBacks[i][1][0] == 'p')
                    {
                        pieces.Add(new Pawn(int.Parse(takeBacks[i][1][1].ToString()), int.Parse(takeBacks[i][1][2].ToString()), col));
                    }
                    else if (takeBacks[i][1][0] == 'R')
                    {
                        pieces.Add(new Rook(int.Parse(takeBacks[i][1][1].ToString()), int.Parse(takeBacks[i][1][2].ToString()), col));
                    }
                    else if (takeBacks[i][1][0] == 'Q')
                    {
                        pieces.Add(new Queen(int.Parse(takeBacks[i][1][1].ToString()), int.Parse(takeBacks[i][1][2].ToString()), col));
                    }
                    else if (takeBacks[i][1][0] == 'k')
                    {
                        pieces.Add(new Knight(int.Parse(takeBacks[i][1][1].ToString()), int.Parse(takeBacks[i][1][2].ToString()), col));
                    }
                    else
                    {
                        pieces.Add(new Bishop(int.Parse(takeBacks[i][1][1].ToString()), int.Parse(takeBacks[i][1][2].ToString()), col));
                    }
                }
            }
            else if (takeBacks[i].Count == 3)
            {
                if (takeBacks[i][2].Length == 4)
                {
                    for (int j = 0; j < pieces.Count; j++)
                    {
                        if (pieces[j].ReturnCoords()[0] == int.Parse(takeBacks[i][2][1].ToString()) && pieces[j].ReturnCoords()[1] == int.Parse(takeBacks[i][2][2].ToString()))
                        {
                            board[pieces[j].ReturnCoords()[0], pieces[j].ReturnCoords()[1]] = takeBacks[i][1][0];
                            pieces.RemoveAt(j);
                        }
                    }
                    bool col;
                    if (takeBacks[i][0][3] == 'F')
                    {
                        col = false;
                    }
                    else
                    {
                        col = true;
                    }
                    pieces.Add(new Pawn(int.Parse(takeBacks[i][0][1].ToString()), int.Parse(takeBacks[i][0][2].ToString()), col));
                    board[pieces[pieces.Count - 1].ReturnCoords()[0], pieces[pieces.Count - 1].ReturnCoords()[1]] = 'p';
                    if (takeBacks[i][1].Length != 3)
                    {
                        if (takeBacks[i][1][3] == 'F')
                        {
                            col = false;
                        }
                        else
                        {
                            col = true;
                        }
                        if (takeBacks[i][1][0] == 'p')
                        {
                            pieces.Add(new Pawn(int.Parse(takeBacks[i][1][1].ToString()), int.Parse(takeBacks[i][1][2].ToString()), col));
                        }
                        else if (takeBacks[i][1][0] == 'R')
                        {
                            pieces.Add(new Rook(int.Parse(takeBacks[i][1][1].ToString()), int.Parse(takeBacks[i][1][2].ToString()), col));
                        }
                        else if (takeBacks[i][1][0] == 'Q')
                        {
                            pieces.Add(new Queen(int.Parse(takeBacks[i][1][1].ToString()), int.Parse(takeBacks[i][1][2].ToString()), col));
                        }
                        else if (takeBacks[i][1][0] == 'k')
                        {
                            pieces.Add(new Knight(int.Parse(takeBacks[i][1][1].ToString()), int.Parse(takeBacks[i][1][2].ToString()), col));
                        }
                        else
                        {
                            pieces.Add(new Bishop(int.Parse(takeBacks[i][1][1].ToString()), int.Parse(takeBacks[i][1][2].ToString()), col));
                        }
                    }
                }
                else
                {
                    bool col = false;
                    board[int.Parse(takeBacks[i][1][1].ToString()), int.Parse(takeBacks[i][1][2].ToString())] = takeBacks[i][1][0];
                    foreach (Pieces p in pieces)
                    {
                        if (p.ReturnCoords()[0] == int.Parse(takeBacks[i][1][1].ToString()) && p.ReturnCoords()[1] == int.Parse(takeBacks[i][1][2].ToString()))
                        {
                            p.ChangeToCoord(int.Parse(takeBacks[i][0][1].ToString()), int.Parse(takeBacks[i][0][2].ToString()));
                            board[p.ReturnCoords()[0], p.ReturnCoords()[1]] = p.ReturnType();
                            col = p.ReturnColour();
                        }
                    }
                    if (col)
                    {
                        col = false;
                    }
                    else
                    {
                        col = true;
                    }
                    pieces.Add(new Pawn(int.Parse(takeBacks[i][2][1].ToString()), int.Parse(takeBacks[i][2][2].ToString()), col));
                    board[pieces[pieces.Count - 1].ReturnCoords()[0], pieces[pieces.Count - 1].ReturnCoords()[1]] = 'p';
                    pieces[pieces.Count - 1].DoubleMoved();
                }
            }
            else
            {
                board[int.Parse(takeBacks[i][1][1].ToString()), int.Parse(takeBacks[i][1][2].ToString())] = takeBacks[i][1][0];
                foreach (Pieces p in pieces)
                {
                    if (p.ReturnCoords()[0] == int.Parse(takeBacks[i][1][1].ToString()) && p.ReturnCoords()[1] == int.Parse(takeBacks[i][1][2].ToString()))
                    {
                        p.ChangeToCoord(int.Parse(takeBacks[i][0][1].ToString()), int.Parse(takeBacks[i][0][2].ToString()));
                        board[p.ReturnCoords()[0], p.ReturnCoords()[1]] = p.ReturnType();
                    }
                }
                board[int.Parse(takeBacks[i][3][1].ToString()), int.Parse(takeBacks[i][3][2].ToString())] = takeBacks[i][3][0];
                foreach (Pieces p in pieces)
                {
                    if (p.ReturnCoords()[0] == int.Parse(takeBacks[i][3][1].ToString()) && p.ReturnCoords()[1] == int.Parse(takeBacks[i][3][2].ToString()))
                    {
                        p.ChangeToCoord(int.Parse(takeBacks[i][2][1].ToString()), int.Parse(takeBacks[i][2][2].ToString()));
                        board[p.ReturnCoords()[0], p.ReturnCoords()[1]] = p.ReturnType();
                    }
                }
            }
            takeBacks.RemoveAt(i);
            return true;
        }
        static bool EnterMove(char[,] board, List<Pieces> pieces, List<List<string>> takeBacks, bool colour, bool allowTakeBacks)
        {
            if (colour)
            {
                Console.WriteLine("Black to move");
            }
            else
            {
                Console.WriteLine("White to move");
            }
            if (allowTakeBacks)
            {
                Console.WriteLine("Please enter your move");
                Console.WriteLine("Enter TakeBack or tb to take back a move");
            }
            else
            {
                Console.WriteLine("Please enter your move");
            }
            string move = Console.ReadLine();
            if (allowTakeBacks && move.ToLower() == "tb" || allowTakeBacks && move.ToLower() == "takeback")
            {
                if (!TakeBacks(board, pieces, takeBacks))
                {
                    Console.WriteLine("Cannot take back anymore");
                    Console.ReadKey();
                }
                return false;
            }
            else if (!allowTakeBacks && move.ToLower() == "tb" || allowTakeBacks && move.ToLower() == "takeback")
            {
                Console.WriteLine("Cannot take back as take backs are not enabled"); Console.ReadKey(true);
                return false;
            }
            if (move.Length != 3)
            {
                return false;
            }
            else if (!int.TryParse(move[2].ToString(), out _))
            {
                return false;
            }
            int[] coords = ProcessMove(char.Parse(move[1].ToString().ToUpper()), move[2]);
            if (move[0] == 'p' || move[0] == 'R' || move[0] == 'k' || move[0] == 'B' || move[0] == 'K' || move[0] == 'Q')
            {
                return MakeMove(board, pieces, takeBacks, coords, move[0], colour);
            }
            return false;
        } // Finished
        static bool EnPassant(char[,] board, List<Pieces> pieces, List<List<string>> takeBacks, ref int i, int j, int available1)
        {
            if (pieces[i].AvailablePlaces(board, pieces)[j].Length == 3)
            {
                List<string> list = new List<string>();
                int available2 = pieces[i].AvailablePlaces(board, pieces)[j][2];
                string str = "p" + available2.ToString() + available1.ToString() + (!pieces[i].ReturnColour()).ToString()[0] + "F";
                list.Add(str);
                for (int k = 0; k < pieces.Count; k++)
                {
                    if (pieces[k].ReturnCoords()[0] == available2 && pieces[k].ReturnCoords()[1] == available1)
                    {
                        board[pieces[k].ReturnCoords()[0], pieces[k].ReturnCoords()[1]] = '_';
                        if (k < i)
                        {
                            i--;
                        }
                        pieces.RemoveAt(k);
                        takeBacks.Add(list);
                        return true;
                    }
                }
            }
            return false;
        } // Finished
        static bool IsItLegal(char[,] board, List<Pieces> pieces, List<List<string>> takeBacks, ref int i, int available0, int available1, int xcoord, int ycoord, ref bool bcheck, ref bool wcheck, bool colour, bool enPassant)
        {
            List<string> list;
            list = new List<string>();
            string str = pieces[i].ReturnType().ToString() + ycoord.ToString() + xcoord.ToString() + pieces[i].ReturnColour().ToString()[0] + "T";
            list.Add(str);
            string pcolour = "";
            string isalive = "";
            if (board[available0, available1] != '_')
            {
                foreach (Pieces p in pieces)
                {
                    if (p.ReturnCoords()[0] == available0 && p.ReturnCoords()[1] == available1)
                    {
                        pcolour = p.ReturnColour().ToString()[0].ToString();
                        isalive = "F";
                    }
                }
            }
            str = board[available0, available1].ToString() + available0.ToString() + available1.ToString() + pcolour + isalive;
            list.Add(str);
            board[ycoord, xcoord] = '_';
            board[available0, available1] = pieces[i].ReturnType();
            pieces[i].ChangeToCoord(available0, available1);
            Pieces temp = new Knight(100, 100, true);
            for (int k = 0; k < pieces.Count; k++)
            {
                if (i != k && pieces[k].ReturnCoords()[0] == available0 && pieces[k].ReturnCoords()[1] == available1)
                {
                    temp = pieces[k];
                    pieces.RemoveAt(k);
                    if (k < i)
                    {
                        i--;
                    }
                    break;
                }
            }
            if (CheckCheck(board, pieces, colour))
            {
                board[available0, available1] = '_';
                board[ycoord, xcoord] = pieces[i].ReturnType();
                pieces[i].ChangeToCoord(ycoord, xcoord);
                pieces.Add(temp);
                if (pieces[pieces.Count - 1].ReturnCoords()[0] == 100)
                {
                    pieces.RemoveAt(pieces.Count - 1);
                }
                board[pieces[pieces.Count - 1].ReturnCoords()[0], pieces[pieces.Count - 1].ReturnCoords()[1]] = pieces[pieces.Count - 1].ReturnType();
                return false;
            }
            else if (CheckCheck(board, pieces, !colour))
            {
                if (colour)
                {
                    wcheck = true;
                }
                else
                {
                    bcheck = true;
                }
            }
            if (enPassant)
            {
                list.Add(takeBacks[takeBacks.Count - 1][0]);
                takeBacks[takeBacks.Count - 1] = list;
                return true;
            }
            takeBacks.Add(list);
            return true;
        } // Have to fix
        static void Castle(char[,] board, List<Pieces> pieces, List<List<string>> takeBacks, int i, int available1, int ycoord, bool colour)
        {
            if (pieces[i].ReturnType() == 'K' && available1 == 2 && !pieces[0].ReturnFCheck(colour))
            {
                List<string> list = takeBacks[takeBacks.Count - 1];
                for (int j = 0; j < pieces.Count; j++)
                {
                    if (!pieces[j].ReturnMoved() && pieces[j].ReturnType() == 'R' && pieces[j].ReturnCoords()[1] - pieces[i].ReturnCoords()[1] < 0 && pieces[i].ReturnColour() == pieces[j].ReturnColour())
                    {
                        string str = "R" + pieces[j].ReturnCoords()[0].ToString() + pieces[j].ReturnCoords()[1].ToString() + pieces[j].ReturnColour().ToString()[0] + "T";
                        list.Add(str);
                        str = "_" + pieces[j].ReturnCoords()[0].ToString() + "3" + pieces[j].ReturnColour().ToString()[0] + "T";
                        list.Add(str);
                        board[pieces[j].ReturnCoords()[0], pieces[j].ReturnCoords()[1]] = '_';
                        board[pieces[j].ReturnCoords()[0], 3] = pieces[j].ReturnType();
                        pieces[j].ChangeToCoord(pieces[j].ReturnCoords()[0], 3);
                        pieces[j].Moved();
                    }
                }
            }
            if (pieces[i].ReturnType() == 'K' && available1 == 6 && !pieces[0].ReturnFCheck(colour))
            {
                List<string> list = takeBacks[takeBacks.Count - 1];
                for (int j = 0; j < pieces.Count; j++)
                {
                    if (!pieces[j].ReturnMoved() && pieces[j].ReturnType() == 'R' && pieces[j].ReturnCoords()[1] - pieces[i].ReturnCoords()[1] > 0 && pieces[i].ReturnColour() == pieces[j].ReturnColour())
                    {
                        string str = "R" + pieces[j].ReturnCoords()[0].ToString() + pieces[j].ReturnCoords()[1].ToString() + pieces[j].ReturnColour().ToString()[0] + "T";
                        list.Add(str);
                        str = "_" + pieces[j].ReturnCoords()[0].ToString() + "5" + pieces[j].ReturnColour().ToString()[0] + "T";
                        list.Add(str);
                        board[pieces[j].ReturnCoords()[0], pieces[j].ReturnCoords()[1]] = '_';
                        board[pieces[j].ReturnCoords()[0], 5] = pieces[j].ReturnType();
                        pieces[j].ChangeToCoord(pieces[j].ReturnCoords()[0], 5);
                        pieces[j].Moved();
                    }
                }
            }
        } // Finished
        static void CheckingDoubleMoved(List<Pieces> pieces, int i, int available0)
        {
            if (pieces[i].ReturnType() == 'p' && !pieces[i].ReturnMoved() && available0 == 3 || pieces[i].ReturnType() == 'p' && !pieces[i].ReturnMoved() && available0 == 4)
            {
                pieces[i].DoubleMoved();
            }
            foreach (Pieces p in pieces)
            {
                if (p.ReturnMoved())
                {
                    p.NoDoubleMoved();
                }
            }
        } // Finished
        static void RemovingDeadPieces(List<Pieces> pieces, ref int i)
        {
            for (int k = 0; k < pieces.Count; k++)
            {
                if (pieces[k].ReturnCoords()[0] == pieces[i].ReturnCoords()[0] && pieces[k].ReturnCoords()[1] == pieces[i].ReturnCoords()[1] && pieces[k].ReturnColour() != pieces[i].ReturnColour())
                {
                    pieces.RemoveAt(k);
                    i--; k--;
                }
            }
        } // Finished
        static bool CheckEndGame(char[,] board, List<Pieces> pieces, bool wcheck, bool bcheck, bool colour)
        {
            if (wcheck || bcheck)
            {
                if (CheckCheckMate(board, pieces, colour))
                {
                    Console.WriteLine("King is in CheckMate");
                    pieces[0].CheckMate(colour);
                    return true;
                }
            }
            else
            {
                if (CheckCheckMate(board, pieces, colour))
                {
                    Console.WriteLine(colour + " has no legal moves");
                    pieces[0].Stalemate();
                    return true;
                }
            }
            if (wcheck)
            {
                Console.WriteLine("White king is in check");
                Console.ReadKey(true);
            }
            else if (bcheck)
            {
                Console.WriteLine("Black king is in check");
                Console.ReadKey(true);
            }
            return false;
        } // Finished
        static bool MakeMove(char[,] board, List<Pieces> pieces, List<List<String>> takeBacks, int[] move, char piece, bool colour)
        {
            pieces[0].ResetCheck();
            for (int i = 0; i < pieces.Count; i++)
            {
                if (pieces[i].IsAlive() && pieces[i].ReturnColour() == colour && pieces[i].ReturnType() == piece)
                {
                    for (int j = 0; j < pieces[i].AvailablePlaces(board, pieces).Count; j++)
                    {
                        if (pieces[i].AvailablePlaces(board, pieces)[j][0] == move[0] && pieces[i].AvailablePlaces(board, pieces)[j][1] == move[1])
                        {
                            int ycoord = pieces[i].ReturnCoords()[0]; int xcoord = pieces[i].ReturnCoords()[1];
                            int available0 = pieces[i].AvailablePlaces(board, pieces)[j][0], available1 = pieces[i].AvailablePlaces(board, pieces)[j][1];
                            bool bcheck = false, wcheck = false;
                            bool enPassant = EnPassant(board, pieces, takeBacks, ref i, j, available1);
                            CheckingDoubleMoved(pieces, i, available0);
                            if (!IsItLegal(board, pieces, takeBacks, ref i, available0, available1, xcoord, ycoord, ref bcheck, ref wcheck, colour, enPassant))
                            {
                                return false;
                            }
                            Castle(board, pieces, takeBacks, i, available1, ycoord, colour);
                            pieces[i].ChangeToCoord(available0, available1);
                            pieces[i].Moved();
                            pieces[i].Promote(pieces, board, takeBacks);
                            RemovingDeadPieces(pieces, ref i);
                            CheckEndGame(board, pieces, wcheck, bcheck, colour);
                            return true;
                        }
                    }
                }
            }
            return false;
        } // Finished
        static bool CheckCheck(char[,] board, List<Pieces> pieces, bool colour)
        {
            foreach (Pieces p in pieces)
            {
                p.AvailablePlaces(board, pieces);
            }
            return pieces[0].ReturnCheck(colour);
        } // Finished
        static bool CheckCheckMate(char[,] board, List<Pieces> pieces, bool colour)
        {
            for (int i = 0; i < pieces.Count; i++)
            {
                if (pieces[i].ReturnColour() != colour)
                {
                    for (int j = 0; j < pieces[i].AvailablePlaces(board, pieces).Count; j++)
                    {
                        int avilable0 = pieces[i].AvailablePlaces(board, pieces)[j][0], avilable1 = pieces[i].AvailablePlaces(board, pieces)[j][1];
                        int ycoord = pieces[i].ReturnCoords()[0], xcoord = pieces[i].ReturnCoords()[1]; char type = pieces[i].ReturnType();
                        board[ycoord, xcoord] = '_';
                        board[avilable0, avilable1] = pieces[i].ReturnType();
                        pieces[i].ChangeToCoord(avilable0, avilable1);
                        Pieces temp = new Knight(100, 100, true);
                        for (int k = 0; k < pieces.Count; k++)
                        {
                            if (pieces[k].ReturnColour() != pieces[i].ReturnColour() && pieces[k].ReturnCoords()[0] == avilable0 && pieces[k].ReturnCoords()[1] == avilable1)
                            {
                                temp = pieces[k];
                                pieces.RemoveAt(k);
                                if (i > k)
                                {
                                    i--;
                                }
                                break;
                            }
                        }
                        pieces[0].ResetCheck();
                        if (CheckCheck(board, pieces, !colour) == false)
                        {
                            board[avilable0, avilable1] = '_';
                            board[ycoord, xcoord] = type;
                            pieces[i].ChangeToCoord(ycoord, xcoord);
                            pieces.Add(temp);
                            if (pieces[pieces.Count - 1].ReturnCoords()[0] == 100)
                            {
                                pieces.RemoveAt(pieces.Count - 1);
                            }
                            board[pieces[pieces.Count - 1].ReturnCoords()[0], pieces[pieces.Count - 1].ReturnCoords()[1]] = pieces[pieces.Count - 1].ReturnType();
                            return false;
                        }
                        board[avilable0, avilable1] = '_';
                        board[ycoord, xcoord] = type;
                        pieces[i].ChangeToCoord(ycoord, xcoord);
                        pieces.Add(temp);
                        if (pieces[pieces.Count - 1].ReturnCoords()[0] == 100)
                        {
                            pieces.RemoveAt(pieces.Count - 1);
                        }
                        board[pieces[pieces.Count - 1].ReturnCoords()[0], pieces[pieces.Count - 1].ReturnCoords()[1]] = pieces[pieces.Count - 1].ReturnType();
                    }
                }
            }
            return true;
        } // Finished
        static void Choices(char[,] board, List<Pieces> pieces, List<List<string>> takeBacks)
        {
            Console.Clear();
            int choice;
            string str = "Choose your difficulty" +
                "\nPlease enter the number corresponding to the type of game you want to play at\n" +
                "   1) To play Player VS Player game\n   2) To play PLayer versus AI (Unfinished)\n   3) To play different game modes (Unfinished)\n   9) To Exit";
            Console.WriteLine(str);
            while (!int.TryParse(Console.ReadLine(), out choice) || choice <= 0 || choice > 3 && choice != 9)
            {
                Console.Clear();
                Console.WriteLine(str);
                Console.WriteLine("Invalid input");
            }
            if (choice == 9)
            {
                Environment.Exit(0);
            }
            else if (choice == 1)
            {
                PlayerVSPlayer(board, pieces, takeBacks);
            }
            else if (choice == 2)
            {
                AIChoice(board, pieces, takeBacks);
            }
            else if (choice == 3)
            {
                Gamemodes(board, pieces, takeBacks);
            }
        }
        static void AIChoice(char[,] board, List<Pieces> pieces, List<List<string>> takeBacks)
        {
            start:
            Console.Clear();
            int choice;
            string str = "Choose your difficulty" +
                "\nPlease enter the number corresponding to the difficulty of the ai you want to play against\n" +
                "   1) Easy\n   2) Medium (In progress)\n   3) Hard (In progress)\n   4) Very hard (In progress)\n   9) Go back";
            Console.WriteLine(str);
            while (!int.TryParse(Console.ReadLine(), out choice) || choice <= 0 || choice > 4 && choice != 9)
            {
                Console.Clear();
                Console.WriteLine(str);
                Console.WriteLine("Invalid input");
            }
            if (choice == 9)
            {
            }
            else if (choice == 1)
            {
                EasyAI(board, pieces, takeBacks);
            }
            else if (choice == 2)
            {
                Console.Clear();
                Console.WriteLine(str);
                Console.WriteLine("I said it was in progress");
                Console.ReadKey(true);
                goto start;
            }
            else if (choice == 3)
            {
                Console.Clear();
                Console.WriteLine(str);
                Console.WriteLine("I said it was in progress");
                Console.ReadKey(true);
                goto start;
            }
            else if (choice == 4)
            {
                Console.Clear();
                Console.WriteLine(str);
                Console.WriteLine("I said it was in progress");
                Console.ReadKey(true);
                goto start;
            }
        }
        static void Gamemodes(char[,] board, List<Pieces> pieces, List<List<string>> takeBacks)
        {
            start:
            Console.Clear();
            int choice;
            string str = "Choose your difficulty" +
                "\nPlease enter the number corresponding to the game mode you want to play at\n" +
                "   1) Chess 960\n   2) Explosive Chess (In progress)\n   9) Go back \n   More game modes coming soon";
            Console.WriteLine(str);
            while (!int.TryParse(Console.ReadLine(), out choice) || choice <= 0 || choice > 2 && choice != 9)
            {
                Console.Clear();
                Console.WriteLine(str);
                Console.WriteLine("Invalid input");
            }
            if (choice == 9)
            {
            }
            else if (choice == 1)
            {
                board = CreateChess960Board();
                pieces = SetupPieces(board);
                PlayerVSPlayer(board, pieces, takeBacks);
            }
            else if (choice == 2)
            {
                Console.Clear();
                Console.WriteLine(str);
                Console.WriteLine("I said it was in progress");
                Console.ReadKey(true);
                goto start;
            }
        }
        static void PlayerVSPlayer(char[,] board, List<Pieces> pieces, List<List<string>> takeBacks)
        {
            Console.Clear();
            Console.WriteLine("Allow takebacks? (y) / (n)");
            string choice = Console.ReadLine().ToLower(); bool allowTakeBacks;
            while (choice != "y" && choice != "n")
            {
                Console.Clear();
                Console.WriteLine("Allow takebacks? (y) / (n)");
                Console.WriteLine("Invalid input");
                choice = Console.ReadLine().ToLower();
            }
            if (choice == "y")
            {
                Console.WriteLine("Take backs enabled");
                allowTakeBacks = true;
            }
            else
            {
                Console.WriteLine("Take backs disabled");
                allowTakeBacks = false;
            }
            Console.Clear();
            bool success;
            PrintBoard(board, pieces);
            while (true)
            {
                success = EnterMove(board, pieces, takeBacks, false, allowTakeBacks);
                while (!success)
                {
                    Console.Clear();
                    PrintBoard(board, pieces);
                    Console.WriteLine("Invalid move try again");
                    success = EnterMove(board, pieces, takeBacks, false, allowTakeBacks);
                }
                Console.Clear();
                if (pieces[0].ReturnCheckMate() || pieces[0].ReturnStalemate())
                {
                    break;
                }
                PrintBoard(board, pieces);
                Console.ReadKey(true);
                Console.Clear();
                PrintFlippedBoard(board, pieces);
                success = EnterMove(board, pieces, takeBacks,true, allowTakeBacks);
                while (!success)
                {
                    Console.Clear();
                    PrintFlippedBoard(board, pieces);
                    Console.WriteLine("Invalid move try again");
                    success = EnterMove(board, pieces, takeBacks, true, allowTakeBacks);
                }
                Console.Clear();
                if (pieces[0].ReturnCheckMate() || pieces[0].ReturnStalemate())
                {
                    break;
                }
                PrintFlippedBoard(board, pieces);
                Console.ReadKey(true);
                Console.Clear();
                PrintBoard(board, pieces);
            }
        }
        static void EasyAI(char[,] board, List<Pieces> pieces, List<List<string>> takeBacks)
        {
            Console.Clear();
            Console.WriteLine("Allow takebacks? (y) / (n)");
            string choice = Console.ReadLine().ToLower(); bool allowTakeBacks;
            while (choice != "y" && choice != "n")
            {
                Console.Clear();
                Console.WriteLine("Allow takebacks? (y) / (n)");
                Console.WriteLine("Invalid input");
                choice = Console.ReadLine().ToLower();
            }
            if (choice == "y")
            {
                Console.WriteLine("Take backs enabled");
                allowTakeBacks = true;
            }
            else
            {
                Console.WriteLine("Take backs disabled");
                allowTakeBacks = false;
            }
            Console.Clear();
            bool success;
            PrintBoard(board, pieces);
            while (true)
            {
                success = EnterMove(board, pieces, takeBacks, false, allowTakeBacks);
                while (!success)
                {
                    Console.Clear();
                    PrintBoard(board, pieces);
                    Console.WriteLine("Invalid move try again");
                    success = EnterMove(board, pieces, takeBacks, false, allowTakeBacks);
                }
                Console.Clear();
                if (pieces[0].ReturnCheckMate() || pieces[0].ReturnStalemate())
                {
                    break;
                }
                PrintBoard(board, pieces);
                EasyAIMove(board, pieces, takeBacks);
                if (pieces[0].ReturnCheckMate() || pieces[0].ReturnStalemate())
                {
                    PrintFlippedBoard(board, pieces);
                    break;
                }
                Console.Clear();
                PrintBoard(board, pieces);
            }
        }
        static void EasyAIMove(char[,] board, List<Pieces> pieces, List<List<string>> takeBacks)
        {
            int[] move = { 0, 0 };
            char piece = 'p';
            Random random = new Random();
            while (!MakeMove(board, pieces, takeBacks, move, piece, true))
            {
                int rnd = random.Next(0, 6);
                if (rnd == 0)
                    piece = 'p';
                else if (rnd == 1)
                    piece = 'R';
                else if (rnd == 2)
                    piece = 'k';
                else if (rnd == 3)
                    piece = 'B';
                else if (rnd == 4)
                    piece = 'K';
                else if (rnd == 5)
                    piece = 'Q';
                move[0] = random.Next(0,8);
                move[1] = random.Next(0, 8);
            }
        }
        static void Main()
        {
            while (true)
            {
                char[,] board = CreateBoard();
                List<Pieces> pieces = SetupPieces(board);
                List<List<string>> takeBacks = new List<List<string>>(); 
                Choices(board, pieces, takeBacks);
            }
        } // Main
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
                for (int i = xcoord+1; i < 8; i++)
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
