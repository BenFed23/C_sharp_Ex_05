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
        private readonly GameManager r_GameManager;
        private readonly DisplayButton[,] r_DisplayBoard;
        private const int k_ButtonSize = 60;
        private const int k_Margin = 10;
        private const int k_GapBetweenLabels = 15;
        private const int k_WidthMarginCorrection = 3;
        private const int k_HeightMarginCorrection = 2;
        private const int k_FormTitleBarAndBordersHeight = 60;

        public FormGame(GameManager i_newGameManager)
        {
            r_GameManager = i_newGameManager;
            r_DisplayBoard = createBoard(r_GameManager.ReturnBoardLength());
            InitializeComponent();
            registerToGameEvents();
            labelPlayer1Score.Text = player1ScoreText();
            labelPlayer2Score.Text = player2ScoreText();
        }

        private void registerToGameEvents()
        {
            r_GameManager.CellChanged += gameBoard_CellChanged;
            r_GameManager.TurnOrScoreChanged += gameBoard_TurnOrScoreChanged;
            r_GameManager.GameEndedWithWinner += board_GameEndedWithWinner;
            r_GameManager.GameEndedWithTie += board_GameEndedWithTie;
        }

        private void gameBoard_CellChanged(int i_Row, int i_Col, eCellState i_Sign)
        {
            r_DisplayBoard[i_Row, i_Col].draw(i_Sign);
        }

        private void gameBoard_TurnOrScoreChanged()
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
            int boardSize = r_GameManager.ReturnBoardLength();
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
                    r_DisplayBoard[i, j] = new DisplayButton(i, j);
                    r_DisplayBoard[i, j].Size = new Size(i_ButtonSize, i_ButtonSize);
                    int positionX = i_Margin + (j * i_ButtonSize);
                    int positionY = i_Margin + (i * i_ButtonSize);
                    r_DisplayBoard[i, j].Location = new Point(positionX, positionY);
                    r_DisplayBoard[i, j].Click += displayButton_Click;
                    Controls.Add(r_DisplayBoard[i, j]);
                }
            }
        }

        private void setFormSizeAndPosition(int i_BoardSize, int i_ButtonSize, int i_Margin, out int o_FormWidth)
        {
            int formHeight = (i_BoardSize * i_ButtonSize) + k_FormTitleBarAndBordersHeight;
            o_FormWidth = (i_BoardSize * i_ButtonSize) + (i_Margin * k_HeightMarginCorrection);
            ClientSize = new Size(o_FormWidth, formHeight);
            StartPosition = FormStartPosition.CenterScreen;
            CenterToScreen();
        }

        private void initializeScoreLabels()
        {
            labelPlayer1Score.AutoSize = true;
            labelPlayer2Score.AutoSize = true;
            updateScoreLabels();
        }

        private void updateScoreLabels()
        {
            int boardSize = r_GameManager.ReturnBoardLength();

            labelPlayer1Score.Text = player1ScoreText();
            labelPlayer2Score.Text = player2ScoreText();
            if (r_GameManager.CurrentPlayer.Sign == eCellState.X)
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

        private DisplayButton[,] createBoard(int i_BoardSize)
        {
            return new DisplayButton[i_BoardSize, i_BoardSize];
        }

        private void displayButton_Click(object sender, EventArgs e)
        {
            DisplayButton clickedButton = sender as DisplayButton;

            if (clickedButton.Text == string.Empty)
            {
                r_GameManager.PlayRound(clickedButton.Row, clickedButton.Col);
            }
        }

        private void resetBoard()
        {
            r_GameManager.ResetLogicalBoard();
            resetPhysicalBoard();
            updateScoreLabels();
        }

        private void resetPhysicalBoard()
        {
            foreach (DisplayButton button in r_DisplayBoard)
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
            Close();
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
            return $"{r_GameManager.GetPlayer1Name()}: {r_GameManager.GetPlayer1Score()}"; 
        }
        private string player2ScoreText()
        {
            return $"{r_GameManager.GetPlayer2Name()}: {r_GameManager.GetPlayer2Score()}";
        }
    }
}