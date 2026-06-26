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
        private const int k_ButtonSize = 60;
        private const int k_Margin = 10;

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
            int formWidth;

            initializeGameButtons(boardSize, k_ButtonSize, k_Margin);
            setFormSizeAndPosition(boardSize, k_ButtonSize, k_Margin, out formWidth);
            initializeScoreLabels();
        }

        private void initializeGameButtons(int i_BoardSize, int i_ButtonSize, int i_Margin)
        {
            m_DisplayBoard = CreateBoard(i_BoardSize);

            for (int i = 0; i < i_BoardSize; i++)
            {
                for (int j = 0; j < i_BoardSize; j++)
                {
                    m_DisplayBoard[i, j] = new displayButton(i, j);
                    m_DisplayBoard[i, j].Size = new Size(i_ButtonSize, i_ButtonSize);
                    int posX = i_Margin + (j * i_ButtonSize);
                    int posY = i_Margin + (i * i_ButtonSize);
                    m_DisplayBoard[i, j].Location = new Point(posX, posY);
                    m_DisplayBoard[i, j].Click += DisplayButtonClick;
                    this.Controls.Add(m_DisplayBoard[i, j]);
                }
            }
        }

        private void setFormSizeAndPosition(int i_BoardSize, int i_ButtonSize, int i_Margin, out int o_FormWidth)
        {
            int formHeight = (i_BoardSize * i_ButtonSize) + 60;
            o_FormWidth = (i_BoardSize * i_ButtonSize) + (i_Margin * 2);
            this.ClientSize = new Size(o_FormWidth, formHeight);
            this.StartPosition = FormStartPosition.CenterScreen;

            this.CenterToScreen();
        }

        private void initializeScoreLabels()
        {
            labelPlayer1Score.RightToLeft = RightToLeft.Yes;
            labelPlayer2Score.RightToLeft = RightToLeft.No;
            labelPlayer1Score.AutoSize = true;
            labelPlayer2Score.AutoSize = true;

            updateScoreLabels();
        }

        private void updateScoreLabels()
        {
            int boardSize = m_GameBoard.returnBoardLength();

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

            int formWidth = (boardSize * k_ButtonSize) + (k_Margin * 3);
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

            if (clickedButton.Text == string.Empty)
            {
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
        }

        private bool MakeATurn(displayButton i_Button)
        {
            int boardSize = m_GameBoard.returnBoardLength();
            bool isWinner = m_GameBoard.IsWinnerExist();
            bool isTie = m_GameBoard.CheckIfThereIsATie();

            if (isWinner)
            {
                m_GameBoard.AddPointToWinningPlayer();
            }

            i_Button.draw(m_GameBoard.currentPlayer.Sign);
            updateScoreLabels();
            handleGameOverIfNeeded(isWinner, isTie);

            return isWinner || isTie;
        }

        private void handleGameOverIfNeeded(bool i_IsWinner, bool i_IsTie)
        {
            if (i_IsWinner || i_IsTie)
            {
                askForRematch(i_IsWinner, i_IsTie);
            }
        }

        public void ResetBoard()
        {
            m_GameBoard.ResetLogicalBoard();
            ResetPhysicalBoard();
            updateScoreLabels();
        }

        private void ResetPhysicalBoard()
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

            getGameOverMessageDetails(i_IsWinner, i_IsTie, out messageText, out messageTitle);
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

        private void getGameOverMessageDetails(bool i_IsWinner, bool i_IsTie, out string o_MessageText, out string o_MessageTitle)
        {
            o_MessageText = string.Empty;
            o_MessageTitle = string.Empty;

            if (i_IsWinner)
            {
                o_MessageText = string.Format("{0} Won!\nWould you like to play another round?", m_GameBoard.currentPlayer.Name);
                o_MessageTitle = "A Win!";
            }
            else if (i_IsTie)
            {
                o_MessageText = "Tie!\nWould you like to play another round?";
                o_MessageTitle = "A Tie!";
            }
        }
    }
}