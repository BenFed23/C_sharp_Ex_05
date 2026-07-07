using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ex05
{
    public class displayButton : Button
    {
        private readonly int r_Row;
        private readonly int r_Column;

        public int Row
        {
            get
            {
                return r_Row; 
            }
        }

        public int Col
        {
            get 
            {
                return r_Column; 
            }
        }

        public displayButton(int i_Row, int i_Column)
        {
            r_Row = i_Row; 
            r_Column = i_Column;   
            Enabled = true;
        }

        public void draw(eCellState i_Sign)
        {
            if (Text == string.Empty)
            {
                Text = i_Sign.ToString();
                Enabled = false;
            }
        }
    }
}
