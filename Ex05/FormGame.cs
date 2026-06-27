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
        private readonly Game r_GameBoard;
        private readonly displayButton[,] r_DisplayBoard;
        private const int k_ButtonSize = 60;
        private const int k_Margin = 10;

        public FormGame(Game i_newGame)
        {
            r_GameBoard = i_newGame;
            r_DisplayBoard = CreateBoard(r_GameBoard.returnBoardLength());
            InitializeComponent();
            labelPlayer1Score.Text = r_GameBoard.Player1ScoreText;
            labelPlayer2Score.Text = r_GameBoard.Player2ScoreText;
        }

        private void gameForm_Load(object sender, EventArgs e)
        {
            int boardSize = r_GameBoard.returnBoardLength();
            int formWidth;

            initializeGameButtons(boardSize, k_ButtonSize, k_Margin);
            setFormSizeAndPosition(boardSize, k_ButtonSize, k_Margin, out formWidth);
            initializeScoreLabels();
        }

        private void initializeGameButtons(int i_BoardSize, int i_ButtonSize, int i_Margin)
        {
            for (int i = 0; i < i_BoardSize; i++)
            {
                for (int j = 0; j < i_BoardSize; j++)
                {
                    r_DisplayBoard[i, j] = new displayButton(i, j);
                    r_DisplayBoard[i, j].Size = new Size(i_ButtonSize, i_ButtonSize);
                    int positionX = i_Margin + (j * i_ButtonSize);
                    int positionY = i_Margin + (i * i_ButtonSize);
                    r_DisplayBoard[i, j].Location = new Point(positionX, positionY);
                    r_DisplayBoard[i, j].Click += displayButtonClick;
                    this.Controls.Add(r_DisplayBoard[i, j]);
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
            labelPlayer1Score.AutoSize = true;
            labelPlayer2Score.AutoSize = true;
            updateScoreLabels();
        }

        private void updateScoreLabels()
        {
            int boardSize = r_GameBoard.returnBoardLength();

            labelPlayer1Score.Text = r_GameBoard.Player1ScoreText;
            labelPlayer2Score.Text = r_GameBoard.Player2ScoreText;
            if (r_GameBoard.currentPlayer.Sign == eCellState.X)
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
            int labelsYPosition = (boardSize * k_ButtonSize) + (k_Margin * 3);
            int gapBetweenLabels = 15;
            int totalLabelsWidth = labelPlayer1Score.Width + gapBetweenLabels + labelPlayer2Score.Width;
            int startXPosition = (formWidth - totalLabelsWidth) / 2;
            labelPlayer1Score.Location = new Point(startXPosition, labelsYPosition);
            labelPlayer2Score.Location = new Point(startXPosition + labelPlayer1Score.Width + gapBetweenLabels, labelsYPosition);
        }

        private displayButton[,] CreateBoard(int i_BoardSize)
        {
            displayButton[,] GameBoard = new displayButton[i_BoardSize, i_BoardSize];

            return GameBoard;
        }

        private void displayButtonClick(object sender, EventArgs e)
        {
            displayButton clickedButton = sender as displayButton;

            if (clickedButton.Text == string.Empty)
            {
                r_GameBoard.MakeAHumanMove(clickedButton.Row, clickedButton.Col);
                bool isGameOver = makeATurn(clickedButton);
                if (!isGameOver && r_GameBoard.IsAgainstComputer)
                {
                    Point? location = r_GameBoard.MakeAComputerMove();
                    if (location.HasValue)
                    {
                        displayButton computerButton = r_DisplayBoard[location.Value.X, location.Value.Y];
                        makeATurn(computerButton);
                    }
                }
            }
        }

        private bool makeATurn(displayButton i_Button)
        {
            int boardSize = r_GameBoard.returnBoardLength();
            bool isWinner = r_GameBoard.IsWinnerExist();
            bool isTie = r_GameBoard.CheckIfThereIsATie();

            if (isWinner)
            {
                r_GameBoard.AddPointToWinningPlayer();
            }

            i_Button.draw(r_GameBoard.currentPlayer.Sign);
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

        private void resetBoard()
        {
            r_GameBoard.ResetLogicalBoard();
            resetPhysicalBoard();
            updateScoreLabels();
        }

        private void resetPhysicalBoard()
        {
            foreach (displayButton button in r_DisplayBoard)
            {
                if (button != null)
                {
                    button.Enabled = true;
                    button.Text = string.Empty;
                }
            }
        }

        private void endGame()
        {
            this.Close();
        }

        private void askForRematch(bool i_IsWinner, bool i_IsTie)
        {
            string messageText = string.Empty;
            string messageTitle = string.Empty;

            getGameOverMessageDetails(i_IsWinner, i_IsTie, out messageText, out messageTitle);
            DialogResult gameOverUserResult = MessageBox.Show(messageText, messageTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (gameOverUserResult == DialogResult.Yes)
            {
                resetBoard();
            }
            else
            {
                endGame();
            }
        }

        private void getGameOverMessageDetails(bool i_IsWinner, bool i_IsTie, out string o_MessageText, out string o_MessageTitle)
        {
            o_MessageText = string.Empty;
            o_MessageTitle = string.Empty;

            if (i_IsWinner)
            {
                o_MessageText = string.Format("The winner is {0}!\nWould you like to play another round?", r_GameBoard.currentPlayer.Name);
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