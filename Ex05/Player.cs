using System;



namespace Ex05
{
    public class Player
    {
        private int m_Score;
        private readonly eCellState r_Sign;
        private readonly string r_Name;

        public Player(eCellState i_Sign, string i_Name) 
        {
            r_Sign = i_Sign;
            r_Name = i_Name;
            m_Score = 0;
        }

        public eCellState Sign
        {
            get
            {
                return r_Sign;
            }   
        }
      
        public int Score
        {
            get
            {
                return m_Score;
            }
            set
            {
                m_Score = value;
            }
        }
        public string Name 
        {
            get
            {
                return r_Name;
            }
        }
    }
}