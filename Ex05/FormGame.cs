using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex05
{
    public partial class FormGame : Form
    {
        private Game m_gameboard;
        
        public FormGame(Game i_newGame)
        {
            m_gameboard = i_newGame;
            
            InitializeComponent();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            displayButton[,] newBoardDisplay = CreateBoard(m_gameboard.returnBordlenght());
        }
        private displayButton[,] CreateBoard(int i_bordSize) 
        {
            displayButton[,] GameBoard = new displayButton[i_bordSize, i_bordSize];
            return GameBoard;
        }

        
    }
}
