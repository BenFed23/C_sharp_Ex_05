using System;
using System.Drawing;
using System.Reflection;

namespace Ex05
{
    public class GameManager
    {
        private readonly GameEngine r_GameEngine;
        private readonly TicTacToeBoard r_Board;
        private readonly Player r_Player1;
        private readonly Player r_Player2;
        private Player m_CurrentPlayer;
        private bool m_IsAgainstComputer;

        public event Action<int, int, eCellState> CellChanged;
        public event Action<string> GameEndedWithWinner;
        public event Action GameEndedWithTie;
        public event Action TurnOrScoreChanged;

        public GameManager(string i_Player1Name, string i_Player2Name, bool i_GameAgainstComputer, int i_BoardSize)
        {
            r_GameEngine = new GameEngine();
            m_IsAgainstComputer = i_GameAgainstComputer;
            r_Player1 = new Player(eCellState.X, i_Player1Name);
            r_Player2 = new Player(eCellState.O, i_Player2Name);
            r_Board = new TicTacToeBoard(i_BoardSize);
            m_CurrentPlayer = r_Player1;
        }

        public void PlayRound(int i_Row, int i_Column)
        {
            if (r_Board.FillCell(i_Row, i_Column, m_CurrentPlayer.Sign))
            {
                bool isGameOver = handlePostMoveActions(i_Row, i_Column);
                if (!isGameOver && m_IsAgainstComputer)
                {
                    Point? computerMove = makeAComputerMove();
                    if (computerMove.HasValue)
                    {
                        r_Board.FillCell(computerMove.Value.Y, computerMove.Value.X, m_CurrentPlayer.Sign);
                        handlePostMoveActions(computerMove.Value.Y, computerMove.Value.X);
                    }

                }
            }
        }

        private bool handlePostMoveActions(int i_Row, int i_Column)
        {
            bool isGameOver = checkGameOverStates();

            onCellChanged(i_Row, i_Column, m_CurrentPlayer.Sign);
            if (!isGameOver)
            {
                switchPlayer();
                onTurnOrScoreChanged();
            }

            return isGameOver;
        }

        private bool checkGameOverStates()
        {
            bool isGameOver = false;

            if (isWinnerExist())
            {
                addPointToWinningPlayer();
                onTurnOrScoreChanged();
                Player gameWinner = (m_CurrentPlayer == r_Player1) ? r_Player2 : r_Player1;
                onGameEndedWithWinner(gameWinner.Name);
                isGameOver = true;
            }
            else if (checkIfThereIsATie())
            {
                onGameEndedWithTie();
                isGameOver = true;
            }

            return isGameOver;
        }

        private Point? makeAComputerMove()
        {
            return r_GameEngine.ComputerMove(r_Board,m_CurrentPlayer.Sign);
        }

        private void addPointToWinningPlayer()
        {
            Player winningPlayer = (m_CurrentPlayer == r_Player1) ? r_Player2 : r_Player1;
            winningPlayer.Score++;
        }

        private bool isWinnerExist()
        {
            return r_GameEngine.IsFullRowColumnOrDiagonalInBoard(r_Board);
        }

        private bool checkIfThereIsATie()
        {
            return r_GameEngine.isFullBoard(r_Board);
        }

        public void ResetLogicalBoard()
        {
            r_Board.FillBoardWithBlankSpaces();
            m_CurrentPlayer = r_Player1;
        }

        private void switchPlayer()
        {
            m_CurrentPlayer = (m_CurrentPlayer == r_Player1) ? r_Player2 : r_Player1;
        }

        public int ReturnBoardLength()
        {
            return r_Board.Size;
        }

        protected virtual void onCellChanged(int i_Row, int i_Col, eCellState i_Sign)
        {
            if (CellChanged != null)
            {
                CellChanged.Invoke(i_Row, i_Col, i_Sign);
            }
        }

        protected virtual void onGameEndedWithWinner(string i_WinnerName)
        {
            if (GameEndedWithWinner != null)
            {
                GameEndedWithWinner.Invoke(i_WinnerName);
            }
        }

        protected virtual void onGameEndedWithTie()
        {
            if (GameEndedWithTie != null)
            {
                GameEndedWithTie.Invoke();
            }
        }

        protected virtual void onTurnOrScoreChanged()
        {
            if (TurnOrScoreChanged != null)
            {
                TurnOrScoreChanged.Invoke();
            }
        }

        public Player CurrentPlayer
        {
            get 
            {
                return m_CurrentPlayer; 
            }
            set 
            {
                m_CurrentPlayer = value; 
            }
        }

        public bool IsAgainstComputer
        {
            get
            {
                return m_IsAgainstComputer;
            }
            set 
            {
                m_IsAgainstComputer = value;
            }
        }

        public string GetPlayer1Name()
        {
           return r_Player1.Name;
        }

        public string GetPlayer2Name()
        {
            return r_Player2.Name;
        }

        public int GetPlayer1Score()
        {
            return r_Player1.Score; 
        }

        public int GetPlayer2Score()
        {
            return r_Player2.Score;
        } 
    } 
}