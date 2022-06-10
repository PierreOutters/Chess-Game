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
        //private List<Pieces> pieces = ;
        public Form1(char[,] inboard)
        {
            
            InitializeComponent();
            board = inboard;
        }
        private void Form1_Load(object sender, EventArgs e)
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
                    if (board[i, j] == 'R')
                    {
                        //squareBoard[i, j].Image = Properties.Resources.Chess_rlt60;
                        //squareBoard[i, j].Name += " R";
                    }
                    left += (Boardpanel.Width - 2) / 8;
                    Boardpanel.Controls.Add(squareBoard[i, j]);
                    if (j == 7)
                    {
                        colour = !colour;
                    }
                }
                top += (Boardpanel.Height - 2) / 8;
            }
        }
    }
}
