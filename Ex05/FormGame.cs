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
        private Game m_GameBoard;
        private displayButton[,] m_DisplayBoard;

        public FormGame(Game i_newGame)
        {
            m_GameBoard = i_newGame;
            m_DisplayBoard = CreateBoard(m_GameBoard.returnBoardLength());
            InitializeComponent();
            labelPlayer1Score.Text = m_GameBoard.Player1ScoreText;
            labelPlayer2Score.Text = m_GameBoard.Player2ScoreText;
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            int boardSize = m_GameBoard.returnBoardLength();
            const int k_ButtonSize = 60;
            const int k_Margin = 10;

            m_DisplayBoard = CreateBoard(boardSize);
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    m_DisplayBoard[i, j] = new displayButton(i, j);
                    m_DisplayBoard[i, j].Size = new Size(k_ButtonSize, k_ButtonSize);
                    int posX = k_Margin + (j * k_ButtonSize);
                    int posY = k_Margin + (i * k_ButtonSize);
                    m_DisplayBoard[i, j].Location = new Point(posX, posY);
                    m_DisplayBoard[i, j].Click += DisplayButtonClick;
                    this.Controls.Add(m_DisplayBoard[i, j]);
                }
            }

            int formWidth = (boardSize * k_ButtonSize) + (k_Margin * 2);
            int formHeight = (boardSize * k_ButtonSize) + 60;
            this.ClientSize = new Size(formWidth, formHeight);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.CenterToScreen();
            labelPlayer1Score.Text = m_GameBoard.Player1ScoreText;
            labelPlayer2Score.Text = m_GameBoard.Player2ScoreText;
            labelPlayer1Score.RightToLeft = RightToLeft.Yes;
            labelPlayer2Score.RightToLeft = RightToLeft.No;
            labelPlayer1Score.AutoSize = true;
            labelPlayer2Score.AutoSize = true;
            if (m_GameBoard.currentPlayer.Sign == eCellState.X)
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
        private displayButton[,] CreateBoard(int i_BoardSize)
        {
            displayButton[,] GameBoard = new displayButton[i_BoardSize, i_BoardSize];

            return GameBoard;
        }
        private void DisplayButtonClick(object sender, EventArgs e)
        {
            displayButton clickedButton = sender as displayButton;

            if (clickedButton.Text != string.Empty)
            {
                return;
            }
           
            m_GameBoard.MakeAHumanMove(clickedButton.Row, clickedButton.Col);
            bool isGameOver = MakeATurn(clickedButton);
            if (!isGameOver && m_GameBoard.IsAgainstComputer)
            {
                Point? location = m_GameBoard.MakeAComputerMove();
                if (location.HasValue)
                {
                    displayButton computerButton = m_DisplayBoard[location.Value.X, location.Value.Y];
                    MakeATurn(computerButton);
                }
            }
        }
        private bool MakeATurn(displayButton i_Button)
        {
            bool isWinner = m_GameBoard.CheckifThereIsAWinner();
            bool isTie = m_GameBoard.CheckIfThereIsATie();

            i_Button.draw(m_GameBoard.currentPlayer.Sign);
            labelPlayer1Score.Text = m_GameBoard.Player1ScoreText;
            labelPlayer2Score.Text = m_GameBoard.Player2ScoreText;
            if (m_GameBoard.currentPlayer.Sign == eCellState.X)
            {
                labelPlayer1Score.Font = new Font(labelPlayer1Score.Font, FontStyle.Bold);
                labelPlayer2Score.Font = new Font(labelPlayer2Score.Font, FontStyle.Regular);
            }
            else
            {
                labelPlayer1Score.Font = new Font(labelPlayer1Score.Font, FontStyle.Regular);
                labelPlayer2Score.Font = new Font(labelPlayer2Score.Font, FontStyle.Bold);
            }

            int boardSize = m_GameBoard.returnBoardLength();
            const int k_ButtonSize = 60;
            const int k_Margin = 10;
            int formWidth = (boardSize * k_ButtonSize) + (k_Margin * 3);
            int labelsY = (boardSize * k_ButtonSize) + (k_Margin * 3);
            int gapBetweenLabels = 15;
            int totalLabelsWidth = labelPlayer1Score.Width + gapBetweenLabels + labelPlayer2Score.Width;
            int startX = (formWidth - totalLabelsWidth) / 2;
            labelPlayer1Score.Location = new Point(startX, labelsY);
            labelPlayer2Score.Location = new Point(startX + labelPlayer1Score.Width + gapBetweenLabels, labelsY);
            if (isWinner || isTie)
            {
                askForRematch(isWinner, isTie);
            }

            return isWinner || isTie;
        }
        public void ResetBoard()
        {
            m_GameBoard.ResetLogicalBoard();
            ResetPysicalBoard();
        }
        private void ResetPysicalBoard()
        {
            foreach (displayButton button in m_DisplayBoard)
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

        private void askForRematch(bool i_IsWinner, bool i_IsTie)
        {
            string messageText = string.Empty;
            string messageTitle = string.Empty;

            if (i_IsWinner)
            {
                messageText = string.Format("{0} Won!\nWould you like to play another round?", m_GameBoard.currentPlayer.Name);
                messageTitle = "A Win!";
            }
            else if (i_IsTie)
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
