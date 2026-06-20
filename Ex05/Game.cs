using System;
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
            
           m_IsAgainstComputer = i_gameAgainstComputer;
            r_Player1 = new Player(eCellState.X, i_player1Name);
            r_Player2 = new Player(eCellState.O, i_player2Name);
            m_Board = new TicTacToeBoard(i_boardSize);

            m_CurrentPlayer = r_Player1;
        }

        public void Run() 
        {
            bool isCountinueToRematch = true;

            //r_UserInterface.GetGameSettingsFromUser(out int boardSize, out m_IsAgainstComputer);
            m_Engine = new GameEngine();
            while(isCountinueToRematch)
            {
               
                m_CurrentPlayer = r_Player1;
                runGameLoop();
                //isCountinueToRematch = r_UserInterface.AskForRematch();
            }
        }

        private void runGameLoop()
        {
            bool isGameOver = false;
            int boardRow = 0;
            int boardColumn = 0;

            while (!isGameOver)
            {
                //r_UserInterface.ClearScreen();
                //r_UserInterface.DrawBoard(m_Board);
                //r_UserInterface.AnnounceTurn(m_CurrentPlayer.Name);
                bool isQuitRequested = false;
                int boardRowIndex = 0;
                int boardColumnIndex = 0;
                if (m_CurrentPlayer == r_Player2 && m_IsAgainstComputer)
                {
                    GameEngine.ComputerMove(m_Board, r_Player2.Sign);
                }
                else
                {
                    //r_UserInterface.GetValidNextMoveFromUser(m_Board.GetLength(), out boardRow, out boardColumn, out isQuitRequested);
                    if (isQuitRequested)
                    {
                        //r_UserInterface.AnnounceGameStopped();
                        break;
                    }

                    boardRowIndex = boardRow - 1;
                    boardColumnIndex = boardColumn - 1;
                    bool isMoveSuccessful = m_Board.FillCell(boardRowIndex, boardColumnIndex, m_CurrentPlayer.Sign);
                    while (!isMoveSuccessful)
                    {
                        //r_UserInterface.ClearScreen();
                        //r_UserInterface.DrawBoard(m_Board);
                        //r_UserInterface.AnnounceInvalidMove();
                        //r_UserInterface.GetValidNextMoveFromUser(m_Board.GetLength(), out boardRow, out boardColumn, out isQuitRequested);
                        if (isQuitRequested)
                        {
                            //r_UserInterface.AnnounceGameStopped();
                            isGameOver = true;
                            break;
                        }

                        boardRowIndex = boardRow - 1;
                        boardColumnIndex = boardColumn - 1;
                        isMoveSuccessful = m_Board.FillCell(boardRowIndex, boardColumnIndex, m_CurrentPlayer.Sign);
                    }
                }

                //r_UserInterface.ClearScreen();
                //r_UserInterface.DrawBoard(m_Board);

                if (m_Engine.IsFullRowColumnOrDiagonalInBoard(m_Board))
                {
                    eCellState winningPlayerSign = m_Engine.GetWinningSign(r_Player1.Sign, r_Player2.Sign, m_CurrentPlayer.Sign);
                    Player winningPlayer = (winningPlayerSign == r_Player1.Sign) ? r_Player1 : r_Player2;
                    winningPlayer.Score++;
                    //r_UserInterface.AnnounceGameOver(m_CurrentPlayer.Name, winningPlayer.Name, winningPlayer.Score);
                    isGameOver = true;
                }
                else if (m_Engine.isFullBoard(m_Board))
                {
                    //r_UserInterface.AnnounceTie();
                    isGameOver = true;
                }
                else
                {
                    switchPlayer();
                }
            }
        }

        private void switchPlayer()
        {
            m_CurrentPlayer = (m_CurrentPlayer == r_Player1) ? r_Player2 : r_Player1;
        }
        public int returnBordlenght()
        {
            return m_Board.GetLength();
        }
    } 
}
