
using System;

namespace Ex05
{
    internal class TicTacToeBoard
    {
        public const int k_MinBoardSize = 4;
        public const int k_MaxBoardSize = 10;
        private readonly int r_BoardSize;
        private eCellState[,] m_MatrixBoard;

        public TicTacToeBoard(int i_BoardSize)
        {
            r_BoardSize = i_BoardSize;
            m_MatrixBoard = new eCellState[r_BoardSize, r_BoardSize];
            FillBoardWithBlankSpaces();
        }
      
        public eCellState GetCellValue(int i_MatrixRow, int i_MatrixColumn) 
        {
            return m_MatrixBoard[i_MatrixRow, i_MatrixColumn];
        }

        public void FillBoardWithBlankSpaces()
        {
            for (int row = 0; row < r_BoardSize; ++row)
            {
                for (int col = 0; col < r_BoardSize; ++col)
                {
                    m_MatrixBoard[row, col] = eCellState.Empty;
                }
            }
        }

        public int Size
        {
            get
            {
                return r_BoardSize;
            }
        }

        public bool IsCellEmpty(int i_Row, int i_Col)
        {
            return m_MatrixBoard[i_Row, i_Col] == eCellState.Empty;
        }
      
        public bool FillCell(int i_MatrixRow, int i_MatrixCol, eCellState i_PlayerSign)
        {
            bool successFill = false;

            if (isValidLength(i_MatrixRow, i_MatrixCol) && IsCellEmpty(i_MatrixRow, i_MatrixCol))
            {
                m_MatrixBoard[i_MatrixRow, i_MatrixCol] = i_PlayerSign;
                successFill = true;
            }   

            return successFill;
        }
      
        private bool isValidLength(int i_Row, int i_Col)
        {
            return (i_Row < r_BoardSize && i_Col < r_BoardSize && i_Row >= 0 && i_Col >= 0);
        }

        public bool CheckIfBoardIsFull()
        {
            bool isBoardFull = true;

            for (int i = 0; i < r_BoardSize; ++i)
            {
                for(int j = 0; j < r_BoardSize; ++j)
                {
                    if (m_MatrixBoard[i,j] == eCellState.Empty)
                    {
                        isBoardFull = false;
                        break;
                    }
                }
            }

            return isBoardFull;
        }
    }
}
