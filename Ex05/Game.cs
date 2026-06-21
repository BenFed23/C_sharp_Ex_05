using System;
using System.Drawing;
using System.Reflection;
using System.Runtime.Remoting.Messaging;

namespace Ex05
{
    public class Game
    {
        private GameEngine m_Engine;
        private TicTacToeBoard m_Board;
        private readonly Player r_Player1;
        private readonly Player r_Player2;
        private Player m_CurrentPlayer;
        private bool m_IsAgainstComputer;

        public Game(string i_player1Name, string i_player2Name, bool i_gameAgainstComputer, int i_boardSize)
        {
            m_Engine = new GameEngine();
            m_IsAgainstComputer = i_gameAgainstComputer;
            r_Player1 = new Player(eCellState.X, i_player1Name);
            r_Player2 = new Player(eCellState.O, i_player2Name);
            m_Board = new TicTacToeBoard(i_boardSize);
            m_CurrentPlayer = r_Player1;
        }
        public bool MakeAHumanMove(int i_row, int i_colom)
        {
            bool isSucecfull = m_Board.FillCell(i_row, i_colom, m_CurrentPlayer.Sign);
            switchPlayer();

            return isSucecfull;
        }
        public void AddPointsToCurrentPlayer()
        {
            m_CurrentPlayer.Score++;
        }
        public Point MakeAComputreMove()
        {
            Point computerChosenCell = GameEngine.ComputerMove(m_Board,m_CurrentPlayer.Sign);
            switchPlayer();

            return computerChosenCell;
        }
        public bool CheckifThereIsAWinner() 
        {
            bool winnerExist = true;

            winnerExist = m_Engine.IsFullRowColumnOrDiagonalInBoard(m_Board);
            if (winnerExist)
            {
                eCellState winningPlayerSign = m_CurrentPlayer.Sign;
                Player winningPlayer = (winningPlayerSign == r_Player1.Sign) ? r_Player1 : r_Player2;
                winningPlayer.Score++;
            }
           
            return winnerExist;
        }
        public bool CheckIfThereIsATie()
        {
            bool tie = m_Engine.isFullBoard(m_Board);

            return tie;
            
        }
        public void ResetLogicalBoard()
        {
            m_Board.fillBoardWithBlankSpaces();
        }
        
        private void switchPlayer()
        {
            m_CurrentPlayer = (m_CurrentPlayer == r_Player1) ? r_Player2 : r_Player1;
        }
        public int returnBordlenght()
        {
            return m_Board.GetLength();
        }
        public Player currentPlayer 
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
        public string Player1ScoreText
        {
            get
            {
                return $" Player 1 score: {r_Player1.Score}";
            }
        }
        public string Player2ScoreText
        {
            get
            {
                return $" Player 2 score: {r_Player2.Score}";
            }
        }
        
    } 
}
