using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess_With_Forms
{
    public partial class Form1 : Form
    {
        private int n;
        private PictureBox[,] squareBoard;
        private char[,] board;
        public Form1(char[,] inboard)
        {
            board = inboard;
            InitializeComponent();
        }
        private void SetupPieces()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (board[i, j] == 'R')
                    {
                        squareBoard[i, j].Image = Properties.Resources.White_Rook;
                        squareBoard[i, j].Name = "R";
                    }
                }
            }
        }
        private void SetupBoard()
        {
            n = 8;
            squareBoard = new PictureBox[n, n];
            int left, top = 2;
            bool colour = false;
            for (int i = 0; i < n; i++)
            {
                left = 2;
                for (int j = 0; j < n; j++)
                {
                    squareBoard[i, j] = new PictureBox();
                    if (colour)
                    {
                        squareBoard[i, j].BackColor = Color.Green;
                        colour = false;
                    }
                    else
                    {
                        squareBoard[i, j].BackColor = Color.FromArgb(255, 225, 217, 209);
                        colour = true;
                    }
                    squareBoard[i, j].Location = new Point(left, top);
                    squareBoard[i, j].Size = new Size((Boardpanel.Width - 2) / 8, (Boardpanel.Height - 2) / 8);
                    left += (Boardpanel.Width - 2) / 8;
                    Boardpanel.Controls.Add(squareBoard[i, j]);
                    if (j == 7)
                    {
                        colour = !colour;
                    }
                }
                top += (Boardpanel.Height - 2) / 8;
            }
            SetupPieces();
        }
        private void ClearBoard()
        {
            bool colour = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (colour)
                    {
                        squareBoard[i, j].BackColor = Color.Green;
                        colour = false;
                    }
                    else
                    {
                        squareBoard[i, j].BackColor = Color.FromArgb(255, 225, 217, 209);
                        colour = true;
                    }
                    if (j == 7)
                    {
                        colour = !colour;
                    }
                }
            }
        }
        private List<int[]> AvailablePlaces(PictureBox piece)
        {
            List<int[]> list = new List<int[]>();
            for (int y = 0; y < piece.Top / 60; y++)
            {
                list.Add(new int[] { y, piece.Left / 60});
            }
            for (int y = piece.Bottom / 60; y < 8; y++)
            {
                list.Add(new int[] { y, piece.Left / 60 });
            }
            for (int x = 0; x < piece.Left / 60; x++)
            {
                list.Add(new int[] { piece.Top / 60, x });
            }
            for (int x = piece.Right / 60; x < 8; x++)
            {
                list.Add(new int[] { piece.Top / 60, x });
            }
            return list;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            SetupBoard();
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    squareBoard[y, x].Click += (sender1, e1) =>
                    {
                        PictureBox piece = sender1 as PictureBox;
                        if (piece.Image != null && piece.BackColor != Color.Blue)
                        {
                            ClearBoard();
                            squareBoard[piece.Top / 60, piece.Left / 60].BackColor = Color.Red;
                            foreach (int[] i in AvailablePlaces(piece))
                            {
                                squareBoard[i[0], i[1]].BackColor = Color.Blue;
                            }
                            for (int i = 0; i < 8; i++)
                            {
                                for (int j = 0; j < 8; j++)
                                {
                                    squareBoard[i, j].Click += (sender2, e2) =>
                                    {
                                        PictureBox piece2 = sender2 as PictureBox;
                                        if (piece2.BackColor == Color.Blue)
                                        {
                                            ClearBoard();
                                            squareBoard[piece2.Top / 60, piece2.Left / 60].Image = piece.Image;
                                            squareBoard[piece.Top / 60, piece.Left / 60].Name = piece.Name;
                                            squareBoard[piece.Top / 60, piece.Left / 60].Image = null;
                                            squareBoard[piece.Top / 60, piece.Left / 60].Name = "";
                                        }
                                        piece = sender2 as PictureBox;
                                    };
                                }
                            }
                        }
                        else if (piece.Image == null && piece.BackColor != Color.Blue)
                        {
                            ClearBoard();
                        }
                    };
                }
            }
        }
    }
}
