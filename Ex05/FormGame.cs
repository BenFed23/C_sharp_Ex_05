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
        private const int k_GapBetweenLabels = 15;
        private const int k_WidthMarginCorrection = 3;
        private const int k_HeightMarginCorrection = 2;
        private const int k_FormTitleBarAndBordersHeight = 60;

        public FormGame(Game i_newGame)
        {
            r_GameBoard = i_newGame;
            r_DisplayBoard = CreateBoard(r_GameBoard.ReturnBoardLength());
            InitializeComponent();
            registerToGameEvents();
            labelPlayer1Score.Text = player1ScoreText();
            labelPlayer2Score.Text = player2ScoreText();
        }

        private void registerToGameEvents()
        {
            r_GameBoard.CellChanged += board_CellChanged;
            r_GameBoard.TurnOrScoreChanged += board_TurnOrScoreChanged;
            r_GameBoard.GameEndedWithWinner += board_GameEndedWithWinner;
            r_GameBoard.GameEndedWithTie += board_GameEndedWithTie;
        }

        private void board_CellChanged(int i_Row, int i_Col, eCellState i_Sign)
        {
            r_DisplayBoard[i_Row, i_Col].draw(i_Sign);
        }

        private void board_TurnOrScoreChanged()
        {
            updateScoreLabels();
        }

        private void board_GameEndedWithWinner(string i_WinnerName)
        {
            string messageText = string.Format("The winner is {0}!\nWould you like to play another round?", i_WinnerName);
            askForRematch(messageText, "A Win!");
        }

        private void board_GameEndedWithTie()
        {
            askForRematch("Tie!\nWould you like to play another round?", "A Tie!");
        }

        private void gameForm_Load(object sender, EventArgs e)
        {
            int boardSize = r_GameBoard.ReturnBoardLength();
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
            int formHeight = (i_BoardSize * i_ButtonSize) + k_FormTitleBarAndBordersHeight;
            o_FormWidth = (i_BoardSize * i_ButtonSize) + (i_Margin * k_HeightMarginCorrection);
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
            int boardSize = r_GameBoard.ReturnBoardLength();

            labelPlayer1Score.Text = player1ScoreText();
            labelPlayer2Score.Text = player2ScoreText();
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

            int formWidth = (boardSize * k_ButtonSize) + (k_Margin * k_WidthMarginCorrection);
            int labelsYPosition = (boardSize * k_ButtonSize) + (k_Margin * k_WidthMarginCorrection);
            int totalLabelsWidth = labelPlayer1Score.Width + k_GapBetweenLabels + labelPlayer2Score.Width;
            int startXPosition = (formWidth - totalLabelsWidth) / 2;
            labelPlayer1Score.Location = new Point(startXPosition, labelsYPosition);
            labelPlayer2Score.Location = new Point(startXPosition + labelPlayer1Score.Width + k_GapBetweenLabels, labelsYPosition);
        }

        private displayButton[,] CreateBoard(int i_BoardSize)
        {
            return new displayButton[i_BoardSize, i_BoardSize];
        }

        private void displayButtonClick(object sender, EventArgs e)
        {
            displayButton clickedButton = sender as displayButton;

            if (clickedButton.Text == string.Empty)
            {
                r_GameBoard.PlayRound(clickedButton.Row, clickedButton.Col);
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

        private void askForRematch(string i_MessageText, string i_MessageTitle)
        {
            DialogResult gameOverUserResult = MessageBox.Show(i_MessageText, i_MessageTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (gameOverUserResult == DialogResult.Yes)
            {
                resetBoard();
            }
            else
            {
                endGame();
            }
        }

        private string player1ScoreText()
        {
            return $"{r_GameBoard.GetPlayer1Name()}: {r_GameBoard.GetPlayer1Score()}"; 
        }
        private string player2ScoreText()
        {
            return $"{r_GameBoard.GetPlayer2Name()}: {r_GameBoard.GetPlayer2Score()}";
        }
    }
}