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
        private readonly int k_cellSize = 60;
        private readonly int r_Row;
        private readonly int r_Col;
        public int Row
        {
            get { return r_Row; }
        }

        public int Col
        {
            get { return r_Col; }
        }
        public displayButton(int i_Row, int i_col)
        {
            r_Row = i_Row; 
            r_Col = i_col;   
            this.Enabled = true;
            Width = k_cellSize;
            Height = k_cellSize;
        }
        public void draw(eCellState i_Sign)
        {
            if(this.Text == string.Empty)
            {
                this.Text = i_Sign.ToString();
                this.Enabled = false;
            }
        }
        
    }
}
