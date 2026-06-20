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
        public displayButton()
        {
            this.Enabled = false;
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
