using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex05
{
    public partial class FormGame : Form
    {
        private Game m_gameboard;
        private displayButton[,] m_displayBoard;

        public FormGame(Game i_newGame)
        {

            m_gameboard = i_newGame;
            m_displayBoard = CreateBoard(m_gameboard.returnBordlenght());
            InitializeComponent();
            labelPlayer1Score.Text = m_gameboard.Player1ScoreText;
            labelPlayer2Score.Text = m_gameboard.Player2ScoreText;

        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            int boardSize = m_gameboard.returnBordlenght();
            m_displayBoard = CreateBoard(boardSize);

            
            const int k_ButtonSize = 60;
            const int k_Margin = 10;

            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    m_displayBoard[i, j] = new displayButton(i, j);
                    m_displayBoard[i, j].Size = new Size(k_ButtonSize, k_ButtonSize);
                    int posX = k_Margin + (j * k_ButtonSize);
                    int posY = k_Margin + (i * k_ButtonSize);
                    m_displayBoard[i, j].Location = new Point(posX, posY);
                    m_displayBoard[i, j].Click += DisplayButtonClick;
                    this.Controls.Add(m_displayBoard[i, j]);
                }
            }

            int formWidth = (boardSize * k_ButtonSize) + (k_Margin * 2);
            int formHeight = (boardSize * k_ButtonSize) + 60;
            this.ClientSize = new Size(formWidth, formHeight);

            labelPlayer1Score.Text = m_gameboard.Player1ScoreText;
            labelPlayer2Score.Text = m_gameboard.Player2ScoreText;

            labelPlayer1Score.RightToLeft = RightToLeft.Yes;
            labelPlayer2Score.RightToLeft = RightToLeft.No;

            labelPlayer1Score.AutoSize = true;
            labelPlayer2Score.AutoSize = true;

            if (m_gameboard.currentPlayer.Sign == eCellState.X)
            {
                labelPlayer1Score.Font = new Font(labelPlayer1Score.Font, FontStyle.Bold);
                labelPlayer2Score.Font = new Font(labelPlayer2Score.Font, FontStyle.Regular);
            }
            else
            {
                labelPlayer1Score.Font = new Font(labelPlayer1Score.Font, FontStyle.Regular);
                labelPlayer2Score.Font = new Font(labelPlayer2Score.Font, FontStyle.Bold);
            }

            int labelsY = (boardSize * k_ButtonSize) + (k_Margin * 3);
            int gapBetweenLabels = 15;
            int totalLabelsWidth = labelPlayer1Score.Width + gapBetweenLabels + labelPlayer2Score.Width;
            int startX = (formWidth - totalLabelsWidth) / 2;

            labelPlayer1Score.Location = new Point(startX, labelsY);
            labelPlayer2Score.Location = new Point(startX + labelPlayer1Score.Width + gapBetweenLabels, labelsY);
        }
        private displayButton[,] CreateBoard(int i_bordSize)
        {
            displayButton[,] GameBoard = new displayButton[i_bordSize, i_bordSize];

            return GameBoard;
        }
        private void DisplayButtonClick(object sender, EventArgs e)
        {
            displayButton clickedButtun = sender as displayButton;
            if (clickedButtun.Text != string.Empty)
            {
                return;
            }
            else
            {
                m_gameboard.MakeAHumanMove(clickedButtun.Row, clickedButtun.Col);
                MakeATurn(clickedButtun);
                if (m_gameboard.IsAgainstComputer)
                {
                    Point location = m_gameboard.MakeAComputreMove();
                    displayButton computerButton = m_displayBoard[location.X, location.Y];
                    MakeATurn(computerButton);
                }
            }
        }
        private void MakeATurn(displayButton i_button)
        {
            i_button.draw(m_gameboard.currentPlayer.Sign);
            bool isWinner = m_gameboard.CheckifThereIsAWinner();
            bool isTie = m_gameboard.CheckIfThereIsATie();

            if (isWinner || isTie)
            {
                if (isWinner)
                {
                    m_gameboard.AddPointsToCurrentPlayer();
                }

                labelPlayer1Score.Text = m_gameboard.Player1ScoreText;
                labelPlayer2Score.Text = m_gameboard.Player2ScoreText;

                askForRemach(isWinner || isTie);
            }
        }
        public void ResetBoard()
        {

            m_gameboard.ResetLogicalBoard();
            ResetPysicalBoard();

        }
        private void ResetPysicalBoard()
        {
            foreach (displayButton button in m_displayBoard)
            {
                if (button != null)
                {
                    button.Enabled = true;
                    button.Text = string.Empty;
                }
            }
        }
        public void EndGame()
        {
            this.Close();
        }

        private void askForRemach(bool i_GameOver)
        {
            if (i_GameOver)
            {
                string messageText = string.Empty;
                string messageTitle = string.Empty;


                if (m_gameboard.CheckifThereIsAWinner())
                {
                    
                    messageText = string.Format("{0} Won!\nWould you like to play another round?", m_gameboard.currentPlayer.Name);
                    messageTitle = "A Win!";
                }
                else if (m_gameboard.CheckIfThereIsATie())
                {
                    messageText = "Tie!\nWould you like to play another round?";
                    messageTitle = "A Tie!";
                }
                DialogResult result = MessageBox.Show(messageText, messageTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (result == DialogResult.Yes)
                {
                    ResetBoard();
                }
                else
                {
                    EndGame();
                }
            }





        }
    }
}
